//Filename: CaptionsGrid.xaml.cs
//Version: 20120902

using Zoomicon.MediaPlayer;

using System.Windows;
using System.Windows.Controls;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace Zoomicon.CaptionsGrid
{
  public partial class CaptionsGrid : UserControl
  {
    #region Constants

    public const int ColIndexStartTime = 0;
    public const int ColIndexEndTime = 1;
    public const int ColIndexDuration = 2;
    public const int ColIndexContent = 3;

    #endregion

    public CaptionsGrid()
    {
      InitializeComponent();
    }

    #region --- Properties ---

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
      gridCaptions.Columns[ColIndexStartTime].Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
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
      gridCaptions.Columns[ColIndexEndTime].Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
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
      gridCaptions.Columns[ColIndexDuration].Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
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
      gridCaptions.Columns[ColIndexContent].Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
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
      DataContext = /*new MediaMarkerCollectionWrapper<TimedTextElement>*/(newMarkers);
    }

    #endregion

    #endregion

  }

}
