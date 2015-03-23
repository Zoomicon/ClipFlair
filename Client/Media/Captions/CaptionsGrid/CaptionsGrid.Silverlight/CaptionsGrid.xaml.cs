//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGrid.xaml.cs
//Version: 20150323

using ClipFlair.AudioRecorder;
using ClipFlair.CaptionsGrid.Resources;
using ClipFlair.CaptionsLib.Models;
using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Utils.Extensions;

//TODO: upon end of content cell edit, need to jump to some other previous time (for 0 jump to next time, say 0.02) to update player view, or throw some event that captions changed?

namespace ClipFlair.CaptionsGrid
{
  public partial class CaptionsGrid : UserControl
  {

    #region --- Constants ---

    public const string IMPORT_FILTER = "Subtitle files (*.srt, *.tts)|*.srt;*.tts|SRT files (*.srt)|*.srt|TTS files (*.tts)|*.tts";
    public const string EXPORT_FILTER = "Subtitle files (SRT, TTS, FAB, ENC)|*.srt;*.tts;*.fab;*.enc|SRT files|*.srt|FAB files|*.fab|Adobe Encore files|*.enc|TTS files|*.tts";

    private TimeSpan CaptionDefaultDuration = new TimeSpan(0, 0, 2); //TODO: see LVS for the best value there

    public const bool DEFAULT_LIMIT_AUDIO_PLAYBACK = true;
    public const bool DEFAULT_LIMIT_AUDIO_RECORDING = true;
    public const bool DEFAULT_DRAW_AUDIO_DURATION = true;

    public const string PROPERTY_LIMIT_AUDIO_PLAYBACK = "LimitAudioPlayback";
    public const string PROPERTY_LIMIT_AUDIO_RECORDING = "LimitAudioRecording";
    public const string PROPERTY_DRAW_AUDIO_DURATION = "DrawAudioDuration";

    #endregion

    #region --- Initialization ---

    public CaptionsGrid()
    {
      InitializeComponent();
      InitializeDataGrid();

      btnRTL.Click += (s, e) =>
      {
        if (btnRTL.IsChecked == true) //when one manually (from the toolar) toggles RTL to true, the RTLVisible column appears to adjust RTL per-caption (remains visible till hidden from component settings)
          RTLVisible = true;
      };
    }

    protected void InitializeDataGrid()
    {
      //Note: Columns below must be in same order as in XAML
      ColumnIndex = gridCaptions.Columns[0];
      ColumnStartTime = gridCaptions.Columns[1];
      ColumnEndTime = gridCaptions.Columns[2];
      ColumnDuration = gridCaptions.Columns[3];
      ColumnRole = gridCaptions.Columns[4];
      ColumnCaption = gridCaptions.Columns[5];
      ColumnRTL = gridCaptions.Columns[6];
      ColumnCPL = gridCaptions.Columns[7];
      ColumnCPS = gridCaptions.Columns[8];
      ColumnWPM = gridCaptions.Columns[9];
      ColumnAudio = gridCaptions.Columns[10];
      ColumnComments = gridCaptions.Columns[11];
      ColumnCommentsAudio = gridCaptions.Columns[12];

      gridCaptions.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
      gridCaptions.SelectionMode = DataGridSelectionMode.Single;
      gridCaptions.SelectionChanged += new SelectionChangedEventHandler(DataGrid_SelectionChanged);
  
      gridCaptions.BeginningEdit += (s, e) => { 
        Editing = true;
      };
      gridCaptions.CellEditEnded += (s, e) => { 
        Editing = false;
      };

    }

    #endregion
        
    #region --- Properties ---

    public bool Editing { get; private set; }
 
    #region Columns

    //not using column indices as constants, using column references instead to allow for column reordering by the user
    public DataGridColumn ColumnIndex { get; private set; }
    public DataGridColumn ColumnStartTime { get; private set; }
    public DataGridColumn ColumnEndTime { get; private set; }
    public DataGridColumn ColumnDuration { get; private set; }
    public DataGridColumn ColumnRole { get; private set; }
    public DataGridColumn ColumnCaption { get; private set; }
    public DataGridColumn ColumnRTL { get; private set; }
    public DataGridColumn ColumnCPL { get; private set; }
    public DataGridColumn ColumnCPS { get; private set; }
    public DataGridColumn ColumnWPM { get; private set; }
    public DataGridColumn ColumnAudio { get; private set; }
    public DataGridColumn ColumnComments { get; private set; }
    public DataGridColumn ColumnCommentsAudio { get; private set; }

