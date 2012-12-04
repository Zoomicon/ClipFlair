//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapView.cs
//Version: 20121203

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class MapView: BaseView, IMapViewer
  {
    public MapView()
    {
    }

    #region Fields

    //fields are initialized at "SetDefaults" method
    private string mode;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(IMapViewerDefaults.DefaultMode)]
    public string Mode
    {
      get { return mode; }
      set
      {
        if (value != mode)
        {
          mode = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyMode);
        }
      }
    }

    #endregion

    #region Methods

    public override void SetDefaults() //do not all at constructor, BaseView does it already
    {
      //BaseView defaults and overrides
      base.SetDefaults();
      Title = IMapViewerDefaults.DefaultTitle;

      //MapView defaults
      mode = IMapViewerDefaults.DefaultMode;
    }

   #endregion

  }

}
