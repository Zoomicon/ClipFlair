//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityDefaults.cs
//Version: 20130118

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
    public static readonly TimeSpan DefaultTime = TimeSpan.Zero;
    public const CaptionRegion DefaultCaptions = null; //don't make this "static readonly", it's an object reference, not a struct (better check for "null" in the view and create default instance if needed)
    public static readonly Point DefaultViewPosition = new Point(0, 0);
    public const double DefaultViewWidth = 1000;
    public const double DefaultViewHeight = 700;
    public const double DefaultContentZoom = 1.0;
    public const bool DefaultContentZoomable = true;
    public const bool DefaultContentPartsConfigurable = true;
    public const bool DefaultToolbarVisible = true;

    #region Methods

    public static void SetDefaults(IActivity activity)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(activity);
      activity.Title = DefaultTitle;
      activity.Width = DefaultWidth;
      activity.Height = DefaultHeight;

      //IActivity defaults
      activity.Source = DefaultSource;
      activity.Time = DefaultTime;
      activity.Captions = DefaultCaptions;
      activity.ViewPosition = DefaultViewPosition;
      activity.ViewWidth = DefaultViewWidth;
      activity.ViewHeight = DefaultViewHeight;
      activity.ContentZoom = DefaultContentZoom;
      activity.ContentZoomable = DefaultContentZoomable;
      activity.ContentPartsConfigurable = DefaultContentPartsConfigurable;
      activity.ToolbarVisible = DefaultToolbarVisible;
    }

    #endregion

  }

}