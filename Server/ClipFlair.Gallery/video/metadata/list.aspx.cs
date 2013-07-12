//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: list.aspx.cs
//Version: 20130711

using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace ClipFlair.Gallery
{
  public partial class VideoMetadataPage : System.Web.UI.Page
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
      return HttpContext.Current.Server.MapPath("~/video/metadata/" + value + ".cxml");
    }

    protected string GetFallbackMetadataFilePath()
    {
      return HttpContext.Current.Server.MapPath("~/video/metadata/old_video.cxml");
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
      IVideoMetadata metadata = VideoMetadata.LoadMetadata(key, GetMetadataFilepath(key), GetFallbackMetadataFilePath());
      DisplayMetadata(key, metadata);
    }

    public void DisplayMetadata(string key, IVideoMetadata metadata)
    {
      UI.LoadTextBox(txtTitle, metadata.Title);
      UI.LoadTextBox(txtUrl, (metadata.Url != null)? metadata.Url.ToString() : "http://studio.clipflair.net/?video=" + key);
      UI.LoadTextBox(txtDescription, metadata.Description);
      UI.LoadCheckBoxList(clistAudioLanguage, metadata.AudioLanguage);
      UI.LoadCheckBoxList(clistCaptionsLanguage, metadata.CaptionsLanguage);
      UI.LoadCheckBoxList(clistGenre, metadata.Genre);
      UI.LoadCheckBox(cbAgeRestricted, metadata.AgeRestricted);
      UI.LoadTextBox(txtDuration, metadata.Duration);
      UI.LoadCheckBoxList(clistAudiovisualRichness, metadata.AudiovisualRichness);
      UI.LoadCheckBox(cbPedagogicalAdaptability, metadata.PedagogicalAdaptability);
      UI.LoadTextBox(txtAuthorSource, metadata.AuthorSource);
      UI.LoadTextBox(txtLicense, metadata.License);
    }

    #endregion

    #region Save

    protected IVideoMetadata ExtractMetadata(string key)
    {
      IVideoMetadata metadata = new VideoMetadata();

      metadata.Title = txtTitle.Text;
      metadata.Image = "../video/" + key + "/" + key + "_thumb.jpg";
      metadata.Url = new Uri(txtUrl.Text);
      metadata.Description = txtDescription.Text;
      metadata.Filename = key;
      metadata.AudioLanguage = UI.GetSelected(clistAudioLanguage);
      metadata.CaptionsLanguage = UI.GetSelected(clistCaptionsLanguage);
      metadata.Genre = UI.GetSelected(clistGenre);
      metadata.AgeRestricted = cbAgeRestricted.Checked;
      metadata.Duration = txtDuration.Text;
      metadata.AudiovisualRichness = UI.GetSelected(clistAudiovisualRichness);
      metadata.PedagogicalAdaptability = cbPedagogicalAdaptability.Checked;
      metadata.AuthorSource = txtAuthorSource.Text;
      metadata.License = txtLicense.Text;
      return metadata;
    }

    protected void SaveMetadata()
    {
      SaveMetadata(listItems.SelectedValue);
    }

    protected void SaveMetadata(string key)
    {
      VideoMetadata.SaveMetadata(ExtractMetadata(key), GetMetadataFilepath(key));
    }

    #endregion

  }

}