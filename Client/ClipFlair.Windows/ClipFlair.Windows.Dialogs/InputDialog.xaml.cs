//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: InputDialog.xaml.cs
//Version: 20121221

using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace ClipFlair.Windows.Dialogs
{
  public partial class InputDialog : ChildWindow
  {
    public InputDialog()
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

    public string Message
    {
      get { return lblMessage.Text; }
      set { lblMessage.Text = value; }
    }

    public string Input
    {
      get { return (DialogResult == true)? txtInput.Text : null; }
      set { txtInput.Text = value;  }
    }

    public static void Show(string Message, string DefaultInput, EventHandler<CancelEventArgs> ClosingHandler)
    {
      InputDialog prompt = new InputDialog();
      prompt.Message = Message;
      prompt.Input = DefaultInput;
      prompt.Closing += ClosingHandler;
      prompt.Show();
    }

  }
}

