//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: PivotDialog.xaml.cs
//Version: 20140616

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

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      //ChildWindow template part names can be found at: http://msdn.microsoft.com/en-us/library/dd833070(v=vs.95).aspx
      FrameworkElement contentRoot = GetTemplateChild("ContentRoot") as FrameworkElement;

      //make the ChildWindow content take up the whole ChildWindow area
      contentRoot.HorizontalAlignment = HorizontalAlignment.Stretch;
      contentRoot.VerticalAlignment = VerticalAlignment.Stretch;
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
