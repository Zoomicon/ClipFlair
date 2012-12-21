//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindowFactory.cs
//Version: 2012121

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
