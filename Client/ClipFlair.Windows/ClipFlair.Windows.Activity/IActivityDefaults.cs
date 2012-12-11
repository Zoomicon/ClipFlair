//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivityDefaults.cs
//Version: 20121206

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IActivityDefaults
  {

    #region IView defaults - overrides
    
    public const string DefaultTitle = "Activity";
    
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
  }

}