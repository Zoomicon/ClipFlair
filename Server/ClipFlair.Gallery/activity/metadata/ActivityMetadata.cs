//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityMetadata.cs
//Version: 20130711

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public class ActivityMetadata : IActivityMetadata
  {
    public string Title { get; set; }
    public string Image { get; set; }
    public Uri Url { get; set; }
    public string Description { get; set; }
    public string Filename { get; set; }
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

    public static IActivityMetadata LoadMetadata(string key, string cxmlFilename, string cxmlFallbackFilename)
    {
      IActivityMetadata metadata = new ActivityMetadata() { Title = "", Image = "", Url = null };

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

        XElement item = doc.Root.Elements(CXML.NODE_ITEMS).Elements(CXML.NODE_ITEM).Where(i => CXML.GetFacetValue(i.Elements(CXML.NODE_FACETS).Elements(CXML.NODE_FACET), ActivityMetadataFacets.FACET_FILENAME) == key).First();

        metadata.Title = (string)item.Attribute(CXML.ATTRIB_NAME);
        metadata.Image = (string)item.Attribute(CXML.ATTRIB_IMG);
        metadata.Url = new Uri((string)item.Attribute(CXML.ATTRIB_HREF));

        metadata.Description = item.Element(CXML.NODE_DESCRIPTION).Value;

        facets = item.Elements(CXML.NODE_FACETS).Elements(CXML.NODE_FACET);
      }
      catch
      {
        //NOP
      }

      metadata.Filename = CXML.GetFacetValue(facets, ActivityMetadataFacets.FACET_FILENAME);
      metadata.ForLearners = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_FOR_LEARNERS);
      metadata.ForSpeakers = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_FOR_SPEAKERS);
      metadata.LanguageCombination = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION);
      metadata.Level = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_LEVEL);
      metadata.Keywords = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_KEYWORDS);
      metadata.EstimatedTimeMinutes = CXML.GetFacetValue(facets, ActivityMetadataFacets.FACET_ESTIMATED_TIME);
      metadata.Authors = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_AUTHORS);
      metadata.License = CXML.GetFacetValue(facets, ActivityMetadataFacets.FACET_LICENSE);
      metadata.FromSkills = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_FROM_SKILLS);
      metadata.ToSkills = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_TO_SKILLS);
      metadata.AVSkills = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_AV_SKILLS);
      metadata.Responses = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_RESPONSES);
      metadata.TasksRevoicing = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_TASKS_REVOICING);
      metadata.TasksCaptioning = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_TASKS_CAPTIONING);
      metadata.LearnerType = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_LEARNER_TYPE);
      metadata.AgeGroup = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_AGE_GROUP);
      metadata.FeedbackModeToLearner = CXML.GetFacetValues(facets, ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER);

      return metadata;
    }

    public static void SaveMetadata(IActivityMetadata metadata, string filename)
    {
      Directory.CreateDirectory(Path.GetDirectoryName(filename)); //create any parent directories needed

      new XElement(CXML.meta + CXML.NODE_COLLECTION,
        new XAttribute("SchemaVersion", "1.0"),
        new XAttribute("Name", "ClipFlair Activity clip"),
        new XAttribute(XNamespace.Xmlns + "xsi", CXML.xsi),
        new XAttribute(XNamespace.Xmlns + "xsd", CXML.xsd),
        new XAttribute(XNamespace.Xmlns + "p", CXML.p),

        new XElement(CXML.NODE_FACET_CATEGORIES,
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FILENAME, CXML.VALUE_STRING, isFilterVisible:false, isMetadataVisible:false, isWordWheelVisible:false),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FOR_LEARNERS, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FOR_SPEAKERS, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LEVEL, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_KEYWORDS, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_ESTIMATED_TIME, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_AUTHORS, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LICENSE, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FROM_SKILLS, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TO_SKILLS, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_AV_SKILLS, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_RESPONSES, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TASKS_REVOICING, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_TASKS_CAPTIONING, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_LEARNER_TYPE, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true),
          MakeAgeGroupFacetCategory(),
          CXML.MakeFacetCategory(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER, CXML.VALUE_STRING, isFilterVisible:true, isMetadataVisible:true, isWordWheelVisible:true)
        ),
       
        new XElement(CXML.NODE_ITEMS,
          new XElement(CXML.NODE_ITEM,
            new XAttribute(CXML.ATTRIB_NAME, metadata.Title),
            new XAttribute(CXML.ATTRIB_IMG, metadata.Image),
            new XAttribute(CXML.ATTRIB_HREF, (metadata.Url != null) ? metadata.Url.ToString() : ""),

            new XElement(CXML.NODE_DESCRIPTION, metadata.Description),

            new XElement(CXML.NODE_FACETS,
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FILENAME, metadata.Filename),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FOR_LEARNERS, metadata.ForLearners),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FOR_SPEAKERS, metadata.ForSpeakers),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LANGUAGE_COMBINATION, metadata.LanguageCombination),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LEVEL, metadata.Level),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_KEYWORDS, metadata.Keywords),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_ESTIMATED_TIME, metadata.EstimatedTimeMinutes),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AUTHORS, metadata.Authors),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LICENSE, metadata.License),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FROM_SKILLS, metadata.FromSkills),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TO_SKILLS, metadata.ToSkills),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AV_SKILLS, metadata.AVSkills),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_RESPONSES, metadata.Responses),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TASKS_REVOICING, metadata.TasksRevoicing),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_TASKS_CAPTIONING, metadata.TasksCaptioning),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_LEARNER_TYPE, metadata.LearnerType),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_AGE_GROUP, metadata.AgeGroup),
              CXML.MakeStringFacet(ActivityMetadataFacets.FACET_FEEDBACK_MODE_TO_LEARNER, metadata.FeedbackModeToLearner)
            )
          )
        )
      ).Save(filename);
    }

    public static XElement MakeAgeGroupFacetCategory()
    {
      return
        new XElement(CXML.NODE_FACET_CATEGORY,
          new XAttribute(CXML.ATTRIB_NAME, ActivityMetadataFacets.FACET_AGE_GROUP),
          new XAttribute(CXML.ATTRIB_TYPE, "String"),
          new XAttribute(CXML.p + CXML.ATTRIB_IS_FILTER_VISIBLE, "True"),
          new XAttribute(CXML.p + CXML.ATTRIB_IS_METADATA_VISIBLE, "True"),
          new XAttribute(CXML.p + CXML.ATTRIB_IS_WORD_WHEEL_VISIBLE, "True"),

          new XElement(CXML.NODE_EXTENSION,
            new XElement(CXML.p + CXML.NODE_SORT_ORDER,
              new XAttribute(CXML.ATTRIB_NAME, ActivityMetadataFacets.FACET_AGE_GROUP),
              
              new XElement(CXML.p + CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "All ages")),
              new XElement(CXML.p + CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "&lt; 13 years old")),
              new XElement(CXML.p + CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "13 - 18 years old")),
              new XElement(CXML.p + CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "18 - 35 years old")),
              new XElement(CXML.p + CXML.NODE_SORT_VALUE,
                new XAttribute(CXML.ATTRIB_VALUE, "&gt; 35 years old"))
            )
          )
        );
    }

  }

}