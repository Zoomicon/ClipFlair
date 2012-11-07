//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMediaPlayerDefaults.cs
//Version: 20121106

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IMediaPlayerDefaults
  {
    #region IView defaults - overrides
    
    public static string DefaultTitle = "Media";
    
    #endregion

    public static Uri DefaultSource = null;
    public static TimeSpan DefaultTime = TimeSpan.Zero;
    public const double DefaultSpeed = 1.0;
    public const double DefaultVolume = 1.0;
    public const bool DefaultLooping = false;
    public const bool DefaultVideoVisible = true;
    public const bool DefaultControllerVisible = true;
    public const bool DefaultCaptionsVisible = false;
  }

}