//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerWindow.xaml.cs
//Version: 20130703

using ClipFlair.Windows.Views;

using Ionic.Zip;

using System;
using System.Windows;

using Microsoft.SilverlightMediaFramework.Core.Media;

namespace ClipFlair.Windows
{

  public partial class MediaPlayerWindow : BaseWindow
  {

    private TimeSpan defaultReplayOffset;

    public MediaPlayerWindow()
    {
      View = new MediaPlayerView(); //must set the view first
      InitializeComponent();

      defaultReplayOffset = player.ReplayOffset; //can set ReplayOffset in XAML
    }

    #region --- Properties ---

    public IMediaPlayer MediaPlayerView
    {
      get {return (IMediaPlayer)View; }
      set { View = value; }
    }
  
    #endregion

    #region --- Methods ---

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);

      if (player.VolumeLevel == MediaPlayerView.Volume) //SMF-bug patch: change volume temporarily, so that the volume control gets updated correctly after state loading
        if (player.VolumeLevel < 1)
          player.VolumeLevel = 1;
        else
          player.VolumeLevel = 0.9;
      player.VolumeLevel = MediaPlayerView.Volume; //try to set again the volume (patch for SMF bug: keeps the volume widget at wrong position)
    }

    public void LoadOfflinePlaylist()
    {
      player.Source = new Uri(""); //this will also do Playlist.Clear()
      try
      {
        foreach (PlaylistItem p in player.OpenOfflinePlaylist("ClipFlairPlaylist")) //TODO: allow to define offline filename and maybe allow to delete old ones? (or show offline size and allow clear)
          player.Playlist.Add(p);
        MessageBox.Show("Offline playlist restored"); //TODO: find parent window and pass here
        player.GoToPlaylistItem(0); //after playlist is restored go to the 1st playlist item
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: " + ex.Message); //TODO: find parent window and pass here
      }
    }

    public void SaveOfflinePlaylist() //TODO: need Smooth Streaming ISO Cache plugin for SMF from Media Experiences project at codeplex since this now works only for ProgressiveDownload media
    {
      try
      {
        player.StorePlaylistContentOffline("ClipFlairPlaylist"); //TODO: allow to define offline filename and maybe allow to delete old ones? (or show offline size and allow clear)
        MessageBox.Show("Playlist stored offline"); //TODO: find parent window and pass here
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: " + ex.Message); //(only Progressive Download media is supported currently, can give empty source to clear playlist, then load some non smooth streaming remote URL)
      }
    }
 
    #endregion

    #region --- Events ---

    private void player_MediaOpened(object sender, EventArgs e)
    {
      player.Time = MediaPlayerView.Time; //apply the stored time value //TODO: doesn't seem to work (maybe the view's time has already changed)
    }

    private void player_TimelineMarkerReached(object sender, Microsoft.SilverlightMediaFramework.Core.TimelineMarkerReachedInfo e)
    {
      if (defaultReplayOffset == TimeSpan.Zero) //only when a ReplayOffset hasn't been set in XAML
        player.ReplayOffset = e.Marker.Duration;
    }

    private void player_TimelineMarkerLeft(object sender, Microsoft.SilverlightMediaFramework.Core.CustomEventArgs<TimelineMediaMarker> e)
    {
      player.ReplayOffset = defaultReplayOffset; //only used when not in a timeline marker range
    }

    #endregion

  }

}