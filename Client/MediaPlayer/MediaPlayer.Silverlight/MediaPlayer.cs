//Filename: MediaPlayer.cs
//Version: 20120903

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
      OnIsCaptionsVisibleChanged(!IsCaptionsVisible, IsCaptionsVisible);
      OnIsFullScreenButtonVisibleChanged(!IsFullScreenButtonVisible, IsFullScreenButtonVisible);
      OnIsSlowMotionButtonVisibleChanged(!IsSlowMotionButtonVisible, IsSlowMotionButtonVisible);
      OnIsReplayButtonVisibleChanged(!IsReplayButtonVisible, IsReplayButtonVisible);
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
 
      //listen for changes to CaptionsVisibility and sync with IsCaptionsVisible
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
      IsCaptionsVisible = (CaptionsVisibility == FeatureVisibility.Visible) ? true : false;
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

    #region IsCaptionsVisible

    /// <summary>
    /// IsCaptionsVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsCaptionsVisibleProperty =
        DependencyProperty.Register("IsCaptionsVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata((bool)false,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnIsCaptionsVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsCaptionsVisible property.
    /// </summary>
    public bool IsCaptionsVisible
    {
      get { return (bool)GetValue(IsCaptionsVisibleProperty); }
      set { SetValue(IsCaptionsVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsCaptionsVisible property.
    /// </summary>
    private static void OnIsCaptionsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldIsCaptionsVisible = (bool)e.OldValue;
      bool newIsCaptionsVisible = target.IsCaptionsVisible;
      target.OnIsCaptionsVisibleChanged(oldIsCaptionsVisible, newIsCaptionsVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsCaptionsVisible property.
    /// </summary>
    protected virtual void OnIsCaptionsVisibleChanged(bool oldIsCaptionsVisible, bool newIsCaptionsVisible)
    {
      CaptionsVisibility = (newIsCaptionsVisible) ? FeatureVisibility.Visible : FeatureVisibility.Hidden;
    }

    #endregion

    #region IsFullScreenButtonVisible

    /// <summary>
    /// IsFullScreenButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsFullScreenButtonVisibleProperty =
        DependencyProperty.Register("IsFullScreenButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata((bool)true,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnIsFullScreenButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsFullScreenButtonVisible property.
    /// </summary>
    public bool IsFullScreenButtonVisible
    {
      get { return (bool)GetValue(IsFullScreenButtonVisibleProperty); }
      set { SetValue(IsFullScreenButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsFullScreenButtonVisible property.
    /// </summary>
    private static void OnIsFullScreenButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldIsFullScreenButtonVisible = (bool)e.OldValue;
      bool newIsFullScreenButtonVisible = target.IsFullScreenButtonVisible;
      target.OnIsFullScreenButtonVisibleChanged(oldIsFullScreenButtonVisible, newIsFullScreenButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsFullScreenButtonVisible property.
    /// </summary>
    protected virtual void OnIsFullScreenButtonVisibleChanged(bool oldIsFullScreenButtonVisible, bool newIsFullScreenButtonVisible)
    {
      if (FullScreenToggleElement != null)
        FullScreenToggleElement.Visibility = (newIsFullScreenButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region IsSlowMotionButtonVisible

    /// <summary>
    /// IsSlowMotionButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsSlowMotionButtonVisibleProperty =
        DependencyProperty.Register("IsSlowMotionButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata((bool)true,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnIsSlowMotionButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsSlowMotionButtonVisible property.
    /// </summary>
    public bool IsSlowMotionButtonVisible
    {
      get { return (bool)GetValue(IsSlowMotionButtonVisibleProperty); }
      set { SetValue(IsSlowMotionButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsSlowMotionButtonVisible property.
    /// </summary>
    private static void OnIsSlowMotionButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldIsSlowMotionButtonVisible = (bool)e.OldValue;
      bool newIsSlowMotionButtonVisible = target.IsSlowMotionButtonVisible;
      target.OnIsSlowMotionButtonVisibleChanged(oldIsSlowMotionButtonVisible, newIsSlowMotionButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsSlowMotionButtonVisible property.
    /// </summary>
    protected virtual void OnIsSlowMotionButtonVisibleChanged(bool oldIsSlowMotionButtonVisible, bool newIsSlowMotionButtonVisible)
    {
      if (SlowMotionElement != null) 
        SlowMotionElement.Visibility = (newIsSlowMotionButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region IsReplayButtonVisible

    /// <summary>
    /// IsReplayButtonVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsReplayButtonVisibleProperty =
        DependencyProperty.Register("IsReplayButtonVisible", typeof(bool), typeof(MediaPlayer),
            new FrameworkPropertyMetadata((bool)true,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnIsReplayButtonVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsReplayButtonVisible property.
    /// </summary>
    public bool IsReplayButtonVisible
    {
      get { return (bool)GetValue(IsReplayButtonVisibleProperty); }
      set { SetValue(IsReplayButtonVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsReplayButtonVisible property.
    /// </summary>
    private static void OnIsReplayButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayer target = (MediaPlayer)d;
      bool oldIsReplayButtonVisible = (bool)e.OldValue;
      bool newIsReplayButtonVisible = target.IsReplayButtonVisible;
      target.OnIsReplayButtonVisibleChanged(oldIsReplayButtonVisible, newIsReplayButtonVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsReplayButtonVisible property.
    /// </summary>
    protected virtual void OnIsReplayButtonVisibleChanged(bool oldIsReplayButtonVisible, bool newIsReplayButtonVisible)
    {
      if (ReplayElement != null) 
        ReplayElement.Visibility = (newIsReplayButtonVisible) ? Visibility.Visible : Visibility.Collapsed;
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

      Captions.Add(region);
   }

    #endregion
  }
  
}
