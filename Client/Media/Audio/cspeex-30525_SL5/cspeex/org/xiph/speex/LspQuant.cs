namespace org.xiph.speex
{
	/// <summary>
	/// Abstract class that is the base for the various LSP Quantisation and
	/// Unquantisation methods.
	/// </summary>
	/// <remarks>
	/// Abstract class that is the base for the various LSP Quantisation and
	/// Unquantisation methods.
	/// </remarks>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public abstract class LspQuant : org.xiph.speex.Codebook
	{
		public const int MAX_LSP_SIZE = 20;

		/// <summary>Constructor.</summary>
		/// <remarks>Constructor.</remarks>
		protected LspQuant()
		{
		}

		/// <summary>Line Spectral Pair Quantification.</summary>
		/// <remarks>Line Spectral Pair Quantification.</remarks>
		/// <param name="lsp">- Line Spectral Pairs table.</param>
		/// <param name="qlsp">- Quantified Line Spectral Pairs table.</param>
		/// <param name="order"></param>
		/// <param name="bits">- Speex bits buffer.</param>
		public abstract void quant(float[] lsp, float[] qlsp, int order, org.xiph.speex.Bits
			 bits);

		/// <summary>Line Spectral Pair Unquantification.</summary>
		/// <remarks>Line Spectral Pair Unquantification.</remarks>
		/// <param name="lsp">- Line Spectral Pairs table.</param>
		/// <param name="order"></param>
		/// <param name="bits">- Speex bits buffer.</param>
		public abstract void unquant(float[] lsp, int order, org.xiph.speex.Bits bits);

		/// <summary>
		/// Read the next 6 bits from the buffer, and using the value read and the
		/// given codebook, rebuild LSP table.
		/// </summary>
		/// <remarks>
		/// Read the next 6 bits from the buffer, and using the value read and the
		/// given codebook, rebuild LSP table.
		/// </remarks>
		/// <param name="lsp"></param>
		/// <param name="tab"></param>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="k"></param>
		/// <param name="ti"></param>
		/// <param name="li"></param>
		protected virtual void unpackPlus(float[] lsp, int[] tab, org.xiph.speex.Bits bits
			, float k, int ti, int li)
		{
			int id = bits.unpack(6);
			for (int i = 0; i < ti; i++)
			{
				lsp[i + li] += k * (float)tab[id * ti + i];
			}
		}

		/// <summary>
		/// LSP quantification
		/// Note: x is modified
		/// </summary>
		/// <param name="x"></param>
		/// <param name="xs"></param>
		/// <param name="cdbk"></param>
		/// <param name="nbVec"></param>
		/// <param name="nbDim"></param>
		/// <returns>
		/// the index of the best match in the codebook
		/// (NB x is also modified).
		/// </returns>
		protected static int lsp_quant(float[] x, int xs, int[] cdbk, int nbVec, int nbDim
			)
		{
			int i;
			int j;
			float dist;
			float tmp;
			float best_dist = 0;
			int best_id = 0;
			int ptr = 0;
			for (i = 0; i < nbVec; i++)
			{
				dist = 0;
				for (j = 0; j < nbDim; j++)
				{
					tmp = (x[xs + j] - cdbk[ptr++]);
					dist += tmp * tmp;
				}
				if (dist < best_dist || i == 0)
				{
					best_dist = dist;
					best_id = i;
				}
			}
			for (j = 0; j < nbDim; j++)
			{
				x[xs + j] -= cdbk[best_id * nbDim + j];
			}
			return best_id;
		}

		/// <summary>
		/// LSP weighted quantification
		/// Note: x is modified
		/// </summary>
		/// <param name="x"></param>
		/// <param name="xs"></param>
		/// <param name="weight"></param>
		/// <param name="ws"></param>
		/// <param name="cdbk"></param>
		/// <param name="nbVec"></param>
		/// <param name="nbDim"></param>
		/// <returns>
		/// the index of the best match in the codebook
		/// (NB x is also modified).
		/// </returns>
		protected static int lsp_weight_quant(float[] x, int xs, float[] weight, int ws, 
			int[] cdbk, int nbVec, int nbDim)
		{
			int i;
			int j;
			float dist;
			float tmp;
			float best_dist = 0;
			int best_id = 0;
			int ptr = 0;
			for (i = 0; i < nbVec; i++)
			{
				dist = 0;
				for (j = 0; j < nbDim; j++)
				{
					tmp = (x[xs + j] - cdbk[ptr++]);
					dist += weight[ws + j] * tmp * tmp;
				}
				if (dist < best_dist || i == 0)
				{
					best_dist = dist;
					best_id = i;
				}
			}
			for (j = 0; j < nbDim; j++)
			{
				x[xs + j] -= cdbk[best_id * nbDim + j];
			}
			return best_id;
		}
	}
}
