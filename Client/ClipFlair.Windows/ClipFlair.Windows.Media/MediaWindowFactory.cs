//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaWindowFactory.cs
//Version: 20140315

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Media
{

  //Supported file extensions
  [Export("WMV", typeof(IWindowFactory))]
  [Export("MP4", typeof(IWindowFactory))]
  [Export("WMA", typeof(IWindowFactory))]
  [Export("MP3", typeof(IWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.MediaPlayerView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MediaPlayerWindowFactory : IFileWindowFactory
  {

    private static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { "WMV", "MP4", "WMA", "MP3" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }
    
    public BaseWindow CreateWindow()
    {
      return new MediaPlayerWindow();
    }

    public BaseWindow CreateWindow(string filename, Stream stream)
    {
      MediaPlayerWindow window = new MediaPlayerWindow();
      window.Open(stream, filename); //passing filename as title
      return window;
    }

  }

}
