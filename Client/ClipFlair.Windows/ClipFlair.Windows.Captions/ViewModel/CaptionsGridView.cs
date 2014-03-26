//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridView.cs
//Version: 20140326

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

    #region --- Fields ---

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private CaptionRegion captions;
    private bool toolbarVisible;
    private bool roleVisible;
    private bool startTimeVisible;
    private bool endTimeVisible;
    private bool durationVisible;
    private bool captionVisible;
    private bool rtlVisible;
    private bool cplVisible;
    private bool cpsVisible;
    private bool wpmVisible;
    private bool audioVisible;
    private bool commentsVisible;
    private bool saveInvisibleAudio;

    #endregion

    #region --- Properties ---

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
          Dirty = true;
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
          Dirty = true;
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
          Dirty = true;
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
          Dirty = true;
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
        if (value != durationVisible)
        {
          durationVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyDurationVisible);
          Dirty = true;
        }
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
          Dirty = true;
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
        if (value != captionVisible)
        {
          captionVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyCaptionVisible);
          Dirty = true;
        }
      }
    }


    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultRTLVisible)]
    public bool RTLVisible
    {
      get { return rtlVisible; }
      set
      {
        if (value != rtlVisible)
        {
          rtlVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyRTLVisible);
          Dirty = true;
        }
      }
    }


    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultCPLVisible)]
    public bool CPLVisible
    {
      get { return cplVisible; }
      set
      {
        if (value != cplVisible)
        {
          cplVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyCPLVisible);
          Dirty = true;
        }
      }
    }
    
    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultCPSVisible)]
    public bool CPSVisible
    {
      get { return cpsVisible; }
      set
      {
        if (value != cpsVisible)
        {
          cpsVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyCPSVisible);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultWPMVisible)]
    public bool WPMVisible
    {
      get { return wpmVisible; }
      set
      {
        if (value != wpmVisible)
        {
          wpmVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyWPMVisible);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultAudioVisible)]
    public bool AudioVisible
    {
      get { return audioVisible; }
      set
      {
        if (value != audioVisible)
        {
          audioVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyAudioVisible);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultCommentsVisible)]
    public bool CommentsVisible
    {
      get { return commentsVisible; }
      set
      {
        if (value != commentsVisible)
        {
          commentsVisible = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertyCommentsVisible);
          Dirty = true;
        }
      }
    }
    
    [DataMember]
    [DefaultValue(CaptionsGridDefaults.DefaultSaveInvisibleAudio)]
    public bool SaveInvisibleAudio
    {
      get { return saveInvisibleAudio; }
      set
      {
        if (value != saveInvisibleAudio)
        {
          saveInvisibleAudio = value;
          RaisePropertyChanged(ICaptionsGridProperties.PropertySaveInvisibleAudio);
          Dirty = true;
        }
      }
    }

    #endregion

    #region --- Methods ---

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      CaptionsGridDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
