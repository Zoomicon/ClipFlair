//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICXMLMetadata.cs
//Version: 20130918

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Metadata.CXML
{

  public interface ICXMLMetadata
  {

    #region --- Properties ---

    string Id { get; set; }
    string Title { get; set; }
    string Image { get; set; }
    Uri Url { get; set; }
    string Description { get; set; }

    #endregion

    #region --- Methods ---

    XElement GetCXMLItem();
    IEnumerable<XElement> GetCXMLFacetCategories();
    IEnumerable<XElement> GetCXMLFacets();

    void Clear();

    ICXMLMetadata Load(XElement item);
    ICXMLMetadata Load(string key, XDocument doc);
    ICXMLMetadata Load(string key, XmlReader cxml, XmlReader cxmlFallback);

    ICXMLMetadata Fix();
    
    void Save(XmlWriter cxml);

    #endregion

  }

}