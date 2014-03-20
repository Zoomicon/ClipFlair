namespace java.io
{
	/// <summary>
	/// A <code>SequenceInputStream</code> represents
	/// the logical concatenation of other input
	/// streams.
	/// </summary>
	/// <remarks>
	/// A <code>SequenceInputStream</code> represents
	/// the logical concatenation of other input
	/// streams. It starts out with an ordered
	/// collection of input streams and reads from
	/// the first one until end of file is reached,
	/// whereupon it reads from the second one,
	/// and so on, until end of file is reached
	/// on the last of the contained input streams.
	/// </remarks>
	/// <author>Author van Hoff</author>
	/// <version>1.33, 06/07/06</version>
	/// <since>JDK1.0</since>
	public class SequenceInputStream : InputStream
	{
		internal System.Collections.IEnumerator e;

		internal InputStream @in;

		/// <summary>
		/// Initializes a newly created <code>SequenceInputStream</code>
		/// by remembering the argument, which must
		/// be an <code>Enumeration</code>  that produces
		/// objects whose run-time type is <code>InputStream</code>.
		/// </summary>
		/// <remarks>
		/// Initializes a newly created <code>SequenceInputStream</code>
		/// by remembering the argument, which must
		/// be an <code>Enumeration</code>  that produces
		/// objects whose run-time type is <code>InputStream</code>.
		/// The input streams that are  produced by
		/// the enumeration will be read, in order,
		/// to provide the bytes to be read  from this
		/// <code>SequenceInputStream</code>. After
		/// each input stream from the enumeration
		/// is exhausted, it is closed by calling its
		/// <code>close</code> method.
		/// </remarks>
		/// <param name="e">an enumeration of input streams.</param>
		/// <seealso cref="System.Collections.IEnumerator{E}">System.Collections.IEnumerator&lt;E&gt;
		/// 	</seealso>
		public SequenceInputStream(System.Collections.Generic.IEnumerator<InputStream> e)
		{
			this.e = e;
			try
			{
				nextStream();
			}
			catch (System.IO.IOException)
			{
				// This should never happen
				throw new System.Exception("panic");
			}
		}

		/// <summary>
		/// Initializes a newly
		/// created <code>SequenceInputStream</code>
		/// by remembering the two arguments, which
		/// will be read in order, first <code>s1</code>
		/// and then <code>s2</code>, to provide the
		/// bytes to be read from this <code>SequenceInputStream</code>.
		/// </summary>
		/// <remarks>
		/// Initializes a newly
		/// created <code>SequenceInputStream</code>
		/// by remembering the two arguments, which
		/// will be read in order, first <code>s1</code>
		/// and then <code>s2</code>, to provide the
		/// bytes to be read from this <code>SequenceInputStream</code>.
		/// </remarks>
		/// <param name="s1">the first input stream to read.</param>
		/// <param name="s2">the second input stream to read.</param>
		public SequenceInputStream(InputStream s1, InputStream s2)
		{
            System.Collections.Generic.List<InputStream> v = new System.Collections.Generic.List<InputStream>(2);
			v.Add(s1);
			v.Add(s2);
			e = v.GetEnumerator();
			try
			{
				nextStream();
			}
			catch (System.IO.IOException)
			{
				// This should never happen
				throw new System.Exception("panic");
			}
		}

		/// <summary>Continues reading in the next stream if an EOF is reached.</summary>
		/// <remarks>Continues reading in the next stream if an EOF is reached.</remarks>
		/// <exception cref="System.IO.IOException"></exception>
		internal void nextStream()
		{
			if (@in != null)
			{
				@in.close();
			}
			if (e.MoveNext())
			{
				@in = (InputStream)e.Current;
				if (@in == null)
				{
					throw new System.ArgumentNullException();
				}
			}
			else
			{
				@in = null;
			}
		}

		/// <summary>
		/// Returns an estimate of the number of bytes that can be read (or
		/// skipped over) from the current underlying input stream without
		/// blocking by the next invocation of a method for the current
		/// underlying input stream.
		/// </summary>
		/// <remarks>
		/// Returns an estimate of the number of bytes that can be read (or
		/// skipped over) from the current underlying input stream without
		/// blocking by the next invocation of a method for the current
		/// underlying input stream. The next invocation might be
		/// the same thread or another thread.  A single read or skip of this
		/// many bytes will not block, but may read or skip fewer bytes.
		/// <p>
		/// This method simply calls
		/// <code>available</code>
		/// of the current underlying
		/// input stream and returns the result.
		/// </remarks>
		/// <returns>
		/// an estimate of the number of bytes that can be read (or
		/// skipped over) from the current underlying input stream
		/// without blocking or
		/// <code>0</code>
		/// if this input stream
		/// has been closed by invoking its
		/// <see cref="close()">close()</see>
		/// method
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <since>JDK1.1</since>
		/// <exception cref="System.IO.IOException"></exception>
		public override int available()
		{
			if (@in == null)
			{
				return 0;
			}
			// no way to signal EOF from available()
			return @in.available();
		}

		/// <summary>Reads the next byte of data from this input stream.</summary>
		/// <remarks>
		/// Reads the next byte of data from this input stream. The byte is
		/// returned as an <code>int</code> in the range <code>0</code> to
		/// <code>255</code>. If no byte is available because the end of the
		/// stream has been reached, the value <code>-1</code> is returned.
		/// This method blocks until input data is available, the end of the
		/// stream is detected, or an exception is thrown.
		/// <p>
		/// This method
		/// tries to read one character from the current substream. If it
		/// reaches the end of the stream, it calls the <code>close</code>
		/// method of the current substream and begins reading from the next
		/// substream.
		/// </remarks>
		/// <returns>
		/// the next byte of data, or <code>-1</code> if the end of the
		/// stream is reached.
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override int read()
		{
			if (@in == null)
			{
				return -1;
			}
			int c = @in.read();
			if (c == -1)
			{
				nextStream();
				return read();
			}
			return c;
		}

		/// <summary>
		/// Reads up to <code>len</code> bytes of data from this input stream
		/// into an array of bytes.
		/// </summary>
		/// <remarks>
		/// Reads up to <code>len</code> bytes of data from this input stream
		/// into an array of bytes.  If <code>len</code> is not zero, the method
		/// blocks until at least 1 byte of input is available; otherwise, no
		/// bytes are read and <code>0</code> is returned.
		/// <p>
		/// The <code>read</code> method of <code>SequenceInputStream</code>
		/// tries to read the data from the current substream. If it fails to
		/// read any characters because the substream has reached the end of
		/// the stream, it calls the <code>close</code> method of the current
		/// substream and begins reading from the next substream.
		/// </remarks>
		/// <param name="b">the buffer into which the data is read.</param>
		/// <param name="off">
		/// the start offset in array <code>b</code>
		/// at which the data is written.
		/// </param>
		/// <param name="len">the maximum number of bytes read.</param>
		/// <returns>int   the number of bytes read.</returns>
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
		/// <exception cref="System.IO.IOException"></exception>
		public override int read(byte[] b, int off, int len)
		{
			if (@in == null)
			{
				return -1;
			}
			else
			{
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
			}
			int n = @in.read(b, off, len);
			if (n <= 0)
			{
				nextStream();
				return read(b, off, len);
			}
			return n;
		}

		/// <summary>
		/// Closes this input stream and releases any system resources
		/// associated with the stream.
		/// </summary>
		/// <remarks>
		/// Closes this input stream and releases any system resources
		/// associated with the stream.
		/// A closed <code>SequenceInputStream</code>
		/// cannot  perform input operations and cannot
		/// be reopened.
		/// <p>
		/// If this stream was created
		/// from an enumeration, all remaining elements
		/// are requested from the enumeration and closed
		/// before the <code>close</code> method returns.
		/// </remarks>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void close()
		{
			do
			{
				nextStream();
			}
			while (@in != null);
		}
	}
}
