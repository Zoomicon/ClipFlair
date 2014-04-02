//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayer.cs
//Version: 20140403

using Utils.Extensions;

using System;
using System.IO;
using System.Linq;

#if !SILVERLIGHT
using System.Web;
#endif

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins;

namespace ClipFlair.MediaPlayer
{

  [TemplatePart(Name = PART_BitrateMonitorElement, Type = typeof(BitrateMonitor))]
  public class MediaPlayer : SMFPlayer
  {

    #region Constants

    public const string MEDIA_LOAD_FILTER = "Media files (*.wmv, *.mp4, *.wma, *.mp3)|*.wmv;*.mp4;*.wma;*.mp3";

    internal const string PART_BitrateMonitorElement = "BitrateMonitorElement";

    private const double CAPTION_REGION_LEFT = 0; //0.05;
    private const double CAPTION_REGION_TOP = 0.05;
    private const double CAPTION_REGION_WIDTH = 1; //0.9; //SMF 2.7 has a bug here, it wraps caption text at the video boundary instead of at the caption region max boundary (as defined by Origin and Extend)
    private const double CAPTION_REGION_HEIGHT = 0.9;

    #endregion
    
    #region Fields

    protected IMediaPlugin activeMediaPlugin;
    protected BitrateMonitor BitrateMonitorElement;
    
    #endregion

    public MediaPlayer()
    {
      if (Application.Current.Host.Settings.EnableGPUAcceleration) //GPU acceleration can be turned on at HTML/ASPX page or at OOB settings for OOB apps
        CacheMode = new BitmapCache(); //TEST    

      AddEventHandlers();

      AllowDrop = true;
    }

    #region --- Properties ---

    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(Uri), typeof(MediaPlayer),
            new FrameworkPropertyMetadata((Uri)null,
            new PropertyChangedCallback(OnSourceChanged)));

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
      MediaPlayer target = (MediaPlayer)d;
      Uri oldSource = (Uri)e.OldValue;
      Uri newSource = target.Source;
      target.OnSourceChanged(oldSource, newSource);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Source property.
    /// </summary>
    protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
    {
      if (newSource == null || newSource.AbsoluteUri == "")
      {
        Playlist.Clear(); //giving an empty source clears the current playlist (this will also stop any current playback)
        return;
      }

      PlaylistItem playlistItem = new PlaylistItem();

      //playlistItem.StartPosition = ... //TODO: see newSource.Fragment and accept Media URIs here to jump directly to time position? (Stop action should maybe also then go there, plus ignore Time value at state load)

      //Set URL//

      string newSourceStr = FixURL(newSource.AbsoluteUri);

      playlistItem.MediaSource = new Uri(newSourceStr);

      string s;
      if (newSourceStr.EndsWith(".ism/manifest", StringComparison.OrdinalIgnoreCase)) //Smooth stream
      {
        playlistItem.DeliveryMethod = DeliveryMethods.AdaptiveStreaming;
        s = newSourceStr.Substring(0, newSourceStr.Length - 13); //".ism/manifest" suffix is 13 chars long (not using Replace method since it doesn't take StringComparison parameter)
      }
      else if (newSourceStr.EndsWith(".ism/manifest(format=mpd-time-csf)", StringComparison.OrdinalIgnoreCase)) //MPEG-DASH (this works)
      {
        playlistItem.DeliveryMethod = DeliveryMethods.AdaptiveStreaming;
        s = newSourceStr.Substring(0, newSourceStr.Length - 34); //".ism/manifest(format=mpd-time-csf)" suffix is 34 chars long (not using Replace method since it doesn't take StringComparison parameter)
      }
      else if (newSourceStr.EndsWith(".mpd", StringComparison.OrdinalIgnoreCase)) //MPEG-DASH (this doesn't seem to work) //TODO: check if server needs to have some MIME type to serve this or client access policy file
      {
        playlistItem.DeliveryMethod = DeliveryMethods.AdaptiveStreaming;
        s = newSourceStr.Substring(0, newSourceStr.Length - 4); //".mpd" suffix is 4 chars long (not using Replace method since it doesn't take StringComparison parameter)
      }
      else
      {
        playlistItem.DeliveryMethod = DeliveryMethods.ProgressiveDownload; //TODO: maybe this should be "Streaming"?
        string ss = newSourceStr.Split('.').Last();
        s = newSourceStr.Substring(0, newSourceStr.Length - ss.Length - 1); //-1 to remove the dot too
      }

      //Set title//
      playlistItem.Title = new Uri(s).GetComponents(UriComponents.Path, UriFormat.Unescaped).Split('/').Last();

      //Set thumbnail//
      playlistItem.ThumbSource = new Uri(s + "_Thumb.jpg"); //TODO: YouTube uses URL of the form "http://img.youtube.com/vi/vV0tjWx9YQ8/0.jpg" (and 1.jpg for smaller thumb)

      /*
            List<MarkerResource> markerResources = new List<MarkerResource>();
            MarkerResource markerResource = new MarkerResource();
            markerResource.Source = new Uri("ExampleCaptions.xml", UriKind.Relative); //TODO: change
            markerResources.Add(markerResource);
            playlistItem.MarkerResources = markerResources;
      */

      Open(playlistItem);
    }

