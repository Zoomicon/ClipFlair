//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageView.cs
//Version: 20141107

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class ImageView: BaseView, IImageViewer
  {
    public ImageView()
    {
    }

    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private bool cameraSourceUsed;
    private bool contentZoomToFit;
    private Uri actionURL;
    private TimeSpan? actionTime;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(ImageViewerDefaults.DefaultSource)]
    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(IImageViewerProperties.PropertySource);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(ImageViewerDefaults.DefaultCameraSourceUsed)]
    public bool CameraSourceUsed
    {
      get { return cameraSourceUsed; }
      set
      {
        if (value != cameraSourceUsed)
        {
          cameraSourceUsed = value;
          RaisePropertyChanged(IImageViewerProperties.PropertyCameraSourceUsed);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(ImageViewerDefaults.DefaultContentZoomToFit)]
    public bool ContentZoomToFit
    {
      get { return contentZoomToFit; }
      set
      {
        if (value != contentZoomToFit)
        {
          contentZoomToFit = value;
          RaisePropertyChanged(IImageViewerProperties.PropertyContentZoomToFit);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(ImageViewerDefaults.DefaultActionURL)]
    public Uri ActionURL
    {
      get { return actionURL; }
      set
      {
        if (value != actionURL)
        {
          actionURL = value;
          RaisePropertyChanged(IImageViewerProperties.PropertyActionURL);
          Dirty = true;
        }
      }
    }

    [DataMember]
    //[DefaultValue(ImageViewerDefaults.DefaultActionTime)] //can't use static fields here (and we're forced to use one for TimeSpan unfortunately, doesn't work with const)
    public TimeSpan? ActionTime
    {
      get { return actionTime; }
      set
      {
        if (value != actionTime)
        {
          actionTime = value;
          RaisePropertyChanged(IImageViewerProperties.PropertyActionTime);
          Dirty = true;
        }
      }
    }

    #endregion

    #region Methods
  
    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      ImageViewerDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
