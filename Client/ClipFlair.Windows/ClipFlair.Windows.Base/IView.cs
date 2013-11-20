//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IView.cs
//Version: 20131120

using System;
using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public interface IView: INotifyPropertyChanged
  {
    bool Dirty { get; set; } //20131120
    bool Busy { get; set; }
    Uri OptionsSource { get; set; }
    string ID { get; set; }
    string Title { get; set; }
    Point Position { get; set; }
    double X { get; set; } //20131120
    double Y { get; set; } //20131120
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
