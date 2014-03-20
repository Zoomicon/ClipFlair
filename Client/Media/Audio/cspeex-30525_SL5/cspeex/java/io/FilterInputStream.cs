namespace java.io
{
	/// <summary>
	/// A <code>FilterInputStream</code> contains
	/// some other input stream, which it uses as
	/// its  basic source of data, possibly transforming
	/// the data along the way or providing  additional
	/// functionality.
	/// </summary>
	/// <remarks>
	/// A <code>FilterInputStream</code> contains
	/// some other input stream, which it uses as
	/// its  basic source of data, possibly transforming
	/// the data along the way or providing  additional
	/// functionality. The class <code>FilterInputStream</code>
	/// itself simply overrides all  methods of
	/// <code>InputStream</code> with versions that
	/// pass all requests to the contained  input
	/// stream. Subclasses of <code>FilterInputStream</code>
	/// may further override some of  these methods
	/// and may also provide additional methods
	/// and fields.
	/// </remarks>
	/// <author>Jonathan Payne</author>
	/// <version>1.33, 04/07/06</version>
	/// <since>JDK1.0</since>
	public class FilterInputStream : InputStream
	{
		/// <summary>The input stream to be filtered.</summary>
		/// <remarks>The input stream to be filtered.</remarks>
		protected InputStream @in;

		/// <summary>
		/// Creates a <code>FilterInputStream</code>
		/// by assigning the  argument <code>in</code>
		/// to the field <code>this.in</code> so as
		/// to remember it for later use.
		/// </summary>
		/// <remarks>
		/// Creates a <code>FilterInputStream</code>
		/// by assigning the  argument <code>in</code>
		/// to the field <code>this.in</code> so as
		/// to remember it for later use.
		/// </remarks>
		/// <param name="in">
		/// the underlying input stream, or <code>null</code> if
		/// this instance is to be created without an underlying stream.
		/// </param>
		protected FilterInputStream(InputStream @in)
		{
			this.@in = @in;
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
		/// <p>
		/// This method
		/// simply performs <code>in.read()</code> and returns the result.
		/// </remarks>
		/// <returns>
		/// the next byte of data, or <code>-1</code> if the end of the
		/// stream is reached.
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <seealso cref="java.io.FilterInputStream#in">java.io.FilterInputStream#in</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int read()
		{
			return @in.read();
		}

		/// <summary>
		/// Reads up to <code>byte.length</code> bytes of data from this
		/// input stream into an array of bytes.
		/// </summary>
		/// <remarks>
		/// Reads up to <code>byte.length</code> bytes of data from this
		/// input stream into an array of bytes. This method blocks until some
		/// input is available.
		/// <p>
		/// This method simply performs the call
		/// <code>read(b, 0, b.length)</code> and returns
		/// the  result. It is important that it does
		/// <i>not</i> do <code>in.read(b)</code> instead;
		/// certain subclasses of  <code>FilterInputStream</code>
		/// depend on the implementation strategy actually
		/// used.
		/// </remarks>
		/// <param name="b">the buffer into which the data is read.</param>
		/// <returns>
		/// the total number of bytes read into the buffer, or
		/// <code>-1</code> if there is no more data because the end of
		/// the stream has been reached.
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <seealso cref="java.io.FilterInputStream.read(byte[], int, int)">java.io.FilterInputStream.read(byte[], int, int)
		/// 	</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int read(byte[] b)
		{
			return read(b, 0, b.Length);
		}

		/// <summary>
		/// Reads up to <code>len</code> bytes of data from this input stream
		/// into an array of bytes.
		/// </summary>
		/// <remarks>
		/// Reads up to <code>len</code> bytes of data from this input stream
		/// into an array of bytes. If <code>len</code> is not zero, the method
		/// blocks until some input is available; otherwise, no
		/// bytes are read and <code>0</code> is returned.
		/// <p>
		/// This method simply performs <code>in.read(b, off, len)</code>
		/// and returns the result.
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
		/// if an I/O error occurs.
		/// </exception>
		/// <seealso cref="java.io.FilterInputStream#in">java.io.FilterInputStream#in</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int read(byte[] b, int off, int len)
		{
			return @in.read(b, off, len);
		}

		/// <summary>
		/// <inheritDoc></inheritDoc>
		/// 
		/// <p>
		/// This method simply performs <code>in.skip(n)</code>.
		/// </summary>
		/// <exception cref="System.IO.IOException"></exception>
		public override long skip(long n)
		{
			return @in.skip(n);
		}

		/// <summary>
		/// Returns an estimate of the number of bytes that can be read (or
		/// skipped over) from this input stream without blocking by the next
		/// caller of a method for this input stream.
		/// </summary>
		/// <remarks>
		/// Returns an estimate of the number of bytes that can be read (or
		/// skipped over) from this input stream without blocking by the next
		/// caller of a method for this input stream. The next caller might be
		/// the same thread or another thread.  A single read or skip of this
		/// many bytes will not block, but may read or skip fewer bytes.
		/// <p>
		/// This method returns the result of
		/// <see cref="@in">in</see>
		/// .available().
		/// </remarks>
		/// <returns>
		/// an estimate of the number of bytes that can be read (or skipped
		/// over) from this input stream without blocking.
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override int available()
		{
			return @in.available();
		}

		/// <summary>
		/// Closes this input stream and releases any system resources
		/// associated with the stream.
		/// </summary>
		/// <remarks>
		/// Closes this input stream and releases any system resources
		/// associated with the stream.
		/// This
		/// method simply performs <code>in.close()</code>.
		/// </remarks>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <seealso cref="java.io.FilterInputStream#in">java.io.FilterInputStream#in</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override void close()
		{
			@in.close();
		}

		/// <summary>Marks the current position in this input stream.</summary>
		/// <remarks>
		/// Marks the current position in this input stream. A subsequent
		/// call to the <code>reset</code> method repositions this stream at
		/// the last marked position so that subsequent reads re-read the same bytes.
		/// <p>
		/// The <code>readlimit</code> argument tells this input stream to
		/// allow that many bytes to be read before the mark position gets
		/// invalidated.
		/// <p>
		/// This method simply performs <code>in.mark(readlimit)</code>.
		/// </remarks>
		/// <param name="readlimit">
		/// the maximum limit of bytes that can be read before
		/// the mark position becomes invalid.
		/// </param>
		/// <seealso cref="java.io.FilterInputStream#in">java.io.FilterInputStream#in</seealso>
		/// <seealso cref="java.io.FilterInputStream.reset()">java.io.FilterInputStream.reset()
		/// 	</seealso>
		public override void mark(int readlimit)
		{
			lock (this)
			{
				@in.mark(readlimit);
			}
		}

		/// <summary>
		/// Repositions this stream to the position at the time the
		/// <code>mark</code> method was last called on this input stream.
		/// </summary>
		/// <remarks>
		/// Repositions this stream to the position at the time the
		/// <code>mark</code> method was last called on this input stream.
		/// <p>
		/// This method
		/// simply performs <code>in.reset()</code>.
		/// <p>
		/// Stream marks are intended to be used in
		/// situations where you need to read ahead a little to see what's in
		/// the stream. Often this is most easily done by invoking some
		/// general parser. If the stream is of the type handled by the
		/// parse, it just chugs along happily. If the stream is not of
		/// that type, the parser should toss an exception when it fails.
		/// If this happens within readlimit bytes, it allows the outer
		/// code to reset the stream and try another parser.
		/// </remarks>
		/// <exception>
		/// IOException
		/// if the stream has not been marked or if the
		/// mark has been invalidated.
		/// </exception>
		/// <seealso cref="java.io.FilterInputStream#in">java.io.FilterInputStream#in</seealso>
		/// <seealso cref="java.io.FilterInputStream.mark(int)">java.io.FilterInputStream.mark(int)
		/// 	</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override void reset()
		{
			lock (this)
			{
				@in.reset();
			}
		}

		/// <summary>
		/// Tests if this input stream supports the <code>mark</code>
		/// and <code>reset</code> methods.
		/// </summary>
		/// <remarks>
		/// Tests if this input stream supports the <code>mark</code>
		/// and <code>reset</code> methods.
		/// This method
		/// simply performs <code>in.markSupported()</code>.
		/// </remarks>
		/// <returns>
		/// <code>true</code> if this stream type supports the
		/// <code>mark</code> and <code>reset</code> method;
		/// <code>false</code> otherwise.
		/// </returns>
		/// <seealso cref="java.io.FilterInputStream#in">java.io.FilterInputStream#in</seealso>
		/// <seealso cref="java.io.InputStream.mark(int)">java.io.InputStream.mark(int)</seealso>
		/// <seealso cref="java.io.InputStream.reset()">java.io.InputStream.reset()</seealso>
		public override bool markSupported()
		{
			return @in.markSupported();
		}
	}
}
