﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorWindow.xaml.cs
//Version: 20130204

using ClipFlair.Windows.Views;

using Ionic.Zip;

using System.IO;

namespace ClipFlair.Windows
{

  public partial class TextEditorWindow : BaseWindow
  {
    public TextEditorWindow()
    {
      View = new TextEditorView(); //must set the view first
      InitializeComponent();
      //editor.LoadResource("/ClipFlair;component/ActivityDescription.text"); //The intial text is stored as XAML at a .text file.
     }

    #region View

    public ITextEditor TextEditorView
    {
      get { return (ITextEditor)View; }
      set { View = value; }
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
