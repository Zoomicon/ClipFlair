//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: list.aspx.cs
//Version: 20130711

using System;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace ClipFlair.Gallery
{
  public partial class ActivityMetadataPage : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Request.QueryString["Generate"] != null)
          GenerateAll();

        UpdateSelection();
      }
    }

    protected void GenerateAll()
    {
      foreach (ListItem l in listItems.Items)
      {
        string value = l.Text;
        if (!File.Exists(GetMetadataFilepath(value))) //to not lose newer data, only saving file from old data if it doesn't already exist
        {
          UpdateSelection(value); //must pass value here
          SaveMetadata(value); //must pass value here
        }
      }

    }

    protected void UpdateSelection()
    {
      UpdateSelection(listItems.SelectedValue);
    }

    protected void listItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateSelection();
    }

    protected string GetMetadataFilepath(string value)
    {
      return HttpContext.Current.Server.MapPath("~/activity/metadata/" + value + ".cxml");
    }

    protected string GetFallbackMetadataFilePath()
    {
      return HttpContext.Current.Server.MapPath("~/activity/metadata/old_activities.cxml");
    }

    public void UpdateSelection(string value)
    {
      DisplayMetadata(value);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      SaveMetadata();
    }

    #region Load

    public void DisplayMetadata(string key)
    {
      IActivityMetadata metadata = ActivityMetadata.LoadMetadata(key, GetMetadataFilepath(key), GetFallbackMetadataFilePath());
      DisplayMetadata(key, metadata);
    }

    public void DisplayMetadata(string key, IActivityMetadata metadata)
    {
      UI.LoadTextBox(txtTitle, metadata.Title);
      UI.LoadTextBox(txtUrl, (metadata.Url != null) ? metadata.Url.ToString() : "http://studio.clipflair.net/?activity=" + key);
      UI.LoadTextBox(txtDescription, metadata.Description);
      UI.LoadCheckBoxList(clistForLearners, metadata.ForLearners);
      UI.LoadCheckBoxList(clistForSpeakers, metadata.ForSpeakers);
      UI.LoadCheckBoxList(clistLanguageCombination, metadata.LanguageCombination);
      UI.LoadCheckBoxList(clistLevel, metadata.Level);
      UI.LoadTextBox(txtKeywords, metadata.Keywords);
      UI.LoadTextBox(txtEstimatedTime, metadata.EstimatedTimeMinutes);
      UI.LoadTextBox(txtAuthors, metadata.Authors);
      UI.LoadTextBox(txtLicense, metadata.License);
      UI.LoadCheckBoxList(clistFromSkills, metadata.FromSkills);
      UI.LoadCheckBoxList(clistToSkills, metadata.ToSkills);
      UI.LoadCheckBoxList(clistAVSkills, metadata.AVSkills);
      UI.LoadCheckBoxList(clistResponses, metadata.Responses);
      UI.LoadCheckBoxList(clistTasksRevoicing, metadata.TasksRevoicing);
      UI.LoadCheckBoxList(clistTasksCaptioning, metadata.TasksCaptioning);
      UI.LoadCheckBoxList(clistLearnerType, metadata.LearnerType);
      UI.LoadCheckBoxList(clistAgeGroup, metadata.AgeGroup);
      UI.LoadTextBox(txtFeedbackModeToLearner, metadata.FeedbackModeToLearner);
    }

    #endregion

    #region Save

    protected IActivityMetadata ExtractMetadata(string key)
    {
      IActivityMetadata metadata = new ActivityMetadata();

      metadata.Title = txtTitle.Text;
      metadata.Image = "../activity/image/" + key + ".png";
      metadata.Url = new Uri(txtUrl.Text);
      metadata.Description = txtDescription.Text;
      metadata.Filename = key;
      metadata.ForLearners = UI.GetSelected(clistForLearners);
      metadata.ForSpeakers = UI.GetSelected(clistForSpeakers);
      metadata.LanguageCombination = UI.GetSelected(clistLanguageCombination);
      metadata.Level = UI.GetSelected(clistLevel);
      metadata.Keywords = UI.GetCommaSeparated(txtKeywords);
      metadata.EstimatedTimeMinutes = txtEstimatedTime.Text;
      metadata.Authors = UI.GetCommaSeparated(txtAuthors);
      metadata.License = txtLicense.Text;
      metadata.FromSkills = UI.GetSelected(clistFromSkills);
      metadata.ToSkills = UI.GetSelected(clistToSkills);
      metadata.AVSkills = UI.GetSelected(clistAVSkills);
      metadata.Responses = UI.GetSelected(clistResponses);
      metadata.TasksRevoicing = UI.GetSelected(clistTasksRevoicing);
      metadata.TasksCaptioning = UI.GetSelected(clistTasksCaptioning);
      metadata.LearnerType = UI.GetSelected(clistLearnerType);
      metadata.AgeGroup = UI.GetSelected(clistAgeGroup);
      metadata.FeedbackModeToLearner = UI.GetCommaSeparated(txtFeedbackModeToLearner);
      return metadata;
    }

    protected void SaveMetadata()
    {
      SaveMetadata(listItems.SelectedValue);
    }

    protected void SaveMetadata(string key)
    {
      ActivityMetadata.SaveMetadata(ExtractMetadata(key), GetMetadataFilepath(key));
    }

    #endregion
     
  }
}