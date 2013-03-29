//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IGalleryViewer.cs
//Version: 20130326

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class IGalleryViewerProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyStretch = "Stretch";
  }

  public interface IGalleryViewer : IView
  {
    Uri Source { get; set; }
    Stretch Stretch { get; set; }
  }

}
