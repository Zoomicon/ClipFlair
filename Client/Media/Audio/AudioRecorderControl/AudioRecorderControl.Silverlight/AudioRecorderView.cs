﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: AudioRecorderView.cs
//Version: 20150320

using AudioLib;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClipFlair.AudioRecorder
{

  public class AudioRecorderView : INotifyPropertyChanged
  {

    #region --- Constants ---

    public const string MARKER_MAX_PLAYBACK_POSITION = "Playback Limit";

    public const string PROPERTY_BUSY = "Busy";
    public const string PROPERTY_AUDIO = "Audio";
    public const string PROPERTY_MAX_PLAYBACK_DURATION = "MaxPlaybackDuration";
    public const string PROPERTY_MAX_RECORDING_DURATION = "MaxRecordingDuration";

    const double DEFAULT_VOLUME = 1.0; //set playback to highest volume (1.0) - MediaElement's default is 0.5

    #region Messages

    const string MSG_NO_AUDIO = "No access to audio device";
    const string MSG_RECORD_OR_LOAD = "Press Record or Load audio file";
    const string MSG_RECORDING = "Recording... Press Rec again to stop";
    const string MSG_RECORDED = "Recording finished. You may play or save your record";
    const string MSG_RECORD_FAILED = "Could not record: ";
    const string MSG_PLAY_FAILED = "Could not play: ";
    const string MSG_LOAD_FILE_FILTER = "Audio files (*.wav, *.mp3)|*.wav;*.mp3|WAV files (*.wav)|*.wav|MP3 files (*.mp3)|*.mp3";
    const string MSG_SAVE_FILE_FILTER_WAV = "WAV files (*.wav)|*.wav";
    const string MSG_SAVE_FILE_FILTER_MP3 = "MP3 files (*.mp3)|*.mp3"; 
    const string MSG_LOADING = "Loading...";
    const string MSG_LOADED = "Loaded OK";
    const string MSG_LOAD_FAILED = "Could not load: ";
    const string MSG_SAVING = "Saving...";
    const string MSG_SAVED = "Saved OK";
    const string MSG_SAVE_FAILED = "Could not save: ";
    const string MSG_NO_AUDIO_TO_SAVE = "No audio available to save";

    #endregion

    #endregion

    #region --- Fields ---

    private bool busy = false;
    
    private AudioStream _audio;
    private MediaStreamSource mediaStreamSource; //WaveMediaStreamSource or MP3MediaStreamSource

    private MediaElement player; //= null //Note: we shouldn't instantiate a MediaElement in code without adding it to a visual tree (won't play audio), better do the instantiation directly in XAML and then pass it to this class
    
    private MemoryAudioSink _sink;
    private CaptureSource _captureSource;

    private TimelineMarker _maxPlaybackMarker = new TimelineMarker() { Text = MARKER_MAX_PLAYBACK_POSITION };    
    private TimeSpan _maxRecordingDuration;    

    #endregion

    #region --- Initialiation ---

    public AudioRecorderView()
    {

      RecordCommand = new ToggleCommand()
      {
        IsEnabled = true,
        IsChecked = false,
        ExecuteAction = () => Record(),
        ExecuteUncheckAction = () => StopRecording()
      };

      PlayCommand = new ToggleCommand()
      {
        IsEnabled = false,
        IsChecked = false,
        ExecuteAction = () => Play(),
        ExecuteUncheckAction = () => StopPlayback()
      };

      LoadCommand = new Command()
      {
        IsEnabled = true,
        ExecuteAction = () => LoadFile()
      };

      SaveCommand = new Command()
      {
        IsEnabled = false,
        ExecuteAction = () => SaveFile()
      };

      Status = MSG_RECORD_OR_LOAD;
    }

    #endregion

    #region --- Properties ---

    #region Commands

    //using public Commands and protected handler methods to keep the buttons state consistent. Outsiders should talk to the commands, not the handlers
    public ToggleCommand RecordCommand { get; private set; }
    public ToggleCommand PlayCommand { get; private set; }
    public Command LoadCommand { get; private set; }
    public Command SaveCommand { get; private set; }

    #endregion

    #region Status

    public string Status { get; set; }

    public bool Busy
    {
      get { return busy; }
      set
      {
        if (busy != value)
        {
          busy = value;
          RaisePropertyChanged(PROPERTY_BUSY);
        }
      }
    }

    #endregion

    #region Audio

    public bool CanRecord
    {
      get { return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess(); }
    }

    public bool HasAudio
    {
      get { return (Audio != null); }
    }

    public AudioStream Audio
    {
      get { return _audio; }
      set
      {
        if (_audio != value)
        {
          _audio = value;

          bool audioAvailable = (_audio != null); //is there any recorded audio?
          RecordCommand.IsEnabled = true;
          LoadCommand.IsEnabled = true;
          PlayCommand.IsEnabled = audioAvailable; //need to do this here too to conver the case where null audio is set (else Play button appears). Enabling it also at "MediaOpened" event and disabling it at "MediaFailed"
          SaveCommand.IsEnabled = audioAvailable;

          mediaStreamSource = (audioAvailable)? _audio.GetMediaStreamSource() : null;
          if (mediaStreamSource != null)
            player.SetSource(mediaStreamSource); //must set the source once, not every time we play the same audio, else with Mp3MediaSource it will throw DRM error
          else
            player.Source = null;

          RaisePropertyChanged(PROPERTY_AUDIO);
        }
      }
    }

    public TimeSpan MaxPlaybackDuration
    {
      get { return _maxPlaybackMarker.Time; }
      set
      {
        if (_maxPlaybackMarker.Time != value)
        {
          _maxPlaybackMarker.Time = value;
          RaisePropertyChanged(PROPERTY_MAX_PLAYBACK_DURATION);
        }
      }
    }

    public TimeSpan MaxRecordingDuration
    {
      get { return _maxRecordingDuration; }
      set
      {
        if (_maxRecordingDuration != value)
        {
          _maxRecordingDuration = value; //TODO: need to check the recording position while recording to stop (could also crop existing audio if it is in WAV form [instead of MP3] in memory)
          RaisePropertyChanged(PROPERTY_MAX_RECORDING_DURATION);
        }
      }
    }

    public double Volume
    {
      get { return player.Volume; }
      set { player.Volume = value; }
    }

    public MediaElement Player
    {
      get { return player; }
      set
      {
        if (player != null)
        {
          player.MediaOpened -= Player_MediaOpened;
          player.MediaFailed -= Player_MediaFailed;
          player.MediaEnded -= Player_MediaEnded;
          player.MarkerReached -= Player_MarkerReached;
        }

        player = value;

        if (player != null)
        {
          player.MediaOpened += Player_MediaOpened;
          player.MediaFailed += Player_MediaFailed;
          player.MediaEnded += Player_MediaEnded;
          player.MarkerReached += Player_MarkerReached;

          player.AutoPlay = false;
          player.PlaybackRate = 1.0;
          player.Balance = 0;
          Volume = DEFAULT_VOLUME;
        }
      }
    }

    #endregion

    #endregion

    #region --- Methods ---

    public void Reset()
    {
      RecordCommand.IsEnabled = true;
      PlayCommand.IsEnabled = false;
      LoadCommand.IsEnabled = true;
      SaveCommand.IsEnabled = false;

      Status = MSG_RECORD_OR_LOAD;
    }

    #region Recording

    public static AudioFormat PickAudioFormat(AudioCaptureDevice audioCaptureDevice, AudioFormatEx desiredFormat)
    {
      return AudioFormatEx.PickAudioFormat(audioCaptureDevice.SupportedFormats, desiredFormat);
    }

    public void Record()
    {
      Busy = true;

      if (!CanRecord)
      {
        RecordCommand.IsChecked = false; //depress recording toggle button //don't talk to ToggleButton directly
        Reset();
        Status = MSG_NO_AUDIO;
        MessageBox.Show(Status); //TODO: find parent window
        Busy = false;
        return;
      }

      try
      {
        if (_captureSource == null) //do not try to run this code at construction time, since we need to first request audio device access
        {
          AudioCaptureDevice audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
          if (audioDevice != null)
          {
            audioDevice.DesiredFormat = PickAudioFormat(audioDevice, new AudioFormatEx(WaveFormatType.Pcm, 1, 16, 44100)); //mono, 16-bit, 44.1Khz (only supporting 16-bit playback, make sure we record at same bit depth) //if not found, will set to null which is the default value
            _captureSource = new CaptureSource() { AudioCaptureDevice = audioDevice };
          }
        }  //TODO: maybe do something if _captureSource is still null here

        _sink = new MemoryAudioSink(); //TODO: should we dispose previous sink if any? (esp. if it uses a backing memory stream)
        _sink.CaptureSource = _captureSource;
        _captureSource.Start(); //assuming captureSource is stopped

        Status = MSG_RECORDING; //keep recording message since we can't depress the button (TODO: may add Uncheck method to ToggleCommand)

        RecordCommand.IsEnabled = true; //need this enabled since it's a ToggleCommand
        LoadCommand.IsEnabled = false;
        PlayCommand.IsEnabled = false;
        SaveCommand.IsEnabled = false;
      }
      catch (Exception e)
      {
        RecordCommand.IsChecked = false; //depress recording toggle button //don't talk to ToggleButton directly
        Reset();
        Status = MSG_RECORD_FAILED + e.Message;
        Busy = false;
        MessageBox.Show(Status); //TODO: find parent window
      }

    }

    public void StopRecording()
    {
      if ((_captureSource == null) || (_captureSource.State != CaptureState.Started))
      {
        Busy = false;
        return;
      }

      _captureSource.Stop();

      try
      {
        MemoryStream recordedWAV = new MemoryStream(WavManager.WAV_HEADER_SIZE + (int)_sink.BackingStream.Length); //setting initial capacity of MemoryStream to avoid having it resize multiple times (default is 0 if not given)
        WavManager.SavePcmToWav(_sink.BackingStream, recordedWAV, new AudioFormatEx(_sink.CurrentFormat));
        _sink.CloseStream(); //close the backing stream, will be reallocated at next recording

        Audio = new AudioStream(recordedWAV, AudioStreamKind.WAV); //must set the Audio property after having a correct WAV file (generated by WavManager.SavePcmToWav)

        Status = MSG_RECORDED;

        RecordCommand.IsEnabled = true;
        LoadCommand.IsEnabled = true;
        //PlayCommand.IsEnabled = flag; //do not use this, enabling it at "MediaOpened" event and disabling it at "MediaFailed"
        SaveCommand.IsEnabled = true;
      }
      catch (Exception e)
      {
        Audio = null;
        Reset();
        Status = MSG_RECORD_FAILED + e.Message;
        MessageBox.Show(Status); //TODO: find parent window
      }
      finally
      {
        Busy = false;
      }

    }

    #endregion

    #region Playback

    private void PlayCommandCheck()
    {
      PlayCommand.IsChecked = true; //visually press playback toggle button //don't talk to ToggleButton directly
    }

    private void PlayCommandUncheck()
    {
      PlayCommand.IsChecked = false; //visually depress playback toggle button //don't talk to ToggleButton directly
    }

    public void Play()
    {
      if (Busy) return;

      if (!HasAudio)
      {
        PlayCommandUncheck();
        Status = MSG_RECORD_OR_LOAD; //prompt user to record audio or load a WAV file
        return;
      }

      try
      {
        StopPlayback();
        PlayCommandCheck(); //press play button //don't talk to ToggleButton directly
        player.Play();
      }
      catch (Exception e)
      {
        PlayCommandUncheck(); //depress playback toggle button //don't talk to ToggleButton directly
        Status = MSG_PLAY_FAILED + e.Message;
      }
    }

    public void StopPlayback()
    {
      try
      {
        player.Stop(); //stops any playback and moves playback position to start (time 0)
      }
      catch (Exception)
      {
        //NOP
      }
      finally
      {
        //do not call "PlayCommandUncheck" here, will do infinite loop, since it results in "StopPlayback" getting called
        Busy = false;
      }
    }

    #endregion

    #region Load-Save

    public void LoadFile() //this has to be called by user-initiated event handler
    {
      OpenFileDialog openFileDialog = new OpenFileDialog()
      {
        Filter = MSG_LOAD_FILE_FILTER
      };

      if (openFileDialog.ShowDialog() == false)
      {
        Busy = false;
        return;
      }

      Busy = true;

      try
      {
        Status = MSG_LOADING;

        FileInfo f = openFileDialog.File;
        using (Stream stream = f.OpenRead())
        {
          MemoryStream m = new MemoryStream((int)f.Length);
          LoadAudio(stream, m);
          
          Audio = new AudioStream(m, f.Extension);
        }

        Status = MSG_LOADED;
      }
      catch (Exception e)
      {
        Audio = null;

        Reset();
        Status = MSG_LOAD_FAILED + e.Message;
        MessageBox.Show(Status); //TODO: find parent window
      }
      finally
      {
        Busy = false;
      }
    }

    public void SaveFile() //this has to be called by user-initiated event handler
    {
      if (!HasAudio)
      {
        MessageBox.Show(MSG_NO_AUDIO_TO_SAVE);
        Busy = false;
        return;
      }

      SaveFileDialog saveFileDialog = new SaveFileDialog();
      switch (Audio.Kind)
      {
        case AudioStreamKind.WAV:
          saveFileDialog.Filter = MSG_SAVE_FILE_FILTER_WAV;
          break;
        case AudioStreamKind.MP3:
          saveFileDialog.Filter = MSG_SAVE_FILE_FILTER_MP3;
          break;
        default:
          Busy = false;
          return;
      };

      if (saveFileDialog.ShowDialog() == false)
      {
        Busy = false;
        return;
      }

      Busy = true;

      try
      {
        Status = MSG_SAVING;

        using (Stream stream = saveFileDialog.OpenFile())
        {
          SaveAudio(stream, Audio);
          stream.Flush(); //write any buffers to file
        }

        Status = MSG_SAVED;
      }
      catch (Exception e)
      {
        Status = MSG_SAVE_FAILED + e.Message;
        MessageBox.Show(Status); //TODO: find parent window
      }
      finally
      {
        Busy = false;
      }
    }

    public static void LoadAudio(Stream stream, Stream target, int bufferSize = 4096) //does not close the stream
    {
      stream.CopyTo(target, bufferSize);
    }

    public static void SaveAudio(Stream stream, AudioStream source) //does not close the stream
    {
      if (source == null) return; //when no source is available, not writing anything to the stream

      Stream s = source.Data;
      long originalPosition = s.Position; //keep position to restore at the end
      s.Position = 0; //reset position to 0
      s.CopyTo(stream); //default buffer size is 4096
      s.Position = originalPosition; //restore position
    }

    #endregion

    #endregion

    #region --- Events ---

    #region MediaElement Events

    protected void Player_MediaOpened(object sender, RoutedEventArgs e)
    {
      PlayCommand.IsEnabled = true;
      player.Markers.Add(_maxPlaybackMarker); //any existing markers have already been removed by MediaElement before raising this event
    }

    protected void Player_MediaFailed(object sender, RoutedEventArgs e)
    {
      PlayCommandUncheck();
      PlayCommand.IsEnabled = false;
    } //do not remove media marker here, since MediaOpened is called once after recording or loading the audio, but this one is called at each fail of playback

    protected void Player_MediaEnded(object sender, RoutedEventArgs e)
    {
      PlayCommandUncheck(); //depress play button //don't talk to ToggleButton directly
    } //do not remove media marker here, since MediaOpened is called once after recording or loading the audio, but this one is called at each end of playback

    void Player_MarkerReached(object sender, TimelineMarkerRoutedEventArgs e)
    {
      if (e.Marker.Text.Equals(MARKER_MAX_PLAYBACK_POSITION)) //do not use e.Marker == _maxPlaybackMarker, seems the marker we give is wrapped or cloned by other marker before raising this event
        PlayCommandUncheck(); //this also results in "StopPlayback" getting called
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged(string PropertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
    }

    #endregion

    #endregion


  }
}
