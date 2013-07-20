//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityMetadata.cs
//Version: 20130720

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public class ActivityMetadata : BaseMetadata, IActivityMetadata
  {

    #region --- Properties ---

    public string[] ForLearners { get; set; }
    public string[] ForSpeakers { get; set; }
    public string[] LanguageCombination { get; set; }
    public string[] Level { get; set; }
    public string[] Keywords { get; set; }
    public string EstimatedTimeMinutes { get; set; }
    public string[] Authors { get; set; }
    public string License { get; set; }
    public string[] FromSkills { get; set; }
    public string[] ToSkills { get; set; }
    public string[] AVSkills { get; set; }
    public string[] Responses { get; set; }
    public string[] TasksRevoicing { get; set; }
    public string[] TasksCaptioning { get; set; }
    public string[] LearnerType { get; set; }
    public string[] AgeGroup { get; set; }
    public string[] FeedbackModeToLearner { get; set; }

    #endregion

    #region --- Methods ---

    public override void Clear()
    {
      Title = "";
      Image = "";
      Url = null;

      Description = "";

      Filename = "";
      ForLearners = new string[] { };
      ForSpeakers = new string[] { };
      LanguageCombination = new string[] { };
      Level = new string[] { };
      Keywords = new string[] { };
      EstimatedTimeMinutes = "";
      Authors = new string[] { };
      License = "CC BY-SA 3.0"; //suggested ClipFlair Activity license
      FromSkills = new string[] { };
      ToSkills = new string[] { };
      AVSkills = new string[] { };
      Responses = new string[] { };
      TasksRevoicing = new string[] { };
      TasksCaptioning = new string[] { };
      LearnerType = new string[] { };
      AgeGroup = new string[] { };
      FeedbackModeToLearner = new string[] { };
    }

    public override IMetadata Load(string key, XDocument doc)
    {
      IEnumerable<XElement> facets = null;
      try
      {
        XElement item = doc.Root.Elements(CXML.NODE_ITEMS).Elements(CXML.NODE_ITEM).CXMLFirstItemWithStringValue(ActivityMetadataFacets.FACET_FILENAME, key);

        Id = item.Attribute(CXML.ATTRIB_ID).Value;
        Title = item.Attribute(CXML.ATTRIB_NAME).Value;
        Image = item.Attribute(CXML.ATTRIB_IMG).Value;
        Url = new Uri(item.Attribute(CXML.ATTRIB_HREF).Value);

        Description = item.Element(CXML.NODE_DESCRIPTION).Value;

        facets = item.Elements(CXML.NODE_FACETS).Elements(CXML.NODE_FACET);

        Filename = facets.CXMLFacetStringValue(ActivityMetadataFacets.FACET_FILENAME);
        ForLearners = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FOR_LEARNERS);
        ForSpeakers = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FOR_SPEAKERS);
        LanguageCombination = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION);
        Level = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LEVEL);
        Keywords = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_KEYWORDS);
        EstimatedTimeMinutes = facets.CXMLFacetStringValue(ActivityMetadataFacets.FACET_ESTIMATED_TIME);
        Authors = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_AUTHORS);
        License = facets.CXMLFacetStringValue(ActivityMetadataFacets.FACET_LICENSE);
        FromSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FROM_SKILLS);
        ToSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TO_SKILLS);
        AVSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_AV_SKILLS);
        Responses = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_RESPONSES);
        TasksRevoicing = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TASKS_REVOICING);
        TasksCaptioning = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TASKS_CAPTIONING);
        LearnerType = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LEARNER_TYPE);
        AgeGroup = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_AGE_GROUP);
        FeedbackModeToLearner = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER);
      }
      catch
      {
        Clear();
      }

      return this;
    }

    public static IEnumerable<XElement> MakeCXMLFacetCategories()
    {
      IList<XElement> result = new List<XElement>();
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, isFilterVisible: false, isMetadataVisible: false, isWordWheelVisible: false));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FOR_LEARNERS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FOR_SPEAKERS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LEVEL, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_KEYWORDS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_ESTIMATED_TIME, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_AUTHORS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LICENSE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FROM_SKILLS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TO_SKILLS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_AV_SKILLS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_RESPONSES, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TASKS_REVOICING, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TASKS_CAPTIONING, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LEARNER_TYPE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(MakeAgeGroupFacetCategory());
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      return result;
    }

    public static XElement MakeAgeGroupFacetCategory()
    {
      return
        new XElement(CXML.NODE_FACET_CATEGORY,
          new XAttribute(CXML.ATTRIB_NAME, ActivityMetadataFacets.FACET_AGE_GROUP),
          new XAttribute(CXML.ATTRIB_TYPE, "String"),
          new XAttribute(CXML.ATTRIB_IS_FILTER_VISIBLE, "True"),
          new XAttribute(CXML.ATTRIB_IS_METADATA_VISIBLE, "True"),
          new XAttribute(CXML.ATTRIB_IS_WORD_WHEEL_VISIBLE, "True"),

          new XElement(CXML.NODE_EXTENSION,
            new XElement(CXML.NODE_SORT_ORDER,
              new XAttribute(CXML.ATTRIB_NAME, ActivityMetadataFacets.FACET_AGE_GROUP),

              new XElement(CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "All ages")),
              new XElement(CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "< 13 years old")),
              new XElement(CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "13 - 18 years old")),
              new XElement(CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "18 - 35 years old")),
              new XElement(CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "> 35 years old"))
            )
          )
        );
    }

    public override IEnumerable<XElement> GetCXMLFacets()
    {
      IList<XElement> facets = new List<XElement>();

      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FILENAME, Filename));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FOR_LEARNERS, ForLearners));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FOR_SPEAKERS, ForSpeakers));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION, LanguageCombination));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LEVEL, Level));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_KEYWORDS, Keywords));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_ESTIMATED_TIME, EstimatedTimeMinutes));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AUTHORS, Authors));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LICENSE, License));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FROM_SKILLS, FromSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TO_SKILLS, ToSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AV_SKILLS, AVSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_RESPONSES, Responses));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TASKS_REVOICING, TasksRevoicing));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TASKS_CAPTIONING, TasksCaptioning));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LEARNER_TYPE, LearnerType));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AGE_GROUP, AgeGroup));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER, FeedbackModeToLearner));

      return facets;
    }

    public override void Save(string cxmlFilename)
    {
      Save(cxmlFilename, "Collection for activity " + Filename, MakeCXMLFacetCategories(), new IMetadata[] { this });
    }

    #endregion

  }

}