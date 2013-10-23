//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorDefaults.cs
//Version: 20131023

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class TextEditorDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Text";
    public const double DefaultWidth = 650;
    public const double DefaultHeight = 630;
    
    #endregion

    public const Uri DefaultSource = null;
    public static readonly TimeSpan DefaultTime = TimeSpan.Zero;    
    public const bool DefaultToolbarVisible = true;
    public const bool DefaultEditable = true;
    public const bool DefaultRTL = false;

    #region Methods

    public static void SetDefaults(ITextEditor editor)
    {
      //IView defaults and overrides
      ViewDefaults.SetDefaults(editor);
      editor.Title = DefaultTitle;
      editor.Width = DefaultWidth;
      editor.Height = DefaultHeight;

      //ITextEditor defaults
      editor.Source = DefaultSource;
      editor.Time = DefaultTime;
      editor.ToolbarVisible = DefaultToolbarVisible;
      editor.Editable = DefaultEditable;
      editor.RTL = DefaultRTL;
    }

    #endregion

  }

}