//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryDefaults.cs
//Version: 20130701

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

    public static void SetDefaults(IGallery gallery)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(gallery);
      gallery.Title = DefaultTitle;
      gallery.Width = DefaultWidth;
      gallery.Height = DefaultHeight;

      //IGalleryViewer defaults
      gallery.Source = DefaultSource;
      gallery.Filter = DefaultFilter;
    }

    #endregion

  }

}