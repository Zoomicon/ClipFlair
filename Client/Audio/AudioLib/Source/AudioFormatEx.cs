//Filename: AudioFormatEx.cs
//Version: 20130401
//Author: George Birbilis (http://zoomicon.com)

using System.Windows.Media;

namespace AudioLib
{

    /// <summary>
    /// Class AudioFormatEx
    /// Extension of AudioFormat class to wrap standard WAVEFORMATEX structure 
    /// </summary>
    public class AudioFormatEx
    {

      #region Fields

      private WaveFormatType format;
      private int bitsPerSample;
      private int channels;
      private int samplesPerSecond;

      #endregion

      public AudioFormatEx(WaveFormatType format, int channels, int bitsPerSample, int samplesPerSecond)
      {
        this.format = format;
        this.channels = channels;
        this.bitsPerSample = bitsPerSample;
        this.samplesPerSecond = samplesPerSecond;
      }

      public AudioFormatEx(AudioFormat audioFormat) : this(audioFormat.WaveFormat, audioFormat.Channels, audioFormat.BitsPerSample, audioFormat.SamplesPerSecond)
      {
      }

      public AudioFormatEx(WAVEFORMATEX wavFormat) : this((WaveFormatType)wavFormat.FormatTag, wavFormat.Channels, wavFormat.BitsPerSample, wavFormat.SamplesPerSec)
      { //note: WAVEFORMATEX.FormatPCM = 1 and also WaveFormatType.Pcm = 1 that's why the cast is OK  
      }

      // Summary:
      //     Gets the encoding format of the audio format as a System.Windows.Media.WaveFormatType
      //     value.
      //
      // Returns:
      //     The encoding format of the audio format.
      public WaveFormatType WaveFormat { 
        get { return format; }
        set { format = value; }
      }

      //
      // Summary:
      //     Gets the number of channels that are provided by the audio format.
      //
      // Returns:
      //     The number of channels that are provided by the audio format.
      public int Channels { 
        get { return channels; }
        set { channels = value; }
      }

      /// <summary>
      /// Gets the number of bits that are used to store the audio information for
      /// a single sample of an audio format.
      /// </summary>
      /// <value>
      /// The number of bits that are used to store the audio information for a single
      /// sample of an audio format.
      /// </value>
      public int BitsPerSample { 
        get { return bitsPerSample; }
        set { bitsPerSample = value; }
      }

      
      //
      // Summary:
      //     Gets the number of samples per second that are provided by the audio format.
      //
      // Returns:
      //     The number of samples per second that are provided by the audio format.
      public int SamplesPerSecond {
        get { return samplesPerSecond; }
        set { samplesPerSecond = value; }
      }
   
    }

}
