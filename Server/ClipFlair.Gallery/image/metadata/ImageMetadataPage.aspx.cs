//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageMetadataPage.aspx.cs
//Version: 20140410

using ClipFlair.Metadata;
using Metadata.CXML;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace ClipFlair.Gallery
{
  public partial class ImageMetadataPage : BaseMetadataPage
  {

    private string path = HttpContext.Current.Server.MapPath("~/image");
    private string filter = "*.png|*.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
      _listItems = listItems; //allow the ancestor class to access our listItems UI object 
      
      if (!IsPostBack)
      {
        listItems.DataSource =
          filter.Split('|').SelectMany(
            oneFilter => Directory.EnumerateFiles(path, oneFilter) //Available in .NET4, more efficient than GetFiles
                         .Select(f => new { Filename = Path.GetFileName(f) })
          );

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
      return Path.Combine(path, "metadata/old_images.cxml");
    }

    public override string GetMergeMetadataFilePath()
    {
      return Path.Combine(path, "images.cxml");
    }

    #endregion

    #region --- Load ---

    public override void DisplayMetadata(string key)
    {
      using (XmlReader cxmlFallback = CreateXmlReader(GetFallbackMetadataFilePath()))
        using (XmlReader cxml = CreateXmlReader(GetMetadataFilepath(key))) 
          DisplayMetadata(key, (IImageMetadata)new ImageMetadata().Load(key, cxml, cxmlFallback));
    }

    public void DisplayMetadata(string key, IImageMetadata metadata)
    {
      UI.LoadTextBox(txtTitle, metadata.Title);
      UI.LoadHyperlink(linkUrl, new Uri("http://studio.clipflair.net/?image=" + key));
      UI.LoadTextBox(txtDescription, metadata.Description);

      //no need to show metadata.Filename since we calculate and show the URL, plus the filename is used as the key and shown at the dropdown list
      UI.LoadLabel(lblFirstPublished, metadata.FirstPublished.ToString(CXML.DEFAULT_DATETIME_FORMAT));
      UI.LoadLabel(lblLastUpdated, metadata.LastUpdated.ToString(CXML.DEFAULT_DATETIME_FORMAT));
      
      UI.LoadCheckBoxList(clistCaptionsLanguage, metadata.CaptionsLanguage);
      //UI.LoadCheckBoxList(clistGenre, metadata.Genre);

      UI.LoadCheckBoxList(clistAgeGroup, metadata.AgeGroup);
      UI.LoadTextBox(txtKeywords, metadata.Keywords);
      UI.LoadTextBox(txtAuthorSource, metadata.AuthorSource);
      UI.LoadTextBox(txtLicense, metadata.License);
    }

    #endregion

    #region --- Save ---

    public override ICXMLMetadata ExtractMetadata(string key)
    {
      IImageMetadata metadata = new ImageMetadata();

      metadata.Title = txtTitle.Text;
      metadata.Image = "../image/" + key;
      metadata.Url = new Uri("http://studio.clipflair.net/?image=" + key);
      metadata.Description = txtDescription.Text;

      metadata.Filename = key;
      string filePath = Path.Combine(path, key);
      metadata.FirstPublished = File.GetCreationTimeUtc(filePath);
      metadata.LastUpdated = File.GetLastWriteTimeUtc(filePath);

      metadata.AgeGroup = UI.GetSelected(clistAgeGroup);
      metadata.Keywords = UI.GetCommaSeparated(txtKeywords);
      metadata.AuthorSource = UI.GetCommaSeparated(txtAuthorSource);
      metadata.License = txtLicense.Text;

      metadata.CaptionsLanguage = UI.GetSelected(clistCaptionsLanguage);
      //metadata.Genre = UI.GetSelected(clistGenre); //TODO: Could have image type (Comics, Map, Historical, Religious etc.)

      return metadata;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      DoSave();
    }

    public override void Merge()
    {
      Merge("ClipFlair Gallery Clips", ImageMetadata.MakeImageFacetCategories());
    }

    #endregion

  }

}