//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextWindowFactory.cs
//Version: 20130326

using System.ComponentModel.Composition;

namespace ClipFlair.Windows.Text
{

  [Export("ClipFlair.Windows.Views.TextEditorView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class TextEditorWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new TextEditorWindow();
    }
  }

}
