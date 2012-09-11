namespace org.xiph.speex
{
	/// <summary>LSP Quantisation and Unquantisation (Lbr)</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class LbrLspQuant : org.xiph.speex.LspQuant
	{
		/// <summary>Line Spectral Pair Quantification (Lbr).</summary>
		/// <remarks>Line Spectral Pair Quantification (Lbr).</remarks>
		/// <param name="lsp">- Line Spectral Pairs table.</param>
		/// <param name="qlsp">- Quantified Line Spectral Pairs table.</param>
		/// <param name="order"></param>
		/// <param name="bits">- Speex bits buffer.</param>
		public sealed override void quant(float[] lsp, float[] qlsp, int order, org.xiph.speex.Bits
			 bits)
		{
			int i;
			float tmp1;
			float tmp2;
			int id;
			float[] quant_weight = new float[MAX_LSP_SIZE];
			for (i = 0; i < order; i++)
			{
				qlsp[i] = lsp[i];
			}
			quant_weight[0] = 1 / (qlsp[1] - qlsp[0]);
			quant_weight[order - 1] = 1 / (qlsp[order - 1] - qlsp[order - 2]);
			for (i = 1; i < order - 1; i++)
			{
				tmp1 = 1 / ((.15f + qlsp[i] - qlsp[i - 1]) * (.15f + qlsp[i] - qlsp[i - 1]));
				tmp2 = 1 / ((.15f + qlsp[i + 1] - qlsp[i]) * (.15f + qlsp[i + 1] - qlsp[i]));
				quant_weight[i] = tmp1 > tmp2 ? tmp1 : tmp2;
			}
			for (i = 0; i < order; i++)
			{
				qlsp[i] -= (.25f * i + .25f);
			}
			for (i = 0; i < order; i++)
			{
				qlsp[i] *= 256;
			}
			id = lsp_quant(qlsp, 0, cdbk_nb, NB_CDBK_SIZE, order);
			bits.pack(id, 6);
			for (i = 0; i < order; i++)
			{
				qlsp[i] *= 2;
			}
			id = lsp_weight_quant(qlsp, 0, quant_weight, 0, cdbk_nb_low1, NB_CDBK_SIZE_LOW1, 
				5);
			bits.pack(id, 6);
			id = lsp_weight_quant(qlsp, 5, quant_weight, 5, cdbk_nb_high1, NB_CDBK_SIZE_HIGH1
				, 5);
			bits.pack(id, 6);
			for (i = 0; i < order; i++)
			{
				qlsp[i] *= 0.0019531f;
			}
			for (i = 0; i < order; i++)
			{
				qlsp[i] = lsp[i] - qlsp[i];
			}
		}

		/// <summary>Line Spectral Pair Unquantification (Lbr).</summary>
		/// <remarks>Line Spectral Pair Unquantification (Lbr).</remarks>
		/// <param name="lsp">- Line Spectral Pairs table.</param>
		/// <param name="order"></param>
		/// <param name="bits">- Speex bits buffer.</param>
		public sealed override void unquant(float[] lsp, int order, org.xiph.speex.Bits bits
			)
		{
			for (int i = 0; i < order; i++)
			{
				lsp[i] = .25f * i + .25f;
			}
			unpackPlus(lsp, cdbk_nb, bits, 0.0039062f, 10, 0);
			unpackPlus(lsp, cdbk_nb_low1, bits, 0.0019531f, 5, 0);
			unpackPlus(lsp, cdbk_nb_high1, bits, 0.0019531f, 5, 5);
		}
	}
}
