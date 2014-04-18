//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BrowserDialog.xaml.cs
//Version: 20140418

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Utils.Extensions;

namespace ClipFlair.UI.Dialogs
{
  public partial class BrowserDialog : ChildWindow
  {
    public BrowserDialog()
    {
      InitializeComponent();
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

    public static void Show(Uri address, string title = "", EventHandler<CancelEventArgs> closingHandler = null)
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
