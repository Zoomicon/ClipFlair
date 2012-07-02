//Version: 20120702

using Microsoft.SilverlightMediaFramework;
using Microsoft.SilverlightMediaFramework.Core.Media;

using ClipFlair.Extensions;

namespace ClipFlair
{
    public partial class MediaPlayerWindow : FlipWindow
    {
        public MediaPlayerWindow()
        {
            InitializeComponent();
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
