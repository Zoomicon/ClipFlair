//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IVideoMetadata.cs
//Version: 20131101

namespace ClipFlair.Metadata
{

  public interface IVideoMetadata : IClipFlairMetadata
  {

    #region --- Properties ---

    string[] AudioLanguage { get; set; }
    string[] CaptionsLanguage { get; set; }
    string[] Genre { get; set; }
    string Duration { get; set; }
    string[] AudiovisualRichness { get; set; }
    bool PedagogicalAdaptability { get; set; }

    #endregion

  }

}