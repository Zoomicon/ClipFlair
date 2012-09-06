//Filename: CaptionsGrid.xaml.cs
//Version: 20120906

using Zoomicon.CaptionsGrid;

using System;
using System.Windows;
using System.Windows.Controls;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace Zoomicon.CaptionsGrid
{
  public partial class CaptionsGrid : UserControl
  {
    #region Constants

    private TimeSpan SmallestTimeStep = new TimeSpan(0,0,0,1);

    //not using column indices as constants, using column references instead to allow for column reordering
    public DataGridColumn ColumnStartTime { get; private set; }
    public DataGridColumn ColumnEndTime { get; private set; }
    public DataGridColumn ColumnDuration { get; private set; }
    public DataGridColumn ColumnContent { get; private set; }

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

      gridCaptions.SelectionMode = DataGridSelectionMode.Single;
      gridCaptions.SelectionChanged += new SelectionChangedEventHandler(DataGrid_SelectionChanged);
    }

    protected void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      CaptionElement selectedMarker = ((CaptionElement)gridCaptions.SelectedItem);
      if (selectedMarker != null)
        Time = selectedMarker.Begin;
    }

    #region --- Properties ---

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
      foreach (CaptionElement activeCaption in Markers.WhereActiveAtPosition(newTime, newTime-SmallestTimeStep)) //if multiple captions cover this position, select the one latest activated
      {
        gridCaptions.SelectedItem = activeCaption;
        return; //select the 1st caption found (and exit method)
      }
 
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

    #region IsCaptionStartTimeVisible

    /// <summary>
    /// IsCaptionStartTimeVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsCaptionStartTimeVisibleProperty =
        DependencyProperty.Register("IsCaptionStartTimeVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnIsCaptionStartTimeVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsCaptionStartTimeVisible property. 
    /// </summary>
    public bool IsCaptionStartTimeVisible
    {
      get { return (bool)GetValue(IsCaptionStartTimeVisibleProperty); }
      set { SetValue(IsCaptionStartTimeVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsCaptionStartTimeVisible property.
    /// </summary>
    private static void OnIsCaptionStartTimeVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnIsCaptionStartTimeVisibleChanged((bool)e.OldValue, target.IsCaptionStartTimeVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsCaptionStartTimeVisible property.
    /// </summary>
    protected virtual void OnIsCaptionStartTimeVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnStartTime.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region IsCaptionEndTimeVisible

    /// <summary>
    /// IsCaptionEndTimeVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsCaptionEndTimeVisibleProperty =
        DependencyProperty.Register("IsCaptionEndTimeVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnIsCaptionEndTimeVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsCaptionEndTimeVisible property. 
    /// </summary>
    public bool IsCaptionEndTimeVisible
    {
      get { return (bool)GetValue(IsCaptionEndTimeVisibleProperty); }
      set { SetValue(IsCaptionEndTimeVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsCaptionEndTimeVisible property.
    /// </summary>
    private static void OnIsCaptionEndTimeVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnIsCaptionEndTimeVisibleChanged((bool)e.OldValue, target.IsCaptionEndTimeVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsCaptionEndTimeVisible property.
    /// </summary>
    protected virtual void OnIsCaptionEndTimeVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnEndTime.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region IsCaptionDurationVisible

    /// <summary>
    /// IsCaptionDurationVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsCaptionDurationVisibleProperty =
        DependencyProperty.Register("IsCaptionDurationVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnIsCaptionDurationVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsCaptionDurationVisible property. 
    /// </summary>
    public bool IsCaptionDurationVisible
    {
      get { return (bool)GetValue(IsCaptionDurationVisibleProperty); }
      set { SetValue(IsCaptionDurationVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsCaptionDurationVisible property.
    /// </summary>
    private static void OnIsCaptionDurationVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnIsCaptionDurationVisibleChanged((bool)e.OldValue, target.IsCaptionDurationVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsCaptionDurationVisible property.
    /// </summary>
    protected virtual void OnIsCaptionDurationVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnDuration.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region IsCaptionContentVisible

    /// <summary>
    /// IsCaptionContentVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsCaptionContentVisibleProperty =
        DependencyProperty.Register("IsCaptionContentVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnIsCaptionContentVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsCaptionContentVisible property. 
    /// </summary>
    public bool IsCaptionContentVisible
    {
      get { return (bool)GetValue(IsCaptionContentVisibleProperty); }
      set { SetValue(IsCaptionContentVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsCaptionContentVisible property.
    /// </summary>
    private static void OnIsCaptionContentVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnIsCaptionContentVisibleChanged((bool)e.OldValue, target.IsCaptionContentVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsCaptionContentVisible property.
    /// </summary>
    protected virtual void OnIsCaptionContentVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnContent.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #endregion

  }

}
