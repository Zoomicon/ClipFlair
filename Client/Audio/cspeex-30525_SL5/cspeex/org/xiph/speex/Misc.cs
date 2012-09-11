namespace org.xiph.speex
{
	/// <summary>Miscellaneous functions</summary>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class Misc
	{
		/// <summary>Builds an Asymmetric "pseudo-Hamming" window.</summary>
		/// <remarks>Builds an Asymmetric "pseudo-Hamming" window.</remarks>
		/// <param name="windowSize"></param>
		/// <param name="subFrameSize"></param>
		/// <returns>an Asymmetric "pseudo-Hamming" window.</returns>
		public static float[] window(int windowSize, int subFrameSize)
		{
			int i;
			int part1 = subFrameSize * 7 / 2;
			int part2 = subFrameSize * 5 / 2;
			float[] window = new float[windowSize];
			for (i = 0; i < part1; i++)
			{
				window[i] = (float)(0.54 - 0.46 * System.Math.Cos(System.Math.PI * i / part1));
			}
			for (i = 0; i < part2; i++)
			{
				window[part1 + i] = (float)(0.54 + 0.46 * System.Math.Cos(System.Math.PI * i / part2
					));
			}
			return window;
		}

		/// <summary>Create the window for autocorrelation (lag-windowing).</summary>
		/// <remarks>Create the window for autocorrelation (lag-windowing).</remarks>
		/// <param name="lpcSize"></param>
		/// <param name="lagFactor"></param>
		/// <returns>the window for autocorrelation.</returns>
		public static float[] lagWindow(int lpcSize, float lagFactor)
		{
			float[] lagWindow = new float[lpcSize + 1];
			for (int i = 0; i < lpcSize + 1; i++)
			{
				lagWindow[i] = (float)System.Math.Exp(-0.5 * (2 * System.Math.PI * lagFactor * i)
					 * (2 * System.Math.PI * lagFactor * i));
			}
			return lagWindow;
		}
	}
}
