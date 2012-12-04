//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IViewDefaults.cs
//Version: 20121129

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IViewDefaults
  {
    public const Uri DefaultOptionsSource = null;
    public const string DefaultTitle = "";
    public static readonly Point DefaultPosition = new Point(0, 0);
    public const double DefaultWidth = 300;
    public const double DefaultHeight = 300;
    public const double DefaultZoom = 1.0; //100%
    public const double DefaultOpacity = 1.0; //opaque
    public const bool DefaultMoveable = true;
    public const bool DefaultResizable = true;
    public const bool DefaultZoomable = true;
  }

}