//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityView.cs
//Version: 20121106

using System;
using System.Runtime.Serialization;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")]
  public class ActivityView: BaseView, IActivity
  {
    public ActivityView()
    {
      //BaseView defaults - overrides
      Title = IActivityDefaults.DefaultTitle;
    }
        
    #region Fields

    //can set fields directly here or at constructor
    private Uri source = IActivityDefaults.DefaultSource;
    private TimeSpan time = IActivityDefaults.DefaultTime;
    private Point viewPosition = IActivityDefaults.DefaultViewPosition;
    private double viewWidth = IActivityDefaults.DefaultViewWidth;
    private double viewHeight = IActivityDefaults.DefaultViewHeight;
    private double contentZoom = IActivityDefaults.DefaultContentZoom;
    private bool contentZoomable = IActivityDefaults.DefaultContentZoomable;
    private bool contentPartsConfigurable = IActivityDefaults.DefaultContentPartsConfigurable;

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
          RaisePropertyChanged(IActivityProperties.PropertySource);
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
          RaisePropertyChanged(IActivityProperties.PropertyTime);
        }
      }
    }

    [DataMember]
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

    #endregion

  }

}
