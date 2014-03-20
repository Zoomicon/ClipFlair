namespace javax.sound.sampled.spi
{
	/// <summary>Provider for audio file reading services.</summary>
	/// <remarks>
	/// Provider for audio file reading services.  Classes providing concrete
	/// implementations can parse the format information from one or more types of
	/// audio file, and can produce audio input streams from files of these types.
	/// </remarks>
	/// <author>Kara Kytle</author>
	/// <version>1.16 05/11/17</version>
	/// <since>1.3</since>
	public abstract class AudioFileReader
	{
		/// <summary>Obtains the audio file format of the input stream provided.</summary>
		/// <remarks>
		/// Obtains the audio file format of the input stream provided.  The stream must
		/// point to valid audio file data.  In general, audio file readers may
		/// need to read some data from the stream before determining whether they
		/// support it.  These parsers must
		/// be able to mark the stream, read enough data to determine whether they
		/// support the stream, and, if not, reset the stream's read pointer to its original
		/// position.  If the input stream does not support this, this method may fail
		/// with an <code>IOException</code>.
		/// </remarks>
		/// <param name="stream">
		/// the input stream from which file format information should be
		/// extracted
		/// </param>
		/// <returns>an <code>AudioFileFormat</code> object describing the audio file format</returns>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException">
		/// if the stream does not point to valid audio
		/// file data recognized by the system
		/// </exception>
		/// <exception cref="System.IO.IOException">if an I/O exception occurs</exception>
		/// <seealso cref="InputStream.markSupported()">InputStream.markSupported()</seealso>
		/// <seealso cref="InputStream.mark(int)">InputStream.mark(int)</seealso>
		public abstract AudioFileFormat getAudioFileFormat(java.io.InputStream 
			stream);

		/// <summary>Obtains the audio file format of the URL provided.</summary>
		/// <remarks>
		/// Obtains the audio file format of the URL provided.  The URL must
		/// point to valid audio file data.
		/// </remarks>
		/// <param name="url">
		/// the URL from which file format information should be
		/// extracted
		/// </param>
		/// <returns>an <code>AudioFileFormat</code> object describing the audio file format</returns>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException">
		/// if the URL does not point to valid audio
		/// file data recognized by the system
		/// </exception>
		/// <exception cref="System.IO.IOException">if an I/O exception occurs</exception>
		public abstract AudioFileFormat getAudioFileFormat(java.net.URL url);

		/// <summary>Obtains the audio file format of the <code>File</code> provided.</summary>
		/// <remarks>
		/// Obtains the audio file format of the <code>File</code> provided.  The <code>File</code> must
		/// point to valid audio file data.
		/// </remarks>
		/// <param name="file">
		/// the <code>File</code> from which file format information should be
		/// extracted
		/// </param>
		/// <returns>an <code>AudioFileFormat</code> object describing the audio file format</returns>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException">
		/// if the <code>File</code> does not point to valid audio
		/// file data recognized by the system
		/// </exception>
		/// <exception cref="System.IO.IOException">if an I/O exception occurs</exception>
		public abstract AudioFileFormat getAudioFileFormat(java.io.File file);

		/// <summary>Obtains an audio input stream from the input stream provided.</summary>
		/// <remarks>
		/// Obtains an audio input stream from the input stream provided.  The stream must
		/// point to valid audio file data.  In general, audio file readers may
		/// need to read some data from the stream before determining whether they
		/// support it.  These parsers must
		/// be able to mark the stream, read enough data to determine whether they
		/// support the stream, and, if not, reset the stream's read pointer to its original
		/// position.  If the input stream does not support this, this method may fail
		/// with an <code>IOException</code>.
		/// </remarks>
		/// <param name="stream">
		/// the input stream from which the <code>AudioInputStream</code> should be
		/// constructed
		/// </param>
		/// <returns>
		/// an <code>AudioInputStream</code> object based on the audio file data contained
		/// in the input stream.
		/// </returns>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException">
		/// if the stream does not point to valid audio
		/// file data recognized by the system
		/// </exception>
		/// <exception cref="System.IO.IOException">if an I/O exception occurs</exception>
		/// <seealso cref="InputStream.markSupported()">InputStream.markSupported()</seealso>
		/// <seealso cref="InputStream.mark(int)">InputStream.mark(int)</seealso>
		public abstract AudioInputStream getAudioInputStream(java.io.InputStream
			 stream);

		/// <summary>Obtains an audio input stream from the URL provided.</summary>
		/// <remarks>
		/// Obtains an audio input stream from the URL provided.  The URL must
		/// point to valid audio file data.
		/// </remarks>
		/// <param name="url">
		/// the URL for which the <code>AudioInputStream</code> should be
		/// constructed
		/// </param>
		/// <returns>
		/// an <code>AudioInputStream</code> object based on the audio file data pointed
		/// to by the URL
		/// </returns>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException">
		/// if the URL does not point to valid audio
		/// file data recognized by the system
		/// </exception>
		/// <exception cref="System.IO.IOException">if an I/O exception occurs</exception>
		public abstract AudioInputStream getAudioInputStream(java.net.URL url);

		/// <summary>Obtains an audio input stream from the <code>File</code> provided.</summary>
		/// <remarks>
		/// Obtains an audio input stream from the <code>File</code> provided.  The <code>File</code> must
		/// point to valid audio file data.
		/// </remarks>
		/// <param name="file">
		/// the <code>File</code> for which the <code>AudioInputStream</code> should be
		/// constructed
		/// </param>
		/// <returns>
		/// an <code>AudioInputStream</code> object based on the audio file data pointed
		/// to by the File
		/// </returns>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException">
		/// if the <code>File</code> does not point to valid audio
		/// file data recognized by the system
		/// </exception>
		/// <exception cref="System.IO.IOException">if an I/O exception occurs</exception>
		public abstract AudioInputStream getAudioInputStream(java.io.File file);
	}
}
