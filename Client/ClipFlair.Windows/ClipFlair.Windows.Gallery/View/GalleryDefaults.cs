//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryDefaults.cs
//Version: 20131205

using System;
using System.Windows;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class GalleryDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Gallery";
    public const double DefaultWidth = 800;
    public const double DefaultHeight = 600;
        
    #endregion

    public const Uri DefaultSource = null;
    public const string DefaultFilter = "";

    #region Methods

    public static void SetDefaults(IGallery view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;

      //IGalleryViewer defaults
      view.Source = DefaultSource;
      view.Filter = DefaultFilter;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}