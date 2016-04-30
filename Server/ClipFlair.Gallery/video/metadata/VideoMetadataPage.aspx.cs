//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: VideoMetadataPage.aspx.cs
//Version: 20160430

using ClipFlair.Metadata;
using Metadata.CXML;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace ClipFlair.Gallery
{
  public partial class VideoMetadataPage : BaseMetadataPage
  {

    private string path = HttpContext.Current.Server.MapPath("~/video");

    protected void Page_Load(object sender, EventArgs e)
    {
      _listItems = listItems; //allow the ancestor class to access our listItems UI object 
      
      if (!IsPostBack)
      {
        var itemPleaseSelect = new[] { new { Foldername = "* Please select..." } };

        var items = Directory.GetDirectories(path)
            .Where(f => (Directory.EnumerateFiles(f, Path.GetFileName(f) + ".ism").Count() != 0)) //Available in .NET4, more efficient than GetFiles
            .Select(f => new { Foldername = Path.GetFileName(f) });
        //when having a full path to a directory don't use Path.GetDirectoryName (gives parent directory),
        //use Path.GetFileName instead to extract the name of the directory

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
      return Path.Combine(path, "metadata/old_video.cxml");
    }

    public override string GetMergeMetadataFilePath()
    {
      return Path.Combine(path, "video.cxml");
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

    #region --- Load ---

    public override void DisplayMetadata(string key)
    {
      using (XmlReader cxmlFallback = CreateXmlReader(GetFallbackMetadataFilePath()))
        using (XmlReader cxml = CreateXmlReader(GetMetadataFilepath(key))) 
          DisplayMetadata(key, (IVideoMetadata)new VideoMetadata().Load(key, cxml, cxmlFallback));
    }

    public void DisplayMetadata(string key, IVideoMetadata metadata)
    {
      UI.Load(txtTitle, metadata.Title);
      UI.Load(linkUrl, new Uri("http://studio.clipflair.net/?video=" + key));
      UI.Load(txtDescription, metadata.Description);

      //no need to show metadata.Filename since we calculate and show the URL, plus the filename is used as the key and shown at the dropdown list
      UI.Load(lblFirstPublished, metadata.FirstPublished.ToString(CXML.DEFAULT_DATETIME_FORMAT));
      UI.Load(lblLastUpdated, metadata.LastUpdated.ToString(CXML.DEFAULT_DATETIME_FORMAT));

      UI.Load(clistAudioLanguage, metadata.AudioLanguage);
      UI.Load(clistCaptionsLanguage, metadata.CaptionsLanguage);
      UI.Load(clistGenre, metadata.Genre);
      UI.Load(txtDuration, metadata.Duration);
      UI.Load(clistAudiovisualRichness, metadata.AudiovisualRichness);
      UI.Load(cbPedagogicalAdaptability, metadata.PedagogicalAdaptability);

      UI.Load(clistAgeGroup, metadata.AgeGroup);
      UI.Load(txtKeywords, metadata.Keywords);
      UI.Load(txtAuthorSource, metadata.AuthorSource);
      UI.Load(txtLicense, metadata.License);
    }

    #endregion

    #region --- Save ---

    public override ICXMLMetadata ExtractMetadata(string key)
    {
      IVideoMetadata metadata = new VideoMetadata();

      metadata.Title = txtTitle.Text;
      metadata.Image = "../video/" + key + "/" + key + "_thumb.jpg";
      metadata.Url = new Uri("http://studio.clipflair.net/?video=" + key);
      metadata.Description = txtDescription.Text;

      metadata.Filename = key;
      string folderPath = Path.Combine(path, key);
      metadata.FirstPublished = Directory.GetCreationTimeUtc(folderPath);
      metadata.LastUpdated = Directory.GetLastWriteTimeUtc(folderPath); //not sure if folders do keep such field

      metadata.AgeGroup = UI.GetSelected(clistAgeGroup);
      metadata.Keywords = UI.GetCommaSeparated(txtKeywords);
      metadata.AuthorSource = UI.GetCommaSeparated(txtAuthorSource);
      metadata.License = txtLicense.Text;

      metadata.AudioLanguage = UI.GetSelected(clistAudioLanguage);
      metadata.CaptionsLanguage = UI.GetSelected(clistCaptionsLanguage);
      metadata.Genre = UI.GetSelected(clistGenre);
      metadata.Duration = txtDuration.Text;
      metadata.AudiovisualRichness = UI.GetSelected(clistAudiovisualRichness);
      metadata.PedagogicalAdaptability = cbPedagogicalAdaptability.Checked;

      return metadata;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      DoSave();
    }

    public override void Merge()
    {
      Merge("ClipFlair Gallery Clips", VideoMetadata.MakeVideoFacetCategories());
    }

    #endregion

  }

}