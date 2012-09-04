//Filename: CaptionsGridView.cs
//Version: 20120904

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class CaptionsGridView: BaseView, ICaptionsGrid
  {
    public CaptionsGridView()
    {
    }

    #region ICaptionsGrid

    #region Fields

    //can set fields directly here or at the construcor
    private Uri source = ICaptionsGridDefaults.DefaultSource;
    private TimeSpan time = ICaptionsGridDefaults.DefaultTime;
    private bool startTimeVisible = ICaptionsGridDefaults.DefaultStartTimeVisible;
    private bool endTimeVisible = ICaptionsGridDefaults.DefaultEndTimeVisible;
    private bool durationVisible = ICaptionsGridDefaults.DefaultDurationVisible;
    private bool captionVisible = ICaptionsGridDefaults.DefaultCaptionVisible;

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

    #endregion
  }

}
