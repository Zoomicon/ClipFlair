//Filename: AudioFormatEx.cs
//Version: 20141117
//Author: George Birbilis (http://zoomicon.com)

using System.Collections.ObjectModel;
using System.Windows.Media;

namespace AudioLib
{

  /// <summary>
  /// Class AudioFormatEx
  /// Extension of AudioFormat class that is instantiatable and can also wrap standard WAVEFORMATEX structure 
  /// </summary>
  public class AudioFormatEx
  {

    #region --- Fields ---

    private WaveFormatType format;
    private int bitsPerSample;
    private int channels;
    private int samplesPerSecond;

    #endregion

    #region --- Initialization ---

    public AudioFormatEx(WaveFormatType format, int channels, int bitsPerSample, int samplesPerSecond)
    {
      this.format = format;
      this.channels = channels;
      this.bitsPerSample = bitsPerSample;
      this.samplesPerSecond = samplesPerSecond;
    }

    public AudioFormatEx(AudioFormat audioFormat)
      : this(audioFormat.WaveFormat, audioFormat.Channels, audioFormat.BitsPerSample, audioFormat.SamplesPerSecond)
    {
      //NOP
    }

    public AudioFormatEx(WAVEFORMATEX wavFormat)
      : this((WaveFormatType)wavFormat.FormatTag, wavFormat.Channels, wavFormat.BitsPerSample, wavFormat.SamplesPerSec)
    { //note: WAVEFORMATEX.FormatPCM = 1 and also WaveFormatType.Pcm = 1 that's why the cast is OK  
      //NOP
    }

    #endregion

    #region --- Properties ---

    // Summary:
    //     Gets the encoding format of the audio format as a System.Windows.Media.WaveFormatType
    //     value.
    //
    // Returns:
    //     The encoding format of the audio format.
    public WaveFormatType WaveFormat
    {
      get { return format; }
      set { format = value; }
    }

    //
    // Summary:
    //     Gets the number of channels that are provided by the audio format.
    //
    // Returns:
    //     The number of channels that are provided by the audio format.
    public int Channels
    {
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
    public int BitsPerSample
    {
      get { return bitsPerSample; }
      set { bitsPerSample = value; }
    }

    /// <summary>
    /// Gets the number of samples per second that are provided by the audio format.
    /// </summary>
    /// <value>
    /// The number of samples per second that are provided by the audio format.
    /// </value>
    public int SamplesPerSecond
    {
      get { return samplesPerSecond; }
      set { samplesPerSecond = value; }
    }

    #endregion

    #region --- Methods ---

    public override bool Equals(object obj)
    {
      if (obj == null) return false;

      AudioFormatEx formatObj = null;
      if (obj.GetType() == typeof(AudioFormatEx))
        formatObj = (AudioFormatEx)obj;
      else if (obj.GetType() == typeof(AudioFormat))
        formatObj = new AudioFormatEx((AudioFormat)obj);
      else if (obj.GetType() == typeof(WAVEFORMATEX))
        formatObj = new AudioFormatEx((WAVEFORMATEX)obj);

      return (format == formatObj.format) &&
             (bitsPerSample == formatObj.bitsPerSample) &&
             (channels == formatObj.channels) &&
             (samplesPerSecond == formatObj.samplesPerSecond);
    }

    public static AudioFormat PickAudioFormat(ReadOnlyCollection<AudioFormat> audioFormats, AudioFormatEx desiredFormat)
    {
      foreach (AudioFormat audioFormat in audioFormats)
        if (desiredFormat.Equals(audioFormat))
          return audioFormat;
      return null;
    }

    #endregion

  }

}
