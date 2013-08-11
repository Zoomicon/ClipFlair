//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: VideoMetadataPage.aspx.cs
//Version: 20130806

using System;
using System.IO;
using System.Linq;
using System.Web;

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
      UI.LoadHyperlink(linkUrl, new Uri("http://studio.clipflair.net/?video=" + key));
      UI.LoadTextBox(txtDescription, metadata.Description);

      UI.LoadTextBox(txtKeywords, metadata.Keywords);
      UI.LoadTextBox(txtLicense, metadata.License);

      UI.LoadCheckBoxList(clistAudioLanguage, metadata.AudioLanguage);
      UI.LoadCheckBoxList(clistCaptionsLanguage, metadata.CaptionsLanguage);
      UI.LoadCheckBoxList(clistGenre, metadata.Genre);
      UI.LoadCheckBox(cbAgeRestricted, metadata.AgeRestricted);
      UI.LoadTextBox(txtDuration, metadata.Duration);
      UI.LoadCheckBoxList(clistAudiovisualRichness, metadata.AudiovisualRichness);
      UI.LoadCheckBox(cbPedagogicalAdaptability, metadata.PedagogicalAdaptability);
      UI.LoadTextBox(txtAuthorSource, metadata.AuthorSource);
    }

    #endregion

    #region --- Save ---

    public override IMetadata ExtractMetadata(string key)
    {
      IVideoMetadata metadata = new VideoMetadata();

      metadata.Title = txtTitle.Text;
      metadata.Image = "../video/" + key + "/" + key + "_thumb.jpg";
      metadata.Url = new Uri("http://studio.clipflair.net/?video=" + key);
      metadata.Description = txtDescription.Text;
      metadata.Filename = key;
      metadata.Keywords = UI.GetCommaSeparated(txtKeywords);
      metadata.License = txtLicense.Text;

      metadata.AudioLanguage = UI.GetSelected(clistAudioLanguage);
      metadata.CaptionsLanguage = UI.GetSelected(clistCaptionsLanguage);
      metadata.Genre = UI.GetSelected(clistGenre);
      metadata.AgeRestricted = cbAgeRestricted.Checked;
      metadata.Duration = txtDuration.Text;
      metadata.AudiovisualRichness = UI.GetSelected(clistAudiovisualRichness);
      metadata.PedagogicalAdaptability = cbPedagogicalAdaptability.Checked;
      metadata.AuthorSource = txtAuthorSource.Text;

      return metadata;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      DoSave();
    }

    public override void Merge()
    {
      Merge("ClipFlair Gallery Clips", VideoMetadata.MakeCXMLFacetCategories());
    }

    #endregion

  }

}