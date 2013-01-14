//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsGridDefaults.cs
//Version: 20130114

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class ICaptionsGridDefaults
  {

    #region IView defaults - overrides
    
    public const string DefaultTitle = "Captions";
    public const double DefaultWidth = 600;
    public const double DefaultHeight = 400;

    public const double DefaultWidth_Revoicing = 300;
    
    #endregion

    public const Uri DefaultSource = null;
    public static readonly TimeSpan DefaultTime = TimeSpan.Zero;
    public const CaptionRegion DefaultCaptions = null; //don't make this "static readonly", it's an object reference, not a struct (better check for "null" in the view and create default instance if needed)
    public const bool DefaultRoleVisible = true;
    public const bool DefaultStartTimeVisible = true;
    public const bool DefaultEndTimeVisible = true;
    public const bool DefaultDurationVisible = false;
    public const bool DefaultCaptionVisible = true;
    public const bool DefaultAudioVisible = false;
    public const bool DefaultCommentsVisible = false;
  }

}