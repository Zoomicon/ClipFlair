namespace javax.sound.sampled.spi
{
	/// <summary>
	/// A format conversion provider provides format conversion services
	/// from one or more input formats to one or more output formats.
	/// </summary>
	/// <remarks>
	/// A format conversion provider provides format conversion services
	/// from one or more input formats to one or more output formats.
	/// Converters include codecs, which encode and/or decode audio data,
	/// as well as transcoders, etc.  Format converters provide methods for
	/// determining what conversions are supported and for obtaining an audio
	/// stream from which converted data can be read.
	/// <p>
	/// The source format represents the format of the incoming
	/// audio data, which will be converted.
	/// <p>
	/// The target format represents the format of the processed, converted
	/// audio data.  This is the format of the data that can be read from
	/// the stream returned by one of the <code>getAudioInputStream</code> methods.
	/// </remarks>
	/// <author>Kara Kytle</author>
	/// <version>1.30, 05/11/17</version>
	/// <since>1.3</since>
	public abstract class FormatConversionProvider
	{
		// NEW METHODS
		/// <summary>
		/// Obtains the set of source format encodings from which format
		/// conversion services are provided by this provider.
		/// </summary>
		/// <remarks>
		/// Obtains the set of source format encodings from which format
		/// conversion services are provided by this provider.
		/// </remarks>
		/// <returns>
		/// array of source format encodings.  The array will always
		/// have a length of at least 1.
		/// </returns>
		public abstract AudioFormat.Encoding[] getSourceEncodings();

		/// <summary>
		/// Obtains the set of target format encodings to which format
		/// conversion services are provided by this provider.
		/// </summary>
		/// <remarks>
		/// Obtains the set of target format encodings to which format
		/// conversion services are provided by this provider.
		/// </remarks>
		/// <returns>
		/// array of target format encodings.  The array will always
		/// have a length of at least 1.
		/// </returns>
		public abstract AudioFormat.Encoding[] getTargetEncodings();

		/// <summary>
		/// Indicates whether the format converter supports conversion from the
		/// specified source format encoding.
		/// </summary>
		/// <remarks>
		/// Indicates whether the format converter supports conversion from the
		/// specified source format encoding.
		/// </remarks>
		/// <param name="sourceEncoding">the source format encoding for which support is queried
		/// 	</param>
		/// <returns><code>true</code> if the encoding is supported, otherwise <code>false</code>
		/// 	</returns>
		public virtual bool isSourceEncodingSupported(AudioFormat.Encoding sourceEncoding
			)
		{
			AudioFormat.Encoding[] sourceEncodings = getSourceEncodings();
			for (int i = 0; i < sourceEncodings.Length; i++)
			{
				if (sourceEncoding.Equals(sourceEncodings[i]))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Indicates whether the format converter supports conversion to the
		/// specified target format encoding.
		/// </summary>
		/// <remarks>
		/// Indicates whether the format converter supports conversion to the
		/// specified target format encoding.
		/// </remarks>
		/// <param name="targetEncoding">the target format encoding for which support is queried
		/// 	</param>
		/// <returns><code>true</code> if the encoding is supported, otherwise <code>false</code>
		/// 	</returns>
		public virtual bool isTargetEncodingSupported(AudioFormat.Encoding targetEncoding
			)
		{
			AudioFormat.Encoding[] targetEncodings = getTargetEncodings();
			for (int i = 0; i < targetEncodings.Length; i++)
			{
				if (targetEncoding.Equals(targetEncodings[i]))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Obtains the set of target format encodings supported by the format converter
		/// given a particular source format.
		/// </summary>
		/// <remarks>
		/// Obtains the set of target format encodings supported by the format converter
		/// given a particular source format.
		/// If no target format encodings are supported for this source format,
		/// an array of length 0 is returned.
		/// </remarks>
		/// <returns>array of supported target format encodings.</returns>
		public abstract AudioFormat.Encoding[] getTargetEncodings(AudioFormat
			 sourceFormat);

		/// <summary>
		/// Indicates whether the format converter supports conversion to a particular encoding
		/// from a particular format.
		/// </summary>
		/// <remarks>
		/// Indicates whether the format converter supports conversion to a particular encoding
		/// from a particular format.
		/// </remarks>
		/// <param name="targetEncoding">desired encoding of the outgoing data</param>
		/// <param name="sourceFormat">format of the incoming data</param>
		/// <returns><code>true</code> if the conversion is supported, otherwise <code>false</code>
		/// 	</returns>
		public virtual bool isConversionSupported(AudioFormat.Encoding targetEncoding
			, AudioFormat sourceFormat)
		{
			AudioFormat.Encoding[] targetEncodings = getTargetEncodings(sourceFormat
				);
			for (int i = 0; i < targetEncodings.Length; i++)
			{
				if (targetEncoding.Equals(targetEncodings[i]))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Obtains the set of target formats with the encoding specified
		/// supported by the format converter
		/// If no target formats with the specified encoding are supported
		/// for this source format, an array of length 0 is returned.
		/// </summary>
		/// <remarks>
		/// Obtains the set of target formats with the encoding specified
		/// supported by the format converter
		/// If no target formats with the specified encoding are supported
		/// for this source format, an array of length 0 is returned.
		/// </remarks>
		/// <returns>array of supported target formats.</returns>
		public abstract AudioFormat[] getTargetFormats(AudioFormat.Encoding
			 targetEncoding, AudioFormat sourceFormat);

		/// <summary>
		/// Indicates whether the format converter supports conversion to one
		/// particular format from another.
		/// </summary>
		/// <remarks>
		/// Indicates whether the format converter supports conversion to one
		/// particular format from another.
		/// </remarks>
		/// <param name="targetFormat">desired format of outgoing data</param>
		/// <param name="sourceFormat">format of the incoming data</param>
		/// <returns><code>true</code> if the conversion is supported, otherwise <code>false</code>
		/// 	</returns>
		public virtual bool isConversionSupported(AudioFormat targetFormat, AudioFormat
			 sourceFormat)
		{
			AudioFormat[] targetFormats = getTargetFormats(targetFormat.getEncoding(
				), sourceFormat);
			for (int i = 0; i < targetFormats.Length; i++)
			{
				if (targetFormat.matches(targetFormats[i]))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Obtains an audio input stream with the specified encoding from the given audio
		/// input stream.
		/// </summary>
		/// <remarks>
		/// Obtains an audio input stream with the specified encoding from the given audio
		/// input stream.
		/// </remarks>
		/// <param name="targetEncoding">desired encoding of the stream after processing</param>
		/// <param name="sourceStream">stream from which data to be processed should be read</param>
		/// <returns>stream from which processed data with the specified target encoding may be read
		/// 	</returns>
		/// <exception cref="System.ArgumentException">
		/// if the format combination supplied is
		/// not supported.
		/// </exception>
		public abstract AudioInputStream getAudioInputStream(AudioFormat.Encoding
			 targetEncoding, AudioInputStream sourceStream);

		/// <summary>
		/// Obtains an audio input stream with the specified format from the given audio
		/// input stream.
		/// </summary>
		/// <remarks>
		/// Obtains an audio input stream with the specified format from the given audio
		/// input stream.
		/// </remarks>
		/// <param name="targetFormat">desired data format of the stream after processing</param>
		/// <param name="sourceStream">stream from which data to be processed should be read</param>
		/// <returns>stream from which processed data with the specified format may be read</returns>
		/// <exception cref="System.ArgumentException">
		/// if the format combination supplied is
		/// not supported.
		/// </exception>
		public abstract AudioInputStream getAudioInputStream(AudioFormat
			 targetFormat, AudioInputStream sourceStream);
	}
}
