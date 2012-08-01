//Version: 20120730

using System;

namespace ClipFlair.Models.Views
{

  public static class ICaptionsGridProperties
  {
    public const string PropertyTime = "Time";
    public const string PropertyStartTimeVisible = "StartTimeVisible";
    public const string PropertyEndTimeVisible = "EndTimeVisible";
    public const string PropertyDurationVisible = "DurationVisible";
    public const string PropertyCaptionVisible = "CaptionVisible";
  }

  public static class ICaptionsGridDefaults
  {
    public static TimeSpan DefaultTime = TimeSpan.Zero;
    public const bool DefaultStartTimeVisible = true;
    public const bool DefaultEndTimeVisible = true;
    public const bool DefaultDurationVisible = false;
    public const bool DefaultCaptionVisible = true;
    //...
  }
  
  public interface ICaptionsGrid: IView
  {

    TimeSpan Time { get; set; }
    bool StartTimeVisible { get; set; }
    bool EndTimeVisible { get; set; }
    bool DurationVisible { get; set; }
    bool CaptionVisible { get; set; }
  }

}
