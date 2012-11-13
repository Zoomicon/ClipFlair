//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IViewDefaults.cs
//Version: 20121113

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IViewDefaults
  {
    public static string DefaultTitle = "";
    public static Point DefaultPosition = new Point(0, 0);
    public static double DefaultWidth = 300;
    public static double DefaultHeight = 300;
    public static double DefaultZoom = 1.0; //100%
    public static double DefaultOpacity = 1.0; //opaque
    public static bool DefaultMoveable = true;
    public static bool DefaultResizable = true;
    public static bool DefaultZoomable = true;
  }

}