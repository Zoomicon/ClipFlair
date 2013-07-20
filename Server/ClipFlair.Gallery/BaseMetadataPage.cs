//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: list.aspx.cs
//Version: 20130720

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
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

    protected void SaveMetadata()
    {
      SaveMetadata(_listItems.SelectedValue);
    }

    protected void SaveMetadata(string key)
    {
      ExtractMetadata(key).Save(GetMetadataFilepath(key));
    }

    protected void Merge(string collectionTitle, IEnumerable<XElement> facetCategories)
    {
      IList<IMetadata> metadataItems = new List<IMetadata>();

      foreach (ListItem l in _listItems.Items)
      {
        string key = l.Text;
        UpdateSelection(key); //must pass value here
        metadataItems.Add(ExtractMetadata(key));
      }

      BaseMetadata.Save(GetMergeMetadataFilePath(), collectionTitle, facetCategories, metadataItems.ToArray());
    }

    #endregion

    #region --- Abstract methods ---

    public abstract string GetMetadataFilepath(string key);
    public abstract string GetFallbackMetadataFilePath();
    public abstract string GetMergeMetadataFilePath();

    public abstract void Merge();
    public abstract void DisplayMetadata(string key);
    public abstract IMetadata ExtractMetadata(string key);

    #endregion

  }

}