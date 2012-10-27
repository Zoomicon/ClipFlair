namespace org.xiph.speex
{
	/// <summary>Ogg Speex Writer</summary>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class OggSpeexWriter : org.xiph.speex.AudioFileWriter
	{
		/// <summary>Number of packets in an Ogg page (must be less than 255)</summary>
		public const int PACKETS_PER_OGG_PAGE = 250;

		/// <summary>The OutputStream</summary>
		private java.io.OutputStream @out;

		/// <summary>Defines the encoder mode (0=NB, 1=WB and 2-UWB).</summary>
		/// <remarks>Defines the encoder mode (0=NB, 1=WB and 2-UWB).</remarks>
		private int mode;

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

		/// <summary>Ogg Stream Serial Number</summary>
		private int streamSerialNumber;

		/// <summary>Data buffer</summary>
		private byte[] dataBuffer;

		/// <summary>Pointer within the Data buffer</summary>
		private int dataBufferPtr;

		/// <summary>Header buffer</summary>
		private byte[] headerBuffer;

		/// <summary>Pointer within the Header buffer</summary>
		private int headerBufferPtr;

		/// <summary>Ogg Page count</summary>
		private int pageCount;

		/// <summary>Speex packet count within an Ogg Page</summary>
		private int packetCount;

		/// <summary>
		/// Absolute granule position
		/// (the number of audio samples from beginning of file to end of Ogg Packet).
		/// </summary>
		/// <remarks>
		/// Absolute granule position
		/// (the number of audio samples from beginning of file to end of Ogg Packet).
		/// </remarks>
		private long granulepos;

		/// <summary>Builds an Ogg Speex Writer.</summary>
		/// <remarks>Builds an Ogg Speex Writer.</remarks>
		public OggSpeexWriter()
		{
			if (streamSerialNumber == 0)
			{
				streamSerialNumber = new java.util.Random().nextInt();
			}
			dataBuffer = new byte[65565];
			dataBufferPtr = 0;
			headerBuffer = new byte[255];
			headerBufferPtr = 0;
			pageCount = 0;
			packetCount = 0;
			granulepos = 0;
		}

		/// <summary>Builds an Ogg Speex Writer.</summary>
		/// <remarks>Builds an Ogg Speex Writer.</remarks>
		/// <param name="mode">the mode of the encoder (0=NB, 1=WB, 2=UWB).</param>
		/// <param name="sampleRate">the number of samples per second.</param>
		/// <param name="channels">the number of audio channels (1=mono, 2=stereo, ...).</param>
		/// <param name="nframes">the number of frames per speex packet.</param>
		/// <param name="vbr"></param>
		public OggSpeexWriter(int mode, int sampleRate, int channels, int nframes, bool vbr
			) : this()
		{
			setFormat(mode, sampleRate, channels, nframes, vbr);
		}

		/// <summary>Sets the output format.</summary>
		/// <remarks>
		/// Sets the output format.
		/// Must be called before WriteHeader().
		/// </remarks>
		/// <param name="mode">the mode of the encoder (0=NB, 1=WB, 2=UWB).</param>
		/// <param name="sampleRate">the number of samples per second.</param>
		/// <param name="channels">the number of audio channels (1=mono, 2=stereo, ...).</param>
		/// <param name="nframes">the number of frames per speex packet.</param>
		/// <param name="vbr"></param>
		private void setFormat(int mode, int sampleRate, int channels, int nframes, bool 
			vbr)
		{
			this.mode = mode;
			this.sampleRate = sampleRate;
			this.channels = channels;
			this.nframes = nframes;
			this.vbr = vbr;
		}

		/// <summary>Sets the Stream Serial Number.</summary>
		/// <remarks>
		/// Sets the Stream Serial Number.
		/// Must not be changed mid stream.
		/// </remarks>
		/// <param name="serialNumber"></param>
		public virtual void setSerialNumber(int serialNumber)
		{
			this.streamSerialNumber = serialNumber;
		}

		/// <summary>Closes the output file.</summary>
		/// <remarks>Closes the output file.</remarks>
		/// <exception>
		/// IOException
		/// if there was an exception closing the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void close()
		{
			flush(true);
			@out.close();
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
			@out = new java.io.FileOutputStream(file);
		}

		/// <summary>Open the output file.</summary>
		/// <remarks>Open the output file.</remarks>
		/// <param name="filename">- file to open.</param>
		/// <exception>
		/// IOException
		/// if there was an exception opening the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void open(string filename)
		{
			open(new java.io.File(filename));
		}

        public override void open(java.io.RandomOutputStream stream)
        {
            @out = stream;
        }

		/// <summary>Writes the header pages that start the Ogg Speex file.</summary>
		/// <remarks>
		/// Writes the header pages that start the Ogg Speex file.
		/// Prepares file for data to be written.
		/// </remarks>
		/// <param name="comment">description to be included in the header.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void writeHeader(string comment)
		{
			int chksum;
			byte[] header;
			byte[] data;
			header = buildOggPageHeader(2, 0, streamSerialNumber, pageCount++, 1, new byte[] 
				{ 80 });
			data = buildSpeexHeader(sampleRate, mode, channels, vbr, nframes);
			chksum = org.xiph.speex.OggCrc.checksum(0, header, 0, header.Length);
			chksum = org.xiph.speex.OggCrc.checksum(chksum, data, 0, data.Length);
			writeInt(header, 22, chksum);
			@out.write(header);
			@out.write(data);
			header = buildOggPageHeader(0, 0, streamSerialNumber, pageCount++, 1, new byte[] 
				{ (byte)(comment.Length + 8) });
			data = buildSpeexComment(comment);
			chksum = org.xiph.speex.OggCrc.checksum(0, header, 0, header.Length);
			chksum = org.xiph.speex.OggCrc.checksum(chksum, data, 0, data.Length);
			writeInt(header, 22, chksum);
			@out.write(header);
			@out.write(data);
		}

		/// <summary>Writes a packet of audio.</summary>
		/// <remarks>Writes a packet of audio.</remarks>
		/// <param name="data">- audio data.</param>
		/// <param name="offset">- the offset from which to start reading the data.</param>
		/// <param name="len">- the length of data to read.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void writePacket(byte[] data, int offset, int len)
		{
			if (len <= 0)
			{
				// nothing to write
				return;
			}
			if (packetCount > PACKETS_PER_OGG_PAGE)
			{
				flush(false);
			}
			System.Array.Copy(data, offset, dataBuffer, dataBufferPtr, len);
			dataBufferPtr += len;
			headerBuffer[headerBufferPtr++] = (byte)len;
			packetCount++;
			granulepos += nframes * (mode == 2 ? 640 : (mode == 1 ? 320 : 160));
		}

		/// <summary>Flush the Ogg page out of the buffers into the file.</summary>
		/// <remarks>Flush the Ogg page out of the buffers into the file.</remarks>
		/// <param name="eos">- end of stream</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		private void flush(bool eos)
		{
			int chksum;
			byte[] header;
			header = buildOggPageHeader((eos ? 4 : 0), granulepos, streamSerialNumber, pageCount
				++, packetCount, headerBuffer);
			chksum = org.xiph.speex.OggCrc.checksum(0, header, 0, header.Length);
			chksum = org.xiph.speex.OggCrc.checksum(chksum, dataBuffer, 0, dataBufferPtr);
			writeInt(header, 22, chksum);
			@out.write(header);
			@out.write(dataBuffer, 0, dataBufferPtr);
			dataBufferPtr = 0;
			headerBufferPtr = 0;
			packetCount = 0;
		}
	}
}
