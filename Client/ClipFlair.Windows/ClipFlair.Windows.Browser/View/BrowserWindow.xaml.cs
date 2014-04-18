//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BrowserWindow.xaml.cs
//Version: 20140415

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using ClipFlair.Windows.Views;
using Utils.Extensions;

namespace ClipFlair.Windows
{

  public partial class BrowserWindow : BaseWindow
  {
    public BrowserWindow()
    {
      View = new BrowserView(); //must set the view first
      InitializeComponent();
    }

    #region View

    public IBrowser BrowserView
    {
      get { return (IBrowser)View; }
      set { View = value; }
    }

    #endregion

  }
}
