//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityView.cs
//Version: 20140903

//TODO: add "Inertia" property

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

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
    private CaptionRegion captions;
    private Point viewPosition;
    private double viewWidth;
    private double viewHeight;
    private double contentZoom;
    private bool contentZoomable;
    private bool contentZoomToFit;
    private bool contentPartsConfigurable;
    private bool iconbarVisible;
    private bool toolbarVisible;
    private Orientation toolbarOrientation;

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
          Dirty = true;
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
          //Dirty = true; //not considering panning arround to be an editing action
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
          //Dirty = true; //not considering zooming in-out to be an editing action //NOTE: THIS PROPERTY IS CURRENTLY READ-ONLY IN THE UI
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
          //Dirty = true; //not considering zooming in-out to be an editing action //NOTE: THIS PROPERTY IS CURRENTLY READ-ONLY IN THE UI
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
          if (!ContentZoomToFit)
            Dirty = true; //not considering zooming in-out to be an editing action while ContentZoomToFit is enabled
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
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultContentZoomToFit)]
    public bool ContentZoomToFit
    {
      get { return contentZoomToFit; }
      set
      {
        if (value != contentZoomToFit)
        {
          contentZoomToFit = value;
          RaisePropertyChanged(IActivityProperties.PropertyContentZoomToFit);
          Dirty = true;
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
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultIconbarVisible)]
    public bool IconbarVisible
    {
      get { return iconbarVisible; }
      set
      {
        if (value != iconbarVisible)
        {
          iconbarVisible = value;
          RaisePropertyChanged(IActivityProperties.PropertyIconbarVisible);
          Dirty = true;
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
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(ActivityDefaults.DefaultToolbarOrientation)]
    public Orientation ToolbarOrientation
    {
      get { return toolbarOrientation; }
      set
      {
        if (value != toolbarOrientation)
        {
          toolbarOrientation = value;
          RaisePropertyChanged(IActivityProperties.PropertyToolbarOrientation);
          Dirty = true;
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
