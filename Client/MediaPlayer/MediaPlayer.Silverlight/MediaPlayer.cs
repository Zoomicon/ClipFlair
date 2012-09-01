﻿//Filename: MediaPlayer.cs
//Version: 20120902

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

using Microsoft.SilverlightMediaFramework;
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Core.Media;

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

    protected virtual void AddEventHandlers()
    {
      //listen for changes to CaptionsVisibility and sync with IsCaptionsVisible
      CaptionsVisibilityChanged += (s, e) =>
      {
        IsCaptionsVisible = (CaptionsVisibility == FeatureVisibility.Visible) ? true : false;
      };
    }

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

      List<MarkerResource> markerResources = new List<MarkerResource>();
      MarkerResource markerResource = new MarkerResource();
      markerResource.Source = new Uri("ExampleCaptions.xml", UriKind.Relative); //TODO: change
      markerResources.Add(markerResource);
      playlistItem.MarkerResources = markerResources;

      //player.Playlist.Clear(); //skip this to allow going back to previous items from playlist
      Playlist.Add(playlistItem);
      GoToPlaylistItem(Playlist.Count - 1);
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

  }

}
