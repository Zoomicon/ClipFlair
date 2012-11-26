//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: AudioRecorderView.cs
//Version: 20121125

using ClipFlair.AudioLib;

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ClipFlair.AudioRecorder
{

  public class AudioRecorderView : INotifyPropertyChanged
  {

    #region Constants

    public const string PROPERTY_AUDIO = "Audio";

    const string MSG_NO_AUDIO = "No access to audio device";
    const string MSG_RECORD_OR_LOAD = "Press Record or Load audio file";
    const string MSG_RECORDING = "Recording... Press Rec again to stop";
    const string MSG_RECORDED = "Recording finished. You may play or save your record";
    const string MSG_RECORD_FAILED = "Could not record: ";
    const string MSG_PLAY_FAILED = "Could not play: ";
    const string MSG_FILE_FILTER = "Audio files (*.wav)|*.wav";
    const string MSG_LOADING = "Loading...";
    const string MSG_LOADED = "Loaded OK";
    const string MSG_LOAD_FAILED = "Could not load: ";
    const string MSG_SAVING = "Saving...";
    const string MSG_SAVED = "Saved OK";
    const string MSG_SAVE_FAILED = "Could not save: ";

    #endregion

    #region Fields

    private Stream _audio;
    private WaveMediaStreamSource wavMss;
    private MediaElement player = new MediaElement();
    private MemoryAudioSink _sink;
    private CaptureSource _captureSource;
    private OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = MSG_FILE_FILTER };
    private SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = MSG_FILE_FILTER };

    #endregion

    #region Properties

    #region Commands

    //using public Commands and protected handler methods to keep the buttons state consistent. Outsiders should talk to the commands, not the handlers
    public ToggleCommand RecordCommand { get; private set; }
    public ToggleCommand PlayCommand { get; private set; }
    public Command LoadCommand { get; private set; }
    public Command SaveCommand { get; private set; }

    #endregion

    #region Status

    public string Status { get; set; }

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

    public Stream Audio
    {
      get { return _audio; }
      set
      {
        if (_audio != value)
        {
          _audio = value;

          bool flag = (_audio != null); //is there any recorded audio?
          RecordCommand.IsEnabled = true;
          LoadCommand.IsEnabled = true;
          PlayCommand.IsEnabled = flag;
          SaveCommand.IsEnabled = flag;

          RaisePropertyChanged(PROPERTY_AUDIO);
        }
      }
    }

    #endregion

    #region Volume

    public double Volume
    {
      get { return player.Volume; }
      set { player.Volume = value; }
    }

    #endregion

    #endregion

    #region Constructor

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

      AudioCaptureDevice audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
      _captureSource = new CaptureSource() { AudioCaptureDevice = audioDevice };

      Volume = 1.0; //set to highest volume (1.0), since MediaElement's default is 0.5
      player.MediaEnded += new RoutedEventHandler(MediaElement_MediaEnded);

      Status = MSG_RECORD_OR_LOAD;
    }

    #endregion

    #region Methods

    protected void Reset()
    {
      RecordCommand.IsEnabled = true;
      PlayCommand.IsEnabled = false;
      LoadCommand.IsEnabled = true;
      SaveCommand.IsEnabled = false;

      Status = MSG_RECORD_OR_LOAD;
    }

    protected void Record()
    {
      if (!CanRecord)
      {
        RecordCommand.IsChecked = false; //depress recording toggle button //don't talk to ToggleButton directly
        Reset();
        Status = MSG_NO_AUDIO;
        MessageBox.Show(Status); //TODO: find parent window
        return;
      }

      try
      {
        _sink = new MemoryAudioSink();
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
        MessageBox.Show(Status); //TODO: find parent window
      }

    }

    protected void StopRecording()
    {
      if (_captureSource.State == CaptureState.Started)
      {
        _captureSource.Stop();

        try
        {
          Audio = new MemoryStream();
          WavManager.SavePcmToWav(_sink.BackingStream, Audio, _sink.CurrentFormat);

          Status = MSG_RECORDED;

          RecordCommand.IsEnabled = true;
          LoadCommand.IsEnabled = true;
          PlayCommand.IsEnabled = true;
          SaveCommand.IsEnabled = true;
        }
        catch (Exception e)
        {
          Audio = null;
          Reset();
          Status = MSG_RECORD_FAILED + e.Message;
          MessageBox.Show(Status); //TODO: find parent window
        }

      }
    }

    protected void Play()
    {
      if (!HasAudio)
      {
        PlayCommand.IsChecked = false; //depress playback toggle button //don't talk to ToggleButton directly
        Status = MSG_RECORD_OR_LOAD; //prompt user to record audio or load a WAV file
        return;
      }

      try
      {
        wavMss = new WaveMediaStreamSource(Audio);
        player.SetSource(wavMss);
        player.Position = TimeSpan.Zero;
        player.Play();
      }
      catch (Exception e)
      {
        PlayCommand.IsChecked = false; //depress playback toggle button //don't talk to ToggleButton directly
        Status = MSG_PLAY_FAILED + e.Message;
      }
    }

    protected void StopPlayback()
    {
      try
      {
        player.Stop(); //stops any playback and moves playback position to start (time 0)
      }
      catch (Exception)
      {
        //NOP
      }
    }

    #region Load-Save

    protected void LoadFile()
    {
      if (openFileDialog.ShowDialog() == false) return;

      try
      {
        Status = MSG_LOADING;

        using (Stream stream = openFileDialog.File.OpenRead())
        {
          MemoryStream m = new MemoryStream();
          LoadAudio(stream, m);
          Audio = m;
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
    }

    protected void SaveFile()
    {
      if (!HasAudio)
      {
        MessageBox.Show("No audio available to save");
        return;
      }

      if (saveFileDialog.ShowDialog() == false) return;

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
    }

    public static void LoadAudio(Stream stream, Stream target) //does not close the stream
    {
      stream.CopyTo(target); //default buffer size is 4096
    }

    public static void SaveAudio(Stream stream, Stream source) //does not close the stream
    {
      if (source == null) return; //when no source is available, not writing anything to the stream

      long originalPosition = source.Position; //keep position to restore at the end
      source.Position = 0; //reset position to 0
      source.CopyTo(stream); //default buffer size is 4096
      source.Position = originalPosition; //restore position
    }

    #endregion

    #endregion

    #region Events

    protected void MediaElement_MediaEnded(object sender, RoutedEventArgs e) //TODO: doesn't seem to get always called
    {
      PlayCommand.IsChecked = false; //when playback ends depress play button //don't talk to ToggleButton directly
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

  }
}
