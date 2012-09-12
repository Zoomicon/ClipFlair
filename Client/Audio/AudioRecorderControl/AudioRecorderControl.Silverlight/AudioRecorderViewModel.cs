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

    const string MSG_SAVING = "Saving...";
    const string MSG_SAVED = "Saved OK";
    const string MSG_CANNOT_SAVE = "Could not save: ";
    const string MSG_CANNOT_PLAY = "Could not play: ";
    const string MSG_RECORDED = "Recording finished. You may play or save your record";
    const string MSG_RECORDING = "Recording... Press Rec again to stop";
    const string MSG_NO_AUDIO = "No access to audio device";
    const string MSG_RECORD_OR_LOAD = "Press Record or Load audio file";
    const string MSG_FILE_FILTER = "Audio files (*.wav)|*.wav";

    #endregion

    #region Fields

    private MemoryStream theMemStream; //must be a class field for async operations to work correctly with it
    private WaveMediaStreamSource wavMss;

    private MediaElement player = new MediaElement();
    private MemoryAudioSink _sink;
    private CaptureSource _captureSource;
    private SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = MSG_FILE_FILTER };

    #endregion

    #region Commands

    public SimpleCommand RecordCommand { get; private set; }
    public SimpleCommand PlayCommand { get; private set; }
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

    public AudioRecorderViewModel(ToggleButton btnRecord)
    {
      RecordCommand = new ToggleCommand(btnRecord)
      {
        MayBeExecuted = true,
        ExecuteAction = () => Record(),
        ExecuteUncheckAction = () => Stop()
      };

      PlayCommand = new SimpleCommand()
      {
        MayBeExecuted = false,
        ExecuteAction = () => Play()
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

      Reset();
    }

    private bool EnsureAudioAccess()
    {
      return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
    }

    public void Reset()
    {
      RecordCommand.MayBeExecuted = true;
      LoadCommand.MayBeExecuted = true;
      PlayCommand.MayBeExecuted = false;
      SaveCommand.MayBeExecuted = false;

      StatusText = MSG_RECORD_OR_LOAD;
    }

    public void Record()
    {
      if (!EnsureAudioAccess())
      {
        StatusText = MSG_NO_AUDIO;
        return;
      }

      if (_captureSource.State != CaptureState.Stopped)
        return;

      _sink = new MemoryAudioSink();
      _sink.CaptureSource = _captureSource;
      _captureSource.Start();

      RecordCommand.MayBeExecuted = true; //need this enabled since it's a ToggleCommand
      LoadCommand.MayBeExecuted = false; 
      PlayCommand.MayBeExecuted = false;
      SaveCommand.MayBeExecuted = false;

      StatusText = MSG_RECORDING;
    }

    public void Stop()
    {
      if (_captureSource.State == CaptureState.Started)
      {
        _captureSource.Stop();

        RecordCommand.MayBeExecuted = true;
        LoadCommand.MayBeExecuted = true;
        PlayCommand.MayBeExecuted = true;
        SaveCommand.MayBeExecuted = true;

        StatusText = MSG_RECORDED;
      }
    }
    
    public void Play()
    {
      try
      {
        theMemStream = new MemoryStream();
        WavManager.SavePcmToWav(_sink.BackingStream, theMemStream, _sink.CurrentFormat);

        wavMss = new WaveMediaStreamSource(theMemStream);
        player.SetSource(wavMss);
        player.Position = new TimeSpan(0);

        player.Play();
      }
      catch (Exception e)
      {
        StatusText = MSG_CANNOT_PLAY + e.Message;
      }
    }

    public void LoadFile()
    {
      //TODO
    }

    public void SaveFile()
    {
      if (saveFileDialog.ShowDialog() == false) return;

      try
      {
        StatusText = MSG_SAVING;
        Stream stream = saveFileDialog.OpenFile();
        WavManager.SavePcmToWav(_sink.BackingStream, stream, _sink.CurrentFormat);
        stream.Close();
        StatusText = MSG_SAVED;
      }
      catch (Exception e)
      {
        StatusText = MSG_CANNOT_SAVE + e.Message;
      }
    }

  }
}
