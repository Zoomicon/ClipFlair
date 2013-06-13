//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IGallery.cs
//Version: 20130613

using System;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class IGalleryProperties
  {
    public const string PropertySource = "Source";
  }

  public interface IGallery : IView
  {
    Uri Source { get; set; }
  }

}
