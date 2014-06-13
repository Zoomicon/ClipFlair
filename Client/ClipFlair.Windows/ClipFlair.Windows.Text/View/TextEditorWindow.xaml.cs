//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorWindow.xaml.cs
//Version: 20140613

using ClipFlair.Windows.Text;
using ClipFlair.Windows.Views;
using Ionic.Zip;
using System.IO;
using Utils.Extensions;

namespace ClipFlair.Windows
{

  public partial class TextEditorWindow : BaseWindow
  {
    public TextEditorWindow()
    {
      View = new TextEditorView2(); //must set the view first
      InitializeComponent();
      //editor.LoadResource("/ClipFlair;component/ActivityDescription.text"); //The intial text is stored as XAML at a .text file.
     }

    #region View

    public override IView View
    {
      get { return base.View; }
      set
      {
        if ( (value != null) && (value.GetType() == typeof(TextEditorView)) ) //Note: if we use TextEditorView.GetType() here instead of typeof(TextEditorView) an exception is thrown
          base.View = new TextEditorView2((ITextEditor)value); //upgrade TextEditorView to TextEditorView2 for next state save operation
        else
          base.View = value;
      }
    }

    public ITextEditor TextEditorView
    {
      get { return (ITextEditor)View; }
      set { View = value; }
    }

    public ITextEditor2 TextEditorView2 //only code that requires ITextEditor2 should use this
    {
      get { return (ITextEditor2)View; }
      set { View = value; }
    }

    #endregion

    #region Load / Save

    public override string LoadFilter
    {
      get
      {
        return base.LoadFilter + "|" + TextEditorWindowFactory.LOAD_FILTER;
      }
    }

    public override void LoadOptions(FileInfo f)
    {
      if (!f.Name.EndsWith(new string[] { CLIPFLAIR_EXTENSION, CLIPFLAIR_ZIP_EXTENSION }))
        editor.Load(f);
      else
        base.LoadOptions(f);
    }

    public override void LoadContent(Stream stream, string filename)
    {
      editor.Load(stream, filename, true);
    }

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);
      
      //load text
      ZipEntry entry = zip[zipFolder + "/document.text"];
      if (entry == null) entry = zip[zipFolder + "/document.txt"];
      if (entry == null) entry = zip[zipFolder + "/document.docx"]; //TODO: check if it's possible to also support RTF there
      if (entry != null)
        editor.Load(entry.OpenReader(), entry.FileName);
      else
        editor.Clear(confirm: false);
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      zip.AddEntry(zipFolder + "/document.text", SaveText); //when there is no text, saving an empty file
    }

    public void SaveText(string entryName, Stream stream) //callback
    {
      editor.SelectNone(); //must clear selection first, else it will save only that in the saved state
      editor.SaveXaml(stream);
    }

    #endregion

  }
}
