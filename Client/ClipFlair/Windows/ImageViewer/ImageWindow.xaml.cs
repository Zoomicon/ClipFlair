//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20121113

using ClipFlair.Windows.Views;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClipFlair.Windows
{
  public partial class ImageWindow : BaseWindow
    {
        public ImageWindow()
        {
          View = new ImageView(); //must set the view first
          InitializeComponent();
          InitializeView();
        }

        #region View

        public new IImageViewer View //hiding parent property
        {
          get { return (IImageViewer)base.View; } //delegating to parent property
          set { base.View = value; }
        }

        #endregion
     
    }
}
