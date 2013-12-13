//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorDefaults.cs
//Version: 20131213

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class TextEditorDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Text";
    public const double DefaultWidth = 650;
    public const double DefaultHeight = 630;
    public static readonly Color DefaultBorderColor = Color.FromArgb(0xFF, 0x02, 0xAB, 0xBE); //#02ABBE

    #endregion

    public const Uri DefaultSource = null;
    public const bool DefaultToolbarVisible = true;
    public const bool DefaultEditable = true;

    #region Methods

    public static void SetDefaults(ITextEditor editor)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(editor);
      editor.Title = DefaultTitle;
      editor.Width = DefaultWidth;
      editor.Height = DefaultHeight;
      editor.BorderColor = DefaultBorderColor;

      //ITextEditor defaults
      editor.Source = DefaultSource;
      editor.ToolbarVisible = DefaultToolbarVisible;
      editor.Editable = DefaultEditable;
      //editor.Color = DefaultColor;

      //Dirty flag
      editor.Dirty = ViewDefaults.DefaultDirty; //must do last - this should be set again at the end of any overriden SetDefaults versions (at descendents)
    }

    #endregion

  }

}