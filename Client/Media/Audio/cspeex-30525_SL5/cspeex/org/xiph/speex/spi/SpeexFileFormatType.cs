namespace org.xiph.speex.spi
{
	/// <summary>FileFormatTypes used by the Speex audio decoder.</summary>
	/// <remarks>FileFormatTypes used by the Speex audio decoder.</remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public class SpeexFileFormatType : javax.sound.sampled.AudioFileFormat.Type
	{
		/// <summary>Specifies an OGG Speex file.</summary>
		/// <remarks>Specifies an OGG Speex file.</remarks>
		public static readonly javax.sound.sampled.AudioFileFormat.Type SPEEX = new org.xiph.speex.spi.SpeexFileFormatType
			("SPEEX", "spx");

		/// <summary>Constructs a file type.</summary>
		/// <remarks>Constructs a file type.</remarks>
		/// <param name="name">- the name of the Speex File Format.</param>
		/// <param name="extension">- the file extension for this Speex File Format.</param>
		public SpeexFileFormatType(string name, string extension) : base(name, extension)
		{
		}
	}
}
