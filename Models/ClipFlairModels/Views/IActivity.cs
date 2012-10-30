//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivity.cs
//Version: 20121030

using System;
using System.Windows;

namespace ClipFlair.Models.Views
{

  public static class IActivityProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyTime = "Time";

    public const String PropertyOffset = "Offset";
    public const String PropertyScale = "Scale";
  }

  public static class IActivityDefaults
  {
    public static Uri DefaultSource = null;
    public static TimeSpan DefaultTime = TimeSpan.Zero;

    public static Point DefaultOffset = new Point(0, 0);
    public static double DefaultScale = 1.0;
  }
  
  public interface IActivity: IView
  {
    Uri Source { get; set; }
    TimeSpan Time { get; set; }

    Point Offset { get; set; }
    double Scale { get; set; }
  }

}
