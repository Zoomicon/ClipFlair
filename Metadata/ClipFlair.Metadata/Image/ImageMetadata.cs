//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageMetadata.cs
//Version: 20131009

using Metadata.CXML;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ClipFlair.Metadata
{

  public class ImageMetadata : ClipFlairMetadata, IImageMetadata
  {

    #region --- Properties ---

    public string[] CaptionsLanguage { get; set; }
    //public string[] Genre { get; set; }
    //public bool AgeRestricted { get; set; }
    public string AuthorSource { get; set; }

    #endregion

    #region --- Methods ---

    public override void Clear()
    {
      base.Clear();

      CaptionsLanguage = new string[] { };
      //Genre = new string[] { };
      //AgeRestricted = false;
      AuthorSource = "";
    }

    public override ICXMLMetadata Load(XElement item)
    {
      base.Load(item);

      IEnumerable<XElement> facets = FindFacets(item);

      CaptionsLanguage = facets.CXMLFacetStringValues(ImageMetadataFacets.FACET_CAPTIONS_LANGUAGE);
      //Genre = facets.CXMLFacetStringValues(ImageMetadataFacets.FACET_GENRE);
      //AgeRestricted = facets.CXMLFacetBoolValue(ImageMetadataFacets.FACET_AGE_RESTRICTED);
      AuthorSource = facets.CXMLFacetStringValue(ImageMetadataFacets.FACET_AUTHOR_SOURCE);

      return this;
    }

    public override IEnumerable<XElement> GetCXMLFacetCategories() //the following also defines the order in which facets appear in PivotViewer's filters pane
    {
      return MakeImageFacetCategories();
    }

    public override IEnumerable<XElement> GetCXMLFacets() //the following also defines the order in which facet values appear in PivotViewer's details pane
    {
      IList<XElement> facets = new List<XElement>();

      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_FILENAME, Filename));

      AddNonNullToList(facets, CXML.MakeStringFacet(ImageMetadataFacets.FACET_CAPTIONS_LANGUAGE, CaptionsLanguage));
      //AddNonNullToList(facets, CXML.MakeStringFacet(ImageMetadataFacets.FACET_GENRE, Genre));
      //AddNonNullToList(facets, CXML.MakeStringFacet(ImageMetadataFacets.FACET_AGE_RESTRICTED, AgeRestricted.ToString())); //this will give True/False (not Yes/No)
      AddNonNullToList(facets, CXML.MakeStringFacet(ImageMetadataFacets.FACET_AUTHOR_SOURCE, AuthorSource));

      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_KEYWORDS, Keywords));
      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_LICENSE, License));

      return facets;
    }

    #endregion

    #region --- Helpers ---

    public static IEnumerable<XElement> MakeImageFacetCategories() //the following also defines the order in which filters appear in PivotViewer's filter pane
    {
      IList<XElement> result = new List<XElement>();
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, isFilterVisible: false, isMetadataVisible: false, isWordWheelVisible: false));

      result.Add(CXML.MakeFacetCategory(ImageMetadataFacets.FACET_CAPTIONS_LANGUAGE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      //result.Add(CXML.MakeFacetCategory(ImageMetadataFacets.FACET_GENRE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      //result.Add(CXML.MakeFacetCategory(ImageMetadataFacets.FACET_AGE_RESTRICTED, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ImageMetadataFacets.FACET_AUTHOR_SOURCE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));

      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_KEYWORDS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_LICENSE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));

      return result;
    }

    #endregion

  }

}