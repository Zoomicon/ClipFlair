//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerDefaults.cs
//Version: 20131205

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class MediaPlayerDefaults
  {
    #region IView defaults - overrides

    public const string DefaultTitle = "Clip";
    public const double DefaultWidth = 600;
    public const double DefaultHeight = 400;

    #endregion

    public const Uri DefaultSource = null;
    public static readonly TimeSpan DefaultReplayOffset = TimeSpan.Zero;
    public const CaptionRegion DefaultCaptions = null;
    public const double DefaultSpeed = 1.0;
    public const double DefaultVolume = 1.0;
    public const double DefaultBalance = 0.0;
    public const bool DefaultAutoPlay = true;
    public const bool DefaultLooping = false;
    public const bool DefaultVideoVisible = true;
    public const bool DefaultControllerVisible = true;
    public const bool DefaultCaptionsVisible = true;
    
    #region Methods

    public static void SetDefaults(IMediaPlayer view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;

      //IMediaPlayer defaults
      view.Source = DefaultSource;
      view.ReplayOffset = DefaultReplayOffset;
      view.Captions = DefaultCaptions;
      view.Speed = DefaultSpeed;
      view.Volume = DefaultVolume;
      view.Balance = DefaultBalance;
      view.AutoPlay = DefaultAutoPlay;
      view.Looping = DefaultLooping;
      view.VideoVisible = DefaultVideoVisible;
      view.ControllerVisible = DefaultControllerVisible;
      view.CaptionsVisible = DefaultCaptionsVisible;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }
}