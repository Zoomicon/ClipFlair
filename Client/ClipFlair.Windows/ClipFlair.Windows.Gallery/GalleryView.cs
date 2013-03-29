//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryView.cs
//Version: 20130326

//TODO: maybe allow to load local Gallery and store it in options file (show Gallery from memorystream)

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class GalleryView: BaseView, IGalleryViewer
  {
    public GalleryView()
    {
    }

    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private Stretch stretch;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(GalleryViewerDefaults.DefaultSource)]
    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(IGalleryViewerProperties.PropertySource);
        }
      }
    }

    [DataMember]
    [DefaultValue(GalleryViewerDefaults.DefaultStretch)]
    public Stretch Stretch
    {
      get { return stretch; }
      set
      {
        if (value != stretch)
        {
          stretch = value;
          RaisePropertyChanged(IGalleryViewerProperties.PropertyStretch);
        }
      }
    }

    public string StretchText
    {
      get { return Stretch.ToString(); }
      set { Stretch = (Stretch)Enum.Parse(typeof(Stretch), value, true); }
    }

    #endregion

    #region Methods
  
    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      GalleryViewerDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
