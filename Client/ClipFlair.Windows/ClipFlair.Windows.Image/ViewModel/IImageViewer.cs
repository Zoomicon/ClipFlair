//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewer.cs
//Version: 20140619

using System;

namespace ClipFlair.Windows.Views
{

  public interface IImageViewer : IView
  {
    Uri Source { get; set; }
    bool CameraSourceUsed { get; set; } //20140619
    bool ContentZoomToFit { get; set; }
    Uri ActionURL { get; set; } //20131120
    TimeSpan? ActionTime { get; set; } //20131120

    //TODO: add ActionDuration, ActionResetURL, ActionResetTime
  }

}
