//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryView.cs
//Version: 20130701

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class GalleryView: BaseView, IGallery
  {

    public GalleryView()
    {
    }

    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private string filter;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(GalleryDefaults.DefaultSource)]
    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(IGalleryProperties.PropertySource);
        }
      }
    }

    [DataMember]
    [DefaultValue(GalleryDefaults.DefaultFilter)]
    public string Filter
    {
      get { return filter; }
      set
      {
        if (value != filter)
        {
          filter = value;
          RaisePropertyChanged(IGalleryProperties.PropertyFilter);
        }
      }
    }

    #endregion

    #region Methods
  
    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      GalleryDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
