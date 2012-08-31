﻿//Filename: IMediaPlayer.cs
//Version: 20120831

using System;

namespace ClipFlair.Models.Views
{

  public static class IMediaPlayerProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyTime = "Time";
    public const string PropertySpeed = "Speed";
    public const string PropertyVolume = "Volume";
    public const string PropertyLooping = "Looping";
    public const string PropertyControllerVisible = "ControllerVisible";
    public const string PropertyCaptionsVisible = "CaptionsVisible";
  }

  public static class IMediaPlayerDefaults
  {
    public static Uri DefaultSource = null;
    public static TimeSpan DefaultTime = TimeSpan.Zero;
    public const double DefaultSpeed = 1.0;
    public const double DefaultVolume = 1.0;
    public const bool DefaultLooping = false;
    public const bool DefaultControllerVisible = true;
    public const bool DefaultCaptionsVisible = false;
  }
  
  public interface IMediaPlayer: IView
  {
    Uri Source { get; set; }
    TimeSpan Time { get; set; }
    double Speed { get; set; }
    double Volume { get; set; }
    bool Looping { get; set; }
    bool ControllerVisible { get; set; }
    bool CaptionsVisible { get; set; }

    void Play();
    void Pause();
    void Stop(); //also resets time to 0
    //...
  }

}
