//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsGrid.cs
//Version: 20150323

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;

namespace ClipFlair.Windows.Views
{

  public interface ICaptionsGrid: IView
  {
    Uri Source { get; set; }
    CaptionRegion Captions { get; set; }
    bool ToolbarVisible { get; set; }
    bool IndexVisible { get; set; } //20140722
    bool StartTimeVisible { get; set; }
    bool EndTimeVisible { get; set; }
    bool DurationVisible { get; set; }
    bool RoleVisible { get; set; }
    bool CaptionVisible { get; set; }
    bool RTLVisible { get; set; } //20140326
    bool CPLVisible { get; set; } //20140326
    bool CPSVisible { get; set; } //20131022
    bool WPMVisible { get; set; }
    bool AudioVisible { get; set; }
    bool CommentsVisible { get; set; }
    bool CommentsAudioVisible { get; set; } //20140707
    //bool SaveInvisibleAudio { get; set; } //20140206
    bool LimitAudioPlayback { get; set; } //20150321
    bool LimitAudioRecording { get; set; } //20150321
    bool DrawAudioDuration { get; set; } //20150323
  }

}
