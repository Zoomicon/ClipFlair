//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageView.cs
//Version: 20121206

//TODO: maybe allow to load local image and store it in .clipflair.zip file (show image from memorystream)

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class ImageView: BaseView, IImageViewer
  {
    public ImageView()
    {
    }

    #region Fields

    //fields are initialized at "SetDefaults" method
    private Uri source;
    private Stretch stretch;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(IImageViewerDefaults.DefaultSource)]
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
    [DefaultValue(IImageViewerDefaults.DefaultStretch)]
    public Stretch Stretch
    {
      get { return stretch; }
      set
      {
        if (value != stretch)
        {
          stretch = value;
          RaisePropertyChanged(IImageViewerProperties.PropertyStretch);
        }
      }
    }

    #endregion

    #region Methods

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    { //Must set property values, not fields

      //BaseView defaults and overrides
      base.SetDefaults();
      Title = IImageViewerDefaults.DefaultTitle;

      //ImageView defaults
      Source = IImageViewerDefaults.DefaultSource;
      Stretch = IImageViewerDefaults.DefaultStretch;
    }

    #endregion

  }

}
