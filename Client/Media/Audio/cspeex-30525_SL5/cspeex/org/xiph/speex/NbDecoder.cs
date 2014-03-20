namespace org.xiph.speex
{
	/// <summary>Narrowband Speex Decoder</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class NbDecoder : org.xiph.speex.NbCodec, org.xiph.speex.Decoder
	{
		private float[] innov2;

		private int count_lost;

		private int last_pitch;

		/// <summary>Pitch of last correctly decoded frame</summary>
		private float last_pitch_gain;

		/// <summary>Pitch gain of last correctly decoded frame</summary>
		private float[] pitch_gain_buf;

		/// <summary>Pitch gain of last decoded frames</summary>
		private int pitch_gain_buf_idx;

		/// <summary>Tail of the buffer</summary>
		private float last_ol_gain;

		protected java.util.Random random = new java.util.Random();

		protected org.xiph.speex.Stereo stereo;

		protected org.xiph.speex.Inband inband;

		protected bool enhanced;

		/// <summary>Constructor</summary>
		public NbDecoder()
		{
			stereo = new org.xiph.speex.Stereo();
			inband = new org.xiph.speex.Inband(stereo);
			enhanced = true;
		}

		/// <summary>Initialise</summary>
		/// <param name="frameSize"></param>
		/// <param name="subframeSize"></param>
		/// <param name="lpcSize"></param>
		/// <param name="bufSize"></param>
		public override void init(int frameSize, int subframeSize, int lpcSize, int bufSize
			)
		{
			base.init(frameSize, subframeSize, lpcSize, bufSize);
			filters.init();
			innov2 = new float[40];
			count_lost = 0;
			last_pitch = 40;
			last_pitch_gain = 0;
			pitch_gain_buf = new float[3];
			pitch_gain_buf_idx = 0;
			last_ol_gain = 0;
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
			int pitch;
			int ol_pitch = 0;
			int m;
			float[] pitch_gain = new float[3];
			float ol_gain = 0.0f;
			float ol_pitch_coef = 0.0f;
			int best_pitch = 40;
			float best_pitch_gain = 0;
			float pitch_average = 0;
			if (bits == null && dtx_enabled != 0)
			{
				submodeID = 0;
			}
			else
			{
				if (bits == null)
				{
					decodeLost(@out);
					return 0;
				}
				do
				{
					if (bits.unpack(1) != 0)
					{
						//Wideband
						m = bits.unpack(org.xiph.speex.SbCodec.SB_SUBMODE_BITS);
						int advance = org.xiph.speex.SbCodec.SB_FRAME_SIZE[m];
						if (advance < 0)
						{
							throw new java.io.StreamCorruptedException("Invalid sideband mode encountered (1st sideband): "
								 + m);
						}
						//return -2;
						advance -= (org.xiph.speex.SbCodec.SB_SUBMODE_BITS + 1);
						bits.advance(advance);
						if (bits.unpack(1) != 0)
						{
							m = bits.unpack(org.xiph.speex.SbCodec.SB_SUBMODE_BITS);
							advance = org.xiph.speex.SbCodec.SB_FRAME_SIZE[m];
							if (advance < 0)
							{
								throw new java.io.StreamCorruptedException("Invalid sideband mode encountered. (2nd sideband): "
									 + m);
							}
							//return -2;
							advance -= (org.xiph.speex.SbCodec.SB_SUBMODE_BITS + 1);
							bits.advance(advance);
							if (bits.unpack(1) != 0)
							{
								throw new java.io.StreamCorruptedException("More than two sideband layers found");
							}
						}
					}
					//return -2;
					//*/
					m = bits.unpack(NB_SUBMODE_BITS);
					if (m == 15)
					{
						return 1;
					}
					else
					{
						if (m == 14)
						{
							inband.speexInbandRequest(bits);
						}
						else
						{
							if (m == 13)
							{
								inband.userInbandRequest(bits);
							}
							else
							{
								if (m > 8)
								{
									throw new java.io.StreamCorruptedException("Invalid mode encountered: " + m);
								}
							}
						}
					}
				}
				while (m > 8);
				//return -2;
				submodeID = m;
			}
			System.Array.Copy(frmBuf, frameSize, frmBuf, 0, bufSize - frameSize);
			System.Array.Copy(excBuf, frameSize, excBuf, 0, bufSize - frameSize);
			if (submodes[submodeID] == null)
			{
				org.xiph.speex.Filters.bw_lpc(.93f, interp_qlpc, lpc, 10);
				float innov_gain = 0;
				for (i = 0; i < frameSize; i++)
				{
					innov_gain += innov[i] * innov[i];
				}
				innov_gain = (float)System.Math.Sqrt(innov_gain / frameSize);
				for (i = excIdx; i < excIdx + frameSize; i++)
				{
					excBuf[i] = 3 * innov_gain * (random.nextFloat() - .5f);
				}
				first = 1;
				org.xiph.speex.Filters.iir_mem2(excBuf, excIdx, lpc, frmBuf, frmIdx, frameSize, lpcSize
					, mem_sp);
				@out[0] = frmBuf[frmIdx] + preemph * pre_mem;
				for (i = 1; i < frameSize; i++)
				{
					@out[i] = frmBuf[frmIdx + i] + preemph * @out[i - 1];
				}
				pre_mem = @out[frameSize - 1];
				count_lost = 0;
				return 0;
			}
			submodes[submodeID].lsqQuant.unquant(qlsp, lpcSize, bits);
			if (count_lost != 0)
			{
				float lsp_dist = 0;
				float fact;
				for (i = 0; i < lpcSize; i++)
				{
					lsp_dist += System.Math.Abs(old_qlsp[i] - qlsp[i]);
				}
				fact = (float)(.6 * System.Math.Exp(-.2 * lsp_dist));
				for (i = 0; i < 2 * lpcSize; i++)
				{
					mem_sp[i] *= fact;
				}
			}
			if (first != 0 || count_lost != 0)
			{
				for (i = 0; i < lpcSize; i++)
				{
					old_qlsp[i] = qlsp[i];
				}
			}
			if (submodes[submodeID].lbr_pitch != -1)
			{
				ol_pitch = min_pitch + bits.unpack(7);
			}
			if (submodes[submodeID].forced_pitch_gain != 0)
			{
				int quant = bits.unpack(4);
				ol_pitch_coef = 0.066667f * quant;
			}
			int qe = bits.unpack(5);
			ol_gain = (float)System.Math.Exp(qe / 3.5);
			if (submodeID == 1)
			{
				int extra = bits.unpack(4);
				if (extra == 15)
				{
					dtx_enabled = 1;
				}
				else
				{
					dtx_enabled = 0;
				}
			}
			if (submodeID > 1)
			{
				dtx_enabled = 0;
			}
			for (sub = 0; sub < nbSubframes; sub++)
			{
				int offset;
				int spIdx;
				int extIdx;
				float tmp;
				offset = subframeSize * sub;
				spIdx = frmIdx + offset;
				extIdx = excIdx + offset;
				tmp = (1.0f + sub) / nbSubframes;
				for (i = 0; i < lpcSize; i++)
				{
					interp_qlsp[i] = (1 - tmp) * old_qlsp[i] + tmp * qlsp[i];
				}
				org.xiph.speex.Lsp.enforce_margin(interp_qlsp, lpcSize, .002f);
				for (i = 0; i < lpcSize; i++)
				{
					interp_qlsp[i] = (float)System.Math.Cos(interp_qlsp[i]);
				}
				m_lsp.lsp2lpc(interp_qlsp, interp_qlpc, lpcSize);
				if (enhanced)
				{
					float r = .9f;
					float k1;
					float k2;
					float k3;
					k1 = submodes[submodeID].lpc_enh_k1;
					k2 = submodes[submodeID].lpc_enh_k2;
					k3 = (1 - (1 - r * k1) / (1 - r * k2)) / r;
					org.xiph.speex.Filters.bw_lpc(k1, interp_qlpc, awk1, lpcSize);
					org.xiph.speex.Filters.bw_lpc(k2, interp_qlpc, awk2, lpcSize);
					org.xiph.speex.Filters.bw_lpc(k3, interp_qlpc, awk3, lpcSize);
				}
				tmp = 1;
				pi_gain[sub] = 0;
				for (i = 0; i <= lpcSize; i++)
				{
					pi_gain[sub] += tmp * interp_qlpc[i];
					tmp = -tmp;
				}
				for (i = 0; i < subframeSize; i++)
				{
					excBuf[extIdx + i] = 0;
				}
				int pit_min;
				int pit_max;
				if (submodes[submodeID].lbr_pitch != -1)
				{
					int margin = submodes[submodeID].lbr_pitch;
					if (margin != 0)
					{
						pit_min = ol_pitch - margin + 1;
						if (pit_min < min_pitch)
						{
							pit_min = min_pitch;
						}
						pit_max = ol_pitch + margin;
						if (pit_max > max_pitch)
						{
							pit_max = max_pitch;
						}
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
				pitch = submodes[submodeID].ltp.unquant(excBuf, extIdx, pit_min, ol_pitch_coef, subframeSize
					, pitch_gain, bits, count_lost, offset, last_pitch_gain);
				if (count_lost != 0 && ol_gain < last_ol_gain)
				{
					float fact = ol_gain / (last_ol_gain + 1);
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[excIdx + i] *= fact;
					}
				}
				tmp = System.Math.Abs(pitch_gain[0] + pitch_gain[1] + pitch_gain[2]);
				tmp = System.Math.Abs(pitch_gain[1]);
				if (pitch_gain[0] > 0)
				{
					tmp += pitch_gain[0];
				}
				else
				{
					tmp -= (float)(.5 * pitch_gain[0]);
				}
				if (pitch_gain[2] > 0)
				{
					tmp += pitch_gain[2];
				}
				else
				{
					tmp -= (float)(.5 * pitch_gain[0]);
				}
				pitch_average += tmp;
				if (tmp > best_pitch_gain)
				{
					best_pitch = pitch;
					best_pitch_gain = tmp;
				}
				int q_energy;
				int ivi = sub * subframeSize;
				float ener;
				for (i = ivi; i < ivi + subframeSize; i++)
				{
					innov[i] = 0.0f;
				}
				if (submodes[submodeID].have_subframe_gain == 3)
				{
					q_energy = bits.unpack(3);
					ener = (float)(ol_gain * System.Math.Exp(exc_gain_quant_scal3[q_energy]));
				}
				else
				{
					if (submodes[submodeID].have_subframe_gain == 1)
					{
						q_energy = bits.unpack(1);
						ener = (float)(ol_gain * System.Math.Exp(exc_gain_quant_scal1[q_energy]));
					}
					else
					{
						ener = ol_gain;
					}
				}
				if (submodes[submodeID].innovation != null)
				{
					submodes[submodeID].innovation.unquant(innov, ivi, subframeSize, bits);
				}
				for (i = ivi; i < ivi + subframeSize; i++)
				{
					innov[i] *= ener;
				}
				if (submodeID == 1)
				{
					float g = ol_pitch_coef;
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[extIdx + i] = 0;
					}
					while (voc_offset < subframeSize)
					{
						if (voc_offset >= 0)
						{
							excBuf[extIdx + voc_offset] = (float)System.Math.Sqrt(1.0f * ol_pitch);
						}
						voc_offset += ol_pitch;
					}
					voc_offset -= subframeSize;
					g = .5f + 2 * (g - .6f);
					if (g < 0)
					{
						g = 0;
					}
					if (g > 1)
					{
						g = 1;
					}
					for (i = 0; i < subframeSize; i++)
					{
						float itmp = excBuf[extIdx + i];
						excBuf[extIdx + i] = .8f * g * excBuf[extIdx + i] * ol_gain + .6f * g * voc_m1 * 
							ol_gain + .5f * g * innov[ivi + i] - .5f * g * voc_m2 + (1 - g) * innov[ivi + i];
						voc_m1 = itmp;
						voc_m2 = innov[ivi + i];
						voc_mean = .95f * voc_mean + .05f * excBuf[extIdx + i];
						excBuf[extIdx + i] -= voc_mean;
					}
				}
				else
				{
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[extIdx + i] += innov[ivi + i];
					}
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
						innov2[i] *= (float)(ener * (1 / 2.2));
					}
					for (i = 0; i < subframeSize; i++)
					{
						excBuf[extIdx + i] += innov2[i];
					}
				}
				for (i = 0; i < subframeSize; i++)
				{
					frmBuf[spIdx + i] = excBuf[extIdx + i];
				}
				if (enhanced && submodes[submodeID].comb_gain > 0)
				{
					filters.comb_filter(excBuf, extIdx, frmBuf, spIdx, subframeSize, pitch, pitch_gain
						, submodes[submodeID].comb_gain);
				}
				if (enhanced)
				{
					org.xiph.speex.Filters.filter_mem2(frmBuf, spIdx, awk2, awk1, subframeSize, lpcSize
						, mem_sp, lpcSize);
					org.xiph.speex.Filters.filter_mem2(frmBuf, spIdx, awk3, interp_qlpc, subframeSize
						, lpcSize, mem_sp, 0);
				}
				else
				{
					for (i = 0; i < lpcSize; i++)
					{
						mem_sp[lpcSize + i] = 0;
					}
					org.xiph.speex.Filters.iir_mem2(frmBuf, spIdx, interp_qlpc, frmBuf, spIdx, subframeSize
						, lpcSize, mem_sp);
				}
			}
			@out[0] = frmBuf[frmIdx] + preemph * pre_mem;
			for (i = 1; i < frameSize; i++)
			{
				@out[i] = frmBuf[frmIdx + i] + preemph * @out[i - 1];
			}
			pre_mem = @out[frameSize - 1];
			for (i = 0; i < lpcSize; i++)
			{
				old_qlsp[i] = qlsp[i];
			}
			first = 0;
			count_lost = 0;
			last_pitch = best_pitch;
			last_pitch_gain = .25f * pitch_average;
			pitch_gain_buf[pitch_gain_buf_idx++] = last_pitch_gain;
			if (pitch_gain_buf_idx > 2)
			{
				pitch_gain_buf_idx = 0;
			}
			last_ol_gain = ol_gain;
			return 0;
		}

		/// <summary>Decode when packets are lost.</summary>
		/// <remarks>Decode when packets are lost.</remarks>
		/// <param name="out">- the generated mono audio frame.</param>
		/// <returns>0 if successful.</returns>
		public virtual int decodeLost(float[] @out)
		{
			int i;
			float pitch_gain;
			float fact;
			float gain_med;
			fact = (float)System.Math.Exp(-.04 * count_lost * count_lost);
			// median3(a, b, c) = (a<b ? (b<c ? b : (a<c ? c : a))
			//                         : (c<b ? b : (c<a ? c : a)))
			gain_med = (pitch_gain_buf[0] < pitch_gain_buf[1] ? (pitch_gain_buf[1] < pitch_gain_buf
				[2] ? pitch_gain_buf[1] : (pitch_gain_buf[0] < pitch_gain_buf[2] ? pitch_gain_buf
				[2] : pitch_gain_buf[0])) : (pitch_gain_buf[2] < pitch_gain_buf[1] ? pitch_gain_buf
				[1] : (pitch_gain_buf[2] < pitch_gain_buf[0] ? pitch_gain_buf[2] : pitch_gain_buf
				[0])));
			if (gain_med < last_pitch_gain)
			{
				last_pitch_gain = gain_med;
			}
			pitch_gain = last_pitch_gain;
			if (pitch_gain > .95f)
			{
				pitch_gain = .95f;
			}
			pitch_gain = pitch_gain * fact + VERY_SMALL;
			System.Array.Copy(frmBuf, frameSize, frmBuf, 0, bufSize - frameSize);
			System.Array.Copy(excBuf, frameSize, excBuf, 0, bufSize - frameSize);
			for (int sub = 0; sub < nbSubframes; sub++)
			{
				int offset;
				int spIdx;
				int extIdx;
				offset = subframeSize * sub;
				spIdx = frmIdx + offset;
				extIdx = excIdx + offset;
				if (enhanced)
				{
					float r = .9f;
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
					k3 = (1 - (1 - r * k1) / (1 - r * k2)) / r;
					org.xiph.speex.Filters.bw_lpc(k1, interp_qlpc, awk1, lpcSize);
					org.xiph.speex.Filters.bw_lpc(k2, interp_qlpc, awk2, lpcSize);
					org.xiph.speex.Filters.bw_lpc(k3, interp_qlpc, awk3, lpcSize);
				}
				float innov_gain = 0;
				for (i = 0; i < frameSize; i++)
				{
					innov_gain += innov[i] * innov[i];
				}
				innov_gain = (float)System.Math.Sqrt(innov_gain / frameSize);
				for (i = 0; i < subframeSize; i++)
				{
					//#if 0
					//          excBuf[extIdx+i] = pitch_gain*excBuf[extIdx+i-last_pitch] + fact*((float)Math.sqrt(1-pitch_gain))*innov[i+offset];
					//          /*Just so it give the same lost packets as with if 0*/
					//          /*rand();*/
					//#else
					excBuf[extIdx + i] = pitch_gain * (excBuf[extIdx + i - last_pitch] + VERY_SMALL) 
						+ fact * ((float)System.Math.Sqrt(1 - pitch_gain)) * 3 * innov_gain * ((random.nextFloat
						()) - 0.5f);
				}
				//#endif
				for (i = 0; i < subframeSize; i++)
				{
					frmBuf[spIdx + i] = excBuf[extIdx + i] + VERY_SMALL;
				}
				if (enhanced)
				{
					org.xiph.speex.Filters.filter_mem2(frmBuf, spIdx, awk2, awk1, subframeSize, lpcSize
						, mem_sp, lpcSize);
					org.xiph.speex.Filters.filter_mem2(frmBuf, spIdx, awk3, interp_qlpc, subframeSize
						, lpcSize, mem_sp, 0);
				}
				else
				{
					for (i = 0; i < lpcSize; i++)
					{
						mem_sp[lpcSize + i] = 0;
					}
					org.xiph.speex.Filters.iir_mem2(frmBuf, spIdx, interp_qlpc, frmBuf, spIdx, subframeSize
						, lpcSize, mem_sp);
				}
			}
			@out[0] = frmBuf[0] + preemph * pre_mem;
			for (i = 1; i < frameSize; i++)
			{
				@out[i] = frmBuf[i] + preemph * @out[i - 1];
			}
			pre_mem = @out[frameSize - 1];
			first = 0;
			count_lost++;
			pitch_gain_buf[pitch_gain_buf_idx++] = pitch_gain;
			if (pitch_gain_buf_idx > 2)
			{
				pitch_gain_buf_idx = 0;
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
