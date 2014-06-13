//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaWindowFactory.cs
//Version: 20140613

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Media
{

  //Supported file extensions
  [Export(".WMV", typeof(IFileWindowFactory))]
  [Export(".MP4", typeof(IFileWindowFactory))]
  [Export(".WMA", typeof(IFileWindowFactory))]
  [Export(".MP3", typeof(IFileWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.MediaPlayerView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MediaPlayerWindowFactory : IFileWindowFactory
  {

    public const string LOAD_FILTER = "Media files (*.wmv, *.mp4, *.wma, *.mp3)|*.wmv;*.mp4;*.wma;*.mp3";

    private static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { ".WMV", ".MP4", ".WMA", ".MP3" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }
    
    public BaseWindow CreateWindow()
    {
      return new MediaPlayerWindow();
    }

  }

}
