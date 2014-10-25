//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ViewDefaults.cs
//Version: 20141025

using System;
using System.Windows;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class ViewDefaults
  {
    public const bool DefaultBusy = false;
    //
    public static readonly TimeSpan DefaultTime = TimeSpan.Zero;
    public const Uri DefaultOptionsSource = null;
    //public const string DefaultID = ""; //there is no default ID, supposed to reset to a new unique id (e.g. a new GUID)
    public const string DefaultTitle = "";
    public const double DefaultX = 0;
    public const double DefaultY = 0;
    public static readonly Point DefaultPosition = new Point(DefaultX, DefaultY);
    public const double DefaultWidth = 300;
    public const double DefaultHeight = 300;
    public const double DefaultZoom = 1.0; //100% scale (zoom)
    public const int DefaultZIndex = 0;
    public const double DefaultOpacity = 1.0; //opaque
    public static readonly Color DefaultBackgroundColor = Color.FromArgb(0xFF, 0xF3, 0xF3, 0xF3); //#F3F3F3
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0x21, 0x71, 0xD2); //#2171D2
    public static readonly Color DefaultTitleForegroundColor = Color.FromArgb(0xFF, 0xE6, 0xE5, 0xEA); //#E6E5EA
    public static readonly Color? DefaultTitleBackgroundColor = null; //Note: special value that will be replaced by border color after loading (for compatibility reasons with older saved data)
    public static readonly Thickness DefaultBorderThickness = new Thickness(3);
    public static readonly CornerRadius DefaultCornerRadius = new CornerRadius(0);
    public const bool DefaultMoveable = true;
    public const bool DefaultResizable = true;
    public const bool DefaultZoomable = true;
    public const bool DefaultWarnOnClosing = true;
    public const bool DefaultRTL = false;
    public const bool DefaultTitlebarVisible = true;
    //
    public const bool DefaultDirty = false;

    #region Methods

    public static void SetDefaults(IView view)
    {
      //IView defaults
      view.Busy = DefaultBusy;
      
      view.Time = DefaultTime;
      view.OptionsSource = DefaultOptionsSource;
      view.ID = Guid.NewGuid().ToString(); //there is no default ID, resetting to a new unique id (using a GUID)
      view.Title = DefaultTitle;
      view.Position = DefaultPosition; //don't set view.X and view.Y, they're just accessors for X and Y components of Position property
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;
      view.Zoom = DefaultZoom;
      view.ZIndex = DefaultZIndex;
      view.Opacity = DefaultOpacity;
      view.TitleForegroundColor = DefaultTitleForegroundColor;
      view.TitleBackgroundColor = DefaultTitleBackgroundColor;
      view.BackgroundColor = DefaultBackgroundColor;
      view.BorderColor = DefaultBorderColor;
      view.BorderThickness = DefaultBorderThickness;
      view.CornerRadius = DefaultCornerRadius;
      view.Moveable = DefaultMoveable;
      view.Resizable = DefaultResizable;
      view.Zoomable = DefaultZoomable;
      view.WarnOnClosing = DefaultWarnOnClosing;
      view.RTL = DefaultRTL;
      view.TitlebarVisible = DefaultTitlebarVisible;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}