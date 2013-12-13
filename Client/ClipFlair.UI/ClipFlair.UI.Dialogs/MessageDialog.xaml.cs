//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MessageDialog.xaml.cs
//Version: 20130501

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.UI.Dialogs
{
  public partial class MessageDialog : ChildWindow
  {
    public MessageDialog()
    {
      InitializeComponent();
    }

    #region Properties

    //we also inherit Title property from ancestor class

    public string Message
    {
      get { return lblMessage.Text; }
      set { lblMessage.Text = value; }
    }

    #endregion

    #region Methods

    public static void Show(string title, string message, EventHandler<CancelEventArgs> closingHandler = null)
    {
      MessageDialog prompt = new MessageDialog();
      prompt.Title = title;
      prompt.Message = message;
      if (closingHandler != null) 
        prompt.Closing += closingHandler;
      prompt.Show();
    }

    #endregion

    #region Events

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }

    #endregion

  }
}

