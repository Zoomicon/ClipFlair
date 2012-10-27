namespace org.xiph.speex
{
	/// <summary>
	/// Speex Decoder inteface, used as a base for the Narrowband and sideband
	/// decoders.
	/// </summary>
	/// <remarks>
	/// Speex Decoder inteface, used as a base for the Narrowband and sideband
	/// decoders.
	/// </remarks>
	/// <author>Jim Lawrence, helloNetwork.com</author>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 140 $</version>
	public interface Decoder
	{
		/// <summary>Decode the given input bits.</summary>
		/// <remarks>Decode the given input bits.</remarks>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="out">- the decoded mono audio frame.</param>
		/// <returns>1 if a terminator was found, 0 if not.</returns>
		/// <exception cref="java.io.StreamCorruptedException">
		/// If there is an error detected in the
		/// data stream.
		/// </exception>
		int decode(org.xiph.speex.Bits bits, float[] @out);

		/// <summary>Decode the given bits to stereo.</summary>
		/// <remarks>Decode the given bits to stereo.</remarks>
		/// <param name="data">
		/// - float array of size 2*frameSize, that contains the mono
		/// audio samples in the first half. When the function has completed, the
		/// array will contain the interlaced stereo audio samples.
		/// </param>
		/// <param name="frameSize">- the size of a frame of mono audio samples.</param>
		void decodeStereo(float[] data, int frameSize);

		/// <summary>Enables or disables perceptual enhancement.</summary>
		/// <remarks>Enables or disables perceptual enhancement.</remarks>
		/// <param name="enhanced"></param>
		void setPerceptualEnhancement(bool enhanced);

		/// <summary>Returns whether perceptual enhancement is enabled or disabled.</summary>
		/// <remarks>Returns whether perceptual enhancement is enabled or disabled.</remarks>
		/// <returns>whether perceptual enhancement is enabled or disabled.</returns>
		bool getPerceptualEnhancement();

		/// <summary>Returns the size of a frame.</summary>
		/// <remarks>Returns the size of a frame.</remarks>
		/// <returns>the size of a frame.</returns>
		int getFrameSize();

		/// <summary>Returns whether or not we are using Discontinuous Transmission encoding.
		/// 	</summary>
		/// <remarks>Returns whether or not we are using Discontinuous Transmission encoding.
		/// 	</remarks>
		/// <returns>whether or not we are using Discontinuous Transmission encoding.</returns>
		bool getDtx();

		/// <summary>Returns the Pitch Gain array.</summary>
		/// <remarks>Returns the Pitch Gain array.</remarks>
		/// <returns>the Pitch Gain array.</returns>
		float[] getPiGain();

		/// <summary>Returns the excitation array.</summary>
		/// <remarks>Returns the excitation array.</remarks>
		/// <returns>the excitation array.</returns>
		float[] getExc();

		/// <summary>Returns the innovation array.</summary>
		/// <remarks>Returns the innovation array.</remarks>
		/// <returns>the innovation array.</returns>
		float[] getInnov();
	}
}
