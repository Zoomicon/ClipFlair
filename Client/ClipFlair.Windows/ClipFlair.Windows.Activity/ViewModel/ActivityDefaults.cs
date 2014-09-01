//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityDefaults.cs
//Version: 20140901

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class ActivityDefaults
  {

    #region IView defaults - overrides
    
    public const string DefaultTitle = "Activity";
    public const double DefaultWidth = 740;
    public const double DefaultHeight = 400;
    public static readonly Color DefaultBackgroundColor = Color.FromArgb(0xFF, 0x27, 0x63, 0x88); //#276388
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0x08, 0x1A, 0x26); //#081A26
    
    #endregion
    
    public const Uri DefaultSource = null;
    public const CaptionRegion DefaultCaptions = null; //don't make this "static readonly", it's an object reference, not a struct (better check for "null" in the view and create default instance if needed)
    public static readonly Point DefaultViewPosition = new Point(0, 0);
    public const double DefaultViewWidth = 1000; //do not confuse with DefaultWidth (see above)
    public const double DefaultViewHeight = 700; //do not confuse with DefaultHeight (see above)
    public const double DefaultContentZoom = 1.0;
    public const bool DefaultContentZoomable = true;
    public const bool DefaultContentZoomToFit = true;
    public const bool DefaultContentPartsConfigurable = true;
    public const bool DefaultToolbarVisible = true;
    public const Orientation DefaultToolbarOrientation = Orientation.Horizontal;

    #region Methods

    public static void SetDefaults(IActivity view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;
      view.BackgroundColor = DefaultBackgroundColor;
      view.BorderColor = DefaultBorderColor;

      //IActivity defaults
      view.Source = DefaultSource;
      view.Captions = DefaultCaptions;
      view.ViewPosition = DefaultViewPosition;
      view.ViewWidth = DefaultViewWidth;
      view.ViewHeight = DefaultViewHeight;
      view.ContentZoom = DefaultContentZoom;
      view.ContentZoomable = DefaultContentZoomable;
      view.ContentZoomToFit = DefaultContentZoomToFit;
      view.ContentPartsConfigurable = DefaultContentPartsConfigurable;
      view.ToolbarVisible = DefaultToolbarVisible;
      view.ToolbarOrientation = DefaultToolbarOrientation;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}