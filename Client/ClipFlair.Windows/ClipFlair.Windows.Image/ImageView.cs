//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageView.cs
//Version: 20130406

//TODO: maybe allow to load local image and store it in options file (show image from memorystream)

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;
using System.Windows.Media;

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
    private bool contentZoomToFit;

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
