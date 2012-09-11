namespace org.xiph.speex.spi
{
	/// <summary>Provider for Speex audio file writing services.</summary>
	/// <remarks>
	/// Provider for Speex audio file writing services.
	/// This implementation can write Speex audio files from an audio stream.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 143 $</version>
	public class SpeexAudioFileWriter : javax.sound.sampled.spi.AudioFileWriter
	{
		public static readonly javax.sound.sampled.AudioFileFormat.Type[] NO_FORMAT = new 
			javax.sound.sampled.AudioFileFormat.Type[] {  };

		public static readonly javax.sound.sampled.AudioFileFormat.Type[] SPEEX_FORMAT = 
			new javax.sound.sampled.AudioFileFormat.Type[] { org.xiph.speex.spi.SpeexFileFormatType
			.SPEEX };

		/// <summary>Obtains the file types for which file writing support is provided by this audio file writer.
		/// 	</summary>
		/// <remarks>Obtains the file types for which file writing support is provided by this audio file writer.
		/// 	</remarks>
		/// <returns>array of file types. If no file types are supported, an array of length 0 is returned.
		/// 	</returns>
		public override javax.sound.sampled.AudioFileFormat.Type[] getAudioFileTypes()
		{
			return SPEEX_FORMAT;
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
		/// - the audio input stream for which audio file type support
		/// is queried.
		/// </param>
		/// <returns>
		/// array of file types. If no file types are supported, an array of
		/// length 0 is returned.
		/// </returns>
		public override javax.sound.sampled.AudioFileFormat.Type[] getAudioFileTypes(javax.sound.sampled.AudioInputStream
			 stream)
		{
			if (stream.getFormat().getEncoding() is org.xiph.speex.spi.SpeexEncoding)
			{
				return SPEEX_FORMAT;
			}
			else
			{
				return NO_FORMAT;
			}
		}

		/// <summary>
		/// Writes a stream of bytes representing an audio file of the file type
		/// indicated to the output stream provided.
		/// </summary>
		/// <remarks>
		/// Writes a stream of bytes representing an audio file of the file type
		/// indicated to the output stream provided. Some file types require that the
		/// length be written into the file header, and cannot be written from start
		/// to finish unless the length is known in advance.
		/// An attempt to write such a file type will fail with an IOException if the
		/// length in the audio file format is AudioSystem.NOT_SPECIFIED.
		/// </remarks>
		/// <param name="stream">
		/// - the audio input stream containing audio data to be written
		/// to the output stream.
		/// </param>
		/// <param name="fileType">- file type to be written to the output stream.</param>
		/// <param name="out">- stream to which the file data should be written.</param>
		/// <returns>the number of bytes written to the output stream.</returns>
		/// <exception>
		/// IOException
		/// - if an I/O exception occurs.
		/// </exception>
		/// <exception>
		/// IllegalArgumentException
		/// - if the file type is not supported by the system.
		/// </exception>
		/// <seealso cref="javax.sound.sampled.spi.AudioFileWriter.isFileTypeSupported(javax.sound.sampled.AudioFileFormat.Type, javax.sound.sampled.AudioInputStream)
		/// 	">javax.sound.sampled.spi.AudioFileWriter.isFileTypeSupported(javax.sound.sampled.AudioFileFormat.Type, javax.sound.sampled.AudioInputStream)
		/// 	</seealso>
		/// <seealso cref="getAudioFileTypes()">getAudioFileTypes()</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int write(javax.sound.sampled.AudioInputStream stream, javax.sound.sampled.AudioFileFormat.Type
			 fileType, java.io.OutputStream @out)
		{
			javax.sound.sampled.AudioFileFormat.Type[] formats = getAudioFileTypes(stream);
			if (formats != null && formats.Length > 0)
			{
				return write(stream, @out);
			}
			else
			{
				throw new System.ArgumentException("cannot write given file type");
			}
		}

		/// <summary>
		/// Writes a stream of bytes representing an audio file of the file format
		/// indicated to the external file provided.
		/// </summary>
		/// <remarks>
		/// Writes a stream of bytes representing an audio file of the file format
		/// indicated to the external file provided.
		/// </remarks>
		/// <param name="stream">
		/// - the audio input stream containing audio data to be written
		/// to the file.
		/// </param>
		/// <param name="fileType">- file type to be written to the file.</param>
		/// <param name="out">- external file to which the file data should be written.</param>
		/// <returns>the number of bytes written to the file.</returns>
		/// <exception>
		/// IOException
		/// - if an I/O exception occurs.
		/// </exception>
		/// <exception>
		/// IllegalArgumentException
		/// - if the file format is not supported by the system
		/// </exception>
		/// <seealso cref="javax.sound.sampled.spi.AudioFileWriter.isFileTypeSupported(javax.sound.sampled.AudioFileFormat.Type)
		/// 	">javax.sound.sampled.spi.AudioFileWriter.isFileTypeSupported(javax.sound.sampled.AudioFileFormat.Type)
		/// 	</seealso>
		/// <seealso cref="getAudioFileTypes()">getAudioFileTypes()</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int write(javax.sound.sampled.AudioInputStream stream, javax.sound.sampled.AudioFileFormat.Type
			 fileType, java.io.File @out)
		{
			javax.sound.sampled.AudioFileFormat.Type[] formats = getAudioFileTypes(stream);
			if (formats != null && formats.Length > 0)
			{
				java.io.FileOutputStream fos = new java.io.FileOutputStream(@out);
				return write(stream, fos);
			}
			else
			{
				throw new System.ArgumentException("cannot write given file type");
			}
		}

		/// <summary>
		/// Writes a stream of bytes representing an audio file of the file type
		/// indicated to the output stream provided.
		/// </summary>
		/// <remarks>
		/// Writes a stream of bytes representing an audio file of the file type
		/// indicated to the output stream provided.
		/// </remarks>
		/// <param name="stream">
		/// - the audio input stream containing audio data to be written
		/// to the output stream.
		/// </param>
		/// <param name="out">- stream to which the file data should be written.</param>
		/// <returns>the number of bytes written to the output stream.</returns>
		/// <exception>
		/// IOException
		/// - if an I/O exception occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		private int write(javax.sound.sampled.AudioInputStream stream, java.io.OutputStream
			 @out)
		{
			byte[] data = new byte[2048];
			int read = 0;
			int temp;
			while ((temp = stream.read(data, 0, 2048)) > 0)
			{
				@out.write(data, 0, temp);
				read += temp;
			}
			@out.flush();
			@out.close();
			return read;
		}
	}
}
