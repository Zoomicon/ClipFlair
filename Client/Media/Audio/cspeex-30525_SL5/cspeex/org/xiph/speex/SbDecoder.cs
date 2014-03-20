namespace org.xiph.speex
{
	/// <summary>Sideband Speex Decoder</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 156 $</version>
	public class SbDecoder : org.xiph.speex.SbCodec, org.xiph.speex.Decoder
	{
		protected org.xiph.speex.Decoder lowdec;

		protected org.xiph.speex.Stereo stereo;

		protected bool enhanced;

		private float[] innov2;

		/// <summary>Constructor</summary>
		public SbDecoder()
		{
			stereo = new org.xiph.speex.Stereo();
			enhanced = true;
		}

		/// <summary>Wideband initialisation</summary>
		public override void wbinit()
		{
			lowdec = new org.xiph.speex.NbDecoder();
			((org.xiph.speex.NbDecoder)lowdec).nbinit();
			lowdec.setPerceptualEnhancement(enhanced);
			// Initialize SubModes
			base.wbinit();
			// Initialize variables
			init(160, 40, 8, 640, .7f);
		}

		/// <summary>Ultra-wideband initialisation</summary>
		public override void uwbinit()
		{
			lowdec = new org.xiph.speex.SbDecoder();
			((org.xiph.speex.SbDecoder)lowdec).wbinit();
			lowdec.setPerceptualEnhancement(enhanced);
			// Initialize SubModes
			base.uwbinit();
			// Initialize variables
			init(320, 80, 8, 1280, .5f);
		}

		/// <summary>Initialisation</summary>
		/// <param name="frameSize"></param>
		/// <param name="subframeSize"></param>
		/// <param name="lpcSize"></param>
		/// <param name="bufSize"></param>
		/// <param name="foldingGain"></param>
		public override void init(int frameSize, int subframeSize, int lpcSize, int bufSize
			, float foldingGain)
		{
			base.init(frameSize, subframeSize, lpcSize, bufSize, foldingGain);
			excIdx = 0;
			innov2 = new float[subframeSize];
		}

		/// <summary>Decode the given input bits.</summary>
		/// <remarks>Decode the given input bits.</remarks>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="out">- the decoded mono audio frame.</param>
		/// <returns>1 if a terminator was found, 0 if not.</returns>
		/// <exception cref="java.io.StreamCorruptedException">
		/// If there is an error detected in the
		/// data stream.
		/// </exception>
		public virtual int decode(org.xiph.speex.Bits bits, float[] @out)
		{
			int i;
			int sub;
			int wideband;
			int ret;
			float[] low_pi_gain;
			float[] low_exc;
			float[] low_innov;
			ret = lowdec.decode(bits, x0d);
			if (ret != 0)
			{
				return ret;
			}
			bool dtx = lowdec.getDtx();
			if (bits == null)
			{
				decodeLost(@out, dtx);
				return 0;
			}
			wideband = bits.peek();
			if (wideband != 0)
			{
				wideband = bits.unpack(1);
				submodeID = bits.unpack(3);
			}
			else
			{
				submodeID = 0;
			}
			for (i = 0; i < frameSize; i++)
			{
				excBuf[i] = 0;
			}
			if (submodes[submodeID] == null)
			{
				if (dtx)
				{
					decodeLost(@out, true);
					return 0;
				}
				for (i = 0; i < frameSize; i++)
				{
					excBuf[i] = VERY_SMALL;
				}
				first = 1;
				org.xiph.speex.Filters.iir_mem2(excBuf, excIdx, interp_qlpc, high, 0, frameSize, 
					lpcSize, mem_sp);
				filters.fir_mem_up(x0d, h0, y0, fullFrameSize, QMF_ORDER, g0_mem);
				filters.fir_mem_up(high, h1, y1, fullFrameSize, QMF_ORDER, g1_mem);
				for (i = 0; i < fullFrameSize; i++)
				{
					@out[i] = 2 * (y0[i] - y1[i]);
				}
				return 0;
			}
			low_pi_gain = lowdec.getPiGain();
			low_exc = lowdec.getExc();
			low_innov = lowdec.getInnov();
			submodes[submodeID].lsqQuant.unquant(qlsp, lpcSize, bits);
			if (first != 0)
			{
				for (i = 0; i < lpcSize; i++)
				{
					old_qlsp[i] = qlsp[i];
				}
			}
			for (sub = 0; sub < nbSubframes; sub++)
			{
				float tmp;
				float filter_ratio;
				float el = 0.0f;
				float rl = 0.0f;
				float rh = 0.0f;
				int subIdx = subframeSize * sub;
				tmp = (1.0f + sub) / nbSubframes;
				for (i = 0; i < lpcSize; i++)
				{
					interp_qlsp[i] = (1 - tmp) * old_qlsp[i] + tmp * qlsp[i];
				}
				org.xiph.speex.Lsp.enforce_margin(interp_qlsp, lpcSize, .05f);
				for (i = 0; i < lpcSize; i++)
				{
					interp_qlsp[i] = (float)System.Math.Cos(interp_qlsp[i]);
				}
				m_lsp.lsp2lpc(interp_qlsp, interp_qlpc, lpcSize);
				if (enhanced)
				{
					float k1;
					float k2;
					float k3;
					k1 = submodes[submodeID].lpc_enh_k1;
					k2 = submodes[submodeID].lpc_enh_k2;
					k3 = k1 - k2;
					org.xiph.speex.Filters.bw_lpc(k1, interp_qlpc, awk1, lpcSize);
					org.xiph.speex.Filters.bw_lpc(k2, interp_qlpc, awk2, lpcSize);
					org.xiph.speex.Filters.bw_lpc(k3, interp_qlpc, awk3, lpcSize);
				}
				tmp = 1;
				pi_gain[sub] = 0;
				for (i = 0; i <= lpcSize; i++)
				{
					rh += tmp * interp_qlpc[i];
					tmp = -tmp;
					pi_gain[sub] += interp_qlpc[i];
				}
				rl = low_pi_gain[sub];
				rl = 1 / (System.Math.Abs(rl) + .01f);
				rh = 1 / (System.Math.Abs(rh) + .01f);
				filter_ratio = System.Math.Abs(.01f + rh) / (.01f + System.Math.Abs(rl));
				for (i = subIdx; i < subIdx + subframeSize; i++)
				{
					excBuf[i] = 0;
				}
				if (submodes[submodeID].innovation == null)
				{
					float g;
					int quant;
					quant = bits.unpack(5);
					g = (float)System.Math.Exp(((double)quant - 10) / 8.0);
					g /= filter_ratio;
					for (i = subIdx; i < subIdx + subframeSize; i++)
					{
						excBuf[i] = foldingGain * g * low_innov[i];
					}
				}
				else
				{
					float gc;
					float scale;
					int qgc = bits.unpack(4);
					for (i = subIdx; i < subIdx + subframeSize; i++)
					{
						el += low_exc[i] * low_exc[i];
					}
					gc = (float)System.Math.Exp((1 / 3.7f) * qgc - 2);
					scale = gc * (float)System.Math.Sqrt(1 + el) / filter_ratio;
					submodes[submodeID].innovation.unquant(excBuf, subIdx, subframeSize, bits);
					for (i = subIdx; i < subIdx + subframeSize; i++)
					{
						excBuf[i] *= scale;
					}
					if (submodes[submodeID].double_codebook != 0)
					{
						for (i = 0; i < subframeSize; i++)
						{
							innov2[i] = 0;
						}
						submodes[submodeID].innovation.unquant(innov2, 0, subframeSize, bits);
						for (i = 0; i < subframeSize; i++)
						{
							innov2[i] *= scale * (1 / 2.5f);
						}
						for (i = 0; i < subframeSize; i++)
						{
							excBuf[subIdx + i] += innov2[i];
						}
					}
				}
				for (i = subIdx; i < subIdx + subframeSize; i++)
				{
					high[i] = excBuf[i];
				}
				if (enhanced)
				{
					org.xiph.speex.Filters.filter_mem2(high, subIdx, awk2, awk1, subframeSize, lpcSize
						, mem_sp, lpcSize);
					org.xiph.speex.Filters.filter_mem2(high, subIdx, awk3, interp_qlpc, subframeSize, 
						lpcSize, mem_sp, 0);
				}
				else
				{
					for (i = 0; i < lpcSize; i++)
					{
						mem_sp[lpcSize + i] = 0;
					}
					org.xiph.speex.Filters.iir_mem2(high, subIdx, interp_qlpc, high, subIdx, subframeSize
						, lpcSize, mem_sp);
				}
			}
			filters.fir_mem_up(x0d, h0, y0, fullFrameSize, QMF_ORDER, g0_mem);
			filters.fir_mem_up(high, h1, y1, fullFrameSize, QMF_ORDER, g1_mem);
			for (i = 0; i < fullFrameSize; i++)
			{
				@out[i] = 2 * (y0[i] - y1[i]);
			}
			for (i = 0; i < lpcSize; i++)
			{
				old_qlsp[i] = qlsp[i];
			}
			first = 0;
			return 0;
		}

		/// <summary>Decode when packets are lost.</summary>
		/// <remarks>Decode when packets are lost.</remarks>
		/// <param name="out">- the generated mono audio frame.</param>
		/// <param name="dtx"></param>
		/// <returns>0 if successful.</returns>
		public virtual int decodeLost(float[] @out, bool dtx)
		{
			int i;
			int saved_modeid = 0;
			if (dtx)
			{
				saved_modeid = submodeID;
				submodeID = 1;
			}
			else
			{
				org.xiph.speex.Filters.bw_lpc(0.99f, interp_qlpc, interp_qlpc, lpcSize);
			}
			first = 1;
			awk1 = new float[lpcSize + 1];
			awk2 = new float[lpcSize + 1];
			awk3 = new float[lpcSize + 1];
			if (enhanced)
			{
				float k1;
				float k2;
				float k3;
				if (submodes[submodeID] != null)
				{
					k1 = submodes[submodeID].lpc_enh_k1;
					k2 = submodes[submodeID].lpc_enh_k2;
				}
				else
				{
					k1 = k2 = 0.7f;
				}
				k3 = k1 - k2;
				org.xiph.speex.Filters.bw_lpc(k1, interp_qlpc, awk1, lpcSize);
				org.xiph.speex.Filters.bw_lpc(k2, interp_qlpc, awk2, lpcSize);
				org.xiph.speex.Filters.bw_lpc(k3, interp_qlpc, awk3, lpcSize);
			}
			if (!dtx)
			{
				for (i = 0; i < frameSize; i++)
				{
					excBuf[excIdx + i] *= .9f;
				}
			}
			for (i = 0; i < frameSize; i++)
			{
				high[i] = excBuf[excIdx + i];
			}
			if (enhanced)
			{
				org.xiph.speex.Filters.filter_mem2(high, 0, awk2, awk1, high, 0, frameSize, lpcSize
					, mem_sp, lpcSize);
				org.xiph.speex.Filters.filter_mem2(high, 0, awk3, interp_qlpc, high, 0, frameSize
					, lpcSize, mem_sp, 0);
			}
			else
			{
				for (i = 0; i < lpcSize; i++)
				{
					mem_sp[lpcSize + i] = 0;
				}
				org.xiph.speex.Filters.iir_mem2(high, 0, interp_qlpc, high, 0, frameSize, lpcSize
					, mem_sp);
			}
			filters.fir_mem_up(x0d, h0, y0, fullFrameSize, QMF_ORDER, g0_mem);
			filters.fir_mem_up(high, h1, y1, fullFrameSize, QMF_ORDER, g1_mem);
			for (i = 0; i < fullFrameSize; i++)
			{
				@out[i] = 2 * (y0[i] - y1[i]);
			}
			if (dtx)
			{
				submodeID = saved_modeid;
			}
			return 0;
		}

		/// <summary>Decode the given bits to stereo.</summary>
		/// <remarks>Decode the given bits to stereo.</remarks>
		/// <param name="data">
		/// - float array of size 2*frameSize, that contains the mono
		/// audio samples in the first half. When the function has completed, the
		/// array will contain the interlaced stereo audio samples.
		/// </param>
		/// <param name="frameSize">- the size of a frame of mono audio samples.</param>
		public virtual void decodeStereo(float[] data, int frameSize)
		{
			stereo.decode(data, frameSize);
		}

		/// <summary>Enables or disables perceptual enhancement.</summary>
		/// <remarks>Enables or disables perceptual enhancement.</remarks>
		/// <param name="enhanced"></param>
		public virtual void setPerceptualEnhancement(bool enhanced)
		{
			this.enhanced = enhanced;
		}

		/// <summary>Returns whether perceptual enhancement is enabled or disabled.</summary>
		/// <remarks>Returns whether perceptual enhancement is enabled or disabled.</remarks>
		/// <returns>whether perceptual enhancement is enabled or disabled.</returns>
		public virtual bool getPerceptualEnhancement()
		{
			return enhanced;
		}
	}
}
