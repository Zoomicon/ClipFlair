//Filename: MediaPlayerView.cs
//Version: 20120824

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class MediaPlayerView: BaseView, IMediaPlayer
  {
    public MediaPlayerView()
    {
      //can set fields directly here since we don't yet have any PropertyChanged listeners
      source = IMediaPlayerDefaults.DefaultSource;
      time = IMediaPlayerDefaults.DefaultTime;
      speed = IMediaPlayerDefaults.DefaultSpeed;
      volume = IMediaPlayerDefaults.DefaultVolume;
      controllerVisible = IMediaPlayerDefaults.DefaultControllerVisible;
      captionsVisible = IMediaPlayerDefaults.DefaultCaptionsVisible;
    }
        
    #region IMediaPlayer

    #region Fields
 
    private Uri source;
    private TimeSpan time;
    private double speed;
    private double volume;
    private bool controllerVisible;
    private bool captionsVisible;

    #endregion

    #region Properties

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

    #endregion

    #region Methods
 
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

    #endregion
  }

}
