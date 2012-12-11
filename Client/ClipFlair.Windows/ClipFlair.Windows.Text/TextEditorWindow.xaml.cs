//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorWindow.xaml.cs
//Version: 20121205

using ClipFlair.Windows.Views;

using Ionic.Zip;

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows
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

  public partial class TextEditorWindow : BaseWindow
  {
    public TextEditorWindow()
    {
      View = new TextEditorView(); //must set the view first
      InitializeComponent();
      //editor.LoadResource("/ClipFlair;component/ActivityDescription.text"); //The intial text is stored as XAML at a .text file.
     }

    #region View

    public new ITextEditor View //hiding parent property
    {
      get { return (ITextEditor)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    #endregion

    #region Load / Save Options

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);
      
      //load text
      ZipEntry entry = zip[zipFolder + "/document.text"];
      //if (entry == null) entry = zip[zipFolder + "/document.txt"];
      //if (entry == null) entry = zip[zipFolder + "/document.docx"]; //TODO: check if it's possible to also support RTF there
      if (entry != null)
        editor.Load(entry.OpenReader()); //TODO: should accept entry.FileName to check for .text (XAML Snippet), .txt, .docx and load it accordingly (similarly change its open file dialog to accept multiple formats)
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      zip.AddEntry(zipFolder + "/document.text", SaveText); //TODO: maybe not save when no text is available
    }

    public void SaveText(string entryName, Stream stream)
    {
      editor.Save(stream);
    }

    #endregion

  }
}
