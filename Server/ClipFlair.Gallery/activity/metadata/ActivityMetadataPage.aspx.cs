//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityMetadataPage.aspx.cs
//Version: 20130806

using System;
using System.Linq;
using System.IO;
using System.Web;

namespace ClipFlair.Gallery
{
  public partial class ActivityMetadataPage : BaseMetadataPage
  {

    private string path = HttpContext.Current.Server.MapPath("~/activity");

    protected void Page_Load(object sender, EventArgs e)
    {
      _listItems = listItems;
      
      if (!IsPostBack)
      {
        listItems.DataSource = 
          Directory.GetFiles(path, "*.clipflair")
            .Select(f => new { Filename = Path.GetFileName(f) });
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

    #region Load

    public override void DisplayMetadata(string key)
    {
      DisplayMetadata(key, (IActivityMetadata)new ActivityMetadata().Load(key, GetMetadataFilepath(key), GetFallbackMetadataFilePath()));
    }

    public void DisplayMetadata(string key, IActivityMetadata metadata)
    {
      UI.LoadTextBox(txtTitle, metadata.Title);
      UI.LoadHyperlink(linkUrl, new Uri("http://studio.clipflair.net/?activity=" + key));
      UI.LoadTextBox(txtDescription, metadata.Description);

      UI.LoadTextBox(txtKeywords, metadata.Keywords);
      UI.LoadTextBox(txtLicense, metadata.License);

      UI.LoadCheckBoxList(clistForLearners, metadata.ForLearners);
      UI.LoadCheckBoxList(clistForSpeakers, metadata.ForSpeakers);
      UI.LoadCheckBoxList(clistLanguageCombination, metadata.LanguageCombination);
      UI.LoadCheckBoxList(clistLevel, metadata.Level);
      UI.LoadTextBox(txtEstimatedTime, metadata.EstimatedTimeMinutes);
      UI.LoadTextBox(txtAuthors, metadata.Authors);
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

    public override IMetadata ExtractMetadata(string key)
    {
      IActivityMetadata metadata = new ActivityMetadata();

      metadata.Title = txtTitle.Text;
      metadata.Image = "../activity/image/" + key + ".png";
      metadata.Url = new Uri("http://studio.clipflair.net/?activity=" + key);
      metadata.Description = txtDescription.Text;
      metadata.Filename = key;
      metadata.Keywords = UI.GetCommaSeparated(txtKeywords);
      metadata.License = txtLicense.Text;
      
      metadata.ForLearners = UI.GetSelected(clistForLearners);
      metadata.ForSpeakers = UI.GetSelected(clistForSpeakers);
      metadata.LanguageCombination = UI.GetSelected(clistLanguageCombination);
      metadata.Level = UI.GetSelected(clistLevel);
      metadata.EstimatedTimeMinutes = txtEstimatedTime.Text;
      metadata.Authors = UI.GetCommaSeparated(txtAuthors);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
      DoSave();
    }

    public override void Merge()
    {
      Merge("ClipFlair Gallery Activities", ActivityMetadata.MakeCXMLFacetCategories());
    }

    #endregion
     
  }
}