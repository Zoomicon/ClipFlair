//Version: 20120713

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
namespace surveyApp
{
  public partial class MainPage : UserControl
  {

    const string FILE_UPLOADER_URL = "http://clipflairsrv.cti.gr/Alpha/AudioUpload/FileUpload.ashx";  //TODO: move to web.config
    const string FILE_UPLOADER_STORAGE_URL = "http://clipflairsrv.cti.gr/Alpha/AudioUpload/Uploads/"; //trailing "/" is optional //TODO: move to web.config

    MemoryAudioSink storage;
    CaptureSource audioGrabber;
    MemoryStream theMemStream;
    WaveMediaStreamSource wavMss;

    public MainPage()
    {
      InitializeComponent();
      UIDispatcher = this.Dispatcher;
      btnStart.IsEnabled = false;
      btnStop.IsEnabled = false;
      linkAllAudio.NavigateUri = new Uri(FILE_UPLOADER_STORAGE_URL);
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      //these two commands take some secs
      var audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
      audioGrabber = new CaptureSource() { AudioCaptureDevice = audioDevice };

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

      if (audioGrabber.State == CaptureState.Started) //may have already stopped after some timeout
      {
        audioGrabber.Stop();
      }

      try
      {
        theMemStream = new MemoryStream();
        WavManager.SavePcmToWav(storage.BackingStream, theMemStream, storage.CurrentFormat);

        StartUpload(theMemStream); //start uploading asynchronously

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
      if (!EnsureAudioAccess())
        return;

      if (audioGrabber.State != CaptureState.Stopped)
        return;

      storage = new MemoryAudioSink();
      storage.CaptureSource = audioGrabber;

      audioGrabber.Start();
    }

    private bool EnsureAudioAccess()
    {
      return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
    }

    #region Upload

    string fileName;
    Stream fileStream;
    Dispatcher UIDispatcher;
    int ChunkSize = 4194304;
    long _dataLength;
    long _dataSent = 0;

    private void StartUpload(Stream voiceStream)
    {
      if (voiceStream != null)
      {
        fileName = Guid.NewGuid().ToString();
        fileStream = voiceStream;
        _dataLength = fileStream.Length;

        long dataToSend = _dataLength - _dataSent;
        bool isLastChunk = dataToSend <= ChunkSize;
        bool isFirstChunk = _dataSent == 0;
        string docType = "document";

        UriBuilder httpHandlerUrlBuilder = new UriBuilder(FILE_UPLOADER_URL);
        httpHandlerUrlBuilder.Query = string.Format("{5}file={0}&offset={1}&last={2}&first={3}&docType={4}", fileName + ".wav", _dataSent, isLastChunk, isFirstChunk, docType, string.IsNullOrEmpty(httpHandlerUrlBuilder.Query) ? "" : httpHandlerUrlBuilder.Query.Remove(0, 1) + "&");

        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(httpHandlerUrlBuilder.Uri);
        webRequest.Method = "POST";
        webRequest.BeginGetRequestStream(new AsyncCallback(WriteToStreamCallback), webRequest);

        linkStatus.Content = "Uploading...";
        linkStatus.NavigateUri = null;
      }
    }

    private void WriteToStreamCallback(IAsyncResult asynchronousResult)
    {
      HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
      Stream requestStream = webRequest.EndGetRequestStream(asynchronousResult);

      byte[] buffer = new Byte[4096];
      int bytesRead = 0;
      int tempTotal = 0;

      //Set the start position
      fileStream.Position = _dataSent;

      //Read the next chunk
      //&& !_file.IsDeleted && _file.State != Constants.FileStates.Error
      while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0 && tempTotal + bytesRead < ChunkSize)
      {
        requestStream.Write(buffer, 0, bytesRead);
        requestStream.Flush();

        _dataSent += bytesRead;
        tempTotal += bytesRead;

      }

      requestStream.Close();

      webRequest.BeginGetResponse(new AsyncCallback(ReadHttpResponseCallback), webRequest);
    }

    private void ReadHttpResponseCallback(IAsyncResult asynchronousResult)
    {

      try
      {
        HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
        HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
        StreamReader reader = new StreamReader(webResponse.GetResponseStream());

        string responsestring = reader.ReadToEnd();
        reader.Close();
      }
      catch (Exception ex) //TODO: show errors
      {

      }

      if (_dataSent < _dataLength)
      {
        //continue uploading the rest of the file in chunks
        StartUpload(fileStream);

        //TODO: Show the progress change

      }
      else
      {
        UIDispatcher.BeginInvoke(delegate()
        {

          string remoteFile = FILE_UPLOADER_STORAGE_URL + (FILE_UPLOADER_STORAGE_URL.EndsWith("/") ? "" : "/") + fileName + ".wav";
          linkStatus.Content = remoteFile;
          linkStatus.NavigateUri = new Uri(remoteFile);
        });

        //TODO: Show the progress change
      }

    }

    #endregion

  }
}
