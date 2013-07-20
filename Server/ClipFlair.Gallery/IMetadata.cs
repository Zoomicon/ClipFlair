//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMetadata.cs
//Version: 20130720

using System;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public interface IMetadata
  {

    #region --- Properties ---

    string Id { get; set; }
    string Title { get; set; }
    string Image { get; set; }
    Uri Url { get; set; }
    string Description { get; set; }
    string Filename { get; set; }

    #endregion

    #region --- Methods ---

    XElement GetCXMLItem();
    void Clear();
    IMetadata Load(string key, XDocument doc);
    IMetadata Load(string key, string cxmlFilename, string cxmlFallbackFilename);
    void Save(string cxmlFilename);

    #endregion

  }

}