//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: NewsReaderDefaults.cs
//Version: 20141017

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
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0xEC, 0x46, 0x14); //#EC4614
        
    #endregion

    public const Uri DefaultSource = null;
    public static readonly TimeSpan DefaultRefreshInterval = new TimeSpan(0,15,0); //refresh news every 15' min

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
      view.RefreshInterval = DefaultRefreshInterval;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any SetDefaults method (at descendents)
    }

    #endregion

  }

}