//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseMetadtata.cs
//Version: 20130720

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public abstract class BaseMetadata : IMetadata
  {

    #region --- Properties ---

    public string Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public Uri Url { get; set; }
    public string Description { get; set; }
    public string Filename { get; set; }

    #endregion

    public IMetadata Load(string key, string cxmlFilename, string cxmlFallbackFilename)
    {
      XDocument doc = null;
      try
      {
        doc = XDocument.Load(cxmlFilename);
      }
      catch
      {
        try
        {
          doc = XDocument.Load(cxmlFallbackFilename);
        }
        catch
        {
          //NOP
        }
      }

      return FixMetadata(Load(key, doc)); //Load should return default metadata when doc==null
    }

    public static IMetadata FixMetadata(IMetadata metadata)
    {
      if (string.IsNullOrWhiteSpace(metadata.Title))
        metadata.Title = metadata.Filename;

      return metadata;
    }

    public static IMetadata ReplaceId(IMetadata metadata, string replaceId)
    {
      metadata.Id = replaceId;
      return metadata;
    }

    public static void Save(string cxmlFilename, string collectionTitle, IEnumerable<XElement> facetCategories, IMetadata[] metadataItems)
    {
      Directory.CreateDirectory(Path.GetDirectoryName(cxmlFilename)); //create any parent directories needed

      int i = 0;
      CXML.MakeCollection(
        collectionTitle,
        facetCategories,
        metadataItems.Select(m => ReplaceId(m, i++.ToString()).GetCXMLItem())
      )
      .Save(cxmlFilename);
    }

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
    
    public abstract void Clear();
    public abstract IMetadata Load(string key, XDocument doc);
    public abstract IEnumerable<XElement> GetCXMLFacets();
    public abstract void Save(string cxmlFilename);

    protected void AddNonNullToList(IList<XElement> list, XElement item)
    {
      if (item != null)
        list.Add(item);
    }

  }

}