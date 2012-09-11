namespace javax.sound.sampled
{
	/// <summary>
	/// An instance of the <code>AudioFileFormat</code> class describes
	/// an audio file, including the file type, the file's length in bytes,
	/// the length in sample frames of the audio data contained in the file,
	/// and the format of the audio data.
	/// </summary>
	/// <remarks>
	/// An instance of the <code>AudioFileFormat</code> class describes
	/// an audio file, including the file type, the file's length in bytes,
	/// the length in sample frames of the audio data contained in the file,
	/// and the format of the audio data.
	/// <p>
	/// The <code>
	/// <see cref="AudioSystem">AudioSystem</see>
	/// </code> class includes methods for determining the format
	/// of an audio file, obtaining an audio input stream from an audio file, and
	/// writing an audio file from an audio input stream.
	/// <p>An <code>AudioFileFormat</code> object can
	/// include a set of properties. A property is a pair of key and value:
	/// the key is of type <code>String</code>, the associated property
	/// value is an arbitrary object.
	/// Properties specify additional informational
	/// meta data (like a author, copyright, or file duration).
	/// Properties are optional information, and file reader and file
	/// writer implementations are not required to provide or
	/// recognize properties.
	/// <p>The following table lists some common properties that should
	/// be used in implementations:
	/// <table border=1>
	/// <tr>
	/// <th>Property key</th>
	/// <th>Value type</th>
	/// <th>Description</th>
	/// </tr>
	/// <tr>
	/// <td>&quot;duration&quot;</td>
	/// <td>
	/// <see cref="long">long</see>
	/// </td>
	/// <td>playback duration of the file in microseconds</td>
	/// </tr>
	/// <tr>
	/// <td>&quot;author&quot;</td>
	/// <td>
	/// <see cref="string">string</see>
	/// </td>
	/// <td>name of the author of this file</td>
	/// </tr>
	/// <tr>
	/// <td>&quot;title&quot;</td>
	/// <td>
	/// <see cref="string">string</see>
	/// </td>
	/// <td>title of this file</td>
	/// </tr>
	/// <tr>
	/// <td>&quot;copyright&quot;</td>
	/// <td>
	/// <see cref="string">string</see>
	/// </td>
	/// <td>copyright message</td>
	/// </tr>
	/// <tr>
	/// <td>&quot;date&quot;</td>
	/// <td>
	/// <see cref="System.DateTime">DateTime</see>
	/// </td>
	/// <td>date of the recording or release</td>
	/// </tr>
	/// <tr>
	/// <td>&quot;comment&quot;</td>
	/// <td>
	/// <see cref="string">string</see>
	/// </td>
	/// <td>an arbitrary text</td>
	/// </tr>
	/// </table>
	/// </remarks>
	/// <author>David Rivas</author>
	/// <author>Kara Kytle</author>
	/// <author>Florian Bomers</author>
	/// <version>1.24 05/11/17</version>
	/// <seealso cref="AudioInputStream">AudioInputStream</seealso>
	/// <since>1.3</since>
	public class AudioFileFormat
	{
		/// <summary>File type.</summary>
		/// <remarks>File type.</remarks>
		private AudioFileFormat.Type type;

		/// <summary>File length in bytes</summary>
		private int byteLength;

		/// <summary>Format of the audio data contained in the file.</summary>
		/// <remarks>Format of the audio data contained in the file.</remarks>
		private AudioFormat format;

		/// <summary>Audio data length in sample frames</summary>
		private int frameLength;

		/// <summary>The set of properties</summary>
		private System.Collections.Generic.Dictionary<string, object> propertiesField;

		/// <summary>Constructs an audio file format object.</summary>
		/// <remarks>
		/// Constructs an audio file format object.
		/// This protected constructor is intended for use by providers of file-reading
		/// services when returning information about an audio file or about supported audio file
		/// formats.
		/// </remarks>
		/// <param name="type">the type of the audio file</param>
		/// <param name="byteLength">the length of the file in bytes, or <code>AudioSystem.NOT_SPECIFIED</code>
		/// 	</param>
		/// <param name="format">the format of the audio data contained in the file</param>
		/// <param name="frameLength">the audio data length in sample frames, or <code>AudioSystem.NOT_SPECIFIED</code>
		/// 	</param>
		/// <seealso cref="getType()">getType()</seealso>
		protected AudioFileFormat(AudioFileFormat.Type type, int byteLength, AudioFormat
			 format, int frameLength)
		{
			// INSTANCE VARIABLES
			this.type = type;
			this.byteLength = byteLength;
			this.format = format;
			this.frameLength = frameLength;
			this.propertiesField = null;
		}

		/// <summary>Constructs an audio file format object.</summary>
		/// <remarks>
		/// Constructs an audio file format object.
		/// This public constructor may be used by applications to describe the
		/// properties of a requested audio file.
		/// </remarks>
		/// <param name="type">the type of the audio file</param>
		/// <param name="format">the format of the audio data contained in the file</param>
		/// <param name="frameLength">the audio data length in sample frames, or <code>AudioSystem.NOT_SPECIFIED</code>
		/// 	</param>
		public AudioFileFormat(AudioFileFormat.Type type, AudioFormat format
			, int frameLength) : this(type, AudioSystem.NOT_SPECIFIED, format, frameLength
			)
		{
		}

		/// <summary>
		/// Construct an audio file format object with a set of
		/// defined properties.
		/// </summary>
		/// <remarks>
		/// Construct an audio file format object with a set of
		/// defined properties.
		/// This public constructor may be used by applications to describe the
		/// properties of a requested audio file. The properties map
		/// will be copied to prevent any changes to it.
		/// </remarks>
		/// <param name="type">the type of the audio file</param>
		/// <param name="format">the format of the audio data contained in the file</param>
		/// <param name="frameLength">
		/// the audio data length in sample frames, or
		/// <code>AudioSystem.NOT_SPECIFIED</code>
		/// </param>
		/// <param name="properties">
		/// a <code>Map&lt;String,Object&gt;</code> object
		/// with properties
		/// </param>
		/// <since>1.5</since>
		public AudioFileFormat(AudioFileFormat.Type type, AudioFormat format
			, int frameLength, System.Collections.Generic.IDictionary<string, object> properties
			) : this(type, AudioSystem.NOT_SPECIFIED, format, frameLength)
		{
			this.propertiesField = new System.Collections.Generic.Dictionary<string, object>(properties
				);
		}

		/// <summary>Obtains the audio file type, such as <code>WAVE</code> or <code>AU</code>.
		/// 	</summary>
		/// <remarks>Obtains the audio file type, such as <code>WAVE</code> or <code>AU</code>.
		/// 	</remarks>
		/// <returns>the audio file type</returns>
		/// <seealso cref="Type.WAVE">Type.WAVE</seealso>
		/// <seealso cref="Type.AU">Type.AU</seealso>
		/// <seealso cref="Type.AIFF">Type.AIFF</seealso>
		/// <seealso cref="Type.AIFC">Type.AIFC</seealso>
		/// <seealso cref="Type.SND">Type.SND</seealso>
		public virtual AudioFileFormat.Type getType()
		{
			return type;
		}

		/// <summary>Obtains the size in bytes of the entire audio file (not just its audio data).
		/// 	</summary>
		/// <remarks>Obtains the size in bytes of the entire audio file (not just its audio data).
		/// 	</remarks>
		/// <returns>the audio file length in bytes</returns>
		/// <seealso cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</seealso>
		public virtual int getByteLength()
		{
			return byteLength;
		}

		/// <summary>Obtains the format of the audio data contained in the audio file.</summary>
		/// <remarks>Obtains the format of the audio data contained in the audio file.</remarks>
		/// <returns>the audio data format</returns>
		public virtual AudioFormat getFormat()
		{
			return format;
		}

		/// <summary>Obtains the length of the audio data contained in the file, expressed in sample frames.
		/// 	</summary>
		/// <remarks>Obtains the length of the audio data contained in the file, expressed in sample frames.
		/// 	</remarks>
		/// <returns>the number of sample frames of audio data in the file</returns>
		/// <seealso cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</seealso>
		public virtual int getFrameLength()
		{
			return frameLength;
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

		/// <summary>Provides a string representation of the file format.</summary>
		/// <remarks>Provides a string representation of the file format.</remarks>
		/// <returns>the file format as a string</returns>
		public override string ToString()
		{
			System.Text.StringBuilder buf = new System.Text.StringBuilder();
			//$$fb2002-11-01: fix for 4672864: AudioFileFormat.toString() throws unexpected NullPointerException
			if (type != null)
			{
                buf.Append(type.ToString() + " (." + type.getExtension() + ") file");
			}
			else
			{
                buf.Append("unknown file format");
			}
			if (byteLength != AudioSystem.NOT_SPECIFIED)
			{
                buf.Append(", byte length: " + byteLength);
			}
            buf.Append(", data format: " + format);
			if (frameLength != AudioSystem.NOT_SPECIFIED)
			{
                buf.Append(", frame length: " + frameLength);
			}
			return buf.ToString();
		}

		/// <summary>
		/// An instance of the <code>Type</code> class represents one of the
		/// standard types of audio file.
		/// </summary>
		/// <remarks>
		/// An instance of the <code>Type</code> class represents one of the
		/// standard types of audio file.  Static instances are provided for the
		/// common types.
		/// </remarks>
		public class Type
		{
			/// <summary>Specifies a WAVE file.</summary>
			/// <remarks>Specifies a WAVE file.</remarks>
			public static readonly AudioFileFormat.Type WAVE = new AudioFileFormat.Type
				("WAVE", "wav");

			/// <summary>Specifies an AU file.</summary>
			/// <remarks>Specifies an AU file.</remarks>
			public static readonly AudioFileFormat.Type AU = new AudioFileFormat.Type
				("AU", "au");

			/// <summary>Specifies an AIFF file.</summary>
			/// <remarks>Specifies an AIFF file.</remarks>
			public static readonly AudioFileFormat.Type AIFF = new AudioFileFormat.Type
				("AIFF", "aif");

			/// <summary>Specifies an AIFF-C file.</summary>
			/// <remarks>Specifies an AIFF-C file.</remarks>
			public static readonly AudioFileFormat.Type AIFC = new AudioFileFormat.Type
				("AIFF-C", "aifc");

			/// <summary>Specifies a SND file.</summary>
			/// <remarks>Specifies a SND file.</remarks>
			public static readonly AudioFileFormat.Type SND = new AudioFileFormat.Type
				("SND", "snd");

			/// <summary>File type name.</summary>
			/// <remarks>File type name.</remarks>
			private readonly string name;

			/// <summary>File type extension.</summary>
			/// <remarks>File type extension.</remarks>
			private readonly string extension;

			/// <summary>Constructs a file type.</summary>
			/// <remarks>Constructs a file type.</remarks>
			/// <param name="name">the string that names the file type</param>
			/// <param name="extension">
			/// the string that commonly marks the file type
			/// without leading dot.
			/// </param>
			public Type(string name, string extension)
			{
				// FILE FORMAT TYPE DEFINES
				// INSTANCE VARIABLES
				// CONSTRUCTOR
				this.name = name;
				this.extension = extension;
			}

			// METHODS
			/// <summary>Finalizes the equals method</summary>
			public sealed override bool Equals(object obj)
			{
				if (ToString() == null)
				{
					return (obj != null) && (obj.ToString() == null);
				}
				if (obj is AudioFileFormat.Type)
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

			/// <summary>
			/// Provides the file type's name as the <code>String</code> representation
			/// of the file type.
			/// </summary>
			/// <remarks>
			/// Provides the file type's name as the <code>String</code> representation
			/// of the file type.
			/// </remarks>
			/// <returns>the file type's name</returns>
			public sealed override string ToString()
			{
				return name;
			}

			/// <summary>Obtains the common file name extension for this file type.</summary>
			/// <remarks>Obtains the common file name extension for this file type.</remarks>
			/// <returns>file type extension</returns>
			public virtual string getExtension()
			{
				return extension;
			}
		}
		// class Type
	}
}
