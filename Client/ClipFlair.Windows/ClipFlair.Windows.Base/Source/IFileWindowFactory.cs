//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IWindowFactory.cs
//Version: 20140315

using System.IO;

namespace ClipFlair.Windows
{

  public interface IFileWindowFactory : IWindowFactory
  {
    string[] SupportedFileExtensions();
    BaseWindow CreateWindow(string filename, Stream stream);
  }

}
