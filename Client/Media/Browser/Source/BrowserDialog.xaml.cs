//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BrowserDialog.xaml.cs
//Version: 20150206

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Utils.Extensions;
using WPF_Compatibility;

namespace ClipFlair.UI.Dialogs
{
  public partial class BrowserDialog : ChildWindowExt
  {
    public BrowserDialog()
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

    public Uri Address
    {
      get { return browser.Source; }
      set {
        try
        {
          browser.Source = value;
        }
        catch
        {
          //NOP
        }
      }
    }

    public static void Show(Uri address, string title = "", 
     #if SILVERLIGHT
     EventHandler<CancelEventArgs>
     #else
     CancelEventHandler
     #endif
     closingHandler = null)
    {
      #if SILVERLIGHT && !WINDOWS_PHONE
      if (!Application.Current.IsRunningOutOfBrowser) {
        if (address != null) 
          address.NavigateTo();
      }
      else
      #endif
      {
        BrowserDialog prompt = new BrowserDialog();
        prompt.Address = address;
        prompt.Title = title;
        if (closingHandler != null) 
          prompt.Closing += closingHandler;

        prompt.Closed += (s, e) => { prompt.Content = null; }; //remove the browser control when closed //TODO: check if this solves Reentrancy Errors when opening the browser dialog multiple times in sequence

        prompt.Show();
      }
    }

  }

}
