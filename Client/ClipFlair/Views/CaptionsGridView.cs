//Filename: CaptionsGridView.cs
//Version: 20120814

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class CaptionsGridView: BaseView, ICaptionsGrid
  {
    public CaptionsGridView()
    {
      source = ICaptionsGridDefaults.DefaultSource;
      Time = ICaptionsGridDefaults.DefaultTime;
      StartTimeVisible = ICaptionsGridDefaults.DefaultStartTimeVisible;
      EndTimeVisible = ICaptionsGridDefaults.DefaultEndTimeVisible;
      DurationVisible = ICaptionsGridDefaults.DefaultDurationVisible;
      CaptionVisible = ICaptionsGridDefaults.DefaultCaptionVisible;
    }

    #region ICaptionsGrid

    private Uri source;
    private TimeSpan time;
    private bool startTimeVisible;
    private bool endTimeVisible;
    private bool durationVisible;
    private bool captionVisible;

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

    public bool DurationVisible
    {
      get { return durationVisible; }
      set
      {
        durationVisible = value;
        RaisePropertyChanged(ICaptionsGridProperties.PropertyDurationVisible);
      }
    }

    public bool CaptionVisible
    {
      get { return captionVisible; }
      set
      {
        captionVisible = value;
        RaisePropertyChanged(ICaptionsGridProperties.PropertyCaptionVisible);
      }
    }

    #endregion
  }

}
