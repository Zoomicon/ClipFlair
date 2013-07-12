//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewer.cs
//Version: 20130710

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public interface IImageViewer : IView
  {
    Uri Source { get; set; }
    bool ContentZoomToFit { get; set; }
  }

}