    #endregion

    #region Time

    /// <summary>
    /// Time Dependency Property
    /// </summary>
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register("Time", typeof(TimeSpan), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(TimeSpan.Zero,
                new PropertyChangedCallback(OnTimeChanged)));

    /// <summary>
    /// Gets or sets the Time property.
    /// </summary>
    public TimeSpan Time
    {
      get { return (TimeSpan)GetValue(TimeProperty); }
      set { SetValue(TimeProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Time property.
    /// </summary>
    private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      TimeSpan oldTime = (TimeSpan)e.OldValue;
      TimeSpan newTime = target.Time;
      target.OnTimeChanged(oldTime, newTime);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Time property.
    /// </summary>
    protected virtual void OnTimeChanged(TimeSpan oldTime, TimeSpan newTime) //if StartTime or EndTime cell is being edited (doesn't work), update its value with current Time, else if any caption contains current Time select it (else keep current selection)
    {
      CaptionElement activeCaption = null;
 /*       activeCaption = (CaptionElement)gridCaptions.SelectedItem;
      DataGridColumn curcol = gridCaptions.CurrentColumn;

      if (curcol == ColumnStartTime || curcol == ColumnEndTime)
      {
        object txt = curcol.GetCellContent(activeCaption);
        if (txt is TextBox) //since Editing property doesn't seem to work OK, not checking it above, but using this check instead
        {
          string timeStr = newTime.ToString("hh:mm:ss.FFF");
          ((TextBox)txt).Text = timeStr;
        }
      }
      else */
      {
        foreach (CaptionElement c in Captions.Children.WhereActiveAtPosition(newTime))
          activeCaption = c; //if multiple captions cover this position, select the last one

        if (activeCaption != null) //remember last selected caption (do not deselect if no active caption at current time position) so that we can change its start/end times using respective buttons
          gridCaptions.SelectedItem = activeCaption;
      }
    }

    #endregion

    #region Captions

    /// <summary>
    /// Captions Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionsProperty =
        DependencyProperty.Register("Captions", typeof(CaptionRegion), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(new CaptionRegion(),
                new PropertyChangedCallback(OnCaptionsChanged)));

    /// <summary>
    /// Gets or sets the Captions property.
    /// </summary>
    public CaptionRegion Captions
    {
      get { return (CaptionRegion)GetValue(CaptionsProperty); }
      set { SetValue(CaptionsProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Captions property.
    /// </summary>
    private static void OnCaptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      CaptionRegion oldCaptions = (CaptionRegion)e.OldValue;
      CaptionRegion newCaptions = target.Captions;
      target.OnCaptionsChanged(oldCaptions, newCaptions);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Captions property.
    /// </summary>
    protected virtual void OnCaptionsChanged(CaptionRegion oldCaptions, CaptionRegion newCaptions)
    {
      if (newCaptions == null)
        Captions = new CaptionRegion(); //this assumes two-way data-binding that will propagate back to setter

      UpdateCaptionsRTL();
    }

    #endregion

    #region ToolbarVisible

    /// <summary>
    /// ToolbarVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty ToolbarVisibleProperty =
        DependencyProperty.Register("ToolbarVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnToolbarVisibleChanged)));

    /// <summary>
    /// Gets or sets the ToolbarVisible property.
    /// </summary>
    public bool ToolbarVisible
    {
      get { return (bool)GetValue(ToolbarVisibleProperty); }
      set { SetValue(ToolbarVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ToolbarVisible property.
    /// </summary>
    private static void OnToolbarVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnToolbarVisibleChanged((bool)e.OldValue, target.ToolbarVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnToolbarVisibleChanged(bool oldToolbarVisible, bool newToolbarVisible)
    {
      Toolbar.Visibility = (newToolbarVisible) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region Column visibility

    #region IndexVisible

    /// <summary>
    /// IndexVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IndexVisibleProperty =
        DependencyProperty.Register("IndexVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnIndexVisibleChanged))); //do not use false here, unless Visibility is hidden in XAML

    /// <summary>
    /// Gets or sets the IndexVisible property. 
    /// </summary>
    public bool IndexVisible
    {
      get { return (bool)GetValue(IndexVisibleProperty); }
      set { SetValue(IndexVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IndexVisible property.
    /// </summary>
    private static void OnIndexVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnIndexVisibleChanged((bool)e.OldValue, target.IndexVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IndexVisible property.
    /// </summary>
    protected virtual void OnIndexVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnIndex.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region StartTimeVisible

    /// <summary>
    /// StartTimeVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty StartTimeVisibleProperty =
        DependencyProperty.Register("StartTimeVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnStartTimeVisibleChanged)));

    /// <summary>
    /// Gets or sets the StartTimeVisible property. 
    /// </summary>
    public bool StartTimeVisible
    {
      get { return (bool)GetValue(StartTimeVisibleProperty); }
      set { SetValue(StartTimeVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the StartTimeVisible property.
    /// </summary>
    private static void OnStartTimeVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnStartTimeVisibleChanged((bool)e.OldValue, target.StartTimeVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the StartTimeVisible property.
    /// </summary>
    protected virtual void OnStartTimeVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnStartTime.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region EndTimeVisible

    /// <summary>
    /// EndTimeVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty EndTimeVisibleProperty =
        DependencyProperty.Register("EndTimeVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnEndTimeVisibleChanged)));

    /// <summary>
    /// Gets or sets the EndTimeVisible property. 
    /// </summary>
    public bool EndTimeVisible
    {
      get { return (bool)GetValue(EndTimeVisibleProperty); }
      set { SetValue(EndTimeVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the EndTimeVisible property.
    /// </summary>
    private static void OnEndTimeVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnEndTimeVisibleChanged((bool)e.OldValue, target.EndTimeVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the EndTimeVisible property.
    /// </summary>
    protected virtual void OnEndTimeVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnEndTime.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region DurationVisible

    /// <summary>
    /// DurationVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty DurationVisibleProperty =
        DependencyProperty.Register("DurationVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnDurationVisibleChanged)));

    /// <summary>
    /// Gets or sets the DurationVisible property. 
    /// </summary>
    public bool DurationVisible
    {
      get { return (bool)GetValue(DurationVisibleProperty); }
      set { SetValue(DurationVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the DurationVisible property.
    /// </summary>
    private static void OnDurationVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnDurationVisibleChanged((bool)e.OldValue, target.DurationVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the DurationVisible property.
    /// </summary>
    protected virtual void OnDurationVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnDuration.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region RoleVisible

    /// <summary>
    /// RoleVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty RoleVisibleProperty =
        DependencyProperty.Register("RoleVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnRoleVisibleChanged)));

    /// <summary>
    /// Gets or sets the RoleVisible property. 
    /// </summary>
    public bool RoleVisible
    {
      get { return (bool)GetValue(RoleVisibleProperty); }
      set { SetValue(RoleVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the RoleVisible property.
    /// </summary>
    private static void OnRoleVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnRoleVisibleChanged((bool)e.OldValue, target.RoleVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the RoleVisible property.
    /// </summary>
    protected virtual void OnRoleVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnRole.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region CaptionVisible

    /// <summary>
    /// CaptionVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionVisibleProperty =
        DependencyProperty.Register("CaptionVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCaptionVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionVisible property. 
    /// </summary>
    public bool CaptionVisible
    {
      get { return (bool)GetValue(CaptionVisibleProperty); }
      set { SetValue(CaptionVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionVisible property.
    /// </summary>
    private static void OnCaptionVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCaptionVisibleChanged((bool)e.OldValue, target.CaptionVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionVisible property.
    /// </summary>
    protected virtual void OnCaptionVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnCaption.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region RTLVisible

    /// <summary>
    /// RTLVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty RTLVisibleProperty =
        DependencyProperty.Register("RTLVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnRTLVisibleChanged)));

    /// <summary>
    /// Gets or sets the RTLVisible property. 
    /// </summary>
    public bool RTLVisible
    {
      get { return (bool)GetValue(RTLVisibleProperty); }
      set { SetValue(RTLVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the RTLVisible property.
    /// </summary>
    private static void OnRTLVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnRTLVisibleChanged((bool)e.OldValue, target.RTLVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the RTLVisible property.
    /// </summary>
    protected virtual void OnRTLVisibleChanged(bool oldValue, bool newValue)
    {
      Visibility visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
      //btnRTL.Visibility = visibility; //this is always visible
      ColumnRTL.Visibility = visibility;
    }

    #endregion

    #region CPLVisible

    /// <summary>
    /// CPLVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CPLVisibleProperty =
        DependencyProperty.Register("CPLVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCPLVisibleChanged)));

    /// <summary>
    /// Gets or sets the CPLVisible property. 
    /// </summary>
    public bool CPLVisible
    {
      get { return (bool)GetValue(CPLVisibleProperty); }
      set { SetValue(CPLVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CPLVisible property.
    /// </summary>
    private static void OnCPLVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCPLVisibleChanged((bool)e.OldValue, target.CPLVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CPLVisible property.
    /// </summary>
    protected virtual void OnCPLVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnCPL.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region CPSVisible

    /// <summary>
    /// CPSVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CPSVisibleProperty =
        DependencyProperty.Register("CPSVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCPSVisibleChanged)));

    /// <summary>
    /// Gets or sets the CPSVisible property. 
    /// </summary>
    public bool CPSVisible
    {
      get { return (bool)GetValue(CPSVisibleProperty); }
      set { SetValue(CPSVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CPSVisible property.
    /// </summary>
    private static void OnCPSVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCPSVisibleChanged((bool)e.OldValue, target.CPSVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CPSVisible property.
    /// </summary>
    protected virtual void OnCPSVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnCPS.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region WPMVisible

    /// <summary>
    /// WPMVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty WPMVisibleProperty =
        DependencyProperty.Register("WPMVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnWPMVisibleChanged)));

    /// <summary>
    /// Gets or sets the WPMVisible property. 
    /// </summary>
    public bool WPMVisible
    {
      get { return (bool)GetValue(WPMVisibleProperty); }
      set { SetValue(WPMVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the WPMVisible property.
    /// </summary>
    private static void OnWPMVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnWPMVisibleChanged((bool)e.OldValue, target.WPMVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the WPMVisible property.
    /// </summary>
    protected virtual void OnWPMVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnWPM.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region AudioVisible

    /// <summary>
    /// AudioVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty AudioVisibleProperty =
        DependencyProperty.Register("AudioVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnAudioVisibleChanged)));

    /// <summary>
    /// Gets or sets the AudioVisible property. 
    /// </summary>
    public bool AudioVisible
    {
      get { return (bool)GetValue(AudioVisibleProperty); }
      set { SetValue(AudioVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the AudioVisible property.
    /// </summary>
    private static void OnAudioVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnAudioVisibleChanged((bool)e.OldValue, target.AudioVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the AudioVisible property.
    /// </summary>
    protected virtual void OnAudioVisibleChanged(bool oldValue, bool newValue)
    {
      Visibility visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
      ColumnAudio.Visibility = visibility;
      btnSaveMergedAudio.Visibility = visibility;
    }

    #endregion

    #region CommentsVisible

    /// <summary>
    /// CommentsVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CommentsVisibleProperty =
        DependencyProperty.Register("CommentsVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCommentsVisibleChanged)));

    /// <summary>
    /// Gets or sets the CommentsVisible property. 
    /// </summary>
    public bool CommentsVisible
    {
      get { return (bool)GetValue(CommentsVisibleProperty); }
      set { SetValue(CommentsVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CommentsVisible property.
    /// </summary>
    private static void OnCommentsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCommentsVisibleChanged((bool)e.OldValue, target.CommentsVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CommentsVisible property.
    /// </summary>
    protected virtual void OnCommentsVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnComments.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region CommentsAudioVisible

    /// <summary>
    /// CommentsAudioVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CommentsAudioVisibleProperty =
        DependencyProperty.Register("CommentsAudioVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCommentsAudioVisibleChanged)));

    /// <summary>
    /// Gets or sets the CommentsAudioVisible property. 
    /// </summary>
    public bool CommentsAudioVisible
    {
      get { return (bool)GetValue(CommentsAudioVisibleProperty); }
      set { SetValue(CommentsAudioVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CommentsAudioVisible property.
    /// </summary>
    private static void OnCommentsAudioVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCommentsAudioVisibleChanged((bool)e.OldValue, target.CommentsAudioVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CommentsAudioVisible property.
    /// </summary>
    protected virtual void OnCommentsAudioVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnCommentsAudio.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #endregion

    #region Roles

    public IEnumerable<string> Roles
    {
      get
      {
        if (Captions == null) return null;
        return (from caption in Captions.Children select ((CaptionElementExt)caption).Role).Distinct().OrderBy(n => n);
      }
    }

    #endregion

    #region RTL

    /// <summary>
    /// RTL Dependency Property
    /// </summary>
    public static readonly DependencyProperty RTLProperty =
        DependencyProperty.Register("RTL", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnRTLChanged)));

    /// <summary>
    /// Gets or sets the RTL property.
    /// </summary>
    public bool RTL
    {
      get { return (bool)GetValue(RTLProperty); }
      set { SetValue(RTLProperty, value); }
    }

    /// <summary>
    /// Handles changes to the RTL property.
    /// </summary>
    private static void OnRTLChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnRTLChanged((bool)e.OldValue, target.RTL);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnRTLChanged(bool oldRTL, bool newRTL)
    {
      UpdateCaptionsRTL();

      //Set the button image based on the state of the toggle button. 
      btnRTL.Content = new Uri(newRTL ? "/CaptionsGrid;component/Images/RTL.png" : "/CaptionsGrid;component/Images/LTR.png", UriKind.RelativeOrAbsolute).CreateImage();

      ReturnFocus();
    }

    #endregion

    #region LimitAudioPlayback

    /// <summary>
    /// LimitAudioPlayback Dependency Property
    /// </summary>
    public static readonly DependencyProperty LimitAudioPlaybackProperty =
        DependencyProperty.Register(PROPERTY_LIMIT_AUDIO_PLAYBACK, typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(DEFAULT_LIMIT_AUDIO_PLAYBACK));

    /// <summary>
    /// Gets or sets the LimitAudioPlayback property.
    /// </summary>
    public bool LimitAudioPlayback
    {
      get { return (bool)GetValue(LimitAudioPlaybackProperty); }
      set { SetValue(LimitAudioPlaybackProperty, value); }
    }

    #endregion

    #region LimitAudioRecording

    /// <summary>
    /// LimitAudioRecording Dependency Property
    /// </summary>
    public static readonly DependencyProperty LimitAudioRecordingProperty =
        DependencyProperty.Register(PROPERTY_LIMIT_AUDIO_RECORDING, typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(DEFAULT_LIMIT_AUDIO_RECORDING));

    /// <summary>
    /// Gets or sets the LimitAudioRecording property.
    /// </summary>
    public bool LimitAudioRecording
    {
      get { return (bool)GetValue(LimitAudioRecordingProperty); }
      set { SetValue(LimitAudioRecordingProperty, value); }
    }

    #endregion

    #region DrawAudioDuration

    /// <summary>
    /// DrawAudioDuration Dependency Property
    /// </summary>
    public static readonly DependencyProperty DrawAudioDurationProperty =
        DependencyProperty.Register(PROPERTY_DRAW_AUDIO_DURATION, typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(DEFAULT_DRAW_AUDIO_DURATION));

    /// <summary>
    /// Gets or sets the DrawAudioDuration property.
    /// </summary>
    public bool DrawAudioDuration
    {
      get { return (bool)GetValue(DrawAudioDurationProperty); }
      set { SetValue(DrawAudioDurationProperty, value); }
    }

    #endregion

    #endregion

    #region --- Methods ---

    private void ReturnFocus()
    {
      if (gridCaptions != null)
        gridCaptions.Focus();
    }

    private void UpdateCaptionsRTL()
    {
      if (Captions != null)
      {
        bool value = RTL;
        foreach (CaptionElementExt c in Captions.Children)
          c.RTL = value;
      }
    }

    private CaptionElement AddCaption()
    {
      if (Captions == null) return null;

      CaptionElement newCaption = new CaptionElementExt()
      {
        Begin = Time,
        End = Time + CaptionDefaultDuration,
        RTL = this.RTL //this refers to CaptionsGrid here
      };

      ClipFlair.MediaPlayer.MediaPlayer.StyleCaption(newCaption); //This is needed else the new caption text won't show up in the MediaPlayer

      Captions.Children.Add(newCaption); //this adds the caption to the correct place in the list based on its Begin time (logic is implemented by SMF in MediaMarkerCollection class) //TODO: blog about this, Insert isn't implemented, Add does its job too (see http://smf.codeplex.com/workitem/23308)
      
      gridCaptions.SelectedItem = newCaption; //select the caption after adding it //TODO: check if it causes hickup and if so tell selection to not jump to selected caption start time
      
      return newCaption;
    }

    #endregion

    #region --- Events ---

    private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      e.Handled = true; //do not allow events to propagate to parent, since DataGrid's column dragging code doesn't consume mouse events as it should
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
      AddCaption();
    }

    private void btnRemove_Click(object sender, RoutedEventArgs e)
    {
      if (Captions == null) return;

      CaptionElement selectedCaption = (CaptionElement)gridCaptions.SelectedItem;
      if (selectedCaption != null)
      {
        gridCaptions.SelectedItem = null; //clear current selection before removing the caption, so that selection doesn't move to the next row and make current time change
        Captions.Children.Remove(selectedCaption);
      }
    }

    private void btnStart_Click(object sender, RoutedEventArgs e)
    {
      if (Captions == null) return;

      CaptionElement selectedCaption = (CaptionElement)gridCaptions.SelectedItem;
      if (selectedCaption != null)
        selectedCaption.Begin = Time;
      else
        AddCaption(); //also sets the begin time, no need to do AddCaption().Begin = Time;
    }
    
    private void btnEnd_Click(object sender, RoutedEventArgs e)
    {
      if (Captions == null) return;

      CaptionElement selectedCaption = (CaptionElement)gridCaptions.SelectedItem;
      if (selectedCaption != null)
        selectedCaption.End = Time;
      else
        AddCaption().End = Time; //also sets the begin time, no need to do AddCaption().Begin = Time;
    }

    protected void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      CaptionElement selectedCaption = (CaptionElement)gridCaptions.SelectedItem;
      if (selectedCaption != null)
      {
        DataGridColumn scrollToColumn = gridCaptions.CurrentColumn;
        if (scrollToColumn == null) scrollToColumn = gridCaptions.Columns[0]; //if no column selected scroll to show 1st one
        gridCaptions.ScrollIntoView(selectedCaption, scrollToColumn); //scroll vertically to show selected caption's row
        //cells that are scrolled out of view aren't created yet and won't give us their content //TODO: blog this workarround (needed if the row is out of view or not rendered yet)

        Time = selectedCaption.Begin;
        ((AudioRecorderControl)ColumnAudio.GetCellContent(selectedCaption)).View.Play(); //assuming the audio column is inside the current view
      }
    }

    #endregion

    #region --- Drag & Drop ---

     private void gridCaptions_DragEnter(object sender, DragEventArgs e)
    {
      VisualStateManager.GoToState(this, "DragOver", true);
      e.Handled = true;
    }

    private void gridCaptions_DragOver(object sender, DragEventArgs e)
    {
      e.Handled = true;
      //NOP
    }

    private void gridCaptions_DragLeave(object sender, DragEventArgs e)
    {
      VisualStateManager.GoToState(this, "Normal", true);
      e.Handled = true;
    }

    private void gridCaptions_Drop(object sender, DragEventArgs e)
    {
      VisualStateManager.GoToState(this, "Normal", true);

      //we receive an array of FileInfo objects for the list of files that were selected and drag-dropped onto this control.
      if (e.Data == null)
        return;

      IDataObject f = e.Data as IDataObject;
      if (f == null) //checks if the dropped objects are files
        return;

      object data = f.GetData(DataFormats.FileDrop); //Silverlight 5 only supports FileDrop - GetData returns null if format is not supported
      FileInfo[] files = data as FileInfo[];

      if (files != null && files.Length > 0) //Use only 1st item from array of FileInfo objects
      {
        //TODO: instead of hardcoding which file extensions to ignore, should have this as property of the control (a ; separated string or an array)
        if (files[0].Name.EndsWith(new string[] { ".clipflair", ".clipflair.zip" }, StringComparison.OrdinalIgnoreCase))
          return;

        e.Handled = true; //must do this
        
        LoadCaptions(files);
      }
    }
    
    #endregion

    #region --- Load-Save ---

    private void btnImport_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        OpenFileDialog dlg = new OpenFileDialog()
        {
          Filter = IMPORT_FILTER,
          FilterIndex = 1, //note: this index is 1-based, not 0-based
          Multiselect = true //allow selection of multiple captions files to merge them at load
        };

        if (dlg.ShowDialog() == true) //TODO: find the parent window
          LoadCaptions(dlg.Files);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Captions import failed: " + ex.Message); //TODO: find the parent window
      }
    }

    public void LoadCaptions(IEnumerable<FileInfo> files) //merge multiple captions files
    {
      CaptionRegion newCaptions = new CaptionRegion();
      foreach (FileInfo file in files)
        LoadCaptions(newCaptions, file); //load all caption files into a single CaptionRegion (merge), which should take care automatically of sorting CaptionElements by start time
    }

    public void LoadCaptions(FileInfo file)
    {
      LoadCaptions(new CaptionRegion(), file);
    }

    private void LoadCaptions(CaptionRegion newCaptions, FileInfo file)
    {
      using (Stream stream = file.OpenRead()) //closes stream when finished
        LoadCaptions(newCaptions, stream, file.Name); //this will also set Captions to newCaptions
    }

    public void LoadCaptions(CaptionRegion newCaptions, Stream stream, string filename) //doesn't close stream
    {
      ICaptionsReader reader = CaptionUtils.GetCaptionsReader(filename);
      reader.ReadCaptions<CaptionElementExt>(newCaptions, stream, Encoding.UTF8);
      Captions = newCaptions;
    }

     //TODO: blog about 1-based index gotcha and the DefaultFileName issue, also make sure one doesn't use OpenFile (says its MethodGroup type) instead of OpenFile() and that SafeFileName, DefaultFileName etc. have N caps in filename and that DefaultExt (point to doc too) isn't used if a filter is supplied. Show how to have a filter with multiple extensions and multiple filters, note that 1st extension of filterindx is used as default

    private void btnExport_Click(object sender, RoutedEventArgs e)
    {
      if (Captions == null) return;

      try
      {
        SaveFileDialog dlg = new SaveFileDialog()
        {
          Filter = EXPORT_FILTER,
          //FilterIndex = 1, //note: this index is 1based, not 0based //not needed if we set DefaultExt
          //DefaultFileName = "Captions", //Silverlight will prompt "Do you want to save Captions?" if we set this, but the prompt can go under the main window, so avoid it
          DefaultExt = ".srt" //this doesn't seem to be used if you set FilterIndex
        };

        if (dlg.ShowDialog() == true) //TODO: find the parent window
          using (Stream stream = dlg.OpenFile()) //closes stream when finished
            SaveCaptions(stream, dlg.SafeFileName);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Captions export failed: " + ex.Message); //TODO: find the parent window
      }
    }

    public void SaveCaptions(Stream stream, string filename) //doesn't close stream
    {
      if (Captions == null) return;
      ICaptionsWriter writer = CaptionUtils.GetCaptionsWriter(filename);
      writer.WriteCaptions(Captions, stream, Encoding.UTF8);
    }

    private void btnSaveMergedAudio_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog()
      {
        Filter = CaptionsGridStrings.filter_wav
      };

      if (saveFileDialog.ShowDialog() == true)
        using (Stream stream = saveFileDialog.OpenFile())
        {
          Captions.SaveMergedAudio(stream);
          stream.Flush(); //write any buffers to file
        }
    }

    #endregion

    /*
    private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
    {
      //e.Accepted = true; //TODO: could use filter to select to show only captions for a given role
      String role = abFilterRole.Text;
      e.Accepted = String.IsNullOrWhiteSpace(role) || role.Equals(((CaptionElementExt)e.Item).Role);
    }
    */

  }

}
