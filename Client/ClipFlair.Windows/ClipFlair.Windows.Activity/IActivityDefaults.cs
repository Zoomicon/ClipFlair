//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivityDefaults.cs
//Version: 20121203

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
    public const CaptionRegion DefaultCaptions = null;
    public static readonly Point DefaultViewPosition = new Point(0, 0);
    public const double DefaultViewWidth = 1000;
    public const double DefaultViewHeight = 700;
    public const double DefaultContentZoom = 1.0;
    public const bool DefaultContentZoomable = true;
    public const bool DefaultContentPartsConfigurable = true;
  }

}