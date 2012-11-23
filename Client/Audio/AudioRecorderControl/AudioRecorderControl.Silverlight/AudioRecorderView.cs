//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: AudioRecorderView.cs
//Version: 20121123

using ClipFlair.AudioLib;

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ClipFlair.AudioRecorder
{

  public class AudioRecorderView : DependencyObject
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

    #region Status

    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(AudioRecorderView), null);

    public string Status
    {
      get { return (string)GetValue(StatusProperty); }
      set { SetValue(StatusProperty, value); }
    }

    #endregion

    #region Audio

    public static readonly DependencyProperty AudioProperty = DependencyProperty.Register("Audio", typeof(Stream), typeof(AudioRecorderView), new FrameworkPropertyMetadata(null, AudioChanged));

    public Stream Audio
    {
      get { return (Stream)GetValue(AudioProperty); }
      set { SetValue(AudioProperty, value); }
    }
 
    private static void AudioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      AudioRecorderView view = (AudioRecorderView)d;
      bool flag = (view.Audio != null); //is there any recorded audio?
      view.RecordCommand.MayBeExecuted = flag;
      view.LoadCommand.MayBeExecuted = true; //set again since it is turned off during recording
      view.PlayCommand.MayBeExecuted = flag;
      view.SaveCommand.MayBeExecuted = flag;
    }

    #endregion

    public double Volume
    {
      get { return player.Volume; }
      set { player.Volume = value; }
    }

    public bool HasAudio 
    {
      get { return (Audio != null); }
    }

    public void SetToggleButtons(ToggleButton btnRecord, ToggleButton btnPlay)
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
    }

    public AudioRecorderView()
    {

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

      Status = MSG_RECORD_OR_LOAD;
    }

    protected void MediaElement_MediaEnded( object sender , RoutedEventArgs e ) //TODO: doesn't seem to get always called
    {
      PlayCommand.IsChecked = false; //when playback ends depress play button //don't talk to ToggleButton directly
    }

    private bool EnsureAudioAccess()
    {
      return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
    }

    protected void Reset()
    {
      if (RecordCommand != null) RecordCommand.MayBeExecuted = true;
      if (PlayCommand != null) PlayCommand.MayBeExecuted = false;
      LoadCommand.MayBeExecuted = true;
      SaveCommand.MayBeExecuted = false;

      Status = MSG_RECORD_OR_LOAD;
    }

    protected void Record()
    {
      if (!EnsureAudioAccess())
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

        RecordCommand.MayBeExecuted = true; //need this enabled since it's a ToggleCommand
        LoadCommand.MayBeExecuted = false;
        PlayCommand.MayBeExecuted = false;
        SaveCommand.MayBeExecuted = false;
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

          RecordCommand.MayBeExecuted = true;
          LoadCommand.MayBeExecuted = true;
          PlayCommand.MayBeExecuted = true;
          SaveCommand.MayBeExecuted = true;
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
      if (Audio == null)
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
          SaveAudio(stream, Audio);

        Status = MSG_SAVED;
      }
      catch (Exception e)
      {
        Status = MSG_SAVE_FAILED + e.Message;
        MessageBox.Show(Status); //TODO: find parent window
      }
    }

    #endregion

    #region Static helper methods 
    //TODO: see if those are needed at all, since they seem to be just copying a stream using a buffer (.NET should have easier method to do that)

    public static void LoadAudio(Stream stream, Stream target) //does not close the stream
    {
      target = new MemoryStream();
      CopyStream(stream, target, 4096);
    }

    private static void CopyStream(Stream source, Stream target, int bufferSize)
    {
      /*
      // Append all data from rawData stream into output stream.
      byte[] buffer = new byte[bufferSize];
      int read; // number of bytes read in one iteration
      while ((read = source.Read(buffer, 0, bufferSize)) > 0)
        target.Write(buffer, 0, read);
      */
      source.CopyTo(target, bufferSize);
    }

    public static void SaveAudio(Stream stream, Stream source) //does not close the stream
    {
      if (source == null) return; //when no source is available, not writing anything to the stream

      // Reset position in memStream and keep its position to restore at the end
      long originalPosition = source.Position;
      source.Seek(0, SeekOrigin.Begin);

      CopyStream(source, stream, 4096);
  
      source.Seek(originalPosition, SeekOrigin.Begin); //restore memStream position
    }

#endregion
  }
}
