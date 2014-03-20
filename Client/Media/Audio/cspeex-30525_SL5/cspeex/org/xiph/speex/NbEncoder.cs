namespace org.xiph.speex
{
	/// <summary>Narrowband Speex Encoder</summary>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class NbEncoder : org.xiph.speex.NbCodec, org.xiph.speex.Encoder
	{
		/// <summary>The Narrowband Quality map indicates which narrowband submode to use for the given narrowband quality setting
		/// 	</summary>
		public static readonly int[] NB_QUALITY_MAP = new int[] { 1, 8, 2, 3, 3, 4, 4, 5, 
			5, 6, 7 };

		private int bounded_pitch;

		/// <summary>Next frame should not rely on previous frames for pitch</summary>
		private int[] pitch;

		private float pre_mem2;

		/// <summary>1-element memory for pre-emphasis</summary>
		private float[] exc2Buf;

		/// <summary>"Pitch enhanced" excitation</summary>
		private int exc2Idx;

		/// <summary>"Pitch enhanced" excitation</summary>
		private float[] swBuf;

		/// <summary>Weighted signal buffer</summary>
		private int swIdx;

		/// <summary>Start of weighted signal frame</summary>
		private float[] window;

		/// <summary>Temporary (Hanning) window</summary>
		private float[] buf2;

		/// <summary>2nd temporary buffer</summary>
		private float[] autocorr;

		/// <summary>auto-correlation</summary>
		private float[] lagWindow;

		/// <summary>Window applied to auto-correlation</summary>
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
		private float[] rc;

		/// <summary>Reflection coefficients</summary>
		private float[] mem_sw;

		/// <summary>Filter memory for perceptually-weighted signal</summary>
		private float[] mem_sw_whole;

		/// <summary>Filter memory for perceptually-weighted signal (whole frame)</summary>
		private float[] mem_exc;

		/// <summary>Filter memory for excitation (whole frame)</summary>
		private org.xiph.speex.Vbr vbr;

		/// <summary>State of the VBR data</summary>
		private int dtx_count;

		/// <summary>Number of consecutive DTX frames</summary>
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

		//private float[]  innov2;
		/// <summary>Initialisation</summary>
		/// <param name="frameSize"></param>
		/// <param name="subframeSize"></param>
		/// <param name="lpcSize"></param>
		/// <param name="bufSize"></param>
		public override void init(int frameSize, int subframeSize, int lpcSize, int bufSize
			)
		{
			base.init(frameSize, subframeSize, lpcSize, bufSize);
			complexity = 3;
			// in C it's 2 here, but set to 3 automatically by the encoder
			vbr_enabled = 0;
			// disabled by default
			vad_enabled = 0;
			// disabled by default
			abr_enabled = 0;
			// disabled by default
			vbr_quality = 8;
			submodeSelect = 5;
			pre_mem2 = 0;
			bounded_pitch = 1;
			exc2Buf = new float[bufSize];
			exc2Idx = bufSize - windowSize;
			swBuf = new float[bufSize];
			swIdx = bufSize - windowSize;
			window = org.xiph.speex.Misc.window(windowSize, subframeSize);
			lagWindow = org.xiph.speex.Misc.lagWindow(lpcSize, lag_factor);
			autocorr = new float[lpcSize + 1];
			buf2 = new float[windowSize];
			interp_lpc = new float[lpcSize + 1];
			interp_qlpc = new float[lpcSize + 1];
			bw_lpc1 = new float[lpcSize + 1];
			bw_lpc2 = new float[lpcSize + 1];
			lsp = new float[lpcSize];
			qlsp = new float[lpcSize];
			old_lsp = new float[lpcSize];
			old_qlsp = new float[lpcSize];
			interp_lsp = new float[lpcSize];
			interp_qlsp = new float[lpcSize];
			rc = new float[lpcSize];
			mem_sp = new float[lpcSize];
			// why was there a *5 before ?!?
			mem_sw = new float[lpcSize];
			mem_sw_whole = new float[lpcSize];
			mem_exc = new float[lpcSize];
			vbr = new org.xiph.speex.Vbr();
			dtx_count = 0;
			abr_count = 0;
			sampling_rate = 8000;
			awk1 = new float[lpcSize + 1];
			awk2 = new float[lpcSize + 1];
			awk3 = new float[lpcSize + 1];
			//innov2 =  new float[40];
			filters.init();
			pitch = new int[nbSubframes];
		}

		/// <summary>Encode the given input signal.</summary>
		/// <remarks>Encode the given input signal.</remarks>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="in">- the raw mono audio frame to encode.</param>
		/// <returns>return 1 if successful.</returns>
		public virtual int encode(org.xiph.speex.Bits bits, float[] @in)
		{
			int i;
			float[] res;
			float[] target;
			float[] mem;
			float[] syn_resp;
			float[] orig;
            float ener; //TODO: Check
			System.Array.Copy(frmBuf, frameSize, frmBuf, 0, bufSize - frameSize);
			frmBuf[bufSize - frameSize] = @in[0] - preemph * pre_mem;
			for (i = 1; i < frameSize; i++)
			{
				frmBuf[bufSize - frameSize + i] = @in[i] - preemph * @in[i - 1];
			}
			pre_mem = @in[frameSize - 1];
			System.Array.Copy(exc2Buf, frameSize, exc2Buf, 0, bufSize - frameSize);
			System.Array.Copy(excBuf, frameSize, excBuf, 0, bufSize - frameSize);
			System.Array.Copy(swBuf, frameSize, swBuf, 0, bufSize - frameSize);
			for (i = 0; i < windowSize; i++)
			{
				buf2[i] = frmBuf[i + frmIdx] * window[i];
			}
			org.xiph.speex.Lpc.autocorr(buf2, autocorr, lpcSize + 1, windowSize);
			autocorr[0] += 10;
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
			if (roots == lpcSize)
			{
				for (i = 0; i < lpcSize; i++)
				{
					lsp[i] = (float)System.Math.Acos(lsp[i]);
				}
			}
			else
			{
				if (complexity > 1)
				{
					roots = org.xiph.speex.Lsp.lpc2lsp(lpc, lpcSize, lsp, 11, 0.05f);
				}
				if (roots == lpcSize)
				{
					for (i = 0; i < lpcSize; i++)
					{
						lsp[i] = (float)System.Math.Acos(lsp[i]);
					}
				}
				else
				{
					for (i = 0; i < lpcSize; i++)
					{
						lsp[i] = old_lsp[i];
					}
				}
			}
			float lsp_dist = 0;
			for (i = 0; i < lpcSize; i++)
			{
				lsp_dist += (old_lsp[i] - lsp[i]) * (old_lsp[i] - lsp[i]);
			}
			float ol_gain;
			int ol_pitch;
			float ol_pitch_coef;
			if (first != 0)
			{
				for (i = 0; i < lpcSize; i++)
				{
					interp_lsp[i] = lsp[i];
				}
			}
			else
			{
				for (i = 0; i < lpcSize; i++)
				{
					interp_lsp[i] = .375f * old_lsp[i] + .625f * lsp[i];
				}
			}
			org.xiph.speex.Lsp.enforce_margin(interp_lsp, lpcSize, .002f);
			for (i = 0; i < lpcSize; i++)
			{
				interp_lsp[i] = (float)System.Math.Cos(interp_lsp[i]);
			}
			m_lsp.lsp2lpc(interp_lsp, interp_lpc, lpcSize);
			if (submodes[submodeID] == null || vbr_enabled != 0 || vad_enabled != 0 || submodes
				[submodeID].forced_pitch_gain != 0 || submodes[submodeID].lbr_pitch != -1)
			{
				int[] nol_pitch = new int[6];
				float[] nol_pitch_coef = new float[6];
				org.xiph.speex.Filters.bw_lpc(gamma1, interp_lpc, bw_lpc1, lpcSize);
				org.xiph.speex.Filters.bw_lpc(gamma2, interp_lpc, bw_lpc2, lpcSize);
				org.xiph.speex.Filters.filter_mem2(frmBuf, frmIdx, bw_lpc1, bw_lpc2, swBuf, swIdx
					, frameSize, lpcSize, mem_sw_whole, 0);
				org.xiph.speex.Ltp.open_loop_nbest_pitch(swBuf, swIdx, min_pitch, max_pitch, frameSize
					, nol_pitch, nol_pitch_coef, 6);
				ol_pitch = nol_pitch[0];
				ol_pitch_coef = nol_pitch_coef[0];
				for (i = 1; i < 6; i++)
				{
					if ((nol_pitch_coef[i] > .85f * ol_pitch_coef) && (System.Math.Abs(nol_pitch[i] -
						 ol_pitch / 2.0f) <= 1 || System.Math.Abs(nol_pitch[i] - ol_pitch / 3.0f) <= 1 ||
						 System.Math.Abs(nol_pitch[i] - ol_pitch / 4.0f) <= 1 || System.Math.Abs(nol_pitch
						[i] - ol_pitch / 5.0f) <= 1))
					{
						ol_pitch = nol_pitch[i];
					}
				}
			}
			else
			{
				ol_pitch = 0;
				ol_pitch_coef = 0;
			}
			org.xiph.speex.Filters.fir_mem2(frmBuf, frmIdx, interp_lpc, excBuf, excIdx, frameSize
				, lpcSize, mem_exc);
			ol_gain = 0;
			for (i = 0; i < frameSize; i++)
			{
				ol_gain += excBuf[excIdx + i] * excBuf[excIdx + i];
			}
			ol_gain = (float)System.Math.Sqrt(1 + ol_gain / frameSize);
			if (vbr != null && (vbr_enabled != 0 || vad_enabled != 0))
			{
				if (abr_enabled != 0)
				{
					float qual_change = 0;
					if (abr_drift2 * abr_drift > 0)
					{
						qual_change = -.00001f * abr_drift / (1 + abr_count);
						if (qual_change > .05f)
						{
							qual_change = .05f;
						}
						if (qual_change < -.05f)
						{
							qual_change = -.05f;
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
				relative_quality = vbr.analysis(@in, frameSize, ol_pitch, ol_pitch_coef);
				if (vbr_enabled != 0)
				{
					int mode;
					int choice = 0;
					float min_diff = 100;
					mode = 8;
					while (mode > 0)
					{
						int v1;
						float thresh;
						v1 = (int)System.Math.Floor(vbr_quality);
						if (v1 == 10)
						{
							thresh = org.xiph.speex.Vbr.nb_thresh[mode][v1];
						}
						else
						{
							thresh = (vbr_quality - v1) * org.xiph.speex.Vbr.nb_thresh[mode][v1 + 1] + (1 + v1
								 - vbr_quality) * org.xiph.speex.Vbr.nb_thresh[mode][v1];
						}
						if (relative_quality > thresh && relative_quality - thresh < min_diff)
						{
							choice = mode;
							min_diff = relative_quality - thresh;
						}
						mode--;
					}
					mode = choice;
					if (mode == 0)
					{
						if (dtx_count == 0 || lsp_dist > .05 || dtx_enabled == 0 || dtx_count > 20)
						{
							mode = 1;
							dtx_count = 1;
						}
						else
						{
							mode = 0;
							dtx_count++;
						}
					}
					else
					{
						dtx_count = 0;
					}
					setMode(mode);
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
					int mode;
					if (relative_quality < 2)
					{
						if (dtx_count == 0 || lsp_dist > .05 || dtx_enabled == 0 || dtx_count > 20)
						{
							dtx_count = 1;
							mode = 1;
						}
						else
						{
							mode = 0;
							dtx_count++;
						}
					}
					else
					{
						dtx_count = 0;
						mode = submodeSelect;
					}
					submodeID = mode;
				}
			}
			else
			{
				relative_quality = -1;
			}
			bits.pack(0, 1);
			bits.pack(submodeID, NB_SUBMODE_BITS);
			if (submodes[submodeID] == null)
			{
				for (i = 0; i < frameSize; i++)
				{
					excBuf[excIdx + i] = exc2Buf[exc2Idx + i] = swBuf[swIdx + i] = VERY_SMALL;
				}
				for (i = 0; i < lpcSize; i++)
				{
					mem_sw[i] = 0;
				}
				first = 1;
				bounded_pitch = 1;
				org.xiph.speex.Filters.iir_mem2(excBuf, excIdx, interp_qlpc, frmBuf, frmIdx, frameSize
					, lpcSize, mem_sp);
				@in[0] = frmBuf[frmIdx] + preemph * pre_mem2;
				for (i = 1; i < frameSize; i++)
				{
					@in[i] = frmBuf[frmIdx + i] + preemph * @in[i - 1];
				}
				pre_mem2 = @in[frameSize - 1];
				return 0;
			}
			if (first != 0)
			{
				for (i = 0; i < lpcSize; i++)
				{
					old_lsp[i] = lsp[i];
				}
			}
			//#if 1 /*0 for unquantized*/
			submodes[submodeID].lsqQuant.quant(lsp, qlsp, lpcSize, bits);
			//#else
			//     for (i=0;i<lpcSize;i++)
			//       qlsp[i]=lsp[i];
			//#endif
			if (submodes[submodeID].lbr_pitch != -1)
			{
				bits.pack(ol_pitch - min_pitch, 7);
			}
			if (submodes[submodeID].forced_pitch_gain != 0)
			{
				int quant;
				quant = (int)System.Math.Floor(.5f + 15 * ol_pitch_coef);
				if (quant > 15)
				{
					quant = 15;
				}
				if (quant < 0)
				{
					quant = 0;
				}
				bits.pack(quant, 4);
				ol_pitch_coef = (float)0.066667f * quant;
			}
			int qe = (int)(System.Math.Floor(0.5 + 3.5 * System.Math.Log(ol_gain)));
			if (qe < 0)
			{
				qe = 0;
			}
			if (qe > 31)
			{
				qe = 31;
			}
			ol_gain = (float)System.Math.Exp(qe / 3.5);
			bits.pack(qe, 5);
			if (first != 0)
			{
				for (i = 0; i < lpcSize; i++)
				{
					old_qlsp[i] = qlsp[i];
				}
			}
			res = new float[subframeSize];
			target = new float[subframeSize];
			syn_resp = new float[subframeSize];
			mem = new float[lpcSize];
			orig = new float[frameSize];
			for (i = 0; i < frameSize; i++)
			{
				orig[i] = frmBuf[frmIdx + i];
			}
			for (int sub = 0; sub < nbSubframes; sub++)
			{
				float tmp;
				int offset;
				int sp;
				int sw;
				int exc;
				int exc2;
				int pitchval;
				offset = subframeSize * sub;
				sp = frmIdx + offset;
				exc = excIdx + offset;
				sw = swIdx + offset;
				exc2 = exc2Idx + offset;
				tmp = (float)(1.0 + sub) / nbSubframes;
				for (i = 0; i < lpcSize; i++)
				{
					interp_lsp[i] = (1 - tmp) * old_lsp[i] + tmp * lsp[i];
				}
				for (i = 0; i < lpcSize; i++)
				{
					interp_qlsp[i] = (1 - tmp) * old_qlsp[i] + tmp * qlsp[i];
				}
				org.xiph.speex.Lsp.enforce_margin(interp_lsp, lpcSize, .002f);
				org.xiph.speex.Lsp.enforce_margin(interp_qlsp, lpcSize, .002f);
				for (i = 0; i < lpcSize; i++)
				{
					interp_lsp[i] = (float)System.Math.Cos(interp_lsp[i]);
				}
				m_lsp.lsp2lpc(interp_lsp, interp_lpc, lpcSize);
				for (i = 0; i < lpcSize; i++)
				{
					interp_qlsp[i] = (float)System.Math.Cos(interp_qlsp[i]);
				}
				m_lsp.lsp2lpc(interp_qlsp, interp_qlpc, lpcSize);
				tmp = 1;
				pi_gain[sub] = 0;
				for (i = 0; i <= lpcSize; i++)
				{
					pi_gain[sub] += tmp * interp_qlpc[i];
					tmp = -tmp;
				}
				org.xiph.speex.Filters.bw_lpc(gamma1, interp_lpc, bw_lpc1, lpcSize);
				if (gamma2 >= 0)
				{
					org.xiph.speex.Filters.bw_lpc(gamma2, interp_lpc, bw_lpc2, lpcSize);
				}
				else
				{
					bw_lpc2[0] = 1;
					bw_lpc2[1] = -preemph;
					for (i = 2; i <= lpcSize; i++)
					{
						bw_lpc2[i] = 0;
					}
				}
				for (i = 0; i < subframeSize; i++)
				{
					excBuf[exc + i] = VERY_SMALL;
				}
				excBuf[exc] = 1;
				org.xiph.speex.Filters.syn_percep_zero(excBuf, exc, interp_qlpc, bw_lpc1, bw_lpc2
					, syn_resp, subframeSize, lpcSize);
				for (i = 0; i < subframeSize; i++)
				{
					excBuf[exc + i] = VERY_SMALL;
				}
				for (i = 0; i < subframeSize; i++)
				{
					exc2Buf[exc2 + i] = VERY_SMALL;
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
				org.xiph.speex.Filters.filter_mem2(excBuf, exc, bw_lpc1, bw_lpc2, res, 0, subframeSize
					, lpcSize, mem, 0);
				for (i = 0; i < lpcSize; i++)
				{
					mem[i] = mem_sw[i];
				}
				org.xiph.speex.Filters.filter_mem2(frmBuf, sp, bw_lpc1, bw_lpc2, swBuf, sw, subframeSize
					, lpcSize, mem, 0);
				for (i = 0; i < subframeSize; i++)
				{
					target[i] = swBuf[sw + i] - res[i];
				}
				for (i = 0; i < subframeSize; i++)
				{
					excBuf[exc + i] = exc2Buf[exc2 + i] = 0;
				}
				//    if (submodes[submodeID].ltp.quant)
				//    {
				int pit_min;
				int pit_max;
				if (submodes[submodeID].lbr_pitch != -1)
				{
					int margin;
					margin = submodes[submodeID].lbr_pitch;
					if (margin != 0)
					{
						if (ol_pitch < min_pitch + margin - 1)
						{
							ol_pitch = min_pitch + margin - 1;
						}
						if (ol_pitch > max_pitch - margin)
						{
							ol_pitch = max_pitch - margin;
						}
						pit_min = ol_pitch - margin + 1;
						pit_max = ol_pitch + margin;
					}
					else
					{
						pit_min = pit_max = ol_pitch;
					}
				}
				else
				{
					pit_min = min_pitch;
					pit_max = max_pitch;
				}
				if (bounded_pitch != 0 && pit_max > offset)
				{
					pit_max = offset;
				}
				pitchval = submodes[submodeID].ltp.quant(target, swBuf, sw, interp_qlpc, bw_lpc1, 
					bw_lpc2, excBuf, exc, pit_min, pit_max, ol_pitch_coef, lpcSize, subframeSize, bits
					, exc2Buf, exc2, syn_resp, complexity);
				pitch[sub] = pitchval;
				//    } else {
				//      speex_error ("No pitch prediction, what's wrong");
				//    }
				org.xiph.speex.Filters.syn_percep_zero(excBuf, exc, interp_qlpc, bw_lpc1, bw_lpc2
					, res, subframeSize, lpcSize);
				for (i = 0; i < subframeSize; i++)
				{
					target[i] -= res[i];
				}
				int innovptr;
				//float ener = 0;
                ener = 0;
				float ener_1;
				innovptr = sub * subframeSize;
				for (i = 0; i < subframeSize; i++)
				{
					innov[innovptr + i] = 0;
				}
				org.xiph.speex.Filters.residue_percep_zero(target, 0, interp_qlpc, bw_lpc1, bw_lpc2
					, buf2, subframeSize, lpcSize);
				for (i = 0; i < subframeSize; i++)
				{
					ener += buf2[i] * buf2[i];
				}
				ener = (float)System.Math.Sqrt(.1f + ener / subframeSize);
				ener /= ol_gain;
				if (submodes[submodeID].have_subframe_gain != 0)
				{
					//int qe;
					ener = (float)System.Math.Log(ener);
					if (submodes[submodeID].have_subframe_gain == 3)
					{
						qe = org.xiph.speex.VQ.index(ener, exc_gain_quant_scal3, 8);
						bits.pack(qe, 3);
						ener = exc_gain_quant_scal3[qe];
					}
					else
					{
						qe = org.xiph.speex.VQ.index(ener, exc_gain_quant_scal1, 2);
						bits.pack(qe, 1);
						ener = exc_gain_quant_scal1[qe];
					}
					ener = (float)System.Math.Exp(ener);
				}
				else
				{
					ener = 1f;
				}
				ener *= ol_gain;
				ener_1 = 1 / ener;
				for (i = 0; i < subframeSize; i++)
				{
					target[i] *= ener_1;
				}
				//      if (submodes[submodeID].innovation != null)
				//      {
				submodes[submodeID].innovation.quant(target, interp_qlpc, bw_lpc1, bw_lpc2, lpcSize
					, subframeSize, innov, innovptr, syn_resp, bits, complexity);
				for (i = 0; i < subframeSize; i++)
				{
					innov[innovptr + i] *= ener;
				}
				for (i = 0; i < subframeSize; i++)
				{
					excBuf[exc + i] += innov[innovptr + i];
				}
				//      } else {
				//        speex_error("No fixed codebook");
				//      }
				if (submodes[submodeID].double_codebook != 0)
				{
					float[] innov2 = new float[subframeSize];
					//          for (i = 0; i < subframeSize; i++)
					//            innov2[i] = 0;
					for (i = 0; i < subframeSize; i++)
					{
						target[i] *= 2.2f;
					}
					submodes[submodeID].innovation.quant(target, interp_qlpc, bw_lpc1, bw_lpc2, lpcSize
						, subframeSize, innov2, 0, syn_resp, bits, complexity);
					for (i = 0; i < subframeSize; i++)
					{
						innov2[i] *= ener * (1f / 2.2f);
					}
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[exc + i] += innov2[i];
					}
				}
				for (i = 0; i < subframeSize; i++)
				{
					target[i] *= ener;
				}
				for (i = 0; i < lpcSize; i++)
				{
					mem[i] = mem_sp[i];
				}
				org.xiph.speex.Filters.iir_mem2(excBuf, exc, interp_qlpc, frmBuf, sp, subframeSize
					, lpcSize, mem_sp);
				org.xiph.speex.Filters.filter_mem2(frmBuf, sp, bw_lpc1, bw_lpc2, swBuf, sw, subframeSize
					, lpcSize, mem_sw, 0);
				for (i = 0; i < subframeSize; i++)
				{
					exc2Buf[exc2 + i] = excBuf[exc + i];
				}
			}
			if (submodeID >= 1)
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
			if (submodeID == 1)
			{
				if (dtx_count != 0)
				{
					bits.pack(15, 4);
				}
				else
				{
					bits.pack(0, 4);
				}
			}
			first = 0;
			//float ener = 0;
            ener = 0;
            float err = 0;
			//float snr;
			for (i = 0; i < frameSize; i++)
			{
				ener += frmBuf[frmIdx + i] * frmBuf[frmIdx + i];
				err += (frmBuf[frmIdx + i] - orig[i]) * (frmBuf[frmIdx + i] - orig[i]);
			}
			//snr = (float) (10*Math.log((ener+1)/(err+1)));
			//System.out.println("Frame result: SNR="+snr+" E="+ener+" Err="+err+"\r\n");
			@in[0] = frmBuf[frmIdx] + preemph * pre_mem2;
			for (i = 1; i < frameSize; i++)
			{
				@in[i] = frmBuf[frmIdx + i] + preemph * @in[i - 1];
			}
			pre_mem2 = @in[frameSize - 1];
			if (submodes[submodeID].innovation is org.xiph.speex.NoiseSearch || submodeID == 
				0)
			{
				bounded_pitch = 1;
			}
			else
			{
				bounded_pitch = 0;
			}
			return 1;
		}

		/// <summary>Returns the size in bits of an audio frame encoded with the current mode.
		/// 	</summary>
		/// <remarks>Returns the size in bits of an audio frame encoded with the current mode.
		/// 	</remarks>
		/// <returns>the size in bits of an audio frame encoded with the current mode.</returns>
		public virtual int getEncodedFrameSize()
		{
			return NB_FRAME_SIZE[submodeID];
		}

		//---------------------------------------------------------------------------
		// Speex Control Functions
		//---------------------------------------------------------------------------
		/// <summary>Sets the Quality</summary>
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
			submodeID = submodeSelect = NB_QUALITY_MAP[quality];
		}

		/// <summary>Gets the bitrate.</summary>
		/// <remarks>Gets the bitrate.</remarks>
		/// <returns>the bitrate.</returns>
		public virtual int getBitRate()
		{
			if (submodes[submodeID] != null)
			{
				return sampling_rate * submodes[submodeID].bits_per_frame / frameSize;
			}
			else
			{
				return sampling_rate * (NB_SUBMODE_BITS + 1) / frameSize;
			}
		}

		//  public void    resetState()
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

		/// <summary>Sets whether or not to use Variable Bit Rate encoding.</summary>
		/// <remarks>Sets whether or not to use Variable Bit Rate encoding.</remarks>
		/// <param name="vbr"></param>
		public virtual void setVbr(bool vbr)
		{
			vbr_enabled = vbr ? 1 : 0;
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

		/// <summary>Sets the Average Bit Rate.</summary>
		/// <remarks>Sets the Average Bit Rate.</remarks>
		/// <param name="abr"></param>
		public virtual void setAbr(int abr)
		{
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

		/// <summary>Sets the Varible Bit Rate Quality.</summary>
		/// <remarks>Sets the Varible Bit Rate Quality.</remarks>
		/// <param name="quality"></param>
		public virtual void setVbrQuality(float quality)
		{
			if (quality < 0f)
			{
				quality = 0f;
			}
			if (quality > 10f)
			{
				quality = 10f;
			}
			vbr_quality = quality;
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

		/// <summary>Sets the sampling rate.</summary>
		/// <remarks>Sets the sampling rate.</remarks>
		/// <param name="rate"></param>
		public virtual void setSamplingRate(int rate)
		{
			sampling_rate = rate;
		}

		/// <summary>Returns the sampling rate.</summary>
		/// <remarks>Returns the sampling rate.</remarks>
		/// <returns>the sampling rate.</returns>
		public virtual int getSamplingRate()
		{
			return sampling_rate;
		}

		/// <summary>Return LookAhead.</summary>
		/// <remarks>Return LookAhead.</remarks>
		/// <returns>LookAhead.</returns>
		public virtual int getLookAhead()
		{
			return windowSize - frameSize;
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
