﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ITextEditor.cs
//Version: 20121103

using System;

namespace ClipFlair.Windows.Views
{

  public static class ITextEditorProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyToolbarVisible = "ToolbarVisible";
    public const string PropertyEditable = "Editable";
    public const string PropertyRTL = "RTL";
  }

  public static class ITextEditorDefaults
  {
    public static Uri DefaultSource = null;
    public static bool DefaultToolbarVisible = true;
    public static bool DefaultEditable = true;
    public static bool DefaultRTL = false;
  }
  
  public interface ITextEditor : IView
  {
    Uri Source { get; set; }
    bool ToolbarVisible { get; set; }
    bool Editable { get; set; }
    bool RTL { get; set; }
  }

}
