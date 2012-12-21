//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IView.cs
//Version: 20121219

using System;
using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IViewProperties
  {
    public const string PropertyOptionsSource = "OptionsSource";
    public const string PropertyID = "ID";
    public const string PropertyTitle = "Title";
    public const string PropertyPosition = "Position";
    public const string PropertyWidth = "Width";
    public const string PropertyHeight = "Height";
    public const string PropertyZoom = "Zoom";
    public const string PropertyOpacity = "Opacity";
    public const string PropertyMoveable = "Moveable";
    public const string PropertyResizable = "Resizable";
    public const string PropertyZoomable = "Zoomable";
    public const string PropertyWarnOnClosing = "WarnOnClosing";
  }

  public interface IView: INotifyPropertyChanged
  {
    Uri OptionsSource { get; set; }
    string ID { get; set; }
    string Title { get; set; }
    Point Position { get; set; }
    double Width { get; set; }
    double Height { get; set; }
    double Zoom { get; set; }
    double Opacity { get; set; }
    bool Moveable { get; set; }
    bool Resizable { get; set; }
    bool Zoomable { get; set; }
    bool WarnOnClosing { get; set; }
  }

}
