//Filename: MediaPlayer.cs
//Version: 20120910

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

namespace Zoomicon.MediaPlayer
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

      //TODO (need to set that when new captions are set) //Captions.GetEnumerator().Current.Style.BackgroundColor = Colors.Transparent; //change black caption region background

      //apply any settings from XAML
      OnSourceChanged(null, Source);
      OnCaptionsVisibleChanged(!CaptionsVisible, CaptionsVisible);
      OnFullScreenButtonVisibleChanged(!FullScreenButtonVisible, FullScreenButtonVisible);
      OnSlowMotionButtonVisibleChanged(!SlowMotionButtonVisible, SlowMotionButtonVisible);
      OnReplayButtonVisibleChanged(!ReplayButtonVisible, ReplayButtonVisible);
    }

    protected override void OnMediaOpened()
    {
      base.OnMediaOpened();
      UpdateMarkers(Markers);
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
                FrameworkPropertyMetadataOptions.None,
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
      PlaylistItem playlistItem = new PlaylistItem();

      playlistItem.MediaSource = newSource;
      playlistItem.DeliveryMethod = Microsoft.SilverlightMediaFramework.Plugins.Primitives.DeliveryMethods.AdaptiveStreaming; //TODO: need to set that: could decide based on the URL format (e.g. if ends at .ism/Manifest is adaptive streaming)

 /*
      List<MarkerResource> markerResources = new List<MarkerResource>();
      MarkerResource markerResource = new MarkerResource();
      markerResource.Source = new Uri("ExampleCaptions.xml", UriKind.Relative); //TODO: change
      markerResources.Add(markerResource);
      playlistItem.MarkerResources = markerResources;
*/

      //player.Playlist.Clear(); //skip this to allow going back to previous items from playlist
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
                FrameworkPropertyMetadataOptions.None,
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
            new FrameworkPropertyMetadata((bool)false,
                FrameworkPropertyMetadataOptions.None,
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

    #region FullScreenButtonVisible

    /// <summary>
    /// FullScreenButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty FullScreenButtonVisibleProperty =
        DependencyProperty.Register("FullScreenButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata((bool)true,
                FrameworkPropertyMetadataOptions.None,
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
            new FrameworkPropertyMetadata((bool)true,
                FrameworkPropertyMetadataOptions.None,
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
            new FrameworkPropertyMetadata((bool)true,
                FrameworkPropertyMetadataOptions.None,
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

    #region Markers

    /// <summary>
    /// Markers Dependency Property
    /// </summary>
    public static readonly DependencyProperty MarkersProperty =
        DependencyProperty.Register("Markers", typeof(MediaMarkerCollection<TimedTextElement>), typeof(MediaPlayer),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnMarkersChanged)));

    /// <summary>
    /// Gets or sets the Markers property.
    /// </summary>
    public MediaMarkerCollection<TimedTextElement> Markers
    {
      get { return (MediaMarkerCollection<TimedTextElement>)GetValue(MarkersProperty); }
      set { SetValue(MarkersProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Markers property.
    /// </summary>
    private static void OnMarkersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      MediaMarkerCollection<TimedTextElement> oldMarkers = (MediaMarkerCollection<TimedTextElement>)e.OldValue;
      MediaMarkerCollection<TimedTextElement> newMarkers = target.Markers;
      target.OnMarkersChanged(oldMarkers, newMarkers);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Markers property.
    /// </summary>
    protected virtual void OnMarkersChanged(MediaMarkerCollection<TimedTextElement> oldMarkers, MediaMarkerCollection<TimedTextElement> newMarkers)
    {
      //NOP (setting up the markers at Media_Opened
    }

    #endregion
    
    #endregion

    #region --- Methods ---

    public void UpdateMarkers(MediaMarkerCollection<TimedTextElement> newMarkers)
    {
      if (newMarkers == null) return;

      CaptionRegion  region = new CaptionRegion();
      region.Style.ShowBackground = ShowBackground.WhenActive; //doesn't seem to work if other than transparent color is used
      region.Style.BackgroundColor = Colors.Transparent;
   
      foreach (CaptionElement marker in newMarkers)
      {
        region.Children.Add(marker);
        StyleMarker(marker);
      }

      Captions.Add(region);
    }

    public static void StyleMarker(TimedTextElement marker)
    {
      marker.CaptionElementType = TimedTextElementType.Text;
      marker.Style.ShowBackground = ShowBackground.WhenActive;
      marker.Style.BackgroundColor = Color.FromArgb(100, 0, 0, 0); //use a semi-transparent background
      marker.Style.Color = Colors.White;
      //marker.Style.TextAlign = TextAlignment.Center;
      Length length = new Length
      {
        Unit = LengthUnit.Pixel, //must use this, since the default LengthUnit.Cell used at TimedTextStyle constructor is not supported
        Value = 20
      };
      marker.Style.FontSize = length;
    }
  
    #endregion
  }
  
}
