namespace org.xiph.speex.spi
{
	/// <summary>Converts a PCM 16bits/sample mono audio stream to Ogg Speex</summary>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public class Pcm2SpeexAudioInputStream : org.xiph.speex.spi.FilteredAudioInputStream
	{
		/// <summary>The default size of the buffer (UWB stereo requires at least 2560b).</summary>
		/// <remarks>The default size of the buffer (UWB stereo requires at least 2560b).</remarks>
		public const int DEFAULT_BUFFER_SIZE = 2560;

		/// <summary>The default sample rate if none is given in the constructor.</summary>
		/// <remarks>The default sample rate if none is given in the constructor.</remarks>
		public const int DEFAULT_SAMPLERATE = 8000;

		/// <summary>The default number of channels if none is given in the constructor.</summary>
		/// <remarks>The default number of channels if none is given in the constructor.</remarks>
		public const int DEFAULT_CHANNELS = 1;

		/// <summary>The default quality setting for the Speex encoder.</summary>
		/// <remarks>The default quality setting for the Speex encoder.</remarks>
		public const int DEFAULT_QUALITY = 3;

		/// <summary>The default number of Speex frames that will be put in each Ogg packet.</summary>
		/// <remarks>The default number of Speex frames that will be put in each Ogg packet.</remarks>
		public const int DEFAULT_FRAMES_PER_PACKET = 1;

		/// <summary>The default number of Ogg packets that will be put in each Ogg page.</summary>
		/// <remarks>The default number of Ogg packets that will be put in each Ogg page.</remarks>
		public const int DEFAULT_PACKETS_PER_OGG_PAGE = 20;

		/// <summary>Indicates the value is unknown or undetermined.</summary>
		/// <remarks>Indicates the value is unknown or undetermined.</remarks>
		public const int UNKNOWN = -1;

		/// <summary>The Speex Encoder class.</summary>
		/// <remarks>The Speex Encoder class.</remarks>
		private org.xiph.speex.SpeexEncoder encoder;

		/// <summary>The encoder mode (0=NB, 1=WB, 2=UWB).</summary>
		/// <remarks>The encoder mode (0=NB, 1=WB, 2=UWB).</remarks>
		private int mode;

		/// <summary>The size in bytes of PCM data that will be encoded into 1 Speex frame.</summary>
		/// <remarks>The size in bytes of PCM data that will be encoded into 1 Speex frame.</remarks>
		private int frameSize;

		/// <summary>The number of Speex frames that will be put in each Ogg packet.</summary>
		/// <remarks>The number of Speex frames that will be put in each Ogg packet.</remarks>
		private int framesPerPacket;

		/// <summary>The number of audio channels (1=mono, 2=stereo).</summary>
		/// <remarks>The number of audio channels (1=mono, 2=stereo).</remarks>
		private int channels;

		/// <summary>The comment String that will appear in the Ogg comment packet.</summary>
		/// <remarks>The comment String that will appear in the Ogg comment packet.</remarks>
		private string comment = null;

		/// <summary>A counter for the number of PCM samples that have been encoded.</summary>
		/// <remarks>A counter for the number of PCM samples that have been encoded.</remarks>
		private int granulepos;

		/// <summary>A unique serial number that identifies the Ogg stream.</summary>
		/// <remarks>A unique serial number that identifies the Ogg stream.</remarks>
		private int streamSerialNumber;

		/// <summary>The number of Ogg packets that will be put in each Ogg page.</summary>
		/// <remarks>The number of Ogg packets that will be put in each Ogg page.</remarks>
		private int packetsPerOggPage;

		/// <summary>The number of Ogg packets that have been encoded in the current page.</summary>
		/// <remarks>The number of Ogg packets that have been encoded in the current page.</remarks>
		private int packetCount;

		/// <summary>The number of Ogg pages that have been written to the stream.</summary>
		/// <remarks>The number of Ogg pages that have been written to the stream.</remarks>
		private int pageCount;

		/// <summary>Pointer in the buffer to the point where Ogg data is added.</summary>
		/// <remarks>Pointer in the buffer to the point where Ogg data is added.</remarks>
		private int oggCount;

		/// <summary>Flag to indicate if this is the first time a encode method is called.</summary>
		/// <remarks>Flag to indicate if this is the first time a encode method is called.</remarks>
		private bool first;

		/// <summary>Constructor</summary>
		/// <param name="in">the underlying input stream.</param>
		/// <param name="format">the target format of this stream's audio data.</param>
		/// <param name="length">the length in sample frames of the data in this stream.</param>
		public Pcm2SpeexAudioInputStream(java.io.InputStream @in, javax.sound.sampled.AudioFormat
			 format, long length) : this(UNKNOWN, UNKNOWN, @in, format, length, DEFAULT_BUFFER_SIZE
			)
		{
		}

		/// <summary>Constructor</summary>
		/// <param name="mode">the mode of the encoder (0=NB, 1=WB, 2=UWB).</param>
		/// <param name="quality">the quality setting of the encoder (between 0 and 10).</param>
		/// <param name="in">the underlying input stream.</param>
		/// <param name="format">the target format of this stream's audio data.</param>
		/// <param name="length">the length in sample frames of the data in this stream.</param>
		public Pcm2SpeexAudioInputStream(int mode, int quality, java.io.InputStream @in, 
			javax.sound.sampled.AudioFormat format, long length) : this(mode, quality, @in, 
			format, length, DEFAULT_BUFFER_SIZE)
		{
		}

		/// <summary>Constructor</summary>
		/// <param name="in">the underlying input stream.</param>
		/// <param name="format">the target format of this stream's audio data.</param>
		/// <param name="length">the length in sample frames of the data in this stream.</param>
		/// <param name="size">the buffer size.</param>
		/// <exception>
		/// IllegalArgumentException
		/// if size &lt;= 0.
		/// </exception>
		public Pcm2SpeexAudioInputStream(java.io.InputStream @in, javax.sound.sampled.AudioFormat
			 format, long length, int size) : this(UNKNOWN, UNKNOWN, @in, format, length, size
			)
		{
		}

		/// <summary>Constructor</summary>
		/// <param name="mode">the mode of the encoder (0=NB, 1=WB, 2=UWB).</param>
		/// <param name="quality">the quality setting of the encoder (between 0 and 10).</param>
		/// <param name="in">the underlying input stream.</param>
		/// <param name="format">the target format of this stream's audio data.</param>
		/// <param name="length">the length in sample frames of the data in this stream.</param>
		/// <param name="size">the buffer size.</param>
		/// <exception>
		/// IllegalArgumentException
		/// if size &lt;= 0.
		/// </exception>
		public Pcm2SpeexAudioInputStream(int mode, int quality, java.io.InputStream @in, 
			javax.sound.sampled.AudioFormat format, long length, int size) : base(@in, format
			, length, size)
		{
			//  public static final boolean DEFAULT_VBR              = true;
			// .4s of audio
			// Speex variables
			// Ogg variables
			// Ogg initialisation
			granulepos = 0;
			if (streamSerialNumber == 0)
			{
				streamSerialNumber = new java.util.Random().nextInt();
			}
			packetsPerOggPage = DEFAULT_PACKETS_PER_OGG_PAGE;
			packetCount = 0;
			pageCount = 0;
			// Speex initialisation
			framesPerPacket = DEFAULT_FRAMES_PER_PACKET;
			int samplerate = (int)format.getSampleRate();
			if (samplerate < 0)
			{
				samplerate = DEFAULT_SAMPLERATE;
			}
			channels = format.getChannels();
			if (channels < 0)
			{
				channels = DEFAULT_CHANNELS;
			}
			if (mode < 0)
			{
				mode = (samplerate < 12000) ? 0 : ((samplerate < 24000) ? 1 : 2);
			}
			this.mode = mode;
			javax.sound.sampled.AudioFormat.Encoding encoding = format.getEncoding();
			if (quality < 0)
			{
				if (encoding is org.xiph.speex.spi.SpeexEncoding)
				{
					quality = ((org.xiph.speex.spi.SpeexEncoding)encoding).getQuality();
				}
				else
				{
					quality = DEFAULT_QUALITY;
				}
			}
			encoder = new org.xiph.speex.SpeexEncoder();
			encoder.init(mode, quality, samplerate, channels);
			if (encoding is org.xiph.speex.spi.SpeexEncoding && ((org.xiph.speex.spi.SpeexEncoding
				)encoding).isVBR())
			{
				setVbr(true);
			}
			else
			{
				setVbr(false);
			}
			frameSize = 2 * channels * encoder.getFrameSize();
			// Misc initialsation
			comment = "Encoded with " + org.xiph.speex.SpeexEncoder.VERSION;
			first = true;
		}

		/// <summary>Sets the Stream Serial Number.</summary>
		/// <remarks>
		/// Sets the Stream Serial Number.
		/// Must not be changed mid stream.
		/// </remarks>
		/// <param name="serialNumber"></param>
		public virtual void setSerialNumber(int serialNumber)
		{
			if (first)
			{
				this.streamSerialNumber = serialNumber;
			}
		}

		/// <summary>Sets the number of Audio Frames that are to be put in every Speex Packet.
		/// 	</summary>
		/// <remarks>
		/// Sets the number of Audio Frames that are to be put in every Speex Packet.
		/// An Audio Frame contains 160 samples for narrowband, 320 samples for
		/// wideband and 640 samples for ultra-wideband.
		/// </remarks>
		/// <param name="framesPerPacket"></param>
		/// <seealso cref="DEFAULT_FRAMES_PER_PACKET">DEFAULT_FRAMES_PER_PACKET</seealso>
		public virtual void setFramesPerPacket(int framesPerPacket)
		{
			if (framesPerPacket <= 0)
			{
				framesPerPacket = DEFAULT_FRAMES_PER_PACKET;
			}
			this.framesPerPacket = framesPerPacket;
		}

		/// <summary>Sets the number of Speex Packets that are to be put in every Ogg Page.</summary>
		/// <remarks>
		/// Sets the number of Speex Packets that are to be put in every Ogg Page.
		/// This value must be less than 256 as the value is encoded in 1 byte in the
		/// Ogg Header (just before the array of packet sizes)
		/// </remarks>
		/// <param name="packetsPerOggPage"></param>
		/// <seealso cref="DEFAULT_PACKETS_PER_OGG_PAGE">DEFAULT_PACKETS_PER_OGG_PAGE</seealso>
		public virtual void setPacketsPerOggPage(int packetsPerOggPage)
		{
			if (packetsPerOggPage <= 0)
			{
				packetsPerOggPage = DEFAULT_PACKETS_PER_OGG_PAGE;
			}
			if (packetsPerOggPage > 255)
			{
				packetsPerOggPage = 255;
			}
			this.packetsPerOggPage = packetsPerOggPage;
		}

		/// <summary>Sets the comment for the Ogg Comment Header.</summary>
		/// <remarks>Sets the comment for the Ogg Comment Header.</remarks>
		/// <param name="comment"></param>
		/// <param name="appendVersion"></param>
		public virtual void setComment(string comment, bool appendVersion)
		{
			this.comment = comment;
			if (appendVersion)
			{
				this.comment += org.xiph.speex.SpeexEncoder.VERSION;
			}
		}

		/// <summary>Sets the Speex encoder Quality.</summary>
		/// <remarks>Sets the Speex encoder Quality.</remarks>
		/// <param name="quality"></param>
		public virtual void setQuality(int quality)
		{
			encoder.getEncoder().setQuality(quality);
			if (encoder.getEncoder().getVbr())
			{
				encoder.getEncoder().setVbrQuality((float)quality);
			}
		}

		/// <summary>Sets whether of not the encoder is to use VBR.</summary>
		/// <remarks>Sets whether of not the encoder is to use VBR.</remarks>
		/// <param name="vbr"></param>
		public virtual void setVbr(bool vbr)
		{
			encoder.getEncoder().setVbr(vbr);
		}

		/// <summary>Returns the Encoder.</summary>
		/// <remarks>Returns the Encoder.</remarks>
		/// <returns>the Encoder.</returns>
		public virtual org.xiph.speex.Encoder getEncoder()
		{
			return encoder.getEncoder();
		}

		/// <summary>
		/// Fills the buffer with more data, taking into account
		/// shuffling and other tricks for dealing with marks.
		/// </summary>
		/// <remarks>
		/// Fills the buffer with more data, taking into account
		/// shuffling and other tricks for dealing with marks.
		/// Assumes that it is being called by a synchronized method.
		/// This method also assumes that all data has already been read in,
		/// hence pos &gt; count.
		/// </remarks>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		protected override void fill()
		{
			makeSpace();
			if (first)
			{
				writeHeaderFrames();
				first = false;
			}
			while (true)
			{
				if ((prebuf.Length - prepos) < framesPerPacket * frameSize * packetsPerOggPage)
				{
					// grow prebuf
					int nsz = prepos + framesPerPacket * frameSize * packetsPerOggPage;
					byte[] nbuf = new byte[nsz];
					System.Array.Copy(prebuf, 0, nbuf, 0, precount);
					prebuf = nbuf;
				}
				int read = @in.read(prebuf, precount, prebuf.Length - precount);
				if (read < 0)
				{
					// inputstream has ended
					if ((precount - prepos) % 2 != 0)
					{
						// we don't have a complete last PCM sample
						throw new java.io.StreamCorruptedException("Incompleted last PCM sample when stream ended"
							);
					}
					while (prepos < precount)
					{
						// still data to encode
						if ((precount - prepos) < framesPerPacket * frameSize)
						{
							// fill end of frame with zeros
							for (; precount < (prepos + framesPerPacket * frameSize); precount++)
							{
								prebuf[precount] = 0;
							}
						}
						if (packetCount == 0)
						{
							writeOggPageHeader(packetsPerOggPage, 0);
						}
						for (int i = 0; i < framesPerPacket; i++)
						{
							encoder.processData(prebuf, prepos, frameSize);
							prepos += frameSize;
						}
						int size = encoder.getProcessedDataByteSize();
						while ((buf.Length - oggCount) < size)
						{
							// grow buffer
							int nsz = buf.Length * 2;
							byte[] nbuf = new byte[nsz];
							System.Array.Copy(buf, 0, nbuf, 0, oggCount);
							buf = nbuf;
						}
						buf[count + 27 + packetCount] = (byte)(unchecked((int)(0xff)) & size);
						encoder.getProcessedData(buf, oggCount);
						oggCount += size;
						packetCount++;
						if (packetCount >= packetsPerOggPage)
						{
							writeOggPageChecksum();
							return;
						}
					}
					if (packetCount > 0)
					{
						// we have less than the normal number of packets in this page.
						buf[count + 5] = (byte)(unchecked((int)(0xff)) & 4);
						// set page header type to end of stream
						buf[count + 26] = (byte)(unchecked((int)(0xff)) & packetCount);
						System.Array.Copy(buf, count + 27 + packetsPerOggPage, buf, count + 27 + packetCount
							, oggCount - (count + 27 + packetsPerOggPage));
						oggCount -= packetsPerOggPage - packetCount;
						writeOggPageChecksum();
					}
					return;
				}
				else
				{
					if (read > 0)
					{
						precount += read;
						if ((precount - prepos) >= framesPerPacket * frameSize * packetsPerOggPage)
						{
							// enough data to encode frame
							while ((precount - prepos) >= framesPerPacket * frameSize * packetsPerOggPage)
							{
								// lets encode all we can
								if (packetCount == 0)
								{
									writeOggPageHeader(packetsPerOggPage, 0);
								}
								while (packetCount < packetsPerOggPage)
								{
									for (int i = 0; i < framesPerPacket; i++)
									{
										encoder.processData(prebuf, prepos, frameSize);
										prepos += frameSize;
									}
									int size = encoder.getProcessedDataByteSize();
									while ((buf.Length - oggCount) < size)
									{
										// grow buffer
										int nsz = buf.Length * 2;
										byte[] nbuf = new byte[nsz];
										System.Array.Copy(buf, 0, nbuf, 0, oggCount);
										buf = nbuf;
									}
									buf[count + 27 + packetCount] = (byte)(unchecked((int)(0xff)) & size);
									encoder.getProcessedData(buf, oggCount);
									oggCount += size;
									packetCount++;
								}
								if (packetCount >= packetsPerOggPage)
								{
									writeOggPageChecksum();
								}
							}
							System.Array.Copy(prebuf, prepos, prebuf, 0, precount - prepos);
							precount -= prepos;
							prepos = 0;
							// we have encoded some data (all that we could),
							// so we can leave now, otherwise we return to a potentially
							// blocking read of the underlying inputstream.
							return;
						}
					}
					else
					{
						// read == 0
						// read 0 bytes from underlying stream yet it is not finished.
						if (precount >= prebuf.Length)
						{
							// no more room in buffer
							if (prepos > 0)
							{
								// free some space
								System.Array.Copy(prebuf, prepos, prebuf, 0, precount - prepos);
								precount -= prepos;
								prepos = 0;
							}
							else
							{
								// we could grow the pre-buffer but that risks in turn growing the
								// buffer which could lead sooner or later to an
								// OutOfMemoryException. 
								return;
							}
						}
						else
						{
							return;
						}
					}
				}
			}
		}

		/// <summary>
		/// Returns the number of bytes that can be read from this inputstream without
		/// blocking.
		/// </summary>
		/// <remarks>
		/// Returns the number of bytes that can be read from this inputstream without
		/// blocking.
		/// <p>
		/// The <code>available</code> method of <code>FilteredAudioInputStream</code>
		/// returns the sum of the the number of bytes remaining to be read in the
		/// buffer (<code>count - pos</code>) and the result of calling the
		/// <code>available</code> method of the underlying inputstream.
		/// </remarks>
		/// <returns>
		/// the number of bytes that can be read from this inputstream without
		/// blocking.
		/// </returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <seealso cref="FilteredAudioInputStream.@in">FilteredAudioInputStream.@in</seealso>
		/// <exception cref="System.IO.IOException"></exception>
		public override int available()
		{
			lock (this)
			{
				int avail = base.available();
				int unencoded = precount - prepos + @in.available();
				if (encoder.getEncoder().getVbr())
				{
					switch (mode)
					{
						case 0:
						{
							// Narrowband
							// ogg header size = 27 + packetsPerOggPage
							// count 1 byte (min 5 bits) for each block available
							return avail + (27 + 2 * packetsPerOggPage) * (unencoded / (packetsPerOggPage * framesPerPacket
								 * 320));
						}

						case 1:
						{
							// Wideband
							// ogg header size = 27 + packetsPerOggPage
							// count 2 byte (min 9 bits) for each block available
							return avail + (27 + 2 * packetsPerOggPage) * (unencoded / (packetsPerOggPage * framesPerPacket
								 * 640));
						}

						case 2:
						{
							// Ultra wideband
							// ogg header size = 27 + packetsPerOggPage
							// count 2 byte (min 13 bits) for each block available
							return avail + (27 + 3 * packetsPerOggPage) * (unencoded / (packetsPerOggPage * framesPerPacket
								 * 1280));
						}

						default:
						{
							return avail;
							break;
						}
					}
				}
				else
				{
					// Calculate size of a packet of Speex data.
					int spxpacketsize = encoder.getEncoder().getEncodedFrameSize();
					if (channels > 1)
					{
						spxpacketsize += 17;
					}
					// 1+4(14=inband)+4(9=stereo)+8(stereo data)
					spxpacketsize *= framesPerPacket;
					spxpacketsize = (spxpacketsize + 7) >> 3;
					// convert bits to bytes
					// Calculate size of an Ogg packet containing X Speex packets.
					// Ogg Packet = Ogg header + size of each packet + Ogg packets 
					int oggpacketsize = 27 + packetsPerOggPage * (spxpacketsize + 1);
					int pcmframesize;
					switch (mode)
					{
						case 0:
						{
							// size of PCM data necessary to encode 1 Speex packet.
							// Narrowband
							// 1 frame = 20ms = 160ech * channels = 320bytes * channels
							pcmframesize = framesPerPacket * 320 * encoder.getChannels();
							avail += oggpacketsize * (unencoded / (packetsPerOggPage * pcmframesize));
							return avail;
						}

						case 1:
						{
							// Wideband
							// 1 frame = 20ms = 320ech * channels = 640bytes * channels
							pcmframesize = framesPerPacket * 640 * encoder.getChannels();
							avail += oggpacketsize * (unencoded / (packetsPerOggPage * pcmframesize));
							return avail;
						}

						case 2:
						{
							// Ultra wideband
							// 1 frame = 20ms = 640ech * channels = 1280bytes * channels
							pcmframesize = framesPerPacket * 1280 * encoder.getChannels();
							avail += oggpacketsize * (unencoded / (packetsPerOggPage * pcmframesize));
							return avail;
						}

						default:
						{
							return avail;
							break;
						}
					}
				}
			}
		}

		//---------------------------------------------------------------------------
		// Ogg Specific Code
		//---------------------------------------------------------------------------
		/// <summary>Write an OGG page header.</summary>
		/// <remarks>Write an OGG page header.</remarks>
		/// <param name="packets">- the number of packets in the Ogg Page (must be between 1 and 255)
		/// 	</param>
		/// <param name="headertype">- 2=bos: beginning of sream, 4=eos: end of sream</param>
		private void writeOggPageHeader(int packets, int headertype)
		{
			while ((buf.Length - count) < (27 + packets))
			{
				// grow buffer
				int nsz = buf.Length * 2;
				byte[] nbuf = new byte[nsz];
				System.Array.Copy(buf, 0, nbuf, 0, count);
				buf = nbuf;
			}
			org.xiph.speex.AudioFileWriter.writeOggPageHeader(buf, count, headertype, granulepos
				, streamSerialNumber, pageCount++, packets, new byte[packets]);
			oggCount = count + 27 + packets;
		}

		/// <summary>Calculate and write the OGG page checksum.</summary>
		/// <remarks>Calculate and write the OGG page checksum. This now closes the Ogg page.
		/// 	</remarks>
		private void writeOggPageChecksum()
		{
			// write the granulpos
			granulepos += framesPerPacket * frameSize * packetCount / 2;
			org.xiph.speex.AudioFileWriter.writeLong(buf, count + 6, granulepos);
			// write the checksum
			int chksum = org.xiph.speex.OggCrc.checksum(0, buf, count, oggCount - count);
			org.xiph.speex.AudioFileWriter.writeInt(buf, count + 22, chksum);
			// reset variables for a new page.
			count = oggCount;
			packetCount = 0;
		}

		/// <summary>Write the OGG Speex header then the comment header.</summary>
		/// <remarks>Write the OGG Speex header then the comment header.</remarks>
		private void writeHeaderFrames()
		{
			int length = comment.Length;
			if (length > 247)
			{
                comment = cspeex.StringUtil.substring(comment, 0, 247);
				length = 247;
			}
			while ((buf.Length - count) < length + 144)
			{
				// grow buffer (108 = 28 + 80 = size of Ogg Header Frame)
				//             (28 + length + 8 = size of Comment Frame) 
				int nsz = buf.Length * 2;
				byte[] nbuf = new byte[nsz];
				System.Array.Copy(buf, 0, nbuf, 0, count);
				buf = nbuf;
			}
			// writes the OGG header page
			org.xiph.speex.AudioFileWriter.writeOggPageHeader(buf, count, 2, granulepos, streamSerialNumber
				, pageCount++, 1, new byte[] { 80 });
			oggCount = count + 28;
			org.xiph.speex.AudioFileWriter.writeSpeexHeader(buf, oggCount, encoder.getSampleRate
				(), mode, encoder.getChannels(), encoder.getEncoder().getVbr(), framesPerPacket);
			oggCount += 80;
			int chksum = org.xiph.speex.OggCrc.checksum(0, buf, count, oggCount - count);
			org.xiph.speex.AudioFileWriter.writeInt(buf, count + 22, chksum);
			count = oggCount;
			// writes the OGG header page
			org.xiph.speex.AudioFileWriter.writeOggPageHeader(buf, count, 0, granulepos, streamSerialNumber
				, pageCount++, 1, new byte[] { (byte)(length + 8) });
			oggCount = count + 28;
			org.xiph.speex.AudioFileWriter.writeSpeexComment(buf, oggCount, comment);
			oggCount += length + 8;
			chksum = org.xiph.speex.OggCrc.checksum(0, buf, count, oggCount - count);
			org.xiph.speex.AudioFileWriter.writeInt(buf, count + 22, chksum);
			count = oggCount;
			// reset variables for a new page.
			packetCount = 0;
		}
	}
}
