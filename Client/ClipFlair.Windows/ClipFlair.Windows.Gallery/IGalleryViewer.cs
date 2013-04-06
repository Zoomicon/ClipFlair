//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IGalleryViewer.cs
//Version: 20130406

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class IGalleryViewerProperties
  {
    public const string PropertySource = "Source";
  }

  public interface IGalleryViewer : IView
  {
    Uri Source { get; set; }
  }

}
