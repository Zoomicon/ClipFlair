namespace org.xiph.speex
{
	/// <summary>Narrowband Codec.</summary>
	/// <remarks>
	/// Narrowband Codec.
	/// This class contains all the basic structures needed by the Narrowband
	/// encoder and decoder.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class NbCodec : org.xiph.speex.Codebook
	{
		/// <summary>Very small initial value for some of the buffers.</summary>
		/// <remarks>Very small initial value for some of the buffers.</remarks>
		public const float VERY_SMALL = (float)0e-15;

		/// <summary>The Narrowband Frame Size gives the size in bits of a Narrowband frame for a given narrowband submode.
		/// 	</summary>
		/// <remarks>The Narrowband Frame Size gives the size in bits of a Narrowband frame for a given narrowband submode.
		/// 	</remarks>
		public static readonly int[] NB_FRAME_SIZE = new int[] { 5, 43, 119, 160, 220, 300
			, 364, 492, 79, 1, 1, 1, 1, 1, 1, 1 };

		/// <summary>The Narrowband Submodes gives the number of submodes possible for the Narrowband codec.
		/// 	</summary>
		/// <remarks>The Narrowband Submodes gives the number of submodes possible for the Narrowband codec.
		/// 	</remarks>
		public const int NB_SUBMODES = 16;

		/// <summary>The Narrowband Submodes Bits gives the number bits used to encode the Narrowband Submode
		/// 	</summary>
		public const int NB_SUBMODE_BITS = 4;

		public static readonly float[] exc_gain_quant_scal1 = new float[] { -0.35f, 0.05f
			 };

		public static readonly float[] exc_gain_quant_scal3 = new float[] { -2.794750f, -
			1.810660f, -1.169850f, -0.848119f, -0.587190f, -0.329818f, -0.063266f, 0.282826f
			 };

		protected org.xiph.speex.Lsp m_lsp;

		protected org.xiph.speex.Filters filters;

		protected org.xiph.speex.SubMode[] submodes;

		/// <summary>Sub-mode data</summary>
		protected int submodeID;

		/// <summary>Activated sub-mode</summary>
		protected int first;

		/// <summary>Is this the first frame?</summary>
		protected int frameSize;

		/// <summary>Size of frames</summary>
		protected int subframeSize;

		/// <summary>Size of sub-frames</summary>
		protected int nbSubframes;

		/// <summary>Number of sub-frames</summary>
		protected int windowSize;

		/// <summary>Analysis (LPC) window length</summary>
		protected int lpcSize;

		/// <summary>LPC order</summary>
		protected int bufSize;

		/// <summary>Buffer size</summary>
		protected int min_pitch;

		/// <summary>Minimum pitch value allowed</summary>
		protected int max_pitch;

		/// <summary>Maximum pitch value allowed</summary>
		protected float gamma1;

		/// <summary>Perceptual filter: A(z/gamma1)</summary>
		protected float gamma2;

		/// <summary>Perceptual filter: A(z/gamma2)</summary>
		protected float lag_factor;

		/// <summary>Lag windowing Gaussian width</summary>
		protected float lpc_floor;

		/// <summary>Noise floor multiplier for A[0] in LPC analysis</summary>
		protected float preemph;

		/// <summary>Pre-emphasis: P(z) = 1 - a*z^-1</summary>
		protected float pre_mem;

		/// <summary>1-element memory for pre-emphasis</summary>
		protected float[] frmBuf;

		/// <summary>Input buffer (original signal)</summary>
		protected int frmIdx;

		protected float[] excBuf;

		/// <summary>Excitation buffer</summary>
		protected int excIdx;

		/// <summary>Start of excitation frame</summary>
		protected float[] innov;

		/// <summary>Innovation for the frame</summary>
		protected float[] lpc;

		/// <summary>LPCs for current frame</summary>
		protected float[] qlsp;

		/// <summary>Quantized LSPs for current frame</summary>
		protected float[] old_qlsp;

		/// <summary>Quantized LSPs for previous frame</summary>
		protected float[] interp_qlsp;

		/// <summary>Interpolated quantized LSPs</summary>
		protected float[] interp_qlpc;

		/// <summary>Interpolated quantized LPCs</summary>
		protected float[] mem_sp;

		/// <summary>Filter memory for synthesis signal</summary>
		protected float[] pi_gain;

		/// <summary>Gain of LPC filter at theta=pi (fe/2)</summary>
		protected float[] awk1;

		/// <summary>Gain of LPC filter at theta=pi (fe/2)</summary>
		protected float[] awk2;

		/// <summary>Gain of LPC filter at theta=pi (fe/2)</summary>
		protected float[] awk3;

		protected float voc_m1;

		protected float voc_m2;

		protected float voc_mean;

		protected int voc_offset;

		protected int dtx_enabled;

		/// <summary>Constructor.</summary>
		/// <remarks>Constructor.</remarks>
		public NbCodec()
		{
			//---------------------------------------------------------------------------
			// Constants
			//---------------------------------------------------------------------------
			//---------------------------------------------------------------------------
			// Tools
			//---------------------------------------------------------------------------
			//---------------------------------------------------------------------------
			// Parameters
			//---------------------------------------------------------------------------
			//---------------------------------------------------------------------------
			// Variables
			//---------------------------------------------------------------------------
			// Vocoder data
			m_lsp = new org.xiph.speex.Lsp();
			filters = new org.xiph.speex.Filters();
		}

		/// <summary>Narrowband initialisation.</summary>
		/// <remarks>Narrowband initialisation.</remarks>
		public virtual void nbinit()
		{
			// Initialize SubModes
			submodes = buildNbSubModes();
			submodeID = 5;
			// Initialize narrwoband parameters and variables
			init(160, 40, 10, 640);
		}

		/// <summary>Initialisation.</summary>
		/// <remarks>Initialisation.</remarks>
		/// <param name="frameSize"></param>
		/// <param name="subframeSize"></param>
		/// <param name="lpcSize"></param>
		/// <param name="bufSize"></param>
		public virtual void init(int frameSize, int subframeSize, int lpcSize, int bufSize
			)
		{
			first = 1;
			// Codec parameters, should eventually have several "modes"
			this.frameSize = frameSize;
			this.windowSize = frameSize * 3 / 2;
			this.subframeSize = subframeSize;
			this.nbSubframes = frameSize / subframeSize;
			this.lpcSize = lpcSize;
			this.bufSize = bufSize;
			min_pitch = 17;
			max_pitch = 144;
			preemph = 0.0f;
			pre_mem = 0.0f;
			gamma1 = 0.9f;
			gamma2 = 0.6f;
			lag_factor = .01f;
			lpc_floor = 1.0001f;
			frmBuf = new float[bufSize];
			frmIdx = bufSize - windowSize;
			excBuf = new float[bufSize];
			excIdx = bufSize - windowSize;
			innov = new float[frameSize];
			lpc = new float[lpcSize + 1];
			qlsp = new float[lpcSize];
			old_qlsp = new float[lpcSize];
			interp_qlsp = new float[lpcSize];
			interp_qlpc = new float[lpcSize + 1];
			mem_sp = new float[5 * lpcSize];
			//TODO - check why 5 (why not 2 or 1)
			pi_gain = new float[nbSubframes];
			awk1 = new float[lpcSize + 1];
			awk2 = new float[lpcSize + 1];
			awk3 = new float[lpcSize + 1];
			voc_m1 = voc_m2 = voc_mean = 0;
			voc_offset = 0;
			dtx_enabled = 0;
		}

		// disabled by default
		/// <summary>Build narrowband submodes</summary>
		private static org.xiph.speex.SubMode[] buildNbSubModes()
		{
			org.xiph.speex.Ltp3Tap ltpNb = new org.xiph.speex.Ltp3Tap(gain_cdbk_nb, 7, 7);
			org.xiph.speex.Ltp3Tap ltpVlbr = new org.xiph.speex.Ltp3Tap(gain_cdbk_lbr, 5, 0);
			org.xiph.speex.Ltp3Tap ltpLbr = new org.xiph.speex.Ltp3Tap(gain_cdbk_lbr, 5, 7);
			org.xiph.speex.Ltp3Tap ltpMed = new org.xiph.speex.Ltp3Tap(gain_cdbk_lbr, 5, 7);
			org.xiph.speex.LtpForcedPitch ltpFP = new org.xiph.speex.LtpForcedPitch();
			org.xiph.speex.NoiseSearch noiseSearch = new org.xiph.speex.NoiseSearch();
			org.xiph.speex.SplitShapeSearch ssNbVlbrSearch = new org.xiph.speex.SplitShapeSearch
				(40, 10, 4, exc_10_16_table, 4, 0);
			org.xiph.speex.SplitShapeSearch ssNbLbrSearch = new org.xiph.speex.SplitShapeSearch
				(40, 10, 4, exc_10_32_table, 5, 0);
			org.xiph.speex.SplitShapeSearch ssNbSearch = new org.xiph.speex.SplitShapeSearch(
				40, 5, 8, exc_5_64_table, 6, 0);
			org.xiph.speex.SplitShapeSearch ssNbMedSearch = new org.xiph.speex.SplitShapeSearch
				(40, 8, 5, exc_8_128_table, 7, 0);
			org.xiph.speex.SplitShapeSearch ssSbSearch = new org.xiph.speex.SplitShapeSearch(
				40, 5, 8, exc_5_256_table, 8, 0);
			org.xiph.speex.SplitShapeSearch ssNbUlbrSearch = new org.xiph.speex.SplitShapeSearch
				(40, 20, 2, exc_20_32_table, 5, 0);
			org.xiph.speex.NbLspQuant nbLspQuant = new org.xiph.speex.NbLspQuant();
			org.xiph.speex.LbrLspQuant lbrLspQuant = new org.xiph.speex.LbrLspQuant();
			org.xiph.speex.SubMode[] nbSubModes = new org.xiph.speex.SubMode[NB_SUBMODES];
			nbSubModes[1] = new org.xiph.speex.SubMode(0, 1, 0, 0, lbrLspQuant, ltpFP, noiseSearch
				, .7f, .7f, -1, 43);
			nbSubModes[2] = new org.xiph.speex.SubMode(0, 0, 0, 0, lbrLspQuant, ltpVlbr, ssNbVlbrSearch
				, 0.7f, 0.5f, .55f, 119);
			nbSubModes[3] = new org.xiph.speex.SubMode(-1, 0, 1, 0, lbrLspQuant, ltpLbr, ssNbLbrSearch
				, 0.7f, 0.55f, .45f, 160);
			nbSubModes[4] = new org.xiph.speex.SubMode(-1, 0, 1, 0, lbrLspQuant, ltpMed, ssNbMedSearch
				, 0.7f, 0.63f, .35f, 220);
			nbSubModes[5] = new org.xiph.speex.SubMode(-1, 0, 3, 0, nbLspQuant, ltpNb, ssNbSearch
				, 0.7f, 0.65f, .25f, 300);
			nbSubModes[6] = new org.xiph.speex.SubMode(-1, 0, 3, 0, nbLspQuant, ltpNb, ssSbSearch
				, 0.68f, 0.65f, .1f, 364);
			nbSubModes[7] = new org.xiph.speex.SubMode(-1, 0, 3, 1, nbLspQuant, ltpNb, ssNbSearch
				, 0.65f, 0.65f, -1, 492);
			nbSubModes[8] = new org.xiph.speex.SubMode(0, 1, 0, 0, lbrLspQuant, ltpFP, ssNbUlbrSearch
				, .7f, .5f, .65f, 79);
			return nbSubModes;
		}

		/// <summary>
		/// Returns the size of a frame (ex: 160 samples for a narrowband frame,
		/// 320 for wideband and 640 for ultra-wideband).
		/// </summary>
		/// <remarks>
		/// Returns the size of a frame (ex: 160 samples for a narrowband frame,
		/// 320 for wideband and 640 for ultra-wideband).
		/// </remarks>
		/// <returns>the size of a frame (number of audio samples in a frame).</returns>
		public virtual int getFrameSize()
		{
			return frameSize;
		}

		/// <summary>Returns whether or not we are using Discontinuous Transmission encoding.
		/// 	</summary>
		/// <remarks>Returns whether or not we are using Discontinuous Transmission encoding.
		/// 	</remarks>
		/// <returns>whether or not we are using Discontinuous Transmission encoding.</returns>
		public virtual bool getDtx()
		{
			return dtx_enabled != 0;
		}

		/// <summary>Returns the Pitch Gain array.</summary>
		/// <remarks>Returns the Pitch Gain array.</remarks>
		/// <returns>the Pitch Gain array.</returns>
		public virtual float[] getPiGain()
		{
			return pi_gain;
		}

		/// <summary>Returns the excitation array.</summary>
		/// <remarks>Returns the excitation array.</remarks>
		/// <returns>the excitation array.</returns>
		public virtual float[] getExc()
		{
			float[] excTmp = new float[frameSize];
			System.Array.Copy(excBuf, excIdx, excTmp, 0, frameSize);
			return excTmp;
		}

		/// <summary>Returns the innovation array.</summary>
		/// <remarks>Returns the innovation array.</remarks>
		/// <returns>the innovation array.</returns>
		public virtual float[] getInnov()
		{
			return innov;
		}
	}
}
