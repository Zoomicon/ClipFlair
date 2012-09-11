namespace org.xiph.speex
{
	/// <summary>Raw Audio File Writer.</summary>
	/// <remarks>Raw Audio File Writer.</remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public class RawWriter : org.xiph.speex.AudioFileWriter
	{
		private java.io.OutputStream @out;

		/// <summary>Closes the output file.</summary>
		/// <remarks>Closes the output file.</remarks>
		/// <exception>
		/// IOException
		/// if there was an exception closing the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void close()
		{
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
            throw new System.NotImplementedException();
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
		}

		// a raw audio file has no header
		/// <summary>Writes a packet of audio.</summary>
		/// <remarks>Writes a packet of audio.</remarks>
		/// <param name="data">audio data</param>
		/// <param name="offset">the offset from which to start reading the data.</param>
		/// <param name="len">the length of data to read.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public override void writePacket(byte[] data, int offset, int len)
		{
			@out.write(data, offset, len);
		}
	}
}
