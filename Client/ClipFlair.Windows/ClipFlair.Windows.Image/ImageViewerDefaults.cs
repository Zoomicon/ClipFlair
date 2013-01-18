//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageViewerDefaults.cs
//Version: 20130118

using System;
using System.Windows;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class ImageViewerDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Image";
    public const double DefaultWidth = 400;
    public const double DefaultHeight = 400;
        
    #endregion

    public const Uri DefaultSource = null;
    public const Stretch DefaultStretch = Stretch.Uniform;

    #region Methods

    public static void SetDefaults(IImageViewer image)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(image);
      image.Title = DefaultTitle;
      image.Width = DefaultWidth;
      image.Height = DefaultHeight;

      //IImageViewer defaults
      image.Source = DefaultSource;
      image.Stretch = DefaultStretch;
    }

    #endregion

  }

}