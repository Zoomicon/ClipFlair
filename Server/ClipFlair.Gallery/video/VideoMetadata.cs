//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: VideoMetadtata.cs
//Version: 20130719

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public class VideoMetadata : BaseMetadata, IVideoMetadata
  {

    #region --- Properties ---

    public string Title { get; set; }
    public string Image { get; set; }
    public Uri Url { get; set; }
    public string Description { get; set; }
    public string Filename { get; set; }
    public string[] AudioLanguage { get; set; }
    public string[] CaptionsLanguage { get; set; }
    public string[] Genre { get; set; }
    public bool AgeRestricted { get; set; }
    public string Duration { get; set; }
    public string[] AudiovisualRichness { get; set; }
    public bool PedagogicalAdaptability { get; set; }
    public string AuthorSource { get; set; }
    public string License { get; set; }

    #endregion

    #region --- Methods ---

    public override void Clear()
    {
      Title = "";
      Image = "";
      Url = null;

      Description = "";

      Filename = "";
      AudioLanguage = new string[] { };
      CaptionsLanguage = new string[] { };
      Genre = new string[] { };
      AgeRestricted = false;
      Duration = "0:0:0";
      AudiovisualRichness = new string[] { };
      PedagogicalAdaptability = false;
      AuthorSource = "";
      License = "";
    }

    public override IMetadata Load(string key, XDocument doc)
    {
      IEnumerable<XElement> facets = null;
      try
      {
        XElement item = doc.Root.Elements(CXML.NODE_ITEMS).Elements(CXML.NODE_ITEM).CXMLFirstItemWithStringValue(VideoMetadataFacets.FACET_FILENAME, key);

        Title = item.Attribute(CXML.ATTRIB_NAME).Value;
        Image = item.Attribute(CXML.ATTRIB_IMG).Value;
        Url = new Uri(item.Attribute(CXML.ATTRIB_HREF).Value);

        Description = item.Element(CXML.NODE_DESCRIPTION).Value;

        facets = item.Elements(CXML.NODE_FACETS).Elements(CXML.NODE_FACET);

        Filename = facets.CXMLFacetStringValue(VideoMetadataFacets.FACET_FILENAME);
        AudioLanguage = facets.CXMLFacetStringValues(VideoMetadataFacets.FACET_AUDIO_LANGUAGE);
        CaptionsLanguage = facets.CXMLFacetStringValues(VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE);
        Genre = facets.CXMLFacetStringValues(VideoMetadataFacets.FACET_GENRE);
        AgeRestricted = facets.CXMLFacetBoolValue(VideoMetadataFacets.FACET_AGE_RESTRICTED);
        Duration = facets.CXMLFacetStringValue(VideoMetadataFacets.FACET_DURATION);
        AudiovisualRichness = facets.CXMLFacetStringValues(VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS);
        PedagogicalAdaptability = facets.CXMLFacetBoolValue(VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY);
        AuthorSource = facets.CXMLFacetStringValue(VideoMetadataFacets.FACET_AUTHOR_SOURCE);
        License = facets.CXMLFacetStringValue(VideoMetadataFacets.FACET_LICENSE);
      }
      catch
      {
        Clear();
      }

      return this;
    }

    public override void Save(string filename)
    {
      Directory.CreateDirectory(Path.GetDirectoryName(filename)); //create any parent directories needed

      new XElement(CXML.NODE_COLLECTION,
        new XAttribute("SchemaVersion", "1.0"),
        new XAttribute("Name", "ClipFlair video clip"),
        new XAttribute(XNamespace.Xmlns + "xsi", CXML.xsi),
        new XAttribute(XNamespace.Xmlns + "xsd", CXML.xsd),
        new XAttribute(XNamespace.Xmlns + "p", CXML.p),

        new XElement(CXML.NODE_FACET_CATEGORIES,
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, isFilterVisible: false, isMetadataVisible: false, isWordWheelVisible: false),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUDIO_LANGUAGE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_GENRE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AGE_RESTRICTED, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_DURATION, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUTHOR_SOURCE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_LICENSE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true)
        ),

        new XElement(CXML.NODE_ITEMS,
          new XElement(CXML.NODE_ITEM,
            new XAttribute(CXML.ATTRIB_NAME, Title),
            new XAttribute(CXML.ATTRIB_IMG, Image),
            new XAttribute(CXML.ATTRIB_HREF, (Url != null) ? Url.ToString() : ""),

            new XElement(CXML.NODE_DESCRIPTION, Description),

            new XElement(CXML.NODE_FACETS,
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_FILENAME, Filename),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUDIO_LANGUAGE, AudioLanguage),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE, CaptionsLanguage),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_GENRE, Genre),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_AGE_RESTRICTED, AgeRestricted.ToString()), //this will give True/False (not Yes/No)
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_DURATION, Duration),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS, AudiovisualRichness),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY, PedagogicalAdaptability.ToString()), //this will give True/False (not Yes/No)
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUTHOR_SOURCE, AuthorSource),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_LICENSE, License)
            )
          )
        )
      ).Save(filename);
    }

    #endregion

  }

}