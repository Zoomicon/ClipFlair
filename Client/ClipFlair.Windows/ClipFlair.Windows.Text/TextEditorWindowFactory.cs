//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorWindowFactory.cs
//Version: 20140615

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Text
{

  //Supported file extensions
  [Export(".TEXT", typeof(IFileWindowFactory))]
  [Export(".DOCX", typeof(IFileWindowFactory))]
  [Export(".TXT", typeof(IFileWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.TextEditorView", typeof(IWindowFactory))]
  [Export("ClipFlair.Windows.Views.TextEditorView2", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class TextEditorWindowFactory : IFileWindowFactory
  {
    public const string LOAD_FILTER = "All Text Files (*.text;*.docx;*.txt)|*.text;*.docx;*.txt|ClipFlair Text Files (*.text)|*.text|Office OpenXML Files (*.docx)|*.docx|Unicode Text Files (*.txt)|*.txt|All Files|*.*";
   
    private static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { ".TEXT", ".DOCX", ".TXT" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }

    public BaseWindow CreateWindow()
    {
      return new TextEditorWindow();
    }

  }

}
