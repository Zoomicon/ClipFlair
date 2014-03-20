namespace org.xiph.speex
{
	/// <summary>Filters</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class Filters
	{
		private int last_pitch;

		private float[] last_pitch_gain;

		private float smooth_gain;

		private float[] xx;

		/// <summary>Constructor.</summary>
		/// <remarks>Constructor.</remarks>
		public Filters()
		{
			last_pitch_gain = new float[3];
			xx = new float[1024];
		}

		/// <summary>Initialisation.</summary>
		/// <remarks>Initialisation.</remarks>
		public virtual void init()
		{
			last_pitch = 0;
			last_pitch_gain[0] = last_pitch_gain[1] = last_pitch_gain[2] = 0;
			smooth_gain = 1;
		}

		/// <summary>bw_lpc</summary>
		/// <param name="gamma"></param>
		/// <param name="lpc_in"></param>
		/// <param name="lpc_out"></param>
		/// <param name="order"></param>
		public static void bw_lpc(float gamma, float[] lpc_in, float[] lpc_out, int order
			)
		{
			float tmp = 1;
			for (int i = 0; i < order + 1; i++)
			{
				lpc_out[i] = tmp * lpc_in[i];
				tmp *= gamma;
			}
		}

		/// <summary>filter_mem2</summary>
		/// <param name="x"></param>
		/// <param name="xs"></param>
		/// <param name="num"></param>
		/// <param name="den"></param>
		/// <param name="N"></param>
		/// <param name="ord"></param>
		/// <param name="mem"></param>
		/// <param name="ms"></param>
		public static void filter_mem2(float[] x, int xs, float[] num, float[] den, int N
			, int ord, float[] mem, int ms)
		{
			int i;
			int j;
			float xi;
			float yi;
			for (i = 0; i < N; i++)
			{
				xi = x[xs + i];
				x[xs + i] = num[0] * xi + mem[ms + 0];
				yi = x[xs + i];
				for (j = 0; j < ord - 1; j++)
				{
					mem[ms + j] = mem[ms + j + 1] + num[j + 1] * xi - den[j + 1] * yi;
				}
				mem[ms + ord - 1] = num[ord] * xi - den[ord] * yi;
			}
		}

		/// <summary>filter_mem2</summary>
		/// <param name="x"></param>
		/// <param name="xs"></param>
		/// <param name="num"></param>
		/// <param name="den"></param>
		/// <param name="y"></param>
		/// <param name="ys"></param>
		/// <param name="N"></param>
		/// <param name="ord"></param>
		/// <param name="mem"></param>
		/// <param name="ms"></param>
		public static void filter_mem2(float[] x, int xs, float[] num, float[] den, float
			[] y, int ys, int N, int ord, float[] mem, int ms)
		{
			int i;
			int j;
			float xi;
			float yi;
			for (i = 0; i < N; i++)
			{
				xi = x[xs + i];
				y[ys + i] = num[0] * xi + mem[0];
				yi = y[ys + i];
				for (j = 0; j < ord - 1; j++)
				{
					mem[ms + j] = mem[ms + j + 1] + num[j + 1] * xi - den[j + 1] * yi;
				}
				mem[ms + ord - 1] = num[ord] * xi - den[ord] * yi;
			}
		}

		/// <summary>iir_mem2</summary>
		/// <param name="x"></param>
		/// <param name="xs"></param>
		/// <param name="den"></param>
		/// <param name="y"></param>
		/// <param name="ys"></param>
		/// <param name="N"></param>
		/// <param name="ord"></param>
		/// <param name="mem"></param>
		public static void iir_mem2(float[] x, int xs, float[] den, float[] y, int ys, int
			 N, int ord, float[] mem)
		{
			int i;
			int j;
			for (i = 0; i < N; i++)
			{
				y[ys + i] = x[xs + i] + mem[0];
				for (j = 0; j < ord - 1; j++)
				{
					mem[j] = mem[j + 1] - den[j + 1] * y[ys + i];
				}
				mem[ord - 1] = -den[ord] * y[ys + i];
			}
		}

		/// <summary>fir_mem2</summary>
		/// <param name="x"></param>
		/// <param name="xs"></param>
		/// <param name="num"></param>
		/// <param name="y"></param>
		/// <param name="ys"></param>
		/// <param name="N"></param>
		/// <param name="ord"></param>
		/// <param name="mem"></param>
		public static void fir_mem2(float[] x, int xs, float[] num, float[] y, int ys, int
			 N, int ord, float[] mem)
		{
			int i;
			int j;
			float xi;
			for (i = 0; i < N; i++)
			{
				xi = x[xs + i];
				y[ys + i] = num[0] * xi + mem[0];
				for (j = 0; j < ord - 1; j++)
				{
					mem[j] = mem[j + 1] + num[j + 1] * xi;
				}
				mem[ord - 1] = num[ord] * xi;
			}
		}

		/// <summary>syn_percep_zero</summary>
		/// <param name="xx"></param>
		/// <param name="xxs"></param>
		/// <param name="ak"></param>
		/// <param name="awk1"></param>
		/// <param name="awk2"></param>
		/// <param name="y"></param>
		/// <param name="N"></param>
		/// <param name="ord"></param>
		public static void syn_percep_zero(float[] xx, int xxs, float[] ak, float[] awk1, 
			float[] awk2, float[] y, int N, int ord)
		{
			int i;
			float[] mem = new float[ord];
			//    for (i = 0; i < ord; i++)
			//      mem[i]=0;
			filter_mem2(xx, xxs, awk1, ak, y, 0, N, ord, mem, 0);
			for (i = 0; i < ord; i++)
			{
				mem[i] = 0;
			}
			iir_mem2(y, 0, awk2, y, 0, N, ord, mem);
		}

		/// <summary>residue_percep_zero</summary>
		/// <param name="xx"></param>
		/// <param name="xxs"></param>
		/// <param name="ak"></param>
		/// <param name="awk1"></param>
		/// <param name="awk2"></param>
		/// <param name="y"></param>
		/// <param name="N"></param>
		/// <param name="ord"></param>
		public static void residue_percep_zero(float[] xx, int xxs, float[] ak, float[] awk1
			, float[] awk2, float[] y, int N, int ord)
		{
			int i;
			float[] mem = new float[ord];
			//    for (i = 0; i < ord; i++)
			//      mem[i] = 0;
			filter_mem2(xx, xxs, ak, awk1, y, 0, N, ord, mem, 0);
			for (i = 0; i < ord; i++)
			{
				mem[i] = 0;
			}
			fir_mem2(y, 0, awk2, y, 0, N, ord, mem);
		}

		/// <summary>fir_mem_up</summary>
		/// <param name="x"></param>
		/// <param name="a"></param>
		/// <param name="y"></param>
		/// <param name="N"></param>
		/// <param name="M"></param>
		/// <param name="mem"></param>
		public virtual void fir_mem_up(float[] x, float[] a, float[] y, int N, int M, float
			[] mem)
		{
			int i;
			int j;
			for (i = 0; i < N / 2; i++)
			{
				xx[2 * i] = x[N / 2 - 1 - i];
			}
			for (i = 0; i < M - 1; i += 2)
			{
				xx[N + i] = mem[i + 1];
			}
			for (i = 0; i < N; i += 4)
			{
				float y0;
				float y1;
				float y2;
				float y3;
				float x0;
				y0 = y1 = y2 = y3 = 0.0f;
				x0 = xx[N - 4 - i];
				for (j = 0; j < M; j += 4)
				{
					float x1;
					float a0;
					float a1;
					a0 = a[j];
					a1 = a[j + 1];
					x1 = xx[N - 2 + j - i];
					y0 += a0 * x1;
					y1 += a1 * x1;
					y2 += a0 * x0;
					y3 += a1 * x0;
					a0 = a[j + 2];
					a1 = a[j + 3];
					x0 = xx[N + j - i];
					y0 += a0 * x0;
					y1 += a1 * x0;
					y2 += a0 * x1;
					y3 += a1 * x1;
				}
				y[i] = y0;
				y[i + 1] = y1;
				y[i + 2] = y2;
				y[i + 3] = y3;
			}
			for (i = 0; i < M - 1; i += 2)
			{
				mem[i + 1] = xx[i];
			}
		}

		/// <summary>Comb Filter.</summary>
		/// <remarks>Comb Filter.</remarks>
		/// <param name="exc">- decoded excitation</param>
		/// <param name="esi"></param>
		/// <param name="new_exc">- enhanced excitation</param>
		/// <param name="nsi"></param>
		/// <param name="nsf">- sub-frame size</param>
		/// <param name="pitch">- pitch period</param>
		/// <param name="pitch_gain">- pitch gain (3-tap)</param>
		/// <param name="comb_gain">- gain of comb filter</param>
		public virtual void comb_filter(float[] exc, int esi, float[] new_exc, int nsi, int
			 nsf, int pitch, float[] pitch_gain, float comb_gain)
		{
			int i;
			int j;
			float exc_energy = 0.0f;
			float new_exc_energy = 0.0f;
			float gain;
			float step;
			float fact;
			float g = 0.0f;
			for (i = esi; i < esi + nsf; i++)
			{
				exc_energy += exc[i] * exc[i];
			}
			g = .5f * System.Math.Abs(pitch_gain[0] + pitch_gain[1] + pitch_gain[2] + last_pitch_gain
				[0] + last_pitch_gain[1] + last_pitch_gain[2]);
			if (g > 1.3f)
			{
				comb_gain *= 1.3f / g;
			}
			if (g < .5f)
			{
				comb_gain *= 2.0f * g;
			}
			step = 1.0f / nsf;
			fact = 0;
			for (i = 0, j = esi; i < nsf; i++, j++)
			{
				fact += step;
				new_exc[nsi + i] = exc[j] + comb_gain * fact * (pitch_gain[0] * exc[j - pitch + 1
					] + pitch_gain[1] * exc[j - pitch] + pitch_gain[2] * exc[j - pitch - 1]) + comb_gain
					 * (1.0f - fact) * (last_pitch_gain[0] * exc[j - last_pitch + 1] + last_pitch_gain
					[1] * exc[j - last_pitch] + last_pitch_gain[2] * exc[j - last_pitch - 1]);
			}
			last_pitch_gain[0] = pitch_gain[0];
			last_pitch_gain[1] = pitch_gain[1];
			last_pitch_gain[2] = pitch_gain[2];
			last_pitch = pitch;
			for (i = nsi; i < nsi + nsf; i++)
			{
				new_exc_energy += new_exc[i] * new_exc[i];
			}
			gain = (float)(System.Math.Sqrt(exc_energy / (.1f + new_exc_energy)));
			if (gain < .5f)
			{
				gain = .5f;
			}
			if (gain > 1.0f)
			{
				gain = 1.0f;
			}
			for (i = nsi; i < nsi + nsf; i++)
			{
				smooth_gain = .96f * smooth_gain + .04f * gain;
				new_exc[i] *= smooth_gain;
			}
		}

		/// <summary>Quadrature Mirror Filter to Split the band in two.</summary>
		/// <remarks>
		/// Quadrature Mirror Filter to Split the band in two.
		/// A 16kHz signal is thus divided into two 8kHz signals representing the low and high bands.
		/// (used by wideband encoder)
		/// </remarks>
		/// <param name="xx"></param>
		/// <param name="aa"></param>
		/// <param name="y1"></param>
		/// <param name="y2"></param>
		/// <param name="N"></param>
		/// <param name="M"></param>
		/// <param name="mem"></param>
		public static void qmf_decomp(float[] xx, float[] aa, float[] y1, float[] y2, int
			 N, int M, float[] mem)
		{
			int i;
			int j;
			int k;
			int M2;
			float[] a;
			float[] x;
			int x2;
			a = new float[M];
			x = new float[N + M - 1];
			x2 = M - 1;
			M2 = M >> 1;
			for (i = 0; i < M; i++)
			{
				a[M - i - 1] = aa[i];
			}
			for (i = 0; i < M - 1; i++)
			{
				x[i] = mem[M - i - 2];
			}
			for (i = 0; i < N; i++)
			{
				x[i + M - 1] = xx[i];
			}
			for (i = 0, k = 0; i < N; i += 2, k++)
			{
				y1[k] = 0;
				y2[k] = 0;
				for (j = 0; j < M2; j++)
				{
					y1[k] += a[j] * (x[i + j] + x[x2 + i - j]);
					y2[k] -= a[j] * (x[i + j] - x[x2 + i - j]);
					j++;
					y1[k] += a[j] * (x[i + j] + x[x2 + i - j]);
					y2[k] += a[j] * (x[i + j] - x[x2 + i - j]);
				}
			}
			for (i = 0; i < M - 1; i++)
			{
				mem[i] = xx[N - i - 1];
			}
		}
	}
}
