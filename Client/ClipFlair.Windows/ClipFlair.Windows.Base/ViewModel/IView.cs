//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IView.cs
//Version: 20140324

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public interface IView: INotifyPropertyChanged
  {
    bool Busy { get; set; }

    TimeSpan Time { get; set; } //20131205
    Uri OptionsSource { get; set; }
    string ID { get; set; }
    string Title { get; set; }
    Point Position { get; set; }
    double X { get; set; } //20131120
    double Y { get; set; } //20131120
    double Width { get; set; }
    double Height { get; set; }
    double Zoom { get; set; }
    int ZIndex { get; set; } //20131206
    double Opacity { get; set; }
    Color TitleColor { get; set; } //20140324
    Color BackgroundColor { get; set; } //20131216
    Color BorderColor { get; set; } //20131213
    Thickness BorderThickness { get; set; } //20131213
    CornerRadius CornerRadius { get; set; } //20131213
    bool Moveable { get; set; }
    bool Resizable { get; set; }
    bool Zoomable { get; set; }
    bool WarnOnClosing { get; set; }
    bool RTL { get; set; } //20131205

    bool Dirty { get; set; } //20131120
  }

}
