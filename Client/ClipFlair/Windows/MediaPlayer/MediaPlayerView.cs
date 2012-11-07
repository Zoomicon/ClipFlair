//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerView.cs
//Version: 20121106

using System;
using System.Runtime.Serialization;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")]
  public class MediaPlayerView: BaseView, IMediaPlayer
  {
    public MediaPlayerView()
    {
      //BaseView defaults - overrides
      Title = IMediaPlayerDefaults.DefaultTitle;
    }
        
    #region Fields

    //can set fields directly here or at constructor
    private Uri source = IMediaPlayerDefaults.DefaultSource;
    private TimeSpan time = IMediaPlayerDefaults.DefaultTime;
    private double speed = IMediaPlayerDefaults.DefaultSpeed;
    private double volume = IMediaPlayerDefaults.DefaultVolume;
    private bool looping = IMediaPlayerDefaults.DefaultLooping;
    private bool videoVisible = IMediaPlayerDefaults.DefaultVideoVisible;
    private bool controllerVisible = IMediaPlayerDefaults.DefaultControllerVisible;
    private bool captionsVisible = IMediaPlayerDefaults.DefaultCaptionsVisible;

    #endregion

    #region Properties
    
    [DataMember]
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

    [DataMember]
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

    [DataMember]
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

    [DataMember]
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

    [DataMember]
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

    [DataMember]
    public bool VideoVisible
    {
      get { return videoVisible; }
      set
      {
        if (value != videoVisible)
        {
          videoVisible = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertyVideoVisible);
        }
      }
    }

    [DataMember]
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

    [DataMember]
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
      Speed = 1.0; //TODO: should maybe set to rate that was before pause?
    }

    public void Pause()
    {
      Speed = 0; //should maybe rename to rate
    }

    public void Stop()
    {
      Pause();
      Time = TimeSpan.Zero;
    }

    #endregion

  }

}
