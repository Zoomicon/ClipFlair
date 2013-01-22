//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridDefaults.cs
//Version: 20130122

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class CaptionsGridDefaults
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
    public const bool DefaultWPMVisible = false;
    public const bool DefaultAudioVisible = false;
    public const bool DefaultCommentsVisible = false;

    #region Methods

    public static void SetDefaults(ICaptionsGrid captions)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(captions);
      captions.Title = DefaultTitle;
      captions.Width = DefaultWidth;
      captions.Height = DefaultHeight;

      //ICaptionsGrid defaults
      captions.Source = DefaultSource;
      captions.Time = DefaultTime;
      captions.Captions = DefaultCaptions;
      captions.RoleVisible = DefaultRoleVisible;
      captions.StartTimeVisible = DefaultStartTimeVisible;
      captions.EndTimeVisible = DefaultEndTimeVisible;
      captions.DurationVisible = DefaultDurationVisible;
      captions.CaptionVisible = DefaultCaptionVisible;
      captions.WPMVisible = DefaultWPMVisible;
      captions.AudioVisible = DefaultAudioVisible;
      captions.CommentsVisible = DefaultCommentsVisible;
    }

    #endregion

  }

}