//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMetadata.cs
//Version: 20130719

using System;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public interface IMetadata
  {

    #region --- Methods ---

    void Clear();
    IMetadata Load(string key, XDocument doc);
    IMetadata Load(string key, string cxmlFilename, string cxmlFallbackFilename);
    void Save(string cxmlFilename);

    #endregion

  }

}