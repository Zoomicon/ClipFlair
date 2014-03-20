namespace java.io
{
	/// <summary>A <tt>Closeable</tt> is a source or destination of data that can be closed.
	/// 	</summary>
	/// <remarks>
	/// A <tt>Closeable</tt> is a source or destination of data that can be closed.
	/// The close method is invoked to release resources that the object is
	/// holding (such as open files).
	/// </remarks>
	/// <version>1.5 05/11/17</version>
	/// <since>1.5</since>
	public interface Closeable
	{
		/// <summary>
		/// Closes this stream and releases any system resources associated
		/// with it.
		/// </summary>
		/// <remarks>
		/// Closes this stream and releases any system resources associated
		/// with it. If the stream is already closed then invoking this
		/// method has no effect.
		/// </remarks>
		/// <exception cref="System.IO.IOException">if an I/O error occurs</exception>
		void close();
	}
}
