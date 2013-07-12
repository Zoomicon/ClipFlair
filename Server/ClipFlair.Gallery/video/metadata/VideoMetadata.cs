//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: VideoMetadata.cs
//Version: 20130711

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public class VideoMetadata : IVideoMetadata
  {
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

    public static IVideoMetadata LoadMetadata(string key, string cxmlFilename, string cxmlFallbackFilename)
    {
      IVideoMetadata metadata = new VideoMetadata() { Title = "", Image = "", Url = null };

      IEnumerable<XElement> facets = null;
      try
      {
        XDocument doc;
        try
        {
          doc = XDocument.Load(cxmlFilename);
        }
        catch
        {
          doc = XDocument.Load(cxmlFallbackFilename);
        }

        XElement item = doc.Root.Elements(CXML.NODE_ITEMS).Elements(CXML.NODE_ITEM).Where(i => CXML.GetFacetValue(i.Elements(CXML.NODE_FACETS).Elements(CXML.NODE_FACET), VideoMetadataFacets.FACET_FILENAME) == key).First();

        metadata.Title = item.Attribute(CXML.ATTRIB_NAME).ToString();
        metadata.Image = (string)item.Attribute(CXML.ATTRIB_IMG);
        metadata.Url = new Uri(item.Attribute(CXML.ATTRIB_HREF).ToString());

        metadata.Description = item.Element(CXML.NODE_DESCRIPTION).Value;

        facets = item.Elements(CXML.NODE_FACETS).Elements(CXML.NODE_FACET);
      }
      catch
      {
        //NOP
      }

      metadata.Filename = CXML.GetFacetValue(facets, VideoMetadataFacets.FACET_FILENAME);
      metadata.AudioLanguage = CXML.GetFacetValues(facets, VideoMetadataFacets.FACET_AUDIO_LANGUAGE);
      metadata.CaptionsLanguage = CXML.GetFacetValues(facets, VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE);
      metadata.Genre = CXML.GetFacetValues(facets, VideoMetadataFacets.FACET_GENRE);
      metadata.AgeRestricted = CXML.GetFacetBoolValue(facets, VideoMetadataFacets.FACET_AGE_RESTRICTED);
      metadata.Duration = CXML.GetFacetValue(facets, VideoMetadataFacets.FACET_DURATION);
      metadata.AudiovisualRichness = CXML.GetFacetValues(facets, VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS);
      metadata.PedagogicalAdaptability = CXML.GetFacetBoolValue(facets, VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY);
      metadata.AuthorSource = CXML.GetFacetValue(facets, VideoMetadataFacets.FACET_AUTHOR_SOURCE);
      metadata.License = CXML.GetFacetValue(facets, VideoMetadataFacets.FACET_LICENSE);

      return metadata;
    }

    public static void SaveMetadata(IVideoMetadata metadata, string filename)
    {
      Directory.CreateDirectory(Path.GetDirectoryName(filename)); //create any parent directories needed

      new XElement(CXML.meta + CXML.NODE_COLLECTION,
        new XAttribute("SchemaVersion", "1.0"),
        new XAttribute("Name", "ClipFlair video clip"),
        new XAttribute(XNamespace.Xmlns + "xsi", CXML.xsi),
        new XAttribute(XNamespace.Xmlns + "xsd", CXML.xsd),
        new XAttribute(XNamespace.Xmlns + "p", CXML.p),

        new XElement(CXML.NODE_FACET_CATEGORIES,
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, isFilterVisible:false, isMetadataVisible:false, isWordWheelVisible:false),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUDIO_LANGUAGE, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_GENRE, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AGE_RESTRICTED, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_DURATION, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUTHOR_SOURCE, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(VideoMetadataFacets.FACET_LICENSE, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true)
        ),
       
        new XElement(CXML.NODE_ITEMS,
          new XElement(CXML.NODE_ITEM,
            new XAttribute(CXML.ATTRIB_NAME, metadata.Title),
            new XAttribute(CXML.ATTRIB_IMG, metadata.Image),
            new XAttribute(CXML.ATTRIB_HREF, (metadata.Url != null) ? metadata.Url.ToString() : ""),

            new XElement(CXML.NODE_DESCRIPTION, metadata.Description),

            new XElement(CXML.NODE_FACETS,
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_FILENAME, metadata.Filename),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUDIO_LANGUAGE, metadata.AudioLanguage),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE, metadata.CaptionsLanguage),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_GENRE, metadata.Genre),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_AGE_RESTRICTED, metadata.AgeRestricted.ToString()), //this will give True/False (not Yes/No)
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_DURATION, metadata.Duration),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS, metadata.AudiovisualRichness),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY, metadata.PedagogicalAdaptability.ToString()), //this will give True/False (not Yes/No)
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUTHOR_SOURCE, metadata.AuthorSource),
              CXML.MakeStringFacet(VideoMetadataFacets.FACET_LICENSE, metadata.License)
            )
          )
        )
      ).Save(filename);
    }

  }

}