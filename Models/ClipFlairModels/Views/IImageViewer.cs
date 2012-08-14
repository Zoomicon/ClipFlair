//Filename: IImageViewer.cs
//Version: 20120814

using System;

namespace ClipFlair.Models.Views
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
