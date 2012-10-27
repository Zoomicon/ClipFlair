namespace org.xiph.speex.spi
{
	/// <summary>Provider for Speex audio file reading services.</summary>
	/// <remarks>
	/// Provider for Speex audio file reading services.
	/// This implementation can parse the format information from Speex audio file,
	/// and can produce audio input streams from files of this type.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public class SpeexAudioFileReader : javax.sound.sampled.spi.AudioFileReader
	{
		public const int OGG_HEADERSIZE = 27;

		/// <summary>The size of the Speex header.</summary>
		/// <remarks>The size of the Speex header.</remarks>
		public const int SPEEX_HEADERSIZE = 80;

		public const int SEGOFFSET = 26;

		/// <summary>The String that identifies the beginning of an Ogg packet.</summary>
		/// <remarks>The String that identifies the beginning of an Ogg packet.</remarks>
		public static readonly string OGGID = "OggS";

		/// <summary>The String that identifies the beginning of the Speex header.</summary>
		/// <remarks>The String that identifies the beginning of the Speex header.</remarks>
		public static readonly string SPEEXID = "Speex   ";

		/// <summary>Obtains the audio file format of the File provided.</summary>
		/// <remarks>
		/// Obtains the audio file format of the File provided.
		/// The File must point to valid audio file data.
		/// </remarks>
		/// <param name="file">
		/// the File from which file format information should be
		/// extracted.
		/// </param>
		/// <returns>an AudioFileFormat object describing the audio file format.</returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override javax.sound.sampled.AudioFileFormat getAudioFileFormat(java.io.File
			 file)
		{
			java.io.InputStream inputStream = null;
			try
			{
				inputStream = new java.io.FileInputStream(file);
				return getAudioFileFormat(inputStream, (int)file.length());
			}
			finally
			{
				inputStream.close();
			}
		}

		/// <summary>Obtains an audio input stream from the URL provided.</summary>
		/// <remarks>
		/// Obtains an audio input stream from the URL provided.
		/// The URL must point to valid audio file data.
		/// </remarks>
		/// <param name="url">the URL for which the AudioInputStream should be constructed.</param>
		/// <returns>
		/// an AudioInputStream object based on the audio file data pointed to
		/// by the URL.
		/// </returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override javax.sound.sampled.AudioFileFormat getAudioFileFormat(java.net.URL
			 url)
		{
			java.io.InputStream inputStream = url.openStream();
			try
			{
				return getAudioFileFormat(inputStream);
			}
			finally
			{
				inputStream.close();
			}
		}

		/// <summary>Obtains an audio input stream from the input stream provided.</summary>
		/// <remarks>Obtains an audio input stream from the input stream provided.</remarks>
		/// <param name="stream">
		/// the input stream from which the AudioInputStream should be
		/// constructed.
		/// </param>
		/// <returns>
		/// an AudioInputStream object based on the audio file data contained
		/// in the input stream.
		/// </returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override javax.sound.sampled.AudioFileFormat getAudioFileFormat(java.io.InputStream
			 stream)
		{
			return getAudioFileFormat(stream, javax.sound.sampled.AudioSystem.NOT_SPECIFIED);
		}

		/// <summary>Return the AudioFileFormat from the given InputStream.</summary>
		/// <remarks>Return the AudioFileFormat from the given InputStream.</remarks>
		/// <param name="stream">
		/// the input stream from which the AudioInputStream should be
		/// constructed.
		/// </param>
		/// <param name="medialength"></param>
		/// <returns>
		/// an AudioInputStream object based on the audio file data contained
		/// in the input stream.
		/// </returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		protected virtual javax.sound.sampled.AudioFileFormat getAudioFileFormat(java.io.InputStream
			 stream, int medialength)
		{
			return getAudioFileFormat(stream, null, medialength);
		}

		/// <summary>Return the AudioFileFormat from the given InputStream.</summary>
		/// <remarks>Return the AudioFileFormat from the given InputStream. Implementation.</remarks>
		/// <param name="bitStream"></param>
		/// <param name="baos"></param>
		/// <param name="mediaLength"></param>
		/// <returns>
		/// an AudioInputStream object based on the audio file data contained
		/// in the input stream.
		/// </returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		protected virtual javax.sound.sampled.AudioFileFormat getAudioFileFormat(java.io.InputStream
			 bitStream, java.io.ByteArrayOutputStream baos, int mediaLength)
		{
			javax.sound.sampled.AudioFormat format;
			try
			{
				// If we can't read the format of this stream, we must restore stream to
				// beginning so other providers can attempt to read the stream.
				if (bitStream.markSupported())
				{
					// maximum number of bytes to determine the stream encoding:
					// Size of 1st Ogg Packet (Speex header) = OGG_HEADERSIZE + SPEEX_HEADERSIZE + 1
					// Size of 2nd Ogg Packet (Comment)      = OGG_HEADERSIZE + comment_size + 1
					// Size of 3rd Ogg Header (First data)   = OGG_HEADERSIZE + number_of_frames
					// where number_of_frames < 256 and comment_size < 256 (if within 1 frame)
					bitStream.mark(3 * OGG_HEADERSIZE + SPEEX_HEADERSIZE + 256 + 256 + 2);
				}
				int mode = -1;
				int sampleRate = 0;
				int channels = 0;
				int frameSize = javax.sound.sampled.AudioSystem.NOT_SPECIFIED;
				float frameRate = javax.sound.sampled.AudioSystem.NOT_SPECIFIED;
				byte[] header = new byte[128];
				int segments = 0;
				int bodybytes = 0;
				java.io.DataInputStream dis = new java.io.DataInputStream(bitStream);
				if (baos == null)
				{
					baos = new java.io.ByteArrayOutputStream(128);
				}
				int origchksum;
				int chksum;
				// read the OGG header
				dis.readFully(header, 0, OGG_HEADERSIZE);
				baos.write(header, 0, OGG_HEADERSIZE);
				origchksum = readInt(header, 22);
				header[22] = 0;
				header[23] = 0;
				header[24] = 0;
				header[25] = 0;
				chksum = org.xiph.speex.OggCrc.checksum(0, header, 0, OGG_HEADERSIZE);
				// make sure its a OGG header
                if (!OGGID.Equals(cspeex.StringUtil.getStringForBytes(header, 0, 4)))
				{
					throw new javax.sound.sampled.UnsupportedAudioFileException("missing ogg id!");
				}
				// how many segments are there?
				segments = header[SEGOFFSET] & unchecked((int)(0xFF));
				if (segments > 1)
				{
					throw new javax.sound.sampled.UnsupportedAudioFileException("Corrupt Speex Header: more than 1 segments"
						);
				}
				dis.readFully(header, OGG_HEADERSIZE, segments);
				baos.write(header, OGG_HEADERSIZE, segments);
				chksum = org.xiph.speex.OggCrc.checksum(chksum, header, OGG_HEADERSIZE, segments);
				// get the number of bytes in the segment
				bodybytes = header[OGG_HEADERSIZE] & unchecked((int)(0xFF));
				if (bodybytes != SPEEX_HEADERSIZE)
				{
					throw new javax.sound.sampled.UnsupportedAudioFileException("Corrupt Speex Header: size="
						 + bodybytes);
				}
				// read the Speex header
				dis.readFully(header, OGG_HEADERSIZE + 1, bodybytes);
				baos.write(header, OGG_HEADERSIZE + 1, bodybytes);
				chksum = org.xiph.speex.OggCrc.checksum(chksum, header, OGG_HEADERSIZE + 1, bodybytes
					);
				// make sure its a Speex header
                if (!SPEEXID.Equals(cspeex.StringUtil.getStringForBytes(header, OGG_HEADERSIZE + 1, 
					8)))
				{
					throw new javax.sound.sampled.UnsupportedAudioFileException("Corrupt Speex Header: missing Speex ID"
						);
				}
				mode = readInt(header, OGG_HEADERSIZE + 1 + 40);
				sampleRate = readInt(header, OGG_HEADERSIZE + 1 + 36);
				channels = readInt(header, OGG_HEADERSIZE + 1 + 48);
				int nframes = readInt(header, OGG_HEADERSIZE + 1 + 64);
				bool vbr = readInt(header, OGG_HEADERSIZE + 1 + 60) == 1;
				// Checksum
				if (chksum != origchksum)
				{
					throw new System.IO.IOException("Ogg CheckSums do not match");
				}
				// Calculate frameSize
				if (!vbr)
				{
				}
				// Frames size is a constant so:
				// Read Comment Packet the Ogg Header of 1st data packet;
				// the array table_segment repeats the frame size over and over.
				// Calculate frameRate
				if (mode >= 0 && mode <= 2 && nframes > 0)
				{
					frameRate = ((float)sampleRate) / ((mode == 0 ? 160f : (mode == 1 ? 320f : 640f))
						 * ((float)nframes));
				}
				format = new javax.sound.sampled.AudioFormat(org.xiph.speex.spi.SpeexEncoding.SPEEX
					, (float)sampleRate, javax.sound.sampled.AudioSystem.NOT_SPECIFIED, channels, frameSize
					, frameRate, false);
			}
			catch (javax.sound.sampled.UnsupportedAudioFileException e)
			{
				// reset the stream for other providers
				if (bitStream.markSupported())
				{
					bitStream.reset();
				}
				// just rethrow this exception
				throw;
			}
			catch (System.IO.IOException ioe)
			{
				// reset the stream for other providers
				if (bitStream.markSupported())
				{
					bitStream.reset();
				}
				throw new javax.sound.sampled.UnsupportedAudioFileException(ioe.Message);
			}
			return new javax.sound.sampled.AudioFileFormat(org.xiph.speex.spi.SpeexFileFormatType
				.SPEEX, format, javax.sound.sampled.AudioSystem.NOT_SPECIFIED);
		}

		/// <summary>Obtains an audio input stream from the File provided.</summary>
		/// <remarks>
		/// Obtains an audio input stream from the File provided.
		/// The File must point to valid audio file data.
		/// </remarks>
		/// <param name="file">the File for which the AudioInputStream should be constructed.
		/// 	</param>
		/// <returns>
		/// an AudioInputStream object based on the audio file data pointed to
		/// by the File.
		/// </returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override javax.sound.sampled.AudioInputStream getAudioInputStream(java.io.File
			 file)
		{
			java.io.InputStream inputStream = new java.io.FileInputStream(file);
			try
			{
				return getAudioInputStream(inputStream, (int)file.length());
			}
			catch (javax.sound.sampled.UnsupportedAudioFileException e)
			{
				inputStream.close();
				throw;
			}
			catch (System.IO.IOException e)
			{
				inputStream.close();
				throw;
			}
		}

		/// <summary>Obtains an audio input stream from the URL provided.</summary>
		/// <remarks>
		/// Obtains an audio input stream from the URL provided.
		/// The URL must point to valid audio file data.
		/// </remarks>
		/// <param name="url">the URL for which the AudioInputStream should be constructed.</param>
		/// <returns>
		/// an AudioInputStream object based on the audio file data pointed to
		/// by the URL.
		/// </returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override javax.sound.sampled.AudioInputStream getAudioInputStream(java.net.URL
			 url)
		{
			java.io.InputStream inputStream = url.openStream();
			try
			{
				return getAudioInputStream(inputStream);
			}
			catch (javax.sound.sampled.UnsupportedAudioFileException e)
			{
				inputStream.close();
				throw;
			}
			catch (System.IO.IOException e)
			{
				inputStream.close();
				throw;
			}
		}

		/// <summary>Obtains an audio input stream from the input stream provided.</summary>
		/// <remarks>
		/// Obtains an audio input stream from the input stream provided.
		/// The stream must point to valid audio file data.
		/// </remarks>
		/// <param name="stream">
		/// the input stream from which the AudioInputStream should be
		/// constructed.
		/// </param>
		/// <returns>
		/// an AudioInputStream object based on the audio file data contained
		/// in the input stream.
		/// </returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override javax.sound.sampled.AudioInputStream getAudioInputStream(java.io.InputStream
			 stream)
		{
			return getAudioInputStream(stream, javax.sound.sampled.AudioSystem.NOT_SPECIFIED);
		}

		/// <summary>Obtains an audio input stream from the input stream provided.</summary>
		/// <remarks>
		/// Obtains an audio input stream from the input stream provided.
		/// The stream must point to valid audio file data.
		/// </remarks>
		/// <param name="inputStream">
		/// the input stream from which the AudioInputStream should
		/// be constructed.
		/// </param>
		/// <param name="medialength"></param>
		/// <returns>
		/// an AudioInputStream object based on the audio file data contained
		/// in the input stream.
		/// </returns>
		/// <exception>
		/// UnsupportedAudioFileException
		/// if the File does not point to
		/// a valid audio file data recognized by the system.
		/// </exception>
		/// <exception>
		/// IOException
		/// if an I/O exception occurs.
		/// </exception>
		/// <exception cref="javax.sound.sampled.UnsupportedAudioFileException"></exception>
		/// <exception cref="System.IO.IOException"></exception>
		protected virtual javax.sound.sampled.AudioInputStream getAudioInputStream(java.io.InputStream
			 inputStream, int medialength)
		{
			java.io.ByteArrayOutputStream baos = new java.io.ByteArrayOutputStream(128);
			javax.sound.sampled.AudioFileFormat audioFileFormat = getAudioFileFormat(inputStream
				, baos, medialength);
			java.io.ByteArrayInputStream bais = new java.io.ByteArrayInputStream(baos.toByteArray
				());
			java.io.SequenceInputStream sequenceInputStream = new java.io.SequenceInputStream
				(bais, inputStream);
			return new javax.sound.sampled.AudioInputStream(sequenceInputStream, audioFileFormat
				.getFormat(), audioFileFormat.getFrameLength());
		}

		/// <summary>Converts Little Endian (Windows) bytes to an int (Java uses Big Endian).
		/// 	</summary>
		/// <remarks>Converts Little Endian (Windows) bytes to an int (Java uses Big Endian).
		/// 	</remarks>
		/// <param name="data">the data to read.</param>
		/// <param name="offset">the offset from which to start reading.</param>
		/// <returns>the integer value of the reassembled bytes.</returns>
		private static int readInt(byte[] data, int offset)
		{
			return (data[offset] & unchecked((int)(0xff))) | ((data[offset + 1] & unchecked((
				int)(0xff))) << 8) | ((data[offset + 2] & unchecked((int)(0xff))) << 16) | (data
				[offset + 3] << 24);
		}
		// no & 0xff at the end to keep the sign
	}
}
