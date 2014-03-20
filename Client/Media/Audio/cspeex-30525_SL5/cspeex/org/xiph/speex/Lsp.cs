namespace org.xiph.speex
{
	/// <summary>Line Spectral Pair</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class Lsp
	{
		private float[] pw;

		/// <summary>Constructor</summary>
		public Lsp()
		{
			pw = new float[42];
		}

		/// <summary>This function evaluates a series of Chebyshev polynomials.</summary>
		/// <remarks>This function evaluates a series of Chebyshev polynomials.</remarks>
		/// <param name="coef">- coefficients of the polynomial to be evaluated.</param>
		/// <param name="x">- the point where polynomial is to be evaluated.</param>
		/// <param name="m">- order of the polynomial.</param>
		/// <returns>the value of the polynomial at point x.</returns>
		public static float cheb_poly_eva(float[] coef, float x, int m)
		{
			int i;
			float sum;
			float[] T;
			int m2 = m >> 1;
			T = new float[m2 + 1];
			T[0] = 1;
			T[1] = x;
			sum = coef[m2] + coef[m2 - 1] * x;
			x *= 2;
			for (i = 2; i <= m2; i++)
			{
				T[i] = x * T[i - 1] - T[i - 2];
				sum += coef[m2 - i] * T[i];
			}
			return sum;
		}

		/// <summary>This function converts LPC coefficients to LSP coefficients.</summary>
		/// <remarks>This function converts LPC coefficients to LSP coefficients.</remarks>
		/// <param name="a">- LPC coefficients.</param>
		/// <param name="lpcrdr">- order of LPC coefficients (10).</param>
		/// <param name="freq">- LSP frequencies in the x domain.</param>
		/// <param name="nb">- number of sub-intervals (4).</param>
		/// <param name="delta">- grid spacing interval (0.02).</param>
		/// <returns>the number of roots (the LSP coefs are returned in the array).</returns>
		public static int lpc2lsp(float[] a, int lpcrdr, float[] freq, int nb, float delta
			)
		{
			float psuml;
			float psumr;
			float psumm;
			float temp_xr;
			float xl;
			float xr;
			float xm = 0;
			float temp_psumr;
			int i;
			int j;
			int m;
			int flag;
			int k;
			float[] Q;
			// ptrs for memory allocation
			float[] P;
			int px;
			// ptrs of respective P'(z) & Q'(z)
			int qx;
			int p;
			int q;
			float[] pt;
			// ptr used for cheb_poly_eval() whether P' or Q'
			int roots = 0;
			// DR 8/2/94: number of roots found
			flag = 1;
			// program is searching for a root when, 1 else has found one
			m = lpcrdr / 2;
			// order of P'(z) & Q'(z) polynomials
			Q = new float[m + 1];
			P = new float[m + 1];
			px = 0;
			qx = 0;
			p = px;
			q = qx;
			P[px++] = 1.0f;
			Q[qx++] = 1.0f;
			for (i = 1; i <= m; i++)
			{
				P[px++] = a[i] + a[lpcrdr + 1 - i] - P[p++];
				Q[qx++] = a[i] - a[lpcrdr + 1 - i] + Q[q++];
			}
			px = 0;
			qx = 0;
			for (i = 0; i < m; i++)
			{
				P[px] = 2 * P[px];
				Q[qx] = 2 * Q[qx];
				px++;
				qx++;
			}
			px = 0;
			qx = 0;
			xr = 0;
			xl = 1.0f;
			for (j = 0; j < lpcrdr; j++)
			{
				if (j % 2 != 0)
				{
					pt = Q;
				}
				else
				{
					pt = P;
				}
				psuml = cheb_poly_eva(pt, xl, lpcrdr);
				flag = 1;
				while ((flag == 1) && (xr >= -1.0))
				{
					float dd;
					dd = (float)(delta * (1f - .9f * xl * xl));
					if (System.Math.Abs(psuml) < .2f)
					{
						dd *= .5f;
					}
					xr = xl - dd;
					psumr = cheb_poly_eva(pt, xr, lpcrdr);
					temp_psumr = psumr;
					temp_xr = xr;
					if ((psumr * psuml) < 0.0)
					{
						roots++;
						psumm = psuml;
						for (k = 0; k <= nb; k++)
						{
							xm = (xl + xr) / 2;
							psumm = cheb_poly_eva(pt, xm, lpcrdr);
							if (psumm * psuml > 0.0)
							{
								psuml = psumm;
								xl = xm;
							}
							else
							{
								psumr = psumm;
								xr = xm;
							}
						}
						freq[j] = xm;
						xl = xm;
						flag = 0;
					}
					else
					{
						psuml = temp_psumr;
						xl = temp_xr;
					}
				}
			}
			return roots;
		}

		/// <summary>Line Spectral Pair to Linear Prediction Coefficients</summary>
		/// <param name="freq"></param>
		/// <param name="ak"></param>
		/// <param name="lpcrdr"></param>
		public virtual void lsp2lpc(float[] freq, float[] ak, int lpcrdr)
		{
			int i;
			int j;
			float xout1;
			float xout2;
			float xin1;
			float xin2;
			int n1;
			int n2;
			int n3;
			int n4 = 0;
			int m = lpcrdr / 2;
			for (i = 0; i < 4 * m + 2; i++)
			{
				pw[i] = 0.0f;
			}
			xin1 = 1.0f;
			xin2 = 1.0f;
			for (j = 0; j <= lpcrdr; j++)
			{
				int i2 = 0;
				for (i = 0; i < m; i++, i2 += 2)
				{
					n1 = i * 4;
					n2 = n1 + 1;
					n3 = n2 + 1;
					n4 = n3 + 1;
					xout1 = xin1 - 2 * (freq[i2]) * pw[n1] + pw[n2];
					xout2 = xin2 - 2 * (freq[i2 + 1]) * pw[n3] + pw[n4];
					pw[n2] = pw[n1];
					pw[n4] = pw[n3];
					pw[n1] = xin1;
					pw[n3] = xin2;
					xin1 = xout1;
					xin2 = xout2;
				}
				xout1 = xin1 + pw[n4 + 1];
				xout2 = xin2 - pw[n4 + 2];
				ak[j] = (xout1 + xout2) * 0.5f;
				pw[n4 + 1] = xin1;
				pw[n4 + 2] = xin2;
				xin1 = 0.0f;
				xin2 = 0.0f;
			}
		}

		/// <summary>Makes sure the LSPs are stable.</summary>
		/// <remarks>Makes sure the LSPs are stable.</remarks>
		/// <param name="lsp"></param>
		/// <param name="len"></param>
		/// <param name="margin"></param>
		public static void enforce_margin(float[] lsp, int len, float margin)
		{
			int i;
			if (lsp[0] < margin)
			{
				lsp[0] = margin;
			}
			if (lsp[len - 1] > (float)System.Math.PI - margin)
			{
				lsp[len - 1] = (float)System.Math.PI - margin;
			}
			for (i = 1; i < len - 1; i++)
			{
				if (lsp[i] < lsp[i - 1] + margin)
				{
					lsp[i] = lsp[i - 1] + margin;
				}
				if (lsp[i] > lsp[i + 1] - margin)
				{
					lsp[i] = .5f * (lsp[i] + lsp[i + 1] - margin);
				}
			}
		}
	}
}
