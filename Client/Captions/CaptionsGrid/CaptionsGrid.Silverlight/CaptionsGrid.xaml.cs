//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGrid.xaml.cs
//Version: 20130122

using ClipFlair.AudioRecorder;
using ClipFlair.CaptionsLib.Utils;
using ClipFlair.CaptionsLib.Models;

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

//TODO: upon end of content cell edit, need to jump to some other previous time (for 0 jump to next time, say 0.02) to update player view, or throw some event that captions changed?

namespace ClipFlair.CaptionsGrid
{
  public partial class CaptionsGrid : UserControl
  {
    #region Constants

    private TimeSpan CaptionDefaultDuration = new TimeSpan(0, 0, 2); //TODO: see LVS for the best value there

    #endregion

    public CaptionsGrid()
    {
      InitializeComponent();
      InitializeDataGrid();
    }

    protected void InitializeDataGrid()
    {
      ColumnStartTime = gridCaptions.Columns[0];
      ColumnEndTime = gridCaptions.Columns[1];
      ColumnDuration = gridCaptions.Columns[2];
      ColumnRole = gridCaptions.Columns[3];
      ColumnCaption = gridCaptions.Columns[4];
      ColumnWPM = gridCaptions.Columns[5];
      ColumnAudio = gridCaptions.Columns[6];
      ColumnComments = gridCaptions.Columns[7];

      gridCaptions.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
      gridCaptions.SelectionMode = DataGridSelectionMode.Single;
      gridCaptions.SelectionChanged += new SelectionChangedEventHandler(DataGrid_SelectionChanged);
    }

    protected void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      CaptionElement selectedCaption = (CaptionElement)gridCaptions.SelectedItem;
      if (selectedCaption != null)
      {
        gridCaptions.ScrollIntoView(selectedCaption, gridCaptions.Columns[0]); //scroll vertically to show selected caption's row (and horizontally if needed to show 1st row)
        //cells that are scrolled out of view aren't created yet and won't give us their content //TODO: blog this workarround (needed if the row is out of view or not rendered yet)
          
        Time = selectedCaption.Begin;
        ((AudioRecorderControl)ColumnAudio.GetCellContent(selectedCaption)).View.Play(); //assuming the audio column is inside the current view
      }
    }

    #region --- Properties ---

    #region Columns

    //not using column indices as constants, using column references instead to allow for column reordering by the user
    public DataGridColumn ColumnRole { get; private set; }
    public DataGridColumn ColumnStartTime { get; private set; }
    public DataGridColumn ColumnEndTime { get; private set; }
    public DataGridColumn ColumnDuration { get; private set; }
    public DataGridColumn ColumnCaption { get; private set; }
    public DataGridColumn ColumnWPM { get; private set; }
    public DataGridColumn ColumnAudio { get; private set; }
    public DataGridColumn ColumnComments { get; private set; }

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
    protected virtual void OnTimeChanged(TimeSpan oldTime, TimeSpan newTime)
    {
      CaptionElement activeCaption = null;
      foreach (CaptionElement c in Captions.Children.WhereActiveAtPosition(newTime))
        activeCaption = c; //if multiple captions cover this position, select the last one
      
      gridCaptions.SelectedItem = activeCaption; //this will deselect if no active caption at that time position
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
      if (newCaptions == null) Captions = new CaptionRegion(); //this assumes two-way data-binding that will propagate back to setter
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
      ColumnAudio.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
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

    public IEnumerable<string> Roles
    {
      get
      {
        if (Captions == null) return null;
        return (from caption in Captions.Children select ((CaptionElementExt)caption).Role).Distinct().OrderBy(n => n);
      }
    }
    
    #endregion

    #region --- Methods ---

    private CaptionElement AddCaption()
    {
      if (Captions == null) return null;

      CaptionElement newCaption = new CaptionElementExt()
      {
        Begin = Time,
        End = Time + CaptionDefaultDuration
      }; //TODO: edit blog about Scale transform to also suggest new ScaleTransform() { ScaleX=..., ScaleY=... }; after checking that is works in recent C#

      ClipFlair.MediaPlayer.MediaPlayer.StyleCaption(newCaption); //This is needed else the new caption text won't show up in the MediaPlayer

      Captions.Children.Add(newCaption); //this adds the caption to the correct place in the list based on its Begin time (logic is implemented by SMF in MediaMarkerCollection class) //TODO: blog about this, Insert isn't implemented, Add does its job too (see http://smf.codeplex.com/workitem/23308)
      
      gridCaptions.SelectedItem = newCaption; //select the caption after adding it //TODO: check if it causes hickup and if so tell selection to not jump to selected caption start time
      
      return newCaption;
    }

    #endregion

    #region --- Events ---

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
        AddCaption().End = Time;
    }

