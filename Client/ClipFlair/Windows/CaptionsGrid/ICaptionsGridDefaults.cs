//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsGridDefaults.cs
//Version: 20121106

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class ICaptionsGridDefaults
  {

    #region IView defaults - overrides
    
    public static string DefaultTitle = "Captions";
    
    #endregion

    public static Uri DefaultSource = null;
    public static TimeSpan DefaultTime = TimeSpan.Zero;
    public const bool DefaultStartTimeVisible = true;
    public const bool DefaultEndTimeVisible = true;
    public const bool DefaultDurationVisible = false;
    public const bool DefaultCaptionVisible = true;
    public const bool DefaultCaptionAudioVisible = true;
  }

}