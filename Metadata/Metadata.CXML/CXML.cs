//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CXML.cs
//Version: 20130720

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Metadata.CXML
{
  public static class CXML
  {

    #region --- Constants ---

    public static readonly XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
    public static readonly XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
    public static readonly XNamespace p = XNamespace.Get("http://schemas.microsoft.com/livelabs/pivot/collection/2009");
    public static readonly XNamespace meta = XNamespace.Get("http://schemas.microsoft.com/collection/metadata/2009");

    public static readonly XName NODE_COLLECTION = CXML.meta + "Collection";

    public static readonly XName NODE_FACET_CATEGORIES = CXML.meta + "FacetCategories";
    public static readonly XName NODE_FACET_CATEGORY = CXML.meta + "FacetCategory";
    
    public static readonly XName NODE_EXTENSION = "Extension";
    public static readonly XName NODE_SORT_ORDER = CXML.p + "SortOrder";
    public static readonly XName NODE_SORT_VALUE = CXML.p + "SortValue";

    public const string NODE_ITEMS = "Items"; //TODO: can't load if we use "CXML.meta +" prefix
    public static readonly XName NODE_ITEM = "Item";

    public static readonly XName NODE_DESCRIPTION = "Description";

    public static readonly XName NODE_FACETS = "Facets";
    public static readonly XName NODE_FACET = "Facet";

    public static readonly XName NODE_STRING = "String";

    public static readonly XName ATTRIB_ID = "Id";
    public static readonly XName ATTRIB_NAME = "Name";
    public static readonly XName ATTRIB_IMG = "Img";
    public static readonly XName ATTRIB_HREF = "Href";

    public static readonly XName ATTRIB_TYPE = "Type";
    public static readonly XName ATTRIB_IS_FILTER_VISIBLE = CXML.p + "IsFilterVisible";
    public static readonly XName ATTRIB_IS_METADATA_VISIBLE = CXML.p + "IsMetadataVisible";
    public static readonly XName ATTRIB_IS_WORD_WHEEL_VISIBLE = CXML.p + "IsWordWheelVisible";

    public static readonly XName ATTRIB_VALUE = "Value";

    public const string VALUE_STRING = "String";
    public const string VALUE_TRUE = "True";
    public const string VALUE_FALSE = "False";

    #endregion

    #region --- Query ---

    public static IEnumerable<XElement> CXMLItemsWithStringValue(this IEnumerable<XElement> items, string facetName, string facetValue)
    {
      return items.Where(i => i.Elements(CXML.NODE_FACETS).Elements(CXML.NODE_FACET).CXMLFacetStringValue(facetName) == facetValue);
    }

    public static XElement CXMLFirstItemWithStringValue(this IEnumerable<XElement> items, string facetName, string facetValue)
    {
      return items.CXMLItemsWithStringValue(facetName, facetValue).First();
    }

    public static XElement CXMLFacet(this IEnumerable<XElement> facets, string facetName)
    {
      try
      {
        return facets.Where(f => f.Attribute("Name").Value == facetName).First();
      }
      catch
      {
        return null;
      }
    }

    public static string CXMLFacetStringValue(this XElement facet)
    {
      return (facet != null)? facet.Element(NODE_STRING).Attribute(ATTRIB_VALUE).Value : "";
    }

    public static string CXMLFacetStringValue(this IEnumerable<XElement> facets, string facetName)
    {
      return facets.CXMLFacet(facetName).CXMLFacetStringValue();
    }

    public static bool CXMLFacetBoolValue(this XElement facet)
    {
      string value =  facet.CXMLFacetStringValue();
      return (value.Equals("True", StringComparison.OrdinalIgnoreCase) ||
              value.Equals("Yes", StringComparison.OrdinalIgnoreCase));
    }

    public static bool CXMLFacetBoolValue(this IEnumerable<XElement> facets, string facetName)
    {
      return facets.CXMLFacet(facetName).CXMLFacetBoolValue();
    }

    public static string[] CXMLFacetStringValues(this XElement facet)
    {
      return (facet != null)? facet.Elements("String").Select(s => s.Attribute("Value").Value).ToArray() : new string[]{};
    }

    public static string[] CXMLFacetStringValues(this IEnumerable<XElement> facets, string facetName)
    {
      return facets.CXMLFacet(facetName).CXMLFacetStringValues();
    }

    #endregion

    #region --- Make ---

    public static XElement MakeFacetCategory(string name, string type, bool isFilterVisible, bool isMetadataVisible, bool isWordWheelVisible)
    {
      return
        new XElement(CXML.NODE_FACET_CATEGORY,
          new XAttribute(CXML.ATTRIB_NAME, name),
          new XAttribute(CXML.ATTRIB_TYPE, type),
          new XAttribute(CXML.ATTRIB_IS_FILTER_VISIBLE, isFilterVisible.ToString()),
          new XAttribute(CXML.ATTRIB_IS_METADATA_VISIBLE, isMetadataVisible.ToString()),
          new XAttribute(CXML.ATTRIB_IS_WORD_WHEEL_VISIBLE, isWordWheelVisible.ToString())
        );
    }

    public static XElement MakeStringFacet(string name, string value)
    {
      if (value == null || string.IsNullOrWhiteSpace(value)) return null; //not saving empty values (note that tools like PAuthor don't support empty facets, so returning null node)

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
      if (values == null || values.Length == 0) return null; //tools like PAuthor don't support empty facets, so returning null node

      return
        new XElement(NODE_FACET,
          new XAttribute(ATTRIB_NAME, name),
          (from v in values select
            new XElement(NODE_STRING,
              new XAttribute(ATTRIB_VALUE, v))
          )
        );
    }

    public static XElement MakeCollection(string collectionTitle, IEnumerable<XElement> cxmlFacetCategories, IEnumerable<XElement> cxmlItems){
      return
      new XElement(CXML.NODE_COLLECTION,
        new XAttribute("SchemaVersion", "1.0"),
        new XAttribute("Name", collectionTitle),
        new XAttribute(XNamespace.Xmlns + "xsi", CXML.xsi),
        new XAttribute(XNamespace.Xmlns + "xsd", CXML.xsd),
        new XAttribute(XNamespace.Xmlns + "p", CXML.p),

        new XElement(CXML.NODE_FACET_CATEGORIES,
          cxmlFacetCategories
        ),

        new XElement(CXML.NODE_ITEMS,
          cxmlItems
        )
      );
    }

    #endregion

  }
  
}