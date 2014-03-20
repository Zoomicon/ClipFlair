namespace java.io
{
	/// <summary>A <tt>Flushable</tt> is a destination of data that can be flushed.</summary>
	/// <remarks>
	/// A <tt>Flushable</tt> is a destination of data that can be flushed.  The
	/// flush method is invoked to write any buffered output to the underlying
	/// stream.
	/// </remarks>
	/// <version>1.2 05/11/17</version>
	/// <since>1.5</since>
	public interface Flushable
	{
		/// <summary>
		/// Flushes this stream by writing any buffered output to the underlying
		/// stream.
		/// </summary>
		/// <remarks>
		/// Flushes this stream by writing any buffered output to the underlying
		/// stream.
		/// </remarks>
		/// <exception cref="System.IO.IOException">If an I/O error occurs</exception>
		void flush();
	}
}
