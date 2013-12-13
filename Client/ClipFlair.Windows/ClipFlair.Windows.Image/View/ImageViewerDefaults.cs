﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageViewerDefaults.cs
//Version: 20131213

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class ImageViewerDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Image";
    public const double DefaultWidth = 400;
    public const double DefaultHeight = 400;
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0xE7, 0xB6, 0x00); //#E7B600
      //Color.FromArgb(0xFF, 0xEC, 0x46, 0x14); //#EC4614
        
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
      view.BorderColor = DefaultBorderColor;

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