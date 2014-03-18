//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityWindowFactory.cs
//Version: 20140318

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Activity
{

  //Supported file extensions
  [Export(".CLIPFLAIR", typeof(IFileWindowFactory))]
  [Export(".CLIPFLAIR.ZIP", typeof(IFileWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.ActivityView", typeof(IWindowFactory))]
  //MEF creation policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ActivityWindowFactory : IFileWindowFactory
  {

    private static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { ".CLIPFLAIR", ".CLIPFLAIR.ZIP" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }

    public BaseWindow CreateWindow()
    {
      return new ActivityWindow();
    }

    public BaseWindow CreateWindow(string filename, Stream stream)
    {
      ActivityWindow window = new ActivityWindow();
      window.LoadOptions(stream);
      return window;
    }

  }

}
