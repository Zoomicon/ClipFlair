//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMapViewerDefaults.cs
//Version: 20130110

using Microsoft.Maps.MapControl.Core;

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IMapViewerDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Map";
    
    #endregion

    public const string DefaultMode = "Aerial";
    public const bool DefaultLabelsVisible = true;
    public const bool DefaultLabelsFading = false;
  }

}