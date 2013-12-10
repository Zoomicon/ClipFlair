//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IMediaPlayer.cs
//Version: 20131205

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;

namespace ClipFlair.Windows.Views
{

 public interface IMediaPlayer: IView
  {
    Uri Source { get; set; }
    TimeSpan ReplayOffset { get; set; }
    CaptionRegion Captions { get; set; }
    double Speed { get; set; }
    double Volume { get; set; }
    double Balance { get; set; } //-1=left only, 0=left/right, 1=right only
    bool AutoPlay { get; set; }
    bool Looping { get; set; }
    bool VideoVisible { get; set; }
    bool ControllerVisible { get; set; }
    bool CaptionsVisible { get; set; }

    void Play();
    void Pause();
    void Stop(); //also resets time to 0
  }

}
