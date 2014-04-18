//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: PivotDialog.xaml.cs
//Version: 20140418

using System;
using System.ComponentModel;
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

    public static void Show(string title, string defaultInput, EventHandler<CancelEventArgs> closingHandler)
    {
      PivotDialog prompt = new PivotDialog();
      prompt.Title = title;
      prompt.Input = defaultInput;
      prompt.Closing += closingHandler;
      prompt.Show();
    }
   
    private void pivot_SelectionChanged(object sender, EventArgs e)
    {
      
    }

  }
}
