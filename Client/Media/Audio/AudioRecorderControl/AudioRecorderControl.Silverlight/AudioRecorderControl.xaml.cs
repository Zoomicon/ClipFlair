﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: AudioRecorderControl.xaml.cs
//Version: 20150320

//TODO: fix so that when ToggleCommand is unchecked the respective toggle button listens for respective event and unchecks too

using AudioLib;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClipFlair.AudioRecorder
{
  public partial class AudioRecorderControl : UserControl
  {
 
    public AudioRecorderControl()
    {
      View = new AudioRecorderView(); //must do first
      InitializeComponent();
      View.Player = player;

      //TODO: check if the following are needed
      //Need to use a MouseLeftButtonDownHandler, to handle mouse events first, before any hosting control (and since the Button/ToggleButton handles the ButtonDown events internally in favor of Click event, asking to process handled events too)
      btnRecord.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(MouseLeftButtonDownHandler), true);
      //don't add handler for btnPlay, else play action would just cancel itself (since it doesn't execute if busy)
      btnLoad.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(MouseLeftButtonDownHandler), true);
      btnSave.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(MouseLeftButtonDownHandler), true);

      //Unloaded += (s, e) => View = null; //release property change event handlers
    }

    #region Properties

    #region View

    public AudioRecorderView View
    {
      get { return (AudioRecorderView)DataContext; }
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          View.PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged); //IView inherits from INotifyPropertyChanged
        
        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);

        //set the new view (must do after setting property change event handler)
        DataContext = value;

        View_PropertyChanged(null, new PropertyChangedEventArgs(null)); //notify property change listeners that all properties of the view changed
      }
    }

    protected virtual void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case null: //multiple (not specified) properties have changed, consider all as changed
          Audio = View.Audio;
          MaxPlaybackDuration = View.MaxPlaybackDuration;
          MaxRecordingDuration = View.MaxRecordingDuration;
          break;
        case AudioRecorderView.PROPERTY_AUDIO:
          Audio = View.Audio;
          break;
        case AudioRecorderView.PROPERTY_MAX_PLAYBACK_DURATION:
          MaxPlaybackDuration = View.MaxPlaybackDuration;
          break;
        case AudioRecorderView.PROPERTY_MAX_RECORDING_DURATION:
          MaxRecordingDuration = View.MaxRecordingDuration;
          break;
      }
    }

    #endregion

    #region Audio

    /// <summary>
    /// Audio Dependency Property
    /// </summary>
    public static readonly DependencyProperty AudioProperty =
        DependencyProperty.Register(AudioRecorderView.PROPERTY_AUDIO, typeof(AudioStream), typeof(AudioRecorderControl),
            new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnAudioChanged)));

    /// <summary>
    /// Gets or sets the Audio property.
    /// </summary>
    public AudioStream Audio
    {
      get { return (AudioStream)GetValue(AudioProperty); }
      set { SetValue(AudioProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Audio property.
    /// </summary>
    private static void OnAudioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      AudioRecorderControl target = (AudioRecorderControl)d;
      AudioStream oldAudio = (AudioStream)e.OldValue;
      AudioStream newAudio = target.Audio;
      target.OnAudioChanged(oldAudio, newAudio);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Audio property.
    /// </summary>
    protected virtual void OnAudioChanged(AudioStream oldAudio, AudioStream newAudio)
    {
      View.Audio = newAudio;
    }

    #endregion

    #region MaxPlaybackDuration

    /// <summary>
    /// MaxPlaybackDuration Dependency Property
    /// </summary>
    public static readonly DependencyProperty MaxPlaybackDurationProperty =
        DependencyProperty.Register(AudioRecorderView.PROPERTY_MAX_PLAYBACK_DURATION, typeof(TimeSpan), typeof(AudioRecorderControl),
            new FrameworkPropertyMetadata(TimeSpan.Zero,
                new PropertyChangedCallback(OnMaxPlaybackDurationChanged)));

    /// <summary>
    /// Gets or sets the MaxPlaybackDuration property.
    /// </summary>
    public TimeSpan MaxPlaybackDuration
    {
      get { return (TimeSpan)GetValue(MaxPlaybackDurationProperty); }
      set { SetValue(MaxPlaybackDurationProperty, value); }
    }

    /// <summary>
    /// Handles changes to the MaxPlaybackDuration property.
    /// </summary>
    private static void OnMaxPlaybackDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      AudioRecorderControl target = (AudioRecorderControl)d;
      TimeSpan oldMaxPlaybackDuration = (TimeSpan)e.OldValue;
      TimeSpan newMaxPlaybackDuration = target.MaxPlaybackDuration;
      target.OnMaxPlaybackDurationChanged(oldMaxPlaybackDuration, newMaxPlaybackDuration);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the MaxPlaybackDuration property.
    /// </summary>
    protected virtual void OnMaxPlaybackDurationChanged(TimeSpan oldMaxPlaybackDuration, TimeSpan newMaxPlaybackDuration)
    {
      View.MaxPlaybackDuration = newMaxPlaybackDuration;
    }

    #endregion

    #region MaxRecordingDuration

    /// <summary>
    /// MaxRecordingDuration Dependency Property
    /// </summary>
    public static readonly DependencyProperty MaxRecordingDurationProperty =
        DependencyProperty.Register(AudioRecorderView.PROPERTY_MAX_RECORDING_DURATION, typeof(TimeSpan), typeof(AudioRecorderControl),
            new FrameworkPropertyMetadata(TimeSpan.Zero,
                new PropertyChangedCallback(OnMaxRecordingDurationChanged)));

    /// <summary>
    /// Gets or sets the MaxRecordingDuration property.
    /// </summary>
    public TimeSpan MaxRecordingDuration
    {
      get { return (TimeSpan)GetValue(MaxRecordingDurationProperty); }
      set { SetValue(MaxRecordingDurationProperty, value); }
    }

    /// <summary>
    /// Handles changes to the MaxRecordingDuration property.
    /// </summary>
    private static void OnMaxRecordingDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      AudioRecorderControl target = (AudioRecorderControl)d;
      TimeSpan oldMaxRecordingDuration = (TimeSpan)e.OldValue;
      TimeSpan newMaxRecordingDuration = target.MaxRecordingDuration;
      target.OnMaxRecordingDurationChanged(oldMaxRecordingDuration, newMaxRecordingDuration);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the MaxRecordingDuration property.
    /// </summary>
    protected virtual void OnMaxRecordingDurationChanged(TimeSpan oldMaxRecordingDuration, TimeSpan newMaxRecordingDuration)
    {
      View.MaxRecordingDuration = newMaxRecordingDuration;
    }

    #endregion

    #endregion

    #region Events

    private void MouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e)
    {
      View.Busy = true;
    }

    #endregion

  }

}
