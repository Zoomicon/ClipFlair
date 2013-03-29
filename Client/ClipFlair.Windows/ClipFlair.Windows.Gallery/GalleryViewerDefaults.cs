//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryViewerDefaults.cs
//Version: 20130326

using System;
using System.Windows;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class GalleryViewerDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Gallery";
    public const double DefaultWidth = 600;
    public const double DefaultHeight = 400;
        
    #endregion

    public const Uri DefaultSource = null;
    public const Stretch DefaultStretch = Stretch.Uniform;

    #region Methods

    public static void SetDefaults(IGalleryViewer Gallery)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(Gallery);
      Gallery.Title = DefaultTitle;
      Gallery.Width = DefaultWidth;
      Gallery.Height = DefaultHeight;

      //IGalleryViewer defaults
      Gallery.Source = DefaultSource;
      Gallery.Stretch = DefaultStretch;
    }

    #endregion

  }

}