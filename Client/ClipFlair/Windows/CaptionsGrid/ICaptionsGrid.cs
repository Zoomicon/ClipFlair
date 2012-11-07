//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsGrid.cs
//Version: 20121106

using System;

namespace ClipFlair.Windows.Views
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
