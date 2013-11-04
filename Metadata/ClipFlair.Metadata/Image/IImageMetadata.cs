//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageMetadata.cs
//Version: 20131101

namespace ClipFlair.Metadata
{

  public interface IImageMetadata : IClipFlairMetadata
  {

    #region --- Properties ---

    string[] CaptionsLanguage { get; set; }
    //string[] Genre { get; set; }

    #endregion

  }

}