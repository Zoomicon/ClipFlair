//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerDefaults.cs
//Version: 20130118

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class MediaPlayerDefaults
  {
    #region IView defaults - overrides

    public const string DefaultTitle = "Media";
    public const double DefaultWidth = 600;
    public const double DefaultHeight = 400;

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

    #region Methods

    public static void SetDefaults(IMediaPlayer player)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(player);
      player.Title = DefaultTitle;
      player.Width = DefaultWidth;
      player.Height = DefaultHeight;

      //IMediaPlayer defaults
      player.Source = DefaultSource;
      player.Time = DefaultTime;
      player.Captions = DefaultCaptions;
      player.Speed = DefaultSpeed;
      player.Volume = DefaultVolume;
      player.AutoPlay = DefaultAutoPlay;
      player.Looping = DefaultLooping;
      player.VideoVisible = DefaultVideoVisible;
      player.ControllerVisible = DefaultControllerVisible;
      player.CaptionsVisible = DefaultCaptionsVisible;
    }

    #endregion

  }
}