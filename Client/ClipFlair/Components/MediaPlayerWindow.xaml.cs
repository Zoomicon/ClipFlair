//Version: 20120730

using ClipFlair.Views;

using Microsoft.SilverlightMediaFramework;
using Microsoft.SilverlightMediaFramework.Core.Media;

using Extensions;

namespace ClipFlair.Components
{
    public partial class MediaPlayerWindow : FlipWindow
    {
        public MediaPlayerWindow()
        {
            InitializeComponent();
            DataContext = new MediaPlayerView();
        }

        private void edMediaURL_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            PlaylistItem[] p= new PlaylistItem[player.Playlist.Count];
            player.Playlist.CopyTo(p, 0);
            p[0].MediaSource = edMediaURL.Text.ToUri();
            //...
        }

   }
}
