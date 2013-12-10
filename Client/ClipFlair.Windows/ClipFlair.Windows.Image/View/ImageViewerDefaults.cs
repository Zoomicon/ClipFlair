//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageViewerDefaults.cs
//Version: 20131205

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
    public const bool DefaultContentZoomToFit = true;
    public const Uri DefaultActionURL = null;
    public static readonly TimeSpan? DefaultActionTime = null;

    #region Methods

    public static void SetDefaults(IImageViewer view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;

      //IImageViewer defaults
      view.Source = DefaultSource;
      view.ContentZoomToFit = DefaultContentZoomToFit;
      view.ActionURL = DefaultActionURL;
      view.ActionTime = DefaultActionTime;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}