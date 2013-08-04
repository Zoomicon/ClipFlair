//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityMetadata.cs
//Version: 20130726

using System;
using System.Collections.Generic;
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
    public string EstimatedTimeMinutes { get; set; }
    public string[] Authors { get; set; }
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
      base.Clear();

      ForLearners = new string[] { };
      ForSpeakers = new string[] { };
      LanguageCombination = new string[] { };
      Level = new string[] { };
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
        XElement item = doc.Root.Elements(CXML.NODE_ITEMS).Elements(CXML.NODE_ITEM).CXMLFirstItemWithStringValue(MetadataFacets.FACET_FILENAME, key);

        Id = item.Attribute(CXML.ATTRIB_ID).Value;
        Title = item.Attribute(CXML.ATTRIB_NAME).Value;
        Image = item.Attribute(CXML.ATTRIB_IMG).Value;
        Url = new Uri(item.Attribute(CXML.ATTRIB_HREF).Value);

        Description = item.Element(CXML.NODE_DESCRIPTION).Value;

        facets = item.Elements(CXML.NODE_FACETS).Elements(CXML.NODE_FACET);

        Filename = facets.CXMLFacetStringValue(MetadataFacets.FACET_FILENAME);
        
        ForLearners = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FOR_LEARNERS);
        ForSpeakers = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FOR_SPEAKERS);
        LanguageCombination = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION);
        Level = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LEVEL);
        EstimatedTimeMinutes = facets.CXMLFacetStringValue(ActivityMetadataFacets.FACET_ESTIMATED_TIME);
        Authors = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_AUTHORS);
        FromSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FROM_SKILLS);
        ToSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TO_SKILLS);
        AVSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_AV_SKILLS);
        Responses = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_RESPONSES);
        TasksRevoicing = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TASKS_REVOICING);
        TasksCaptioning = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TASKS_CAPTIONING);
        LearnerType = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LEARNER_TYPE);
        AgeGroup = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_AGE_GROUP);
        FeedbackModeToLearner = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER);

        Keywords = facets.CXMLFacetStringValues(MetadataFacets.FACET_KEYWORDS);
        License = facets.CXMLFacetStringValue(MetadataFacets.FACET_LICENSE);
      }
      catch
      {
        Clear();
      }

      return this;
    }

    public static IEnumerable<XElement> MakeCXMLFacetCategories() //the following also defines the order in which filters appear in PivotViewer's filter pane
    {
      IList<XElement> result = new List<XElement>();
      result.Add(CXML.MakeFacetCategory(MetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, isFilterVisible: false, isMetadataVisible: false, isWordWheelVisible: false));

      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FOR_LEARNERS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FOR_SPEAKERS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LEVEL, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_ESTIMATED_TIME, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_AUTHORS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FROM_SKILLS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TO_SKILLS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_AV_SKILLS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_RESPONSES, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TASKS_REVOICING, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TASKS_CAPTIONING, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LEARNER_TYPE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(MakeAgeGroupFacetCategory());
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));

      result.Add(CXML.MakeFacetCategory(MetadataFacets.FACET_KEYWORDS, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(MetadataFacets.FACET_LICENSE, CXML.VALUE_STRING, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      
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

    public override IEnumerable<XElement> GetCXMLFacets() //the following also defines the order in which facet values appear in PivotViewer's details pane
    {
      IList<XElement> facets = new List<XElement>();

      AddNonNullToList(facets, CXML.MakeStringFacet(MetadataFacets.FACET_FILENAME, Filename));

      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FOR_LEARNERS, ForLearners));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FOR_SPEAKERS, ForSpeakers));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION, LanguageCombination));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LEVEL, Level));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_ESTIMATED_TIME, EstimatedTimeMinutes));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AUTHORS, Authors));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FROM_SKILLS, FromSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TO_SKILLS, ToSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AV_SKILLS, AVSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_RESPONSES, Responses));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TASKS_REVOICING, TasksRevoicing));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TASKS_CAPTIONING, TasksCaptioning));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LEARNER_TYPE, LearnerType));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AGE_GROUP, AgeGroup));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER, FeedbackModeToLearner));

      AddNonNullToList(facets, CXML.MakeStringFacet(MetadataFacets.FACET_KEYWORDS, Keywords));
      AddNonNullToList(facets, CXML.MakeStringFacet(MetadataFacets.FACET_LICENSE, License));

      return facets;
    }

    public override void Save(string cxmlFilename)
    {
      Save(cxmlFilename, "Collection for activity " + Filename, MakeCXMLFacetCategories(), new IMetadata[] { this });
    }

    #endregion

  }

}