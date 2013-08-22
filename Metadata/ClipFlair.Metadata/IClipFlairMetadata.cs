//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IClipFlairMetadata.cs
//Version: 20130823

using Metadata.CXML;

namespace ClipFlair.Metadata
{

  public interface IClipFlairMetadata : ICXMLMetadata
  {

    #region --- Properties ---

    //Facets//
    string Filename { get; set; }
    string[] Keywords { get; set; }
    string License { get; set; }

    #endregion

  }

}