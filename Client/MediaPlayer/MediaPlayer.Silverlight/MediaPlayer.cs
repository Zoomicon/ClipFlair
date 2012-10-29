//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayer.cs
//Version: 20121029

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using Microsoft.SilverlightMediaFramework;
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace ClipFlair.MediaPlayer
{

  public class MediaPlayer : SMFPlayer
  {

    public MediaPlayer()
    {
      AddEventHandlers();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      ShowPlaylistElement.Content = "..."; //don't show "Playlist" text but show "..." instead to avoid localizing it and because it can distract viewers from the captions' text

      //apply UI template overrides
      OnFullScreenButtonVisibleChanged(!FullScreenButtonVisible, FullScreenButtonVisible);
      OnSlowMotionButtonVisibleChanged(!SlowMotionButtonVisible, SlowMotionButtonVisible);
      OnReplayButtonVisibleChanged(!ReplayButtonVisible, ReplayButtonVisible);
      OnPlaylistButtonVisibleChanged(!PlaylistButtonVisible, PlaylistButtonVisible);

      //the following don't seem to do something:
      /* 
      if (GraphToggleElement!=null) GraphToggleElement.Visibility = Visibility.Collapsed;
      if (ControlStripToggleElement != null) ControlStripToggleElement.Visibility = Visibility.Visible;
      */
    }

    protected override void OnMediaOpened()
    {
      base.OnMediaOpened();
      UpdateCaptions1(Captions1);
    }

    protected virtual void AddEventHandlers()
    {
      //listen for changed to PlaybackPosition and sync with Time
      PlaybackPositionChanged += new EventHandler<CustomEventArgs<TimeSpan>>(Player_PlaybackPositionChanged);

      //listen for changes to CaptionsVisibility and sync with CaptionsVisible
      CaptionsVisibilityChanged += new EventHandler(Player_CaptionsVisibilityChanged);
    }

    protected virtual void RemoveEventHandlers()
    {
      PlaybackPositionChanged -= new EventHandler<CustomEventArgs<TimeSpan>>(Player_PlaybackPositionChanged);
      CaptionsVisibilityChanged -= new EventHandler(Player_CaptionsVisibilityChanged);
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

      string newSourceStr = newSource.AbsoluteUri;

      if (newSourceStr.EndsWith(".ism", StringComparison.OrdinalIgnoreCase))
        newSourceStr = newSourceStr + "/manifest"; //append "/manifest" to URIs ending in ".ism" (Smooth Streaming)

      playlistItem.MediaSource = new Uri(newSourceStr);

      string s;
      if (newSourceStr.EndsWith(".ism/manifest", StringComparison.OrdinalIgnoreCase))
      {
        playlistItem.DeliveryMethod = DeliveryMethods.AdaptiveStreaming;
        s = newSourceStr.Substring(0, newSourceStr.Length - 13); //".ism/manifest" suffix is 13 chars long (not using Replace method since it doesn't take StringComparison parameter)
      }
      else
      {
        playlistItem.DeliveryMethod = DeliveryMethods.ProgressiveDownload; //TODO: maybe this should be "Streaming"?
        string ss = newSourceStr.Split('.').Last();
        s = newSourceStr.Substring(0, newSourceStr.Length - ss.Length - 1); //-1 to remove the dot too
      }
      playlistItem.Title = new Uri(s).GetComponents(UriComponents.Path, UriFormat.Unescaped).Split('/').Last();
      playlistItem.ThumbSource = new Uri(s + "_Thumb.jpg");
      
      /*
            List<MarkerResource> markerResources = new List<MarkerResource>();
            MarkerResource markerResource = new MarkerResource();
            markerResource.Source = new Uri("ExampleCaptions.xml", UriKind.Relative); //TODO: change
            markerResources.Add(markerResource);
            playlistItem.MarkerResources = markerResources;
      */

      //Playlist.Clear(); //skip this to allow going back to previous items from playlist
      Playlist.Add(playlistItem);
      GoToPlaylistItem(Playlist.Count - 1);
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
      TimeSpan newTime = target.Time;
      target.OnTimeChanged(oldTime, newTime);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Time property.
    /// </summary>
    protected virtual void OnTimeChanged(TimeSpan oldTime, TimeSpan newTime)
    {
      if (newTime != null && base.PlaybackPosition != newTime) //check against PlayBackPosition, not oldTime to avoid loops
        this.SeekToPosition(newTime);
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
      MediaPresenterElement.MaxWidth = (newVideoVisible)? double.PositiveInfinity : 0;
      MediaPresenterElement.MaxHeight = (newVideoVisible) ? double.PositiveInfinity : 0;
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

    public void UpdateCaptions1(CaptionRegion newCaptions1)
    {
      Captions.Clear(); //TODO: if we have multiple regions in the future, replace region 1 instead in this collection

      if (newCaptions1 != null)
        Captions.Add(newCaptions1);
    }

    public static void StyleCaptions(CaptionRegion theCaptions)
    {
      if (theCaptions != null)
      {
        theCaptions.Style.ShowBackground = ShowBackground.WhenActive; //doesn't seem to work if other than transparent color is used
        theCaptions.Style.BackgroundColor = Colors.Transparent;

        foreach (CaptionElement caption in theCaptions.Children)
          StyleCaption(caption);
      }
    }

    public static void StyleCaption(TimedTextElement theCaption)
    {
      if (theCaption != null)
      {
        theCaption.CaptionElementType = TimedTextElementType.Text;
        theCaption.Style.ShowBackground = ShowBackground.WhenActive;
        theCaption.Style.BackgroundColor = Color.FromArgb(100, 0, 0, 0); //use a semi-transparent background
        theCaption.Style.Color = Colors.White;
        //theCaption.Style.TextAlign = TextAlignment.Center;
        Length length = new Length
        {
          Unit = LengthUnit.Pixel, //must use this, since the default LengthUnit.Cell used at TimedTextStyle constructor is not supported
          Value = 20
        };
        theCaption.Style.FontSize = length;
      }
    }

    #endregion
  }

}
