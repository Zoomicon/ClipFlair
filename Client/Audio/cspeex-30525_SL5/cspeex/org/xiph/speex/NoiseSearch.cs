namespace org.xiph.speex
{
	/// <summary>Noise codebook search</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class NoiseSearch : org.xiph.speex.CbSearch
	{
		/// <summary>Codebook Search Quantification (Noise).</summary>
		/// <remarks>Codebook Search Quantification (Noise).</remarks>
		/// <param name="target">target vector</param>
		/// <param name="ak">LPCs for this subframe</param>
		/// <param name="awk1">Weighted LPCs for this subframe</param>
		/// <param name="awk2">Weighted LPCs for this subframe</param>
		/// <param name="p">number of LPC coeffs</param>
		/// <param name="nsf">number of samples in subframe</param>
		/// <param name="exc">excitation array.</param>
		/// <param name="es">position in excitation array.</param>
		/// <param name="r"></param>
		/// <param name="bits">Speex bits buffer.</param>
		/// <param name="complexity"></param>
		public sealed override void quant(float[] target, float[] ak, float[] awk1, float
			[] awk2, int p, int nsf, float[] exc, int es, float[] r, org.xiph.speex.Bits bits
			, int complexity)
		{
			int i;
			float[] tmp = new float[nsf];
			org.xiph.speex.Filters.residue_percep_zero(target, 0, ak, awk1, awk2, tmp, nsf, p
				);
			for (i = 0; i < nsf; i++)
			{
				exc[es + i] += tmp[i];
			}
			for (i = 0; i < nsf; i++)
			{
				target[i] = 0;
			}
		}

		/// <summary>Codebook Search Unquantification (Noise).</summary>
		/// <remarks>Codebook Search Unquantification (Noise).</remarks>
		/// <param name="exc">- excitation array.</param>
		/// <param name="es">- position in excitation array.</param>
		/// <param name="nsf">- number of samples in subframe.</param>
		/// <param name="bits">- Speex bits buffer.</param>
		public sealed override void unquant(float[] exc, int es, int nsf, org.xiph.speex.Bits
			 bits)
		{
			for (int i = 0; i < nsf; i++)
			{
				exc[es + i] += (float)(3.0 * (java.util.Random.random() - .5));
			}
		}
	}
}
