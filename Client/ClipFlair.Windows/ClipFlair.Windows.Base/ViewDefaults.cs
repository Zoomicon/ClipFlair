﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ViewDefaults.cs
//Version: 20131120

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class ViewDefaults
  {
    public const bool DefaultDirty = false;
    public const bool DefaultBusy = false;
    public const Uri DefaultOptionsSource = null;
    //public const string DefaultID = ""; //there is no default ID, supposed to reset to a new unique id (e.g. a new GUID)
    public const string DefaultTitle = "";
    public const double DefaultX = 0;
    public const double DefaultY = 0;
    public static readonly Point DefaultPosition = new Point(DefaultX, DefaultY);
    public const double DefaultWidth = 300;
    public const double DefaultHeight = 300;
    public const double DefaultZoom = 1.0; //100% scale (zoom)
    public const double DefaultOpacity = 1.0; //opaque
    public const bool DefaultMoveable = true;
    public const bool DefaultResizable = true;
    public const bool DefaultZoomable = true;
    public const bool DefaultWarnOnClosing = true;

    #region Methods

    public static void SetDefaults(IView view)
    {
      //IView defaults
      view.Busy = DefaultBusy;
      view.OptionsSource = DefaultOptionsSource;
      view.ID = Guid.NewGuid().ToString(); //there is no default ID, resetting to a new unique id (using a GUID)
      view.Title = DefaultTitle;
      view.Position = DefaultPosition; //don't set view.X and view.Y, they're just accessors for X and Y components of Position property
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;
      view.Zoom = DefaultZoom;
      view.Opacity = DefaultOpacity;
      view.Moveable = DefaultMoveable;
      view.Resizable = DefaultResizable;
      view.Zoomable = DefaultZoomable;
      view.WarnOnClosing = DefaultWarnOnClosing;
      //
      view.Dirty = DefaultDirty; //must do last - this should be set again at the end of any overriden SetDefaults versions (at descendents)
    }

    #endregion

  }

}