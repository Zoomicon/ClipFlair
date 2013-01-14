//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMapViewer.cs
//Version: 20130114

using Microsoft.Maps.MapControl.Core;

using System;

namespace ClipFlair.Windows.Views
{

  public static class IMapViewerProperties
  {
    public const string PropertyInertia = "Inertia";
    public const string PropertyNavigationVisible = "NavigationVisible";
    public const string PropertyScaleVisible = "ScaleVisible";
    public const string PropertyCulture = "Culture"; //e.g. "en-us"
    public const string PropertyMode = "Mode"; //Road, Aerial
    public const string PropertyLabelsVisible = "LabelsVisible";
    public const string PropertyLabelsFading = "LabelsFading";
    public const string PropertyMapZoom = "MapZoom";
  }

  public interface IMapViewer : IView
  {
    bool Inertia { get; set; }
    bool NavigationVisible { get; set; }
    bool ScaleVisible { get; set; }
    string Culture { get; set; }
    MapMode ModeValue { get; set; }
    string Mode { get; set; }
    bool LabelsVisible { get; set; }
    bool LabelsFading { get; set; }
    double MapZoom { get; set; }
  }

}
