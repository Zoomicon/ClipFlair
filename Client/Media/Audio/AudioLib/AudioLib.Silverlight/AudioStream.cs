//Filename: AudioStream.cs
//Version: 20141118
//Author: George Birbilis (http://zoomicon.com)

using Media;
using System.IO;
using System.Windows.Media;

namespace AudioLib
{

  public class AudioStream
  {

    public Stream Data { get; set; }
    public AudioStreamKind Kind {get; set; }

    public AudioStream(Stream s, AudioStreamKind k)
    {
      Data = s;
      Kind = k;
    }

    public AudioStream(Stream s, string fileExtension) : this(s, GetAudioStreamKind(fileExtension))
    {
      ///NOP
    }

    public virtual MediaStreamSource GetMediaStreamSource()
    {
      Data.Position = 0; //rewind the stream if possible (doing it everytime, since it may have been [partially] played before)
 
      switch (Kind)
      {
        case AudioStreamKind.WAV:
          return new WaveMediaStreamSource(Data);
        case AudioStreamKind.MP3:
          return new Mp3MediaStreamSource(Data);
        default:
          return null;
      }
    }        

    public static AudioStreamKind GetAudioStreamKind(string fileExtension)
    {
      if (fileExtension != null)
      {
        string ext = fileExtension.ToUpper();
        if (ext.Equals(".WAV")) return AudioStreamKind.WAV;
        if (ext.Equals(".MP3")) return AudioStreamKind.MP3;
      }
      return AudioStreamKind.UNKNOWN;
    }

  }

}
