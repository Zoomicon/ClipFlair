//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsGridDefaults.cs
//Version: 20121203

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class ICaptionsGridDefaults
  {

    #region IView defaults - overrides
    
    public const string DefaultTitle = "Captions";
    
    #endregion

    public const Uri DefaultSource = null;
    public static readonly TimeSpan DefaultTime = TimeSpan.Zero;
    public const CaptionRegion DefaultCaptions = null; 
    public const bool DefaultRoleVisible = false;
    public const bool DefaultStartTimeVisible = true;
    public const bool DefaultEndTimeVisible = true;
    public const bool DefaultDurationVisible = false;
    public const bool DefaultCaptionVisible = true;
    public const bool DefaultAudioVisible = true;
    public const bool DefaultCommentsVisible = false;
  }

}