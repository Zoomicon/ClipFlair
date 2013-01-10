//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMapViewer.cs
//Version: 20130110

using Microsoft.Maps.MapControl.Core;

using System;

namespace ClipFlair.Windows.Views
{

  public static class IMapViewerProperties
  {
    public const string PropertyMode = "Mode";
    public const string PropertyLabelsVisible = "LabelsVisible";
    public const string PropertyLabelsFading = "LabelsFading";
  }

  public interface IMapViewer : IView
  {
    MapMode ModeValue { get; set; }
    string Mode { get; set; }
    bool LabelsVisible { get; set; }
    bool LabelsFading { get; set; }
  }

}
