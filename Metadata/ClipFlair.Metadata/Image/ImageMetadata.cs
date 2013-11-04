//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageMetadata.cs
//Version: 20131101

using Metadata.CXML;

using System.Collections.Generic;
using System.Xml.Linq;

namespace ClipFlair.Metadata
{

  public class ImageMetadata : ClipFlairMetadata, IImageMetadata
  {

    #region --- Properties ---

    public string[] CaptionsLanguage { get; set; }
    //public string[] Genre { get; set; }

    #endregion

    #region --- Methods ---

    public override void Clear()
    {
      base.Clear();

      CaptionsLanguage = new string[] { };
      //Genre = new string[] { };
      //AgeRestricted = false;
      AuthorSource = new string[] {};
    }

    public override ICXMLMetadata Load(XElement item)
    {
      base.Load(item);

      IEnumerable<XElement> facets = FindFacets(item);

      CaptionsLanguage = facets.CXMLFacetStringValues(ImageMetadataFacets.FACET_CAPTIONS_LANGUAGE);
      //Genre = facets.CXMLFacetStringValues(ImageMetadataFacets.FACET_GENRE);
 
      return this;
    }

    public override IEnumerable<XElement> GetCXMLFacetCategories() //the following also defines the order in which facets appear in PivotViewer's filters pane
    {
      return MakeImageFacetCategories();
    }

    public override IEnumerable<XElement> GetCXMLFacets()
    {
      IList<XElement> facets = new List<XElement>();

      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_FILENAME, Filename));

      AddNonNullToList(facets, CXML.MakeStringFacet(ImageMetadataFacets.FACET_CAPTIONS_LANGUAGE, CaptionsLanguage));
      //AddNonNullToList(facets, CXML.MakeStringFacet(ImageMetadataFacets.FACET_GENRE, Genre));

      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_AGE_GROUP, AgeGroup));
      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_KEYWORDS, Keywords));
      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_AUTHORS_SOURCE, AuthorSource));
      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_LICENSE, License));

      AddNonNullToList(facets, CXML.MakeDateTimeFacet(ClipFlairMetadataFacets.FACET_FIRST_PUBLISHED, FirstPublished));
      AddNonNullToList(facets, CXML.MakeDateTimeFacet(ClipFlairMetadataFacets.FACET_LAST_UPDATED, LastUpdated));

      return facets;
    }

    #endregion

    #region --- Helpers ---

    public static IEnumerable<XElement> MakeImageFacetCategories() //the following also defines the order in which filters appear in PivotViewer's filter pane
    {
      IList<XElement> result = new List<XElement>();
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, null, isFilterVisible: false, isMetadataVisible: false, isWordWheelVisible: false));

      result.Add(CXML.MakeFacetCategory(ImageMetadataFacets.FACET_CAPTIONS_LANGUAGE, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      //result.Add(CXML.MakeFacetCategory(ImageMetadataFacets.FACET_GENRE, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));

      result.Add(MakeAgeGroupFacetCategory());
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_KEYWORDS, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_AUTHORS_SOURCE, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_LICENSE, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));

      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_FIRST_PUBLISHED, CXML.VALUE_DATETIME, CXML.DEFAULT_DATETIME_FORMAT, isFilterVisible: false, isMetadataVisible: true, isWordWheelVisible: false));
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_LAST_UPDATED, CXML.VALUE_DATETIME, CXML.DEFAULT_DATETIME_FORMAT, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: false));

      return result;
    }

    #endregion

  }

}