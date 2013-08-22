//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IVideoMetadata.cs
//Version: 20130823

using System;
using System.Xml.Linq;

namespace ClipFlair.Metadata
{

  public interface IVideoMetadata : IClipFlairMetadata
  {

    #region --- Properties ---

    string[] AudioLanguage { get; set; }
    string[] CaptionsLanguage { get; set; }
    string[] Genre { get; set; }
    bool AgeRestricted { get; set; }
    string Duration { get; set; }
    string[] AudiovisualRichness { get; set; }
    bool PedagogicalAdaptability { get; set; }
    string AuthorSource { get; set; }

    #endregion

  }

}