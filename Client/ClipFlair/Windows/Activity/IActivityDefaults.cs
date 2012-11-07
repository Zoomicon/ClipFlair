//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivityDefaults.cs
//Version: 20121106

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class IActivityDefaults
  {

    #region IView defaults - overrides
    
    public static string DefaultTitle = "Activity";
    
    #endregion
    
    public static Uri DefaultSource = null;
    public static TimeSpan DefaultTime = TimeSpan.Zero;
    public static Point DefaultViewPosition = new Point(0, 0);
    public static double DefaultViewWidth = 1000;
    public static double DefaultViewHeight = 700;
    public static double DefaultContentZoom = 1.0;
    public static bool DefaultContentZoomable = true;
    public static bool DefaultContentPartsConfigurable = true;
  }

}