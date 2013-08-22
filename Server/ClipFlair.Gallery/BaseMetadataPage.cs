//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseMetadataPage.cs
//Version: 20130823

using Metadata.CXML;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace ClipFlair.Gallery
{

  public abstract class BaseMetadataPage : System.Web.UI.Page
  {

    protected DropDownList _listItems;

    protected override void OnPreRenderComplete(EventArgs e)
    {
      base.OnPreRenderComplete(e);

      if (!IsPostBack)
      {
        if (Request.QueryString["generate"] != null)
          Generate();
        else if (Request.QueryString["merge"] != null)
          Merge();

        UpdateSelection();
      }
    }

    protected void Generate()
    {
      foreach (ListItem l in _listItems.Items)
      {
        string key = l.Text;
        //if (!File.Exists(GetMetadataFilepath(key)))
        { //not using the above line anymore so that we can save existing files again after any format change
          UpdateSelection(key); //must pass value here
          SaveMetadata(key); //must pass value here
        }
      }
    }
    
    #region --- Select ---

    public void UpdateSelection(string key)
    {
      DisplayMetadata(key);
    }

    protected void UpdateSelection()
    {
      UpdateSelection(_listItems.SelectedValue);
    }

    #endregion

    #region --- Save ---

    protected void DoSave()
    {
      SaveMetadata();
      /**/
      Merge();
      UpdateSelection(); //must do after merge (else will show metadata of last item on page controls), but anyway should do to check that the metadata was saved OK
    }

    protected void SaveMetadata()
    {
      SaveMetadata(_listItems.SelectedValue);
    }

    protected void SaveMetadata(string key)
    {
      string cxmlFilename = GetMetadataFilepath(key);
      Directory.CreateDirectory(Path.GetDirectoryName(cxmlFilename)); //create any parent directories needed
      using (XmlWriter cxml = XmlWriter.Create(cxmlFilename))
        ExtractMetadata(key).Save(cxml);
    }

    protected void Merge(string collectionTitle, IEnumerable<XElement> facetCategories)
    {
      IList<ICXMLMetadata> metadataItems = new List<ICXMLMetadata>();

      foreach (ListItem l in _listItems.Items)
      {
        string key = l.Text;
        UpdateSelection(key); //must pass value here
        metadataItems.Add(ExtractMetadata(key));
      }

      string cxmlFilename = GetMergeMetadataFilePath();
      Directory.CreateDirectory(Path.GetDirectoryName(cxmlFilename)); //create any parent directories needed
      using (XmlWriter cxml = XmlWriter.Create(cxmlFilename))
        CXMLMetadata.Save(cxml, collectionTitle, facetCategories, metadataItems.ToArray());
    }

    #endregion

    #region --- Abstract methods ---

    public abstract string GetMetadataFilepath(string key);
    public abstract string GetFallbackMetadataFilePath();
    public abstract string GetMergeMetadataFilePath();

    public abstract void Merge();
    public abstract void DisplayMetadata(string key);
    public abstract ICXMLMetadata ExtractMetadata(string key);

    #endregion

  }

}