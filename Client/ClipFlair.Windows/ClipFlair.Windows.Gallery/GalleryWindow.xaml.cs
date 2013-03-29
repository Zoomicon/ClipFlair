//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryWindow.xaml.cs
//Version: 20130327

using ClipFlair.Windows.Views;

using System.Windows;

namespace ClipFlair.Windows
{

  public partial class GalleryWindow : BaseWindow
  {
    public GalleryWindow()
    {
      View = new GalleryView(); //must set the view first
      InitializeComponent();
      
      //IsAnimated = false; //workarround: Can't turn over Pivot viewer when using 3D-style rotation, thowing transformation class cast errors
    }

    #region View

    public IGalleryViewer GalleryView
    {
      get { return (IGalleryViewer)View; }
      set { View = value; }
    }

    #endregion

    #region Events

    /*
    public override void ShowOptions() //workarround: Can't turn over Pivot viewer when using 3D-style rotation, thowing transformation class cast errors (the IsAnimated=false isn't enough, need this too, else at resize etc. when flipped will throw exceptions)
    {
      if (!Flipped)
        pivot.Visibility = Visibility.Collapsed;

      base.ShowOptions();

      if (!Flipped)
        pivot.Visibility = Visibility.Visible;
    }
*/

    #endregion

  }
}
