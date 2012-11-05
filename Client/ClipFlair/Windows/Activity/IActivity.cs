//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivity.cs
//Version: 20121103

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IActivityProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyTime = "Time";
    public const string PropertyViewPosition = "ViewPosition";
    public const string PropertyViewWidth = "ViewWidth";
    public const string PropertyViewHeight = "ViewHeight";
    public const string PropertyContentZoom = "ViewContentZoom";
    public const string PropertyContentZoomable = "ViewContentZoomable";
    public const string PropertyContentPartsConfigurable = "ContentPartsConfigurable";

    //TODO: maybe also add WindowsMoveable/WindowsResizable/WindowZoomable to override child settings
  }

  public static class IActivityDefaults
  {
    public static Uri DefaultSource = null;
    public static TimeSpan DefaultTime = TimeSpan.Zero;
    public static Point DefaultViewPosition = new Point(0, 0);
    public static double DefaultViewWidth = 1000;
    public static double DefaultViewHeight = 700;
    public static double DefaultContentZoom = 1.0;
    public static bool DefaultContentZoomable = true;
    public static bool DefaultContentPartsConfigurable = true;
  }
  
  public interface IActivity: IView
  {
    Uri Source { get; set; }
    TimeSpan Time { get; set; }
    Point ViewPosition { get; set; }
    double ViewWidth { get; set; }
    double ViewHeight { get; set; }
    double ContentZoom { get; set; }
    bool ContentZoomable { get; set; }
    bool ContentPartsConfigurable { get; set; }
  }

}
