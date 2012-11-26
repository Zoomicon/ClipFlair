//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridView.cs
//Version: 20121124

using System;
using System.Runtime.Serialization;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace="http://clipflair.net/Contracts/View")]
  public class CaptionsGridView: BaseView, ICaptionsGrid
  {
    public CaptionsGridView()
    {
      {
        //BaseView defaults - overrides
        Title = ICaptionsGridDefaults.DefaultTitle;
      }
    }

    #region Fields

    //can set fields directly here or at the construcor
    private Uri source = ICaptionsGridDefaults.DefaultSource;
    private TimeSpan time = ICaptionsGridDefaults.DefaultTime;
    private bool actorVisible = ICaptionsGridDefaults.DefaultActorVisible;
    private bool startTimeVisible = ICaptionsGridDefaults.DefaultStartTimeVisible;
    private bool endTimeVisible = ICaptionsGridDefaults.DefaultEndTimeVisible;
    private bool durationVisible = ICaptionsGridDefaults.DefaultDurationVisible;
    private bool captionVisible = ICaptionsGridDefaults.DefaultCaptionVisible;
    private bool audioVisible = ICaptionsGridDefaults.DefaultAudioVisible;
    private bool commentsVisible = ICaptionsGridDefaults.DefaultCommentsVisible;

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
          RaisePropertyChanged(ICaptionsGridProperties.PropertySource);
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
          RaisePropertyChanged(ICaptionsGridProperties.PropertyTime);
        }
      }
    }

    [DataMember]
    public bool ActorVisible
    {
      get { return actorVisible; }
      set
      {
        if (value != actorVisible)
        {
          actorVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyActorVisible);
        }
      }
    }

    [DataMember]
    public bool StartTimeVisible
    {
      get { return startTimeVisible; }
      set
      {
        if (value != startTimeVisible)
        {
          startTimeVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyStartTimeVisible);
        }
      }
    }

    [DataMember]
    public bool EndTimeVisible
    {
      get { return endTimeVisible; }
      set {
        if (value != endTimeVisible)
        {
          endTimeVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyEndTimeVisible);
        }
      }
    }

    [DataMember]
    public bool DurationVisible
    {
      get { return durationVisible; }
      set
      {
        durationVisible = value;
        RaisePropertyChanged(ICaptionsGridProperties.PropertyDurationVisible);
      }
    }

    [DataMember]
    public bool CaptionVisible
    {
      get { return captionVisible; }
      set
      {
        captionVisible = value;
        RaisePropertyChanged(ICaptionsGridProperties.PropertyCaptionVisible);
      }
    }

    [DataMember]
    public bool AudioVisible
    {
      get { return audioVisible; }
      set
      {
        audioVisible = value;
        RaisePropertyChanged(ICaptionsGridProperties.PropertyAudioVisible);
      }
    }

    [DataMember]
    public bool CommentsVisible
    {
      get { return commentsVisible; }
      set
      {
        commentsVisible = value;
        RaisePropertyChanged(ICaptionsGridProperties.PropertyCommentsVisible);
      }
    }
    
    #endregion

  }

}
