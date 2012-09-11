namespace org.xiph.speex
{
	/// <summary>Main Speex Decoder class.</summary>
	/// <remarks>
	/// Main Speex Decoder class.
	/// This class decodes the given Speex packets into PCM 16bit samples.
	/// <p>Here's an example that decodes and recovers one Speex packet.
	/// <pre>
	/// SpeexDecoder speexDecoder = new SpeexDecoder();
	/// speexDecoder.processData(data, packetOffset, packetSize);
	/// byte[] decoded = new byte[speexDecoder.getProcessedBataByteSize()];
	/// speexDecoder.getProcessedData(decoded, 0);
	/// </pre>
	/// </remarks>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class SpeexDecoder
	{
		/// <summary>Version of the Speex Decoder</summary>
		public static readonly string VERSION = "Java Speex Decoder v0.9.7 ($Revision: 188 $)";

		private int sampleRate;

		private int channels;

		private float[] decodedData;

		private short[] outputData;

		private int outputSize;

		private org.xiph.speex.Bits bits;

		private org.xiph.speex.Decoder decoder;

		private int frameSize;

		/// <summary>Constructor</summary>
		public SpeexDecoder()
		{
			bits = new org.xiph.speex.Bits();
			sampleRate = 0;
			channels = 0;
		}

		/// <summary>Initialise the Speex Decoder.</summary>
		/// <remarks>Initialise the Speex Decoder.</remarks>
		/// <param name="mode">the mode of the decoder (0=NB, 1=WB, 2=UWB).</param>
		/// <param name="sampleRate">the number of samples per second.</param>
		/// <param name="channels">the number of audio channels (1=mono, 2=stereo, ...).</param>
		/// <param name="enhanced">whether to enable perceptual enhancement or not.</param>
		/// <returns>true if initialisation successful.</returns>
		public virtual bool init(int mode, int sampleRate, int channels, bool enhanced)
		{
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
					//Wideband
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
					//*/
					return false;
					break;
				}
			}
			decoder.setPerceptualEnhancement(enhanced);
			this.frameSize = decoder.getFrameSize();
			this.sampleRate = sampleRate;
			this.channels = channels;
			int secondSize = sampleRate * channels;
			decodedData = new float[secondSize * 2];
			outputData = new short[secondSize * 2];
			outputSize = 0;
			bits.init();
			return true;
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

		/// <summary>
		/// Pull the decoded data out into a byte array at the given offset
		/// and returns the number of bytes processed and just read.
		/// </summary>
		/// <remarks>
		/// Pull the decoded data out into a byte array at the given offset
		/// and returns the number of bytes processed and just read.
		/// </remarks>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <returns>the number of bytes processed and just read.</returns>
		public virtual int getProcessedData(byte[] data, int offset)
		{
			if (outputSize <= 0)
			{
				return outputSize;
			}
			for (int i = 0; i < outputSize; i++)
			{
				int dx = offset + (i << 1);
				data[dx] = (byte)(outputData[i] & unchecked((int)(0xff)));
				data[dx + 1] = (byte)((outputData[i] >> 8) & unchecked((int)(0xff)));
			}
			int size = outputSize * 2;
			outputSize = 0;
			return size;
		}

		/// <summary>
		/// Pull the decoded data out into a short array at the given offset
		/// and returns tne number of shorts processed and just read
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <returns>the number of samples processed and just read.</returns>
		public virtual int getProcessedData(short[] data, int offset)
		{
			if (outputSize <= 0)
			{
				return outputSize;
			}
			System.Array.Copy(outputData, 0, data, offset, outputSize);
			int size = outputSize;
			outputSize = 0;
			return size;
		}

		/// <summary>Returns the number of bytes processed and ready to be read.</summary>
		/// <remarks>Returns the number of bytes processed and ready to be read.</remarks>
		/// <returns>the number of bytes processed and ready to be read.</returns>
		public virtual int getProcessedDataByteSize()
		{
			return (outputSize * 2);
		}

		/// <summary>This is where the actual decoding takes place</summary>
		/// <param name="data">
		/// - the Speex data (frame) to decode.
		/// If it is null, the packet is supposed lost.
		/// </param>
		/// <param name="offset">- the offset from which to start reading the data.</param>
		/// <param name="len">- the length of data to read (Speex frame size).</param>
		/// <exception cref="java.io.StreamCorruptedException">If the input stream is invalid.
		/// 	</exception>
		public virtual void processData(byte[] data, int offset, int len)
		{
			if (data == null)
			{
				processData(true);
			}
			else
			{
				bits.read_from(data, offset, len);
				processData(false);
			}
		}

		/// <summary>This is where the actual decoding takes place.</summary>
		/// <remarks>This is where the actual decoding takes place.</remarks>
		/// <param name="lost">- true if the Speex packet has been lost.</param>
		/// <exception cref="java.io.StreamCorruptedException">If the input stream is invalid.
		/// 	</exception>
		public virtual void processData(bool lost)
		{
			int i;
			if (lost)
			{
				decoder.decode(null, decodedData);
			}
			else
			{
				decoder.decode(bits, decodedData);
			}
			if (channels == 2)
			{
				decoder.decodeStereo(decodedData, frameSize);
			}
			for (i = 0; i < frameSize * channels; i++)
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
			for (i = 0; i < frameSize * channels; i++, outputSize++)
			{
				outputData[outputSize] = (decodedData[i] > 0) ? (short)(decodedData[i] + 0.5f) : 
					(short)(decodedData[i] - 0.5f);
			}
		}
	}
}
