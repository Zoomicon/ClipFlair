//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerWindow.xaml.cs
//Version: 20121203

using ClipFlair.Windows.Views;

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.Windows
{

  [Export("ClipFlair.Windows.Views.MediaPlayerView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MediaPlayerWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new MediaPlayerWindow();
    }
  }

  public partial class MediaPlayerWindow : BaseWindow
  {
    public MediaPlayerWindow()
    {
      View = new MediaPlayerView(); //must set the view first
      InitializeComponent();
      InitializeView();
    }

    #region --- Properties ---

    #region View

    public new IMediaPlayer View //hiding parent property
    {
      get {return (IMediaPlayer)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Time = View.Time;
        Captions = View.Captions;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case IMediaPlayerProperties.PropertyTime:
            Time = View.Time;
            break;
          case IMediaPlayerProperties.PropertyCaptions:
            Captions = View.Captions;
            break;
          //...
        }
    }

    #endregion

    #region Time

    /// <summary>
    /// Time Dependency Property
    /// </summary>
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register(IMediaPlayerProperties.PropertyTime, typeof(TimeSpan), typeof(MediaPlayerWindow),
            new FrameworkPropertyMetadata(IMediaPlayerDefaults.DefaultTime, new PropertyChangedCallback(OnTimeChanged)));

    /// <summary>
    /// Gets or sets the Time property.
    /// </summary>
    public TimeSpan Time
    {
      get { return (TimeSpan)GetValue(TimeProperty); }
      set { SetValue(TimeProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Time property.
    /// </summary>
    private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayerWindow target = (MediaPlayerWindow)d;
      TimeSpan oldTime = (TimeSpan)e.OldValue;
      TimeSpan newTime = target.Time;
      target.OnTimeChanged(oldTime, newTime);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Time property.
    /// </summary>
    protected virtual void OnTimeChanged(TimeSpan oldTime, TimeSpan newTime)
    {
      View.Time = newTime;
    }

    #endregion

    #region Captions

    /// <summary>
    /// Captions Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionsProperty =
        DependencyProperty.Register("Captions", typeof(CaptionRegion), typeof(MediaPlayerWindow),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnCaptionsChanged)));

    /// <summary>
    /// Gets or sets the Captions property.
    /// </summary>
    public CaptionRegion Captions
    {
      get { return (CaptionRegion)GetValue(CaptionsProperty); }
      set { SetValue(CaptionsProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Captions property.
    /// </summary>
    private static void OnCaptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayerWindow target = (MediaPlayerWindow)d;
      CaptionRegion oldCaptions = (CaptionRegion)e.OldValue;
      CaptionRegion newCaptions = target.Captions;
      target.OnCaptionsChanged(oldCaptions, newCaptions);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Captions property.
    /// </summary>
    protected virtual void OnCaptionsChanged(CaptionRegion oldCaptions, CaptionRegion newCaptions)
    {
      View.Captions = newCaptions;
    }

    #endregion
    
    #endregion

    #region Offline 
    //TODO: need Smooth Streaming ISO Cache plugin for SMF from Media Experiences project at codeplex since this now works only for ProgressiveDownload media

    private void btnSaveOffline_Click(object sender, RoutedEventArgs e) //TODO: doesn't seem to work, maybe needs offline cache plugin or respective SMF assemblies
    {
      try
      {
        player.StorePlaylistContentOffline("ClipFlairPlaylist"); //TODO: allow to define offline filename and maybe allow to delete old ones? (or show offline size and allow clear)
        MessageBox.Show("Playlist stored offline"); //TODO: find parent window and pass here
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: " + ex.Message); //(only Progressive Download media is supported currently, can give empty source to clear playlist, then load some non smooth streaming remote URL)
        //TODO: find parent window and pass here
      }
    }

    private void btnLoadOffline_Click(object sender, RoutedEventArgs e) //TODO: doesn't seem to work, maybe needs offline cache plugin or respective SMF assemblies
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

    #endregion

  }

}