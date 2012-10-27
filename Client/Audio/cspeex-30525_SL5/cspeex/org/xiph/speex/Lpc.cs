namespace org.xiph.speex
{
	/// <summary>LPC - and Reflection Coefficients.</summary>
	/// <remarks>
	/// LPC - and Reflection Coefficients.
	/// <p>The next two functions calculate linear prediction coefficients
	/// and/or the related reflection coefficients from the first P_MAX+1
	/// values of the autocorrelation function.
	/// <p>Invented by N. Levinson in 1947, modified by J. Durbin in 1959.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class Lpc
	{
		/// <summary>Returns minimum mean square error.</summary>
		/// <remarks>Returns minimum mean square error.</remarks>
		/// <param name="lpc">- float[0...p-1] LPC coefficients</param>
		/// <param name="ac">-  in: float[0...p] autocorrelation values</param>
		/// <param name="ref">- out: float[0...p-1] reflection coef's</param>
		/// <param name="p"></param>
		/// <returns>minimum mean square error.</returns>
		public static float wld(float[] lpc, float[] ac, float[] @ref, int p)
		{
			int i;
			int j;
			float r;
			float error = ac[0];
			if (ac[0] == 0)
			{
				for (i = 0; i < p; i++)
				{
					@ref[i] = 0;
				}
				return 0;
			}
			for (i = 0; i < p; i++)
			{
				r = -ac[i + 1];
				for (j = 0; j < i; j++)
				{
					r -= lpc[j] * ac[i - j];
				}
				@ref[i] = r /= error;
				lpc[i] = r;
				for (j = 0; j < i / 2; j++)
				{
					float tmp = lpc[j];
					lpc[j] += r * lpc[i - 1 - j];
					lpc[i - 1 - j] += r * tmp;
				}
				if ((i % 2) != 0)
				{
					lpc[j] += lpc[j] * r;
				}
				error *= (float)(1.0 - r * r);
			}
			return error;
		}

		/// <summary>
		/// Compute the autocorrelation
		/// ,--,
		/// ac(i) = &gt;  x(n) * x(n-i)  for all n
		/// `--'
		/// for lags between 0 and lag-1, and x == 0 outside 0...n-1
		/// </summary>
		/// <param name="x">- in: float[0...n-1] samples x</param>
		/// <param name="ac">- out: float[0...lag-1] ac values</param>
		/// <param name="lag"></param>
		/// <param name="n"></param>
		public static void autocorr(float[] x, float[] ac, int lag, int n)
		{
			float d;
			int i;
			while (lag-- > 0)
			{
				for (i = lag, d = 0; i < n; i++)
				{
					d += x[i] * x[i - lag];
				}
				ac[lag] = d;
			}
		}
	}
}
