//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: NewsReaderDefaults.cs
//Version: 20140311

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class NewsReaderDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "News";
    public const double DefaultWidth = 400;
    public const double DefaultHeight = 400;
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0xE4, 0xCA, 0x37); //#E4CA37
      //Color.FromArgb(0xFF, 0xE4, 0xB5, 0x03); //#E4B503
      //Color.FromArgb(0xFF, 0xE7, 0xB6, 0x00); //#E7B600
      //Color.FromArgb(0xFF, 0xEC, 0x46, 0x14); //#EC4614
        
    #endregion

    public const Uri DefaultSource = null;

    #region Methods

    public static void SetDefaults(INewsReader view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;
      view.BorderColor = DefaultBorderColor;

      //INewsReader defaults
      view.Source = DefaultSource;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}