//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ClipFlairMetadtata.cs
//Version: 20130918

using Metadata.CXML;

using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace ClipFlair.Metadata
{

  public abstract class ClipFlairMetadata : CXMLMetadata, IClipFlairMetadata
  {

    #region --- Properties ---
   
    //Facets//
    public string Filename { get; set; }
    public string[] Keywords { get; set; }
    public string License { get; set; }

    #endregion

    #region --- Load ---

    public override ICXMLMetadata Load(XElement item)
    {
      base.Load(item);

      IEnumerable<XElement> facets = FindFacets(item);

      Filename = facets.CXMLFacetStringValue(ClipFlairMetadataFacets.FACET_FILENAME);
      Keywords = facets.CXMLFacetStringValues(ClipFlairMetadataFacets.FACET_KEYWORDS);
      License = facets.CXMLFacetStringValue(ClipFlairMetadataFacets.FACET_LICENSE);

      return this;
    }

    public override ICXMLMetadata Load(string key, XDocument doc)
    {
      try
      {
        XElement item = FindItem(key, doc);
        Load(item);
        Fix();
      }
      catch
      {
        Clear();
      }

      return this;
    }

    public override ICXMLMetadata Load(string key, XmlReader cxml, XmlReader cxmlFallback)
    {
      return ((IClipFlairMetadata)base.Load(key, cxml, cxmlFallback));
    }

    public override ICXMLMetadata Fix()
    {
      if (string.IsNullOrWhiteSpace(Title)) //also checks for empty string
        Title = Filename;

      return this;
    }

    #endregion

    public override void Clear()
    {
      base.Clear();
 
      //Facets//
      Filename = "";
      Keywords = new string[] { };
      License = "";
    }

    #region --- Helpers ---

    public static XElement FindItem(string key, XDocument doc)
    {
      return doc.Root.Elements(CXML.NODE_ITEMS).Elements(CXML.NODE_ITEM).CXMLFirstItemWithStringValue(ClipFlairMetadataFacets.FACET_FILENAME, key);
    }

    #endregion

  }

}