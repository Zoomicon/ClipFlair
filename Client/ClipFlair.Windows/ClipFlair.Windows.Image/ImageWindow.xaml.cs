//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20121221

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

        public new IImageViewer View //hiding parent property
        {
          get { return (IImageViewer)base.View; } //delegating to parent property
          set { base.View = value; }
        }

        #endregion
     
    }
}
