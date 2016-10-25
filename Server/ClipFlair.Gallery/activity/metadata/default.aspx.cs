﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: default.aspx.cs
//Version: 20160430

using Metadata.CXML;
using ClipFlair.Metadata;

using System;
using System.Linq;
using System.IO;
using System.Web;
using System.Xml;

namespace ClipFlair.Gallery
{
  public partial class ActivityMetadataPage : BaseMetadataPage
  {

    private string path = HttpContext.Current.Server.MapPath("~/activity");

    protected void Page_Load(object sender, EventArgs e)
    {
      _listItems = listItems; //allow the ancestor class to access our listItems UI object
      
      if (!IsPostBack)
      {
        var itemPleaseSelect = new[] { new { Filename = "* Please select..." } };

        var items = Directory.EnumerateFiles(path, "*.clipflair") //Available in .NET4, more efficient than GetFiles
                             .Select(f => new { Filename = Path.GetFileName(f) });

        listItems.DataSource = itemPleaseSelect.Concat(items);

        listItems.DataBind(); //must call this

        if (Request.QueryString["item"] != null)
          listItems.SelectedValue = Request.QueryString["item"]; //must do after listItems.DataBind
      }
    }

    protected void listItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateSelection();
    }

    #region --- Paths ---

    public override string GetMetadataFilepath(string value)
    {
      return Path.Combine(path, "metadata/" + value + ".cxml");
    }

    public override string GetFallbackMetadataFilePath()
    {
      return Path.Combine(path, "metadata/old_activities.cxml");
    }

    public override string GetMergeMetadataFilePath()
    {
      return Path.Combine(path, "activities.cxml");
    }

    #endregion

    #region UI

    public override void ShowMetadataUI(bool visible)
    {
      if (uiMetadata != null)
        uiMetadata.Visible = visible;

      linkUrl.Visible = visible;
    }

    #endregion

    #region Load

    public override void DisplayMetadata(string key)
    {
      using (XmlReader cxmlFallback = CreateXmlReader(GetFallbackMetadataFilePath()))
        using (XmlReader cxml = CreateXmlReader(GetMetadataFilepath(key)))
          DisplayMetadata(key, (IActivityMetadata)new ActivityMetadata().Load(key, cxml, cxmlFallback));
    }

    public void DisplayMetadata(string key, IActivityMetadata metadata)
    {
      UI.Load(txtTitle, metadata.Title);
      UI.Load(linkUrl, new Uri("http://studio.clipflair.net/?activity=" + key));
      UI.Load(txtDescription, metadata.Description);

      //no need to show metadata.Filename since we calculate and show the URL, plus the filename is used as the key and shown at the dropdown list
      UI.Load(lblFirstPublished, metadata.FirstPublished.ToString(CXML.DEFAULT_DATETIME_FORMAT));
      UI.Load(lblLastUpdated, metadata.LastUpdated.ToString(CXML.DEFAULT_DATETIME_FORMAT));

      UI.Load(clistForLearners, metadata.ForLearners);
      UI.Load(clistForSpeakers, metadata.ForSpeakers);
      UI.Load(clistLanguageCombination, metadata.LanguageCombination);
      UI.Load(clistLevel, metadata.Level);
      UI.Load(txtEstimatedTime, metadata.EstimatedTimeMinutes);
      UI.Load(clistFromSkills, metadata.FromSkills);
      UI.Load(clistToSkills, metadata.ToSkills);
      UI.Load(clistAVSkills, metadata.AVSkills);
      UI.Load(clistResponses, metadata.Responses);
      UI.Load(clistTasksRevoicing, metadata.TasksRevoicing);
      UI.Load(clistTasksCaptioning, metadata.TasksCaptioning);
      UI.Load(clistLearnerType, metadata.LearnerType);
      UI.Load(txtFeedbackModeToLearner, metadata.FeedbackModeToLearner);

      UI.Load(clistAgeGroup, metadata.AgeGroup);
      UI.Load(txtKeywords, metadata.Keywords);
      UI.Load(txtAuthorSource, metadata.AuthorSource);
      UI.Load(txtLicense, metadata.License);
    }

    #endregion

    #region Save

    public override ICXMLMetadata ExtractMetadata(string key)
    {
      IActivityMetadata metadata = new ActivityMetadata();

      metadata.Title = txtTitle.Text;
      metadata.Image = "../activity/image/" + key + ".png";
      metadata.Url = new Uri("http://studio.clipflair.net/?activity=" + key);
      metadata.Description = txtDescription.Text;

      metadata.Filename = key;
      string filePath = Path.Combine(path, key);
      metadata.FirstPublished = File.GetCreationTimeUtc(filePath);
      metadata.LastUpdated = File.GetLastWriteTimeUtc(filePath);

      metadata.AgeGroup = UI.GetSelected(clistAgeGroup);
      metadata.Keywords = UI.GetCommaSeparated(txtKeywords);
      metadata.AuthorSource = UI.GetCommaSeparated(txtAuthorSource);
      metadata.License = txtLicense.Text;
      
      metadata.ForLearners = UI.GetSelected(clistForLearners);
      metadata.ForSpeakers = UI.GetSelected(clistForSpeakers);
      metadata.LanguageCombination = UI.GetSelected(clistLanguageCombination);
      metadata.Level = UI.GetSelected(clistLevel);
      metadata.EstimatedTimeMinutes = txtEstimatedTime.Text;
      metadata.FromSkills = UI.GetSelected(clistFromSkills);
      metadata.ToSkills = UI.GetSelected(clistToSkills);
      metadata.AVSkills = UI.GetSelected(clistAVSkills);
      metadata.Responses = UI.GetSelected(clistResponses);
      metadata.TasksRevoicing = UI.GetSelected(clistTasksRevoicing);
      metadata.TasksCaptioning = UI.GetSelected(clistTasksCaptioning);
      metadata.LearnerType = UI.GetSelected(clistLearnerType);
      metadata.FeedbackModeToLearner = UI.GetCommaSeparated(txtFeedbackModeToLearner);

      return metadata;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      DoSave();
    }

    public override void Merge()
    {
      Merge("ClipFlair Gallery Activities", ActivityMetadata.MakeActivityFacetCategories());
    }

    #endregion
     
  }
}