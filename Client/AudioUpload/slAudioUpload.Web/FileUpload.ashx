<%@ WebHandler Language="C#" Class="FileUploader" %>

//Version: 20120713

//TODO: calculate file size of folder (does .NET give this or do we need to sum file sizes of all files in folder?) and don't allow above a limit, maybe autodelete folder contents when limit reached (also maybe not allow too big files)

using System;
using System.Web;
using System.IO;
using System.Web.Hosting;
using System.Diagnostics;

public class FileUploader : IHttpHandler
{

  private HttpContext _httpContext;

  const string preSaveExt = "tmp";
  const string uploadFolder = "Uploads";

  StreamWriter _debugFileStreamWriter;
  TextWriterTraceListener _debugListener;

  public void ProcessRequest(HttpContext context)
  {
    _httpContext = context;

    if (context.Request.InputStream.Length == 0)
      throw new ArgumentException("No file input");

    try
    {
      #region Get query string parameters

      string fName = _httpContext.Request.QueryString["file"];
      bool _lastChunk = string.IsNullOrEmpty(_httpContext.Request.QueryString["last"]) ? true : bool.Parse(_httpContext.Request.QueryString["last"]);
      bool _firstChunk = string.IsNullOrEmpty(_httpContext.Request.QueryString["first"]) ? true : bool.Parse(_httpContext.Request.QueryString["first"]);
      long _startByte = string.IsNullOrEmpty(_httpContext.Request.QueryString["offset"]) ? 0 : long.Parse(_httpContext.Request.QueryString["offset"]); ;

      #endregion

      string tempFile = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, uploadFolder, fName + preSaveExt);
      string targetFile = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, uploadFolder, fName);

      if (_firstChunk)
      {
        //Delete temp file if pre-exists
        if (File.Exists(tempFile)) File.Delete(tempFile);

        //Delete target file if pre-exists
        if (File.Exists(targetFile)) File.Delete(targetFile);
      }

      using (FileStream fs = File.Open(tempFile, FileMode.Append))
      {
        SaveFile(context.Request.InputStream, fs);
        fs.Close();
      }

      if (_lastChunk)
      {
        File.Move(tempFile, targetFile);
      }

    }
    catch (Exception)
    {
      throw;
    }
  }

  private void SaveFile(Stream stream, FileStream fs)
  {
    byte[] buffer = new byte[4096];
    int bytesRead;
    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
    {
      fs.Write(buffer, 0, bytesRead);
    }
  }



  public bool IsReusable
  {
    get { return false; }
  }

}