//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorWindow.xaml.cs
//Version: 20130919

using ClipFlair.Windows.Views;

using Ionic.Zip;

using System.IO;

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

    #region Load / Save Options

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);
      
      //load text
      ZipEntry entry = zip[zipFolder + "/document.text"];
      if (entry == null) entry = zip[zipFolder + "/document.txt"];
      if (entry == null) entry = zip[zipFolder + "/document.docx"]; //TODO: check if it's possible to also support RTF there
      if (entry != null)
        editor.Load(entry.OpenReader(), entry.FileName);
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      zip.AddEntry(zipFolder + "/document.text", SaveText); //TODO: maybe should not save when no text is available
    }

    public void SaveText(string entryName, Stream stream)
    {
      editor.SaveXaml(stream);
    }

    #endregion

  }
}
