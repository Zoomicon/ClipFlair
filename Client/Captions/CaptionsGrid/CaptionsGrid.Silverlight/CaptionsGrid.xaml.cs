//Filename: CaptionsGrid.xaml.cs
//Version: 20120912

using Zoomicon.CaptionsGrid;
using Zoomicon.AudioRecorder;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace Zoomicon.CaptionsGrid
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
      ColumnContent = gridCaptions.Columns[3];
      ColumnAudio = gridCaptions.Columns[4];

      gridCaptions.SelectionMode = DataGridSelectionMode.Single;
      gridCaptions.SelectionChanged += new SelectionChangedEventHandler(DataGrid_SelectionChanged);
    }

    protected void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      CaptionElement selectedMarker = ((CaptionElement)gridCaptions.SelectedItem);
      if (selectedMarker != null)
      {
        Time = selectedMarker.Begin;
        ((AudioRecorderControl)ColumnAudio.GetCellContent(selectedMarker)).Play();
      }
    }

    #region --- Properties ---

    #region Columns

    //not using column indices as constants, using column references instead to allow for column reordering
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
                FrameworkPropertyMetadataOptions.None,
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
      foreach (CaptionElement activeCaption in Markers.WhereActiveAtPosition(newTime))
        gridCaptions.SelectedItem = activeCaption; //if multiple captions cover this position, select the last one
 
      CaptionElement selectedCaption = (CaptionElement)gridCaptions.SelectedItem;
      if (selectedCaption != null)
        if (!selectedCaption.IsActiveAtPosition(newTime)) //if currently selected caption (from older selection) is not active at the given position, clear selection
          gridCaptions.SelectedItem = null;
    }

    #endregion

    #region Markers

    /// <summary>
    /// Markers Dependency Property
    /// </summary>
    public static readonly DependencyProperty MarkersProperty =
        DependencyProperty.Register("Markers", typeof(MediaMarkerCollection<TimedTextElement>), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnMarkersChanged)));

    /// <summary>
    /// Gets or sets the Markers property.
    /// </summary>
    public MediaMarkerCollection<TimedTextElement> Markers
    {
      get { return (MediaMarkerCollection<TimedTextElement>)GetValue(MarkersProperty); }
      set { SetValue(MarkersProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Markers property.
    /// </summary>
    private static void OnMarkersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      MediaMarkerCollection<TimedTextElement> oldMarkers = (MediaMarkerCollection<TimedTextElement>)e.OldValue;
      MediaMarkerCollection<TimedTextElement> newMarkers = target.Markers;
      target.OnMarkersChanged(oldMarkers, newMarkers);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Markers property.
    /// </summary>
    protected virtual void OnMarkersChanged(MediaMarkerCollection<TimedTextElement> oldMarkers, MediaMarkerCollection<TimedTextElement> newMarkers)
    {
      gridCaptions.DataContext = /*new MediaMarkerCollectionWrapper<TimedTextElement>*/(newMarkers); //don't changed the UserControl's DataContext, else data binding won't work in the parent
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

    #region --- Events ---

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
      CaptionElement newCaption = new CaptionElement()
      {
        Begin = Time,
        End = Time + CaptionDefaultDuration
      }; //TODO: edit blog about Scale transform to also suggest new ScaleTransform() { ScaleX=..., ScaleY=... }; after checking that is works in recent C#
      
      Zoomicon.MediaPlayer.MediaPlayer.StyleMarker(newCaption); //This is needed else the new caption text won't show up in the MediaPlayer

      Markers.Add(newCaption); //this adds the marker to the correct place in the list based on its Begin time (logic is implemented by SMF in MediaMarkerCollection class) //TODO: blog about this, Insert isn't implemented, Add does its job too (see http://smf.codeplex.com/workitem/23308)
    }

    private void btnRemove_Click(object sender, RoutedEventArgs e)
    {
      CaptionElement selectedMarker = ((CaptionElement)gridCaptions.SelectedItem);
      if (selectedMarker != null)
        Markers.Remove(selectedMarker);
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
      //TODO: use LeViS code?
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      //TODO: use LeViS code?
    }

    #endregion

  }

}
