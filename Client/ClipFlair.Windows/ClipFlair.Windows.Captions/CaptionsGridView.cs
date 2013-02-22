//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridView.cs
//Version: 20130122

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace="http://clipflair.net/Contracts/View")]
  public class CaptionsGridView: BaseView, ICaptionsGrid
  {
    public CaptionsGridView()
    {
    }

    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private TimeSpan time;
    private CaptionRegion captions;
    private bool toolbarVisible;
    private bool roleVisible;
    private bool startTimeVisible;
    private bool endTimeVisible;
    private bool durationVisible;
    private bool captionVisible;
    private bool wpmVisible;
    private bool audioVisible;
    private bool commentsVisible;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultSource)]
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
    //[DefaultValue(CaptionsGridDefaults.DefaultTime)] //can't use static fields here (we're forced to use static instead of const since we set a non-null default value)
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
    [DefaultValue(CaptionsGridDefaults.DefaultCaptions)]
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
          RaisePropertyChanged(ICaptionsGridProperties.PropertyCaptions);
        }
      }
    }

    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultToolbarVisible)]
    public bool ToolbarVisible
    {
      get { return toolbarVisible; }
      set
      {
        if (value != toolbarVisible)
        {
          toolbarVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyToolbarVisible);
        }
      }
    }

    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultStartTimeVisible)]
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
    [DefaultValue(CaptionsGridDefaults.DefaultEndTimeVisible)]
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
    [DefaultValue(CaptionsGridDefaults.DefaultDurationVisible)]

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
    [DefaultValue(CaptionsGridDefaults.DefaultRoleVisible)]
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
    [DefaultValue(CaptionsGridDefaults.DefaultCaptionVisible)]
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
    [DefaultValue(CaptionsGridDefaults.DefaultWPMVisible)]
    public bool WPMVisible
    {
      get { return wpmVisible; }
      set
      {
        wpmVisible = value;
        RaisePropertyChanged(ICaptionsGridProperties.PropertyWPMVisible);
      }
    }

    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultAudioVisible)]
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
    [DefaultValue(CaptionsGridDefaults.DefaultCommentsVisible)]
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
    {
      CaptionsGridDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
