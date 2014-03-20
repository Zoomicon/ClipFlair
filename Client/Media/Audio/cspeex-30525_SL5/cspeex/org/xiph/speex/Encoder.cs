namespace org.xiph.speex
{
	/// <summary>
	/// Speex Encoder interface, used as a base for the Narrowband and sideband
	/// encoders.
	/// </summary>
	/// <remarks>
	/// Speex Encoder interface, used as a base for the Narrowband and sideband
	/// encoders.
	/// </remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public interface Encoder
	{
		/// <summary>Encode the given input signal.</summary>
		/// <remarks>Encode the given input signal.</remarks>
		/// <param name="bits">- Speex bits buffer.</param>
		/// <param name="in">- the raw mono audio frame to encode.</param>
		/// <returns>1 if successful.</returns>
		int encode(org.xiph.speex.Bits bits, float[] @in);

		/// <summary>Returns the size in bits of an audio frame encoded with the current mode.
		/// 	</summary>
		/// <remarks>Returns the size in bits of an audio frame encoded with the current mode.
		/// 	</remarks>
		/// <returns>the size in bits of an audio frame encoded with the current mode.</returns>
		int getEncodedFrameSize();

		//--------------------------------------------------------------------------
		// Speex Control Functions
		//--------------------------------------------------------------------------
		/// <summary>Returns the size of a frame.</summary>
		/// <remarks>Returns the size of a frame.</remarks>
		/// <returns>the size of a frame.</returns>
		int getFrameSize();

		/// <summary>Sets the Quality (between 0 and 10).</summary>
		/// <remarks>Sets the Quality (between 0 and 10).</remarks>
		/// <param name="quality">- the desired Quality (between 0 and 10).</param>
		void setQuality(int quality);

		/// <summary>Get the current Bit Rate.</summary>
		/// <remarks>Get the current Bit Rate.</remarks>
		/// <returns>the current Bit Rate.</returns>
		int getBitRate();

		//  public void resetState();
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

		/// <summary>Sets the encoding submode.</summary>
		/// <remarks>Sets the encoding submode.</remarks>
		/// <param name="mode"></param>
		void setMode(int mode);

		/// <summary>Returns the encoding submode currently in use.</summary>
		/// <remarks>Returns the encoding submode currently in use.</remarks>
		/// <returns>the encoding submode currently in use.</returns>
		int getMode();

		/// <summary>Sets the bitrate.</summary>
		/// <remarks>Sets the bitrate.</remarks>
		/// <param name="bitrate"></param>
		void setBitRate(int bitrate);

		/// <summary>Sets whether or not to use Variable Bit Rate encoding.</summary>
		/// <remarks>Sets whether or not to use Variable Bit Rate encoding.</remarks>
		/// <param name="vbr"></param>
		void setVbr(bool vbr);

		/// <summary>Returns whether or not we are using Variable Bit Rate encoding.</summary>
		/// <remarks>Returns whether or not we are using Variable Bit Rate encoding.</remarks>
		/// <returns>whether or not we are using Variable Bit Rate encoding.</returns>
		bool getVbr();

		/// <summary>Sets whether or not to use Voice Activity Detection encoding.</summary>
		/// <remarks>Sets whether or not to use Voice Activity Detection encoding.</remarks>
		/// <param name="vad"></param>
		void setVad(bool vad);

		/// <summary>Returns whether or not we are using Voice Activity Detection encoding.</summary>
		/// <remarks>Returns whether or not we are using Voice Activity Detection encoding.</remarks>
		/// <returns>whether or not we are using Voice Activity Detection encoding.</returns>
		bool getVad();

		/// <summary>Sets whether or not to use Discontinuous Transmission encoding.</summary>
		/// <remarks>Sets whether or not to use Discontinuous Transmission encoding.</remarks>
		/// <param name="dtx"></param>
		void setDtx(bool dtx);

		/// <summary>Returns whether or not we are using Discontinuous Transmission encoding.
		/// 	</summary>
		/// <remarks>Returns whether or not we are using Discontinuous Transmission encoding.
		/// 	</remarks>
		/// <returns>whether or not we are using Discontinuous Transmission encoding.</returns>
		bool getDtx();

		/// <summary>Returns the Average Bit Rate used (0 if ABR is not turned on).</summary>
		/// <remarks>Returns the Average Bit Rate used (0 if ABR is not turned on).</remarks>
		/// <returns>the Average Bit Rate used (0 if ABR is not turned on).</returns>
		int getAbr();

		/// <summary>Sets the Average Bit Rate.</summary>
		/// <remarks>Sets the Average Bit Rate.</remarks>
		/// <param name="abr">- the desired Average Bit Rate.</param>
		void setAbr(int abr);

		/// <summary>Sets the Varible Bit Rate Quality.</summary>
		/// <remarks>Sets the Varible Bit Rate Quality.</remarks>
		/// <param name="quality">- the desired Varible Bit Rate Quality.</param>
		void setVbrQuality(float quality);

		/// <summary>Returns the Varible Bit Rate Quality.</summary>
		/// <remarks>Returns the Varible Bit Rate Quality.</remarks>
		/// <returns>the Varible Bit Rate Quality.</returns>
		float getVbrQuality();

		/// <summary>Sets the algorithmic complexity.</summary>
		/// <remarks>Sets the algorithmic complexity.</remarks>
		/// <param name="complexity">- the desired algorithmic complexity (between 1 and 10 - default is 3).
		/// 	</param>
		void setComplexity(int complexity);

		/// <summary>Returns the algorthmic complexity.</summary>
		/// <remarks>Returns the algorthmic complexity.</remarks>
		/// <returns>the algorthmic complexity.</returns>
		int getComplexity();

		/// <summary>Sets the sampling rate.</summary>
		/// <remarks>Sets the sampling rate.</remarks>
		/// <param name="rate">- the sampling rate.</param>
		void setSamplingRate(int rate);

		/// <summary>Returns the sampling rate.</summary>
		/// <remarks>Returns the sampling rate.</remarks>
		/// <returns>the sampling rate.</returns>
		int getSamplingRate();

		/// <summary>Return LookAhead.</summary>
		/// <remarks>Return LookAhead.</remarks>
		/// <returns>LookAhead.</returns>
		int getLookAhead();

		/// <summary>Returns the relative quality.</summary>
		/// <remarks>Returns the relative quality.</remarks>
		/// <returns>the relative quality.</returns>
		float getRelativeQuality();
	}
}
