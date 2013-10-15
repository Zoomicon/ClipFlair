//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ErrorDialog.xaml.cs
//Version: 20131015

using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace ClipFlair.UI.Dialogs
{
  public partial class ErrorDialog : ChildWindow
  {
    public ErrorDialog()
    {
      InitializeComponent();
    }

    public ErrorDialog(Exception e) : this()
    {
      Message = e.Message;
      Details = e.Message + e.StackTrace; //have the message too in the details to be able to select and copy it
      Exception inner = e.InnerException;
      if (inner != null)
        Details = Details + "\n\n" + inner.Message + inner.StackTrace; 
    }

    #region Properties

    //we also inherit Title property from ancestor class

    public string Message
    {
      get { return lblMessage.Text; }
      set { lblMessage.Text = value; }
    }

    public string Details
    {
      get { return txtDetails.Text; }
      set { txtDetails.Text = value; }
    }

    #endregion

    #region Methods

    public static void Show(string title, Exception e, EventHandler<CancelEventArgs> closingHandler = null)
    {
      ErrorDialog prompt = new ErrorDialog(e);
      prompt.Title = title;
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

