//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindowFactory.cs
//Version: 2012121

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Captions
{

  //Supported file extensions
  [Export("SRT", typeof(IWindowFactory))]
  [Export("TTS", typeof(IWindowFactory))]
  [Export("FAB", typeof(IWindowFactory))]
  [Export("ENC", typeof(IWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.CaptionsGridView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class CaptionsGridWindowFactory : IFileWindowFactory
  {

    private static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { "SRT", "TTS", "FAB", "ENC" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }

    public BaseWindow CreateWindow()
    {
      return new CaptionsGridWindow();
    }

    public BaseWindow CreateWindow(string filename, Stream stream)
    {
      CaptionsGridWindow window = new CaptionsGridWindow();
      window.LoadCaptions(stream, filename); //passing filename as title
      return window;
    }

  }

}
