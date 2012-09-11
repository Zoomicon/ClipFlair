//Filename: CaptionsGridWindow.xaml.cs
//Version: 20120911

using ClipFlair.Models.Views;

using System;
using System.ComponentModel;
using System.Windows;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.Views
{
  public partial class CaptionsGridWindow : FlipWindow
  {

    public CaptionsGridWindow()
    {
      View = new CaptionsGridView(); //must set the view first
      InitializeComponent();
    }

    #region --- Properties ---

    #region View

    public CaptionsGridView View
    {
      get { return (CaptionsGridView)DataContext; }
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          ((INotifyPropertyChanged)DataContext).PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged);
        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
        //set the new view (must do last)
        DataContext = value;
      }
    }

    protected void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Source = View.Source;
        Time = View.Time;
        CaptionVisible = View.CaptionVisible;
        CaptionAudioVisible = View.CaptionAudioVisible;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case ICaptionsGridProperties.PropertySource:
            Source = View.Source;
            break;
          case ICaptionsGridProperties.PropertyTime:
            Time = View.Time;
            break;
          case ICaptionsGridProperties.PropertyCaptionVisible:
            CaptionVisible = View.CaptionVisible;
            break;
          case ICaptionsGridProperties.PropertyCaptionAudioVisible:
            CaptionAudioVisible = View.CaptionAudioVisible;
            break;
          //...
        }
    }

    #endregion
    
    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(ICaptionsGridProperties.PropertySource, typeof(Uri), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata((Uri)ICaptionsGridDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

    /// <summary>
    /// Gets or sets the Source property.
    /// </summary>
    public Uri Source
    {
      get { return (Uri)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Source property.
    /// </summary>
    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGridWindow target = (CaptionsGridWindow)d;
      target.OnSourceChanged((Uri)e.OldValue, target.Source);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
    {
      View.Source = newSource;
      //TODO: load captions
    }

    #endregion

    #region Time

    /// <summary>
    /// Time Dependency Property
    /// </summary>
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register(ICaptionsGridProperties.PropertyTime, typeof(TimeSpan), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata(ICaptionsGridDefaults.DefaultTime,
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
      CaptionsGridWindow target = (CaptionsGridWindow)d;
      TimeSpan oldTime = (TimeSpan)e.OldValue;
      TimeSpan newTime = target.Time;
      target.OnTimeChanged(oldTime, newTime);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Time property.
    /// </summary>
    protected virtual void OnTimeChanged(TimeSpan oldTime, TimeSpan newTime)
    {
      View.Time = newTime;
    }

    #endregion

    #region Markers

    /// <summary>
    /// Markers Dependency Property
    /// </summary>
    public static readonly DependencyProperty MarkersProperty =
        DependencyProperty.Register("Markers", typeof(MediaMarkerCollection<TimedTextElement>), typeof(CaptionsGridWindow),
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
      CaptionsGridWindow target = (CaptionsGridWindow)d;
      MediaMarkerCollection<TimedTextElement> oldMarkers = (MediaMarkerCollection<TimedTextElement>)e.OldValue;
      MediaMarkerCollection<TimedTextElement> newMarkers = target.Markers;
      target.OnMarkersChanged(oldMarkers, newMarkers);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Markers property.
    /// </summary>
    protected virtual void OnMarkersChanged(MediaMarkerCollection<TimedTextElement> oldMarkers, MediaMarkerCollection<TimedTextElement> newMarkers)
    {
      gridCaptions.Markers = newMarkers;
    }

    #endregion

    #region CaptionVisible

    /// <summary>
    /// CaptionVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionVisibleProperty =
        DependencyProperty.Register(ICaptionsGridProperties.PropertyCaptionVisible, typeof(bool), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata(ICaptionsGridDefaults.DefaultCaptionVisible,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnCaptionVisibleChanged)));

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
      CaptionsGridWindow target = (CaptionsGridWindow)d;
      bool oldCaptionVisible = (bool)e.OldValue;
      bool newCaptionVisible = target.CaptionVisible;
      target.OnCaptionVisibleChanged(oldCaptionVisible, newCaptionVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionVisible property.
    /// </summary>
    protected virtual void OnCaptionVisibleChanged(bool oldCaptionVisible, bool newCaptionVisible)
    {
      View.CaptionVisible = newCaptionVisible;
    }

    #endregion

    #region CaptionAudioVisible

    /// <summary>
    /// CaptionAudioVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionAudioVisibleProperty =
        DependencyProperty.Register(ICaptionsGridProperties.PropertyCaptionAudioVisible, typeof(bool), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata(ICaptionsGridDefaults.DefaultCaptionAudioVisible,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnCaptionAudioVisibleChanged)));

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
      CaptionsGridWindow target = (CaptionsGridWindow)d;
      bool oldCaptionAudioVisible = (bool)e.OldValue;
      bool newCaptionAudioVisible = target.CaptionAudioVisible;
      target.OnCaptionAudioVisibleChanged(oldCaptionAudioVisible, newCaptionAudioVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionAudioVisible property.
    /// </summary>
    protected virtual void OnCaptionAudioVisibleChanged(bool oldCaptionAudioVisible, bool newCaptionAudioVisible)
    {
      View.CaptionAudioVisible = newCaptionAudioVisible;
    }

    #endregion

    #endregion

  }

}