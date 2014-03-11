//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: INewsReader.cs
//Version: 20140311

using System;

namespace ClipFlair.Windows.Views
{

  public interface INewsReader : IView
  {
    Uri Source { get; set; }
  }

}
