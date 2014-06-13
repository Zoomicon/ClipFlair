//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IWindowFactory.cs
//Version: 20140613

using System.IO;

namespace ClipFlair.Windows
{

  public interface IFileWindowFactory : IWindowFactory
  {
    string[] SupportedFileExtensions();
  }

}
