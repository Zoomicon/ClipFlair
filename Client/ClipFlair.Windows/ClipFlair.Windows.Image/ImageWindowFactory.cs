//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindowFactory.cs
//Version: 20140613

using System.ComponentModel.Composition;
using System.IO;

namespace ClipFlair.Windows.Image
{

  //Supported file extensions
  [Export(".PNG", typeof(IFileWindowFactory))]
  [Export(".JPG", typeof(IFileWindowFactory))]
  //Supported views
  [Export("ClipFlair.Windows.Views.ImageView", typeof(IWindowFactory))]
  //MEF creation Policy
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ImageWindowFactory : IFileWindowFactory
  {

    public const string LOAD_FILTER = "Image files (*.png, *.jpg)|*.png;*.jpg";

    private static string[] SUPPORTED_FILE_EXTENSIONS = new string[] { ".PNG", ".JPG" };

    public string[] SupportedFileExtensions()
    {
      return SUPPORTED_FILE_EXTENSIONS;
    }

    public BaseWindow CreateWindow()
    {
      return new ImageWindow();
    }

  }

}
