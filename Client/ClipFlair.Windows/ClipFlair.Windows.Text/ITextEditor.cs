//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ITextEditor.cs
//Version: 20130118

using System;

namespace ClipFlair.Windows.Views
{

  public static class ITextEditorProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyTime = "Time";
    public const string PropertyToolbarVisible = "ToolbarVisible";
    public const string PropertyEditable = "Editable";
    public const string PropertyRTL = "RTL";
  }

  public interface ITextEditor : IView
  {
    Uri Source { get; set; }
    TimeSpan Time { get; set; }
    bool ToolbarVisible { get; set; }
    bool Editable { get; set; }
    bool RTL { get; set; }
  }

}
