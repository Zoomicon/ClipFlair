//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMapViewer.cs
//Version: 20130114

using Microsoft.Maps.MapControl.Core;

using System;

namespace ClipFlair.Windows.Views
{

  public static class IMapViewerProperties
  {
    public const string PropertyNavigationVisible = "NavigationVisible";
    public const string PropertyCulture = "Culture"; //e.g. "en-us"
    public const string PropertyMode = "Mode"; //Road, Aerial
    public const string PropertyLabelsVisible = "LabelsVisible";
    public const string PropertyLabelsFading = "LabelsFading";
  }

  public interface IMapViewer : IView
  {
    bool NavigationVisible { get; set; }
    string Culture { get; set; }
    MapMode ModeValue { get; set; }
    string Mode { get; set; }
    bool LabelsVisible { get; set; }
    bool LabelsFading { get; set; }
  }

}
