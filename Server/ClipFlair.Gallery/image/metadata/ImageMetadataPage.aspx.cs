//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageMetadataPage.aspx.cs
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
  public partial class ImageMetadataPage : BaseMetadataPage
  {

    private string path = HttpContext.Current.Server.MapPath("~/image");
    private string filter = "*.png|*.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
      _listItems = listItems; //allow the ancestor class to access our listItems UI object 
      
      if (!IsPostBack)
      {
        var itemPleaseSelect = new[] { new { Filename = "* Please select..." } };

        var items = filter.Split('|').SelectMany(
            oneFilter => Directory.EnumerateFiles(path, oneFilter) //Available in .NET4, more efficient than GetFiles
                         .Select(f => new { Filename = Path.GetFileName(f) })
          );

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
      return Path.Combine(path, "metadata/old_images.cxml");
    }

    public override string GetMergeMetadataFilePath()
    {
      return Path.Combine(path, "images.cxml");
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
          DisplayMetadata(key, (IImageMetadata)new ImageMetadata().Load(key, cxml, cxmlFallback));
    }

    public void DisplayMetadata(string key, IImageMetadata metadata)
    {
      UI.Load(txtTitle, metadata.Title);
      UI.Load(linkUrl, new Uri("http://studio.clipflair.net/?image=" + key));
      UI.Load(txtDescription, metadata.Description);

      //no need to show metadata.Filename since we calculate and show the URL, plus the filename is used as the key and shown at the dropdown list
      UI.Load(lblFirstPublished, metadata.FirstPublished.ToString(CXML.DEFAULT_DATETIME_FORMAT));
      UI.Load(lblLastUpdated, metadata.LastUpdated.ToString(CXML.DEFAULT_DATETIME_FORMAT));
      
      UI.Load(clistCaptionsLanguage, metadata.CaptionsLanguage);
      //UI.Load(clistGenre, metadata.Genre);

      UI.Load(clistAgeGroup, metadata.AgeGroup);
      UI.Load(txtKeywords, metadata.Keywords);
      UI.Load(txtAuthorSource, metadata.AuthorSource);
      UI.Load(txtLicense, metadata.License);
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