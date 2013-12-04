//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IGallery.cs
//Version: 20130701

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public interface IGallery : IView
  {
    Uri Source { get; set; }
    string Filter { get; set; }
  }

}
