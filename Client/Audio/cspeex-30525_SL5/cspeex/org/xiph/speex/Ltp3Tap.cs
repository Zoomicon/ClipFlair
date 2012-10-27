namespace org.xiph.speex
{
	/// <summary>Long Term Prediction Quantisation and Unquantisation (3Tap)</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class Ltp3Tap : org.xiph.speex.Ltp
	{
		private float[] gain;

		private int[] gain_cdbk;

		private int gain_bits;

		private int pitch_bits;

		private float[][] e;

		/// <summary>Constructor</summary>
		/// <param name="gain_cdbk"></param>
		/// <param name="gain_bits"></param>
		/// <param name="pitch_bits"></param>
		public Ltp3Tap(int[] gain_cdbk, int gain_bits, int pitch_bits)
		{
			this.gain = new float[3];
			this.gain_cdbk = gain_cdbk;
			this.gain_bits = gain_bits;
			this.pitch_bits = pitch_bits;
			this.e = new float[][] { new float[128], new float[128], new float[128] };
		}

		/// <summary>Long Term Prediction Quantification (3Tap).</summary>
		/// <remarks>Long Term Prediction Quantification (3Tap).</remarks>
		/// <returns>pitch</returns>
		public sealed override int quant(float[] target, float[] sw, int sws, float[] ak, 
			float[] awk1, float[] awk2, float[] exc, int es, int start, int end, float pitch_coef
			, int p, int nsf, org.xiph.speex.Bits bits, float[] exc2, int e2s, float[] r, int
			 complexity)
		{
			int i;
			int j;
			int[] cdbk_index = new int[1];
			int pitch = 0;
			int best_gain_index = 0;
			float[] best_exc;
			int best_pitch = 0;
			float err;
			float best_err = -1;
			int N;
			int[] nbest;
			float[] gains;
			N = complexity;
			if (N > 10)
			{
				N = 10;
			}
			nbest = new int[N];
			gains = new float[N];
			if (N == 0 || end < start)
			{
				bits.pack(0, pitch_bits);
				bits.pack(0, gain_bits);
				for (i = 0; i < nsf; i++)
				{
					exc[es + i] = 0;
				}
				return start;
			}
			best_exc = new float[nsf];
			if (N > end - start + 1)
			{
				N = end - start + 1;
			}
			open_loop_nbest_pitch(sw, sws, start, end, nsf, nbest, gains, N);
			for (i = 0; i < N; i++)
			{
				pitch = nbest[i];
				for (j = 0; j < nsf; j++)
				{
					exc[es + j] = 0;
				}
				err = pitch_gain_search_3tap(target, ak, awk1, awk2, exc, es, pitch, p, nsf, bits
					, exc2, e2s, r, cdbk_index);
				if (err < best_err || best_err < 0)
				{
					for (j = 0; j < nsf; j++)
					{
						best_exc[j] = exc[es + j];
					}
					best_err = err;
					best_pitch = pitch;
					best_gain_index = cdbk_index[0];
				}
			}
			bits.pack(best_pitch - start, pitch_bits);
			bits.pack(best_gain_index, gain_bits);
			for (i = 0; i < nsf; i++)
			{
				exc[es + i] = best_exc[i];
			}
			return pitch;
		}

		/// <summary>Long Term Prediction Unquantification (3Tap).</summary>
		/// <remarks>Long Term Prediction Unquantification (3Tap).</remarks>
		/// <param name="exc">- Excitation</param>
		/// <param name="es">- Excitation offset</param>
		/// <param name="start">- Smallest pitch value allowed</param>
		/// <param name="pitch_coef">- Voicing (pitch) coefficient</param>
		/// <param name="nsf">- Number of samples in subframe</param>
		/// <param name="gain_val"></param>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="count_lost"></param>
		/// <param name="subframe_offset"></param>
		/// <param name="last_pitch_gain"></param>
		/// <returns>pitch</returns>
		public sealed override int unquant(float[] exc, int es, int start, float pitch_coef
			, int nsf, float[] gain_val, org.xiph.speex.Bits bits, int count_lost, int subframe_offset
			, float last_pitch_gain)
		{
			int i;
			int pitch;
			int gain_index;
			pitch = bits.unpack(pitch_bits);
			pitch += start;
			gain_index = bits.unpack(gain_bits);
			gain[0] = 0.015625f * (float)gain_cdbk[gain_index * 3] + .5f;
			gain[1] = 0.015625f * (float)gain_cdbk[gain_index * 3 + 1] + .5f;
			gain[2] = 0.015625f * (float)gain_cdbk[gain_index * 3 + 2] + .5f;
			if (count_lost != 0 && pitch > subframe_offset)
			{
				float gain_sum = System.Math.Abs(gain[1]);
				float tmp = count_lost < 4 ? last_pitch_gain : 0.4f * last_pitch_gain;
				if (tmp > .95f)
				{
					tmp = .95f;
				}
				if (gain[0] > 0)
				{
					gain_sum += gain[0];
				}
				else
				{
					gain_sum -= .5f * gain[0];
				}
				if (gain[2] > 0)
				{
					gain_sum += gain[2];
				}
				else
				{
					gain_sum -= .5f * gain[0];
				}
				if (gain_sum > tmp)
				{
					float fact = tmp / gain_sum;
					for (i = 0; i < 3; i++)
					{
						gain[i] *= fact;
					}
				}
			}
			gain_val[0] = gain[0];
			gain_val[1] = gain[1];
			gain_val[2] = gain[2];
			for (i = 0; i < 3; i++)
			{
				int j;
				int pp = pitch + 1 - i;
				int tmp1;
				int tmp2;
				tmp1 = nsf;
				if (tmp1 > pp)
				{
					tmp1 = pp;
				}
				tmp2 = nsf;
				if (tmp2 > pp + pitch)
				{
					tmp2 = pp + pitch;
				}
				for (j = 0; j < tmp1; j++)
				{
					e[i][j] = exc[es + j - pp];
				}
				for (j = tmp1; j < tmp2; j++)
				{
					e[i][j] = exc[es + j - pp - pitch];
				}
				for (j = tmp2; j < nsf; j++)
				{
					e[i][j] = 0;
				}
			}
			for (i = 0; i < nsf; i++)
			{
				exc[es + i] = org.xiph.speex.NbCodec.VERY_SMALL + gain[0] * e[2][i] + gain[1] * e
					[1][i] + gain[2] * e[0][i];
			}
			return pitch;
		}

		/// <summary>Finds the best quantized 3-tap pitch predictor by analysis by synthesis.
		/// 	</summary>
		/// <remarks>Finds the best quantized 3-tap pitch predictor by analysis by synthesis.
		/// 	</remarks>
		/// <param name="target">Target vector</param>
		/// <param name="ak">LPCs for this subframe</param>
		/// <param name="awk1">Weighted LPCs #1 for this subframe</param>
		/// <param name="awk2">Weighted LPCs #2 for this subframe</param>
		/// <param name="exc">Excitation</param>
		/// <param name="es"></param>
		/// <param name="pitch">Pitch value</param>
		/// <param name="p">Number of LPC coeffs</param>
		/// <param name="nsf">Number of samples in subframe</param>
		/// <param name="bits"></param>
		/// <param name="exc2"></param>
		/// <param name="e2s"></param>
		/// <param name="r"></param>
		/// <param name="cdbk_index"></param>
		/// <returns>the best quantized 3-tap pitch predictor by analysis by synthesis.</returns>
		private float pitch_gain_search_3tap(float[] target, float[] ak, float[] awk1, float
			[] awk2, float[] exc, int es, int pitch, int p, int nsf, org.xiph.speex.Bits bits
			, float[] exc2, int e2s, float[] r, int[] cdbk_index)
		{
			int i;
			int j;
			float[][] x;
			//    float[][] e;
			float[] corr = new float[3];
			float[][] A = new float[][] { new float[3], new float[3], new float[3] };
			int gain_cdbk_size;
			float err1;
			float err2;
			gain_cdbk_size = 1 << gain_bits;
			x = new float[][] { new float[nsf], new float[nsf], new float[nsf] };
			e = new float[][] { new float[nsf], new float[nsf], new float[nsf] };
			for (i = 2; i >= 0; i--)
			{
				int pp = pitch + 1 - i;
				for (j = 0; j < nsf; j++)
				{
					if (j - pp < 0)
					{
						e[i][j] = exc2[e2s + j - pp];
					}
					else
					{
						if (j - pp - pitch < 0)
						{
							e[i][j] = exc2[e2s + j - pp - pitch];
						}
						else
						{
							e[i][j] = 0;
						}
					}
				}
				if (i == 2)
				{
					org.xiph.speex.Filters.syn_percep_zero(e[i], 0, ak, awk1, awk2, x[i], nsf, p);
				}
				else
				{
					for (j = 0; j < nsf - 1; j++)
					{
						x[i][j + 1] = x[i + 1][j];
					}
					x[i][0] = 0;
					for (j = 0; j < nsf; j++)
					{
						x[i][j] += e[i][0] * r[j];
					}
				}
			}
			for (i = 0; i < 3; i++)
			{
				corr[i] = inner_prod(x[i], 0, target, 0, nsf);
			}
			for (i = 0; i < 3; i++)
			{
				for (j = 0; j <= i; j++)
				{
					A[i][j] = A[j][i] = inner_prod(x[i], 0, x[j], 0, nsf);
				}
			}
			float[] C = new float[9];
			int ptr = 0;
			int best_cdbk = 0;
			float best_sum = 0;
			C[0] = corr[2];
			C[1] = corr[1];
			C[2] = corr[0];
			C[3] = A[1][2];
			C[4] = A[0][1];
			C[5] = A[0][2];
			C[6] = A[2][2];
			C[7] = A[1][1];
			C[8] = A[0][0];
			for (i = 0; i < gain_cdbk_size; i++)
			{
				float sum = 0;
				float g0;
				float g1;
				float g2;
				ptr = 3 * i;
				g0 = 0.015625f * gain_cdbk[ptr] + .5f;
				g1 = 0.015625f * gain_cdbk[ptr + 1] + .5f;
				g2 = 0.015625f * gain_cdbk[ptr + 2] + .5f;
				sum += C[0] * g0;
				sum += C[1] * g1;
				sum += C[2] * g2;
				sum -= C[3] * g0 * g1;
				sum -= C[4] * g2 * g1;
				sum -= C[5] * g2 * g0;
				sum -= .5f * C[6] * g0 * g0;
				sum -= .5f * C[7] * g1 * g1;
				sum -= .5f * C[8] * g2 * g2;
				if (sum > best_sum || i == 0)
				{
					best_sum = sum;
					best_cdbk = i;
				}
			}
			gain[0] = 0.015625f * gain_cdbk[best_cdbk * 3] + .5f;
			gain[1] = 0.015625f * gain_cdbk[best_cdbk * 3 + 1] + .5f;
			gain[2] = 0.015625f * gain_cdbk[best_cdbk * 3 + 2] + .5f;
			cdbk_index[0] = best_cdbk;
			for (i = 0; i < nsf; i++)
			{
				exc[es + i] = gain[0] * e[2][i] + gain[1] * e[1][i] + gain[2] * e[0][i];
			}
			err1 = 0;
			err2 = 0;
			for (i = 0; i < nsf; i++)
			{
				err1 += target[i] * target[i];
			}
			for (i = 0; i < nsf; i++)
			{
				err2 += (target[i] - gain[2] * x[0][i] - gain[1] * x[1][i] - gain[0] * x[2][i]) *
					 (target[i] - gain[2] * x[0][i] - gain[1] * x[1][i] - gain[0] * x[2][i]);
			}
			return err2;
		}
	}
}
