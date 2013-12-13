//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapViewerDefaults.cs
//Version: 20131213

using Microsoft.Maps.MapControl;

using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class MapViewerDefaults
  {
    #region IView defaults - overrides

    public const string DefaultTitle = "Map";
    public const double DefaultWidth = 490;
    public const double DefaultHeight = 375;
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0xB5, 0xBE, 0x21); //#B5BE21

    #endregion

    public const bool DefaultInertia = true;
    public const bool DefaultNavigationVisible = true;
    public const bool DefaultScaleVisible = true;
    public const string DefaultCulture = "en-us";
    public const string DefaultMode = "Aerial";
    public const bool DefaultLabelsVisible = true;
    public const bool DefaultLabelsFading = false;
    public const double DefaultLatitude = 0;
    public const double DefaultLongitude = 0;
    public const double DefaultAltitude = 0;
    public static Location DefaultMapCenter = new Location(DefaultLatitude, DefaultLongitude, DefaultAltitude);
    public const double DefaultMapZoom = 1.0;
 
    #region Methods

    public static void SetDefaults(IMapViewer view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;
      view.BorderColor = DefaultBorderColor;

      //IMapViewer defaults
      view.Inertia = DefaultInertia;
      view.NavigationVisible = DefaultNavigationVisible;
      view.ScaleVisible = DefaultScaleVisible;
      view.Culture = DefaultCulture;
      view.Mode = DefaultMode;
      view.LabelsVisible = DefaultLabelsVisible;
      view.LabelsFading = DefaultLabelsFading;
      view.MapCenter = DefaultMapCenter;
      view.MapZoom = DefaultMapZoom;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}