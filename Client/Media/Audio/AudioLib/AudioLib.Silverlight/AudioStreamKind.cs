//Filename: AudioStreamKind.cs
//Version: 20141117
//Author: George Birbilis (http://zoomicon.com)

namespace AudioLib
{

  public enum AudioStreamKind
  {
    UNKNOWN = 0,
    WAV = 1,
    MP3 = 2
  }

  public static class AudioStreamKindUtils
  {
    public const string EXTENSION_WAV = ".WAV";
    public const string EXTENSION_MP3 = ".MP3";

    public static string GetExtension(this AudioStreamKind kind)
    {
      switch (kind)
      {
        case AudioStreamKind.WAV:
          return EXTENSION_WAV;
        case AudioStreamKind.MP3:
          return EXTENSION_MP3;
        default:
          return "";
      }
    }
  }

}
