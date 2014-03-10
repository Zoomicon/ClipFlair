//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageOptions.xaml.cs
//Version: 20140310

using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.Windows.Options
{

  public partial class ImageOptions : UserControl
  {

    public ImageOptions()
    {
      InitializeComponent();
    }

    #region --- Fields ---

    protected ImageWindow imageWindow;

    #endregion

    #region --- Properties ---

    public ImageWindow ImageWindow
    {
      get { return imageWindow; }
      set
      {
        imageWindow = value;
        DataContext = (value != null) ? value.View : null;
      }
    }

    #endregion

/*
    #region --- Events ---

    private void btnLoadImage_Click(object sender, RoutedEventArgs e)
    {
      ImageWindow.OpenLocalFile();
    }

    #endregion
*/

  }

}