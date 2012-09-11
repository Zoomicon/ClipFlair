namespace java.io
{
	/// <summary>
	/// A <code>PushbackInputStream</code> adds
	/// functionality to another input stream, namely
	/// the  ability to "push back" or "unread"
	/// one byte.
	/// </summary>
	/// <remarks>
	/// A <code>PushbackInputStream</code> adds
	/// functionality to another input stream, namely
	/// the  ability to "push back" or "unread"
	/// one byte. This is useful in situations where
	/// it is  convenient for a fragment of code
	/// to read an indefinite number of data bytes
	/// that  are delimited by a particular byte
	/// value; after reading the terminating byte,
	/// the  code fragment can "unread" it, so that
	/// the next read operation on the input stream
	/// will reread the byte that was pushed back.
	/// For example, bytes representing the  characters
	/// constituting an identifier might be terminated
	/// by a byte representing an  operator character;
	/// a method whose job is to read just an identifier
	/// can read until it  sees the operator and
	/// then push the operator back to be re-read.
	/// </remarks>
	/// <author>David Connelly</author>
	/// <author>Jonathan Payne</author>
	/// <version>1.43, 06/19/06</version>
	/// <since>JDK1.0</since>
	public class PushbackInputStream : FilterInputStream
	{
		/// <summary>The pushback buffer.</summary>
		/// <remarks>The pushback buffer.</remarks>
		/// <since>JDK1.1</since>
		protected byte[] buf;

		/// <summary>
		/// The position within the pushback buffer from which the next byte will
		/// be read.
		/// </summary>
		/// <remarks>
		/// The position within the pushback buffer from which the next byte will
		/// be read.  When the buffer is empty, <code>pos</code> is equal to
		/// <code>buf.length</code>; when the buffer is full, <code>pos</code> is
		/// equal to zero.
		/// </remarks>
		/// <since>JDK1.1</since>
		protected int pos;

		/// <summary>Check to make sure that this stream has not been closed</summary>
		/// <exception cref="System.IO.IOException"></exception>
		private void ensureOpen()
		{
			if (@in == null)
			{
				throw new System.IO.IOException("Stream closed");
			}
		}

		/// <summary>
		/// Creates a <code>PushbackInputStream</code>
		/// with a pushback buffer of the specified <code>size</code>,
		/// and saves its  argument, the input stream
		/// <code>in</code>, for later use.
		/// </summary>
		/// <remarks>
		/// Creates a <code>PushbackInputStream</code>
		/// with a pushback buffer of the specified <code>size</code>,
		/// and saves its  argument, the input stream
		/// <code>in</code>, for later use. Initially,
		/// there is no pushed-back byte  (the field
		/// <code>pushBack</code> is initialized to
		/// <code>-1</code>).
		/// </remarks>
		/// <param name="in">the input stream from which bytes will be read.</param>
		/// <param name="size">the size of the pushback buffer.</param>
		/// <exception>
		/// IllegalArgumentException
		/// if size is &lt;= 0
		/// </exception>
		/// <since>JDK1.1</since>
		public PushbackInputStream(InputStream @in, int size) : base(@in)
		{
			if (size <= 0)
			{
				throw new System.ArgumentException("size <= 0");
			}
			this.buf = new byte[size];
			this.pos = size;
		}

		/// <summary>
		/// Creates a <code>PushbackInputStream</code>
		/// and saves its  argument, the input stream
		/// <code>in</code>, for later use.
		/// </summary>
		/// <remarks>
		/// Creates a <code>PushbackInputStream</code>
		/// and saves its  argument, the input stream
		/// <code>in</code>, for later use. Initially,
		/// there is no pushed-back byte  (the field
		/// <code>pushBack</code> is initialized to
		/// <code>-1</code>).
		/// </remarks>
		/// <param name="in">the input stream from which bytes will be read.</param>
		public PushbackInputStream(InputStream @in) : this(@in, 1)
		{
		}

		/// <summary>Reads the next byte of data from this input stream.</summary>
		/// <remarks>
		/// Reads the next byte of data from this input stream. The value
		/// byte is returned as an <code>int</code> in the range
		/// <code>0</code> to <code>255</code>. If no byte is available
		/// because the end of the stream has been reached, the value
		/// <code>-1</code> is returned. This method blocks until input data
		/// is available, the end of the stream is detected, or an exception
		/// is thrown.
		/// <p> This method returns the most recently pushed-back byte, if there is
		/// one, and otherwise calls the <code>read</code> method of its underlying
		/// input stream and returns whatever value that method returns.
		/// </remarks>
		/// <returns>
		/// the next byte of data, or <code>-1</code> if the end of the
		/// stream has been reached.
		/// </returns>
		/// <exception>
		/// IOException
		/// if this input stream has been closed by
		/// invoking its
		/// <see cref="close()">close()</see>
		/// method,
		/// or an I/O error occurs.
		/// </exception>
		/// <seealso cref="java.io.InputStream.read()">java.io.InputStream.read()</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int read()
		{
			ensureOpen();
			if (pos < buf.Length)
			{
				return buf[pos++] & unchecked((int)(0xff));
			}
			return base.read();
		}

		/// <summary>
		/// Reads up to <code>len</code> bytes of data from this input stream into
		/// an array of bytes.
		/// </summary>
		/// <remarks>
		/// Reads up to <code>len</code> bytes of data from this input stream into
		/// an array of bytes.  This method first reads any pushed-back bytes; after
		/// that, if fewer than <code>len</code> bytes have been read then it
		/// reads from the underlying input stream. If <code>len</code> is not zero, the method
		/// blocks until at least 1 byte of input is available; otherwise, no
		/// bytes are read and <code>0</code> is returned.
		/// </remarks>
		/// <param name="b">the buffer into which the data is read.</param>
		/// <param name="off">the start offset in the destination array <code>b</code></param>
		/// <param name="len">the maximum number of bytes read.</param>
		/// <returns>
		/// the total number of bytes read into the buffer, or
		/// <code>-1</code> if there is no more data because the end of
		/// the stream has been reached.
		/// </returns>
		/// <exception>
		/// NullPointerException
		/// If <code>b</code> is <code>null</code>.
		/// </exception>
		/// <exception>
		/// IndexOutOfBoundsException
		/// If <code>off</code> is negative,
		/// <code>len</code> is negative, or <code>len</code> is greater than
		/// <code>b.length - off</code>
		/// </exception>
		/// <exception>
		/// IOException
		/// if this input stream has been closed by
		/// invoking its
		/// <see cref="close()">close()</see>
		/// method,
		/// or an I/O error occurs.
		/// </exception>
		/// <seealso cref="java.io.InputStream.read(byte[], int, int)">java.io.InputStream.read(byte[], int, int)
		/// 	</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int read(byte[] b, int off, int len)
		{
			ensureOpen();
			if (b == null)
			{
				throw new System.ArgumentNullException();
			}
			else
			{
				if (off < 0 || len < 0 || len > b.Length - off)
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
			}
			int avail = buf.Length - pos;
			if (avail > 0)
			{
				if (len < avail)
				{
					avail = len;
				}
				System.Array.Copy(buf, pos, b, off, avail);
				pos += avail;
				off += avail;
				len -= avail;
			}
			if (len > 0)
			{
				len = base.read(b, off, len);
				if (len == -1)
				{
					return avail == 0 ? -1 : avail;
				}
				return avail + len;
			}
			return avail;
		}

		/// <summary>Pushes back a byte by copying it to the front of the pushback buffer.</summary>
		/// <remarks>
		/// Pushes back a byte by copying it to the front of the pushback buffer.
		/// After this method returns, the next byte to be read will have the value
		/// <code>(byte)b</code>.
		/// </remarks>
		/// <param name="b">
		/// the <code>int</code> value whose low-order
		/// byte is to be pushed back.
		/// </param>
		/// <exception>
		/// IOException
		/// If there is not enough room in the pushback
		/// buffer for the byte, or this input stream has been closed by
		/// invoking its
		/// <see cref="close()">close()</see>
		/// method.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public virtual void unread(int b)
		{
			ensureOpen();
			if (pos == 0)
			{
				throw new System.IO.IOException("Push back buffer is full");
			}
			buf[--pos] = (byte)b;
		}

		/// <summary>
		/// Pushes back a portion of an array of bytes by copying it to the front
		/// of the pushback buffer.
		/// </summary>
		/// <remarks>
		/// Pushes back a portion of an array of bytes by copying it to the front
		/// of the pushback buffer.  After this method returns, the next byte to be
		/// read will have the value <code>b[off]</code>, the byte after that will
		/// have the value <code>b[off+1]</code>, and so forth.
		/// </remarks>
		/// <param name="b">the byte array to push back.</param>
		/// <param name="off">the start offset of the data.</param>
		/// <param name="len">the number of bytes to push back.</param>
		/// <exception>
		/// IOException
		/// If there is not enough room in the pushback
		/// buffer for the specified number of bytes,
		/// or this input stream has been closed by
		/// invoking its
		/// <see cref="close()">close()</see>
		/// method.
		/// </exception>
		/// <since>JDK1.1</since>
		/// <exception cref="System.IO.IOException"></exception>
		public virtual void unread(byte[] b, int off, int len)
		{
			ensureOpen();
			if (len > pos)
			{
				throw new System.IO.IOException("Push back buffer is full");
			}
			pos -= len;
			System.Array.Copy(b, off, buf, pos, len);
		}

		/// <summary>
		/// Pushes back an array of bytes by copying it to the front of the
		/// pushback buffer.
		/// </summary>
		/// <remarks>
		/// Pushes back an array of bytes by copying it to the front of the
		/// pushback buffer.  After this method returns, the next byte to be read
		/// will have the value <code>b[0]</code>, the byte after that will have the
		/// value <code>b[1]</code>, and so forth.
		/// </remarks>
		/// <param name="b">the byte array to push back</param>
		/// <exception>
		/// IOException
		/// If there is not enough room in the pushback
		/// buffer for the specified number of bytes,
		/// or this input stream has been closed by
		/// invoking its
		/// <see cref="close()">close()</see>
		/// method.
		/// </exception>
		/// <since>JDK1.1</since>
		/// <exception cref="System.IO.IOException"></exception>
		public virtual void unread(byte[] b)
		{
			unread(b, 0, b.Length);
		}

		/// <summary>
		/// Returns an estimate of the number of bytes that can be read (or
		/// skipped over) from this input stream without blocking by the next
		/// invocation of a method for this input stream.
		/// </summary>
		/// <remarks>
		/// Returns an estimate of the number of bytes that can be read (or
		/// skipped over) from this input stream without blocking by the next
		/// invocation of a method for this input stream. The next invocation might be
		/// the same thread or another thread.  A single read or skip of this
		/// many bytes will not block, but may read or skip fewer bytes.
		/// <p> The method returns the sum of the number of bytes that have been
		/// pushed back and the value returned by
		/// <see cref="java.io.FilterInputStream.available()">available</see>
		/// .
		/// </remarks>
		/// <returns>
		/// the number of bytes that can be read (or skipped over) from
		/// the input stream without blocking.
		/// </returns>
		/// <exception>
		/// IOException
		/// if this input stream has been closed by
		/// invoking its
		/// <see cref="close()">close()</see>
		/// method,
		/// or an I/O error occurs.
		/// </exception>
		/// <seealso cref="java.io.FilterInputStream#in">java.io.FilterInputStream#in</seealso>
		/// <seealso cref="java.io.InputStream.available()">java.io.InputStream.available()</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int available()
		{
			ensureOpen();
			return (buf.Length - pos) + base.available();
		}

		/// <summary>
		/// Skips over and discards <code>n</code> bytes of data from this
		/// input stream.
		/// </summary>
		/// <remarks>
		/// Skips over and discards <code>n</code> bytes of data from this
		/// input stream. The <code>skip</code> method may, for a variety of
		/// reasons, end up skipping over some smaller number of bytes,
		/// possibly zero.  If <code>n</code> is negative, no bytes are skipped.
		/// <p> The <code>skip</code> method of <code>PushbackInputStream</code>
		/// first skips over the bytes in the pushback buffer, if any.  It then
		/// calls the <code>skip</code> method of the underlying input stream if
		/// more bytes need to be skipped.  The actual number of bytes skipped
		/// is returned.
		/// </remarks>
		/// <param name="n">
		/// 
		/// <inheritDoc></inheritDoc>
		/// 
		/// </param>
		/// <returns>
		/// 
		/// <inheritDoc></inheritDoc>
		/// </returns>
		/// <exception>
		/// IOException
		/// if the stream does not support seek,
		/// or the stream has been closed by
		/// invoking its
		/// <see cref="close()">close()</see>
		/// method,
		/// or an I/O error occurs.
		/// </exception>
		/// <seealso cref="java.io.FilterInputStream#in">java.io.FilterInputStream#in</seealso>
		/// <seealso cref="java.io.InputStream.skip(long)">java.io.InputStream.skip(long)</seealso>
		/// <since>1.2</since>
		/// <exception cref="System.IO.IOException"></exception>
		public override long skip(long n)
		{
			ensureOpen();
			if (n <= 0)
			{
				return 0;
			}
			long pskip = buf.Length - pos;
			if (pskip > 0)
			{
				if (n < pskip)
				{
					pskip = n;
				}
				pos += (int)pskip;
				n -= pskip;
			}
			if (n > 0)
			{
				pskip += base.skip(n);
			}
			return pskip;
		}

		/// <summary>
		/// Tests if this input stream supports the <code>mark</code> and
		/// <code>reset</code> methods, which it does not.
		/// </summary>
		/// <remarks>
		/// Tests if this input stream supports the <code>mark</code> and
		/// <code>reset</code> methods, which it does not.
		/// </remarks>
		/// <returns>
		/// <code>false</code>, since this class does not support the
		/// <code>mark</code> and <code>reset</code> methods.
		/// </returns>
		/// <seealso cref="java.io.InputStream.mark(int)">java.io.InputStream.mark(int)</seealso>
		/// <seealso cref="java.io.InputStream.reset()">java.io.InputStream.reset()</seealso>
		public override bool markSupported()
		{
			return false;
		}

		/// <summary>Marks the current position in this input stream.</summary>
		/// <remarks>
		/// Marks the current position in this input stream.
		/// <p> The <code>mark</code> method of <code>PushbackInputStream</code>
		/// does nothing.
		/// </remarks>
		/// <param name="readlimit">
		/// the maximum limit of bytes that can be read before
		/// the mark position becomes invalid.
		/// </param>
		/// <seealso cref="java.io.InputStream.reset()">java.io.InputStream.reset()</seealso>
		public override void mark(int readlimit)
		{
			lock (this)
			{
			}
		}

		/// <summary>
		/// Repositions this stream to the position at the time the
		/// <code>mark</code> method was last called on this input stream.
		/// </summary>
		/// <remarks>
		/// Repositions this stream to the position at the time the
		/// <code>mark</code> method was last called on this input stream.
		/// <p> The method <code>reset</code> for class
		/// <code>PushbackInputStream</code> does nothing except throw an
		/// <code>IOException</code>.
		/// </remarks>
		/// <exception>
		/// IOException
		/// if this method is invoked.
		/// </exception>
		/// <seealso cref="java.io.InputStream.mark(int)">java.io.InputStream.mark(int)</seealso>
		/// <seealso cref="System.IO.IOException">System.IO.IOException</seealso>
		public override void reset()
		{
			lock (this)
			{
				throw new System.IO.IOException("mark/reset not supported");
			}
		}

		/// <summary>
		/// Closes this input stream and releases any system resources
		/// associated with the stream.
		/// </summary>
		/// <remarks>
		/// Closes this input stream and releases any system resources
		/// associated with the stream.
		/// Once the stream has been closed, further read(), unread(),
		/// available(), reset(), or skip() invocations will throw an IOException.
		/// Closing a previously closed stream has no effect.
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
			}
		}
	}
}
