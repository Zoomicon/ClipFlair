namespace org.xiph.speex.spi
{
	/// <summary>
	/// A format conversion provider provides format conversion services from one or
	/// more input formats to one or more output formats.
	/// </summary>
	/// <remarks>
	/// A format conversion provider provides format conversion services from one or
	/// more input formats to one or more output formats. Converters include codecs,
	/// which encode and/or decode audio data, as well as transcoders, etc.
	/// Format converters provide methods for determining what conversions are
	/// supported and for obtaining an audio stream from which converted data can be
	/// read.
	/// The source format represents the format of the incoming audio data, which
	/// will be converted.
	/// The target format represents the format of the processed, converted audio
	/// data. This is the format of the data that can be read from the stream
	/// returned by one of the getAudioInputStream methods.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public class SpeexFormatConvertionProvider : javax.sound.sampled.spi.FormatConversionProvider
	{
		public static readonly javax.sound.sampled.AudioFormat.Encoding[] NO_ENCODING = new 
			javax.sound.sampled.AudioFormat.Encoding[] {  };

		public static readonly javax.sound.sampled.AudioFormat.Encoding[] PCM_ENCODING = 
			new javax.sound.sampled.AudioFormat.Encoding[] { javax.sound.sampled.AudioFormat.Encoding
			.PCM_SIGNED };

		public static readonly javax.sound.sampled.AudioFormat.Encoding[] SPEEX_ENCODING = 
			new javax.sound.sampled.AudioFormat.Encoding[] { org.xiph.speex.spi.SpeexEncoding
			.SPEEX };

		public static readonly javax.sound.sampled.AudioFormat.Encoding[] BOTH_ENCODINGS = 
			new javax.sound.sampled.AudioFormat.Encoding[] { org.xiph.speex.spi.SpeexEncoding
			.SPEEX, javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED };

		public static readonly javax.sound.sampled.AudioFormat[] NO_FORMAT = new javax.sound.sampled.AudioFormat
			[] {  };

		/// <summary>
		/// Obtains the set of source format encodings from which format conversion
		/// services are provided by this provider.
		/// </summary>
		/// <remarks>
		/// Obtains the set of source format encodings from which format conversion
		/// services are provided by this provider.
		/// </remarks>
		/// <returns>
		/// array of source format encodings.
		/// The array will always have a length of at least 1.
		/// </returns>
		public override javax.sound.sampled.AudioFormat.Encoding[] getSourceEncodings()
		{
			javax.sound.sampled.AudioFormat.Encoding[] encodings = new javax.sound.sampled.AudioFormat.Encoding
				[] { org.xiph.speex.spi.SpeexEncoding.SPEEX, javax.sound.sampled.AudioFormat.Encoding
				.PCM_SIGNED };
			return encodings;
		}

		/// <summary>
		/// Obtains the set of target format encodings to which format conversion
		/// services are provided by this provider.
		/// </summary>
		/// <remarks>
		/// Obtains the set of target format encodings to which format conversion
		/// services are provided by this provider.
		/// </remarks>
		/// <returns>
		/// array of target format encodings.
		/// The array will always have a length of at least 1.
		/// </returns>
		public override javax.sound.sampled.AudioFormat.Encoding[] getTargetEncodings()
		{
			javax.sound.sampled.AudioFormat.Encoding[] encodings = new javax.sound.sampled.AudioFormat.Encoding
				[] { org.xiph.speex.spi.SpeexEncoding.SPEEX_Q0, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_Q1, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q2, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_Q3, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q4, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_Q5, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q6, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_Q7, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q8, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_Q9, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q10, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_VBR0, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR1, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_VBR2, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR3, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_VBR4, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR5, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_VBR6, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR7, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_VBR8, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR9, org.xiph.speex.spi.SpeexEncoding
				.SPEEX_VBR10, javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED };
			return encodings;
		}

		/// <summary>
		/// Obtains the set of target format encodings supported by the format
		/// converter given a particular source format.
		/// </summary>
		/// <remarks>
		/// Obtains the set of target format encodings supported by the format
		/// converter given a particular source format. If no target format encodings
		/// are supported for this source format, an array of length 0 is returned.
		/// </remarks>
		/// <param name="sourceFormat">format of the incoming data.</param>
		/// <returns>array of supported target format encodings.</returns>
		public override javax.sound.sampled.AudioFormat.Encoding[] getTargetEncodings(javax.sound.sampled.AudioFormat
			 sourceFormat)
		{
			if (sourceFormat.getEncoding().Equals(javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED
				))
			{
				javax.sound.sampled.AudioFormat.Encoding[] encodings = new javax.sound.sampled.AudioFormat.Encoding
					[] { org.xiph.speex.spi.SpeexEncoding.SPEEX_Q0, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_Q1, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q2, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_Q3, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q4, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_Q5, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q6, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_Q7, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q8, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_Q9, org.xiph.speex.spi.SpeexEncoding.SPEEX_Q10, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_VBR0, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR1, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_VBR2, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR3, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_VBR4, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR5, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_VBR6, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR7, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_VBR8, org.xiph.speex.spi.SpeexEncoding.SPEEX_VBR9, org.xiph.speex.spi.SpeexEncoding
					.SPEEX_VBR10 };
				return encodings;
			}
			else
			{
				if (sourceFormat.getEncoding() is org.xiph.speex.spi.SpeexEncoding)
				{
					javax.sound.sampled.AudioFormat.Encoding[] encodings = new javax.sound.sampled.AudioFormat.Encoding
						[] { javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED };
					return encodings;
				}
				else
				{
					javax.sound.sampled.AudioFormat.Encoding[] encodings = new javax.sound.sampled.AudioFormat.Encoding
						[] {  };
					return encodings;
				}
			}
		}

		/// <summary>
		/// Obtains the set of target formats with the encoding specified supported by
		/// the format converter.
		/// </summary>
		/// <remarks>
		/// Obtains the set of target formats with the encoding specified supported by
		/// the format converter. If no target formats with the specified encoding are
		/// supported for this source format, an array of length 0 is returned.
		/// </remarks>
		/// <param name="targetEncoding">desired encoding of the outgoing data.</param>
		/// <param name="sourceFormat">format of the incoming data.</param>
		/// <returns>array of supported target formats.</returns>
		public override javax.sound.sampled.AudioFormat[] getTargetFormats(javax.sound.sampled.AudioFormat.Encoding
			 targetEncoding, javax.sound.sampled.AudioFormat sourceFormat)
		{
			if (sourceFormat.getEncoding().Equals(javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED
				) && targetEncoding is org.xiph.speex.spi.SpeexEncoding)
			{
				if (sourceFormat.getChannels() > 2 || sourceFormat.getChannels() <= 0 || sourceFormat
					.isBigEndian())
				{
					javax.sound.sampled.AudioFormat[] formats = new javax.sound.sampled.AudioFormat[]
						 {  };
					return formats;
				}
				else
				{
					javax.sound.sampled.AudioFormat[] formats = new javax.sound.sampled.AudioFormat[]
						 { new javax.sound.sampled.AudioFormat(targetEncoding, sourceFormat.getSampleRate
						(), -1, sourceFormat.getChannels(), -1, -1, false) };
					// sample size in bits
					// frame size
					// frame rate
					// little endian
					return formats;
				}
			}
			else
			{
				if (sourceFormat.getEncoding() is org.xiph.speex.spi.SpeexEncoding && targetEncoding
					.Equals(javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED))
				{
					javax.sound.sampled.AudioFormat[] formats = new javax.sound.sampled.AudioFormat[]
						 { new javax.sound.sampled.AudioFormat(sourceFormat.getSampleRate(), 16, sourceFormat
						.getChannels(), true, false) };
					// sample size in bits
					// signed
					// little endian (for PCM wav)
					return formats;
				}
				else
				{
					javax.sound.sampled.AudioFormat[] formats = new javax.sound.sampled.AudioFormat[]
						 {  };
					return formats;
				}
			}
		}

		/// <summary>
		/// Obtains an audio input stream with the specified encoding from the given
		/// audio input stream.
		/// </summary>
		/// <remarks>
		/// Obtains an audio input stream with the specified encoding from the given
		/// audio input stream.
		/// </remarks>
		/// <param name="targetEncoding">- desired encoding of the stream after processing.</param>
		/// <param name="sourceStream">- stream from which data to be processed should be read.
		/// 	</param>
		/// <returns>
		/// stream from which processed data with the specified target
		/// encoding may be read.
		/// </returns>
		/// <exception>
		/// IllegalArgumentException
		/// - if the format combination supplied
		/// is not supported.
		/// </exception>
		public override javax.sound.sampled.AudioInputStream getAudioInputStream(javax.sound.sampled.AudioFormat.Encoding
			 targetEncoding, javax.sound.sampled.AudioInputStream sourceStream)
		{
			if (isConversionSupported(targetEncoding, sourceStream.getFormat()))
			{
				javax.sound.sampled.AudioFormat[] formats = getTargetFormats(targetEncoding, sourceStream
					.getFormat());
				if (formats != null && formats.Length > 0)
				{
					javax.sound.sampled.AudioFormat sourceFormat = sourceStream.getFormat();
					javax.sound.sampled.AudioFormat targetFormat = formats[0];
					if (sourceFormat.Equals(targetFormat))
					{
						return sourceStream;
					}
					else
					{
						if (sourceFormat.getEncoding() is org.xiph.speex.spi.SpeexEncoding && targetFormat
							.getEncoding().Equals(javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED))
						{
							return new org.xiph.speex.spi.Speex2PcmAudioInputStream(sourceStream, targetFormat
								, -1);
						}
						else
						{
							if (sourceFormat.getEncoding().Equals(javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED
								) && targetFormat.getEncoding() is org.xiph.speex.spi.SpeexEncoding)
							{
								return new org.xiph.speex.spi.Pcm2SpeexAudioInputStream(sourceStream, targetFormat
									, -1);
							}
							else
							{
								throw new System.ArgumentException("unable to convert " + sourceFormat.ToString()
									 + " to " + targetFormat.ToString());
							}
						}
					}
				}
				else
				{
					throw new System.ArgumentException("target format not found");
				}
			}
			else
			{
				throw new System.ArgumentException("conversion not supported");
			}
		}

		/// <summary>
		/// Obtains an audio input stream with the specified format from the given
		/// audio input stream.
		/// </summary>
		/// <remarks>
		/// Obtains an audio input stream with the specified format from the given
		/// audio input stream.
		/// </remarks>
		/// <param name="targetFormat">- desired data format of the stream after processing.</param>
		/// <param name="sourceStream">- stream from which data to be processed should be read.
		/// 	</param>
		/// <returns>
		/// stream from which processed data with the specified format may be
		/// read.
		/// </returns>
		/// <exception>
		/// IllegalArgumentException
		/// - if the format combination supplied
		/// is not supported.
		/// </exception>
		public override javax.sound.sampled.AudioInputStream getAudioInputStream(javax.sound.sampled.AudioFormat
			 targetFormat, javax.sound.sampled.AudioInputStream sourceStream)
		{
			if (isConversionSupported(targetFormat, sourceStream.getFormat()))
			{
				javax.sound.sampled.AudioFormat[] formats = getTargetFormats(targetFormat.getEncoding
					(), sourceStream.getFormat());
				if (formats != null && formats.Length > 0)
				{
					javax.sound.sampled.AudioFormat sourceFormat = sourceStream.getFormat();
					if (sourceFormat.Equals(targetFormat))
					{
						return sourceStream;
					}
					else
					{
						if (sourceFormat.getEncoding() is org.xiph.speex.spi.SpeexEncoding && targetFormat
							.getEncoding().Equals(javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED))
						{
							return new org.xiph.speex.spi.Speex2PcmAudioInputStream(sourceStream, targetFormat
								, -1);
						}
						else
						{
							if (sourceFormat.getEncoding().Equals(javax.sound.sampled.AudioFormat.Encoding.PCM_SIGNED
								) && targetFormat.getEncoding() is org.xiph.speex.spi.SpeexEncoding)
							{
								return new org.xiph.speex.spi.Pcm2SpeexAudioInputStream(sourceStream, targetFormat
									, -1);
							}
							else
							{
								throw new System.ArgumentException("unable to convert " + sourceFormat.ToString()
									 + " to " + targetFormat.ToString());
							}
						}
					}
				}
				else
				{
					throw new System.ArgumentException("target format not found");
				}
			}
			else
			{
				throw new System.ArgumentException("conversion not supported");
			}
		}
	}
}
