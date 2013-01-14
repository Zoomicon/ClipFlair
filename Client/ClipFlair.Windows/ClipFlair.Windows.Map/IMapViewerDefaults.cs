//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMapViewerDefaults.cs
//Version: 20130114

using Microsoft.Maps.MapControl.Core;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IMapViewerDefaults
  {
    #region IView defaults - overrides

    public const string DefaultTitle = "Map";
    public const double DefaultWidth = 490;
    public const double DefaultHeight = 375;

    #endregion

    public const bool DefaultInertia = true;
    public const bool DefaultNavigationVisible = true;
    public const bool DefaultScaleVisible = true;
    public const string DefaultCulture = "en-us";
    public const string DefaultMode = "Aerial";
    public const bool DefaultLabelsVisible = true;
    public const bool DefaultLabelsFading = false;
    public const double DefaultMapZoom = 1.0;
  }

}