//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerWindow.xaml.cs
//Version: 20141107

using ClipFlair.Windows.Media;
using ClipFlair.Windows.Views;
using Ionic.Zip;
using Microsoft.SilverlightMediaFramework.Core.Media;
using System;
using System.IO;
using System.Windows;
using Utils.Extensions;

namespace ClipFlair.Windows
{

  public partial class MediaPlayerWindow : BaseWindow
  {

    private TimeSpan defaultReplayOffset;

    public MediaPlayerWindow()
    {
      View = new MediaPlayerView(); //must set the view first
      InitializeComponent();

      if (mediaPlayerOptions != null)
        mediaPlayerOptions.MediaPlayerWindow = this;

      defaultReplayOffset = player.ReplayOffset; //can set ReplayOffset in XAML
    }

    #region --- Properties ---

    public IMediaPlayer MediaPlayerView
    {
      get { return (IMediaPlayer)View; }
      set { View = value; }
    }

    public override IView View
    {
      get { return base.View; }
      set { 
        base.View = value;
        if (mediaPlayerOptions != null)
          mediaPlayerOptions.MediaPlayerWindow = this;
      }
    }

    #endregion

    #region --- Methods ---
    
    #region --- Offline playlist ---

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

    #region --- Open local file dialog ---

    public bool OpenLocalFile()
    {
      bool done = player.OpenLocalFile();
      if (done) Flipped = false; //flip back to front
      return done;
    }

    #endregion
    
    #region --- LoadOptions dialog ---

    public override string LoadFilter
    {
      get
      {
        return base.LoadFilter +"|" + MediaPlayerWindowFactory.LOAD_FILTER;
      } 
    }

    public override void LoadOptions(FileInfo f)
    {
      if (!f.Name.EndsWith(new string[] { CLIPFLAIR_EXTENSION, CLIPFLAIR_ZIP_EXTENSION }))
        player.Open(f);
      else
        base.LoadOptions(f);
    }

    #endregion

    #region --- Load media from stream ----

    public override void LoadContent(Stream stream, string title = "") //doesn't close stream
    {
      player.Open(stream, title);
    }

    #endregion

    #region --- Load saved state ---

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);

      //fix for loading saved volume
      if (player.VolumeLevel == MediaPlayerView.Volume) //SMF-bug patch: change volume temporarily, so that the volume control gets updated correctly after state loading
        if (player.VolumeLevel < 1)
          player.VolumeLevel = 1;
        else
          player.VolumeLevel = 0.9;
      player.VolumeLevel = MediaPlayerView.Volume; //try to set again the volume (patch for SMF bug: keeps the volume widget at wrong position)

      //load embedded media file
      foreach (string ext in MediaPlayerWindowFactory.SUPPORTED_FILE_EXTENSIONS)
        foreach (ZipEntry mediaEntry in zip.SelectEntries("*" + ext, zipFolder))
          using (Stream zipStream = mediaEntry.OpenReader())
          {
            MemoryStream memStream = new MemoryStream((int)mediaEntry.UncompressedSize); //do not wrap this in "using" clause, keep the stream alive in memory
            zipStream.CopyTo(memStream);
            memStream.Position = 0;
            LoadContent(memStream, mediaEntry.FileName); //seems we need to copy the media file to a memory stream for it to play OK
            return; //just load the first one
          }
    }

    #endregion

    #region --- Save state ---

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      //save media file (embed)
      if (player.MediaData != null)
        zip.AddEntry(zipFolder + "/" + player.Filename, SaveMedia); //SaveMedia is a callback method
    }

    public void SaveMedia(string entryName, Stream stream) //callback
    {
      Stream s = player.MediaData;
      if (s != null)
      {
        s.Position = 0;
        s.CopyTo(stream); //default buffer size is 4096
      }
    }

    #endregion

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