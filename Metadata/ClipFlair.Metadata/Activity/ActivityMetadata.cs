//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityMetadata.cs
//Version: 20131101

using Metadata.CXML;

using System.Collections.Generic;
using System.Xml.Linq;

namespace ClipFlair.Metadata
{

  public class ActivityMetadata : ClipFlairMetadata, IActivityMetadata
  {

    #region --- Properties ---

    public string[] ForLearners { get; set; }
    public string[] ForSpeakers { get; set; }
    public string[] LanguageCombination { get; set; }
    public string[] Level { get; set; }
    public string EstimatedTimeMinutes { get; set; }
    public string[] FromSkills { get; set; }
    public string[] ToSkills { get; set; }
    public string[] AVSkills { get; set; }
    public string[] Responses { get; set; }
    public string[] TasksRevoicing { get; set; }
    public string[] TasksCaptioning { get; set; }
    public string[] LearnerType { get; set; }
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
      License = "CC BY-SA 3.0"; //suggested ClipFlair Activity license
      FromSkills = new string[] { };
      ToSkills = new string[] { };
      AVSkills = new string[] { };
      Responses = new string[] { };
      TasksRevoicing = new string[] { };
      TasksCaptioning = new string[] { };
      LearnerType = new string[] { };
      FeedbackModeToLearner = new string[] { };
    }

    public override ICXMLMetadata Load(XElement item)
    {
      base.Load(item);

      IEnumerable<XElement> facets = FindFacets(item);

      ForLearners = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FOR_LEARNERS);
      ForSpeakers = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FOR_SPEAKERS);
      LanguageCombination = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION);
      Level = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LEVEL);
      EstimatedTimeMinutes = facets.CXMLFacetStringValue(ActivityMetadataFacets.FACET_ESTIMATED_TIME);
      FromSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FROM_SKILLS);
      ToSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TO_SKILLS);
      AVSkills = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_AV_SKILLS);
      Responses = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_RESPONSES);
      TasksRevoicing = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TASKS_REVOICING);
      TasksCaptioning = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_TASKS_CAPTIONING);
      LearnerType = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_LEARNER_TYPE);
      FeedbackModeToLearner = facets.CXMLFacetStringValues(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER);

      return this;
    }

    public override IEnumerable<XElement> GetCXMLFacetCategories() //the following also defines the order in which facets appear in PivotViewer's filters pane
    {
      return MakeActivityFacetCategories();
    }

    public override IEnumerable<XElement> GetCXMLFacets()
    {
      IList<XElement> facets = new List<XElement>();

      AddNonNullToList(facets, CXML.MakeStringFacet(ClipFlairMetadataFacets.FACET_FILENAME, Filename));

      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FOR_LEARNERS, ForLearners));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FOR_SPEAKERS, ForSpeakers));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION, LanguageCombination));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LEVEL, Level));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_ESTIMATED_TIME, EstimatedTimeMinutes));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FROM_SKILLS, FromSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TO_SKILLS, ToSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AV_SKILLS, AVSkills));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_RESPONSES, Responses));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TASKS_REVOICING, TasksRevoicing));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TASKS_CAPTIONING, TasksCaptioning));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LEARNER_TYPE, LearnerType));
      AddNonNullToList(facets, CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER, FeedbackModeToLearner));

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

    public static IEnumerable<XElement> MakeActivityFacetCategories() //the following also defines the order in which facets appear in PivotViewer's filters and details panes
    {
      IList<XElement> result = new List<XElement>();
      result.Add(CXML.MakeFacetCategory(ClipFlairMetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, null, isFilterVisible: false, isMetadataVisible: false, isWordWheelVisible: false));

      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FOR_LEARNERS, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FOR_SPEAKERS, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LEVEL, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_ESTIMATED_TIME, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FROM_SKILLS, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TO_SKILLS, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_AV_SKILLS, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_RESPONSES, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TASKS_REVOICING, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TASKS_CAPTIONING, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LEARNER_TYPE, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));
      result.Add(CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER, CXML.VALUE_STRING, null, isFilterVisible: true, isMetadataVisible: true, isWordWheelVisible: true));

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