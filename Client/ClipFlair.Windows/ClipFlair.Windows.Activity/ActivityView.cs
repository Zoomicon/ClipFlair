//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityView.cs
//Version: 20130118

//TODO: add "Inertia" property

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class ActivityView: BaseView, IActivity
  {
    public ActivityView()
    {
    }
        
    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private TimeSpan time;
    private CaptionRegion captions;
    private Point viewPosition;
    private double viewWidth;
    private double viewHeight;
    private double contentZoom;
    private bool contentZoomable;
    private bool contentPartsConfigurable;
    private bool toolbarVisible;

    #endregion

    #region Properties
    
    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultSource)]
    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(IActivityProperties.PropertySource);
        }
      }
    }

    [DataMember]
    //[DefaultValue(ActivityDefaults.DefaultTime)]
    public TimeSpan Time
    {
      get { return time; }
      set
      {
        if (value != time)
        {
          time = value;
          RaisePropertyChanged(IActivityProperties.PropertyTime);
        }
      }
    }

    //don't make this a DataMember (not storing in Activity)
    [DefaultValue(ActivityDefaults.DefaultCaptions)]
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
    //[DefaultValue(ActivityDefaults.DefaultViewPosition)]
    public Point ViewPosition
    {
      get { return viewPosition; }
      set
      {
        if (value != viewPosition)
        {
          viewPosition = value;
          RaisePropertyChanged(IActivityProperties.PropertyViewPosition);
        }
      }
    }

    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultViewWidth)]
    public double ViewWidth
    {
      get { return viewWidth; }
      set
      {
        if (value != viewWidth)
        {
          viewWidth = value;
          RaisePropertyChanged(IActivityProperties.PropertyViewWidth);
        }
      }
    }
    
    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultViewHeight)]
    public double ViewHeight
    {
      get { return viewHeight; }
      set
      {
        if (value != viewHeight)
        {
          viewHeight = value;
          RaisePropertyChanged(IActivityProperties.PropertyViewHeight);
        }
      }
    }

    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultContentZoom)]
    public double ContentZoom
    {
      get { return contentZoom; }
      set
      {
        if (value != contentZoom)
        {
          contentZoom = value;
          RaisePropertyChanged(IActivityProperties.PropertyContentZoom);
        }
      }
    }

    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultContentZoomable)]
    public bool ContentZoomable
    {
      get { return contentZoomable; }
      set
      {
        if (value != contentZoomable)
        {
          contentZoomable = value;
          RaisePropertyChanged(IActivityProperties.PropertyContentZoomable);
        }
      }
    }

    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultContentPartsConfigurable)]
    public bool ContentPartsConfigurable
    {
      get { return contentPartsConfigurable; }
      set
      {
        if (value != contentPartsConfigurable)
        {
          contentPartsConfigurable = value;
          RaisePropertyChanged(IActivityProperties.PropertyContentPartsConfigurable);
        }
      }
    }

    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultToolbarVisible)]
    public bool ToolbarVisible
    {
      get { return toolbarVisible; }
      set
      {
        if (value != toolbarVisible)
        {
          toolbarVisible = value;
          RaisePropertyChanged(IActivityProperties.PropertyToolbarVisible);
        }
      }
    }

    #endregion

    #region Methods

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      ActivityDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
