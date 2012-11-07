//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IView.cs
//Version: 20121106

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
