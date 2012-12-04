//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewer.cs
//Version: 20121203

using System;

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
  }

}
