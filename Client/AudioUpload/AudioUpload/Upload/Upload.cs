//Version: 20120724

using System;
using System.Net;
using System.Windows.Controls;
using System.IO;
using System.Windows.Threading;

namespace AudioUpload
{

  public class Upload
  {

    public string FILE_UPLOADER_URL = "http://clipflairsrv.cti.gr/Alpha/AudioUpload/Upload.ashx";  //TODO: move to web.config
    
    string storageUrl;
    string fileName;
    Stream fileStream;
    Dispatcher UIDispatcher;
    int ChunkSize = 4194304;
    long _dataLength;
    long _dataSent = 0;
    HyperlinkButton linkStatus;

    public Upload(HyperlinkButton h, Dispatcher d, string storageUrl)
    {
      linkStatus = h;
      UIDispatcher = d;
      this.storageUrl = storageUrl;
    }

    public void StartUpload(Stream voiceStream)
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
      else
      {
        linkStatus.Content = "No audio data to upload";
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
        
/*        if(disposeStream)
          try
          {
            if (fileStream != null)
            {
              fileStream.Close();
              fileStream.Dispose();
              fileStream = null;
            }
          }
          catch (Exception ex)
          {
          }
 */        
          if (_dataLength != 0)
          {
            string remoteFile = storageUrl + (storageUrl.EndsWith("/") ? "" : "/") + fileName + ".wav";
            linkStatus.Content = remoteFile;
            linkStatus.NavigateUri = new Uri(remoteFile);
          }
        });

        //TODO: Show the progress change
      }

    }


  }

}