//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: NewsWindow.xaml.cs
//Version: 20140311

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using ClipFlair.Windows.Views;
using Utils.Extensions;

namespace ClipFlair.Windows
{

  public partial class NewsWindow : BaseWindow
  {
    public NewsWindow()
    {
      View = new NewsView(); //must set the view first
      InitializeComponent();
    }

    #region View

    public INewsReader NewsView
    {
      get { return (INewsReader)View; }
      set { View = value; }
    }

    #endregion

  }
}
