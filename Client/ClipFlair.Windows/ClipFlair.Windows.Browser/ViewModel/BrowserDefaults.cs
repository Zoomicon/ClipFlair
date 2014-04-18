//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BrowserDefaults.cs
//Version: 20140418

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class BrowserDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Browser";
    public const double DefaultWidth = 800;
    public const double DefaultHeight = 600;
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0x4F, 0x60, 0xFF); //#4F60FF
          
    #endregion

    public const Uri DefaultSource = null;

    #region Methods

    public static void SetDefaults(IBrowser view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;
      view.BorderColor = DefaultBorderColor;

      //IBrowser defaults
      view.Source = DefaultSource;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}