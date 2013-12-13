//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ConfirmDialog.xaml.cs
//Version: 20130501

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.UI.Dialogs
{
  public partial class ConfirmDialog : ChildWindow
  {
    public ConfirmDialog()
    {
      InitializeComponent();
    }

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = false;
    }

    //we also inherit Title property from ancestor class

    public string Message
    {
      get { return lblMessage.Text; }
      set { lblMessage.Text = value; }
    }

    public static void Show(string title, string message, EventHandler<CancelEventArgs> closingHandler)
    {
      ConfirmDialog prompt = new ConfirmDialog();
      prompt.Title = title;
      prompt.Message = message;
      prompt.Closing += closingHandler;
      prompt.Show();
    }

  }
}

