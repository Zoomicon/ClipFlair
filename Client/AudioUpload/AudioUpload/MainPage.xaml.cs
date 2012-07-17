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

    MemoryAudioSink storage;
    CaptureSource grabber;
    MemoryStream theMemStream;
    WaveMediaStreamSource wavMss;
    FileUpload uploader;

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
      //these two commands take some secs
      var audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
      grabber = new CaptureSource() { VideoCaptureDevice = null, AudioCaptureDevice = audioDevice };

      btnStart.IsEnabled = true;
    }

    private void btnStart_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        theMemStream.Close();
        theMemStream.Dispose();
      }
      catch (Exception ex) //TODO: show errors
      {

      }

      Record();
      btnStop.IsEnabled = true;
      btnStart.IsEnabled = false;
    }

    private void btnStop_Click(object sender, RoutedEventArgs e)
    {
      btnStop.IsEnabled = false;
      btnStart.IsEnabled = true;
      //btnSave.IsEnabled = true;

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

        if ((bool)cbPlayback.IsChecked) //TODO: should have option to playback from web here
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
      //TODO: allow save to local file (via open file dialog)
    }

    protected void Record()
    {
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
