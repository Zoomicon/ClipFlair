namespace javax.sound.sampled
{
	/// <summary><code>AudioFormat</code> is the class that specifies a particular arrangement of data in a sound stream.
	/// 	</summary>
	/// <remarks>
	/// <code>AudioFormat</code> is the class that specifies a particular arrangement of data in a sound stream.
	/// By examing the information stored in the audio format, you can discover how to interpret the bits in the
	/// binary sound data.
	/// <p>
	/// Every data line has an audio format associated with its data stream. The audio format of a source (playback) data line indicates
	/// what kind of data the data line expects to receive for output.  For a target (capture) data line, the audio format specifies the kind
	/// of the data that can be read from the line.
	/// Sound files also have audio formats, of course.  The <code>
	/// <see cref="AudioFileFormat">AudioFileFormat</see>
	/// </code>
	/// class encapsulates an <code>AudioFormat</code> in addition to other,
	/// file-specific information.  Similarly, an <code>
	/// <see cref="AudioInputStream">AudioInputStream</see>
	/// </code> has an
	/// <code>AudioFormat</code>.
	/// <p>
	/// The <code>AudioFormat</code> class accommodates a number of common sound-file encoding techniques, including
	/// pulse-code modulation (PCM), mu-law encoding, and a-law encoding.  These encoding techniques are predefined,
	/// but service providers can create new encoding types.
	/// The encoding that a specific format uses is named by its <code>encoding</code> field.
	/// <p>
	/// In addition to the encoding, the audio format includes other properties that further specify the exact
	/// arrangement of the data.
	/// These include the number of channels, sample rate, sample size, byte order, frame rate, and frame size.
	/// Sounds may have different numbers of audio channels: one for mono, two for stereo.
	/// The sample rate measures how many "snapshots" (samples) of the sound pressure are taken per second, per channel.
	/// (If the sound is stereo rather than mono, two samples are actually measured at each instant of time: one for the left channel,
	/// and another for the right channel; however, the sample rate still measures the number per channel, so the rate is the same
	/// regardless of the number of channels.   This is the standard use of the term.)
	/// The sample size indicates how many bits are used to store each snapshot; 8 and 16 are typical values.
	/// For 16-bit samples (or any other sample size larger than a byte),
	/// byte order is important; the bytes in each sample are arranged in
	/// either the "little-endian" or "big-endian" style.
	/// For encodings like PCM, a frame consists of the set of samples for all channels at a given
	/// point in time, and so the size of a frame (in bytes) is always equal to the size of a sample (in bytes) times
	/// the number of channels.  However, with some other sorts of encodings a frame can contain
	/// a bundle of compressed data for a whole series of samples, as well as additional, non-sample
	/// data.  For such encodings, the sample rate and sample size refer to the data after it is decoded into PCM,
	/// and so they are completely different from the frame rate and frame size.
	/// <p>An <code>AudioFormat</code> object can include a set of
	/// properties. A property is a pair of key and value: the key
	/// is of type <code>String</code>, the associated property
	/// value is an arbitrary object. Properties specify
	/// additional format specifications, like the bit rate for
	/// compressed formats. Properties are mainly used as a means
	/// to transport additional information of the audio format
	/// to and from the service providers. Therefore, properties
	/// are ignored in the
	/// <see cref="matches(AudioFormat)">matches(AudioFormat)</see>
	/// method.
	/// However, methods which rely on the installed service
	/// providers, like
	/// <see cref="AudioSystem#isConversionSupported(AudioFormat,AudioFormat)">isConversionSupported
	/// 	</see>
	/// may consider
	/// properties, depending on the respective service provider
	/// implementation.
	/// <p>The following table lists some common properties which
	/// service providers should use, if applicable:
	/// <table border=0>
	/// <tr>
	/// <th>Property key</th>
	/// <th>Value type</th>
	/// <th>Description</th>
	/// </tr>
	/// <tr>
	/// <td>&quot;bitrate&quot;</td>
	/// <td>
	/// <see cref="int">int</see>
	/// </td>
	/// <td>average bit rate in bits per second</td>
	/// </tr>
	/// <tr>
	/// <td>&quot;vbr&quot;</td>
	/// <td>
	/// <see cref="bool">bool</see>
	/// </td>
	/// <td><code>true</code>, if the file is encoded in variable bit
	/// rate (VBR)</td>
	/// </tr>
	/// <tr>
	/// <td>&quot;quality&quot;</td>
	/// <td>
	/// <see cref="int">int</see>
	/// </td>
	/// <td>encoding/conversion quality, 1..100</td>
	/// </tr>
	/// </table>
	/// <p>Vendors of service providers (plugins) are encouraged
	/// to seek information about other already established
	/// properties in third party plugins, and follow the same
	/// conventions.
	/// </remarks>
	/// <author>Kara Kytle</author>
	/// <author>Florian Bomers</author>
	/// <version>1.36 05/11/17</version>
	/// <seealso cref="DataLine.getFormat()">DataLine.getFormat()</seealso>
	/// <seealso cref="AudioInputStream.getFormat()">AudioInputStream.getFormat()</seealso>
	/// <seealso cref="AudioFileFormat">AudioFileFormat</seealso>
	/// <seealso cref="javax.sound.sampled.spi.FormatConversionProvider">javax.sound.sampled.spi.FormatConversionProvider
	/// 	</seealso>
	/// <since>1.3</since>
	public class AudioFormat
	{
		/// <summary>The audio encoding technique used by this format.</summary>
		/// <remarks>The audio encoding technique used by this format.</remarks>
		protected AudioFormat.Encoding encoding;

		/// <summary>The number of samples played or recorded per second, for sounds that have this format.
		/// 	</summary>
		/// <remarks>The number of samples played or recorded per second, for sounds that have this format.
		/// 	</remarks>
		protected float sampleRate;

		/// <summary>The number of bits in each sample of a sound that has this format.</summary>
		/// <remarks>The number of bits in each sample of a sound that has this format.</remarks>
		protected int sampleSizeInBits;

		/// <summary>The number of audio channels in this format (1 for mono, 2 for stereo).</summary>
		/// <remarks>The number of audio channels in this format (1 for mono, 2 for stereo).</remarks>
		protected int channels;

		/// <summary>The number of bytes in each frame of a sound that has this format.</summary>
		/// <remarks>The number of bytes in each frame of a sound that has this format.</remarks>
		protected int frameSize;

		/// <summary>The number of frames played or recorded per second, for sounds that have this format.
		/// 	</summary>
		/// <remarks>The number of frames played or recorded per second, for sounds that have this format.
		/// 	</remarks>
		protected float frameRate;

		/// <summary>Indicates whether the audio data is stored in big-endian or little-endian order.
		/// 	</summary>
		/// <remarks>Indicates whether the audio data is stored in big-endian or little-endian order.
		/// 	</remarks>
		protected bool bigEndian;

		/// <summary>The set of properties</summary>
		private System.Collections.Generic.Dictionary<string, object> propertiesField;

		/// <summary>Constructs an <code>AudioFormat</code> with the given parameters.</summary>
		/// <remarks>
		/// Constructs an <code>AudioFormat</code> with the given parameters.
		/// The encoding specifies the convention used to represent the data.
		/// The other parameters are further explained in the
		/// <see cref="AudioFormat">class description</see>
		/// .
		/// </remarks>
		/// <param name="encoding">the audio encoding technique</param>
		/// <param name="sampleRate">the number of samples per second</param>
		/// <param name="sampleSizeInBits">the number of bits in each sample</param>
		/// <param name="channels">the number of channels (1 for mono, 2 for stereo, and so on)
		/// 	</param>
		/// <param name="frameSize">the number of bytes in each frame</param>
		/// <param name="frameRate">the number of frames per second</param>
		/// <param name="bigEndian">
		/// indicates whether the data for a single sample
		/// is stored in big-endian byte order (<code>false</code>
		/// means little-endian)
		/// </param>
		public AudioFormat(AudioFormat.Encoding encoding, float sampleRate, int 
			sampleSizeInBits, int channels, int frameSize, float frameRate, bool bigEndian)
		{
			// INSTANCE VARIABLES
			this.encoding = encoding;
			this.sampleRate = sampleRate;
			this.sampleSizeInBits = sampleSizeInBits;
			this.channels = channels;
			this.frameSize = frameSize;
			this.frameRate = frameRate;
			this.bigEndian = bigEndian;
			this.propertiesField = null;
		}

		/// <summary>Constructs an <code>AudioFormat</code> with the given parameters.</summary>
		/// <remarks>
		/// Constructs an <code>AudioFormat</code> with the given parameters.
		/// The encoding specifies the convention used to represent the data.
		/// The other parameters are further explained in the
		/// <see cref="AudioFormat">class description</see>
		/// .
		/// </remarks>
		/// <param name="encoding">the audio encoding technique</param>
		/// <param name="sampleRate">the number of samples per second</param>
		/// <param name="sampleSizeInBits">the number of bits in each sample</param>
		/// <param name="channels">
		/// the number of channels (1 for mono, 2 for
		/// stereo, and so on)
		/// </param>
		/// <param name="frameSize">the number of bytes in each frame</param>
		/// <param name="frameRate">the number of frames per second</param>
		/// <param name="bigEndian">
		/// indicates whether the data for a single sample
		/// is stored in big-endian byte order
		/// (<code>false</code> means little-endian)
		/// </param>
		/// <param name="properties">
		/// a <code>Map&lt;String,Object&gt;</code> object
		/// containing format properties
		/// </param>
		/// <since>1.5</since>
		public AudioFormat(AudioFormat.Encoding encoding, float sampleRate, int 
			sampleSizeInBits, int channels, int frameSize, float frameRate, bool bigEndian, 
			System.Collections.Generic.IDictionary<string, object> properties) : this(encoding
			, sampleRate, sampleSizeInBits, channels, frameSize, frameRate, bigEndian)
		{
			this.propertiesField = new System.Collections.Generic.Dictionary<string, object>(properties
				);
		}

		/// <summary>
		/// Constructs an <code>AudioFormat</code> with a linear PCM encoding and
		/// the given parameters.
		/// </summary>
		/// <remarks>
		/// Constructs an <code>AudioFormat</code> with a linear PCM encoding and
		/// the given parameters.  The frame size is set to the number of bytes
		/// required to contain one sample from each channel, and the frame rate
		/// is set to the sample rate.
		/// </remarks>
		/// <param name="sampleRate">the number of samples per second</param>
		/// <param name="sampleSizeInBits">the number of bits in each sample</param>
		/// <param name="channels">the number of channels (1 for mono, 2 for stereo, and so on)
		/// 	</param>
		/// <param name="signed">indicates whether the data is signed or unsigned</param>
		/// <param name="bigEndian">
		/// indicates whether the data for a single sample
		/// is stored in big-endian byte order (<code>false</code>
		/// means little-endian)
		/// </param>
		public AudioFormat(float sampleRate, int sampleSizeInBits, int channels, bool signed
			, bool bigEndian) : this((signed == true ? AudioFormat.Encoding.PCM_SIGNED
			 : AudioFormat.Encoding.PCM_UNSIGNED), sampleRate, sampleSizeInBits, channels
			, (channels == AudioSystem.NOT_SPECIFIED || sampleSizeInBits == AudioSystem
			.NOT_SPECIFIED) ? AudioSystem.NOT_SPECIFIED : ((sampleSizeInBits + 7) /
			 8) * channels, sampleRate, bigEndian)
		{
		}

		/// <summary>Obtains the type of encoding for sounds in this format.</summary>
		/// <remarks>Obtains the type of encoding for sounds in this format.</remarks>
		/// <returns>the encoding type</returns>
		/// <seealso cref="Encoding.PCM_SIGNED">Encoding.PCM_SIGNED</seealso>
		/// <seealso cref="Encoding.PCM_UNSIGNED">Encoding.PCM_UNSIGNED</seealso>
		/// <seealso cref="Encoding.ULAW">Encoding.ULAW</seealso>
		/// <seealso cref="Encoding.ALAW">Encoding.ALAW</seealso>
		public virtual AudioFormat.Encoding getEncoding()
		{
			return encoding;
		}

		/// <summary>Obtains the sample rate.</summary>
		/// <remarks>
		/// Obtains the sample rate.
		/// For compressed formats, the return value is the sample rate of the uncompressed
		/// audio data.
		/// When this AudioFormat is used for queries (e.g.
		/// <see cref="AudioSystem#isConversionSupported(AudioFormat,AudioFormat)">AudioSystem.isConversionSupported
		/// 	</see>
		/// ) or capabilities (e.g.
		/// <see cref="Info.getFormats()">DataLine.Info.getFormats</see>
		/// ), a sample rate of
		/// <code>AudioSystem.NOT_SPECIFIED</code> means that any sample rate is
		/// acceptable. <code>AudioSystem.NOT_SPECIFIED</code> is also returned when
		/// the sample rate is not defined for this audio format.
		/// </remarks>
		/// <returns>
		/// the number of samples per second,
		/// or <code>AudioSystem.NOT_SPECIFIED</code>
		/// </returns>
		/// <seealso cref="getFrameRate()">getFrameRate()</seealso>
		/// <seealso cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</seealso>
		public virtual float getSampleRate()
		{
			return sampleRate;
		}

		/// <summary>Obtains the size of a sample.</summary>
		/// <remarks>
		/// Obtains the size of a sample.
		/// For compressed formats, the return value is the sample size of the
		/// uncompressed audio data.
		/// When this AudioFormat is used for queries (e.g.
		/// <see cref="AudioSystem#isConversionSupported(AudioFormat,AudioFormat)">AudioSystem.isConversionSupported
		/// 	</see>
		/// ) or capabilities (e.g.
		/// <see cref="Info.getFormats()">DataLine.Info.getFormats</see>
		/// ), a sample size of
		/// <code>AudioSystem.NOT_SPECIFIED</code> means that any sample size is
		/// acceptable. <code>AudioSystem.NOT_SPECIFIED</code> is also returned when
		/// the sample size is not defined for this audio format.
		/// </remarks>
		/// <returns>
		/// the number of bits in each sample,
		/// or <code>AudioSystem.NOT_SPECIFIED</code>
		/// </returns>
		/// <seealso cref="getFrameSize()">getFrameSize()</seealso>
		/// <seealso cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</seealso>
		public virtual int getSampleSizeInBits()
		{
			return sampleSizeInBits;
		}

		/// <summary>Obtains the number of channels.</summary>
		/// <remarks>
		/// Obtains the number of channels.
		/// When this AudioFormat is used for queries (e.g.
		/// <see cref="AudioSystem#isConversionSupported(AudioFormat,AudioFormat)">AudioSystem.isConversionSupported
		/// 	</see>
		/// ) or capabilities (e.g.
		/// <see cref="Info.getFormats()">DataLine.Info.getFormats</see>
		/// ), a return value of
		/// <code>AudioSystem.NOT_SPECIFIED</code> means that any (positive) number of channels is
		/// acceptable.
		/// </remarks>
		/// <returns>
		/// The number of channels (1 for mono, 2 for stereo, etc.),
		/// or <code>AudioSystem.NOT_SPECIFIED</code>
		/// </returns>
		/// <seealso cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</seealso>
		public virtual int getChannels()
		{
			return channels;
		}

		/// <summary>Obtains the frame size in bytes.</summary>
		/// <remarks>
		/// Obtains the frame size in bytes.
		/// When this AudioFormat is used for queries (e.g.
		/// <see cref="AudioSystem#isConversionSupported(AudioFormat,AudioFormat)">AudioSystem.isConversionSupported
		/// 	</see>
		/// ) or capabilities (e.g.
		/// <see cref="Info.getFormats()">DataLine.Info.getFormats</see>
		/// ), a frame size of
		/// <code>AudioSystem.NOT_SPECIFIED</code> means that any frame size is
		/// acceptable. <code>AudioSystem.NOT_SPECIFIED</code> is also returned when
		/// the frame size is not defined for this audio format.
		/// </remarks>
		/// <returns>
		/// the number of bytes per frame,
		/// or <code>AudioSystem.NOT_SPECIFIED</code>
		/// </returns>
		/// <seealso cref="getSampleSizeInBits()">getSampleSizeInBits()</seealso>
		/// <seealso cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</seealso>
		public virtual int getFrameSize()
		{
			return frameSize;
		}

		/// <summary>Obtains the frame rate in frames per second.</summary>
		/// <remarks>
		/// Obtains the frame rate in frames per second.
		/// When this AudioFormat is used for queries (e.g.
		/// <see cref="AudioSystem#isConversionSupported(AudioFormat,AudioFormat)">AudioSystem.isConversionSupported
		/// 	</see>
		/// ) or capabilities (e.g.
		/// <see cref="Info.getFormats()">DataLine.Info.getFormats</see>
		/// ), a frame rate of
		/// <code>AudioSystem.NOT_SPECIFIED</code> means that any frame rate is
		/// acceptable. <code>AudioSystem.NOT_SPECIFIED</code> is also returned when
		/// the frame rate is not defined for this audio format.
		/// </remarks>
		/// <returns>
		/// the number of frames per second,
		/// or <code>AudioSystem.NOT_SPECIFIED</code>
		/// </returns>
		/// <seealso cref="getSampleRate()">getSampleRate()</seealso>
		/// <seealso cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</seealso>
		public virtual float getFrameRate()
		{
			return frameRate;
		}

		/// <summary>
		/// Indicates whether the audio data is stored in big-endian or little-endian
		/// byte order.
		/// </summary>
		/// <remarks>
		/// Indicates whether the audio data is stored in big-endian or little-endian
		/// byte order.  If the sample size is not more than one byte, the return value is
		/// irrelevant.
		/// </remarks>
		/// <returns>
		/// <code>true</code> if the data is stored in big-endian byte order,
		/// <code>false</code> if little-endian
		/// </returns>
		public virtual bool isBigEndian()
		{
			return bigEndian;
		}

		/// <summary>Obtain an unmodifiable map of properties.</summary>
		/// <remarks>
		/// Obtain an unmodifiable map of properties.
		/// The concept of properties is further explained in
		/// the
		/// <see cref="AudioFileFormat">class description</see>
		/// .
		/// </remarks>
		/// <returns>
		/// a <code>Map&lt;String,Object&gt;</code> object containing
		/// all properties. If no properties are recognized, an empty map is
		/// returned.
		/// </returns>
		/// <seealso cref="getProperty(string)">getProperty(string)</seealso>
		/// <since>1.5</since>
		public virtual System.Collections.Generic.IDictionary<string, object> properties(
			)
		{
			System.Collections.Generic.IDictionary<string, object> ret;
			if (propertiesField == null)
			{
				ret = new System.Collections.Generic.Dictionary<string, object>(0);
			}
			else
			{
				ret = new System.Collections.Generic.Dictionary<string, object>(propertiesField);
			}
			return ret;
		}

		/// <summary>Obtain the property value specified by the key.</summary>
		/// <remarks>
		/// Obtain the property value specified by the key.
		/// The concept of properties is further explained in
		/// the
		/// <see cref="AudioFileFormat">class description</see>
		/// .
		/// <p>If the specified property is not defined for a
		/// particular file format, this method returns
		/// <code>null</code>.
		/// </remarks>
		/// <param name="key">the key of the desired property</param>
		/// <returns>
		/// the value of the property with the specified key,
		/// or <code>null</code> if the property does not exist.
		/// </returns>
		/// <seealso cref="properties">properties</seealso>
		/// <since>1.5</since>
		public virtual object getProperty(string key)
		{
			if (propertiesField == null)
			{
				return null;
			}
			return propertiesField[key];
		}

		/// <summary>Indicates whether this format matches the one specified.</summary>
		/// <remarks>
		/// Indicates whether this format matches the one specified.  To match,
		/// two formats must have the same encoding, the same number of channels,
		/// and the same number of bits per sample and bytes per frame.
		/// The two formats must also have the same sample rate,
		/// unless the specified format has the sample rate value <code>AudioSystem.NOT_SPECIFIED</code>,
		/// which any sample rate will match.  The frame rates must
		/// similarly be equal, unless the specified format has the frame rate
		/// value <code>AudioSystem.NOT_SPECIFIED</code>.  The byte order (big-endian or little-endian)
		/// must match if the sample size is greater than one byte.
		/// </remarks>
		/// <param name="format">format to test for match</param>
		/// <returns>
		/// <code>true</code> if this format matches the one specified,
		/// <code>false</code> otherwise.
		/// </returns>
		public virtual bool matches(AudioFormat format)
		{
			if (format.getEncoding().Equals(getEncoding()) && ((format.getSampleRate() == (float
				)AudioSystem.NOT_SPECIFIED) || (format.getSampleRate() == getSampleRate
				())) && (format.getSampleSizeInBits() == getSampleSizeInBits()) && (format.getChannels
				() == getChannels() && (format.getFrameSize() == getFrameSize()) && ((format.getFrameRate
				() == (float)AudioSystem.NOT_SPECIFIED) || (format.getFrameRate() == getFrameRate
				())) && ((format.getSampleSizeInBits() <= 8) || (format.isBigEndian() == isBigEndian
				()))))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns a string that describes the format, such as:
		/// "PCM SIGNED 22050 Hz 16 bit mono big-endian".
		/// </summary>
		/// <remarks>
		/// Returns a string that describes the format, such as:
		/// "PCM SIGNED 22050 Hz 16 bit mono big-endian".  The contents of the string
		/// may vary between implementations of Java Sound.
		/// </remarks>
		/// <returns>a string that describes the format parameters</returns>
		public override string ToString()
		{
			string sEncoding = string.Empty;
			if (getEncoding() != null)
			{
				sEncoding = getEncoding().ToString() + " ";
			}
			string sSampleRate;
			if (getSampleRate() == (float)AudioSystem.NOT_SPECIFIED)
			{
				sSampleRate = "unknown sample rate, ";
			}
			else
			{
				sSampleRate = string.Empty + getSampleRate() + " Hz, ";
			}
			string sSampleSizeInBits;
			if (getSampleSizeInBits() == (float)AudioSystem.NOT_SPECIFIED)
			{
				sSampleSizeInBits = "unknown bits per sample, ";
			}
			else
			{
				sSampleSizeInBits = string.Empty + getSampleSizeInBits() + " bit, ";
			}
			string sChannels;
			if (getChannels() == 1)
			{
				sChannels = "mono, ";
			}
			else
			{
				if (getChannels() == 2)
				{
					sChannels = "stereo, ";
				}
				else
				{
					if (getChannels() == AudioSystem.NOT_SPECIFIED)
					{
						sChannels = " unknown number of channels, ";
					}
					else
					{
						sChannels = string.Empty + getChannels() + " channels, ";
					}
				}
			}
			string sFrameSize;
			if (getFrameSize() == (float)AudioSystem.NOT_SPECIFIED)
			{
				sFrameSize = "unknown frame size, ";
			}
			else
			{
				sFrameSize = string.Empty + getFrameSize() + " bytes/frame, ";
			}
			string sFrameRate = string.Empty;
			if (System.Math.Abs(getSampleRate() - getFrameRate()) > 0.00001)
			{
				if (getFrameRate() == (float)AudioSystem.NOT_SPECIFIED)
				{
					sFrameRate = "unknown frame rate, ";
				}
				else
				{
					sFrameRate = getFrameRate() + " frames/second, ";
				}
			}
			string sEndian = string.Empty;
			if ((getEncoding().Equals(AudioFormat.Encoding.PCM_SIGNED) || getEncoding
				().Equals(AudioFormat.Encoding.PCM_UNSIGNED)) && ((getSampleSizeInBits(
				) > 8) || (getSampleSizeInBits() == AudioSystem.NOT_SPECIFIED)))
			{
				if (isBigEndian())
				{
					sEndian = "big-endian";
				}
				else
				{
					sEndian = "little-endian";
				}
			}
			return sEncoding + sSampleRate + sSampleSizeInBits + sChannels + sFrameSize + sFrameRate
				 + sEndian;
		}

		/// <summary>
		/// The <code>Encoding</code> class  names the  specific type of data representation
		/// used for an audio stream.
		/// </summary>
		/// <remarks>
		/// The <code>Encoding</code> class  names the  specific type of data representation
		/// used for an audio stream.   The encoding includes aspects of the
		/// sound format other than the number of channels, sample rate, sample size,
		/// frame rate, frame size, and byte order.
		/// <p>
		/// One ubiquitous type of audio encoding is pulse-code modulation (PCM),
		/// which is simply a linear (proportional) representation of the sound
		/// waveform.  With PCM, the number stored in each sample is proportional
		/// to the instantaneous amplitude of the sound pressure at that point in
		/// time.  The numbers are frequently signed or unsigned integers.
		/// Besides PCM, other encodings include mu-law and a-law, which are nonlinear
		/// mappings of the sound amplitude that are often used for recording speech.
		/// <p>
		/// You can use a predefined encoding by referring to one of the static
		/// objects created by this class, such as PCM_SIGNED or
		/// PCM_UNSIGNED.  Service providers can create new encodings, such as
		/// compressed audio formats or floating-point PCM samples, and make
		/// these available through the <code>
		/// <see cref="AudioSystem">AudioSystem</see>
		/// </code> class.
		/// <p>
		/// The <code>Encoding</code> class is static, so that all
		/// <code>AudioFormat</code> objects that have the same encoding will refer
		/// to the same object (rather than different instances of the same class).
		/// This allows matches to be made by checking that two format's encodings
		/// are equal.
		/// </remarks>
		/// <seealso cref="AudioFormat">AudioFormat</seealso>
		/// <seealso cref="javax.sound.sampled.spi.FormatConversionProvider">javax.sound.sampled.spi.FormatConversionProvider
		/// 	</seealso>
		/// <author>Kara Kytle</author>
		/// <version>1.36 05/11/17</version>
		/// <since>1.3</since>
		public class Encoding
		{
			/// <summary>Specifies signed, linear PCM data.</summary>
			/// <remarks>Specifies signed, linear PCM data.</remarks>
			public static readonly AudioFormat.Encoding PCM_SIGNED = new AudioFormat.Encoding
				("PCM_SIGNED");

			/// <summary>Specifies unsigned, linear PCM data.</summary>
			/// <remarks>Specifies unsigned, linear PCM data.</remarks>
			public static readonly AudioFormat.Encoding PCM_UNSIGNED = new AudioFormat.Encoding
				("PCM_UNSIGNED");

			/// <summary>Specifies u-law encoded data.</summary>
			/// <remarks>Specifies u-law encoded data.</remarks>
			public static readonly AudioFormat.Encoding ULAW = new AudioFormat.Encoding
				("ULAW");

			/// <summary>Specifies a-law encoded data.</summary>
			/// <remarks>Specifies a-law encoded data.</remarks>
			public static readonly AudioFormat.Encoding ALAW = new AudioFormat.Encoding
				("ALAW");

			/// <summary>Encoding name.</summary>
			/// <remarks>Encoding name.</remarks>
			private string name;

			/// <summary>Constructs a new encoding.</summary>
			/// <remarks>Constructs a new encoding.</remarks>
			/// <param name="name">the name of the new type of encoding</param>
			public Encoding(string name)
			{
				// ENCODING DEFINES
				// INSTANCE VARIABLES
				// CONSTRUCTOR
				this.name = name;
			}

			// METHODS
			/// <summary>Finalizes the equals method</summary>
			public sealed override bool Equals(object obj)
			{
				if (ToString() == null)
				{
					return (obj != null) && (obj.ToString() == null);
				}
				if (obj is AudioFormat.Encoding)
				{
					return ToString().Equals(obj.ToString());
				}
				return false;
			}

			/// <summary>Finalizes the hashCode method</summary>
			public sealed override int GetHashCode()
			{
				if (ToString() == null)
				{
					return 0;
				}
				return ToString().GetHashCode();
			}

			/// <summary>Provides the <code>String</code> representation of the encoding.</summary>
			/// <remarks>
			/// Provides the <code>String</code> representation of the encoding.  This <code>String</code> is
			/// the same name that was passed to the constructor.  For the predefined encodings, the name
			/// is similar to the encoding's variable (field) name.  For example, <code>PCM_SIGNED.toString()</code> returns
			/// the name "pcm_signed".
			/// </remarks>
			/// <returns>the encoding name</returns>
			public sealed override string ToString()
			{
				return name;
			}
		}
		// class Encoding
	}
}
