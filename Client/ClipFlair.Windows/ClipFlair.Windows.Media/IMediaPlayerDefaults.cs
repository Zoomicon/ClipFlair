//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMediaPlayerDefaults.cs
//Version: 20121219

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IMediaPlayerDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Media";
    
    #endregion

    public const Uri DefaultSource = null;
    public static readonly TimeSpan DefaultTime = TimeSpan.Zero;
    public const CaptionRegion DefaultCaptions = null;
    public const double DefaultSpeed = 1.0;
    public const double DefaultVolume = 1.0;
    public const bool DefaultAutoPlay = true;
    public const bool DefaultLooping = false;
    public const bool DefaultVideoVisible = true;
    public const bool DefaultControllerVisible = true;
    public const bool DefaultCaptionsVisible = true;
  }

}