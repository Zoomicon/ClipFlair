//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: PivotDialog.xaml.cs
//Version: 20140128

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.UI.Dialogs
{
  public partial class PivotDialog : ChildWindow
  {
    public PivotDialog()
    {
      InitializeComponent();
    }

    //we inherit Title property from ancestor class



    public string Input
    {
      get { return (DialogResult == true)? "http://clipflair.net" : null; } //TODO: change URL returned, maybe return url object
      set {    } //TODO: set selected item of pivot control?
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
      prompt.Show();
    }
    
    public event EventHandler HelpRequested;

    private void pivot_SelectionChanged(object sender, EventArgs e)
    {
      
    }

  }
}
