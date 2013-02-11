//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerView.cs
//Version: 20130211

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class MediaPlayerView: BaseView, IMediaPlayer
  {
    public MediaPlayerView()
    {
    }
        
    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private TimeSpan time;
    private CaptionRegion captions;
    private double speed;
    private double volume;
    private bool autoPlay;
    private bool looping;
    private bool videoVisible;
    private bool controllerVisible;
    private bool captionsVisible;

    #endregion

    #region Properties
    
    [DataMember(Order=1)] //Order=1 is for making sure this is deserialized after other fields, including Time which has Order=0
    [DefaultValue(MediaPlayerDefaults.DefaultSource)]
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

    [DataMember(Order=0)] //TODO: see why it stops loading state if we use Order=1 here and Order=0 at Source property //TODO: maybe make source respect current Time? (when setting source from backpanel could erase Time with an event handler)
    //[DefaultValue(MediaPlayerDefaults.DefaultTime)] //can't use static fields here (and we're forced to use one for TimeSpan unfortunately, doesn't work with const)
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

    //don't make this a DataMember (not storing in MediaPlayer)
    [DefaultValue(MediaPlayerDefaults.DefaultCaptions)]
    public CaptionRegion Captions
    {
      get { return captions; }
      set
      {
        if (value == null)
          value = new CaptionRegion(); //if null create a new CaptionRegion
        
        if (value != captions)
        {
          captions = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertyCaptions);
        }
      }
    }

    [DataMember]
    [DefaultValue(MediaPlayerDefaults.DefaultSpeed)]
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
    [DefaultValue(MediaPlayerDefaults.DefaultVolume)]
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
    [DefaultValue(MediaPlayerDefaults.DefaultAutoPlay)]
    public bool AutoPlay
    {
      get { return autoPlay; }
      set
      {
        if (value != autoPlay)
        {
          autoPlay = value;
          RaisePropertyChanged(IMediaPlayerProperties.PropertyAutoPlay);
        }
      }
    }

    [DataMember]
    [DefaultValue(MediaPlayerDefaults.DefaultLooping)]
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
    [DefaultValue(MediaPlayerDefaults.DefaultVideoVisible)]
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
    [DefaultValue(MediaPlayerDefaults.DefaultControllerVisible)]
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
    [DefaultValue(MediaPlayerDefaults.DefaultCaptionsVisible)]
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

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      MediaPlayerDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }
 
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
