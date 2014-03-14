//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextWindowFactory.cs
//Version: 20140314

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Text
{

  [Export("ClipFlair.Windows.Views.TextEditorView", typeof(IWindowFactory))]
  [Export("ClipFlair.Windows.Views.TextEditorView2", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class TextEditorWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new TextEditorWindow();
    }
  }

}
