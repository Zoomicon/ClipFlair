//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMapViewer.cs
//Version: 20131205

using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.Core;


namespace ClipFlair.Windows.Views
{

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
    double Latitude { get; set; }
    double Longitude { get; set; }
    Location MapCenter { get; set; }
    double MapZoom { get; set; }
  }

}
