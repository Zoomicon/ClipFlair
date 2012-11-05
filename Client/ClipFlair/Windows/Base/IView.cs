//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IView.cs
//Version: 20121102

using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IViewProperties
  {
    public const string PropertyTitle = "Title";
    public const string PropertyPosition = "Position";
    public const string PropertyWidth = "Width";
    public const string PropertyHeight = "Height";
    public const string PropertyZoom = "Zoom";
    public const string PropertyMoveable = "Moveable";
    public const string PropertyResizable = "Resizable";
    public const string PropertyZoomable = "Zoomable";
  }

  public static class IViewDefaults
  {
    public static string DefaultTitle = "";
    public static Point DefaultPosition = new Point(0, 0);
    public static double DefaultWidth = 0;
    public static double DefaultHeight = 0;
    public static double DefaultZoom = 1.0;
    public static bool DefaultMoveable = true;
    public static bool DefaultResizable = true;
    public static bool DefaultZoomable = true;
  }

  public interface IView: INotifyPropertyChanged
  {
    string Title { get; set; }
    Point Position { get; set; }
    double Width { get; set; }
    double Height { get; set; }
    double Zoom { get; set; }
    bool Moveable { get; set; }
    bool Resizable { get; set; }
    bool Zoomable { get; set; }
  }

}
