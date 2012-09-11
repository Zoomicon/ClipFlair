namespace org.xiph.speex
{
	/// <summary>Abstract Class that defines an Audio File Writer.</summary>
	/// <remarks>Abstract Class that defines an Audio File Writer.</remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public abstract class AudioFileWriter
	{
		/// <summary>Closes the output file.</summary>
		/// <remarks>Closes the output file.</remarks>
		/// <exception>
		/// IOException
		/// if there was an exception closing the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public abstract void close();

		/// <summary>Open the output file.</summary>
		/// <remarks>Open the output file.</remarks>
		/// <param name="file">- file to open.</param>
		/// <exception>
		/// IOException
		/// if there was an exception opening the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public abstract void open(java.io.File file);

		/// <summary>Open the output file.</summary>
		/// <remarks>Open the output file.</remarks>
		/// <param name="filename">- file to open.</param>
		/// <exception>
		/// IOException
		/// if there was an exception opening the Audio Writer.
		/// </exception>
		/// <exception cref="System.IO.IOException"></exception>
		public abstract void open(string filename);

        public abstract void open(java.io.RandomOutputStream stream);

		/// <summary>Writes the header pages that start the Ogg Speex file.</summary>
		/// <remarks>
		/// Writes the header pages that start the Ogg Speex file.
		/// Prepares file for data to be written.
		/// </remarks>
		/// <param name="comment">description to be included in the header.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public abstract void writeHeader(string comment);

		/// <summary>Writes a packet of audio.</summary>
		/// <remarks>Writes a packet of audio.</remarks>
		/// <param name="data">audio data</param>
		/// <param name="offset">the offset from which to start reading the data.</param>
		/// <param name="len">the length of data to read.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public abstract void writePacket(byte[] data, int offset, int len);

		/// <summary>Writes an Ogg Page Header to the given byte array.</summary>
		/// <remarks>
		/// Writes an Ogg Page Header to the given byte array.
		/// Ogg Page Header structure:
		/// <pre>
		/// 0 -  3: capture_pattern
		/// 4: stream_structure_version
		/// 5: header_type_flag
		/// 6 - 13: absolute granule position
		/// 14 - 17: stream serial number
		/// 18 - 21: page sequence no
		/// 22 - 25: page checksum
		/// 26: page_segments
		/// 27 -  x: segment_table
		/// </pre>
		/// </remarks>
		/// <param name="buf">the buffer to write to.</param>
		/// <param name="offset">the from which to start writing.</param>
		/// <param name="headerType">
		/// the header type flag
		/// (0=normal, 2=bos: beginning of stream, 4=eos: end of stream).
		/// </param>
		/// <param name="granulepos">the absolute granule position.</param>
		/// <param name="streamSerialNumber"></param>
		/// <param name="pageCount"></param>
		/// <param name="packetCount"></param>
		/// <param name="packetSizes"></param>
		/// <returns>the amount of data written to the buffer.</returns>
		public static int writeOggPageHeader(byte[] buf, int offset, int headerType, long
			 granulepos, int streamSerialNumber, int pageCount, int packetCount, byte[] packetSizes
			)
		{
			writeString(buf, offset, "OggS");
			//  0 -  3: capture_pattern
			buf[offset + 4] = 0;
			//       4: stream_structure_version
			buf[offset + 5] = (byte)headerType;
			//       5: header_type_flag
			writeLong(buf, offset + 6, granulepos);
			//  6 - 13: absolute granule position
			writeInt(buf, offset + 14, streamSerialNumber);
			// 14 - 17: stream serial number
			writeInt(buf, offset + 18, pageCount);
			// 18 - 21: page sequence no
			writeInt(buf, offset + 22, 0);
			// 22 - 25: page checksum
			buf[offset + 26] = (byte)packetCount;
			//      26: page_segments
			System.Array.Copy(packetSizes, 0, buf, offset + 27, packetCount);
			// 27 -  x: segment_table
			return packetCount + 27;
		}

		/// <summary>Builds and returns an Ogg Page Header.</summary>
		/// <remarks>Builds and returns an Ogg Page Header.</remarks>
		/// <param name="headerType">
		/// the header type flag
		/// (0=normal, 2=bos: beginning of stream, 4=eos: end of stream).
		/// </param>
		/// <param name="granulepos">the absolute granule position.</param>
		/// <param name="streamSerialNumber"></param>
		/// <param name="pageCount"></param>
		/// <param name="packetCount"></param>
		/// <param name="packetSizes"></param>
		/// <returns>an Ogg Page Header.</returns>
		public static byte[] buildOggPageHeader(int headerType, long granulepos, int streamSerialNumber
			, int pageCount, int packetCount, byte[] packetSizes)
		{
			byte[] data = new byte[packetCount + 27];
			writeOggPageHeader(data, 0, headerType, granulepos, streamSerialNumber, pageCount
				, packetCount, packetSizes);
			return data;
		}

		/// <summary>Writes a Speex Header to the given byte array.</summary>
		/// <remarks>
		/// Writes a Speex Header to the given byte array.
		/// Speex Header structure:
		/// <pre>
		/// 0 -  7: speex_string
		/// 8 - 27: speex_version
		/// 28 - 31: speex_version_id
		/// 32 - 35: header_size
		/// 36 - 39: rate
		/// 40 - 43: mode (0=NB, 1=WB, 2=UWB)
		/// 44 - 47: mode_bitstream_version
		/// 48 - 51: nb_channels
		/// 52 - 55: bitrate
		/// 56 - 59: frame_size (NB=160, WB=320, UWB=640)
		/// 60 - 63: vbr
		/// 64 - 67: frames_per_packet
		/// 68 - 71: extra_headers
		/// 72 - 75: reserved1
		/// 76 - 79: reserved2
		/// </pre>
		/// </remarks>
		/// <param name="buf">the buffer to write to.</param>
		/// <param name="offset">the from which to start writing.</param>
		/// <param name="sampleRate"></param>
		/// <param name="mode"></param>
		/// <param name="channels"></param>
		/// <param name="vbr"></param>
		/// <param name="nframes"></param>
		/// <returns>the amount of data written to the buffer.</returns>
		public static int writeSpeexHeader(byte[] buf, int offset, int sampleRate, int mode
			, int channels, bool vbr, int nframes)
		{
			writeString(buf, offset, "Speex   ");
			//  0 -  7: speex_string
			writeString(buf, offset + 8, "speex-1.0");
			//  8 - 27: speex_version
			System.Array.Copy(new byte[11], 0, buf, offset + 17, 11);
			// : speex_version (fill in up to 20 bytes)
			writeInt(buf, offset + 28, 1);
			// 28 - 31: speex_version_id
			writeInt(buf, offset + 32, 80);
			// 32 - 35: header_size
			writeInt(buf, offset + 36, sampleRate);
			// 36 - 39: rate
			writeInt(buf, offset + 40, mode);
			// 40 - 43: mode (0=NB, 1=WB, 2=UWB)
			writeInt(buf, offset + 44, 4);
			// 44 - 47: mode_bitstream_version
			writeInt(buf, offset + 48, channels);
			// 48 - 51: nb_channels
			writeInt(buf, offset + 52, -1);
			// 52 - 55: bitrate
			writeInt(buf, offset + 56, 160 << mode);
			// 56 - 59: frame_size (NB=160, WB=320, UWB=640)
			writeInt(buf, offset + 60, vbr ? 1 : 0);
			// 60 - 63: vbr
			writeInt(buf, offset + 64, nframes);
			// 64 - 67: frames_per_packet
			writeInt(buf, offset + 68, 0);
			// 68 - 71: extra_headers
			writeInt(buf, offset + 72, 0);
			// 72 - 75: reserved1
			writeInt(buf, offset + 76, 0);
			// 76 - 79: reserved2
			return 80;
		}

		/// <summary>Builds a Speex Header.</summary>
		/// <remarks>Builds a Speex Header.</remarks>
		/// <param name="sampleRate"></param>
		/// <param name="mode"></param>
		/// <param name="channels"></param>
		/// <param name="vbr"></param>
		/// <param name="nframes"></param>
		/// <returns>a Speex Header.</returns>
		public static byte[] buildSpeexHeader(int sampleRate, int mode, int channels, bool
			 vbr, int nframes)
		{
			byte[] data = new byte[80];
			writeSpeexHeader(data, 0, sampleRate, mode, channels, vbr, nframes);
			return data;
		}

		/// <summary>Writes a Speex Comment to the given byte array.</summary>
		/// <remarks>Writes a Speex Comment to the given byte array.</remarks>
		/// <param name="buf">the buffer to write to.</param>
		/// <param name="offset">the from which to start writing.</param>
		/// <param name="comment">the comment.</param>
		/// <returns>the amount of data written to the buffer.</returns>
		public static int writeSpeexComment(byte[] buf, int offset, string comment)
		{
			int length = comment.Length;
			writeInt(buf, offset, length);
			// vendor comment size
			writeString(buf, offset + 4, comment);
			// vendor comment
			writeInt(buf, offset + length + 4, 0);
			// user comment list length
			return length + 8;
		}

		/// <summary>Builds and returns a Speex Comment.</summary>
		/// <remarks>Builds and returns a Speex Comment.</remarks>
		/// <param name="comment">the comment.</param>
		/// <returns>a Speex Comment.</returns>
		public static byte[] buildSpeexComment(string comment)
		{
			byte[] data = new byte[comment.Length + 8];
			writeSpeexComment(data, 0, comment);
			return data;
		}

		/// <summary>Writes a Little-endian short.</summary>
		/// <remarks>Writes a Little-endian short.</remarks>
		/// <param name="out">the data output to write to.</param>
		/// <param name="v">value to write.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public static void writeShort(java.io.DataOutput @out, short v)
		{
            if (System.BitConverter.IsLittleEndian)
            {
                @out.writeShort(v);
            }
            else
            {
                @out.writeByte((unchecked((int)(0xff)) & v));
                @out.writeByte((unchecked((int)(0xff)) & ((v) >> (8 & 0x1f))));
            }
		}

		/// <summary>Writes a Little-endian int.</summary>
		/// <remarks>Writes a Little-endian int.</remarks>
		/// <param name="out">the data output to write to.</param>
		/// <param name="v">value to write.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public static void writeInt(java.io.DataOutput @out, int v)
		{
            if (System.BitConverter.IsLittleEndian)
            {
                @out.writeInt(v);
            }
            else
            {
                @out.writeByte(unchecked((int)(0xff)) & v);
                @out.writeByte(unchecked((int)(0xff)) & ((v) >> (8 & 0x1f)));
                @out.writeByte(unchecked((int)(0xff)) & ((v) >> (16 & 0x1f)));
                @out.writeByte(unchecked((int)(0xff)) & ((v) >> (24 & 0x1f)));
            }
		}

		/// <summary>Writes a Little-endian short.</summary>
		/// <remarks>Writes a Little-endian short.</remarks>
		/// <param name="os">- the output stream to write to.</param>
		/// <param name="v">- the value to write.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public static void writeShort(java.io.OutputStream os, short v)
		{
            if (System.BitConverter.IsLittleEndian)
            {
                os.write(System.BitConverter.GetBytes(v));
            }
            else
            {
                os.write((unchecked((int)(0xff)) & v));
                os.write((unchecked((int)(0xff)) & ((v) >> (8 & 0x1f))));
            }
		}

		/// <summary>Writes a Little-endian int.</summary>
		/// <remarks>Writes a Little-endian int.</remarks>
		/// <param name="os">- the output stream to write to.</param>
		/// <param name="v">- the value to write.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public static void writeInt(java.io.OutputStream os, int v)
		{
            if (System.BitConverter.IsLittleEndian)
            {
                os.write(System.BitConverter.GetBytes(v));
            }
            else
            {
                os.write(unchecked((int)(0xff)) & v);
                os.write(unchecked((int)(0xff)) & ((v) >> (8 & 0x1f)));
                os.write(unchecked((int)(0xff)) & ((v) >> (16 & 0x1f)));
                os.write(unchecked((int)(0xff)) & ((v) >> (24 & 0x1f)));
            }
		}

		/// <summary>Writes a Little-endian long.</summary>
		/// <remarks>Writes a Little-endian long.</remarks>
		/// <param name="os">- the output stream to write to.</param>
		/// <param name="v">- the value to write.</param>
		/// <exception>IOException</exception>
		/// <exception cref="System.IO.IOException"></exception>
		public static void writeLong(java.io.OutputStream os, long v)
		{
            if (System.BitConverter.IsLittleEndian)
            {
                os.write(System.BitConverter.GetBytes(v));
            }
            else
            {
                os.write((int)(unchecked((int)(0xff)) & v));
                os.write((int)(unchecked((int)(0xff)) & ((v) >> (8 & 0x1f))));
                os.write((int)(unchecked((int)(0xff)) & ((v) >> (16 & 0x1f))));
                os.write((int)(unchecked((int)(0xff)) & ((v) >> (24 & 0x1f))));
                os.write((int)(unchecked((int)(0xff)) & ((v) >> (32 & 0x1f))));
                os.write((int)(unchecked((int)(0xff)) & ((v) >> (40 & 0x1f))));
                os.write((int)(unchecked((int)(0xff)) & ((v) >> (48 & 0x1f))));
                os.write((int)(unchecked((int)(0xff)) & ((v) >> (56 & 0x1f))));
            }
		}

		/// <summary>Writes a Little-endian short.</summary>
		/// <remarks>Writes a Little-endian short.</remarks>
		/// <param name="data">the array into which the data should be written.</param>
		/// <param name="offset">the offset from which to start writing in the array.</param>
		/// <param name="v">the value to write.</param>
		public static void writeShort(byte[] data, int offset, int v)
		{
            if (System.BitConverter.IsLittleEndian)
            {
                byte[] temp = System.BitConverter.GetBytes(v);
                System.Buffer.BlockCopy(temp, 0, data, offset, temp.Length);
            }
            else
            {
                data[offset] = (byte)(unchecked((int)(0xff)) & v);
                data[offset + 1] = (byte)(unchecked((int)(0xff)) & ((v) >> (8 & 0x1f)));
            }
		}

		/// <summary>Writes a Little-endian int.</summary>
		/// <remarks>Writes a Little-endian int.</remarks>
		/// <param name="data">the array into which the data should be written.</param>
		/// <param name="offset">the offset from which to start writing in the array.</param>
		/// <param name="v">the value to write.</param>
		public static void writeInt(byte[] data, int offset, int v)
		{
            if (System.BitConverter.IsLittleEndian)
            {
                byte[] temp = System.BitConverter.GetBytes(v);
                System.Buffer.BlockCopy(temp, 0, data, offset, temp.Length);
            }
            else
            {
                data[offset] = (byte)(unchecked((int)(0xff)) & v);
                data[offset + 1] = (byte)(unchecked((int)(0xff)) & ((v) >> (8 & 0x1f)));
                data[offset + 2] = (byte)(unchecked((int)(0xff)) & ((v) >> (16 & 0x1f)));
                data[offset + 3] = (byte)(unchecked((int)(0xff)) & ((v) >> (24 & 0x1f)));
            }
		}

		/// <summary>Writes a Little-endian long.</summary>
		/// <remarks>Writes a Little-endian long.</remarks>
		/// <param name="data">the array into which the data should be written.</param>
		/// <param name="offset">the offset from which to start writing in the array.</param>
		/// <param name="v">the value to write.</param>
		public static void writeLong(byte[] data, int offset, long v)
		{
            if (System.BitConverter.IsLittleEndian)
            {
                byte[] temp = System.BitConverter.GetBytes(v);
                System.Buffer.BlockCopy(temp, 0, data, offset, temp.Length);
            }
            else
            {
                data[offset] = (byte)(unchecked((int)(0xff)) & v);
                data[offset + 1] = (byte)(unchecked((int)(0xff)) & ((v) >> (8 & 0x1f)));
                data[offset + 2] = (byte)(unchecked((int)(0xff)) & ((v) >> (16 & 0x1f)));
                data[offset + 3] = (byte)(unchecked((int)(0xff)) & ((v) >> (24 & 0x1f)));
                data[offset + 4] = (byte)(unchecked((int)(0xff)) & ((v) >> (32 & 0x1f)));
                data[offset + 5] = (byte)(unchecked((int)(0xff)) & ((v) >> (40 & 0x1f)));
                data[offset + 6] = (byte)(unchecked((int)(0xff)) & ((v) >> (48 & 0x1f)));
                data[offset + 7] = (byte)(unchecked((int)(0xff)) & ((v) >> (56 & 0x1f)));
            }
		}

		/// <summary>Writes a String.</summary>
		/// <remarks>Writes a String.</remarks>
		/// <param name="data">the array into which the data should be written.</param>
		/// <param name="offset">the offset from which to start writing in the array.</param>
		/// <param name="v">the value to write.</param>
		public static void writeString(byte[] data, int offset, string v)
		{
			byte[] str = cspeex.StringUtil.getBytesForString(v);
			System.Array.Copy(str, 0, data, offset, str.Length);
		}
	}
}
