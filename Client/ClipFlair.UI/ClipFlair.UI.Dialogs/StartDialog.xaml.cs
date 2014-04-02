//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StartDialog.xaml.cs
//Version: 20140402

using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace ClipFlair.UI.Dialogs
{
  public partial class StartDialog : ChildWindow
  {
    public StartDialog()
    {
      InitializeComponent();
    }

    #region Methods

    public static void Show(string title, EventHandler<CancelEventArgs> closingHandler = null)
    {
      StartDialog prompt = new StartDialog();
      prompt.Title = title;
      if (closingHandler != null) 
        prompt.Closing += closingHandler;
      prompt.Show();
    }

    #endregion

 }

}

