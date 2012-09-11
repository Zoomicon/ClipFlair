﻿//Filename: ICaptionsGrid.cs
//Version: 20120911

using System;

namespace ClipFlair.Models.Views
{

  public static class ICaptionsGridProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyTime = "Time";
    public const string PropertyStartTimeVisible = "StartTimeVisible";
    public const string PropertyEndTimeVisible = "EndTimeVisible";
    public const string PropertyDurationVisible = "DurationVisible";
    public const string PropertyCaptionVisible = "CaptionVisible";
    public const string PropertyCaptionAudioVisible = "CaptionAudioVisible";
  }

  public static class ICaptionsGridDefaults
  {
    public static Uri DefaultSource = null;
    public static TimeSpan DefaultTime = TimeSpan.Zero;
    public const bool DefaultStartTimeVisible = true;
    public const bool DefaultEndTimeVisible = true;
    public const bool DefaultDurationVisible = false;
    public const bool DefaultCaptionVisible = true;
    public const bool DefaultCaptionAudioVisible = false;
  }
  
  public interface ICaptionsGrid: IView
  {
    Uri Source { get; set; }
    TimeSpan Time { get; set; }
    bool StartTimeVisible { get; set; }
    bool EndTimeVisible { get; set; }
    bool DurationVisible { get; set; }
    bool CaptionVisible { get; set; }
    bool CaptionAudioVisible { get; set; }
  }

}
