//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: VideoMetadata.cs
//Version: 20130918

using Metadata.CXML;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ClipFlair.Metadata
{

  public class VideoMetadata : ClipFlairMetadata, IVideoMetadata
  {

    #region --- Properties ---

    public string[] AudioLanguage { get; set; }
    public string[] CaptionsLanguage { get; set; }
    public string[] Genre { get; set; }
    public bool AgeRestricted { get; set; }
    public string Duration { get; set; }
    public string[] AudiovisualRichness { get; set; }
    public bool PedagogicalAdaptability { get; set; }
    public string AuthorSource { get; set; }

    #endregion

    #region --- Methods ---

    public override void Clear()
    {
      base.Clear();

      AudioLanguage = new string[] { };
      CaptionsLanguage = new string[] { };
      Genre = new string[] { };
      AgeRestricted = false;
      Duration = "0:0:0";
      AudiovisualRichness = new string[] { };
      PedagogicalAdaptability = false;
      AuthorSource = "";
    }

    public override ICXMLMetadata Load(XElement item)
    {
      base.Load(item);

      IEnumerable<XElement> facets = FindFacets(item);

      AudioLanguage = facets.CXMLFacetStringValues(VideoMetadataFacets.FACET_AUDIO_LANGUAGE);
      CaptionsLanguage = facets.CXMLFacetStringValues(VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE);
      Genre = facets.CXMLFacetStringValues(VideoMetadataFacets.FACET_GENRE);
      AgeRestricted = facets.CXMLFacetBoolValue(VideoMetadataFacets.FACET_AGE_RESTRICTED);
      Duration = facets.CXMLFacetStringValue(VideoMetadataFacets.FACET_DURATION);
      AudiovisualRichness = facets.CXMLFacetStringValues(VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS);
      PedagogicalAdaptability = facets.CXMLFacetBoolValue(VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY);
      AuthorSource = facets.CXMLFacetStringValue(VideoMetadataFacets.FACET_AUTHOR_SOURCE);

      return this;
    }

    public override IEnumerable<XElement> GetCXMLFacetCategories() //the following also defines the order in which facets appear in PivotViewer's filters pane
    {
      return MakeVideoFacetCategories();
    }

    public override IEnumerable<XElement> GetCXMLFacets() //the following also defines the order in which facet values appear in PivotViewer's details pane
    {
      IList<XElement> facets = new List<XElement>();

      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_FILENAME, Filename));

      AddNonNullToList(facets, CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUDIO_LANGUAGE, AudioLanguage));
      AddNonNullToList(facets, CXML.MakeStringFacet(VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE, CaptionsLanguage));
      AddNonNullToList(facets, CXML.MakeStringFacet(VideoMetadataFacets.FACET_GENRE, Genre));
      AddNonNullToList(facets, CXML.MakeStringFacet(VideoMetadataFacets.FACET_AGE_RESTRICTED, AgeRestricted.ToString())); //this will give True/False (not Yes/No)
      AddNonNullToList(facets, CXML.MakeStringFacet(VideoMetadataFacets.FACET_DURATION, Duration));
      AddNonNullToList(facets, CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS, AudiovisualRichness));
      AddNonNullToList(facets, CXML.MakeStringFacet(VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY, PedagogicalAdaptability.ToString())); //this will give True/False (not Yes/No)
      AddNonNullToList(facets, CXML.MakeStringFacet(VideoMetadataFacets.FACET_AUTHOR_SOURCE, AuthorSource));

      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_KEYWORDS, Keywords));
      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_LICENSE, License));

      return facets;
    }

    #endregion

    #region --- Helpers ---

    public static IEnumerable<XElement> MakeVideoFacetCategories() //the following also defines the order in which filters appear in PivotViewer's filter pane
    {
      IList<XElement> result = new List<XElement>();
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, isFilterVisible: false, isMetadataVisible: false, isWordWheelVisible: false));

      result.Add(CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUDIO_LANGUAGE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(VideoMetadataFacets.FACET_CAPTIONS_LANGUAGE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(VideoMetadataFacets.FACET_GENRE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AGE_RESTRICTED, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(VideoMetadataFacets.FACET_DURATION, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUDIOVISUAL_RICHNESS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(VideoMetadataFacets.FACET_PEDAGOGICAL_ADAPTABILITY, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(VideoMetadataFacets.FACET_AUTHOR_SOURCE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));

      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_KEYWORDS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_LICENSE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));

      return result;
    }

    #endregion

  }

}