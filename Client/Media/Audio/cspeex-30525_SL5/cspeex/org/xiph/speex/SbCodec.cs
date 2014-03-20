namespace org.xiph.speex
{
	/// <summary>Sideband Codec.</summary>
	/// <remarks>
	/// Sideband Codec.
	/// This class contains all the basic structures needed by the Sideband
	/// encoder and decoder.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class SbCodec : org.xiph.speex.NbCodec
	{
		/// <summary>The Sideband Frame Size gives the size in bits of a Sideband frame for a given sideband submode.
		/// 	</summary>
		/// <remarks>The Sideband Frame Size gives the size in bits of a Sideband frame for a given sideband submode.
		/// 	</remarks>
		public static readonly int[] SB_FRAME_SIZE = new int[] { 4, 36, 112, 192, 352, -1
			, -1, -1 };

		/// <summary>The Sideband Submodes gives the number of submodes possible for the Sideband codec.
		/// 	</summary>
		/// <remarks>The Sideband Submodes gives the number of submodes possible for the Sideband codec.
		/// 	</remarks>
		public const int SB_SUBMODES = 8;

		/// <summary>The Sideband Submodes Bits gives the number bits used to encode the Sideband Submode
		/// 	</summary>
		public const int SB_SUBMODE_BITS = 3;

		/// <summary>Quadratic Mirror Filter Order</summary>
		public const int QMF_ORDER = 64;

		protected int fullFrameSize;

		protected float foldingGain;

		protected float[] high;

		protected float[] y0;

		protected float[] y1;

		protected float[] x0d;

		protected float[] g0_mem;

		protected float[] g1_mem;

		//---------------------------------------------------------------------------
		// Constants
		//---------------------------------------------------------------------------
		//---------------------------------------------------------------------------
		// Parameters
		//---------------------------------------------------------------------------
		//---------------------------------------------------------------------------
		// Variables
		//---------------------------------------------------------------------------
		/// <summary>Wideband initialisation</summary>
		public virtual void wbinit()
		{
			// Initialize SubModes
			submodes = buildWbSubModes();
			submodeID = 3;
		}

		// Initialize narrwoband parameters and variables
		//init(160, 40, 8, 640, .9f);
		/// <summary>Ultra-wideband initialisation</summary>
		public virtual void uwbinit()
		{
			// Initialize SubModes
			submodes = buildUwbSubModes();
			submodeID = 1;
		}

		// Initialize narrwoband parameters and variables
		//init(320, 80, 8, 1280, .7f);
		/// <summary>Initialisation</summary>
		/// <param name="frameSize"></param>
		/// <param name="subframeSize"></param>
		/// <param name="lpcSize"></param>
		/// <param name="bufSize"></param>
		/// <param name="foldingGain"></param>
		public virtual void init(int frameSize, int subframeSize, int lpcSize, int bufSize
			, float foldingGain)
		{
			base.init(frameSize, subframeSize, lpcSize, bufSize);
			this.fullFrameSize = 2 * frameSize;
			this.foldingGain = foldingGain;
			lag_factor = 0.002f;
			high = new float[fullFrameSize];
			y0 = new float[fullFrameSize];
			y1 = new float[fullFrameSize];
			x0d = new float[frameSize];
			g0_mem = new float[QMF_ORDER];
			g1_mem = new float[QMF_ORDER];
		}

		/// <summary>Build wideband submodes.</summary>
		/// <remarks>Build wideband submodes.</remarks>
		/// <returns>the wideband submodes.</returns>
		protected static org.xiph.speex.SubMode[] buildWbSubModes()
		{
			org.xiph.speex.HighLspQuant highLU = new org.xiph.speex.HighLspQuant();
			org.xiph.speex.SplitShapeSearch ssCbHighLbrSearch = new org.xiph.speex.SplitShapeSearch
				(40, 10, 4, hexc_10_32_table, 5, 0);
			org.xiph.speex.SplitShapeSearch ssCbHighSearch = new org.xiph.speex.SplitShapeSearch
				(40, 8, 5, hexc_table, 7, 1);
			org.xiph.speex.SubMode[] wbSubModes = new org.xiph.speex.SubMode[SB_SUBMODES];
			wbSubModes[1] = new org.xiph.speex.SubMode(0, 0, 1, 0, highLU, null, null, .75f, 
				.75f, -1, 36);
			wbSubModes[2] = new org.xiph.speex.SubMode(0, 0, 1, 0, highLU, null, ssCbHighLbrSearch
				, .85f, .6f, -1, 112);
			wbSubModes[3] = new org.xiph.speex.SubMode(0, 0, 1, 0, highLU, null, ssCbHighSearch
				, .75f, .7f, -1, 192);
			wbSubModes[4] = new org.xiph.speex.SubMode(0, 0, 1, 1, highLU, null, ssCbHighSearch
				, .75f, .75f, -1, 352);
			return wbSubModes;
		}

		/// <summary>Build ultra-wideband submodes.</summary>
		/// <remarks>Build ultra-wideband submodes.</remarks>
		/// <returns>the ultra-wideband submodes.</returns>
		protected static org.xiph.speex.SubMode[] buildUwbSubModes()
		{
			org.xiph.speex.HighLspQuant highLU = new org.xiph.speex.HighLspQuant();
			org.xiph.speex.SubMode[] uwbSubModes = new org.xiph.speex.SubMode[SB_SUBMODES];
			uwbSubModes[1] = new org.xiph.speex.SubMode(0, 0, 1, 0, highLU, null, null, .75f, 
				.75f, -1, 2);
			return uwbSubModes;
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
		public override int getFrameSize()
		{
			return fullFrameSize;
		}

		/// <summary>Returns whether or not we are using Discontinuous Transmission encoding.
		/// 	</summary>
		/// <remarks>Returns whether or not we are using Discontinuous Transmission encoding.
		/// 	</remarks>
		/// <returns>whether or not we are using Discontinuous Transmission encoding.</returns>
		public override bool getDtx()
		{
			// TODO - should return DTX for the NbCodec
			return dtx_enabled != 0;
		}

		/// <summary>Returns the excitation array.</summary>
		/// <remarks>Returns the excitation array.</remarks>
		/// <returns>the excitation array.</returns>
		public override float[] getExc()
		{
			int i;
			float[] excTmp = new float[fullFrameSize];
			for (i = 0; i < frameSize; i++)
			{
				excTmp[2 * i] = 2.0f * excBuf[excIdx + i];
			}
			return excTmp;
		}

		/// <summary>Returns the innovation array.</summary>
		/// <remarks>Returns the innovation array.</remarks>
		/// <returns>the innovation array.</returns>
		public override float[] getInnov()
		{
			return getExc();
		}
	}
}
