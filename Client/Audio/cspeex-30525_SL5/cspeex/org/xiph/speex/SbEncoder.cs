namespace org.xiph.speex
{
	/// <summary>Wideband Speex Encoder</summary>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class SbEncoder : org.xiph.speex.SbCodec, org.xiph.speex.Encoder
	{
		/// <summary>The Narrowband Quality map indicates which narrowband submode to use for the given wideband/ultra-wideband quality setting
		/// 	</summary>
		public static readonly int[] NB_QUALITY_MAP = new int[] { 1, 8, 2, 3, 4, 5, 5, 6, 
			6, 7, 7 };

		/// <summary>The Wideband Quality map indicates which sideband submode to use for the given wideband/ultra-wideband quality setting
		/// 	</summary>
		public static readonly int[] WB_QUALITY_MAP = new int[] { 1, 1, 1, 1, 1, 1, 2, 2, 
			3, 3, 4 };

		/// <summary>The Ultra-wideband Quality map indicates which sideband submode to use for the given ultra-wideband quality setting
		/// 	</summary>
		public static readonly int[] UWB_QUALITY_MAP = new int[] { 0, 1, 1, 1, 1, 1, 1, 1
			, 1, 1, 1 };

		/// <summary>The encoder for the lower half of the Spectrum.</summary>
		/// <remarks>The encoder for the lower half of the Spectrum.</remarks>
		protected org.xiph.speex.Encoder lowenc;

		private float[] x1d;

		private float[] h0_mem;

		private float[] buf;

		private float[] swBuf;

		/// <summary>Weighted signal buffer</summary>
		private float[] res;

		private float[] target;

		private float[] window;

		private float[] lagWindow;

		private float[] rc;

		/// <summary>Reflection coefficients</summary>
		private float[] autocorr;

		/// <summary>auto-correlation</summary>
		private float[] lsp;

		/// <summary>LSPs for current frame</summary>
		private float[] old_lsp;

		/// <summary>LSPs for previous frame</summary>
		private float[] interp_lsp;

		/// <summary>Interpolated LSPs</summary>
		private float[] interp_lpc;

		/// <summary>Interpolated LPCs</summary>
		private float[] bw_lpc1;

		/// <summary>LPCs after bandwidth expansion by gamma1 for perceptual weighting</summary>
		private float[] bw_lpc2;

		/// <summary>LPCs after bandwidth expansion by gamma2 for perceptual weighting</summary>
		private float[] mem_sp2;

		private float[] mem_sw;

		protected int nb_modes;

		private bool uwb;

		protected int complexity;

		/// <summary>Complexity setting (0-10 from least complex to most complex)</summary>
		protected int vbr_enabled;

		/// <summary>1 for enabling VBR, 0 otherwise</summary>
		protected int vad_enabled;

		/// <summary>1 for enabling VAD, 0 otherwise</summary>
		protected int abr_enabled;

		/// <summary>ABR setting (in bps), 0 if off</summary>
		protected float vbr_quality;

		/// <summary>Quality setting for VBR encoding</summary>
		protected float relative_quality;

		/// <summary>Relative quality that will be needed by VBR</summary>
		protected float abr_drift;

		protected float abr_drift2;

		protected float abr_count;

		protected int sampling_rate;

		protected int submodeSelect;

		/// <summary>Wideband initialisation</summary>
		public override void wbinit()
		{
			lowenc = new org.xiph.speex.NbEncoder();
			((org.xiph.speex.NbEncoder)lowenc).nbinit();
			// Initialize SubModes
			base.wbinit();
			// Initialize variables
			init(160, 40, 8, 640, .9f);
			uwb = false;
			nb_modes = 5;
			sampling_rate = 16000;
		}

		/// <summary>Ultra-wideband initialisation</summary>
		public override void uwbinit()
		{
			lowenc = new org.xiph.speex.SbEncoder();
			((org.xiph.speex.SbEncoder)lowenc).wbinit();
			// Initialize SubModes
			base.uwbinit();
			// Initialize variables
			init(320, 80, 8, 1280, .7f);
			uwb = true;
			nb_modes = 2;
			sampling_rate = 32000;
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
			complexity = 3;
			// in C it's 2 here, but set to 3 automatically by the encoder
			vbr_enabled = 0;
			// disabled by default
			vad_enabled = 0;
			// disabled by default
			abr_enabled = 0;
			// disabled by default
			vbr_quality = 8;
			submodeSelect = submodeID;
			x1d = new float[frameSize];
			h0_mem = new float[QMF_ORDER];
			buf = new float[windowSize];
			swBuf = new float[frameSize];
			res = new float[frameSize];
			target = new float[subframeSize];
			window = org.xiph.speex.Misc.window(windowSize, subframeSize);
			lagWindow = org.xiph.speex.Misc.lagWindow(lpcSize, lag_factor);
			rc = new float[lpcSize];
			autocorr = new float[lpcSize + 1];
			lsp = new float[lpcSize];
			old_lsp = new float[lpcSize];
			interp_lsp = new float[lpcSize];
			interp_lpc = new float[lpcSize + 1];
			bw_lpc1 = new float[lpcSize + 1];
			bw_lpc2 = new float[lpcSize + 1];
			mem_sp2 = new float[lpcSize];
			mem_sw = new float[lpcSize];
			abr_count = 0;
		}

		/// <summary>Encode the given input signal.</summary>
		/// <remarks>Encode the given input signal.</remarks>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="in">- the raw mono audio frame to encode.</param>
		/// <returns>1 if successful.</returns>
		public virtual int encode(org.xiph.speex.Bits bits, float[] @in)
		{
			int i;
			float[] mem;
			float[] innov;
			float[] syn_resp;
			float[] low_pi_gain;
			float[] low_exc;
			float[] low_innov;
			int dtx;
			org.xiph.speex.Filters.qmf_decomp(@in, h0, x0d, x1d, fullFrameSize, QMF_ORDER, h0_mem
				);
			lowenc.encode(bits, x0d);
			for (i = 0; i < windowSize - frameSize; i++)
			{
				high[i] = high[frameSize + i];
			}
			for (i = 0; i < frameSize; i++)
			{
				high[windowSize - frameSize + i] = x1d[i];
			}
			System.Array.Copy(excBuf, frameSize, excBuf, 0, bufSize - frameSize);
			low_pi_gain = lowenc.getPiGain();
			low_exc = lowenc.getExc();
			low_innov = lowenc.getInnov();
			int low_mode = lowenc.getMode();
			if (low_mode == 0)
			{
				dtx = 1;
			}
			else
			{
				dtx = 0;
			}
			for (i = 0; i < windowSize; i++)
			{
				buf[i] = high[i] * window[i];
			}
			org.xiph.speex.Lpc.autocorr(buf, autocorr, lpcSize + 1, windowSize);
			autocorr[0] += 1;
			autocorr[0] *= lpc_floor;
			for (i = 0; i < lpcSize + 1; i++)
			{
				autocorr[i] *= lagWindow[i];
			}
			org.xiph.speex.Lpc.wld(lpc, autocorr, rc, lpcSize);
			// tmperr
			System.Array.Copy(lpc, 0, lpc, 1, lpcSize);
			lpc[0] = 1;
			int roots = org.xiph.speex.Lsp.lpc2lsp(lpc, lpcSize, lsp, 15, 0.2f);
			if (roots != lpcSize)
			{
				roots = org.xiph.speex.Lsp.lpc2lsp(lpc, lpcSize, lsp, 11, 0.02f);
				if (roots != lpcSize)
				{
					for (i = 0; i < lpcSize; i++)
					{
						lsp[i] = (float)System.Math.Cos(System.Math.PI * ((float)(i + 1)) / (lpcSize + 1)
							);
					}
				}
			}
			for (i = 0; i < lpcSize; i++)
			{
				lsp[i] = (float)System.Math.Acos(lsp[i]);
			}
			float lsp_dist = 0;
			for (i = 0; i < lpcSize; i++)
			{
				lsp_dist += (old_lsp[i] - lsp[i]) * (old_lsp[i] - lsp[i]);
			}
			if ((vbr_enabled != 0 || vad_enabled != 0) && dtx == 0)
			{
				float e_low = 0;
				float e_high = 0;
				float ratio;
				if (abr_enabled != 0)
				{
					float qual_change = 0;
					if (abr_drift2 * abr_drift > 0)
					{
						qual_change = -.00001f * abr_drift / (1 + abr_count);
						if (qual_change > .1f)
						{
							qual_change = .1f;
						}
						if (qual_change < -.1f)
						{
							qual_change = -.1f;
						}
					}
					vbr_quality += qual_change;
					if (vbr_quality > 10)
					{
						vbr_quality = 10;
					}
					if (vbr_quality < 0)
					{
						vbr_quality = 0;
					}
				}
				for (i = 0; i < frameSize; i++)
				{
					e_low += x0d[i] * x0d[i];
					e_high += high[i] * high[i];
				}
				ratio = (float)System.Math.Log((1 + e_high) / (1 + e_low));
				relative_quality = lowenc.getRelativeQuality();
				if (ratio < -4)
				{
					ratio = -4;
				}
				if (ratio > 2)
				{
					ratio = 2;
				}
				if (vbr_enabled != 0)
				{
					int modeid;
					modeid = nb_modes - 1;
					relative_quality += 1.0f * (ratio + 2f);
					if (relative_quality < -1)
					{
						relative_quality = -1;
					}
					while (modeid != 0)
					{
						int v1;
						float thresh;
						v1 = (int)System.Math.Floor(vbr_quality);
						if (v1 == 10)
						{
							thresh = org.xiph.speex.Vbr.hb_thresh[modeid][v1];
						}
						else
						{
							thresh = (vbr_quality - v1) * org.xiph.speex.Vbr.hb_thresh[modeid][v1 + 1] + (1 +
								 v1 - vbr_quality) * org.xiph.speex.Vbr.hb_thresh[modeid][v1];
						}
						if (relative_quality >= thresh)
						{
							break;
						}
						modeid--;
					}
					setMode(modeid);
					if (abr_enabled != 0)
					{
						int bitrate;
						bitrate = getBitRate();
						abr_drift += (bitrate - abr_enabled);
						abr_drift2 = .95f * abr_drift2 + .05f * (bitrate - abr_enabled);
						abr_count += 1.0f;
					}
				}
				else
				{
					int modeid;
					if (relative_quality < 2.0)
					{
						modeid = 1;
					}
					else
					{
						modeid = submodeSelect;
					}
					submodeID = modeid;
				}
			}
			bits.pack(1, 1);
			if (dtx != 0)
			{
				bits.pack(0, SB_SUBMODE_BITS);
			}
			else
			{
				bits.pack(submodeID, SB_SUBMODE_BITS);
			}
			if (dtx != 0 || submodes[submodeID] == null)
			{
				for (i = 0; i < frameSize; i++)
				{
					excBuf[excIdx + i] = swBuf[i] = VERY_SMALL;
				}
				for (i = 0; i < lpcSize; i++)
				{
					mem_sw[i] = 0;
				}
				first = 1;
				org.xiph.speex.Filters.iir_mem2(excBuf, excIdx, interp_qlpc, high, 0, subframeSize
					, lpcSize, mem_sp);
				filters.fir_mem_up(x0d, h0, y0, fullFrameSize, QMF_ORDER, g0_mem);
				filters.fir_mem_up(high, h1, y1, fullFrameSize, QMF_ORDER, g1_mem);
				for (i = 0; i < fullFrameSize; i++)
				{
					@in[i] = 2 * (y0[i] - y1[i]);
				}
				if (dtx != 0)
				{
					return 0;
				}
				else
				{
					return 1;
				}
			}
			submodes[submodeID].lsqQuant.quant(lsp, qlsp, lpcSize, bits);
			if (first != 0)
			{
				for (i = 0; i < lpcSize; i++)
				{
					old_lsp[i] = lsp[i];
				}
				for (i = 0; i < lpcSize; i++)
				{
					old_qlsp[i] = qlsp[i];
				}
			}
			mem = new float[lpcSize];
			syn_resp = new float[subframeSize];
			innov = new float[subframeSize];
			for (int sub = 0; sub < nbSubframes; sub++)
			{
				float tmp;
				float filter_ratio;
				int exc;
				int sp;
				int sw;
				int resp;
				int offset;
				float rl;
				float rh;
				float eh = 0;
				float el = 0;
				//      int fold;
				offset = subframeSize * sub;
				sp = offset;
				exc = excIdx + offset;
				resp = offset;
				sw = offset;
				tmp = (1.0f + sub) / nbSubframes;
				for (i = 0; i < lpcSize; i++)
				{
					interp_lsp[i] = (1 - tmp) * old_lsp[i] + tmp * lsp[i];
				}
				for (i = 0; i < lpcSize; i++)
				{
					interp_qlsp[i] = (1 - tmp) * old_qlsp[i] + tmp * qlsp[i];
				}
				org.xiph.speex.Lsp.enforce_margin(interp_lsp, lpcSize, .05f);
				org.xiph.speex.Lsp.enforce_margin(interp_qlsp, lpcSize, .05f);
				for (i = 0; i < lpcSize; i++)
				{
					interp_lsp[i] = (float)System.Math.Cos(interp_lsp[i]);
				}
				for (i = 0; i < lpcSize; i++)
				{
					interp_qlsp[i] = (float)System.Math.Cos(interp_qlsp[i]);
				}
				m_lsp.lsp2lpc(interp_lsp, interp_lpc, lpcSize);
				m_lsp.lsp2lpc(interp_qlsp, interp_qlpc, lpcSize);
				org.xiph.speex.Filters.bw_lpc(gamma1, interp_lpc, bw_lpc1, lpcSize);
				org.xiph.speex.Filters.bw_lpc(gamma2, interp_lpc, bw_lpc2, lpcSize);
				rl = rh = 0;
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
				//      fold = filter_ratio<5 ? 1 : 0;
				//      fold=0;
				org.xiph.speex.Filters.fir_mem2(high, sp, interp_qlpc, excBuf, exc, subframeSize, 
					lpcSize, mem_sp2);
				for (i = 0; i < subframeSize; i++)
				{
					eh += excBuf[exc + i] * excBuf[exc + i];
				}
				if (submodes[submodeID].innovation == null)
				{
					float g;
					for (i = 0; i < subframeSize; i++)
					{
						el += low_innov[offset + i] * low_innov[offset + i];
					}
					g = eh / (.01f + el);
					g = (float)System.Math.Sqrt(g);
					g *= filter_ratio;
					int quant = (int)System.Math.Floor(.5 + 10 + 8.0 * System.Math.Log((g + .0001)));
					if (quant < 0)
					{
						quant = 0;
					}
					if (quant > 31)
					{
						quant = 31;
					}
					bits.pack(quant, 5);
					g = (float)(.1 * System.Math.Exp(quant / 9.4));
					g /= filter_ratio;
				}
				else
				{
					float gc;
					float scale;
					float scale_1;
					for (i = 0; i < subframeSize; i++)
					{
						el += low_exc[offset + i] * low_exc[offset + i];
					}
					gc = (float)(System.Math.Sqrt(1 + eh) * filter_ratio / System.Math.Sqrt((1 + el) 
						* subframeSize));
					int qgc = (int)System.Math.Floor(.5 + 3.7 * (System.Math.Log(gc) + 2));
					if (qgc < 0)
					{
						qgc = 0;
					}
					if (qgc > 15)
					{
						qgc = 15;
					}
					bits.pack(qgc, 4);
					gc = (float)System.Math.Exp((1 / 3.7) * qgc - 2);
					scale = gc * (float)System.Math.Sqrt(1 + el) / filter_ratio;
					scale_1 = 1 / scale;
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[exc + i] = 0;
					}
					excBuf[exc] = 1;
					org.xiph.speex.Filters.syn_percep_zero(excBuf, exc, interp_qlpc, bw_lpc1, bw_lpc2
						, syn_resp, subframeSize, lpcSize);
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[exc + i] = 0;
					}
					for (i = 0; i < lpcSize; i++)
					{
						mem[i] = mem_sp[i];
					}
					org.xiph.speex.Filters.iir_mem2(excBuf, exc, interp_qlpc, excBuf, exc, subframeSize
						, lpcSize, mem);
					for (i = 0; i < lpcSize; i++)
					{
						mem[i] = mem_sw[i];
					}
					org.xiph.speex.Filters.filter_mem2(excBuf, exc, bw_lpc1, bw_lpc2, res, resp, subframeSize
						, lpcSize, mem, 0);
					for (i = 0; i < lpcSize; i++)
					{
						mem[i] = mem_sw[i];
					}
					org.xiph.speex.Filters.filter_mem2(high, sp, bw_lpc1, bw_lpc2, swBuf, sw, subframeSize
						, lpcSize, mem, 0);
					for (i = 0; i < subframeSize; i++)
					{
						target[i] = swBuf[sw + i] - res[resp + i];
					}
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[exc + i] = 0;
					}
					for (i = 0; i < subframeSize; i++)
					{
						target[i] *= scale_1;
					}
					for (i = 0; i < subframeSize; i++)
					{
						innov[i] = 0;
					}
					submodes[submodeID].innovation.quant(target, interp_qlpc, bw_lpc1, bw_lpc2, lpcSize
						, subframeSize, innov, 0, syn_resp, bits, (complexity + 1) >> 1);
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[exc + i] += innov[i] * scale;
					}
					if (submodes[submodeID].double_codebook != 0)
					{
						float[] innov2 = new float[subframeSize];
						for (i = 0; i < subframeSize; i++)
						{
							innov2[i] = 0;
						}
						for (i = 0; i < subframeSize; i++)
						{
							target[i] *= 2.5f;
						}
						submodes[submodeID].innovation.quant(target, interp_qlpc, bw_lpc1, bw_lpc2, lpcSize
							, subframeSize, innov2, 0, syn_resp, bits, (complexity + 1) >> 1);
						for (i = 0; i < subframeSize; i++)
						{
							innov2[i] *= scale * (1f / 2.5f);
						}
						for (i = 0; i < subframeSize; i++)
						{
							excBuf[exc + i] += innov2[i];
						}
					}
				}
				for (i = 0; i < lpcSize; i++)
				{
					mem[i] = mem_sp[i];
				}
				org.xiph.speex.Filters.iir_mem2(excBuf, exc, interp_qlpc, high, sp, subframeSize, 
					lpcSize, mem_sp);
				org.xiph.speex.Filters.filter_mem2(high, sp, bw_lpc1, bw_lpc2, swBuf, sw, subframeSize
					, lpcSize, mem_sw, 0);
			}
			//#ifndef RELEASE
			filters.fir_mem_up(x0d, h0, y0, fullFrameSize, QMF_ORDER, g0_mem);
			filters.fir_mem_up(high, h1, y1, fullFrameSize, QMF_ORDER, g1_mem);
			for (i = 0; i < fullFrameSize; i++)
			{
				@in[i] = 2 * (y0[i] - y1[i]);
			}
			//#endif
			for (i = 0; i < lpcSize; i++)
			{
				old_lsp[i] = lsp[i];
			}
			for (i = 0; i < lpcSize; i++)
			{
				old_qlsp[i] = qlsp[i];
			}
			first = 0;
			return 1;
		}

		/// <summary>Returns the size in bits of an audio frame encoded with the current mode.
		/// 	</summary>
		/// <remarks>Returns the size in bits of an audio frame encoded with the current mode.
		/// 	</remarks>
		/// <returns>the size in bits of an audio frame encoded with the current mode.</returns>
		public virtual int getEncodedFrameSize()
		{
			int size = SB_FRAME_SIZE[submodeID];
			size += lowenc.getEncodedFrameSize();
			return size;
		}

		//---------------------------------------------------------------------------
		// Speex Control Functions
		//---------------------------------------------------------------------------
		/// <summary>Sets the Quality.</summary>
		/// <remarks>Sets the Quality.</remarks>
		/// <param name="quality"></param>
		public virtual void setQuality(int quality)
		{
			if (quality < 0)
			{
				quality = 0;
			}
			if (quality > 10)
			{
				quality = 10;
			}
			if (uwb)
			{
				lowenc.setQuality(quality);
				this.setMode(UWB_QUALITY_MAP[quality]);
			}
			else
			{
				lowenc.setMode(NB_QUALITY_MAP[quality]);
				this.setMode(WB_QUALITY_MAP[quality]);
			}
		}

		/// <summary>Sets the Varible Bit Rate Quality.</summary>
		/// <remarks>Sets the Varible Bit Rate Quality.</remarks>
		/// <param name="quality"></param>
		public virtual void setVbrQuality(float quality)
		{
			vbr_quality = quality;
			float qual = quality + 0.6f;
			if (qual > 10)
			{
				qual = 10;
			}
			lowenc.setVbrQuality(qual);
			int q = (int)System.Math.Floor(.5 + quality);
			if (q > 10)
			{
				q = 10;
			}
			setQuality(q);
		}

		/// <summary>Sets whether or not to use Variable Bit Rate encoding.</summary>
		/// <remarks>Sets whether or not to use Variable Bit Rate encoding.</remarks>
		/// <param name="vbr"></param>
		public virtual void setVbr(bool vbr)
		{
			//    super.setVbr(vbr);
			vbr_enabled = vbr ? 1 : 0;
			lowenc.setVbr(vbr);
		}

		/// <summary>Sets the Average Bit Rate.</summary>
		/// <remarks>Sets the Average Bit Rate.</remarks>
		/// <param name="abr"></param>
		public virtual void setAbr(int abr)
		{
			lowenc.setVbr(true);
			//    super.setAbr(abr);
			abr_enabled = (abr != 0) ? 1 : 0;
			vbr_enabled = 1;
			int i = 10;
			int rate;
			int target;
			float vbr_qual;
			target = abr;
			while (i >= 0)
			{
				setQuality(i);
				rate = getBitRate();
				if (rate <= target)
				{
					break;
				}
				i--;
			}
			vbr_qual = i;
			if (vbr_qual < 0)
			{
				vbr_qual = 0;
			}
			setVbrQuality(vbr_qual);
			abr_count = 0;
			abr_drift = 0;
			abr_drift2 = 0;
		}

		/// <summary>Returns the bitrate.</summary>
		/// <remarks>Returns the bitrate.</remarks>
		/// <returns>the bitrate.</returns>
		public virtual int getBitRate()
		{
			if (submodes[submodeID] != null)
			{
				return lowenc.getBitRate() + sampling_rate * submodes[submodeID].bits_per_frame /
					 frameSize;
			}
			else
			{
				return lowenc.getBitRate() + sampling_rate * (SB_SUBMODE_BITS + 1) / frameSize;
			}
		}

		/// <summary>Sets the sampling rate.</summary>
		/// <remarks>Sets the sampling rate.</remarks>
		/// <param name="rate"></param>
		public virtual void setSamplingRate(int rate)
		{
			//    super.setSamplingRate(rate);
			sampling_rate = rate;
			lowenc.setSamplingRate(rate);
		}

		/// <summary>Return LookAhead.</summary>
		/// <remarks>Return LookAhead.</remarks>
		/// <returns>LookAhead.</returns>
		public virtual int getLookAhead()
		{
			return 2 * lowenc.getLookAhead() + QMF_ORDER - 1;
		}

		//  public void resetState()
		//  {
		//  }
		//---------------------------------------------------------------------------
		//---------------------------------------------------------------------------
		/// <summary>Sets the encoding submode.</summary>
		/// <remarks>Sets the encoding submode.</remarks>
		/// <param name="mode"></param>
		public virtual void setMode(int mode)
		{
			if (mode < 0)
			{
				mode = 0;
			}
			submodeID = submodeSelect = mode;
		}

		/// <summary>Returns the encoding submode currently in use.</summary>
		/// <remarks>Returns the encoding submode currently in use.</remarks>
		/// <returns>the encoding submode currently in use.</returns>
		public virtual int getMode()
		{
			return submodeID;
		}

		/// <summary>Sets the bitrate.</summary>
		/// <remarks>Sets the bitrate.</remarks>
		/// <param name="bitrate"></param>
		public virtual void setBitRate(int bitrate)
		{
			for (int i = 10; i >= 0; i--)
			{
				setQuality(i);
				if (getBitRate() <= bitrate)
				{
					return;
				}
			}
		}

		/// <summary>Returns whether or not we are using Variable Bit Rate encoding.</summary>
		/// <remarks>Returns whether or not we are using Variable Bit Rate encoding.</remarks>
		/// <returns>whether or not we are using Variable Bit Rate encoding.</returns>
		public virtual bool getVbr()
		{
			return vbr_enabled != 0;
		}

		/// <summary>Sets whether or not to use Voice Activity Detection encoding.</summary>
		/// <remarks>Sets whether or not to use Voice Activity Detection encoding.</remarks>
		/// <param name="vad"></param>
		public virtual void setVad(bool vad)
		{
			vad_enabled = vad ? 1 : 0;
		}

		/// <summary>Returns whether or not we are using Voice Activity Detection encoding.</summary>
		/// <remarks>Returns whether or not we are using Voice Activity Detection encoding.</remarks>
		/// <returns>whether or not we are using Voice Activity Detection encoding.</returns>
		public virtual bool getVad()
		{
			return vad_enabled != 0;
		}

		/// <summary>Sets whether or not to use Discontinuous Transmission encoding.</summary>
		/// <remarks>Sets whether or not to use Discontinuous Transmission encoding.</remarks>
		/// <param name="dtx"></param>
		public virtual void setDtx(bool dtx)
		{
			dtx_enabled = dtx ? 1 : 0;
		}

		/// <summary>Returns the Average Bit Rate used (0 if ABR is not turned on).</summary>
		/// <remarks>Returns the Average Bit Rate used (0 if ABR is not turned on).</remarks>
		/// <returns>the Average Bit Rate used (0 if ABR is not turned on).</returns>
		public virtual int getAbr()
		{
			return abr_enabled;
		}

		/// <summary>Returns the Varible Bit Rate Quality.</summary>
		/// <remarks>Returns the Varible Bit Rate Quality.</remarks>
		/// <returns>the Varible Bit Rate Quality.</returns>
		public virtual float getVbrQuality()
		{
			return vbr_quality;
		}

		/// <summary>Sets the algorthmic complexity.</summary>
		/// <remarks>Sets the algorthmic complexity.</remarks>
		/// <param name="complexity"></param>
		public virtual void setComplexity(int complexity)
		{
			if (complexity < 0)
			{
				complexity = 0;
			}
			if (complexity > 10)
			{
				complexity = 10;
			}
			this.complexity = complexity;
		}

		/// <summary>Returns the algorthmic complexity.</summary>
		/// <remarks>Returns the algorthmic complexity.</remarks>
		/// <returns>the algorthmic complexity.</returns>
		public virtual int getComplexity()
		{
			return complexity;
		}

		/// <summary>Returns the sampling rate.</summary>
		/// <remarks>Returns the sampling rate.</remarks>
		/// <returns>the sampling rate.</returns>
		public virtual int getSamplingRate()
		{
			return sampling_rate;
		}

		/// <summary>Returns the relative quality.</summary>
		/// <remarks>Returns the relative quality.</remarks>
		/// <returns>the relative quality.</returns>
		public virtual float getRelativeQuality()
		{
			return relative_quality;
		}
	}
}
