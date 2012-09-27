//Filename: IView.cs
//Version: 20120927

using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Models.Views
{

  public static class IViewProperties
  {
    public const string PropertyPosition = "Position";
    public const string PropertyWidth = "Width";
    public const string PropertyHeight = "Height";
  }

  public static class IViewDefaults
  {
    public static Point DefaultPosition = new Point(0, 0);
    public static double DefaultWidth = 0;
    public static double DefaultHeight = 0;
  }

  public interface IView: INotifyPropertyChanged
  {
    Point Position { get; set; }
    double Width { get; set; }
    double Height { get; set; }
  }

}
