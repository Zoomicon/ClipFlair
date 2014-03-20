namespace org.xiph.speex
{
	/// <summary>Long Term Prediction Quantisation and Unquantisation (Forced Pitch)</summary>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class LtpForcedPitch : org.xiph.speex.Ltp
	{
		/// <summary>Long Term Prediction Quantification (Forced Pitch).</summary>
		/// <remarks>Long Term Prediction Quantification (Forced Pitch).</remarks>
		/// <returns>pitch</returns>
		public sealed override int quant(float[] target, float[] sw, int sws, float[] ak, 
			float[] awk1, float[] awk2, float[] exc, int es, int start, int end, float pitch_coef
			, int p, int nsf, org.xiph.speex.Bits bits, float[] exc2, int e2s, float[] r, int
			 complexity)
		{
			int i;
			if (pitch_coef > .99f)
			{
				pitch_coef = .99f;
			}
			for (i = 0; i < nsf; i++)
			{
				exc[es + i] = exc[es + i - start] * pitch_coef;
			}
			return start;
		}

		/// <summary>Long Term Prediction Unquantification (Forced Pitch).</summary>
		/// <remarks>Long Term Prediction Unquantification (Forced Pitch).</remarks>
		/// <param name="exc">- Excitation</param>
		/// <param name="es">- Excitation offset</param>
		/// <param name="start">- Smallest pitch value allowed</param>
		/// <param name="pitch_coef">- Voicing (pitch) coefficient</param>
		/// <param name="nsf">- Number of samples in subframe</param>
		/// <param name="gain_val"></param>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="count_lost"></param>
		/// <param name="subframe_offset"></param>
		/// <param name="last_pitch_gain"></param>
		/// <returns>pitch</returns>
		public sealed override int unquant(float[] exc, int es, int start, float pitch_coef
			, int nsf, float[] gain_val, org.xiph.speex.Bits bits, int count_lost, int subframe_offset
			, float last_pitch_gain)
		{
			int i;
			if (pitch_coef > .99f)
			{
				pitch_coef = .99f;
			}
			for (i = 0; i < nsf; i++)
			{
				exc[es + i] = exc[es + i - start] * pitch_coef;
			}
			gain_val[0] = gain_val[2] = 0;
			gain_val[1] = pitch_coef;
			return start;
		}
	}
}
