//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsGrid.cs
//Version: 20140206

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;

namespace ClipFlair.Windows.Views
{

  public interface ICaptionsGrid: IView
  {
    Uri Source { get; set; }
    CaptionRegion Captions { get; set; }
    bool ToolbarVisible { get; set; }
    bool StartTimeVisible { get; set; }
    bool EndTimeVisible { get; set; }
    bool DurationVisible { get; set; }
    bool RoleVisible { get; set; }
    bool CaptionVisible { get; set; }
    bool CPSVisible { get; set; } //20131022
    bool WPMVisible { get; set; }
    bool AudioVisible { get; set; }
    bool CommentsVisible { get; set; }
    bool SaveInvisibleAudio { get; set; } //20140206
  }

}
