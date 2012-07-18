//Version: 20120718

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using WaveMSS;
using System.Windows.Threading;

namespace AudioUpload
{

  public partial class MainPage : UserControl
  {

    #region Fields

    private MemoryAudioSink storage;
    private CaptureSource grabber;
    private MemoryStream theMemStream;
    private WaveMediaStreamSource wavMss;
    private SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Audio files (*.wav)|*.wav" };
    private FileUpload uploader;

    #endregion

    #region Properties

    String StatusText
    {
      get { return (String)linkStatus.Content; }
      set { linkStatus.Content = value; }
    }

    #endregion

    public MainPage()
    {
      InitializeComponent();
      uploader = new FileUpload(linkStatus, this.Dispatcher);
      btnStart.IsEnabled = false;
      btnStop.IsEnabled = false;
      linkAllAudio.NavigateUri = new Uri(FileUpload.FILE_UPLOADER_STORAGE_URL);
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      Init();
      btnStart.IsEnabled = true;
    }

    private void Init()
    {
      //these two commands take some secs
      var audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
      grabber = new CaptureSource() { VideoCaptureDevice = null, AudioCaptureDevice = audioDevice };
    }

    private void btnStart_Click(object sender, RoutedEventArgs e)
    {
      Record();
      btnStop.IsEnabled = true;
      btnStart.IsEnabled = false;
      btnSave.IsEnabled = false;
    }

    private void btnStop_Click(object sender, RoutedEventArgs e)
    {
      btnStop.IsEnabled = false;
      btnStart.IsEnabled = true;
      btnSave.IsEnabled = true;

      Stop((bool)cbPlayback.IsChecked);
    }

    private void Stop(bool playback)
    {
      if (grabber.State == CaptureState.Started) //may have already stopped after some timeout
      {
        grabber.Stop();
      }

      try
      {
        theMemStream = new MemoryStream();
        WavManager.SavePcmToWav(storage.BackingStream, theMemStream, storage.CurrentFormat);

        uploader = new FileUpload(linkStatus, this.Dispatcher); //!!! temp to fix 2nd file not uploading        
        uploader.StartUpload(theMemStream); //start uploading asynchronously

        if (playback) //TODO: should have option to playback from web here
        {
          wavMss = new WaveMediaStreamSource(theMemStream);
          player.SetSource(wavMss);
          player.Play();
        }

      }
      catch (Exception ex) //TODO: show errors
      {

      }
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      if (saveFileDialog.ShowDialog() == false)
      {
        return;
      }

      StatusText = "Saving...";

      Stream stream = saveFileDialog.OpenFile();

      WavManager.SavePcmToWav(storage.BackingStream, stream, storage.CurrentFormat);

      stream.Close();

      MessageBox.Show("Your record is saved.");
    }

    protected void Record()
    {
      try
      {
        theMemStream.Close();
        theMemStream.Dispose();
      }
      catch (Exception ex) //TODO: show errors
      {

      }

      if (!EnsureAudioAccess()) return;

      if (grabber.State != CaptureState.Stopped) grabber.Stop();

      storage = new MemoryAudioSink();
      storage.CaptureSource = grabber;

      grabber.Start();
    }

    private bool EnsureAudioAccess()
    {
      return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
    }

  }

}
