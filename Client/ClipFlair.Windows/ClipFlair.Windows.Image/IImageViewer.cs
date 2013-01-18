//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewer.cs
//Version: 20130118

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class IImageViewerProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyStretch = "Stretch";
  }

  public interface IImageViewer : IView
  {
    Uri Source { get; set; }
    Stretch Stretch { get; set; }
  }

}
