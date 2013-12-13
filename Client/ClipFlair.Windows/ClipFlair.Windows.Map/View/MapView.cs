//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapView.cs
//Version: 20131213

using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.Core;

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

    //fields are initialized via respective properties at "SetDefaults" method
    private bool inertia;
    private bool navigationVisible;
    private bool scaleVisible;
    private string culture;
    private string mode;
    private bool labelsVisible;
    private bool labelsFading;
    private Location mapCenter;
    private double mapZoom;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(MapViewerDefaults.DefaultInertia)]
    public bool Inertia
    {
      get { return inertia; }
      set
      {
        if (value != inertia)
        {
          inertia = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyInertia);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(MapViewerDefaults.DefaultNavigationVisible)]
    public bool NavigationVisible
    {
      get { return navigationVisible; }
      set
      {
        if (value != navigationVisible)
        {
          navigationVisible = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyNavigationVisible);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(MapViewerDefaults.DefaultScaleVisible)]
    public bool ScaleVisible
    {
      get { return scaleVisible; }
      set
      {
        if (value != scaleVisible)
        {
          scaleVisible = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyScaleVisible);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(MapViewerDefaults.DefaultCulture)]
    public string Culture
    {
      get { return culture; }
      set
      {
        if (value != culture)
        {
          culture = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyCulture);
          Dirty = true;
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
    [DefaultValue(MapViewerDefaults.DefaultMode)]
    public string Mode
    {
      get { return mode; }
      set
      {
        if (value != mode)
        {
          mode = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyMode);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(MapViewerDefaults.DefaultLabelsVisible)]
    public bool LabelsVisible
    {
      get { return labelsVisible; }
      set
      {
        if (value != labelsVisible)
        {
          labelsVisible = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyLabelsVisible);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(MapViewerDefaults.DefaultLabelsFading)]
    public bool LabelsFading
    {
      get { return labelsFading; }
      set
      {
        if (value != labelsFading)
        {
          labelsFading = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyLabelsFading);
          Dirty = true;
        }
      }
    }

    [DefaultValue(MapViewerDefaults.DefaultLatitude)]
    public double Latitude
    {
      get { return mapCenter.Latitude; }
      set
      {
        if (value != mapCenter.Latitude)
          MapCenter = new Location(mapCenter) { Latitude = value };
      }
    }

    [DefaultValue(MapViewerDefaults.DefaultLongitude)]
    public double Longitude
    {
      get { return mapCenter.Longitude; }
      set
      {
        if (value != mapCenter.Longitude)
          MapCenter = new Location(mapCenter) { Longitude = value };
      }
    }

    [DefaultValue(MapViewerDefaults.DefaultAltitude)]
    public double Altitude
    {
      get { return mapCenter.Altitude; }
      set
      {
        if (value != mapCenter.Altitude)
          MapCenter = new Location(mapCenter) { Altitude = value };
      }
    }

    [DataMember]
    public Location MapCenter
    {
      get { return mapCenter; }
      set
      {
        if (value != mapCenter)
        {
          bool latitudeChanged = (mapCenter == null) || (value == null) || (mapCenter.Latitude != value.Latitude);
          bool longitudeChanged = (mapCenter == null) || (value == null) || (mapCenter.Longitude != value.Longitude);
          bool altitudeChanged = (mapCenter == null) || (value == null) || (mapCenter.Altitude != value.Altitude);

          mapCenter = value;

          RaisePropertyChanged(IMapViewerProperties.PropertyMapCenter);
          if (latitudeChanged) RaisePropertyChanged(IMapViewerProperties.PropertyLatitude);
          if (longitudeChanged) RaisePropertyChanged(IMapViewerProperties.PropertyLongitude);
          if (altitudeChanged) RaisePropertyChanged(IMapViewerProperties.PropertyAltitude);

          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(MapViewerDefaults.DefaultMapZoom)]
    public double MapZoom
    {
      get { return mapZoom; }
      set
      {
        if (value != mapZoom)
        {
          mapZoom = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyMapZoom);
          Dirty = true;
        }
      }
    }

    #endregion

    #region Methods

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      MapViewerDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

   #endregion

  }

}
