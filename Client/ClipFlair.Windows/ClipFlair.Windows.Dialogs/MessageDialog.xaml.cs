//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MessageDialog.xaml.cs
//Version: 20121222

using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace ClipFlair.Windows.Dialogs
{
  public partial class MessageDialog : ChildWindow
  {
    public MessageDialog()
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
      InputDialog prompt = new InputDialog();
      prompt.Title = title;
      prompt.Message = message;
      prompt.Closing += closingHandler;
      prompt.Show();
    }

  }
}

