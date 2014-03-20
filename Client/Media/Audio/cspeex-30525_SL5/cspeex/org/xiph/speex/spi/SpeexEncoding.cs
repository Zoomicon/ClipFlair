namespace org.xiph.speex.spi
{
	/// <summary>Encodings used by the Speex audio decoder.</summary>
	/// <remarks>Encodings used by the Speex audio decoder.</remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public class SpeexEncoding : javax.sound.sampled.AudioFormat.Encoding
	{
		/// <summary>Specifies any Speex encoding.</summary>
		/// <remarks>Specifies any Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX");

		/// <summary>Specifies constant bitrate, quality 0, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 0, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q0 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_0", 0, false);

		/// <summary>Specifies constant bitrate, quality 1, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 1, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q1 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_1", 1, false);

		/// <summary>Specifies constant bitrate, quality 2, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 2, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q2 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_2", 2, false);

		/// <summary>Specifies constant bitrate, quality 3, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 3, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q3 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_3", 3, false);

		/// <summary>Specifies constant bitrate, quality 4, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 4, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q4 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_4", 4, false);

		/// <summary>Specifies constant bitrate, quality 5, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 5, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q5 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_5", 5, false);

		/// <summary>Specifies constant bitrate, quality 6, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 6, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q6 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_6", 6, false);

		/// <summary>Specifies constant bitrate, quality 7, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 7, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q7 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_7", 7, false);

		/// <summary>Specifies constant bitrate, quality 8, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 8, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q8 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_8", 8, false);

		/// <summary>Specifies constant bitrate, quality 9, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 9, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q9 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_9", 9, false);

		/// <summary>Specifies constant bitrate, quality 10, Speex encoding.</summary>
		/// <remarks>Specifies constant bitrate, quality 10, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_Q10 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_quality_10", 10, false);

		/// <summary>Specifies variable bitrate, quality 0, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 0, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR0 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_0", 0, true);

		/// <summary>Specifies variable bitrate, quality 1, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 1, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR1 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_1", 1, true);

		/// <summary>Specifies variable bitrate, quality 2, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 2, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR2 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_2", 2, true);

		/// <summary>Specifies variable bitrate, quality 3, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 3, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR3 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_3", 3, true);

		/// <summary>Specifies variable bitrate, quality 4, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 4, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR4 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_4", 4, true);

		/// <summary>Specifies variable bitrate, quality 5, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 5, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR5 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_5", 5, true);

		/// <summary>Specifies variable bitrate, quality 6, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 6, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR6 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_6", 6, true);

		/// <summary>Specifies variable bitrate, quality 7, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 7, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR7 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_7", 7, true);

		/// <summary>Specifies variable bitrate, quality 8, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 8, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR8 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_8", 8, true);

		/// <summary>Specifies variable bitrate, quality 9, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 9, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR9 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_9", 9, true);

		/// <summary>Specifies variable bitrate, quality 10, Speex encoding.</summary>
		/// <remarks>Specifies variable bitrate, quality 10, Speex encoding.</remarks>
		public static readonly org.xiph.speex.spi.SpeexEncoding SPEEX_VBR10 = new org.xiph.speex.spi.SpeexEncoding
			("SPEEX_VBR_quality_10", 10, true);

		/// <summary>Default quality setting for the Speex encoding.</summary>
		/// <remarks>Default quality setting for the Speex encoding.</remarks>
		public const int DEFAULT_QUALITY = 3;

		/// <summary>Default VBR setting for the Speex encoding.</summary>
		/// <remarks>Default VBR setting for the Speex encoding.</remarks>
		public const bool DEFAULT_VBR = false;

		/// <summary>Quality setting for the Speex encoding.</summary>
		/// <remarks>Quality setting for the Speex encoding.</remarks>
		protected int quality;

		/// <summary>Defines whether or not the encoding is Variable Bit Rate.</summary>
		/// <remarks>Defines whether or not the encoding is Variable Bit Rate.</remarks>
		protected bool vbr;

		/// <summary>Constructs a new encoding.</summary>
		/// <remarks>Constructs a new encoding.</remarks>
		/// <param name="name">- Name of the Speex encoding.</param>
		/// <param name="quality">- Quality setting for the Speex encoding.</param>
		/// <param name="vbr">- Defines whether or not the encoding is Variable Bit Rate.</param>
		public SpeexEncoding(string name, int quality, bool vbr) : base(name)
		{
			this.quality = quality;
			this.vbr = vbr;
		}

		/// <summary>Constructs a new encoding.</summary>
		/// <remarks>Constructs a new encoding.</remarks>
		/// <param name="name">- Name of the Speex encoding.</param>
		public SpeexEncoding(string name) : this(name, DEFAULT_QUALITY, DEFAULT_VBR)
		{
		}

		/// <summary>Returns the quality setting for the Speex encoding.</summary>
		/// <remarks>Returns the quality setting for the Speex encoding.</remarks>
		/// <returns>the quality setting for the Speex encoding.</returns>
		public virtual int getQuality()
		{
			return quality;
		}

		/// <summary>Returns whether or not the encoding is Variable Bit Rate.</summary>
		/// <remarks>Returns whether or not the encoding is Variable Bit Rate.</remarks>
		/// <returns>whether or not the encoding is Variable Bit Rate.</returns>
		public virtual bool isVBR()
		{
			return vbr;
		}
	}
}
