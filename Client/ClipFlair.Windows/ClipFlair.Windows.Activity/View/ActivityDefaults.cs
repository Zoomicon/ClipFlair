//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityDefaults.cs
//Version: 20131205

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class ActivityDefaults
  {

    #region IView defaults - overrides
    
    public const string DefaultTitle = "Activity";
    public const double DefaultWidth = 600;
    public const double DefaultHeight = 400;
    
    #endregion
    
    public const Uri DefaultSource = null;
    public const CaptionRegion DefaultCaptions = null; //don't make this "static readonly", it's an object reference, not a struct (better check for "null" in the view and create default instance if needed)
    public static readonly Point DefaultViewPosition = new Point(0, 0);
    public const double DefaultViewWidth = 1000;
    public const double DefaultViewHeight = 700;
    public const double DefaultContentZoom = 1.0;
    public const bool DefaultContentZoomable = true;
    public const bool DefaultContentZoomToFit = true;
    public const bool DefaultContentPartsConfigurable = true;
    public const bool DefaultToolbarVisible = true;

    #region Methods

    public static void SetDefaults(IActivity view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;

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

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}