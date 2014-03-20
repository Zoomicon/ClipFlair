namespace org.xiph.speex
{
	/// <summary>Split shape codebook search</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class SplitShapeSearch : org.xiph.speex.CbSearch
	{
		public const int MAX_COMPLEXITY = 10;

		private int subvect_size;

		private int nb_subvect;

		private int[] shape_cb;

		private int shape_cb_size;

		private int shape_bits;

		private int have_sign;

		private int[] ind;

		private int[] signs;

		private float[] t;

		private float[] e;

		private float[] E;

		private float[] r2;

		private float[][] ot;

		private float[][] nt;

		private int[][] nind;

		private int[][] oind;

		/// <summary>Constructor</summary>
		/// <param name="subframesize"></param>
		/// <param name="subvect_size"></param>
		/// <param name="nb_subvect"></param>
		/// <param name="shape_cb"></param>
		/// <param name="shape_bits"></param>
		/// <param name="have_sign"></param>
		public SplitShapeSearch(int subframesize, int subvect_size, int nb_subvect, int[]
			 shape_cb, int shape_bits, int have_sign)
		{
			//  private int   subframesize;
			// Varibles used by the encoder
			//    this.subframesize = subframesize;
			this.subvect_size = subvect_size;
			this.nb_subvect = nb_subvect;
			this.shape_cb = shape_cb;
			this.shape_bits = shape_bits;
			this.have_sign = have_sign;
			this.ind = new int[nb_subvect];
			this.signs = new int[nb_subvect];
			shape_cb_size = 1 << shape_bits;
			ot = new float[][] { new float[subframesize], new float[subframesize], new float[
				subframesize], new float[subframesize], new float[subframesize], new float[subframesize
				], new float[subframesize], new float[subframesize], new float[subframesize], new 
				float[subframesize] };
			nt = new float[][] { new float[subframesize], new float[subframesize], new float[
				subframesize], new float[subframesize], new float[subframesize], new float[subframesize
				], new float[subframesize], new float[subframesize], new float[subframesize], new 
				float[subframesize] };
			oind = new int[][] { new int[nb_subvect], new int[nb_subvect], new int[nb_subvect
				], new int[nb_subvect], new int[nb_subvect], new int[nb_subvect], new int[nb_subvect
				], new int[nb_subvect], new int[nb_subvect], new int[nb_subvect] };
			nind = new int[][] { new int[nb_subvect], new int[nb_subvect], new int[nb_subvect
				], new int[nb_subvect], new int[nb_subvect], new int[nb_subvect], new int[nb_subvect
				], new int[nb_subvect], new int[nb_subvect], new int[nb_subvect] };
			t = new float[subframesize];
			e = new float[subframesize];
			r2 = new float[subframesize];
			E = new float[shape_cb_size];
		}

		/// <summary>Codebook Search Quantification (Split Shape).</summary>
		/// <remarks>Codebook Search Quantification (Split Shape).</remarks>
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
			int j;
			int k;
			int m;
			int n;
			int q;
			float[] resp;
			float[] ndist;
			float[] odist;
			int[] best_index;
			float[] best_dist;
			int N = complexity;
			if (N > 10)
			{
				N = 10;
			}
			resp = new float[shape_cb_size * subvect_size];
			best_index = new int[N];
			best_dist = new float[N];
			ndist = new float[N];
			odist = new float[N];
			for (i = 0; i < N; i++)
			{
				for (j = 0; j < nb_subvect; j++)
				{
					nind[i][j] = oind[i][j] = -1;
				}
			}
			for (j = 0; j < N; j++)
			{
				for (i = 0; i < nsf; i++)
				{
					ot[j][i] = target[i];
				}
			}
			//    System.arraycopy(target, 0, t, 0, nsf);
			for (i = 0; i < shape_cb_size; i++)
			{
				int res;
				int shape;
				res = i * subvect_size;
				shape = i * subvect_size;
				for (j = 0; j < subvect_size; j++)
				{
					resp[res + j] = 0;
					for (k = 0; k <= j; k++)
					{
						resp[res + j] += 0.03125f * shape_cb[shape + k] * r[j - k];
					}
				}
				E[i] = 0;
				for (j = 0; j < subvect_size; j++)
				{
					E[i] += resp[res + j] * resp[res + j];
				}
			}
			for (j = 0; j < N; j++)
			{
				odist[j] = 0;
			}
			for (i = 0; i < nb_subvect; i++)
			{
				int offset = i * subvect_size;
				for (j = 0; j < N; j++)
				{
					ndist[j] = -1;
				}
				for (j = 0; j < N; j++)
				{
					if (have_sign != 0)
					{
						org.xiph.speex.VQ.nbest_sign(ot[j], offset, resp, subvect_size, shape_cb_size, E, 
							N, best_index, best_dist);
					}
					else
					{
						org.xiph.speex.VQ.nbest(ot[j], offset, resp, subvect_size, shape_cb_size, E, N, best_index
							, best_dist);
					}
					for (k = 0; k < N; k++)
					{
						float[] ct;
						float err = 0;
						ct = ot[j];
						for (m = offset; m < offset + subvect_size; m++)
						{
							t[m] = ct[m];
						}
						int rind;
						int res;
						float sign = 1;
						rind = best_index[k];
						if (rind >= shape_cb_size)
						{
							sign = -1;
							rind -= shape_cb_size;
						}
						res = rind * subvect_size;
						if (sign > 0)
						{
							for (m = 0; m < subvect_size; m++)
							{
								t[offset + m] -= resp[res + m];
							}
						}
						else
						{
							for (m = 0; m < subvect_size; m++)
							{
								t[offset + m] += resp[res + m];
							}
						}
						err = odist[j];
						for (m = offset; m < offset + subvect_size; m++)
						{
							err += t[m] * t[m];
						}
						if (err < ndist[N - 1] || ndist[N - 1] < -0.5f)
						{
							for (m = offset + subvect_size; m < nsf; m++)
							{
								t[m] = ct[m];
							}
							for (m = 0; m < subvect_size; m++)
							{
								float g;
								//int rind;
								//float sign = 1;
                                sign = 1;
								rind = best_index[k];
								if (rind >= shape_cb_size)
								{
									sign = -1;
									rind -= shape_cb_size;
								}
								g = sign * 0.03125f * shape_cb[rind * subvect_size + m];
								q = subvect_size - m;
								for (n = offset + subvect_size; n < nsf; n++, q++)
								{
									t[n] -= g * r[q];
								}
							}
							for (m = 0; m < N; m++)
							{
								if (err < ndist[m] || ndist[m] < -0.5f)
								{
									for (n = N - 1; n > m; n--)
									{
										for (q = offset + subvect_size; q < nsf; q++)
										{
											nt[n][q] = nt[n - 1][q];
										}
										for (q = 0; q < nb_subvect; q++)
										{
											nind[n][q] = nind[n - 1][q];
										}
										ndist[n] = ndist[n - 1];
									}
									for (q = offset + subvect_size; q < nsf; q++)
									{
										nt[m][q] = t[q];
									}
									for (q = 0; q < nb_subvect; q++)
									{
										nind[m][q] = oind[j][q];
									}
									nind[m][i] = best_index[k];
									ndist[m] = err;
									break;
								}
							}
						}
					}
					if (i == 0)
					{
						break;
					}
				}
				float[][] tmp2;
				tmp2 = ot;
				ot = nt;
				nt = tmp2;
				for (j = 0; j < N; j++)
				{
					for (m = 0; m < nb_subvect; m++)
					{
						oind[j][m] = nind[j][m];
					}
				}
				for (j = 0; j < N; j++)
				{
					odist[j] = ndist[j];
				}
			}
			for (i = 0; i < nb_subvect; i++)
			{
				ind[i] = nind[0][i];
				bits.pack(ind[i], shape_bits + have_sign);
			}
			for (i = 0; i < nb_subvect; i++)
			{
				int rind;
				float sign = 1;
				rind = ind[i];
				if (rind >= shape_cb_size)
				{
					sign = -1;
					rind -= shape_cb_size;
				}
				for (j = 0; j < subvect_size; j++)
				{
					e[subvect_size * i + j] = sign * 0.03125f * shape_cb[rind * subvect_size + j];
				}
			}
			for (j = 0; j < nsf; j++)
			{
				exc[es + j] += e[j];
			}
			org.xiph.speex.Filters.syn_percep_zero(e, 0, ak, awk1, awk2, r2, nsf, p);
			for (j = 0; j < nsf; j++)
			{
				target[j] -= r2[j];
			}
		}

		/// <summary>Codebook Search Unquantification (Split Shape).</summary>
		/// <remarks>Codebook Search Unquantification (Split Shape).</remarks>
		/// <param name="exc">- excitation array.</param>
		/// <param name="es">- position in excitation array.</param>
		/// <param name="nsf">- number of samples in subframe.</param>
		/// <param name="bits">- Speex bits buffer.</param>
		public sealed override void unquant(float[] exc, int es, int nsf, org.xiph.speex.Bits
			 bits)
		{
			int i;
			int j;
			for (i = 0; i < nb_subvect; i++)
			{
				if (have_sign != 0)
				{
					signs[i] = bits.unpack(1);
				}
				else
				{
					signs[i] = 0;
				}
				ind[i] = bits.unpack(shape_bits);
			}
			for (i = 0; i < nb_subvect; i++)
			{
				float s = 1.0f;
				if (signs[i] != 0)
				{
					s = -1.0f;
				}
				for (j = 0; j < subvect_size; j++)
				{
					exc[es + subvect_size * i + j] += s * 0.03125f * (float)shape_cb[ind[i] * subvect_size
						 + j];
				}
			}
		}
	}
}
