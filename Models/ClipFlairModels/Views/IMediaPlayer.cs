//Version: 20120730

using System;

namespace ClipFlair.Models.Views
{

  public static class IMediaPlayerProperties
  {
    public const string PropertyTime = "Time";
    public const string PropertySpeed = "Speed";
    public const string PropertyVolume = "Volume";
    public const string PropertyControllerVisible = "ControllerVisible";
    public const string PropertyCaptionsVisible = "CaptionsVisible";
  }

  public static class IMediaPlayerDefaults
  {
    public static TimeSpan DefaultTime = TimeSpan.Zero;
    public const double DefaultSpeed = 1.0;
    public const double DefaultVolume = 1.0;
    public const bool DefaultControllerVisible = true;
    public const bool DefaultCaptionsVisible = false;
  }
  
  public interface IMediaPlayer: IView
  {

    TimeSpan Time { get; set; }
    double Speed { get; set; }
    double Volume { get; set; }
    bool ControllerVisible { get; set; }
    bool CaptionsVisible { get; set; }

    void Play();
    void Pause();
    void Stop(); //also resets time to 0
    //...
  }

}
