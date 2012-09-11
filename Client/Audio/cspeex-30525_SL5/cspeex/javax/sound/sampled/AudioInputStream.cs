namespace javax.sound.sampled
{
	/// <summary>
	/// An audio input stream is an input stream with a specified audio format and
	/// length.
	/// </summary>
	/// <remarks>
	/// An audio input stream is an input stream with a specified audio format and
	/// length.  The length is expressed in sample frames, not bytes.
	/// Several methods are provided for reading a certain number of bytes from
	/// the stream, or an unspecified number of bytes.
	/// The audio input stream keeps track  of the last byte that was read.
	/// You can skip over an arbitrary number of bytes to get to a later position
	/// for reading. An audio input stream may support marks.  When you set a mark,
	/// the current position is remembered so that you can return to it later.
	/// <p>
	/// The <code>AudioSystem</code> class includes many methods that manipulate
	/// <code>AudioInputStream</code> objects.
	/// For example, the methods let you:
	/// <ul>
	/// <li> obtain an
	/// audio input stream from an external audio file, stream, or URL
	/// <li> write an external file from an audio input stream
	/// <li> convert an audio input stream to a different audio format
	/// </ul>
	/// </remarks>
	/// <author>David Rivas</author>
	/// <author>Kara Kytle</author>
	/// <author>Florian Bomers</author>
	/// <version>1.34, 05/11/17</version>
	/// <seealso cref="AudioSystem">AudioSystem</seealso>
	/// <seealso cref="Clip#open(AudioInputStream)">Clip.open(AudioInputStream)</seealso>
	/// <since>1.3</since>
	public class AudioInputStream : java.io.InputStream
	{
		/// <summary>
		/// The <code>InputStream</code> from which this <code>AudioInputStream</code>
		/// object was constructed.
		/// </summary>
		/// <remarks>
		/// The <code>InputStream</code> from which this <code>AudioInputStream</code>
		/// object was constructed.
		/// </remarks>
		private java.io.InputStream stream;

		/// <summary>The format of the audio data contained in the stream.</summary>
		/// <remarks>The format of the audio data contained in the stream.</remarks>
		protected AudioFormat format;

		/// <summary>This stream's length, in sample frames.</summary>
		/// <remarks>This stream's length, in sample frames.</remarks>
		protected long frameLength;

		/// <summary>The size of each frame, in bytes.</summary>
		/// <remarks>The size of each frame, in bytes.</remarks>
		protected int frameSize;

		/// <summary>The current position in this stream, in sample frames (zero-based).</summary>
		/// <remarks>The current position in this stream, in sample frames (zero-based).</remarks>
		protected long framePos;

		/// <summary>The position where a mark was set.</summary>
		/// <remarks>The position where a mark was set.</remarks>
		private long markpos;

		/// <summary>
		/// When the underlying stream could only return
		/// a non-integral number of frames, store
		/// the remainder in a temporary buffer
		/// </summary>
		private byte[] pushBackBuffer = null;

		/// <summary>number of valid bytes in the pushBackBuffer</summary>
		private int pushBackLen = 0;

		/// <summary>MarkBuffer at mark position</summary>
		private byte[] markPushBackBuffer = null;

		/// <summary>number of valid bytes in the markPushBackBuffer</summary>
		private int markPushBackLen = 0;

		/// <summary>
		/// Constructs an audio input stream that has the requested format and length in sample frames,
		/// using audio data from the specified input stream.
		/// </summary>
		/// <remarks>
		/// Constructs an audio input stream that has the requested format and length in sample frames,
		/// using audio data from the specified input stream.
		/// </remarks>
		/// <param name="stream">
		/// the stream on which this <code>AudioInputStream</code>
		/// object is based
		/// </param>
		/// <param name="format">the format of this stream's audio data</param>
		/// <param name="length">the length in sample frames of the data in this stream</param>
		public AudioInputStream(java.io.InputStream stream, AudioFormat format, 
			long length) : base()
		{
			this.format = format;
			this.frameLength = length;
			this.frameSize = format.getFrameSize();
			// any frameSize that is not well-defined will
			// cause that this stream will be read in bytes
			if (this.frameSize == AudioSystem.NOT_SPECIFIED || frameSize <= 0)
			{
				this.frameSize = 1;
			}
			this.stream = stream;
			framePos = 0;
			markpos = 0;
		}

		/// <summary>
		/// Constructs an audio input stream that reads its data from the target
		/// data line indicated.
		/// </summary>
		/// <remarks>
		/// Constructs an audio input stream that reads its data from the target
		/// data line indicated.  The format of the stream is the same as that of
		/// the target data line, and the length is AudioSystem#NOT_SPECIFIED.
		/// </remarks>
		/// <param name="line">the target data line from which this stream obtains its data.</param>
		/// <seealso cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</seealso>
		public AudioInputStream(TargetDataLine line)
		{
			AudioInputStream.TargetDataLineInputStream tstream = new AudioInputStream.TargetDataLineInputStream
				(this, line);
			format = line.getFormat();
			frameLength = AudioSystem.NOT_SPECIFIED;
			frameSize = format.getFrameSize();
			if (frameSize == AudioSystem.NOT_SPECIFIED || frameSize <= 0)
			{
				frameSize = 1;
			}
			this.stream = tstream;
			framePos = 0;
			markpos = 0;
		}

		/// <summary>Obtains the audio format of the sound data in this audio input stream.</summary>
		/// <remarks>Obtains the audio format of the sound data in this audio input stream.</remarks>
		/// <returns>an audio format object describing this stream's format</returns>
		public virtual AudioFormat getFormat()
		{
			return format;
		}

		/// <summary>Obtains the length of the stream, expressed in sample frames rather than bytes.
		/// 	</summary>
		/// <remarks>Obtains the length of the stream, expressed in sample frames rather than bytes.
		/// 	</remarks>
		/// <returns>the length in sample frames</returns>
		public virtual long getFrameLength()
		{
			return frameLength;
		}

		/// <summary>Reads the next byte of data from the audio input stream.</summary>
		/// <remarks>
		/// Reads the next byte of data from the audio input stream.  The audio input
		/// stream's frame size must be one byte, or an <code>IOException</code>
		/// will be thrown.
		/// </remarks>
		/// <returns>the next byte of data, or -1 if the end of the stream is reached</returns>
		/// <exception cref="System.IO.IOException">if an input or output error occurs</exception>
		/// <seealso cref="read(byte[], int, int)">read(byte[], int, int)</seealso>
		/// <seealso cref="read(byte[])">read(byte[])</seealso>
		/// <seealso cref="available()"><p></seealso>
		public override int read()
		{
			if (frameSize != 1)
			{
				throw new System.IO.IOException("cannot read a single byte if frame size > 1");
			}
			byte[] data = new byte[1];
			int temp = read(data);
			if (temp <= 0)
			{
				// we have a weird situation if read(byte[]) returns 0!
				return -1;
			}
			return data[0] & unchecked((int)(0xFF));
		}

		/// <summary>
		/// Reads some number of bytes from the audio input stream and stores them into
		/// the buffer array <code>b</code>.
		/// </summary>
		/// <remarks>
		/// Reads some number of bytes from the audio input stream and stores them into
		/// the buffer array <code>b</code>. The number of bytes actually read is
		/// returned as an integer. This method blocks until input data is
		/// available, the end of the stream is detected, or an exception is thrown.
		/// <p>This method will always read an integral number of frames.
		/// If the length of the array is not an integral number
		/// of frames, a maximum of <code>b.length - (b.length % frameSize)
		/// </code> bytes will be read.
		/// </remarks>
		/// <param name="b">the buffer into which the data is read</param>
		/// <returns>
		/// the total number of bytes read into the buffer, or -1 if there
		/// is no more data because the end of the stream has been reached
		/// </returns>
		/// <exception cref="System.IO.IOException">if an input or output error occurs</exception>
		/// <seealso cref="read(byte[], int, int)">read(byte[], int, int)</seealso>
		/// <seealso cref="read()">read()</seealso>
		/// <seealso cref="available()">available()</seealso>
		public override int read(byte[] b)
		{
			return read(b, 0, b.Length);
		}

		/// <summary>
		/// Reads up to a specified maximum number of bytes of data from the audio
		/// stream, putting them into the given byte array.
		/// </summary>
		/// <remarks>
		/// Reads up to a specified maximum number of bytes of data from the audio
		/// stream, putting them into the given byte array.
		/// <p>This method will always read an integral number of frames.
		/// If <code>len</code> does not specify an integral number
		/// of frames, a maximum of <code>len - (len % frameSize)
		/// </code> bytes will be read.
		/// </remarks>
		/// <param name="b">the buffer into which the data is read</param>
		/// <param name="off">
		/// the offset, from the beginning of array <code>b</code>, at which
		/// the data will be written
		/// </param>
		/// <param name="len">the maximum number of bytes to read</param>
		/// <returns>
		/// the total number of bytes read into the buffer, or -1 if there
		/// is no more data because the end of the stream has been reached
		/// </returns>
		/// <exception cref="System.IO.IOException">if an input or output error occurs</exception>
		/// <seealso cref="read(byte[])">read(byte[])</seealso>
		/// <seealso cref="read()">read()</seealso>
		/// <seealso cref="skip(long)">skip(long)</seealso>
		/// <seealso cref="available()">available()</seealso>
		public override int read(byte[] b, int off, int len)
		{
			// make sure we don't read fractions of a frame.
			if ((len % frameSize) != 0)
			{
				len -= (len % frameSize);
				if (len == 0)
				{
					return 0;
				}
			}
			if (frameLength != AudioSystem.NOT_SPECIFIED)
			{
				if (framePos >= frameLength)
				{
					return -1;
				}
				else
				{
					// don't try to read beyond our own set length in frames
					if ((len / frameSize) > (frameLength - framePos))
					{
						len = (int)(frameLength - framePos) * frameSize;
					}
				}
			}
			int bytesRead = 0;
			int thisOff = off;
			// if we've bytes left from last call to read(),
			// use them first
			if (pushBackLen > 0 && len >= pushBackLen)
			{
				System.Array.Copy(pushBackBuffer, 0, b, off, pushBackLen);
				thisOff += pushBackLen;
				len -= pushBackLen;
				bytesRead += pushBackLen;
				pushBackLen = 0;
			}
			int thisBytesRead = stream.read(b, thisOff, len);
			if (thisBytesRead == -1)
			{
				return -1;
			}
			if (thisBytesRead > 0)
			{
				bytesRead += thisBytesRead;
			}
			if (bytesRead > 0)
			{
				pushBackLen = bytesRead % frameSize;
				if (pushBackLen > 0)
				{
					// copy everything we got from the beginning of the frame
					// to our pushback buffer
					if (pushBackBuffer == null)
					{
						pushBackBuffer = new byte[frameSize];
					}
					System.Array.Copy(b, off + bytesRead - pushBackLen, pushBackBuffer, 0, pushBackLen
						);
					bytesRead -= pushBackLen;
				}
				// make sure to update our framePos
				framePos += bytesRead / frameSize;
			}
			return bytesRead;
		}

		/// <summary>
		/// Skips over and discards a specified number of bytes from this
		/// audio input stream.
		/// </summary>
		/// <remarks>
		/// Skips over and discards a specified number of bytes from this
		/// audio input stream.
		/// </remarks>
		/// <param name="n">the requested number of bytes to be skipped</param>
		/// <returns>the actual number of bytes skipped</returns>
		/// <exception cref="System.IO.IOException">if an input or output error occurs</exception>
		/// <seealso cref="read()">read()</seealso>
		/// <seealso cref="available()">available()</seealso>
		public override long skip(long n)
		{
			// make sure not to skip fractional frames
			if ((n % frameSize) != 0)
			{
				n -= (n % frameSize);
			}
			if (frameLength != AudioSystem.NOT_SPECIFIED)
			{
				// don't skip more than our set length in frames.
				if ((n / frameSize) > (frameLength - framePos))
				{
					n = (frameLength - framePos) * frameSize;
				}
			}
			long temp = stream.skip(n);
			// if no error, update our position.
			if (temp % frameSize != 0)
			{
				// Throw an IOException if we've skipped a fractional number of frames
				throw new System.IO.IOException("Could not skip an integer number of frames.");
			}
			if (temp >= 0)
			{
				framePos += temp / frameSize;
			}
			return temp;
		}

		/// <summary>
		/// Returns the maximum number of bytes that can be read (or skipped over) from this
		/// audio input stream without blocking.
		/// </summary>
		/// <remarks>
		/// Returns the maximum number of bytes that can be read (or skipped over) from this
		/// audio input stream without blocking.  This limit applies only to the next invocation of
		/// a <code>read</code> or <code>skip</code> method for this audio input stream; the limit
		/// can vary each time these methods are invoked.
		/// Depending on the underlying stream,an IOException may be thrown if this
		/// stream is closed.
		/// </remarks>
		/// <returns>the number of bytes that can be read from this audio input stream without blocking
		/// 	</returns>
		/// <exception cref="System.IO.IOException">if an input or output error occurs</exception>
		/// <seealso cref="read(byte[], int, int)">read(byte[], int, int)</seealso>
		/// <seealso cref="read(byte[])">read(byte[])</seealso>
		/// <seealso cref="read()">read()</seealso>
		/// <seealso cref="skip(long)">skip(long)</seealso>
		public override int available()
		{
			int temp = stream.available();
			// don't return greater than our set length in frames
			if ((frameLength != AudioSystem.NOT_SPECIFIED) && ((temp / frameSize) > 
				(frameLength - framePos)))
			{
				return (int)(frameLength - framePos) * frameSize;
			}
			else
			{
				return temp;
			}
		}

		/// <summary>
		/// Closes this audio input stream and releases any system resources associated
		/// with the stream.
		/// </summary>
		/// <remarks>
		/// Closes this audio input stream and releases any system resources associated
		/// with the stream.
		/// </remarks>
		/// <exception cref="System.IO.IOException">if an input or output error occurs</exception>
		public override void close()
		{
			stream.close();
		}

		/// <summary>Marks the current position in this audio input stream.</summary>
		/// <remarks>Marks the current position in this audio input stream.</remarks>
		/// <param name="readlimit">
		/// the maximum number of bytes that can be read before
		/// the mark position becomes invalid.
		/// </param>
		/// <seealso cref="reset()">reset()</seealso>
		/// <seealso cref="markSupported()">markSupported()</seealso>
		public override void mark(int readlimit)
		{
			stream.mark(readlimit);
			if (markSupported())
			{
				markpos = framePos;
				// remember the pushback buffer
				markPushBackLen = pushBackLen;
				if (markPushBackLen > 0)
				{
					if (markPushBackBuffer == null)
					{
						markPushBackBuffer = new byte[frameSize];
					}
					System.Array.Copy(pushBackBuffer, 0, markPushBackBuffer, 0, markPushBackLen);
				}
			}
		}

		/// <summary>
		/// Repositions this audio input stream to the position it had at the time its
		/// <code>mark</code> method was last invoked.
		/// </summary>
		/// <remarks>
		/// Repositions this audio input stream to the position it had at the time its
		/// <code>mark</code> method was last invoked.
		/// </remarks>
		/// <exception cref="System.IO.IOException">if an input or output error occurs.</exception>
		/// <seealso cref="mark(int)">mark(int)</seealso>
		/// <seealso cref="markSupported()">markSupported()</seealso>
		public override void reset()
		{
			stream.reset();
			framePos = markpos;
			// re-create the pushback buffer
			pushBackLen = markPushBackLen;
			if (pushBackLen > 0)
			{
				if (pushBackBuffer == null)
				{
					pushBackBuffer = new byte[frameSize - 1];
				}
				System.Array.Copy(markPushBackBuffer, 0, pushBackBuffer, 0, pushBackLen);
			}
		}

		/// <summary>
		/// Tests whether this audio input stream supports the <code>mark</code> and
		/// <code>reset</code> methods.
		/// </summary>
		/// <remarks>
		/// Tests whether this audio input stream supports the <code>mark</code> and
		/// <code>reset</code> methods.
		/// </remarks>
		/// <returns>
		/// <code>true</code> if this stream supports the <code>mark</code>
		/// and <code>reset</code> methods; <code>false</code> otherwise
		/// </returns>
		/// <seealso cref="mark(int)">mark(int)</seealso>
		/// <seealso cref="reset()">reset()</seealso>
		public override bool markSupported()
		{
			return stream.markSupported();
		}

		/// <summary>Private inner class that makes a TargetDataLine look like an InputStream.
		/// 	</summary>
		/// <remarks>Private inner class that makes a TargetDataLine look like an InputStream.
		/// 	</remarks>
		private class TargetDataLineInputStream : java.io.InputStream
		{
			/// <summary>The TargetDataLine on which this TargetDataLineInputStream is based.</summary>
			/// <remarks>The TargetDataLine on which this TargetDataLineInputStream is based.</remarks>
			internal TargetDataLine line;

			internal TargetDataLineInputStream(AudioInputStream _enclosing, TargetDataLine
				 line) : base()
			{
				this._enclosing = _enclosing;
				this.line = line;
			}

			/// <exception cref="System.IO.IOException"></exception>
			public override int available()
			{
				return this.line.available();
			}

			//$$fb 2001-07-16: added this method to correctly close the underlying TargetDataLine.
			// fixes bug 4479984
			/// <exception cref="System.IO.IOException"></exception>
			public override void close()
			{
				// the line needs to be flushed and stopped to avoid a dead lock...
				// Probably related to bugs 4417527, 4334868, 4383457
				if (this.line.isActive())
				{
					this.line.flush();
					this.line.stop();
				}
				this.line.close();
			}

			/// <exception cref="System.IO.IOException"></exception>
			public override int read()
			{
				byte[] b = new byte[1];
				int value = this.read(b, 0, 1);
				if (value == -1)
				{
					return -1;
				}
				value = (int)b[0];
				if (this.line.getFormat().getEncoding().Equals(AudioFormat.Encoding.PCM_SIGNED
					))
				{
					value += 128;
				}
				return value;
			}

			/// <exception cref="System.IO.IOException"></exception>
			public override int read(byte[] b, int off, int len)
			{
				try
				{
					return this.line.read(b, off, len);
				}
				catch (System.ArgumentException e)
				{
					throw new System.IO.IOException(e.Message);
				}
			}

			private readonly AudioInputStream _enclosing;
		}
	}
}
