namespace javax.sound.sampled.spi
{
	/// <summary>Provider for audio file writing services.</summary>
	/// <remarks>
	/// Provider for audio file writing services.  Classes providing concrete
	/// implementations can write one or more types of audio file from an audio
	/// stream.
	/// </remarks>
	/// <author>Kara Kytle</author>
	/// <version>1.25, 05/11/17</version>
	/// <since>1.3</since>
	public abstract class AudioFileWriter
	{
		/// <summary>
		/// Obtains the file types for which file writing support is provided by this
		/// audio file writer.
		/// </summary>
		/// <remarks>
		/// Obtains the file types for which file writing support is provided by this
		/// audio file writer.
		/// </remarks>
		/// <returns>
		/// array of file types.  If no file types are supported,
		/// an array of length 0 is returned.
		/// </returns>
		public abstract AudioFileFormat.Type[] getAudioFileTypes();

		/// <summary>
		/// Indicates whether file writing support for the specified file type is provided
		/// by this audio file writer.
		/// </summary>
		/// <remarks>
		/// Indicates whether file writing support for the specified file type is provided
		/// by this audio file writer.
		/// </remarks>
		/// <param name="fileType">the file type for which write capabilities are queried</param>
		/// <returns>
		/// <code>true</code> if the file type is supported,
		/// otherwise <code>false</code>
		/// </returns>
		public virtual bool isFileTypeSupported(AudioFileFormat.Type fileType)
		{
			AudioFileFormat.Type[] types = getAudioFileTypes();
			for (int i = 0; i < types.Length; i++)
			{
				if (fileType.Equals(types[i]))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Obtains the file types that this audio file writer can write from the
		/// audio input stream specified.
		/// </summary>
		/// <remarks>
		/// Obtains the file types that this audio file writer can write from the
		/// audio input stream specified.
		/// </remarks>
		/// <param name="stream">
		/// the audio input stream for which audio file type support
		/// is queried
		/// </param>
		/// <returns>
		/// array of file types.  If no file types are supported,
		/// an array of length 0 is returned.
		/// </returns>
		public abstract AudioFileFormat.Type[] getAudioFileTypes(AudioInputStream
			 stream);

		/// <summary>
		/// Indicates whether an audio file of the type specified can be written
		/// from the audio input stream indicated.
		/// </summary>
		/// <remarks>
		/// Indicates whether an audio file of the type specified can be written
		/// from the audio input stream indicated.
		/// </remarks>
		/// <param name="fileType">file type for which write capabilities are queried</param>
		/// <param name="stream">for which file writing support is queried</param>
		/// <returns>
		/// <code>true</code> if the file type is supported for this audio input stream,
		/// otherwise <code>false</code>
		/// </returns>
		public virtual bool isFileTypeSupported(AudioFileFormat.Type fileType, AudioInputStream
			 stream)
		{
			AudioFileFormat.Type[] types = getAudioFileTypes(stream);
			for (int i = 0; i < types.Length; i++)
			{
				if (fileType.Equals(types[i]))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Writes a stream of bytes representing an audio file of the file type
		/// indicated to the output stream provided.
		/// </summary>
		/// <remarks>
		/// Writes a stream of bytes representing an audio file of the file type
		/// indicated to the output stream provided.  Some file types require that
		/// the length be written into the file header, and cannot be written from
		/// start to finish unless the length is known in advance.  An attempt
		/// to write such a file type will fail with an IOException if the length in
		/// the audio file format is
		/// <see cref="javax.sound.sampled.AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED
		/// 	</see>
		/// .
		/// </remarks>
		/// <param name="stream">
		/// the audio input stream containing audio data to be
		/// written to the output stream
		/// </param>
		/// <param name="fileType">file type to be written to the output stream</param>
		/// <param name="out">stream to which the file data should be written</param>
		/// <returns>the number of bytes written to the output stream</returns>
		/// <exception cref="System.IO.IOException">if an I/O exception occurs</exception>
		/// <exception cref="System.ArgumentException">
		/// if the file type is not supported by
		/// the system
		/// </exception>
		/// <seealso cref="isFileTypeSupported(Type, AudioInputStream)">isFileTypeSupported(Type, AudioInputStream)
		/// 	</seealso>
		/// <seealso cref="getAudioFileTypes()">getAudioFileTypes()</seealso>
		public abstract int write(AudioInputStream stream, AudioFileFormat.Type
			 fileType, java.io.OutputStream @out);

		/// <summary>
		/// Writes a stream of bytes representing an audio file of the file format
		/// indicated to the external file provided.
		/// </summary>
		/// <remarks>
		/// Writes a stream of bytes representing an audio file of the file format
		/// indicated to the external file provided.
		/// </remarks>
		/// <param name="stream">
		/// the audio input stream containing audio data to be
		/// written to the file
		/// </param>
		/// <param name="fileType">file type to be written to the file</param>
		/// <param name="out">external file to which the file data should be written</param>
		/// <returns>the number of bytes written to the file</returns>
		/// <exception cref="System.IO.IOException">if an I/O exception occurs</exception>
		/// <exception cref="System.ArgumentException">
		/// if the file format is not supported by
		/// the system
		/// </exception>
		/// <seealso cref="isFileTypeSupported(Type)">isFileTypeSupported(Type)</seealso>
		/// <seealso cref="getAudioFileTypes()">getAudioFileTypes()</seealso>
		public abstract int write(AudioInputStream stream, AudioFileFormat.Type
			 fileType, java.io.File @out);
	}
}
