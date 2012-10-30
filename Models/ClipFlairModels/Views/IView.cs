//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IView.cs
//Version: 20121030

using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Models.Views
{

  public static class IViewProperties
  {
    public const string PropertyTitle = "Title";
    public const string PropertyPosition = "Position";
    public const string PropertyScale = "Scale";
    public const string PropertyWidth = "Width";
    public const string PropertyHeight = "Height";
  }

  public static class IViewDefaults
  {
    public static string DefaultTitle = "";
    public static Point DefaultPosition = new Point(0, 0);
    public static double DefaultScale = 1.0;
    public static double DefaultWidth = 0;
    public static double DefaultHeight = 0;
  }

  public interface IView: INotifyPropertyChanged
  {
    string Title { get; set; }
    Point Position { get; set; }
    double Scale { get; set; }
    double Width { get; set; }
    double Height { get; set; }
  }

}
