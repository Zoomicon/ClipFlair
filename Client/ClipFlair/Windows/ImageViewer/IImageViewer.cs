//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewer.cs
//Version: 20121106

using System;

namespace ClipFlair.Windows.Views
{

  public static class IImageViewerProperties
  {
    public const string PropertySource = "Source";
  }

  public interface IImageViewer : IView
  {
    Uri Source { get; set; }
  }

}
