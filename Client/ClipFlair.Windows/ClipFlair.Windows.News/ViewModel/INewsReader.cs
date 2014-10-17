//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: INewsReader.cs
//Version: 20141017

using System;

namespace ClipFlair.Windows.Views
{

  public interface INewsReader : IView
  {
    Uri Source { get; set; }
    TimeSpan RefreshInterval { get; set; } //20141017
  }

}
