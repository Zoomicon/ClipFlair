//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridView.cs
//Version: 20121111

using System;
using System.Runtime.Serialization;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace="http://clipflair.net/Contracts/View")]
  public class CaptionsGridView: BaseView, ICaptionsGrid
  {
    public CaptionsGridView()
    {
    }

    #region Fields

    //can set fields directly here or at the construcor
    private Uri source = ICaptionsGridDefaults.DefaultSource;
    private TimeSpan time = ICaptionsGridDefaults.DefaultTime;
    private bool startTimeVisible = ICaptionsGridDefaults.DefaultStartTimeVisible;
    private bool endTimeVisible = ICaptionsGridDefaults.DefaultEndTimeVisible;
    private bool durationVisible = ICaptionsGridDefaults.DefaultDurationVisible;
    private bool captionVisible = ICaptionsGridDefaults.DefaultCaptionVisible;
    private bool captionAudioVisible = ICaptionsGridDefaults.DefaultCaptionAudioVisible;

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
    public bool CaptionAudioVisible
    {
      get { return captionAudioVisible; }
      set
      {
        captionAudioVisible = value;
        RaisePropertyChanged(ICaptionsGridProperties.PropertyCaptionAudioVisible);
      }
    }

    #endregion

  }

}
