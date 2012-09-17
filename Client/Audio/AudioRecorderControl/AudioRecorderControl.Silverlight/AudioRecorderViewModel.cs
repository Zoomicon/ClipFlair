//Filename: AudioRecorderViewModel.cs
//Version: 20120912

using Zoomicon.AudioLib;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;

namespace Zoomicon.AudioRecorder
{

  public class AudioRecorderViewModel : DependencyObject
  {

    #region Constants

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

    private MemoryStream theMemStream; //must be a class field for async operations to work correctly with it
    private WaveMediaStreamSource wavMss;

    private MediaElement player = new MediaElement();
    private MemoryAudioSink _sink;
    private CaptureSource _captureSource;
    private OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = MSG_FILE_FILTER };
    private SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = MSG_FILE_FILTER };

    #endregion

    #region Commands

    //using public Commands and protected handler methods to keep the buttons state consistent. Outsiders should talk to the commands, not the handlers
    public ToggleCommand RecordCommand { get; private set; }
    public ToggleCommand PlayCommand { get; private set; }
    public SimpleCommand LoadCommand { get; private set; }
    public SimpleCommand SaveCommand { get; private set; }

    #endregion

    #region StatusText

    public static readonly DependencyProperty StatusTextProperty = DependencyProperty.Register("StatusText", typeof(string), typeof(AudioRecorderViewModel), null);

    public string StatusText
    {
      get { return (string)GetValue(StatusTextProperty); }
      set { SetValue(StatusTextProperty, value); }
    }

    #endregion

    #region Volume

    public double Volume
    {
      get { return player.Volume; }
      set { player.Volume = value; }
    }

    #endregion

    public AudioRecorderViewModel(ToggleButton btnRecord, ToggleButton btnPlay)
    {
      RecordCommand = new ToggleCommand(btnRecord)
      {
        MayBeExecuted = true,
        ExecuteAction = () => Record(),
        ExecuteUncheckAction = () => StopRecording()
      };

      PlayCommand = new ToggleCommand(btnPlay)
      {
        MayBeExecuted = false,
        ExecuteAction = () => Play(),
        ExecuteUncheckAction = () => StopPlayback()
      };

      LoadCommand = new SimpleCommand()
      {
        MayBeExecuted = true,
        ExecuteAction = () => LoadFile()
      };

      SaveCommand = new SimpleCommand()
      {
        MayBeExecuted = false,
        ExecuteAction = () => SaveFile()
      };

      AudioCaptureDevice audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
      _captureSource = new CaptureSource() { AudioCaptureDevice = audioDevice };

      Volume = 1.0; //set to highest volume (1.0), since MediaElement's default is 0.5
      player.MediaEnded += new RoutedEventHandler(MediaElement_MediaEnded); 

      Reset();
    }

    protected void MediaElement_MediaEnded( object sender , RoutedEventArgs e )
    {
      PlayCommand.IsChecked = false; //when playback ends depress play button //don't talk to ToggleButton directly
    }

    private bool EnsureAudioAccess()
    {
      return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
    }

    protected void Reset()
    {
      RecordCommand.MayBeExecuted = true;
      LoadCommand.MayBeExecuted = true;
      PlayCommand.MayBeExecuted = false;
      SaveCommand.MayBeExecuted = false;

      StatusText = MSG_RECORD_OR_LOAD;
    }

    protected void Record()
    {
      if (!EnsureAudioAccess())
      {
        RecordCommand.IsChecked = false; //depress recording toggle button //don't talk to ToggleButton directly
        Reset();
        StatusText = MSG_NO_AUDIO;
        return;
      }
      
      try
      {
        _sink = new MemoryAudioSink();
        _sink.CaptureSource = _captureSource;
        _captureSource.Start(); //assuming captureSource is stopped

        StatusText = MSG_RECORDING; //keep recording message since we can't depress the button (TODO: may add Uncheck method to ToggleCommand)

        RecordCommand.MayBeExecuted = true; //need this enabled since it's a ToggleCommand
        LoadCommand.MayBeExecuted = false;
        PlayCommand.MayBeExecuted = false;
        SaveCommand.MayBeExecuted = false;
      }
      catch (Exception e)
      {
        RecordCommand.IsChecked = false; //depress recording toggle button //don't talk to ToggleButton directly
        Reset();
        StatusText = MSG_RECORD_FAILED + e.Message;
      }

    }

    protected void StopRecording()
    {
      if (_captureSource.State == CaptureState.Started)
      {
        _captureSource.Stop();

        try
        {
          theMemStream = new MemoryStream();
          WavManager.SavePcmToWav(_sink.BackingStream, theMemStream, _sink.CurrentFormat);
 
          StatusText = MSG_RECORDED;

          RecordCommand.MayBeExecuted = true;
          LoadCommand.MayBeExecuted = true;
          PlayCommand.MayBeExecuted = true;
          SaveCommand.MayBeExecuted = true;
        }
        catch (Exception e)
        {
          theMemStream = null;
          Reset();
          StatusText = MSG_RECORD_FAILED + e.Message;
        }

      }
    }
    
    protected void Play()
    {
      if (theMemStream == null)
      {
        PlayCommand.IsChecked = false; //depress playback toggle button //don't talk to ToggleButton directly
        StatusText = MSG_RECORD_OR_LOAD; //prompt user to record audio or load a WAV file
        return;
      }

      try
      {
        wavMss = new WaveMediaStreamSource(theMemStream);
        player.SetSource(wavMss);
        player.Position = TimeSpan.Zero;
        player.Play();
      }
      catch (Exception e)
      {
        PlayCommand.IsChecked = false; //depress playback toggle button //don't talk to ToggleButton directly
        StatusText = MSG_PLAY_FAILED + e.Message;
      }
    }

    protected void StopPlayback()
    {
      try
      {
        player.Stop(); //stops any playback and moves playback position to start (time 0)
      }
      catch (Exception e)
      {
        //NOP
      }
    }

    protected void LoadFile()
    {
      if (openFileDialog.ShowDialog() == false) return;

      try
      {
        StatusText = MSG_LOADING;

        Stream stream = openFileDialog.File.OpenRead();
        theMemStream = new MemoryStream();

        // Append all data from rawData stream into output stream.
        byte[] buffer = new byte[4096];
        int read;       // number of bytes read in one iteration
        while ((read = stream.Read(buffer, 0, 4096)) > 0)
        {
          theMemStream.Write(buffer, 0, read);
        }

        stream.Close();

        StatusText = MSG_LOADED;

        RecordCommand.MayBeExecuted = true;
        LoadCommand.MayBeExecuted = true;
        PlayCommand.MayBeExecuted = true;
        SaveCommand.MayBeExecuted = true;
      }
      catch (Exception e)
      {
        theMemStream = null;
  
        Reset();
        StatusText = MSG_LOAD_FAILED + e.Message;
      }
    }

    protected void SaveFile()
    {
      if (saveFileDialog.ShowDialog() == false) return;

      try
      {
        StatusText = MSG_SAVING;

        Stream stream = saveFileDialog.OpenFile();
 
        // Reset position in memStream and keep its position to restore at the end
        long originalPosition = theMemStream.Position;
        theMemStream.Seek(0, SeekOrigin.Begin);

        // Append all data from rawData stream into output stream.
        byte[] buffer = new byte[4096];
        int read;       // number of bytes read in one iteration
        while ((read = theMemStream.Read(buffer, 0, 4096)) > 0)
          stream.Write(buffer, 0, read);

        theMemStream.Seek(originalPosition, SeekOrigin.Begin); //restore memStream position
 
        stream.Close();
        StatusText = MSG_SAVED;
      }
      catch (Exception e)
      {
        StatusText = MSG_SAVE_FAILED + e.Message;
      }
    }

  }
}
