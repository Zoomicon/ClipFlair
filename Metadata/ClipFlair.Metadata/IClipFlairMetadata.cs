//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IClipFlairMetadata.cs
//Version: 20131101

using Metadata.CXML;

using System;

namespace ClipFlair.Metadata
{

  public interface IClipFlairMetadata : ICXMLMetadata
  {

    #region --- Properties ---

    //Facets//
    string Filename { get; set; }
    DateTime FirstPublished { get; set; }
    DateTime LastUpdated { get; set; }

    string[] AgeGroup { get; set; }
    string[] Keywords { get; set; }
    string[] AuthorSource { get; set; }
    string License { get; set; }

    #endregion

  }

}