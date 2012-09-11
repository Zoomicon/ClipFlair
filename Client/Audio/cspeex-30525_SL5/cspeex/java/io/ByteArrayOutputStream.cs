namespace java.io
{
	/// <summary>
	/// This class implements an output stream in which the data is
	/// written into a byte array.
	/// </summary>
	/// <remarks>
	/// This class implements an output stream in which the data is
	/// written into a byte array. The buffer automatically grows as data
	/// is written to it.
	/// The data can be retrieved using <code>toByteArray()</code> and
	/// <code>toString()</code>.
	/// <p>
	/// Closing a <tt>ByteArrayOutputStream</tt> has no effect. The methods in
	/// this class can be called after the stream has been closed without
	/// generating an <tt>IOException</tt>.
	/// </remarks>
	/// <author>Arthur van Hoff</author>
	/// <version>1.53, 06/07/06</version>
	/// <since>JDK1.0</since>
	public class ByteArrayOutputStream : OutputStream
	{
		/// <summary>The buffer where data is stored.</summary>
		/// <remarks>The buffer where data is stored.</remarks>
		protected byte[] buf;

		/// <summary>The number of valid bytes in the buffer.</summary>
		/// <remarks>The number of valid bytes in the buffer.</remarks>
		protected int count;

		/// <summary>Creates a new byte array output stream.</summary>
		/// <remarks>
		/// Creates a new byte array output stream. The buffer capacity is
		/// initially 32 bytes, though its size increases if necessary.
		/// </remarks>
		public ByteArrayOutputStream() : this(32)
		{
		}

		/// <summary>
		/// Creates a new byte array output stream, with a buffer capacity of
		/// the specified size, in bytes.
		/// </summary>
		/// <remarks>
		/// Creates a new byte array output stream, with a buffer capacity of
		/// the specified size, in bytes.
		/// </remarks>
		/// <param name="size">the initial size.</param>
		/// <exception>
		/// IllegalArgumentException
		/// if size is negative.
		/// </exception>
		public ByteArrayOutputStream(int size)
		{
			if (size < 0)
			{
				throw new System.ArgumentException("Negative initial size: " + size);
			}
			buf = new byte[size];
		}

		/// <summary>Writes the specified byte to this byte array output stream.</summary>
		/// <remarks>Writes the specified byte to this byte array output stream.</remarks>
		/// <param name="b">the byte to be written.</param>
		public override void write(int b)
		{
			lock (this)
			{
				int newcount = count + 1;
				if (newcount > buf.Length)
				{
                    byte[] temp = new byte[System.Math.Max(buf.Length << 1, newcount)];
                    System.Array.Copy(buf, temp, buf.Length);
                    buf = temp;
				}
				buf[count] = (byte)b;
				count = newcount;
			}
		}

		/// <summary>
		/// Writes <code>len</code> bytes from the specified byte array
		/// starting at offset <code>off</code> to this byte array output stream.
		/// </summary>
		/// <remarks>
		/// Writes <code>len</code> bytes from the specified byte array
		/// starting at offset <code>off</code> to this byte array output stream.
		/// </remarks>
		/// <param name="b">the data.</param>
		/// <param name="off">the start offset in the data.</param>
		/// <param name="len">the number of bytes to write.</param>
		public override void write(byte[] b, int off, int len)
		{
			lock (this)
			{
				if ((off < 0) || (off > b.Length) || (len < 0) || ((off + len) > b.Length) || ((off
					 + len) < 0))
				{
					throw new System.IndexOutOfRangeException();
				}
				else
				{
					if (len == 0)
					{
						return;
					}
				}
				int newcount = count + len;
				if (newcount > buf.Length)
				{
                    byte[] temp = new byte[System.Math.Max(buf.Length << 1, newcount)];
                    System.Array.Copy(buf, temp, buf.Length);
                    buf = temp;
				}
				System.Array.Copy(b, off, buf, count, len);
				count = newcount;
			}
		}

		/// <summary>
		/// Writes the complete contents of this byte array output stream to
		/// the specified output stream argument, as if by calling the output
		/// stream's write method using <code>out.write(buf, 0, count)</code>.
		/// </summary>
		/// <remarks>
		/// Writes the complete contents of this byte array output stream to
		/// the specified output stream argument, as if by calling the output
		/// stream's write method using <code>out.write(buf, 0, count)</code>.
		/// </remarks>
		/// <param name="out">the output stream to which to write the data.</param>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public virtual void writeTo(OutputStream @out)
		{
			lock (this)
			{
				@out.write(buf, 0, count);
			}
		}

		/// <summary>
		/// Resets the <code>count</code> field of this byte array output
		/// stream to zero, so that all currently accumulated output in the
		/// output stream is discarded.
		/// </summary>
		/// <remarks>
		/// Resets the <code>count</code> field of this byte array output
		/// stream to zero, so that all currently accumulated output in the
		/// output stream is discarded. The output stream can be used again,
		/// reusing the already allocated buffer space.
		/// </remarks>
		/// <seealso cref="java.io.ByteArrayInputStream#count">java.io.ByteArrayInputStream#count
		/// 	</seealso>
		public virtual void reset()
		{
			lock (this)
			{
				count = 0;
			}
		}

		/// <summary>Creates a newly allocated byte array.</summary>
		/// <remarks>
		/// Creates a newly allocated byte array. Its size is the current
		/// size of this output stream and the valid contents of the buffer
		/// have been copied into it.
		/// </remarks>
		/// <returns>the current contents of this output stream, as a byte array.</returns>
		/// <seealso cref="java.io.ByteArrayOutputStream.size()">java.io.ByteArrayOutputStream.size()
		/// 	</seealso>
		public virtual byte[] toByteArray()
		{
			lock (this)
			{
                byte[] temp = new byte[count];
                System.Array.Copy(buf, temp, count);
                return temp;
			}
		}

		/// <summary>Returns the current size of the buffer.</summary>
		/// <remarks>Returns the current size of the buffer.</remarks>
		/// <returns>
		/// the value of the <code>count</code> field, which is the number
		/// of valid bytes in this output stream.
		/// </returns>
		/// <seealso cref="java.io.ByteArrayOutputStream#count">java.io.ByteArrayOutputStream#count
		/// 	</seealso>
		public virtual int size()
		{
			lock (this)
			{
				return count;
			}
		}

		/// <summary>
		/// Converts the buffer's contents into a string decoding bytes using the
		/// platform's default character set.
		/// </summary>
		/// <remarks>
		/// Converts the buffer's contents into a string decoding bytes using the
		/// platform's default character set. The length of the new <tt>String</tt>
		/// is a function of the character set, and hence may not be equal to the
		/// size of the buffer.
		/// <p> This method always replaces malformed-input and unmappable-character
		/// sequences with the default replacement string for the platform's
		/// default character set. The
		/// <linkplain>java.nio.charset.CharsetDecoder</linkplain>
		/// class should be used when more control over the decoding process is
		/// required.
		/// </remarks>
		/// <returns>String decoded from the buffer's contents.</returns>
		/// <since>JDK1.1</since>
		public override string ToString()
		{
			lock (this)
			{
				return cspeex.StringUtil.getStringForBytes(buf, 0, count);
			}
		}

		/// <summary>
		/// Converts the buffer's contents into a string by decoding the bytes using
		/// the specified
		/// <see cref="java.nio.charset.Charset">charsetName</see>
		/// . The length of
		/// the new <tt>String</tt> is a function of the charset, and hence may not be
		/// equal to the length of the byte array.
		/// <p> This method always replaces malformed-input and unmappable-character
		/// sequences with this charset's default replacement string. The
		/// <see cref="java.nio.charset.CharsetDecoder">java.nio.charset.CharsetDecoder</see>
		/// class should be used when more control
		/// over the decoding process is required.
		/// </summary>
		/// <param name="charsetName">
		/// the name of a supported
		/// <linkplain>
		/// java.nio.charset.Charset
		/// </code>charset<code>
		/// </linkplain>
		/// </param>
		/// <returns>String decoded from the buffer's contents.</returns>
		/// <exception>
		/// UnsupportedEncodingException
		/// If the named charset is not supported
		/// </exception>
		/// <exception cref="java.io.UnsupportedEncodingException"></exception>
		/// <since>JDK1.1</since>
		public virtual string toString(string charsetName)
		{
			lock (this)
			{
				return System.Text.Encoding.GetEncoding(charsetName).GetString(buf, 0, count);
			}
		}

		/// <summary>Creates a newly allocated string.</summary>
		/// <remarks>
		/// Creates a newly allocated string. Its size is the current size of
		/// the output stream and the valid contents of the buffer have been
		/// copied into it. Each character <i>c</i> in the resulting string is
		/// constructed from the corresponding element <i>b</i> in the byte
		/// array such that:
		/// <blockquote><pre>
		/// c == (char)(((hibyte &amp; 0xff) &lt;&lt; 8) | (b &amp; 0xff))
		/// </pre></blockquote>
		/// </remarks>
		/// <param name="hibyte">the high byte of each resulting Unicode character.</param>
		/// <returns>the current contents of the output stream, as a string.</returns>
		/// <seealso cref="java.io.ByteArrayOutputStream.size()">java.io.ByteArrayOutputStream.size()
		/// 	</seealso>
		/// <seealso cref="java.io.ByteArrayOutputStream.toString(string)">java.io.ByteArrayOutputStream.toString(string)
		/// 	</seealso>
		/// <seealso cref="java.io.ByteArrayOutputStream.ToString()">java.io.ByteArrayOutputStream.ToString()
		/// 	</seealso>
		[System.ObsoleteAttribute(@"This method does not properly convert bytes into characters. As of JDK&nbsp;1.1, the preferred way to do this is via the <code>toString(String enc)</code> method, which takes an encoding-name argument, or the <code>toString()</code> method, which uses the platform's default character encoding."
			)]
		public virtual string toString(int hibyte)
		{
			lock (this)
			{
				return cspeex.StringUtil.getStringForBytes(buf, 0, count);
			}
		}

		/// <summary>Closing a <tt>ByteArrayOutputStream</tt> has no effect.</summary>
		/// <remarks>
		/// Closing a <tt>ByteArrayOutputStream</tt> has no effect. The methods in
		/// this class can be called after the stream has been closed without
		/// generating an <tt>IOException</tt>.
		/// <p>
		/// </remarks>
		/// <exception cref="System.IO.IOException"></exception>
		public override void close()
		{
		}
	}
}