    #endregion

    #region Load-Save

    private void btnImport_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "Subtitle files (SRT, TTS)|*.srt;*.tts|SRT files|*.srt|TTS files|*.tts";
        dlg.FilterIndex = 1; //note: this index is 1-based, not 0-based
        
        if (dlg.ShowDialog() == true) //TODO: find the parent window
         using (Stream stream = dlg.File.OpenRead()) //closes stream when finished
            LoadCaptions(stream, dlg.File.Name);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Captions import failed: " + ex.Message); //TODO: find the parent window
      }
    }

    //TODO: blog about 1-based index gotcha and the DefaultFileName issue, also make sure one doesn't use OpenFile (says its MethodGroup type) instead of OpenFile() and that SafeFileName, DefaultFileName etc. have N caps in filename and that DefaultExt (point to doc too) isn't used if a filter is supplied. Show how to have a filter with multiple extensions and multiple filters, note that 1st extension of filterindx is used as default

    private void btnExport_Click(object sender, RoutedEventArgs e)
    {
      if (Captions == null) return;

      try
      {
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.Filter = "Subtitle files (SRT, TTS, FAB, ENC)|*.srt;*.tts;*.fab;*.enc|SRT files|*.srt|FAB files|*.fab|Adobe Encore files|*.enc|TTS files|*.tts";
        //dlg.FilterIndex = 1; //note: this index is 1-based, not 0-based //not needed if we set DefaultExt
        //dlg.DefaultFileName = "Captions"; //Silverlight will prompt "Do you want to save Captions?" if we set this, but the prompt can go under the main window, so avoid it
        dlg.DefaultExt = ".srt"; //this doesn't seem to be used if you set FilterIndex

        if (dlg.ShowDialog() == true) //TODO: find the parent window
          using (Stream stream = dlg.OpenFile()) //closes stream when finished
            SaveCaptions(stream, dlg.SafeFileName);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Captions export failed: " + ex.Message); //TODO: find the parent window
      }
    }

    public void LoadCaptions(Stream stream, string filename) //doesn't close stream
    {
      ICaptionsReader reader = CaptionUtils.GetCaptionsReader(filename);
      CaptionRegion newCaptions = new CaptionRegion();
      reader.ReadCaptions<CaptionElementExt>(newCaptions, stream, Encoding.UTF8);
      Captions = newCaptions;
    }

    public void SaveCaptions(Stream stream, string filename) //doesn't close stream
    {
      if (Captions == null) return;
      ICaptionsWriter writer = CaptionUtils.GetCaptionsWriter(filename);
      writer.WriteCaptions(Captions, stream, Encoding.UTF8);
    }

    public static void LoadAudio(CaptionElement caption, Stream stream)
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      MemoryStream buffer = new MemoryStream();
      AudioRecorderView.LoadAudio(stream, buffer); //keep load logic encapsulated so that we can add decoding/decompression there
      captionExt.Audio = buffer;
    }

    public static void SaveAudio(CaptionElement caption, Stream stream)
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      AudioRecorderView.SaveAudio(stream, captionExt.Audio); //keep save logic encapsulated so that we can add encoding/compression there
    }

    #endregion

  }

}
