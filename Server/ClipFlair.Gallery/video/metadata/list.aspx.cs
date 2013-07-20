//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: list.aspx.cs
//Version: 20130720

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ClipFlair.Gallery
{
  public partial class VideoMetadataPage : BaseMetadataPage
  {

    private string path = HttpContext.Current.Server.MapPath("~/video");

    protected void Page_Load(object sender, EventArgs e)
    {
      _listItems = listItems; 
      
      if (!IsPostBack)
      {
        listItems.DataSource =
          Directory.GetDirectories(path)
            .Where(f => (Directory.GetFiles(f, Path.GetFileName(f) + ".ism").Length != 0))
            .Select(f => new { Foldername = Path.GetFileName(f) });
        //when having a full path to a directory don't use Path.GetDirectoryName (gives parent directory),
        //use Path.GetFileName instead to extract the name of the directory

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
      return Path.Combine(path, "metadata/old_video.cxml");
    }

    public override string GetMergeMetadataFilePath()
    {
      return Path.Combine(path, "video.cxml");
    }

    #endregion

    #region --- Load ---

    public override void DisplayMetadata(string key)
    {
      DisplayMetadata(key, (IVideoMetadata)new VideoMetadata().Load(key, GetMetadataFilepath(key), GetFallbackMetadataFilePath()));
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

    #region --- Save ---

    public override IMetadata ExtractMetadata(string key)
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
      SaveMetadata();
    }

    public override void Merge()
    {
      Merge("ClipFlair Gallery Clips", VideoMetadata.MakeCXMLFacetCategories());
    }

    #endregion

  }

}