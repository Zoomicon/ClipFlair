//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapView.cs
//Version: 20130115

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
    [DefaultValue(IMapViewerDefaults.DefaultInertia)]
    public bool Inertia
    {
      get { return inertia; }
      set
      {
        if (value != inertia)
        {
          inertia = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyInertia);
        }
      }
    }

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
    [DefaultValue(IMapViewerDefaults.DefaultScaleVisible)]
    public bool ScaleVisible
    {
      get { return scaleVisible; }
      set
      {
        if (value != scaleVisible)
        {
          scaleVisible = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyScaleVisible);
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

    [DefaultValue(IMapViewerDefaults.DefaultLatitude)]
    public double Latitude
    {
      get { return mapCenter.Latitude; }
      set
      {
        if (value != mapCenter.Latitude)
          MapCenter = new Location(mapCenter) { Latitude = value };
      }
    }

    [DefaultValue(IMapViewerDefaults.DefaultLongitude)]
    public double Longitude
    {
      get { return mapCenter.Longitude; }
      set
      {
        if (value != mapCenter.Longitude)
          MapCenter = new Location(mapCenter) { Longitude = value };
      }
    }

    [DefaultValue(IMapViewerDefaults.DefaultAltitude)]
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
        }
      }
    }

    [DataMember]
    [DefaultValue(IMapViewerDefaults.DefaultMapZoom)]
    public double MapZoom
    {
      get { return mapZoom; }
      set
      {
        if (value != mapZoom)
        {
          mapZoom = value;
          RaisePropertyChanged(IMapViewerProperties.PropertyMapZoom);
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
      Width = IMapViewerDefaults.DefaultWidth;
      Height = IMapViewerDefaults.DefaultHeight;

      //MapView defaults
      Inertia = IMapViewerDefaults.DefaultInertia;
      NavigationVisible = IMapViewerDefaults.DefaultNavigationVisible;
      ScaleVisible = IMapViewerDefaults.DefaultScaleVisible;
      Culture = IMapViewerDefaults.DefaultCulture;
      Mode = IMapViewerDefaults.DefaultMode;
      LabelsVisible = IMapViewerDefaults.DefaultLabelsVisible;
      LabelsFading = IMapViewerDefaults.DefaultLabelsFading;
      MapCenter = IMapViewerDefaults.DefaultMapCenter;
      MapZoom = IMapViewerDefaults.DefaultMapZoom;
    }

   #endregion

  }

}
