namespace org.xiph.speex
{
	/// <summary>Calculates the CRC checksum for Ogg packets.</summary>
	/// <remarks>
	/// Calculates the CRC checksum for Ogg packets.
	/// <p>Ogg uses the same generator polynomial as ethernet, although with an
	/// unreflected alg and an init/final of 0, not 0xffffffff.
	/// </remarks>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class OggCrc
	{
		/// <summary>CRC checksum lookup table</summary>
		private static int[] crc_lookup;

		static OggCrc()
		{
			// TODO - implement java.util.zip.Checksum
			crc_lookup = new int[256];
			for (int i = 0; i < crc_lookup.Length; i++)
			{
				int r = i << 24;
				for (int j = 0; j < 8; j++)
				{
					if ((r & unchecked((int)(0x80000000))) != 0)
					{
						r = (r << 1) ^ unchecked((int)(0x04c11db7));
					}
					else
					{
						r <<= 1;
					}
				}
				crc_lookup[i] = (r & unchecked((int)(0xffffffff)));
			}
		}

		/// <summary>
		/// Calculates the checksum on the given data, from the give offset and
		/// for the given length, using the given initial value.
		/// </summary>
		/// <remarks>
		/// Calculates the checksum on the given data, from the give offset and
		/// for the given length, using the given initial value.
		/// This allows on to calculate the checksum iteratively, by reinjecting the
		/// last returned value as the initial value when the function is called for
		/// the next data chunk.
		/// The initial value should be 0 for the first iteration.
		/// </remarks>
		/// <param name="crc">- the initial value</param>
		/// <param name="data">- the data</param>
		/// <param name="offset">- the offset at which to start calculating the checksum.</param>
		/// <param name="length">- the length of data over which to calculate the checksum.</param>
		/// <returns>the checksum.</returns>
		public static int checksum(int crc, byte[] data, int offset, int length)
		{
			int end = offset + length;
			for (; offset < end; offset++)
			{
				crc = (crc << 8) ^ crc_lookup[(((crc) >> (24 & 0x1f)) & unchecked((int)(0xff))) ^
					 (data[offset] & unchecked((int)(0xff)))];
			}
			return crc;
		}
	}
}
