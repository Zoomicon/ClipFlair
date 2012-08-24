//Filename: MediaPlayerWindow.xaml.cs
//Version: 20120824

using ClipFlair.Models.Views;

using Microsoft.SilverlightMediaFramework;
using Microsoft.SilverlightMediaFramework.Core.Media;

using Extensions;

using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClipFlair.Views
{
  public partial class MediaPlayerWindow : FlipWindow
  {
    public MediaPlayerWindow()
    {
      View = new MediaPlayerView(); //must set the view first
      InitializeComponent();
    }

    #region View

    public MediaPlayerView View
    {
      get { return (MediaPlayerView)DataContext; }
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          ((INotifyPropertyChanged)DataContext).PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged);
        //add property changed handler to new view
        value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
        //set the new view
        DataContext = value;
      }
    }

    protected void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == null)
      {
        Source = View.Source;
         //...
      }
      else if (e.PropertyName.Equals(IMediaPlayerProperties.PropertySource))
      {
        Source = View.Source;
      }
    }

    #endregion

    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(IMediaPlayerProperties.PropertySource, typeof(Uri), typeof(MediaPlayerWindow),
            new FrameworkPropertyMetadata((Uri)IMediaPlayerDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

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
      UpdatePlaylist();
    }

    #endregion

    #region Playlist

    public void UpdatePlaylist()
    {
      PlaylistItem playlistItem = new PlaylistItem();

      playlistItem.MediaSource = View.Source;
      playlistItem.DeliveryMethod = Microsoft.SilverlightMediaFramework.Plugins.Primitives.DeliveryMethods.AdaptiveStreaming; //TODO: need to set that: could decide based on the URL format (e.g. if ends at .ism/Manifest is adaptive streaming)

      List<MarkerResource> markerResources = new List<MarkerResource>();
      MarkerResource markerResource = new MarkerResource();
      markerResource.Source = new Uri("ExampleCaptions.xml", UriKind.Relative); //TODO: change
      markerResources.Add(markerResource);
      playlistItem.MarkerResources = markerResources;

      //player.Playlist.Clear(); //skip this to allow going back to previous items from playlist
      player.Playlist.Add(playlistItem);
      player.GoToPlaylistItem(player.Playlist.Count - 1);
    }

    #endregion

  }
}
