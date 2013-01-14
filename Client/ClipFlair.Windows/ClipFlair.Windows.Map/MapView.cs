//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapView.cs
//Version: 20130114

using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.Core;

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
    private bool navigationVisible;
    private string culture;
    private string mode;
    private bool labelsVisible;
    private bool labelsFading;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(IMapViewerDefaults.DefaultNavigationVisible)]
    public bool NavigationVisible
    {
      get { return navigationVisible; }
      set
      {
        if (value != navigationVisible)
        {
          navigationVisible = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyNavigationVisible);
        }
      }
    }

    [DataMember]
    [DefaultValue(IMapViewerDefaults.DefaultCulture)]
    public string Culture
    {
      get { return culture; }
      set
      {
        if (value != culture)
        {
          culture = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyCulture);
        }
      }
    }

    public MapMode ModeValue
    {
      get {
        switch (Mode)
        {
          case "Road":
            return new RoadMode();
          case "Aerial":
            AerialMode m = new AerialMode(LabelsVisible);
            m.FadingLabels = LabelsFading;
            return m;
          default:
            return null;
        }
      }
      set
      {
        if (value is RoadMode)
          Mode = "Road"; //raises PropertyChanged event
        else if (value is AerialMode)
        {
          Mode = "Aerial"; //raises PropertyChanged event
          AerialMode m = (AerialMode)value;
          LabelsVisible = m.Labels; //raises PropertyChanged event
          LabelsFading = m.FadingLabels; //raises PropertyChanged event
        }
        else
          Mode = "";
      }
    }

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

    [DataMember]
    [DefaultValue(IMapViewerDefaults.DefaultLabelsVisible)]
    public bool LabelsVisible
    {
      get { return labelsVisible; }
      set
      {
        if (value != labelsVisible)
        {
          labelsVisible = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyLabelsVisible);
        }
      }
    }

    [DataMember]
    [DefaultValue(IMapViewerDefaults.DefaultLabelsFading)]
    public bool LabelsFading
    {
      get { return labelsFading; }
      set
      {
        if (value != labelsFading)
        {
          labelsFading = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyLabelsFading);
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
      NavigationVisible = IMapViewerDefaults.DefaultNavigationVisible;
      Culture = IMapViewerDefaults.DefaultCulture;
      Mode = IMapViewerDefaults.DefaultMode;
      LabelsVisible = IMapViewerDefaults.DefaultLabelsVisible;
      LabelsFading = IMapViewerDefaults.DefaultLabelsFading;
    }

   #endregion

  }

}
