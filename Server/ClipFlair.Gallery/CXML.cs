//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CXML.cs
//Version: 20130711

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{
  public static class CXML
  {

    #region --- Constants ---

    public const string NODE_COLLECTION = "Collection";

    public const string NODE_FACET_CATEGORIES = "FacetCategories";
    public const string NODE_FACET_CATEGORY = "FacetCategory";
    
    public const string NODE_EXTENSION = "Extension";
    public const string NODE_SORT_ORDER = "SortOrder";
    public const string NODE_SORT_VALUE = "SortValue";

    public const string NODE_ITEMS = "Items";
    public const string NODE_ITEM = "Item";

    public const string NODE_DESCRIPTION = "Description";
    
    public const string NODE_FACETS = "Facets";
    public const string NODE_FACET = "Facet";

    public const string NODE_STRING = "String";

    public const string ATTRIB_NAME = "Name";
    public const string ATTRIB_IMG = "Img";
    public const string ATTRIB_HREF = "Href";
    
    public const string ATTRIB_TYPE = "Type";
    public const string ATTRIB_IS_FILTER_VISIBLE = "IsFilterVisible";
    public const string ATTRIB_IS_METADATA_VISIBLE = "IsMetadataVisible";
    public const string ATTRIB_IS_WORD_WHEEL_VISIBLE = "IsWordWheelVisible";
    
    public const string ATTRIB_VALUE = "Value";

    public const string VALUE_STRING = "String";
    public const string VALUE_TRUE = "True";
    public const string VALUE_FALSE = "False";

    public static readonly XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
    public static readonly XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
    public static readonly XNamespace p = XNamespace.Get("http://schemas.microsoft.com/livelabs/pivot/collection/2009");
    public static readonly XNamespace meta = XNamespace.Get("http://schemas.microsoft.com/collection/metadata/2009");

    #endregion

    public static XElement GetFacet(IEnumerable<XElement> facets, string facetName)
    {
      try
      {
        return facets.Where(f => (string)f.Attribute("Name") == facetName).First();
      }
      catch
      {
        return null;
      }
    }

    public static string GetFacetValue(XElement facet)
    {
      return (facet != null)? facet.Element(NODE_STRING).Attribute(ATTRIB_VALUE).Value : "";
    }

    public static string GetFacetValue(IEnumerable<XElement> facets, string facetName)
    {
      return GetFacetValue(GetFacet(facets, facetName));
    }

    public static bool GetFacetBoolValue(XElement facet)
    {
      string value =  GetFacetValue(facet);
      return (value.Equals("True", StringComparison.OrdinalIgnoreCase) ||
              value.Equals("Yes", StringComparison.OrdinalIgnoreCase));
    }

    public static bool GetFacetBoolValue(IEnumerable<XElement> facets, string facetName)
    {
      return GetFacetBoolValue(GetFacet(facets, facetName));
    }

    public static string[] GetFacetValues(XElement facet)
    {
      return (facet != null)? facet.Elements("String").Select(s => s.Attribute("Value").Value).ToArray() : new string[]{};
    }

    public static string[] GetFacetValues(IEnumerable<XElement> facets, string facetName)
    {
      return GetFacetValues(GetFacet(facets, facetName));
    }

    public static XElement MakeFacetCategory(string name, string type, bool isFilterVisible, bool isMetadataVisible, bool isWordWheelVisible)
    {
      return
        new XElement(CXML.NODE_FACET_CATEGORY,
          new XAttribute(CXML.ATTRIB_NAME, name),
          new XAttribute(CXML.ATTRIB_TYPE, type),
          new XAttribute(p + CXML.ATTRIB_IS_FILTER_VISIBLE, isFilterVisible.ToString()),
          new XAttribute(p + CXML.ATTRIB_IS_METADATA_VISIBLE, isMetadataVisible.ToString()),
          new XAttribute(p + CXML.ATTRIB_IS_WORD_WHEEL_VISIBLE, isWordWheelVisible.ToString())
        );
    }

    public static XElement MakeStringFacet(string name, string value)
    {
       return
         new XElement(NODE_FACET,
           new XAttribute(ATTRIB_NAME, name),
           new XElement(NODE_STRING, 
             new XAttribute(ATTRIB_VALUE, value)
           )
         );
    }

    public static XElement MakeStringFacet(string name, string[] values)
    {
      return
        new XElement(NODE_FACET,
          new XAttribute(ATTRIB_NAME, name),
          (from v in values select
            new XElement(NODE_STRING,
              new XAttribute(ATTRIB_VALUE, v))
          )
        );
    }

  }
  
}