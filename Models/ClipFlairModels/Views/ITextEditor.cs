//Filename: ITextEditor.cs
//Version: 20120814

using System;

namespace ClipFlair.Models.Views
{

  public static class ITextEditorProperties
  {
    public const string PropertySource = "Source";
  }

  public static class ITextEditorDefaults
  {
    public static Uri DefaultSource = null;
  }
  
  public interface ITextEditor : IView
  {
    Uri Source { get; set; }
  }

}