    #endregion

    #region Time

    /// <summary>
    /// Time Dependency Property
    /// </summary>
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register("Time", typeof(TimeSpan), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(TimeSpan.Zero,
            new PropertyChangedCallback(OnTimeChanged)));

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
      MediaPlayer target = (MediaPlayer)d;
      TimeSpan oldTime = (TimeSpan)e.OldValue;
      TimeSpan newTime = (TimeSpan)e.NewValue; //target.Time; //Note: changed this in case it helps keep sync when two videos are playing and trying to sync their times two-ways
      target.OnTimeChanged(oldTime, newTime);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Time property.
    /// </summary>
    protected virtual void OnTimeChanged(TimeSpan oldTime, TimeSpan newTime)
    {
      try
      {
        if (newTime != null && base.PlaybackPosition != newTime) //check against PlayBackPosition, not oldTime to avoid loops
          this.SeekToPosition(newTime);
      }
      catch
      {
        //NOP
      }
    }

    #endregion

    #region Balance

    /// <summary>
    /// Balance Dependency Property
    /// </summary>
    public static readonly DependencyProperty BalanceProperty =
        DependencyProperty.Register("Balance", typeof(double), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnBalanceChanged)));

    /// <summary>
    /// Gets or sets the Balance property
    /// </summary>
    public double Balance
    {
      get { return (double)GetValue(BalanceProperty); }
      set { SetValue(BalanceProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Balance property.
    /// </summary>
    private static void OnBalanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      double oldBalance = (double)e.OldValue;
      double newBalance = target.Balance;
      target.OnBalanceChanged(oldBalance, newBalance);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Balance property.
    /// </summary>
    protected virtual void OnBalanceChanged(double oldBalance, double newBalance)
    {
      if (activeMediaPlugin != null)
        activeMediaPlugin.Balance = newBalance;
    }

    #endregion

    #region CaptionsVisible

    /// <summary>
    /// CaptionsVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionsVisibleProperty =
        DependencyProperty.Register("CaptionsVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(false,
            new PropertyChangedCallback(OnCaptionsVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionsVisible property.
    /// </summary>
    public bool CaptionsVisible
    {
      get { return (bool)GetValue(CaptionsVisibleProperty); }
      set { SetValue(CaptionsVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionsVisible property.
    /// </summary>
    private static void OnCaptionsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldCaptionsVisible = (bool)e.OldValue;
      bool newCaptionsVisible = target.CaptionsVisible;
      target.OnCaptionsVisibleChanged(oldCaptionsVisible, newCaptionsVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionsVisible property.
    /// </summary>
    protected virtual void OnCaptionsVisibleChanged(bool oldCaptionsVisible, bool newCaptionsVisible)
    {
      CaptionsVisibility = (newCaptionsVisible) ? FeatureVisibility.Visible : FeatureVisibility.Hidden;
    }

    #endregion

    #region VideoVisible

    /// <summary>
    /// VideoVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty VideoVisibleProperty =
        DependencyProperty.Register("VideoVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(true,
            new PropertyChangedCallback(OnVideoVisibleChanged)));

    /// <summary>
    /// Gets or sets the VideoVisible property.
    /// </summary>
    public bool VideoVisible
    {
      get { return (bool)GetValue(VideoVisibleProperty); }
      set { SetValue(VideoVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the VideoVisible property.
    /// </summary>
    private static void OnVideoVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldVideoVisible = (bool)e.OldValue;
      bool newVideoVisible = target.VideoVisible;
      target.OnVideoVisibleChanged(oldVideoVisible, newVideoVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the VideoVisible property.
    /// </summary>
    protected virtual void OnVideoVisibleChanged(bool oldVideoVisible, bool newVideoVisible)
    {
      if (MediaPresenterElement != null) //this can be null if media url can't be resolved
      {
        MediaPresenterElement.MaxWidth = (newVideoVisible) ? double.PositiveInfinity : 0;
        MediaPresenterElement.MaxHeight = (newVideoVisible) ? double.PositiveInfinity : 0;
      }
    }

    #endregion

    #region ConsoleVisible

    /// <summary>
    /// ConsoleVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty ConsoleVisibleProperty =
        DependencyProperty.Register("ConsoleVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(false,
            new PropertyChangedCallback(OnConsoleVisibleChanged)));

    /// <summary>
    /// Gets or sets the ConsoleVisible property.
    /// </summary>
    public bool ConsoleVisible
    {
      get { return (bool)GetValue(ConsoleVisibleProperty); }
      set { SetValue(ConsoleVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ConsoleVisible property.
    /// </summary>
    private static void OnConsoleVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldConsoleVisible = (bool)e.OldValue;
      bool newConsoleVisible = target.ConsoleVisible;
      target.OnConsoleVisibleChanged(oldConsoleVisible, newConsoleVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ConsoleVisible property.
    /// </summary>
    protected virtual void OnConsoleVisibleChanged(bool oldConsoleVisible, bool newConsoleVisible)
    {
      if (newConsoleVisible)
      {
        LogLevel = LogLevel.All;
        LoggingConsoleVisibility = FeatureVisibility.Visible;
      }
      else
      {
        LogLevel = LogLevel.None;
        LoggingConsoleVisibility = FeatureVisibility.Disabled;
      }
    }

    #endregion

    #region GraphVisible

    /// <summary>
    /// GraphVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty GraphVisibleProperty =
        DependencyProperty.Register("GraphVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(false,
            new PropertyChangedCallback(OnGraphVisibleChanged)));

    /// <summary>
    /// Gets or sets the GraphVisible property
    /// </summary>
    public bool GraphVisible
    {
      get { return (bool)GetValue(GraphVisibleProperty); }
      set { SetValue(GraphVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the GraphVisible property.
    /// </summary>
    private static void OnGraphVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldGraphVisible = (bool)e.OldValue;
      bool newGraphVisible = target.GraphVisible;
      target.OnGraphVisibleChanged(oldGraphVisible, newGraphVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the GraphVisible property.
    /// </summary>
    protected virtual void OnGraphVisibleChanged(bool oldGraphVisible, bool newGraphVisible)
    {
      PlayerGraphVisibility = (newGraphVisible) ? FeatureVisibility.Visible : FeatureVisibility.Disabled;

      if (GraphToggleElement != null)
        GraphToggleElement.Visibility = (newGraphVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region BitrateVisible

    /// <summary>
    /// BitrateVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty BitrateVisibleProperty =
        DependencyProperty.Register("BitrateVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(false,
            new PropertyChangedCallback(OnBitrateVisibleChanged)));

    /// <summary>
    /// Gets or sets the BitrateVisible property
    /// </summary>
    public bool BitrateVisible
    {
      get { return (bool)GetValue(BitrateVisibleProperty); }
      set { SetValue(BitrateVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the BitrateVisible property.
    /// </summary>
    private static void OnBitrateVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldBitrateVisible = (bool)e.OldValue;
      bool newBitrateVisible = target.BitrateVisible;
      target.OnBitrateVisibleChanged(oldBitrateVisible, newBitrateVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the BitrateVisible property.
    /// </summary>
    protected virtual void OnBitrateVisibleChanged(bool oldBitrateVisible, bool newBitrateVisible)
    {
      if (BitrateMonitorElement != null)
        BitrateMonitorElement.Visibility = (newBitrateVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion
        
    #region FullScreenButtonVisible

    /// <summary>
    /// FullScreenButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty FullScreenButtonVisibleProperty =
        DependencyProperty.Register("FullScreenButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(true,
            new PropertyChangedCallback(OnFullScreenButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the FullScreenButtonVisible property.
    /// </summary>
    public bool FullScreenButtonVisible
    {
      get { return (bool)GetValue(FullScreenButtonVisibleProperty); }
      set { SetValue(FullScreenButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the FullScreenButtonVisible property.
    /// </summary>
    private static void OnFullScreenButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldFullScreenButtonVisible = (bool)e.OldValue;
      bool newFullScreenButtonVisible = target.FullScreenButtonVisible;
      target.OnFullScreenButtonVisibleChanged(oldFullScreenButtonVisible, newFullScreenButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the FullScreenButtonVisible property.
    /// </summary>
    protected virtual void OnFullScreenButtonVisibleChanged(bool oldFullScreenButtonVisible, bool newFullScreenButtonVisible)
    {
      if (FullScreenToggleElement != null)
        FullScreenToggleElement.Visibility = (newFullScreenButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region SlowMotionButtonVisible

    /// <summary>
    /// SlowMotionButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty SlowMotionButtonVisibleProperty =
        DependencyProperty.Register("SlowMotionButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(true,
            new PropertyChangedCallback(OnSlowMotionButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the SlowMotionButtonVisible property.
    /// </summary>
    public bool SlowMotionButtonVisible
    {
      get { return (bool)GetValue(SlowMotionButtonVisibleProperty); }
      set { SetValue(SlowMotionButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the SlowMotionButtonVisible property.
    /// </summary>
    private static void OnSlowMotionButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldSlowMotionButtonVisible = (bool)e.OldValue;
      bool newSlowMotionButtonVisible = target.SlowMotionButtonVisible;
      target.OnSlowMotionButtonVisibleChanged(oldSlowMotionButtonVisible, newSlowMotionButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the SlowMotionButtonVisible property.
    /// </summary>
    protected virtual void OnSlowMotionButtonVisibleChanged(bool oldSlowMotionButtonVisible, bool newSlowMotionButtonVisible)
    {
      if (SlowMotionElement != null)
        SlowMotionElement.Visibility = (newSlowMotionButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region ReplayButtonVisible

    /// <summary>
    /// ReplayButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty ReplayButtonVisibleProperty =
        DependencyProperty.Register("ReplayButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(true,
            new PropertyChangedCallback(OnReplayButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the ReplayButtonVisible property.
    /// </summary>
    public bool ReplayButtonVisible
    {
      get { return (bool)GetValue(ReplayButtonVisibleProperty); }
      set { SetValue(ReplayButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ReplayButtonVisible property.
    /// </summary>
    private static void OnReplayButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldReplayButtonVisible = (bool)e.OldValue;
      bool newReplayButtonVisible = target.ReplayButtonVisible;
      target.OnReplayButtonVisibleChanged(oldReplayButtonVisible, newReplayButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ReplayButtonVisible property.
    /// </summary>
    protected virtual void OnReplayButtonVisibleChanged(bool oldReplayButtonVisible, bool newReplayButtonVisible)
    {
      if (ReplayElement != null)
        ReplayElement.Visibility = (newReplayButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region RewindButtonVisible

    /// <summary>
    /// RewindButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty RewindButtonVisibleProperty =
        DependencyProperty.Register("RewindButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(true,
            new PropertyChangedCallback(OnRewindButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the RewindButtonVisible property.
    /// </summary>
    public bool RewindButtonVisible
    {
      get { return (bool)GetValue(RewindButtonVisibleProperty); }
      set { SetValue(RewindButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the RewindButtonVisible property.
    /// </summary>
    private static void OnRewindButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldRewindButtonVisible = (bool)e.OldValue;
      bool newRewindButtonVisible = target.RewindButtonVisible;
      target.OnRewindButtonVisibleChanged(oldRewindButtonVisible, newRewindButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the RewindButtonVisible property.
    /// </summary>
    protected virtual void OnRewindButtonVisibleChanged(bool oldRewindButtonVisible, bool newRewindButtonVisible)
    {
      if (RewindElement != null)
        RewindElement.Visibility = (newRewindButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region FastForwardButtonVisible

    /// <summary>
    /// FastForwardButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty FastForwardButtonVisibleProperty =
        DependencyProperty.Register("FastForwardButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(true,
            new PropertyChangedCallback(OnFastForwardButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the FastForwardButtonVisible property.
    /// </summary>
    public bool FastForwardButtonVisible
    {
      get { return (bool)GetValue(FastForwardButtonVisibleProperty); }
      set { SetValue(FastForwardButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the FastForwardButtonVisible property.
    /// </summary>
    private static void OnFastForwardButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldFastForwardButtonVisible = (bool)e.OldValue;
      bool newFastForwardButtonVisible = target.FastForwardButtonVisible;
      target.OnFastForwardButtonVisibleChanged(oldFastForwardButtonVisible, newFastForwardButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the FastForwardButtonVisible property.
    /// </summary>
    protected virtual void OnFastForwardButtonVisibleChanged(bool oldFastForwardButtonVisible, bool newFastForwardButtonVisible)
    {
      if (FastForwardElement != null)
        FastForwardElement.Visibility = (newFastForwardButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region PlaylistButtonVisible

    /// <summary>
    /// PlaylistButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty PlaylistButtonVisibleProperty =
        DependencyProperty.Register("PlaylistButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(true,
            new PropertyChangedCallback(OnPlaylistButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the PlaylistButtonVisible property.
    /// </summary>
    public bool PlaylistButtonVisible
    {
      get { return (bool)GetValue(PlaylistButtonVisibleProperty); }
      set { SetValue(PlaylistButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the PlaylistButtonVisible property.
    /// </summary>
    private static void OnPlaylistButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldPlaylistButtonVisible = (bool)e.OldValue;
      bool newPlaylistButtonVisible = target.PlaylistButtonVisible;
      target.OnPlaylistButtonVisibleChanged(oldPlaylistButtonVisible, newPlaylistButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the PlaylistButtonVisible property.
    /// </summary>
    protected virtual void OnPlaylistButtonVisibleChanged(bool oldPlaylistButtonVisible, bool newPlaylistButtonVisible)
    {
      if (ShowPlaylistElement != null)
        ShowPlaylistElement.Visibility = (newPlaylistButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region Captions1

    /// <summary>
    /// Captions1 Dependency Property
    /// </summary>
    public static readonly DependencyProperty Captions1Property =
        DependencyProperty.Register("Captions1", typeof(CaptionRegion), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(null,
            new PropertyChangedCallback(OnCaptions1Changed)));

    /// <summary>
    /// Gets or sets the Captions1 property.
    /// </summary>
    public CaptionRegion Captions1
    {
      get { return (CaptionRegion)GetValue(Captions1Property); }
      set { SetValue(Captions1Property, value); }
    }

    /// <summary>
    /// Handles changes to the Captions1 property.
    /// </summary>
    private static void OnCaptions1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      CaptionRegion oldCaptions1 = (CaptionRegion)e.OldValue;
      CaptionRegion newCaptions1 = target.Captions1;
      target.OnCaptions1Changed(oldCaptions1, newCaptions1);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Captions1 property.
    /// </summary>
    protected virtual void OnCaptions1Changed(CaptionRegion oldCaptions1, CaptionRegion newCaptions1)
    {
      StyleCaptions(newCaptions1);
      UpdateCaptions1(newCaptions1); //if media is already loaded try to set the captions for region 1, else they'll be set at OnMediaOpened
    }

    #endregion

    #endregion

    #region --- Methods ---

    public void OpenLocalFile() //Note: this has to be initiated by user action (Silverlight security)
    {
      try
      {
        OpenFileDialog dlg = new OpenFileDialog()
        {
          Filter = MEDIA_LOAD_FILTER,
          FilterIndex = 1 //note: this index is 1-based, not 0-based //OpenFileDialog doesn't seem to have a DefaultExt like SaveFileDialog
        };

        if (dlg.ShowDialog() == true) //TODO: find the parent window
          Open(dlg.File);
      }
      catch (Exception e)
      {
        MessageBox.Show("Loading failed: " + e.Message); //TODO: find the parent window
      }
    }

    public void Open(FileInfo file)
    {
      Open(file.OpenRead(), file.Name); //TODO: when playlist is cleared should close the stream (not sure if can listen for playlistitem lifetime events)
    }

    public void Open(Stream stream, string title = "") //TODO: add support for opening .url (and maybe pinned site shortcuts too if one can pin media files) //doesn't close stream
    {
      Source = null; //clear source URL since we're loading directly from a Stream

      PlaylistItem playlistItem = new PlaylistItem();
      playlistItem.StreamSource = stream;
      playlistItem.Title = title;

      Open(playlistItem);
    }

    public void Open(PlaylistItem playlistItem)
    {
      //Playlist.Clear(); //skip this to allow going back to previous items from playlist
      Playlist.Add(playlistItem);
      GoToPlaylistItem(Playlist.Count - 1);
    }

    public void UpdateCaptions1(CaptionRegion newCaptions1)
    {
      if (Captions == null) return; //must check this since media may not be ready yet (setting captions again [cached at the dependency property] at OnMediaOpened event handler

      Captions.Clear(); //TODO: if we have multiple regions in the future, replace region 1 instead in this collection

      if (newCaptions1 != null)
        Captions.Add(newCaptions1);
    }

    public static void StyleCaptions(CaptionRegion theCaptions)
    {
      if (theCaptions == null) return;

      theCaptions.Style.ShowBackground = ShowBackground.WhenActive; //doesn't seem to work if other than transparent color is used
      theCaptions.Style.BackgroundColor = Colors.Transparent;

      //set caption region (max) bounds
      theCaptions.Style.Origin = new Origin() { Left = new Length() { Unit = LengthUnit.Percent, Value = CAPTION_REGION_LEFT }, Top = new Length() { Unit = LengthUnit.Percent, Value = CAPTION_REGION_TOP } };
      theCaptions.Style.Extent = new Extent() { Width = new Length() { Unit = LengthUnit.Percent, Value = CAPTION_REGION_WIDTH }, Height = new Length() { Unit = LengthUnit.Percent, Value = CAPTION_REGION_HEIGHT } };

      //theCaptions.Style.Padding = new Padding() { Left = new Length() { Unit = LengthUnit.Percent, Value = CAPTION_REGION_LEFT }, Right = { Unit = LengthUnit.Percent, Value = 1-CAPTION_REGION_LEFT-CAPTION_REGION_HEIGHT }, Bottom = { Unit = LengthUnit.Percent, Value = CAPTION_REGION_TOP }, Top = { Unit = LengthUnit.Percent, Value = 1-CAPTION_REGION_TOP-CAPTION_REGION_HEIGHT } }; //this crashes Silverlight

      //theCaptions.Style.Direction = Direction.LeftToRight;

      theCaptions.Style.DisplayAlign = DisplayAlign.After; //align multirow catpions to bottom of region
      theCaptions.Style.TextAlign = TextAlignment.Justify; //horizontally center captions

      theCaptions.Style.WrapOption = TextWrapping.Wrap; //wrap too long captions to next row
      theCaptions.Style.Overflow = Overflow.Dynamic; //extends the area for the captions as needed, up to the given Extent

      foreach (CaptionElement caption in theCaptions.Children)
        StyleCaption(caption);
    } //TODO: should keep styling settings as properties of CaptionGrid

    public static void StyleCaption(TimedTextElement theCaption)
    {
      if (theCaption == null) return;

      theCaption.CaptionElementType = TimedTextElementType.Text;
      theCaption.Style.ShowBackground = ShowBackground.WhenActive;
      theCaption.Style.BackgroundColor = Color.FromArgb(100, 0, 0, 0); //use a semi-transparent background
      theCaption.Style.Color = Colors.White;

      Length length = new Length
     {
       Unit = LengthUnit.Pixel, //must use this, since the default LengthUnit.Cell used at TimedTextStyle constructor is not supported
       Value = 20
     };
      theCaption.Style.FontSize = length;
    }

    protected void ApplyTemplateOverrides()
    {
      if (ShowPlaylistElement != null)
        ShowPlaylistElement.Content = "..."; //don't show "Playlist" text but show "..." instead to avoid localizing it and because it can distract viewers from the captions' text //TODO: should maybe show an Image here or expose this as a property?

      //apply UI template overrides
      OnFullScreenButtonVisibleChanged(!FullScreenButtonVisible, FullScreenButtonVisible);
      OnSlowMotionButtonVisibleChanged(!SlowMotionButtonVisible, SlowMotionButtonVisible);
      OnReplayButtonVisibleChanged(!ReplayButtonVisible, ReplayButtonVisible);
      OnRewindButtonVisibleChanged(!RewindButtonVisible, RewindButtonVisible);
      OnFastForwardButtonVisibleChanged(!FastForwardButtonVisible, FastForwardButtonVisible);
      OnPlaylistButtonVisibleChanged(!PlaylistButtonVisible, PlaylistButtonVisible);

      //the following don't seem to do something:
      /* 
      if (GraphToggleElement!=null) GraphToggleElement.Visibility = Visibility.Collapsed;
      if (ControlStripToggleElement != null) ControlStripToggleElement.Visibility = Visibility.Visible;
      */
    }

    protected void UpdateVolumeElement()
    {
      //patch for SMF to update VolumeElement UI with any already set VolumeLevel
      if (VolumeElement != null) //check this in case some SMF skin doesn't have a volume element
        VolumeElement.VolumeLevel = VolumeLevel;
    }

    #endregion

    #region --- Events ----

    protected virtual void AddEventHandlers()
    {
      //listen for changed to PlaybackPosition and sync with Time
      PlaybackPositionChanged += Player_PlaybackPositionChanged;

      //listen for changes to CaptionsVisibility and sync with CaptionsVisible
      CaptionsVisibilityChanged += Player_CaptionsVisibilityChanged;

      //listen to MediaPlugin registration and keep reference to use for getting Balance property which is not exposed by SMFPlayer 2.7
      MediaPluginRegistered += MediaPlayer_MediaPluginRegistered;
    }

    protected virtual void RemoveEventHandlers()
    {
      PlaybackPositionChanged -= Player_PlaybackPositionChanged;
      CaptionsVisibilityChanged -= Player_CaptionsVisibilityChanged;
      MediaPluginRegistered -= MediaPlayer_MediaPluginRegistered;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      BitrateMonitorElement = GetTemplateChild("BitrateMonitorElement") as BitrateMonitor;

      ApplyTemplateOverrides();

      UpdateVolumeElement(); //patch for SMF bug
    }

    protected override void OnMediaOpened()
    {
      base.OnMediaOpened();
      OnBitrateVisibleChanged(!BitrateVisible, BitrateVisible); //BitrateMonitorElement seems to be shown by default after media opening
      UpdateCaptions1(Captions1);
    }
    
    protected void MediaPlayer_MediaPluginRegistered(object source, CustomEventArgs<IMediaPlugin> args)
    {
      activeMediaPlugin = args.Value;
      activeMediaPlugin.EnableGPUAcceleration = true; //make sure GPU acceleration is used by SSME etc. plugins
      activeMediaPlugin.Balance = Balance;
    }

    protected void Player_PlaybackPositionChanged(object sender, CustomEventArgs<TimeSpan> args)
    {
      TimeSpan newTime = (TimeSpan)args.Value;
      if (Time != newTime) //check this for speedup and to avoid loops
        Time = newTime;
    }

    protected void Player_CaptionsVisibilityChanged(object sender, EventArgs args) //EventArgs.Empty is passed here by SMF (could have been passing the new value of the CaptionsVisibility)
    {
      CaptionsVisible = (CaptionsVisibility == FeatureVisibility.Visible) ? true : false;
    }

    #region Drag & Drop

    protected override void OnDragEnter(DragEventArgs e)
    {
      base.OnDragEnter(e);
      e.Handled = true; //must do this
      //VisualStateManager.GoToState(this, "DragOver", true);
    }

    protected override void OnDragOver(DragEventArgs e)
    {
      base.OnDragOver(e);
      e.Handled = true; //must do this
      //NOP
    }

    protected override void OnDragLeave(DragEventArgs e)
    {
      base.OnDragLeave(e);
      e.Handled = true; //must do this
      //VisualStateManager.GoToState(this, "Normal", true);
    }

    protected override void OnDrop(DragEventArgs e)
    {
      base.OnDrop(e);

      //VisualStateManager.GoToState(this, "Normal", true);

      //we receive an array of FileInfo objects for the list of files that were selected and drag-dropped onto this control.
      if (e.Data == null)
        return;

      IDataObject f = e.Data as IDataObject;
      if (f == null) //checks if the dropped objects are files
        return;

      object data = f.GetData(DataFormats.FileDrop); //Silverlight 5 only supports FileDrop - GetData returns null if format is not supported
      FileInfo[] files = data as FileInfo[];

      if (files != null && files.Length > 0) //Use only 1st item from array of FileInfo objects
      {
        //TODO: instead of hardcoding which file extensions to ignore, should have this as property of the control (a ; separated string or an array)
        if (files[0].Name.EndsWith(new string[] { ".clipflair", ".clipflair.zip" }, StringComparison.OrdinalIgnoreCase))
          return;

        e.Handled = true; //must do this

        try
        {
          Open(files[0]);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Loading failed: " + ex.Message); //TODO: find the parent window
          //TODO: maybe should wrap the original exception as inner exception and throw a new one
        }
      }
    }

    #endregion

    #endregion

    #region --- Helpers ---

    /// <summary>
    /// HTTPS not supported for now, changing to HTTP, assuming server supports both with same URL format.
    /// </summary>
    /// <param name="url">the url</param>
    /// <returns>the fixed url</returns>
    private static string FixURL_HTTPS(string url)
    {
      return url.ReplacePrefix("https://", "http://", StringComparison.OrdinalIgnoreCase); //TODO: add support for https:// (now trying to fallback to http://)
    }

    /// <summary>
    /// When people share videos via Dropbox, they can get different URLs depending on whether
    /// they share from their public foler or not and whether they use the OS file context menu's
    /// Dropbox/Share link option or use the dropbox website to create a link to the file.
    /// Need to remove https and use to dl.dropbox.com server to point to the download file.
    /// </summary>
    /// <param name="url">the url</param>
    /// <returns>the fixed url (note: also changing HTTPS to HTTP)</returns>
    private static string FixURL_Dropbox(string url)
    {
      return url.ReplacePrefix(
        new string[] { "https://dl.dropbox.com/s/", "https://www.dropbox.com/s/", "http://www.dropbox.com/s/" },
        "http://dl.dropbox.com/s/", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Append "/manifest" to URIs ending in ".ism" (Smooth Streaming)
    /// </summary>
    /// <param name="url">the url</param>
    /// <returns>the fixed url</returns>
    private static string FixURL_ISM(string url)
    {
      return url.ReplaceSuffix(".ism", ".ism/manifest", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Append "/manifest" to URIs ending in ".ism" (Smooth Streaming)
    /// </summary>
    /// <param name="url">the url</param>
    /// <returns>the fixed url</returns>
    private static string FixURL_ClipFlairStudio(string url)
    {
      String[] videoPrefixes = new string[] { "http://studio.clipflair.net?video=", "http://studio.clipflair.net?clip=", "http://studio.clipflair.net/?video=", "http://studio.clipflair.net/?clip=" };
      String[] audioPrefixes = new string[] { "http://studio.clipflair.net?audio=", "http://studio.clipflair.net/?audio=" };

      if (url.StartsWith(videoPrefixes))
      {
        url = url.ReplacePrefix(videoPrefixes, "");
        if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
          url = "http://gallery.clipflair.net/video/" + url + "/" + url + ".ism/Manifest";
      }
      else if (url.StartsWith(audioPrefixes))
      {
        url = url.ReplacePrefix(audioPrefixes, "");
        if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
          url = "http://gallery.clipflair.net/audio/" + url;
      }
      
      return url;
    }

    /* //THE FOLLOWING WOULD ONLY WORK IF WE COULD GET ANY WEBPAGE CONTENT - NEED TO USE JQUERY VIA JAVASCRIPT BRIDGE TO DO IT
        /// <summary>
        /// Fix YouTube URL
        /// </summary>
        /// <param name="url">the url ( of the form http://www.youtube.com/watch?v=vV0tjWx9YQ8 )</param>
        /// <param name="millisecondsTimeout">milliseconds before timing out (default 30000, use -1 for infinite wait)</param>
        /// <returns>the fixed url</returns>
        private static string FixURL_YouTube(string url, int millisecondsTimeout = 30000)
        {
          url = url.ReplacePrefix(
            new string[]{"http://youtube.com", "https://youtube.com", "https://www.youtube.com"},
            "http://www.youtube.com"
            );
      
          if (!url.StartsWith("http://www.youtube.com"))
            return url;

          string result = "";
          ManualResetEvent completionEvent = new ManualResetEvent(false);
          WebClient client = new WebClient();
          client.DownloadStringCompleted += (s, e) =>
          {
            result = ExtractYouTubeURL(e);
            completionEvent.Set();
          };
          client.DownloadStringAsync(new Uri(url, UriKind.Absolute));
          completionEvent.WaitOne(millisecondsTimeout); 
          return result;
        }

        private static string ExtractYouTubeURL(DownloadStringCompletedEventArgs e)
        {
          if (e.Cancelled || e.Error != null)
            return "";

          string textString = (string)e.Result; //the page content

          Regex rx = new Regex("(?<=url_encoded_fmt_stream_map=)([^(\\\\)]*)(?=\\\\)", RegexOptions.IgnoreCase);
          MatchCollection match = rx.Matches(textString);

          string video_format = match[0].ToString();

          string sep1 = "%2C";
          string sep2 = "%26";
          string sep3 = "%3D";
          string[] videoFormatsGroup = Regex.Split(video_format, sep1);

          for (var i = 0; i < videoFormatsGroup.Length; i++)
          {
            string[] videoFormatsElem = Regex.Split(videoFormatsGroup[i], sep2);
            if (videoFormatsElem.Length < 5) continue;
            string[] partialResult1 = Regex.Split(videoFormatsElem[0], sep3);
            if (partialResult1.Length < 2) continue;
            string url = partialResult1[1];
            url = HttpUtility.UrlDecode(HttpUtility.UrlDecode(url));
            string[] partialResult2 = Regex.Split(videoFormatsElem[4], sep3);
            if (partialResult2.Length < 2) continue;
            int itag = Convert.ToInt32(partialResult2[1]);
            if (itag == 18) //MP4 360p
              return url;
          }

          return null;
        }
    */

    /// <summary>
    /// Fix Media URL
    /// </summary>
    /// <param name="url">the media url</param>
    /// <returns>the fixed url</returns>
    private static string FixURL(string url)
    {
      return FixURL_HTTPS(/*FixURL_YouTube*/(FixURL_ISM(FixURL_Dropbox(FixURL_ClipFlairStudio(url))))); //Fix methods will get called from inside to outside
    }

    #endregion

  }

}
