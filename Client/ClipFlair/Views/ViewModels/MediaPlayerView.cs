﻿//Filename: MediaPlayerView.cs
//Version: 20120831

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class MediaPlayerView: BaseView, IMediaPlayer
  {
    public MediaPlayerView()
    {
    }
        
    #region IMediaPlayer

    #region Fields

    //can set fields directly here or at constructor
    private Uri source = IMediaPlayerDefaults.DefaultSource;
    private TimeSpan time = IMediaPlayerDefaults.DefaultTime;
    private double speed = IMediaPlayerDefaults.DefaultSpeed;
    private double volume = IMediaPlayerDefaults.DefaultVolume;
    private bool looping = IMediaPlayerDefaults.DefaultLooping;
    private bool controllerVisible = IMediaPlayerDefaults.DefaultControllerVisible;
    private bool captionsVisible = IMediaPlayerDefaults.DefaultCaptionsVisible;

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

    public bool Looping
    {
      get { return looping; }
      set
      {
        if (value != looping)
        {
          looping = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertyLooping);
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