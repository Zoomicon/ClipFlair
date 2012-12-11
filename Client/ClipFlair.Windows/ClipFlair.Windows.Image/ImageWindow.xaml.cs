//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20121205

using ClipFlair.Windows.Views;

using System.ComponentModel.Composition;

namespace ClipFlair.Windows
{

  [Export("ClipFlair.Windows.Views.ImageView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ImageWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new ImageWindow();
    }
  }

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
