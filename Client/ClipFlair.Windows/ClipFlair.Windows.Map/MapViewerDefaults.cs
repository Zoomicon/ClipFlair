//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapViewerDefaults.cs
//Version: 20130118

using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.Core;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class MapViewerDefaults
  {
    #region IView defaults - overrides

    public const string DefaultTitle = "Map";
    public const double DefaultWidth = 490;
    public const double DefaultHeight = 375;

    #endregion

    public const bool DefaultInertia = true;
    public const bool DefaultNavigationVisible = true;
    public const bool DefaultScaleVisible = true;
    public const string DefaultCulture = "en-us";
    public const string DefaultMode = "Aerial";
    public const bool DefaultLabelsVisible = true;
    public const bool DefaultLabelsFading = false;
    public const double DefaultLatitude = 0;
    public const double DefaultLongitude = 0;
    public const double DefaultAltitude = 0;
    public static Location DefaultMapCenter = new Location(DefaultLatitude, DefaultLongitude, DefaultAltitude);
    public const double DefaultMapZoom = 1.0;

    #region Methods

    public static void SetDefaults(IMapViewer map)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(map);
      map.Title = DefaultTitle;
      map.Width = DefaultWidth;
      map.Height = DefaultHeight;

      //IMapViewer defaults
      map.Inertia = DefaultInertia;
      map.NavigationVisible = DefaultNavigationVisible;
      map.ScaleVisible = DefaultScaleVisible;
      map.Culture = DefaultCulture;
      map.Mode = DefaultMode;
      map.LabelsVisible = DefaultLabelsVisible;
      map.LabelsFading = DefaultLabelsFading;
      map.MapCenter = DefaultMapCenter;
      map.MapZoom = DefaultMapZoom;
    }

    #endregion

  }

}