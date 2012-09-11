namespace org.xiph.speex
{
	/// <summary>Main Speex Encoder class.</summary>
	/// <remarks>
	/// Main Speex Encoder class.
	/// This class encodes the given PCM 16bit samples into Speex packets.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class SpeexEncoder
	{
		/// <summary>Version of the Speex Encoder</summary>
		public static readonly string VERSION = "Java Speex Encoder v0.9.7 ($Revision: 188 $)";

		private org.xiph.speex.Encoder encoder;

		private org.xiph.speex.Bits bits;

		private float[] rawData;

		private int sampleRate;

		private int channels;

		private int frameSize;

		/// <summary>Constructor</summary>
		public SpeexEncoder()
		{
			bits = new org.xiph.speex.Bits();
		}

		/// <summary>Initialisation</summary>
		/// <param name="mode">the mode of the encoder (0=NB, 1=WB, 2=UWB).</param>
		/// <param name="quality">the quality setting of the encoder (between 0 and 10).</param>
		/// <param name="sampleRate">the number of samples per second.</param>
		/// <param name="channels">the number of audio channels (1=mono, 2=stereo, ...).</param>
		/// <returns>true if initialisation successful.</returns>
		public virtual bool init(int mode, int quality, int sampleRate, int channels)
		{
			switch (mode)
			{
				case 0:
				{
					encoder = new org.xiph.speex.NbEncoder();
					((org.xiph.speex.NbEncoder)encoder).nbinit();
					break;
				}

				case 1:
				{
					//Wideband
					encoder = new org.xiph.speex.SbEncoder();
					((org.xiph.speex.SbEncoder)encoder).wbinit();
					break;
				}

				case 2:
				{
					encoder = new org.xiph.speex.SbEncoder();
					((org.xiph.speex.SbEncoder)encoder).uwbinit();
					break;
				}

				default:
				{
					//*/
					return false;
					break;
				}
			}
			encoder.setQuality(quality);
			this.frameSize = encoder.getFrameSize();
			this.sampleRate = sampleRate;
			this.channels = channels;
			rawData = new float[channels * frameSize];
			bits.init();
			return true;
		}

		/// <summary>Returns the Encoder being used (Narrowband, Wideband or Ultrawideband).</summary>
		/// <remarks>Returns the Encoder being used (Narrowband, Wideband or Ultrawideband).</remarks>
		/// <returns>the Encoder being used (Narrowband, Wideband or Ultrawideband).</returns>
		public virtual org.xiph.speex.Encoder getEncoder()
		{
			return encoder;
		}

		/// <summary>Returns the sample rate.</summary>
		/// <remarks>Returns the sample rate.</remarks>
		/// <returns>the sample rate.</returns>
		public virtual int getSampleRate()
		{
			return sampleRate;
		}

		/// <summary>Returns the number of channels.</summary>
		/// <remarks>Returns the number of channels.</remarks>
		/// <returns>the number of channels.</returns>
		public virtual int getChannels()
		{
			return channels;
		}

		/// <summary>Returns the size of a frame.</summary>
		/// <remarks>Returns the size of a frame.</remarks>
		/// <returns>the size of a frame.</returns>
		public virtual int getFrameSize()
		{
			return frameSize;
		}

		/// <summary>
		/// Pull the decoded data out into a byte array at the given offset
		/// and returns the number of bytes of encoded data just read.
		/// </summary>
		/// <remarks>
		/// Pull the decoded data out into a byte array at the given offset
		/// and returns the number of bytes of encoded data just read.
		/// </remarks>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <returns>the number of bytes of encoded data just read.</returns>
		public virtual int getProcessedData(byte[] data, int offset)
		{
			int size = bits.getBufferSize();
			System.Array.Copy(bits.getBuffer(), 0, data, offset, size);
			bits.init();
			return size;
		}

		/// <summary>Returns the number of bytes of encoded data ready to be read.</summary>
		/// <remarks>Returns the number of bytes of encoded data ready to be read.</remarks>
		/// <returns>the number of bytes of encoded data ready to be read.</returns>
		public virtual int getProcessedDataByteSize()
		{
			return bits.getBufferSize();
		}

		/// <summary>This is where the actual encoding takes place</summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="len"></param>
		/// <returns>true if successful.</returns>
		public virtual bool processData(byte[] data, int offset, int len)
		{
			// converty raw bytes into float samples
			mapPcm16bitLittleEndian2Float(data, offset, rawData, 0, len / 2);
			// encode the bitstream
			return processData(rawData, len / 2);
		}

		/// <summary>Encode an array of shorts.</summary>
		/// <remarks>Encode an array of shorts.</remarks>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="numShorts"></param>
		/// <returns>true if successful.</returns>
		public virtual bool processData(short[] data, int offset, int numShorts)
		{
			int numSamplesRequired = channels * frameSize;
			if (numShorts != numSamplesRequired)
			{
				throw new System.ArgumentException("SpeexEncoder requires " + numSamplesRequired 
					+ " samples to process a Frame, not " + numShorts);
			}
			// convert shorts into float samples,
			for (int i = 0; i < numShorts; i++)
			{
				rawData[i] = (float)data[offset + i];
			}
			// encode the bitstream
			return processData(rawData, numShorts);
		}

		/// <summary>Encode an array of floats.</summary>
		/// <remarks>Encode an array of floats.</remarks>
		/// <param name="data"></param>
		/// <param name="numSamples"></param>
		/// <returns>true if successful.</returns>
		public virtual bool processData(float[] data, int numSamples)
		{
			int numSamplesRequired = channels * frameSize;
			if (numSamples != numSamplesRequired)
			{
				throw new System.ArgumentException("SpeexEncoder requires " + numSamplesRequired 
					+ " samples to process a Frame, not " + numSamples);
			}
			// encode the bitstream
			if (channels == 2)
			{
				org.xiph.speex.Stereo.encode(bits, data, frameSize);
			}
			encoder.encode(bits, data);
			return true;
		}

		/// <summary>
		/// Converts a 16 bit linear PCM stream (in the form of a byte array)
		/// into a floating point PCM stream (in the form of an float array).
		/// </summary>
		/// <remarks>
		/// Converts a 16 bit linear PCM stream (in the form of a byte array)
		/// into a floating point PCM stream (in the form of an float array).
		/// Here are some important details about the encoding:
		/// <ul>
		/// <li> Java uses big endian for shorts and ints, and Windows uses little Endian.
		/// Therefore, shorts and ints must be read as sequences of bytes and
		/// combined with shifting operations.
		/// </ul>
		/// </remarks>
		/// <param name="pcm16bitBytes">- byte array of linear 16-bit PCM formated audio.</param>
		/// <param name="offsetInput"></param>
		/// <param name="samples">- float array to receive the 16-bit linear audio samples.</param>
		/// <param name="offsetOutput"></param>
		/// <param name="length"></param>
		public static void mapPcm16bitLittleEndian2Float(byte[] pcm16bitBytes, int offsetInput
			, float[] samples, int offsetOutput, int length)
		{
			if (pcm16bitBytes.Length - offsetInput < 2 * length)
			{
				throw new System.ArgumentException("Insufficient Samples to convert to floats");
			}
			if (samples.Length - offsetOutput < length)
			{
				throw new System.ArgumentException("Insufficient float buffer to convert the samples"
					);
			}
			for (int i = 0; i < length; i++)
			{
				samples[offsetOutput + i] = (float)(short)((pcm16bitBytes[offsetInput + 2 * i] & unchecked((int)(0xff))) |
                    (pcm16bitBytes[offsetInput + 2 * i + 1] << 8));
			}
		}
		// no & 0xff at the end to keep the sign
	}
}
