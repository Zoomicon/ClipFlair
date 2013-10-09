//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageMetadata.cs
//Version: 20131009

using System;
using System.Xml.Linq;

namespace ClipFlair.Metadata
{

  public interface IImageMetadata : IClipFlairMetadata
  {

    #region --- Properties ---

    string[] CaptionsLanguage { get; set; }
    //string[] Genre { get; set; }
    //bool AgeRestricted { get; set; }
    string AuthorSource { get; set; }

    #endregion

  }

}