//Filename: CaptionsGridView.cs
//Version: 20120730

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class CaptionsGridView: BaseView, ICaptionsGrid
  {
    public CaptionsGridView()
    {
      Time = ICaptionsGridDefaults.DefaultTime;
      StartTimeVisible = ICaptionsGridDefaults.DefaultStartTimeVisible;
      EndTimeVisible = ICaptionsGridDefaults.DefaultEndTimeVisible;
      DurationVisible = ICaptionsGridDefaults.DefaultDurationVisible;
      CaptionVisible = ICaptionsGridDefaults.DefaultCaptionVisible;
    }

    #region ICaptionsGrid

    private TimeSpan time;
    private bool startTimeVisible;
    private bool endTimeVisible;
    private bool durationVisible;
    private bool captionVisible;

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
