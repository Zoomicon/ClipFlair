//Filename: MediaPlayerView.cs
//Version: 20120814

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class MediaPlayerView: BaseView, IMediaPlayer
  {
    public MediaPlayerView()
    {
      Source = IMediaPlayerDefaults.DefaultSource;
      Time = IMediaPlayerDefaults.DefaultTime;
      Speed = IMediaPlayerDefaults.DefaultSpeed;
      Volume = IMediaPlayerDefaults.DefaultVolume;
      ControllerVisible = IMediaPlayerDefaults.DefaultControllerVisible;
      CaptionsVisible = IMediaPlayerDefaults.DefaultCaptionsVisible;
    }
        
    #region IMediaPlayer

    private Uri source;
    private TimeSpan time;
    private double speed;
    private double volume;
    private bool controllerVisible;
    private bool captionsVisible;

    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertySource);
        }
      }
    }

    public TimeSpan Time
    {
      get { return time; }
      set
      {
        if (value != time)
        {
          time = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertyTime);
        }
      }
    }

    public double Speed
    {
      get { return speed; }
      set
      {
        if (value != speed)
        {
          speed = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertySpeed);
        }
      }
    }

    public double Volume
    {
      get { return volume; }
      set
      {
        if (value != volume)
        {
          volume = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertyVolume);
        }
      }
    }

    public bool ControllerVisible
    {
      get { return controllerVisible; }
      set
      {
        if (value != controllerVisible)
        {
          controllerVisible = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertyControllerVisible);
        }
      }
    }

    public bool CaptionsVisible
    {
      get { return captionsVisible; }
      set
      {
        if (value != captionsVisible)
        {
          captionsVisible = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertyCaptionsVisible);
        }
      }
    }

    public void Play()
    {
      throw new NotImplementedException();
    }

    public void Pause()
    {
      throw new NotImplementedException();
    }

    public void Stop()
    {
      Pause();
      Time = TimeSpan.Zero;
    }

    #endregion
  }

}
