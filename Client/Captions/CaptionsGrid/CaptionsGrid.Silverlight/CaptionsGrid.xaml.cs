//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGrid.xaml.cs
//Version: 20121123

using ClipFlair.AudioRecorder;
using ClipFlair.CaptionsLib.Utils;
using ClipFlair.CaptionsLib.Models;

using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections;

using Microsoft.SilverlightMediaFramework.Core.Media;
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
      ColumnActor = gridCaptions.Columns[0];
      ColumnStartTime = gridCaptions.Columns[1];
      ColumnEndTime = gridCaptions.Columns[2];
      ColumnDuration = gridCaptions.Columns[3];
      ColumnContent = gridCaptions.Columns[4];
      ColumnAudio = gridCaptions.Columns[5];

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
        ((AudioRecorderControl)ColumnAudio.GetCellContent(selectedCaption)).Play(); //assuming the audio column is inside the current view
      }
    }

    #region --- Properties ---

    #region Columns

    //not using column indices as constants, using column references instead to allow for column reordering
    public DataGridColumn ColumnActor { get; private set; }
    public DataGridColumn ColumnStartTime { get; private set; }
    public DataGridColumn ColumnEndTime { get; private set; }
    public DataGridColumn ColumnDuration { get; private set; }
    public DataGridColumn ColumnContent { get; private set; }
    public DataGridColumn ColumnAudio { get; private set; }

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
      //NOP (using data binding)
      //gridCaptions.DataContext = newCaptions.Children; //don't change the UserControl's DataContext (just the DataGrid's), else data binding won't work in the parent (this would need ItemsSource="{Binding}" in the XAML)
    }

    #endregion

    #region CaptionActorVisible

    /// <summary>
    /// CaptionActorVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionActorVisibleProperty =
        DependencyProperty.Register("CaptionActorVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCaptionActorVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionActorVisible property. 
    /// </summary>
    public bool CaptionActorVisible
    {
      get { return (bool)GetValue(CaptionActorVisibleProperty); }
      set { SetValue(CaptionActorVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionActorVisible property.
    /// </summary>
    private static void OnCaptionActorVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCaptionActorVisibleChanged((bool)e.OldValue, target.CaptionActorVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionActorVisible property.
    /// </summary>
    protected virtual void OnCaptionActorVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnActor.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region CaptionStartTimeVisible

    /// <summary>
    /// CaptionStartTimeVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionStartTimeVisibleProperty =
        DependencyProperty.Register("CaptionStartTimeVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCaptionStartTimeVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionStartTimeVisible property. 
    /// </summary>
    public bool CaptionStartTimeVisible
    {
      get { return (bool)GetValue(CaptionStartTimeVisibleProperty); }
      set { SetValue(CaptionStartTimeVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionStartTimeVisible property.
    /// </summary>
    private static void OnCaptionStartTimeVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCaptionStartTimeVisibleChanged((bool)e.OldValue, target.CaptionStartTimeVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionStartTimeVisible property.
    /// </summary>
    protected virtual void OnCaptionStartTimeVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnStartTime.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region CaptionEndTimeVisible

    /// <summary>
    /// CaptionEndTimeVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionEndTimeVisibleProperty =
        DependencyProperty.Register("CaptionEndTimeVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCaptionEndTimeVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionEndTimeVisible property. 
    /// </summary>
    public bool CaptionEndTimeVisible
    {
      get { return (bool)GetValue(CaptionEndTimeVisibleProperty); }
      set { SetValue(CaptionEndTimeVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionEndTimeVisible property.
    /// </summary>
    private static void OnCaptionEndTimeVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCaptionEndTimeVisibleChanged((bool)e.OldValue, target.CaptionEndTimeVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionEndTimeVisible property.
    /// </summary>
    protected virtual void OnCaptionEndTimeVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnEndTime.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region CaptionDurationVisible

    /// <summary>
    /// CaptionDurationVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionDurationVisibleProperty =
        DependencyProperty.Register("CaptionDurationVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCaptionDurationVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionDurationVisible property. 
    /// </summary>
    public bool CaptionDurationVisible
    {
      get { return (bool)GetValue(CaptionDurationVisibleProperty); }
      set { SetValue(CaptionDurationVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionDurationVisible property.
    /// </summary>
    private static void OnCaptionDurationVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCaptionDurationVisibleChanged((bool)e.OldValue, target.CaptionDurationVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionDurationVisible property.
    /// </summary>
    protected virtual void OnCaptionDurationVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnDuration.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region CaptionContentVisible

    /// <summary>
    /// CaptionContentVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionContentVisibleProperty =
        DependencyProperty.Register("CaptionContentVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCaptionContentVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionContentVisible property. 
    /// </summary>
    public bool CaptionContentVisible
    {
      get { return (bool)GetValue(CaptionContentVisibleProperty); }
      set { SetValue(CaptionContentVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionContentVisible property.
    /// </summary>
    private static void OnCaptionContentVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCaptionContentVisibleChanged((bool)e.OldValue, target.CaptionContentVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionContentVisible property.
    /// </summary>
    protected virtual void OnCaptionContentVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnContent.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region CaptionAudioVisible

    /// <summary>
    /// CaptionAudioVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionAudioVisibleProperty =
        DependencyProperty.Register("CaptionAudioVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnCaptionAudioVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionAudioVisible property. 
    /// </summary>
    public bool CaptionAudioVisible
    {
      get { return (bool)GetValue(CaptionAudioVisibleProperty); }
      set { SetValue(CaptionAudioVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionAudioVisible property.
    /// </summary>
    private static void OnCaptionAudioVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnCaptionAudioVisibleChanged((bool)e.OldValue, target.CaptionAudioVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionAudioVisible property.
    /// </summary>
    protected virtual void OnCaptionAudioVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnAudio.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #endregion

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
      AudioRecorderView.LoadAudio(stream, buffer);
      captionExt.Audio = buffer;
    }

    public static void SaveAudio(CaptionElement caption, Stream stream)
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      AudioRecorderView.SaveAudio(stream, captionExt.Audio);
    }

    #endregion

  }

}
