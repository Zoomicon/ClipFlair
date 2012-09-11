namespace org.xiph.speex
{
	/// <summary>Stereo</summary>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class Stereo
	{
		/// <summary>Inband code number for Stereo</summary>
		public const int SPEEX_INBAND_STEREO = 9;

		public static readonly float[] e_ratio_quant = new float[] { .25f, .315f, .397f, 
			.5f };

		private float balance = 1f;

		/// <summary>Left/right balance info</summary>
		private float e_ratio = 0.5f;

		/// <summary>Ratio of energies: E(left+right)/[E(left)+E(right)]</summary>
		private float smooth_left = 1f;

		/// <summary>Smoothed left channel gain</summary>
		private float smooth_right = 1f;

		//  private float reserved1;           /** Reserved for future use */
		//  private float reserved2;           /** Reserved for future use */
		/// <summary>
		/// Transforms a stereo frame into a mono frame and stores intensity stereo
		/// info in 'bits'.
		/// </summary>
		/// <remarks>
		/// Transforms a stereo frame into a mono frame and stores intensity stereo
		/// info in 'bits'.
		/// </remarks>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="data"></param>
		/// <param name="frameSize"></param>
		public static void encode(org.xiph.speex.Bits bits, float[] data, int frameSize)
		{
			int i;
			int tmp;
			float e_left = 0;
			float e_right = 0;
			float e_tot = 0;
			float balance;
			float e_ratio;
			for (i = 0; i < frameSize; i++)
			{
				e_left += data[2 * i] * data[2 * i];
				e_right += data[2 * i + 1] * data[2 * i + 1];
				data[i] = .5f * (data[2 * i] + data[2 * i + 1]);
				e_tot += data[i] * data[i];
			}
			balance = (e_left + 1) / (e_right + 1);
			e_ratio = e_tot / (1 + e_left + e_right);
			bits.pack(14, 5);
			bits.pack(SPEEX_INBAND_STEREO, 4);
			balance = (float)(4 * System.Math.Log(balance));
			if (balance > 0)
			{
				bits.pack(0, 1);
			}
			else
			{
				bits.pack(1, 1);
			}
			balance = (float)System.Math.Floor(.5f + System.Math.Abs(balance));
			if (balance > 30)
			{
				balance = 31;
			}
			bits.pack((int)balance, 5);
			tmp = org.xiph.speex.VQ.index(e_ratio, e_ratio_quant, 4);
			bits.pack(tmp, 2);
		}

		/// <summary>Transforms a mono frame into a stereo frame using intensity stereo info.
		/// 	</summary>
		/// <remarks>Transforms a mono frame into a stereo frame using intensity stereo info.
		/// 	</remarks>
		/// <param name="data">
		/// - float array of size 2*frameSize, that contains the mono
		/// audio samples in the first half. When the function has completed, the
		/// array will contain the interlaced stereo audio samples.
		/// </param>
		/// <param name="frameSize">- the size of a frame of mono audio samples.</param>
		public virtual void decode(float[] data, int frameSize)
		{
			int i;
			float e_tot = 0;
			float e_left;
			float e_right;
			float e_sum;
			for (i = frameSize - 1; i >= 0; i--)
			{
				e_tot += data[i] * data[i];
			}
			e_sum = e_tot / e_ratio;
			e_left = e_sum * balance / (1 + balance);
			e_right = e_sum - e_left;
			e_left = (float)System.Math.Sqrt(e_left / (e_tot + .01f));
			e_right = (float)System.Math.Sqrt(e_right / (e_tot + .01f));
			for (i = frameSize - 1; i >= 0; i--)
			{
				float ftmp = data[i];
				smooth_left = .98f * smooth_left + .02f * e_left;
				smooth_right = .98f * smooth_right + .02f * e_right;
				data[2 * i] = smooth_left * ftmp;
				data[2 * i + 1] = smooth_right * ftmp;
			}
		}

		/// <summary>Callback handler for intensity stereo info</summary>
		/// <param name="bits">- Speex bits buffer.</param>
		public virtual void init(org.xiph.speex.Bits bits)
		{
			float sign = 1;
			int tmp;
			if (bits.unpack(1) != 0)
			{
				sign = -1;
			}
			tmp = bits.unpack(5);
			balance = (float)System.Math.Exp(sign * .25 * tmp);
			tmp = bits.unpack(2);
			e_ratio = e_ratio_quant[tmp];
		}
	}
}
