﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMediaPlayer.cs
//Version: 20130419

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;

namespace ClipFlair.Windows.Views
{

  public static class IMediaPlayerProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyTime = "Time";
    public const string PropertyReplayOffset = "ReplayOffset";
    public const string PropertyCaptions = "Captions";
    public const string PropertySpeed = "Speed";
    public const string PropertyVolume = "Volume";
    public const string PropertyBalance = "Balance";
    public const string PropertyAutoPlay = "AutoPlay";
    public const string PropertyLooping = "Looping";
    public const string PropertyVideoVisible = "VideoVisible";   
    public const string PropertyControllerVisible = "ControllerVisible";
    public const string PropertyCaptionsVisible = "CaptionsVisible";
  }

  public interface IMediaPlayer: IView
  {
    Uri Source { get; set; }
    TimeSpan Time { get; set; }
    TimeSpan ReplayOffset { get; set; }
    CaptionRegion Captions { get; set; }
    double Speed { get; set; }
    double Volume { get; set; }
    double Balance { get; set; } //-1=left only, 0=left/right, 1=right only
    bool AutoPlay { get; set; }
    bool Looping { get; set; }
    bool VideoVisible { get; set; }
    bool ControllerVisible { get; set; }
    bool CaptionsVisible { get; set; }

    void Play();
    void Pause();
    void Stop(); //also resets time to 0
  }

}
