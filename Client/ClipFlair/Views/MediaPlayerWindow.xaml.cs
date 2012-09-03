//Filename: MediaPlayerWindow.xaml.cs
//Version: 20120902

using ClipFlair.Models.Views;

using Extensions;

using System;
using System.Windows;
using System.ComponentModel;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.Views
{
  public partial class MediaPlayerWindow : FlipWindow
  {
    public MediaPlayerWindow()
    {
      View = new MediaPlayerView(); //must set the view first
      InitializeComponent();
    }

    #region --- Properties ---

    #region View

    public MediaPlayerView View
    {
      get { return (MediaPlayerView)DataContext; }
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
      if (e.PropertyName == null)
      {
        Source = View.Source;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case IMediaPlayerProperties.PropertySource:
            Source = View.Source;
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
        DependencyProperty.Register(IMediaPlayerProperties.PropertySource, typeof(Uri), typeof(MediaPlayerWindow),
            new FrameworkPropertyMetadata((Uri)IMediaPlayerDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

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
      MediaPlayerWindow target = (MediaPlayerWindow)d;
      target.OnSourceChanged((Uri)e.OldValue, target.Source);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
    {
      View.Source = newSource;
      player.Source = newSource;
    }

    #endregion

    #region Markers

    /// <summary>
    /// Markers Dependency Property
    /// </summary>
    public static readonly DependencyProperty MarkersProperty =
        DependencyProperty.Register("Markers", typeof(MediaMarkerCollection<TimedTextElement>), typeof(MediaPlayerWindow),
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
      MediaPlayerWindow target = (MediaPlayerWindow)d;
      MediaMarkerCollection<TimedTextElement> oldMarkers = (MediaMarkerCollection<TimedTextElement>)e.OldValue;
      MediaMarkerCollection<TimedTextElement> newMarkers = target.Markers;
      target.OnMarkersChanged(oldMarkers, newMarkers);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Markers property.
    /// </summary>
    protected virtual void OnMarkersChanged(MediaMarkerCollection<TimedTextElement> oldMarkers, MediaMarkerCollection<TimedTextElement> newMarkers)
    {
      player.Markers = newMarkers;
    }

    #endregion

    #endregion

  }

}