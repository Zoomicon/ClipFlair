//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CXMLMetadata.cs
//Version: 20130822

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Metadata.CXML
{

  public abstract class CXMLMetadata : ICXMLMetadata
  {

    #region --- Properties ---

    public string Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public Uri Url { get; set; }
    public string Description { get; set; }
    
    #endregion

    #region --- Methods ---

    public XElement GetCXMLItem()
    {
      return
        new XElement(CXML.NODE_ITEM,
          new XAttribute(CXML.ATTRIB_ID, Id),
          new XAttribute(CXML.ATTRIB_NAME, Title),
          new XAttribute(CXML.ATTRIB_IMG, Image),
          new XAttribute(CXML.ATTRIB_HREF, (Url != null) ? Url.ToString() : ""),

          new XElement(CXML.NODE_DESCRIPTION, Description),

          new XElement(CXML.NODE_FACETS,
            GetCXMLFacets()
          )
        );
    }

    public abstract IEnumerable<XElement> GetCXMLFacetCategories();
    public abstract IEnumerable<XElement> GetCXMLFacets();

    public virtual void Clear()
    {
      Title = "";
      Image = "";
      Url = null;
      Description = "";
    }

    public abstract ICXMLMetadata Load(string key, XDocument doc);

    public virtual ICXMLMetadata Load(string key, XmlReader cxml, XmlReader cxmlFallback)
    {
      XDocument doc = null;
      try
      {
        doc = XDocument.Load(cxml);
      }
      catch
      {
        try
        {
          doc = XDocument.Load(cxmlFallback);
        }
        catch
        {
          //NOP
        }
      }

      return Load(key, doc); //Load should return default metadata when doc==null
    }
    
    public virtual void Save(XmlWriter cxml)
    {
      Save(cxml, Title, GetCXMLFacetCategories(), new ICXMLMetadata[] { this });
    }

   #endregion
    
    #region --- Helpers ---

    public static void AddNonNullToList(IList<XElement> list, XElement item)
    {
      if (item != null)
        list.Add(item);
    }

    public static ICXMLMetadata ReplaceId(ICXMLMetadata metadata, string replaceId)
    {
      metadata.Id = replaceId;
      return metadata;
    }

    public static void Save(XmlWriter cxml, string collectionTitle, IEnumerable<XElement> facetCategories, ICXMLMetadata[] metadataItems)
    {
      int i = 0;
      CXML.MakeCollection(
        collectionTitle,
        facetCategories,
        metadataItems.Select(m => ReplaceId(m, i++.ToString()).GetCXMLItem())
      )
      .Save(cxml);
    }

    #endregion

  }

}