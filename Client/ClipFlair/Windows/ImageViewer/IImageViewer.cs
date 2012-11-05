//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewer.cs
//Version: 20121102

using System;

namespace ClipFlair.Windows.Views
{

  public static class IImageViewerProperties
  {
    public const string PropertySource = "Source";
  }

  public static class IImageViewerDefaults
  {
    public static Uri DefaultSource = null;
  }
  
  public interface IImageViewer : IView
  {
    Uri Source { get; set; }
  }

}
