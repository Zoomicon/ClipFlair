//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridView.cs
//Version: 20121206

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.ComponentModel;
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

    //fields are initialized at "SetDefaults" method
    private Uri source;
    private TimeSpan time;
    private CaptionRegion captions;
    private bool roleVisible;
    private bool startTimeVisible;
    private bool endTimeVisible;
    private bool durationVisible;
    private bool captionVisible;
    private bool audioVisible;
    private bool commentsVisible;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(ICaptionsGridDefaults.DefaultSource)]
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
    //[DefaultValue(ICaptionsGridDefaults.DefaultTime)] //can't use static fields here (we're forced to use static instead of const since we set a non-null default value)
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

    //don't make this a DataMember (storing separately)
    [DefaultValue(ICaptionsGridDefaults.DefaultCaptions)]
    public CaptionRegion Captions
    {
      get { return captions; }
      set
      {
        if (value != captions)
        {
          captions = value ?? new CaptionRegion(); //if null create a new CaptionRegion
          RaisePropertyChanged(ICaptionsGridProperties.PropertyCaptions);
        }
      }
    }

    [DataMember]
    [DefaultValue(ICaptionsGridDefaults.DefaultStartTimeVisible)]
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
    [DefaultValue(ICaptionsGridDefaults.DefaultEndTimeVisible)]
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
    [DefaultValue(ICaptionsGridDefaults.DefaultDurationVisible)]

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
    [DefaultValue(ICaptionsGridDefaults.DefaultRoleVisible)]
    public bool RoleVisible
    {
      get { return roleVisible; }
      set
      {
        if (value != roleVisible)
        {
          roleVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyRoleVisible);
        }
      }
    }
    
    [DataMember]
    [DefaultValue(ICaptionsGridDefaults.DefaultCaptionVisible)]
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
    [DefaultValue(ICaptionsGridDefaults.DefaultAudioVisible)]
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
    [DefaultValue(ICaptionsGridDefaults.DefaultCommentsVisible)]
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

    #region Methods

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    { //Must set property values, not fields

      //BaseView defaults and overrides
      base.SetDefaults();
      Title = ICaptionsGridDefaults.DefaultTitle;

      //CaptionsGridView defaults
      Source = ICaptionsGridDefaults.DefaultSource;
      Time = ICaptionsGridDefaults.DefaultTime;
      Captions = ICaptionsGridDefaults.DefaultCaptions;
      RoleVisible = ICaptionsGridDefaults.DefaultRoleVisible;
      StartTimeVisible = ICaptionsGridDefaults.DefaultStartTimeVisible;
      EndTimeVisible = ICaptionsGridDefaults.DefaultEndTimeVisible;
      DurationVisible = ICaptionsGridDefaults.DefaultDurationVisible;
      CaptionVisible = ICaptionsGridDefaults.DefaultCaptionVisible;
      AudioVisible = ICaptionsGridDefaults.DefaultAudioVisible;
      CommentsVisible = ICaptionsGridDefaults.DefaultCommentsVisible;
    }

    #endregion

  }

}
