//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ICaptionsGrid.xaml.cs
//Version: 20150614

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.CaptionsGrid
{
  interface ICaptionsGrid
  {

    #region --- Properties ---

    #region Captioning

    TimeSpan Time { get; set; }
    CaptionRegion Captions { get; set; }
    IEnumerable<string> Roles { get; }
    IEnumerable<TimedTextElement> ActiveCaptions { get; }

    #region Audio Duration

    bool LimitAudioPlayback { get; set; }
    bool LimitAudioRecording { get; set; }

    #endregion

    #endregion

    #region UI

    bool ToolbarVisible { get; set; }
    bool RTL { get; set; }
    bool DrawAudioDuration { get; set; }
    bool Editing { get; }

    #region Columns

    DataGridColumn ColumnAudio { get; }
    DataGridColumn ColumnCaption { get; }
    DataGridColumn ColumnComments { get; }
    DataGridColumn ColumnCommentsAudio { get; }
    DataGridColumn ColumnCPL { get; }
    DataGridColumn ColumnCPS { get; }
    DataGridColumn ColumnDuration { get; }
    DataGridColumn ColumnEndTime { get; }
    DataGridColumn ColumnIndex { get; }
    DataGridColumn ColumnRole { get; }
    DataGridColumn ColumnRTL { get; }
    DataGridColumn ColumnStartTime { get; }
    DataGridColumn ColumnWPM { get; }
    
    #endregion

    # region Column visibility

    bool IndexVisible { get; set; }
    bool StartTimeVisible { get; set; }
    bool EndTimeVisible { get; set; }
    bool CaptionVisible { get; set; }

    bool CPLVisible { get; set; }
    bool CPSVisible { get; set; }
    bool WPMVisible { get; set; }
    bool AudioVisible { get; set; }
    bool CommentsVisible { get; set; }
    bool CommentsAudioVisible { get; set; }
    bool DurationVisible { get; set; }
    bool RoleVisible { get; set; }
    bool RTLVisible { get; set; }

    #endregion

    #endregion

    #endregion

    #region --- Methods ---

    #region Selection

    void Select(TimedTextElement captionToSelect);
    void Select(IEnumerable<TimedTextElement> captionsToSelect); 
    void DeselectAll();

    #endregion

    #region Captioning

    #region Add / Remove captions

    CaptionElement AddCaption();
    void RemoveSelectedCaptions();
    
    #endregion

    #region Adjust Time slot

    void SetCaptionEnd();
    void SetCaptionStart();

    #endregion

    #endregion

    #region Load / Save

    void LoadCaptions(CaptionRegion newCaptions, System.IO.Stream stream, string filename);
    void LoadCaptions(IEnumerable<FileInfo> files);
    void LoadCaptions(System.IO.FileInfo file);
    void LoadCaptions(CaptionRegion newCaptions, FileInfo file);
    void SaveCaptions(System.IO.Stream stream, string filename);

    #endregion

    #endregion

  }
}
