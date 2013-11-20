//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewer.cs
//Version: 20131120

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public interface IImageViewer : IView
  {
    Uri Source { get; set; }
    bool ContentZoomToFit { get; set; }
    Uri ActionURL { get; set; } //20131120
    TimeSpan? ActionTime { get; set; } //20131120
    TimeSpan Time { get; set; } //20131120
  }

}
