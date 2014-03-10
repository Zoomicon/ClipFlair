//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerOptions.xaml.cs
//Version: 20140310

using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.Windows.Options
{

  public partial class MediaPlayerOptions : UserControl
  {

    public MediaPlayerOptions()
    {
      InitializeComponent();
    }

    #region --- Fields ---

    protected MediaPlayerWindow mediaPlayerWindow;

    #endregion

    #region --- Properties ---

    public MediaPlayerWindow MediaPlayerWindow
    {
      get { return mediaPlayerWindow; }
      set
      {
        mediaPlayerWindow = value;
        DataContext = (value != null) ? value.View : null;
      }
    }

    #endregion

    #region --- Events ---

    private void btnLoadMedia_Click(object sender, RoutedEventArgs e)
    {
      MediaPlayerWindow.player.OpenLocalFile();
    }

    private void btnLoadOffline_Click(object sender, RoutedEventArgs e) //TODO: doesn't seem to work, maybe needs offline cache plugin or respective SMF assemblies
    {
      MediaPlayerWindow.LoadOfflinePlaylist();
    }

    private void btnSaveOffline_Click(object sender, RoutedEventArgs e) //TODO: doesn't seem to work, maybe needs offline cache plugin or respective SMF assemblies
    {
      MediaPlayerWindow.SaveOfflinePlaylist();
    }

    #endregion

  }

}