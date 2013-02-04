//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20130204

using ClipFlair.Windows.Views;

namespace ClipFlair.Windows
{

  public partial class ImageWindow : BaseWindow
  {
    public ImageWindow()
    {
      View = new ImageView(); //must set the view first
      InitializeComponent();
    }

    #region View

    public IImageViewer ImageView
    {
      get { return (IImageViewer)View; }
      set { View = value; }
    }

    #endregion

  }
}
