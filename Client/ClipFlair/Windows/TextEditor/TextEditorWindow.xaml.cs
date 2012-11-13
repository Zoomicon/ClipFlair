//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorWindow.xaml.cs
//Version: 20121113

using ClipFlair.Windows.Views;

using Ionic.Zip;

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace ClipFlair.Windows
{
  public partial class TextEditorWindow : BaseWindow
  {
    public TextEditorWindow()
    {
      View = new TextEditorView(); //must set the view first
      InitializeComponent();
      InitializeView();
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
      editor.Load(zip[zipFolder + "/document.text"].OpenReader());
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      MemoryStream stream = new MemoryStream(); //TODO: not optimal implementation, should try to pipe streams without first saving into memory
      editor.Save(stream);
      stream.Position = 0;
      zip.AddEntry(zipFolder + "/document.text", stream);
    }

    #endregion

  }
}
