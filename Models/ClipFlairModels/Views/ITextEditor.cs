//Filename: ITextEditor.cs
//Version: 20120823

using System;

namespace ClipFlair.Models.Views
{

  public static class ITextEditorProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyToolbarVisible = "ToolbarVisible";
  }

  public static class ITextEditorDefaults
  {
    public static Uri DefaultSource = null;
    public static bool DefaultToolbarVisible = true;
  }
  
  public interface ITextEditor : IView
  {
    Uri Source { get; set; }
    bool ToolbarVisible { get; set; }
  }

}
