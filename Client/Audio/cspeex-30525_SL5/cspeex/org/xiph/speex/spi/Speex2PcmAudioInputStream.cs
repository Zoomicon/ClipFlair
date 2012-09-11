namespace org.xiph.speex.spi
{
	/// <summary>Converts an Ogg Speex bitstream into a PCM 16bits/sample audio stream.</summary>
	/// <remarks>Converts an Ogg Speex bitstream into a PCM 16bits/sample audio stream.</remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 160 $</version>
	public class Speex2PcmAudioInputStream : org.xiph.speex.spi.FilteredAudioInputStream
	{
		/// <summary>Flag to indicate if this Stream has been initialised.</summary>
		/// <remarks>Flag to indicate if this Stream has been initialised.</remarks>
		private bool initialised;

		/// <summary>The sample rate of the audio, in samples per seconds (Hz).</summary>
		/// <remarks>The sample rate of the audio, in samples per seconds (Hz).</remarks>
		private int sampleRate;

		/// <summary>The number of audio channels (1=mono, 2=stereo).</summary>
		/// <remarks>The number of audio channels (1=mono, 2=stereo).</remarks>
		private int channelCount;

		/// <summary>Array containing the decoded audio samples.</summary>
		/// <remarks>Array containing the decoded audio samples.</remarks>
		private float[] decodedData;

		/// <summary>Array containing the decoded audio samples converted into bytes.</summary>
		/// <remarks>Array containing the decoded audio samples converted into bytes.</remarks>
		private byte[] outputData;

		/// <summary>Speex bit packing and unpacking class.</summary>
		/// <remarks>Speex bit packing and unpacking class.</remarks>
		private org.xiph.speex.Bits bits;

		/// <summary>Speex Decoder.</summary>
		/// <remarks>Speex Decoder.</remarks>
		private org.xiph.speex.Decoder decoder;

		/// <summary>The frame size, in samples.</summary>
		/// <remarks>The frame size, in samples.</remarks>
		private int frameSize;

		/// <summary>The number of Speex frames that will be put in each Ogg packet.</summary>
		/// <remarks>The number of Speex frames that will be put in each Ogg packet.</remarks>
		private int framesPerPacket;

		/// <summary>A unique serial number that identifies the Ogg stream.</summary>
		/// <remarks>A unique serial number that identifies the Ogg stream.</remarks>
		private int streamSerialNumber;

		/// <summary>The number of Ogg packets that are in each Ogg page.</summary>
		/// <remarks>The number of Ogg packets that are in each Ogg page.</remarks>
		private int packetsPerOggPage;

		/// <summary>The number of Ogg packets that have been decoded in the current page.</summary>
		/// <remarks>The number of Ogg packets that have been decoded in the current page.</remarks>
		private int packetCount;

		/// <summary>Array containing the sizes of Ogg packets in the current page.</summary>
		/// <remarks>Array containing the sizes of Ogg packets in the current page.</remarks>
		private byte[] packetSizes;

		/// <summary>Constructor</summary>
		/// <param name="in">the underlying input stream.</param>
		/// <param name="format">the target format of this stream's audio data.</param>
		/// <param name="length">the length in sample frames of the data in this stream.</param>
		public Speex2PcmAudioInputStream(java.io.InputStream @in, javax.sound.sampled.AudioFormat
			 format, long length) : this(@in, format, length, DEFAULT_BUFFER_SIZE)
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
		public Speex2PcmAudioInputStream(java.io.InputStream @in, javax.sound.sampled.AudioFormat
			 format, long length, int size) : base(@in, format, length, size)
		{
			// InputStream variables
			// audio parameters
			// Speex variables
			// Ogg variables
			bits = new org.xiph.speex.Bits();
			packetSizes = new byte[256];
			initialised = false;
		}

		/// <summary>Initialises the Ogg Speex to PCM InputStream.</summary>
		/// <remarks>
		/// Initialises the Ogg Speex to PCM InputStream.
		/// Read the Ogg Speex header and extract the speex decoder parameters to
		/// initialise the decoder. Then read the Comment header.
		/// Ogg Header description:
		/// <pre>
		/// 0 -  3: capture_pattern
		/// 4: stream_structure_version
		/// 5: header_type_flag (2=bos: beginning of sream)
		/// 6 - 13: absolute granule position
		/// 14 - 17: stream serial number
		/// 18 - 21: page sequence no
		/// 22 - 25: page checksum
		/// 26: page_segments
		/// 27 -...: segment_table
		/// </pre>
		/// Speex Header description
		/// <pre>
		/// 0 -  7: speex_string
		/// 8 - 27: speex_version
		/// 28 - 31: speex_version_id
		/// 32 - 35: header_size
		/// 36 - 39: rate
		/// 40 - 43: mode (0=narrowband, 1=wb, 2=uwb)
		/// 44 - 47: mode_bitstream_version
		/// 48 - 51: nb_channels
		/// 52 - 55: bitrate
		/// 56 - 59: frame_size
		/// 60 - 63: vbr
		/// 64 - 67: frames_per_packet
		/// 68 - 71: extra_headers
		/// 72 - 75: reserved1
		/// 76 - 79: reserved2
		/// </pre>
		/// </remarks>
		/// <param name="blocking">
		/// whether the method should block until initialisation is
		/// successfully completed or not.
		/// </param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		protected virtual void initialise(bool blocking)
		{
			while (!initialised)
			{
				int readsize = prebuf.Length - precount - 1;
				int avail = @in.available();
				if (!blocking && avail <= 0)
				{
					return;
				}
				readsize = (avail > 0 ? System.Math.Min(avail, readsize) : readsize);
				int n = @in.read(prebuf, precount, readsize);
				if (n < 0)
				{
					throw new java.io.StreamCorruptedException("Incomplete Ogg Headers");
				}
				if (n == 0)
				{
				}
				// This should never happen.
				//assert false : "Read 0 bytes from stream - possible infinate loop";
				precount += n;
				if (decoder == null && precount >= 108)
				{
					// we can process the speex header
					if (!(cspeex.StringUtil.getStringForBytes(prebuf, 0, 4).Equals("OggS")))
					{
						throw new java.io.StreamCorruptedException("The given stream does not appear to be Ogg."
							);
					}
					streamSerialNumber = readInt(prebuf, 14);
                    if (!(cspeex.StringUtil.getStringForBytes(prebuf, 28, 8).Equals("Speex   ")))
					{
						throw new java.io.StreamCorruptedException("The given stream does not appear to be Ogg Speex."
							);
					}
					sampleRate = readInt(prebuf, 28 + 36);
					channelCount = readInt(prebuf, 28 + 48);
					framesPerPacket = readInt(prebuf, 28 + 64);
					int mode = readInt(prebuf, 28 + 40);
					switch (mode)
					{
						case 0:
						{
							decoder = new org.xiph.speex.NbDecoder();
							((org.xiph.speex.NbDecoder)decoder).nbinit();
							break;
						}

						case 1:
						{
							decoder = new org.xiph.speex.SbDecoder();
							((org.xiph.speex.SbDecoder)decoder).wbinit();
							break;
						}

						case 2:
						{
							decoder = new org.xiph.speex.SbDecoder();
							((org.xiph.speex.SbDecoder)decoder).uwbinit();
							break;
						}

						default:
						{
							break;
						}
					}
					decoder.setPerceptualEnhancement(true);
					frameSize = decoder.getFrameSize();
					decodedData = new float[frameSize * channelCount];
					outputData = new byte[2 * frameSize * channelCount * framesPerPacket];
					bits.init();
				}
				if (decoder != null && precount >= 108 + 27)
				{
					// we can process the comment (skip them)
					packetsPerOggPage = unchecked((int)(0xff)) & prebuf[108 + 26];
					if (precount >= 108 + 27 + packetsPerOggPage)
					{
						int size = 0;
						for (int i = 0; i < packetsPerOggPage; i++)
						{
							size += unchecked((int)(0xff)) & prebuf[108 + 27 + i];
						}
						if (precount >= 108 + 27 + packetsPerOggPage + size)
						{
							// we have read the complete comment page
							prepos = 108 + 27 + packetsPerOggPage + size;
							packetsPerOggPage = 0;
							packetCount = 255;
							initialised = true;
						}
					}
				}
			}
		}

		/// <summary>
		/// Fills the buffer with more data, taking into account shuffling and other
		/// tricks for dealing with marks.
		/// </summary>
		/// <remarks>
		/// Fills the buffer with more data, taking into account shuffling and other
		/// tricks for dealing with marks.
		/// Assumes that it is being called by a synchronized method.
		/// This method also assumes that all data has already been read in, hence
		/// pos &gt; count.
		/// </remarks>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		protected override void fill()
		{
			makeSpace();
			while (!initialised)
			{
				initialise(true);
			}
			while (true)
			{
				int read = @in.read(prebuf, precount, prebuf.Length - precount);
				if (read < 0)
				{
					// inputstream has ended
					while (prepos < precount)
					{
						// still data to decode
						if (packetCount >= packetsPerOggPage)
						{
							// read new Ogg Page header
							readOggPageHeader();
						}
						if (packetCount < packetsPerOggPage)
						{
							// Ogg Page might be empty (0 packets)
							int n = packetSizes[packetCount++];
							if ((precount - prepos) < n)
							{
								// we don't have enough data for a complete speex frame
								throw new java.io.StreamCorruptedException("Incompleted last Speex packet");
							}
							// do last stuff here
							decode(prebuf, prepos, n);
							prepos += n;
							while ((buf.Length - count) < outputData.Length)
							{
								// grow buffer
								int nsz = buf.Length * 2;
								byte[] nbuf = new byte[nsz];
								System.Array.Copy(buf, 0, nbuf, 0, count);
								buf = nbuf;
							}
							System.Array.Copy(outputData, 0, buf, count, outputData.Length);
							count += outputData.Length;
						}
					}
					return;
				}
				else
				{
					// if read=0 but the prebuffer contains data, it is decoded and returned.
					// if read=0 but the prebuffer is almost empty, it loops back to read.
					if (read >= 0)
					{
						precount += read;
						// do stuff here
						if (packetCount >= packetsPerOggPage)
						{
							// read new Ogg Page header
							readOggPageHeader();
						}
						if (packetCount < packetsPerOggPage)
						{
							// read the next packet
							if ((precount - prepos) >= packetSizes[packetCount])
							{
								// we have enough data, lets start decoding
								while (((precount - prepos) >= packetSizes[packetCount]) && (packetCount < packetsPerOggPage
									))
								{
									// lets decode all we can
									int n = packetSizes[packetCount++];
									decode(prebuf, prepos, n);
									prepos += n;
									while ((buf.Length - count) < outputData.Length)
									{
										// grow buffer
										int nsz = buf.Length * 2;
										byte[] nbuf = new byte[nsz];
										System.Array.Copy(buf, 0, nbuf, 0, count);
										buf = nbuf;
									}
									System.Array.Copy(outputData, 0, buf, count, outputData.Length);
									count += outputData.Length;
									if (packetCount >= packetsPerOggPage)
									{
										// read new Ogg Page header
										readOggPageHeader();
									}
								}
								System.Array.Copy(prebuf, prepos, prebuf, 0, precount - prepos);
								precount -= prepos;
								prepos = 0;
								return;
							}
						}
					}
				}
			}
		}

		// we have decoded some data (all that we could), so we can leave now, otherwise we return to a potentially blocking read of the underlying inputstream.
		/// <summary>This is where the actual decoding takes place.</summary>
		/// <remarks>This is where the actual decoding takes place.</remarks>
		/// <param name="data">the array of data to decode.</param>
		/// <param name="offset">the offset from which to start reading the data.</param>
		/// <param name="len">the length of data to read from the array.</param>
		/// <exception cref="java.io.StreamCorruptedException">
		/// If the input stream not valid Ogg Speex
		/// data.
		/// </exception>
		protected virtual void decode(byte[] data, int offset, int len)
		{
			int i;
			short val;
			int outputSize = 0;
			bits.read_from(data, offset, len);
			for (int frame = 0; frame < framesPerPacket; frame++)
			{
				decoder.decode(bits, decodedData);
				if (channelCount == 2)
				{
					decoder.decodeStereo(decodedData, frameSize);
				}
				for (i = 0; i < frameSize * channelCount; i++)
				{
					if (decodedData[i] > 32767.0f)
					{
						decodedData[i] = 32767.0f;
					}
					else
					{
						if (decodedData[i] < -32768.0f)
						{
							decodedData[i] = -32768.0f;
						}
					}
				}
				for (i = 0; i < frameSize * channelCount; i++)
				{
					val = (decodedData[i] > 0) ? (short)(decodedData[i] + .5) : (short)(decodedData[i
						] - .5);
					outputData[outputSize++] = (byte)(val & unchecked((int)(0xff)));
					outputData[outputSize++] = (byte)((val >> 8) & unchecked((int)(0xff)));
				}
			}
		}

		/// <summary>
		/// See the general contract of the <code>skip</code> method of
		/// <code>InputStream</code>.
		/// </summary>
		/// <remarks>
		/// See the general contract of the <code>skip</code> method of
		/// <code>InputStream</code>.
		/// </remarks>
		/// <param name="n">the number of bytes to be skipped.</param>
		/// <returns>the actual number of bytes skipped.</returns>
		/// <exception>
		/// IOException
		/// if an I/O error occurs.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override long skip(long n)
		{
			lock (this)
			{
				while (!initialised)
				{
					initialise(true);
				}
				checkIfStillOpen();
				// Sanity check
				if (n <= 0)
				{
					return 0;
				}
				// Skip buffered data if there is any
				if (pos < count)
				{
					return base.skip(n);
				}
				else
				{
					// Nothing in the buffers to skip
					int decodedPacketSize = 2 * framesPerPacket * frameSize * channelCount;
					if (markpos < 0 && n >= decodedPacketSize)
					{
						// We aren't buffering and skipping more than a complete Speex packet:
						// Lets try to skip complete Speex packets without decoding
						if (packetCount >= packetsPerOggPage)
						{
							// read new Ogg Page header
							readOggPageHeader();
						}
						if (packetCount < packetsPerOggPage)
						{
							// read the next packet
							int skipped = 0;
							if ((precount - prepos) < packetSizes[packetCount])
							{
								// we don't have enough data
								int avail = @in.available();
								if (avail > 0)
								{
									int size = System.Math.Min(prebuf.Length - precount, avail);
									int read = @in.read(prebuf, precount, size);
									if (read < 0)
									{
										// inputstream has ended
										throw new System.IO.IOException("End of stream but there are still supposed to be packets to decode"
											);
									}
									precount += read;
								}
							}
							while (((precount - prepos) >= packetSizes[packetCount]) && (packetCount < packetsPerOggPage
								) && (n >= decodedPacketSize))
							{
								// lets skip all we can
								prepos += packetSizes[packetCount++];
								skipped += decodedPacketSize;
								n -= decodedPacketSize;
								if (packetCount >= packetsPerOggPage)
								{
									// read new Ogg Page header
									readOggPageHeader();
								}
							}
							System.Array.Copy(prebuf, prepos, prebuf, 0, precount - prepos);
							precount -= prepos;
							prepos = 0;
							return skipped;
						}
					}
					// we have skipped some data (all that we could), so we can leave now, otherwise we return to a potentially blocking read of the underlying inputstream.
					// We are buffering, or couldn't skip a complete Speex packet:
					// Read (decode) into buffers and skip (this is potentially blocking)
					return base.skip(n);
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
		/// buffer (<code>count - pos</code>).
		/// The result of calling the <code>available</code> method of the underlying
		/// inputstream is not used, as this data will have to be filtered, and thus
		/// may not be the same size after processing (although subclasses that do the
		/// filtering should override this method and use the amount of data available
		/// in the underlying inputstream).
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
				if (!initialised)
				{
					initialise(false);
					if (!initialised)
					{
						return 0;
					}
				}
				int avail = base.available();
				if (packetCount >= packetsPerOggPage)
				{
					// read new Ogg Page header
					readOggPageHeader();
				}
				// See how much we could decode from the underlying stream.
				if (packetCount < packetsPerOggPage)
				{
					int undecoded = precount - prepos + @in.available();
					int size = packetSizes[packetCount];
					int tempCount = 0;
					while (size < undecoded && packetCount + tempCount < packetsPerOggPage)
					{
						undecoded -= size;
						avail += 2 * frameSize * framesPerPacket;
						tempCount++;
						size = packetSizes[packetCount + tempCount];
					}
				}
				return avail;
			}
		}

		//---------------------------------------------------------------------------
		// Ogg Specific code
		//---------------------------------------------------------------------------
		/// <summary>Read the Ogg Page header and extract the speex packet sizes.</summary>
		/// <remarks>
		/// Read the Ogg Page header and extract the speex packet sizes.
		/// Note: the checksum is ignores.
		/// Note: the method should no block on a read because it will not read more
		/// then is available
		/// </remarks>
		/// <exception cref="System.IO.IOException"></exception>
		private void readOggPageHeader()
		{
			int packets = 0;
			if (precount - prepos < 27)
			{
				int avail = @in.available();
				if (avail > 0)
				{
					int size = System.Math.Min(prebuf.Length - precount, avail);
					int read = @in.read(prebuf, precount, size);
					if (read < 0)
					{
						// inputstream has ended
						throw new System.IO.IOException("End of stream but available was positive");
					}
					precount += read;
				}
			}
			if (precount - prepos >= 27)
			{
				// can read beginning of Page header
				if (!(cspeex.StringUtil.getStringForBytes(prebuf, prepos, 4).Equals("OggS")))
				{
					throw new java.io.StreamCorruptedException("Lost Ogg Sync");
				}
				if (streamSerialNumber != readInt(prebuf, prepos + 14))
				{
					throw new java.io.StreamCorruptedException("Ogg Stream Serial Number mismatch");
				}
				packets = unchecked((int)(0xff)) & prebuf[prepos + 26];
			}
			if (precount - prepos < 27 + packets)
			{
				int avail = @in.available();
				if (avail > 0)
				{
					int size = System.Math.Min(prebuf.Length - precount, avail);
					int read = @in.read(prebuf, precount, size);
					if (read < 0)
					{
						// inputstream has ended
						throw new System.IO.IOException("End of stream but available was positive");
					}
					precount += read;
				}
			}
			if (precount - prepos >= 27 + packets)
			{
				// can read entire Page header
				System.Array.Copy(prebuf, prepos + 27, packetSizes, 0, packets);
				packetCount = 0;
				prepos += 27 + packets;
				packetsPerOggPage = packets;
			}
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
