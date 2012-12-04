//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IWindowFactory.cs
//Version: 20121201

using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Windows
{

  public interface IWindowFactory
  {
    BaseWindow CreateWindow();
  }

}
