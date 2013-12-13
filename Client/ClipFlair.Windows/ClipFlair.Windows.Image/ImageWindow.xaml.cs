//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20131120

using ClipFlair.UI.Dialogs;
using ClipFlair.Windows.Views;
using System;
using System.Windows;
using System.Windows.Input;
using Utils.Extensions;

namespace ClipFlair.Windows
{

  public partial class ImageWindow : BaseWindow
  {
    public ImageWindow()
    {
      View = new ImageView(); //must set the view first
      InitializeComponent();
      imgContent.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(imgContent_MouseLeftButtonDown), true); //must pass "true" to handle events marked as already "handled"
    }

    #region View

    public IImageViewer ImageView
    {
      get { return (IImageViewer)View; }
      set { View = value; }
    }

    #endregion

    private void imgContent_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (ImageView.ActionURL != null)
        Dispatcher.BeginInvoke(delegate
        {
          try
          {
            ImageView.ActionURL.NavigateTo();
          }
          catch
          {
            MessageDialog.Show("Action", "Please visit " + ImageView.ActionURL.ToString()); //TODO: use URLDialog here with clickable URL on it
          }
        });
      
      if (ImageView.ActionTime != null)
        ImageView.Time = (TimeSpan)ImageView.ActionTime;
    }

  }
}
