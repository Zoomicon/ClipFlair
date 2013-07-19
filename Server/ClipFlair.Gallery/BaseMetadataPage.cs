//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: list.aspx.cs
//Version: 20130719

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;


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

    #region --- Commands ---

    protected void Merge()
    {
      //TODO
    }

    protected void Generate()
    {
      foreach (ListItem l in _listItems.Items)
      {
        string key = l.Text;
        if (!File.Exists(GetMetadataFilepath(key))) //to not lose newer data, only saving file from old data if it doesn't already exist
        {
          UpdateSelection(key); //must pass value here
          SaveMetadata(key); //must pass value here
        }
      }

    }

    #endregion
     
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

    #endregion

    #region --- Abstract methods ---

    public abstract string GetMetadataFilepath(String key);
    public abstract string GetFallbackMetadataFilePath();
    public abstract string GetMergeMetadataFilePath();
    
    public abstract void DisplayMetadata(String key);
    public abstract IMetadata ExtractMetadata(String key);

    #endregion

  }

}