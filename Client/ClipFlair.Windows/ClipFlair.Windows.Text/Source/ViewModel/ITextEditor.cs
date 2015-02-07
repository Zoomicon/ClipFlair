//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ITextEditor.cs
//Version: 20131205

//NOTE: Do not add any more properties to this interface, kept for backwards compatibility. Use ITextEditor2 instead.

using System;

namespace ClipFlair.Windows.Views
{

  public interface ITextEditor : IView
  {
    Uri Source { get; set; }
    bool ToolbarVisible { get; set; }
    bool Editable { get; set; }
  }

}
