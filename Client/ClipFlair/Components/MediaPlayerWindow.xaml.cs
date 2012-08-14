//Filename: MediaPlayerWindow.xaml.cs
//Version: 20120814

using ClipFlair.Views;

using Microsoft.SilverlightMediaFramework;
using Microsoft.SilverlightMediaFramework.Core.Media;

using Extensions;

using System;
using System.Windows;

namespace ClipFlair.Components
{
    public partial class MediaPlayerWindow : FlipWindow
    {
        public MediaPlayerWindow()
        {
          View = new MediaPlayerView(); //must set the view first
          InitializeComponent();
        }

        private void edMediaURL_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            PlaylistItem[] p= new PlaylistItem[player.Playlist.Count];
            player.Playlist.CopyTo(p, 0);
            p[0].MediaSource = edMediaURL.Text.ToUri();
            //...
        }

        #region View

        public MediaPlayerView View
        {
          get { return (MediaPlayerView)DataContext; }
          set { DataContext = value; }
        }

        #endregion

        #region Source

        /// <summary>
        /// Source Dependency Property
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(MediaPlayerWindow),
                new FrameworkPropertyMetadata((Uri)null, new PropertyChangedCallback(OnSourceChanged)));

        /// <summary>
        /// Gets or sets the Source property.
        /// </summary>
        public Uri Source
        {
          get { return (Uri)GetValue(SourceProperty); }
          set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Source property.
        /// </summary>
        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
          MediaPlayerWindow target = (MediaPlayerWindow)d;
          target.OnSourceChanged((Uri)e.OldValue, target.Source);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
        /// </summary>
        protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
        {
          View.Source = newSource;
        }

        #endregion

   }
}
