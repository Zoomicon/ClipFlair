//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextWindowFactory.cs
//Version: 20140315

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Text
{

  //Supported file extensions
  [Export("TEXT", typeof(IWindowFactory))]
  [Export("DOCX", typeof(IWindowFactory))]
  [Export("TXT", typeof(IWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.TextEditorView", typeof(IWindowFactory))]
  [Export("ClipFlair.Windows.Views.TextEditorView2", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class TextEditorWindowFactory : IFileWindowFactory
  {
    private static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { "TEXT", "DOCX", "TXT" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }

    public BaseWindow CreateWindow()
    {
      return new TextEditorWindow();
    }

    public BaseWindow CreateWindow(string filename, Stream stream)
    {
      TextEditorWindow window = new TextEditorWindow();
      window.Load(stream, filename); //passing filename as title
      return window;
    }

  }

}
