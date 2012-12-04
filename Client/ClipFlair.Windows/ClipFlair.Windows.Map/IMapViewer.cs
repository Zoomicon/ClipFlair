//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMapViewer.cs
//Version: 20121129

using System;

namespace ClipFlair.Windows.Views
{

  public static class IMapViewerProperties
  {
    public const string PropertyMode = "Mode";
  }

  public interface IMapViewer : IView
  {
    string Mode { get; set; }
  }

}
