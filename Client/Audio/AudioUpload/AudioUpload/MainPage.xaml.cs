//Filename: MainPage.xaml.cs
//Version: 20120912

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

using Zoomicon.AudioLib;
using HSS.Interlink;

namespace AudioUpload
{

  public partial class MainPage : UserControl
  {

    public const string FILE_UPLOADER_STORAGE_URL = "http://MYSERVER/Uploads/"; //trailing "/" is optional //TODO: move to web.config

    #region Fields

    private MemoryStream theMemStream; //must be a class field for async operations to work correctly with it
    private MemoryAudioSink storage;
    private CaptureSource grabber;
    private WaveMediaStreamSource wavMss;
    private SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Audio files (*.wav)|*.wav" };
    //private Upload uploader1;
    /**/private UploadClient uploader2 = new UploadClient();

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

      linkAllAudio.NavigateUri = new Uri(FILE_UPLOADER_STORAGE_URL);
      
      btnStart.IsEnabled = false;
      btnStop.IsEnabled = false;
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

        StatusText = "Uploading...";
        string filename = Guid.NewGuid().ToString()+".wav";
        theMemStream.Position = 0; //must point to start of stream
        uploader2.Initialize("FileUpload.ashx", "UploadHandler", true);
        uploader2.UploadStreamAsync(theMemStream, filename); //, null, false); //if we set to true the player won't be able to play
        uploader2.UploadCompleted += (sender, e) =>
        {
          if (e.Error != null)
          {
            StatusText = e.Error.Message;
            linkStatus.NavigateUri = null;
          }
          else
          {
            string remoteFile = FILE_UPLOADER_STORAGE_URL + (FILE_UPLOADER_STORAGE_URL.EndsWith("/") ? "" : "/") + filename;
            StatusText = remoteFile;
            linkStatus.NavigateUri = new Uri(remoteFile);
          }
        };
        
        if (playback) //TODO: should have option to playback from web here
        {
          wavMss = new WaveMediaStreamSource(theMemStream);
          player.SetSource(wavMss);
          player.Position = TimeSpan.Zero;
          player.Play();
        }

      }
      catch (Exception ex)
      {
        LogException(ex);
      }
    }

    private void LogException(Exception ex)
    {
      StatusText = ex.Message;
      output.Visibility = Visibility.Visible; //output TextBox is hidden by default
      output.Text += "\n\n" + ex.StackTrace;
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
