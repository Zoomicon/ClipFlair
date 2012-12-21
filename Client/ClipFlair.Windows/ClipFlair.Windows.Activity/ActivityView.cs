//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityView.cs
//Version: 20121219

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

    //fields are initialized at "SetDefaults" method
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
    [DefaultValue(IActivityDefaults.DefaultSource)]
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
    //[DefaultValue(IActivityDefaults.DefaultTime)]
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
    [DefaultValue(IActivityDefaults.DefaultCaptions)]
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
    //[DefaultValue(IActivityDefaults.DefaultViewPosition)]
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
    [DefaultValue(IActivityDefaults.DefaultViewWidth)]
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
    [DefaultValue(IActivityDefaults.DefaultViewHeight)]
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
    [DefaultValue(IActivityDefaults.DefaultContentZoom)]
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
    [DefaultValue(IActivityDefaults.DefaultContentZoomable)]
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
    [DefaultValue(IActivityDefaults.DefaultContentPartsConfigurable)]
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
    [DefaultValue(ITextEditorDefaults.DefaultToolbarVisible)]
    public bool ToolbarVisible
    {
      get { return toolbarVisible; }
      set
      {
        if (value != toolbarVisible)
        {
          toolbarVisible = value;
          RaisePropertyChanged(ITextEditorProperties.PropertyToolbarVisible);
        }
      }
    }

    #endregion

    #region Methods

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    { //Must set property values, not fields

      //BaseView defaults and overrides
      base.SetDefaults();
      Title = IActivityDefaults.DefaultTitle;

      //ActivityView defaults
      Source = IActivityDefaults.DefaultSource;
      Time = IActivityDefaults.DefaultTime;
      Captions = IActivityDefaults.DefaultCaptions;
      ViewPosition = IActivityDefaults.DefaultViewPosition;
      ViewWidth = IActivityDefaults.DefaultViewWidth;
      ViewHeight = IActivityDefaults.DefaultViewHeight;
      ContentZoom = IActivityDefaults.DefaultContentZoom;
      ContentZoomable = IActivityDefaults.DefaultContentZoomable;
      ContentPartsConfigurable = IActivityDefaults.DefaultContentPartsConfigurable;
      ToolbarVisible = IActivityDefaults.DefaultToolbarVisible;
    }

    #endregion


  }

}
