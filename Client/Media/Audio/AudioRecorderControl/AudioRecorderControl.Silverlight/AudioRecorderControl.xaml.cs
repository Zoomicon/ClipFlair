//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: AudioRecorderControl.xaml.cs
//Version: 20150323

using AudioLib;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ClipFlair.AudioRecorder
{
  public partial class AudioRecorderControl : UserControl
  {

     #region --- Constants ---
 
     public static readonly Brush COLOR_EXCESS_DURATION = new SolidColorBrush(Colors.Red);
     public static readonly Brush COLOR_NORMAL_DURATION = new SolidColorBrush(Colors.Green);
 
     #endregion

    #region --- Initialization ---

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

    #endregion

    #region --- Properties ---

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
          LimitPlayback = View.LimitPlayback;
          LimitRecording = View.LimitRecording;
          break;
        case AudioRecorderView.PROPERTY_AUDIO:
          Audio = View.Audio;
          break;
        case AudioRecorderView.PROPERTY_DURATION:
          CheckDurationLimit();
          break;
        case AudioRecorderView.PROPERTY_MAX_PLAYBACK_DURATION:
          MaxPlaybackDuration = View.MaxPlaybackDuration;
          CheckDurationLimit();
          break;
        case AudioRecorderView.PROPERTY_MAX_RECORDING_DURATION:
          MaxRecordingDuration = View.MaxRecordingDuration;
          break;
        case AudioRecorderView.PROPERTY_LIMIT_PLAYBACK:
          LimitPlayback = View.LimitPlayback;
          break;
        case AudioRecorderView.PROPERTY_LIMIT_RECORDING:
          LimitRecording = View.LimitRecording;
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

    #region LimitPlayback

    /// <summary>
    /// LimitPlayback Dependency Property
    /// </summary>
    public static readonly DependencyProperty LimitPlaybackProperty =
        DependencyProperty.Register(AudioRecorderView.PROPERTY_LIMIT_PLAYBACK, typeof(bool), typeof(AudioRecorderControl),
            new FrameworkPropertyMetadata(AudioRecorderView.DEFAULT_LIMIT_PLAYBACK,
                new PropertyChangedCallback(OnLimitPlaybackChanged)));

    /// <summary>
    /// Gets or sets the LimitPlayback property.
    /// </summary>
    public bool LimitPlayback
    {
      get { return (bool)GetValue(LimitPlaybackProperty); }
      set { SetValue(LimitPlaybackProperty, value); }
    }

    /// <summary>
    /// Handles changes to the LimitPlayback property.
    /// </summary>
    private static void OnLimitPlaybackChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      AudioRecorderControl target = (AudioRecorderControl)d;
      bool oldLimitPlayback = (bool)e.OldValue;
      bool newLimitPlayback = target.LimitPlayback;
      target.OnLimitPlaybackChanged(oldLimitPlayback, newLimitPlayback);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the LimitPlayback property.
    /// </summary>
    protected virtual void OnLimitPlaybackChanged(bool oldLimitPlayback, bool newLimitPlayback)
    {
      View.LimitPlayback = newLimitPlayback;
    }

    #endregion

    #region LimitRecording

    /// <summary>
    /// LimitRecording Dependency Property
    /// </summary>
    public static readonly DependencyProperty LimitRecordingProperty =
        DependencyProperty.Register(AudioRecorderView.PROPERTY_LIMIT_RECORDING, typeof(bool), typeof(AudioRecorderControl),
            new FrameworkPropertyMetadata(AudioRecorderView.DEFAULT_LIMIT_RECORDING,
                new PropertyChangedCallback(OnLimitRecordingChanged)));

    /// <summary>
    /// Gets or sets the LimitRecording property.
    /// </summary>
    public bool LimitRecording
    {
      get { return (bool)GetValue(LimitRecordingProperty); }
      set { SetValue(LimitRecordingProperty, value); }
    }

    /// <summary>
    /// Handles changes to the LimitRecording property.
    /// </summary>
    private static void OnLimitRecordingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      AudioRecorderControl target = (AudioRecorderControl)d;
      bool oldLimitRecording = (bool)e.OldValue;
      bool newLimitRecording = target.LimitRecording;
      target.OnLimitRecordingChanged(oldLimitRecording, newLimitRecording);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the LimitRecording property.
    /// </summary>
    protected virtual void OnLimitRecordingChanged(bool oldLimitRecording, bool newLimitRecording)
    {
      View.LimitRecording = newLimitRecording;
    }

    #endregion

    #region MaxPlaybackDuration

    /// <summary>
    /// MaxPlaybackDuration Dependency Property
    /// </summary>
    public static readonly DependencyProperty MaxPlaybackDurationProperty =
        DependencyProperty.Register(AudioRecorderView.PROPERTY_MAX_PLAYBACK_DURATION, typeof(TimeSpan), typeof(AudioRecorderControl),
            new FrameworkPropertyMetadata(AudioRecorderView.DEFAULT_MAX_PLAYBACK_DURATION,
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
            new FrameworkPropertyMetadata(AudioRecorderView.DEFAULT_MAX_RECORDING_DURATION,
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

    #region --- Methods ---

    protected void CheckDurationLimit()
    {
      TimeSpan duration = View.Duration;
      TimeSpan maxDuration = View.MaxPlaybackDuration;
      progress.Minimum = 0;
      progress.Maximum = maxDuration.TotalMilliseconds;
      progress.Value = duration.TotalMilliseconds;
      progress.Foreground = (duration > maxDuration) ? COLOR_EXCESS_DURATION : COLOR_NORMAL_DURATION;
    }

    #endregion

    #region --- Events ---

    private void MouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e)
    {
      View.Busy = true;
    }

    #endregion

  }

}
