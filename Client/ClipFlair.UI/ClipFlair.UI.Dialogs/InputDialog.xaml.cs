//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: InputDialog.xaml.cs
//Version: 20130131

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.UI.Dialogs
{
  public partial class InputDialog : ChildWindow
  {
    public InputDialog()
    {
      InitializeComponent();
    }

    private void btnOK_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = false;
    }

    private void btnHelp_Click(object sender, RoutedEventArgs e)
    {
      if (HelpRequested != null)
        HelpRequested(this, e);
    }

    //we also inherit Title property from ancestor class

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

    public bool HelpButtonVisible
    {
      get { return btnHelp.Visibility == Visibility.Visible; }
      set { btnHelp.Visibility = value ? Visibility.Visible : Visibility.Collapsed;  }
    }

    public static void Show(string title, string message, string defaultInput, EventHandler<CancelEventArgs> closingHandler)
    {
      InputDialog prompt = new InputDialog();
      prompt.Title = title;
      prompt.Message = message;
      prompt.Input = defaultInput;
      prompt.Closing += closingHandler;
      prompt.Show();
    }

    public static void Show(string title, string message, string defaultInput, EventHandler<CancelEventArgs> closingHandler, EventHandler helpHandler)
    {
      InputDialog prompt = new InputDialog();
      prompt.Title = title;
      prompt.Message = message;
      prompt.Input = defaultInput;
      prompt.Closing += closingHandler;
      if (helpHandler != null)
      {
        prompt.HelpRequested += helpHandler;
        prompt.HelpButtonVisible = true;
      }
      prompt.Show();
    }
    
    public event EventHandler HelpRequested;

  }
}
