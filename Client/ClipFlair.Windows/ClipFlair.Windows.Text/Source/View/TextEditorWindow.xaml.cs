//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorWindow.xaml.cs
//Version: 20140921

using ClipFlair.Windows.Text;
using ClipFlair.Windows.Views;
using Ionic.Zip;
using System.IO;
using Utils.Extensions;

namespace ClipFlair.Windows
{

  public partial class TextEditorWindow : BaseWindow
  {

    #region --- Constants ---

    public const string DEFAULT_TEXT = "document.text";

    #endregion

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

    #region --- Load Options Dialog ---

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

    #endregion

    #region --- Load from stream ---

    public override void LoadContent(Stream stream, string filename)
    {
      editor.Load(stream, filename, true);
    }

    #endregion

    #region --- Load saved state ---

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);

      editor.Clear(confirm: false);

      //load embedded text documents
      foreach (string ext in TextEditorWindowFactory.SUPPORTED_FILE_EXTENSIONS)
        foreach (ZipEntry textEntry in zip.SelectEntries("*" + ext, zipFolder))
          using (Stream zipStream = textEntry.OpenReader()) //closing stream when done
            editor.Load(zipStream, textEntry.FileName, clearFirst:false); //merge multiple text documents together (if user has embedded them manually in the saved state file, since we save only DEFAULT_TEXT with all the content)

      editor.ScrollToStart(); //must do this here, since we use false for "clearFirst" above (because we're merging multiple documents), which makes "editor.Load" not call "ScrollToStart"
    }

    #endregion

    #region --- Save state ---

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      zip.AddEntry(zipFolder + "/" + DEFAULT_TEXT, SaveText); //when there is no text, saving an empty file //SaveText is callback method
    }

    public void SaveText(string entryName, Stream stream) //callback
    {
      editor.SelectNone(); //must clear selection first, else it will save only that in the saved state
      editor.SaveXaml(stream);
    }

    #endregion

    #endregion

  }
}
