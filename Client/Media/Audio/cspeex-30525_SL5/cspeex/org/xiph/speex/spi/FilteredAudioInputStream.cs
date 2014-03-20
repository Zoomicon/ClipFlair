namespace org.xiph.speex.spi
{
	/// <summary>
	/// A <code>FilteredAudioInputStream</code> is an AudioInputStream with buffers
	/// to facilitate transcoding the audio.
	/// </summary>
	/// <remarks>
	/// A <code>FilteredAudioInputStream</code> is an AudioInputStream with buffers
	/// to facilitate transcoding the audio.
	/// A first byte array can buffer the data from the underlying inputstream until
	/// sufficient data for transcoding is present.
	/// A second byte array can hold the transcoded audio data, ready to be read out
	/// by the stream user.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public abstract class FilteredAudioInputStream : javax.sound.sampled.AudioInputStream
	{
		/// <summary>The default size of the buffer.</summary>
		/// <remarks>The default size of the buffer.</remarks>
		public const int DEFAULT_BUFFER_SIZE = 2048;

		/// <summary>The underlying inputStream.</summary>
		/// <remarks>The underlying inputStream.</remarks>
		protected java.io.InputStream @in;

		/// <summary>The internal buffer array where the data is stored.</summary>
		/// <remarks>
		/// The internal buffer array where the data is stored. When necessary,
		/// it may be replaced by another array of a different size.
		/// </remarks>
		protected byte[] buf;

		/// <summary>The index one greater than the index of the last valid byte in the buffer.
		/// 	</summary>
		/// <remarks>
		/// The index one greater than the index of the last valid byte in the buffer.
		/// This value is always in the range 0 through <code>buf.length</code>;
		/// elements <code>buf[0]</code> through <code>buf[count-1]</code> contain
		/// buffered input data obtained and filtered from the underlying inputstream.
		/// </remarks>
		protected int count;

		/// <summary>The current position in the buffer.</summary>
		/// <remarks>
		/// The current position in the buffer. This is the index of the next
		/// character to be read from the <code>buf</code> array.
		/// <p>
		/// This value is always in the range 0 through <code>count</code>.
		/// If it is less than <code>count</code>, then <code>buf[pos]</code> is the
		/// next byte to be supplied as input.
		/// If it is equal to <code>count</code>, then the next <code>read</code> or
		/// <code>skip</code> operation will require more bytes to be read and
		/// filtered from the underlying inputstream.
		/// </remarks>
		/// <seealso cref="buf">buf</seealso>
		protected int pos;

		/// <summary>
		/// The value of the <code>pos</code> field at the time the last
		/// <code>mark</code> method was called.
		/// </summary>
		/// <remarks>
		/// The value of the <code>pos</code> field at the time the last
		/// <code>mark</code> method was called.
		/// <p>
		/// This value is always in the range -1 through <code>pos</code>.
		/// If there is no marked position in the inputstream, this field is -1.
		/// If there is a marked position in the inputstream, then
		/// <code>buf[markpos]</code> is the first byte to be supplied as input after
		/// a <code>reset</code> operation.
		/// If <code>markpos</code> is not -1, then all bytes from positions
		/// <code>buf[markpos]</code> through <code>buf[pos-1]</code> must remain in
		/// the buffer array (though they may be moved to another place in the buffer
		/// array, with suitable adjustments to the values of <code>count</code>,
		/// <code>pos</code>, and <code>markpos</code>); they may not be discarded
		/// unless and until the difference between <code>pos</code> and
		/// <code>markpos</code> exceeds <code>marklimit</code>.
		/// </remarks>
		/// <seealso cref="mark(int)">mark(int)</seealso>
		/// <seealso cref="pos">pos</seealso>
		protected int markpos;

		/// <summary>
		/// The maximum read ahead allowed after a call to the <code>mark</code>
		/// method before subsequent calls to the <code>reset</code> method fail.
		/// </summary>
		/// <remarks>
		/// The maximum read ahead allowed after a call to the <code>mark</code>
		/// method before subsequent calls to the <code>reset</code> method fail.
		/// Whenever the difference between <code>pos</code> and <code>markpos</code>
		/// exceeds <code>marklimit</code>, then the  mark may be dropped by setting
		/// <code>markpos</code> to -1.
		/// </remarks>
		/// <seealso cref="mark(int)">mark(int)</seealso>
		/// <seealso cref="reset()">reset()</seealso>
		protected int marklimit;

		/// <summary>Array of size 1, used by the read method to read just 1 byte.</summary>
		/// <remarks>Array of size 1, used by the read method to read just 1 byte.</remarks>
		private readonly byte[] single = new byte[1];

		/// <summary>The internal buffer array where the unfiltered data is temporarily stored.
		/// 	</summary>
		/// <remarks>The internal buffer array where the unfiltered data is temporarily stored.
		/// 	</remarks>
		protected byte[] prebuf;

		/// <summary>
		/// The index one greater than the index of the last valid byte in the
		/// unfiltered data buffer.
		/// </summary>
		/// <remarks>
		/// The index one greater than the index of the last valid byte in the
		/// unfiltered data buffer.
		/// This value is always in the range 0 through <code>prebuf.length</code>;
		/// elements <code>prebuf[0]</code> through <code>prebuf[count-1]</code>
		/// contain buffered input data obtained from the underlying input stream.
		/// </remarks>
		protected int precount;

		/// <summary>The current position in the unfiltered data buffer.</summary>
		/// <remarks>
		/// The current position in the unfiltered data buffer. This is the index of
		/// the next character to be read from the <code>prebuf</code> array.
		/// <p>
		/// This value is always in the range 0 through <code>precount</code>.
		/// If it is less than <code>precount</code>, then <code>prebuf[pos]</code> is
		/// the next byte to be supplied as input.
		/// If it is equal to <code>precount</code>, then the next <code>read</code>
		/// or <code>skip</code> operation will require more bytes to be read from the
		/// contained inputstream.
		/// </remarks>
		/// <seealso cref="prebuf">prebuf</seealso>
		protected int prepos;

		/// <summary>Check to make sure that this stream has not been closed</summary>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		protected virtual void checkIfStillOpen()
		{
			if (@in == null)
			{
				throw new System.IO.IOException("Stream closed");
			}
		}

		/// <summary>
		/// Creates a <code>FilteredAudioInputStream</code> and saves its argument,
		/// the input stream <code>in</code>, for later use.
		/// </summary>
		/// <remarks>
		/// Creates a <code>FilteredAudioInputStream</code> and saves its argument,
		/// the input stream <code>in</code>, for later use.
		/// An internal buffer array is created and  stored in <code>buf</code>.
		/// </remarks>
		/// <param name="in">the underlying input stream.</param>
		/// <param name="format">the format of this stream's audio data.</param>
		/// <param name="length">the length in sample frames of the data in this stream.</param>
		/// <exception>
		/// IllegalArgumentException
		/// if size &lt;= 0 or presize &lt;= 0.
		/// </exception>
		public FilteredAudioInputStream(java.io.InputStream @in, javax.sound.sampled.AudioFormat
			 format, long length) : this(@in, format, length, DEFAULT_BUFFER_SIZE)
		{
		}

		/// <summary>
		/// Creates a <code>FilteredAudioInputStream</code> with the specified buffer
		/// size, and saves its argument, the inputstream <code>in</code> for later use.
		/// </summary>
		/// <remarks>
		/// Creates a <code>FilteredAudioInputStream</code> with the specified buffer
		/// size, and saves its argument, the inputstream <code>in</code> for later use.
		/// An internal buffer array of length <code>size</code> is created and stored
		/// in <code>buf</code>.
		/// </remarks>
		/// <param name="in">the underlying input stream.</param>
		/// <param name="format">the format of this stream's audio data.</param>
		/// <param name="length">the length in sample frames of the data in this stream.</param>
		/// <param name="size">the buffer sizes.</param>
		/// <exception>
		/// IllegalArgumentException
		/// if size &lt;= 0.
		/// </exception>
		public FilteredAudioInputStream(java.io.InputStream @in, javax.sound.sampled.AudioFormat
			 format, long length, int size) : this(@in, format, length, size, size)
		{
		}

		/// <summary>
		/// Creates a <code>FilteredAudioInputStream</code> with the specified buffer
		/// size, and saves its argument, the inputstream <code>in</code> for later use.
		/// </summary>
		/// <remarks>
		/// Creates a <code>FilteredAudioInputStream</code> with the specified buffer
		/// size, and saves its argument, the inputstream <code>in</code> for later use.
		/// An internal buffer array of length <code>size</code> is created and stored
		/// in <code>buf</code>.
		/// </remarks>
		/// <param name="in">the underlying input stream.</param>
		/// <param name="format">the format of this stream's audio data.</param>
		/// <param name="length">the length in sample frames of the data in this stream.</param>
		/// <param name="size">the buffer size.</param>
		/// <param name="presize">the prebuffer size.</param>
		/// <exception>
		/// IllegalArgumentException
		/// if size &lt;= 0 or presize &lt;= 0.
		/// </exception>
		public FilteredAudioInputStream(java.io.InputStream @in, javax.sound.sampled.AudioFormat
			 format, long length, int size, int presize) : base(@in, format, length)
		{
			this.@in = @in;
			if ((size <= 0) || (presize <= 0))
			{
				throw new System.ArgumentException("Buffer size <= 0");
			}
			buf = new byte[size];
			count = 0;
			prebuf = new byte[presize];
			precount = 0;
			marklimit = size;
			markpos = -1;
		}

		/// <summary>
		/// Fills the buffer with more data, taking into account shuffling and other
		/// tricks for dealing with marks.
		/// </summary>
		/// <remarks>
		/// Fills the buffer with more data, taking into account shuffling and other
		/// tricks for dealing with marks.
		/// Assumes that it is being called by a synchronized method.
		/// This method also assumes that all data has already been read in,
		/// hence pos &gt; count.
		/// </remarks>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		protected virtual void fill()
		{
			makeSpace();
			while (true)
			{
				int read = @in.read(prebuf, precount, prebuf.Length - precount);
				if (read < 0)
				{
					// inputstream has ended
					// do last stuff here
					break;
				}
				else
				{
					if (read > 0)
					{
						// do stuff here
						precount += read;
						break;
					}
				}
			}
		}

		// read == 0
		// read 0 bytes from underlying stream yet it is not finished.
		/// <summary>Free up some space in the buffers.</summary>
		/// <remarks>Free up some space in the buffers.</remarks>
		protected virtual void makeSpace()
		{
			if (markpos < 0)
			{
				pos = 0;
			}
			else
			{
				if (pos >= buf.Length)
				{
					if (markpos > 0)
					{
						int sz = pos - markpos;
						System.Array.Copy(buf, markpos, buf, 0, sz);
						pos = sz;
						markpos = 0;
					}
					else
					{
						if (buf.Length >= marklimit)
						{
							markpos = -1;
							pos = 0;
						}
						else
						{
							int nsz = pos * 2;
							if (nsz > marklimit)
							{
								nsz = marklimit;
							}
							byte[] nbuf = new byte[nsz];
							System.Array.Copy(buf, 0, nbuf, 0, pos);
							buf = nbuf;
						}
					}
				}
			}
			count = pos;
		}

		/// <summary>
		/// See the general contract of the <code>read</code> method of
		/// <code>InputStream</code>.
		/// </summary>
		/// <remarks>
		/// See the general contract of the <code>read</code> method of
		/// <code>InputStream</code>.
		/// </remarks>
		/// <returns>
		/// the next byte of data, or <code>-1</code> if the end of the
		/// stream is reached.
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <seealso cref="@in">@in</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int read()
		{
			lock (this)
			{
				if (read(single, 0, 1) == -1)
				{
					return (-1);
				}
				else
				{
					return (single[0] & unchecked((int)(0xFF)));
				}
			}
		}

		/// <summary>
		/// Reads bytes from this byte-input stream into the specified byte array,
		/// starting at the given offset.
		/// </summary>
		/// <remarks>
		/// Reads bytes from this byte-input stream into the specified byte array,
		/// starting at the given offset.
		/// <p> This method implements the general contract of the corresponding
		/// <code>
		/// <see cref="java.io.InputStream.read(byte[], int, int)">read</see>
		/// </code> method of
		/// the <code>
		/// <see cref="java.io.InputStream">java.io.InputStream</see>
		/// </code> class.  As an additional
		/// convenience, it attempts to read as many bytes as possible by repeatedly
		/// invoking the <code>read</code> method of the underlying stream.  This
		/// iterated <code>read</code> continues until one of the following
		/// conditions becomes true: <ul>
		/// <li> The specified number of bytes have been read,
		/// <li> The <code>read</code> method of the underlying stream returns
		/// <code>-1</code>, indicating end-of-file, or
		/// <li> The <code>available</code> method of the underlying stream
		/// returns zero, indicating that further input requests would block.
		/// </ul> If the first <code>read</code> on the underlying stream returns
		/// <code>-1</code> to indicate end-of-file then this method returns
		/// <code>-1</code>.  Otherwise this method returns the number of bytes
		/// actually read.
		/// <p> Subclasses of this class are encouraged, but not required, to
		/// attempt to read as many bytes as possible in the same fashion.
		/// </remarks>
		/// <param name="b">destination buffer.</param>
		/// <param name="off">offset at which to start storing bytes.</param>
		/// <param name="len">maximum number of bytes to read.</param>
		/// <returns>
		/// the number of bytes read, or <code>-1</code> if the end of
		/// the stream has been reached.
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override int read(byte[] b, int off, int len)
		{
			lock (this)
			{
				checkIfStillOpen();
				if ((off < 0) || (off > b.Length) || (len < 0) || ((off + len) > b.Length) || ((off
					 + len) < 0))
				{
					throw new System.IndexOutOfRangeException();
				}
				else
				{
					if (len == 0)
					{
						return 0;
					}
				}
				int avail = count - pos;
				if (avail <= 0)
				{
					fill();
					avail = count - pos;
					if (avail <= 0)
					{
						return -1;
					}
				}
				int cnt = (avail < len) ? avail : len;
				System.Array.Copy(buf, pos, b, off, cnt);
				pos += cnt;
				return cnt;
			}
		}

		/// <summary>
		/// See the general contract of the <code>skip</code> method of
		/// <code>InputStream</code>.
		/// </summary>
		/// <remarks>
		/// See the general contract of the <code>skip</code> method of
		/// <code>InputStream</code>.
		/// </remarks>
		/// <param name="n">the number of bytes to be skipped.</param>
		/// <returns>the actual number of bytes skipped.</returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override long skip(long n)
		{
			lock (this)
			{
				checkIfStillOpen();
				// Sanity check
				if (n <= 0)
				{
					return 0;
				}
				// Skip buffered data if there is any
				if (pos < count)
				{
					int avail = count - pos;
					if (avail > n)
					{
						pos += (int)n;
						return n;
					}
					else
					{
						pos = count;
						return avail;
					}
				}
				else
				{
					// Read into buffers and skip
					fill();
					// This is potentially blocking (i.e. the read on the underlying inputStream could be blocking)
					int avail = count - pos;
					if (avail <= 0)
					{
						return 0;
					}
					long skipped = (avail < n) ? avail : n;
					pos += (int)skipped;
					return skipped;
				}
			}
		}

		/// <summary>
		/// Returns the number of bytes that can be read from this inputstream without
		/// blocking.
		/// </summary>
		/// <remarks>
		/// Returns the number of bytes that can be read from this inputstream without
		/// blocking.
		/// <p>
		/// The <code>available</code> method of <code>FilteredAudioInputStream</code>
		/// returns the sum of the the number of bytes remaining to be read in the
		/// buffer (<code>count - pos</code>).
		/// The result of calling the <code>available</code> method of the underlying
		/// inputstream is not used, as this data will have to be filtered, and thus
		/// may not be the same size after processing (although subclasses that do the
		/// filtering should override this method and use the amount of data available
		/// in the underlying inputstream).
		/// </remarks>
		/// <returns>
		/// the number of bytes that can be read from this inputstream without
		/// blocking.
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <seealso cref="@in">@in</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int available()
		{
			lock (this)
			{
				checkIfStillOpen();
				return (count - pos);
			}
		}

		/// <summary>
		/// See the general contract of the <code>mark</code> method of
		/// <code>InputStream</code>.
		/// </summary>
		/// <remarks>
		/// See the general contract of the <code>mark</code> method of
		/// <code>InputStream</code>.
		/// </remarks>
		/// <param name="readlimit">
		/// the maximum limit of bytes that can be read before
		/// the mark position becomes invalid.
		/// </param>
		/// <seealso cref="reset()">reset()</seealso>
		public override void mark(int readlimit)
		{
			lock (this)
			{
				if (readlimit > buf.Length - pos)
				{
					// not enough room
					byte[] newbuf;
					if (readlimit <= buf.Length)
					{
						newbuf = buf;
					}
					else
					{
						// just shift buffer
						newbuf = new byte[readlimit];
					}
					// need a new buffer
					System.Array.Copy(buf, pos, newbuf, 0, count - pos);
					buf = newbuf;
					count -= pos;
					pos = markpos = 0;
				}
				else
				{
					markpos = pos;
				}
				marklimit = readlimit;
			}
		}

		/// <summary>
		/// See the general contract of the <code>reset</code> method of
		/// <code>InputStream</code>.
		/// </summary>
		/// <remarks>
		/// See the general contract of the <code>reset</code> method of
		/// <code>InputStream</code>.
		/// <p>
		/// If <code>markpos</code> is -1 (no mark has been set or the mark has been
		/// invalidated), an <code>IOException</code> is thrown.
		/// Otherwise, <code>pos</code> is set equal to <code>markpos</code>.
		/// </remarks>
		/// <exception>
		/// IOException
		/// if this stream has not been marked or
		/// if the mark has been invalidated.
		/// </exception>
		/// <seealso cref="mark(int)">mark(int)</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override void reset()
		{
			lock (this)
			{
				checkIfStillOpen();
				if (markpos < 0)
				{
					throw new System.IO.IOException("Attempt to reset when no mark is valid");
				}
				pos = markpos;
			}
		}

		/// <summary>
		/// Tests if this input stream supports the <code>mark</code> and
		/// <code>reset</code> methods.
		/// </summary>
		/// <remarks>
		/// Tests if this input stream supports the <code>mark</code> and
		/// <code>reset</code> methods. The <code>markSupported</code> method of
		/// <code>FilteredAudioInputStream</code> returns <code>true</code>.
		/// </remarks>
		/// <returns>
		/// a <code>boolean</code> indicating if this stream type supports
		/// the <code>mark</code> and <code>reset</code> methods.
		/// </returns>
		/// <seealso cref="mark(int)">mark(int)</seealso>
		/// <seealso cref="reset()">reset()</seealso>
		public override bool markSupported()
		{
			return true;
		}

		/// <summary>
		/// Closes this input stream and releases any system resources associated with
		/// the stream.
		/// </summary>
		/// <remarks>
		/// Closes this input stream and releases any system resources associated with
		/// the stream.
		/// </remarks>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void close()
		{
			lock (this)
			{
				if (@in == null)
				{
					return;
				}
				@in.close();
				@in = null;
				buf = null;
				prebuf = null;
			}
		}
	}
}
