namespace org.xiph.speex
{
	/// <summary>Writes basic PCM wave files from binary audio data.</summary>
	/// <remarks>
	/// Writes basic PCM wave files from binary audio data.
	/// <p>Here's an example that writes 2 seconds of silence
	/// <pre>
	/// PcmWaveWriter s_wsw = new PcmWaveWriter(2, 44100);
	/// byte[] silence = new byte[16*2*44100];
	/// wsw.Open("C:\\out.wav");
	/// wsw.WriteHeader();
	/// wsw.WriteData(silence, 0, silence.length);
	/// wsw.WriteData(silence, 0, silence.length);
	/// wsw.Close();
	/// </pre>
	/// </remarks>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class PcmWaveWriter : org.xiph.speex.AudioFileWriter
	{
		/// <summary>Wave type code of PCM</summary>
		public const short WAVE_FORMAT_PCM = (short)unchecked((int)(0x01));

		/// <summary>Wave type code of Speex</summary>
		public const short WAVE_FORMAT_SPEEX = (short)unchecked((short)(0xa109));

		/// <summary>
		/// Table describing the number of frames per packet in a Speex Wave file,
		/// depending on its mode-1 (1=NB, 2=WB, 3=UWB), channels-1 (1=mono, 2=stereo)
		/// and the quality setting (0 to 10).
		/// </summary>
		/// <remarks>
		/// Table describing the number of frames per packet in a Speex Wave file,
		/// depending on its mode-1 (1=NB, 2=WB, 3=UWB), channels-1 (1=mono, 2=stereo)
		/// and the quality setting (0 to 10).
		/// See end of file for exerpt from SpeexACM code for more explanations.
		/// </remarks>
		public static readonly int[][][] WAVE_FRAME_SIZES = new int[][][] { new int[][] { 
			new int[] { 8, 8, 8, 1, 1, 2, 2, 2, 2, 2, 2 }, new int[] { 2, 1, 1, 7, 7, 8, 8, 
			8, 8, 3, 3 } }, new int[][] { new int[] { 8, 8, 8, 2, 1, 1, 2, 2, 2, 2, 2 }, new 
			int[] { 1, 2, 2, 8, 7, 6, 3, 3, 3, 3, 3 } }, new int[][] { new int[] { 8, 8, 8, 
			1, 2, 2, 1, 1, 1, 1, 1 }, new int[] { 2, 1, 1, 7, 8, 3, 6, 6, 5, 5, 5 } } };

		/// <summary>
		/// Table describing the number of bit per Speex frame, depending on its
		/// mode-1 (1=NB, 2=WB, 3=UWB), channels-1 (1=mono, 2=stereo) and the quality
		/// setting (0 to 10).
		/// </summary>
		/// <remarks>
		/// Table describing the number of bit per Speex frame, depending on its
		/// mode-1 (1=NB, 2=WB, 3=UWB), channels-1 (1=mono, 2=stereo) and the quality
		/// setting (0 to 10).
		/// See end of file for exerpt from SpeexACM code for more explanations.
		/// </remarks>
		public static readonly int[][][] WAVE_BITS_PER_FRAME = new int[][][] { new int[][
			] { new int[] { 43, 79, 119, 160, 160, 220, 220, 300, 300, 364, 492 }, new int[]
			 { 60, 96, 136, 177, 177, 237, 237, 317, 317, 381, 509 } }, new int[][] { new int
			[] { 79, 115, 155, 196, 256, 336, 412, 476, 556, 684, 844 }, new int[] { 96, 132
			, 172, 213, 273, 353, 429, 493, 573, 701, 861 } }, new int[][] { new int[] { 83, 
			151, 191, 232, 292, 372, 448, 512, 592, 720, 880 }, new int[] { 100, 168, 208, 249
			, 309, 389, 465, 529, 609, 737, 897 } } };

		private java.io.RandomOutputStream raf;

		/// <summary>Defines the encoder mode (0=NB, 1=WB and 2-UWB).</summary>
		/// <remarks>Defines the encoder mode (0=NB, 1=WB and 2-UWB).</remarks>
		private int mode;

		private int quality;

		/// <summary>Defines the sampling rate of the audio input.</summary>
		/// <remarks>Defines the sampling rate of the audio input.</remarks>
		private int sampleRate;

		/// <summary>Defines the number of channels of the audio input (1=mono, 2=stereo).</summary>
		/// <remarks>Defines the number of channels of the audio input (1=mono, 2=stereo).</remarks>
		private int channels;

		/// <summary>Defines the number of frames per speex packet.</summary>
		/// <remarks>Defines the number of frames per speex packet.</remarks>
		private int nframes;

		/// <summary>Defines whether or not to use VBR (Variable Bit Rate).</summary>
		/// <remarks>Defines whether or not to use VBR (Variable Bit Rate).</remarks>
		private bool vbr;

		private int size;

		private bool isPCM;

		/// <summary>Constructor.</summary>
		/// <remarks>Constructor.</remarks>
		public PcmWaveWriter()
		{
			// NB mono
			// NB stereo
			// WB mono
			// WB stereo
			// UWB mono
			// UWB stereo
			// NB mono
			// NB stereo
			// WB mono
			// WB stereo
			// UWB mono
			// UWB stereo
			size = 0;
		}

		/// <summary>Constructor.</summary>
		/// <remarks>Constructor.</remarks>
		/// <param name="sampleRate">the number of samples per second.</param>
		/// <param name="channels">the number of audio channels (1=mono, 2=stereo, ...).</param>
		public PcmWaveWriter(int sampleRate, int channels) : this()
		{
			setPCMFormat(sampleRate, channels);
		}

		/// <summary>Constructor.</summary>
		/// <remarks>Constructor.</remarks>
		/// <param name="mode">the mode of the encoder (0=NB, 1=WB, 2=UWB).</param>
		/// <param name="quality"></param>
		/// <param name="sampleRate">the number of samples per second.</param>
		/// <param name="channels">the number of audio channels (1=mono, 2=stereo, ...).</param>
		/// <param name="nframes">the number of frames per speex packet.</param>
		/// <param name="vbr"></param>
		public PcmWaveWriter(int mode, int quality, int sampleRate, int channels, int nframes
			, bool vbr) : this()
		{
			setSpeexFormat(mode, quality, sampleRate, channels, nframes, vbr);
		}

		/// <summary>Sets the output format for a PCM Wave file.</summary>
		/// <remarks>
		/// Sets the output format for a PCM Wave file.
		/// Must be called before WriteHeader().
		/// </remarks>
		/// <param name="sampleRate">the number of samples per second.</param>
		/// <param name="channels">the number of audio channels (1=mono, 2=stereo, ...).</param>
		private void setPCMFormat(int sampleRate, int channels)
		{
			this.channels = channels;
			this.sampleRate = sampleRate;
			isPCM = true;
		}

		/// <summary>Sets the output format for a Speex Wave file.</summary>
		/// <remarks>
		/// Sets the output format for a Speex Wave file.
		/// Must be called before WriteHeader().
		/// </remarks>
		/// <param name="mode">the mode of the encoder (0=NB, 1=WB, 2=UWB).</param>
		/// <param name="quality"></param>
		/// <param name="sampleRate">the number of samples per second.</param>
		/// <param name="channels">the number of audio channels (1=mono, 2=stereo, ...).</param>
		/// <param name="nframes">the number of frames per speex packet.</param>
		/// <param name="vbr"></param>
		private void setSpeexFormat(int mode, int quality, int sampleRate, int channels, 
			int nframes, bool vbr)
		{
			this.mode = mode;
			this.quality = quality;
			this.sampleRate = sampleRate;
			this.channels = channels;
			this.nframes = nframes;
			this.vbr = vbr;
			isPCM = false;
		}

		/// <summary>Closes the output file.</summary>
		/// <remarks>
		/// Closes the output file.
		/// MUST be called to have a correct stream.
		/// </remarks>
		/// <exception>
		/// IOException
		/// if there was an exception closing the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void close()
		{
			raf.seek(4);
			int fileLength = (int)raf.length() - 8;
			writeInt(raf, fileLength);
			raf.seek(40);
			writeInt(raf, size);
			raf.close();
		}

		/// <summary>Open the output file.</summary>
		/// <remarks>Open the output file.</remarks>
		/// <param name="file">- file to open.</param>
		/// <exception>
		/// IOException
		/// if there was an exception opening the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void open(java.io.File file)
		{
			file.delete();
			raf = new java.io.RandomOutputStream(new System.IO.FileStream(file.Filename, System.IO.FileMode.Create));
			size = 0;
		}

        public override void open(java.io.RandomOutputStream stream)
        {
            raf = stream;
        }

		/// <summary>Open the output file.</summary>
		/// <remarks>Open the output file.</remarks>
		/// <param name="filename">filename to open.</param>
		/// <exception>
		/// IOException
		/// if there was an exception opening the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void open(string filename)
		{
			open(new java.io.File(filename));
		}

		/// <summary>Writes the initial data chunks that start the wave file.</summary>
		/// <remarks>
		/// Writes the initial data chunks that start the wave file.
		/// Prepares file for data samples to written.
		/// </remarks>
		/// <param name="comment">ignored by the WAV header.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void writeHeader(string comment)
		{
			byte[] chkid = cspeex.StringUtil.getBytesForString("RIFF");
			raf.write(chkid, 0, chkid.Length);
			writeInt(raf, 0);
			chkid = cspeex.StringUtil.getBytesForString("WAVE");
			raf.write(chkid, 0, chkid.Length);
			chkid = cspeex.StringUtil.getBytesForString("fmt ");
			raf.write(chkid, 0, chkid.Length);
			if (isPCM)
			{
				writeInt(raf, 16);
				// Size of format chunk
				writeShort(raf, WAVE_FORMAT_PCM);
				// Format tag: PCM
				writeShort(raf, (short)channels);
				// Number of channels
				writeInt(raf, sampleRate);
				// Sampling frequency
				writeInt(raf, sampleRate * channels * 2);
				// Average bytes per second
				writeShort(raf, (short)(channels * 2));
				// Blocksize of data
				writeShort(raf, (short)16);
			}
			else
			{
				// Bits per sample
				int length = comment.Length;
				writeInt(raf, (short)(18 + 2 + 80 + length));
				// Size of format chunk
				writeShort(raf, WAVE_FORMAT_SPEEX);
				// Format tag: Speex
				writeShort(raf, (short)channels);
				// Number of channels
				writeInt(raf, sampleRate);
				// Sampling frequency
				writeInt(raf, (calculateEffectiveBitrate(mode, channels, quality) + 7) >> 3);
				// Average bytes per second
				writeShort(raf, (short)calculateBlockSize(mode, channels, quality));
				// Blocksize of data
				writeShort(raf, (short)quality);
				// Bits per sample
				writeShort(raf, (short)(2 + 80 + length));
				// The count in bytes of the extra size
				raf.writeByte(unchecked((int)(0xff)) & 1);
				// ACM major version number
				raf.writeByte(unchecked((int)(0xff)) & 0);
				// ACM minor version number
				raf.write(buildSpeexHeader(sampleRate, mode, channels, vbr, nframes));
				raf.writeBytes(comment);
			}
			chkid = cspeex.StringUtil.getBytesForString("data");
			raf.write(chkid, 0, chkid.Length);
			writeInt(raf, 0);
		}

		/// <summary>Writes a packet of audio.</summary>
		/// <remarks>Writes a packet of audio.</remarks>
		/// <param name="data">audio data</param>
		/// <param name="offset">the offset from which to start reading the data.</param>
		/// <param name="len">the length of data to read.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void writePacket(byte[] data, int offset, int len)
		{
			raf.write(data, offset, len);
			size += len;
		}

		/// <summary>Calculates effective bitrate (considering padding).</summary>
		/// <remarks>
		/// Calculates effective bitrate (considering padding).
		/// See end of file for exerpt from SpeexACM code for more explanations.
		/// </remarks>
		/// <param name="mode"></param>
		/// <param name="channels"></param>
		/// <param name="quality"></param>
		/// <returns>effective bitrate (considering padding).</returns>
		private static int calculateEffectiveBitrate(int mode, int channels, int quality)
		{
			return ((((WAVE_FRAME_SIZES[mode - 1][channels - 1][quality] * WAVE_BITS_PER_FRAME
				[mode - 1][channels - 1][quality]) + 7) >> 3) * 50 * 8) / WAVE_BITS_PER_FRAME[mode
				 - 1][channels - 1][quality];
		}

		/// <summary>Calculates block size (considering padding).</summary>
		/// <remarks>
		/// Calculates block size (considering padding).
		/// See end of file for exerpt from SpeexACM code for more explanations.
		/// </remarks>
		/// <param name="mode"></param>
		/// <param name="channels"></param>
		/// <param name="quality"></param>
		/// <returns>block size (considering padding).</returns>
		private static int calculateBlockSize(int mode, int channels, int quality)
		{
			return (((WAVE_FRAME_SIZES[mode - 1][channels - 1][quality] * WAVE_BITS_PER_FRAME
				[mode - 1][channels - 1][quality]) + 7) >> 3);
		}
	}
}
