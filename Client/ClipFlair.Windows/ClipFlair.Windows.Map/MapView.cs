//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapView.cs
//Version: 20121218

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
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

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    { //Must set property values, not fields

      //BaseView defaults and overrides
      base.SetDefaults();
      Title = IMapViewerDefaults.DefaultTitle;

      //MapView defaults
      Mode = IMapViewerDefaults.DefaultMode;
    }

   #endregion

  }

}
