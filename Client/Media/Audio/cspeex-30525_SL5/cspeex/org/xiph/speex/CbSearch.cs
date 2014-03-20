namespace org.xiph.speex
{
	/// <summary>Abstract class that is the base for the various Codebook search methods.
	/// 	</summary>
	/// <remarks>Abstract class that is the base for the various Codebook search methods.
	/// 	</remarks>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public abstract class CbSearch
	{
		/// <summary>Codebook Search Quantification.</summary>
		/// <remarks>Codebook Search Quantification.</remarks>
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
		public abstract void quant(float[] target, float[] ak, float[] awk1, float[] awk2
			, int p, int nsf, float[] exc, int es, float[] r, org.xiph.speex.Bits bits, int 
			complexity);

		/// <summary>Codebook Search Unquantification.</summary>
		/// <remarks>Codebook Search Unquantification.</remarks>
		/// <param name="exc">- excitation array.</param>
		/// <param name="es">- position in excitation array.</param>
		/// <param name="nsf">- number of samples in subframe.</param>
		/// <param name="bits">- Speex bits buffer.</param>
		public abstract void unquant(float[] exc, int es, int nsf, org.xiph.speex.Bits bits
			);
	}
}
