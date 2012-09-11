//Filename: AudioRecorderViewModel.cs
//Version: 20120911

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using AudioLib;

namespace AudioRecorder
{

  public class AudioRecorderViewModel : DependencyObject
  {

    #region Fields

    private MemoryAudioSink _sink;
    private CaptureSource _captureSource;
    private SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Audio files (*.wav)|*.wav" };

    #endregion

    #region Commands

    public SimpleCommand RecordCommand { get; private set; }
    public SimpleCommand PlayPauseCommand { get; private set; }
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

    public AudioRecorderViewModel()
    {
      RecordCommand = new SimpleCommand()
      {
        MayBeExecuted = true,
        ExecuteAction = () => Record()
      };

      SaveCommand = new SimpleCommand()
      {
        MayBeExecuted = false,
        ExecuteAction = () => SaveFile()
      };

      PlayPauseCommand = new SimpleCommand()
      {
        MayBeExecuted = false,
        ExecuteAction = () => PlayOrPause()
      };

      AudioCaptureDevice audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
      _captureSource = new CaptureSource() { AudioCaptureDevice = audioDevice };

      GoToStartState();
    }

    private bool EnsureAudioAccess()
    {
      return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
    }

    protected void GoToStartState()
    {
      StatusText = "Ready...";
      SaveCommand.MayBeExecuted = false;
      RecordCommand.MayBeExecuted = true;
      PlayPauseCommand.MayBeExecuted = false;
    }

    protected void Record()
    {
      if (!EnsureAudioAccess())
        return;

      if (_captureSource.State != CaptureState.Stopped)
        return;

      _sink = new MemoryAudioSink();
      _sink.CaptureSource = _captureSource;
      _captureSource.Start();

      // Enable pause command, disable record command
      PlayPauseCommand.MayBeExecuted = true;
      RecordCommand.MayBeExecuted = false;
      StatusText = "Recording...";
    }

    protected void PlayOrPause()
    {
      if (_captureSource.State == CaptureState.Started)
      {
        _captureSource.Stop();

        // Disable pause command, enable save command
        PlayPauseCommand.MayBeExecuted = false;
        SaveCommand.MayBeExecuted = true;
        StatusText = "Recording finished. You may save your record.";
      }
    }

    protected void SaveFile()
    {
      if (saveFileDialog.ShowDialog() == false) return;

      StatusText = "Saving...";

      Stream stream = saveFileDialog.OpenFile();
      WavManager.SavePcmToWav(_sink.BackingStream, stream, _sink.CurrentFormat);
      stream.Close();

      MessageBox.Show("Saved OK");

      GoToStartState();
    }
  }
}
