//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorDefaults.cs
//Version: 20140907

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class TextEditorDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Text";
    public const double DefaultWidth = 500;
    public const double DefaultHeight = 500;
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0x09, 0x98, 0xA8 ); //#0998A8
      //Color.FromArgb(0xFF, 0x02, 0xAB, 0xBE); //#02ABBE

    #endregion

    public const Uri DefaultSource = null;
    public const bool DefaultToolbarVisible = true;
    public const bool DefaultEditable = true;

    #region Methods

    public static void SetDefaults(ITextEditor view)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(view);
      view.Title = DefaultTitle;
      view.Width = DefaultWidth;
      view.Height = DefaultHeight;
      view.BorderColor = DefaultBorderColor;

      //ITextview defaults
      view.Source = DefaultSource;
      view.ToolbarVisible = DefaultToolbarVisible;
      view.Editable = DefaultEditable;

      //Dirty flag
      view.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any overriden SetDefaults versions (at descendents)
    }

    #endregion

  }

}