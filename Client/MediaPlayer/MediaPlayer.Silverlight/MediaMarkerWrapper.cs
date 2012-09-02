//Filename: MediaMarkerWrapper.cs
//Version: 20120902

using System;
using System.Windows;
using System.ComponentModel;

using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Zoomicon.MediaPlayer
{
  
  public class MediaMarkerWrapper<T> : DependencyObject where T : MediaMarker
  {

    #region --- Fields ---

    private T marker;

    #endregion

    #region --- Properties ---

    #region Marker

    public T Marker
    {
      get { return marker; }
      set
      {
        //remove property changed handler from old media marker
        if (marker != null)
          ((INotifyPropertyChanged)marker).PropertyChanged -= new PropertyChangedEventHandler(Marker_PropertyChanged);
  
        //set the new media marker
        marker = value;
      
        //add property changed handler to new media marker
        if (marker != null)
          ((INotifyPropertyChanged)marker).PropertyChanged += new PropertyChangedEventHandler(Marker_PropertyChanged);
      }
    }

    #endregion
    
    #region Begin

    /// <summary>
    /// Begin Dependency Property
    /// </summary>
    public static readonly DependencyProperty BeginProperty =
        DependencyProperty.Register("Begin", typeof(TimeSpan), typeof(MediaMarkerWrapper<T>),
            new FrameworkPropertyMetadata(TimeSpan.Zero,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnBeginChanged)));

    /// <summary>
    /// Gets or sets the Begin property.
    /// </summary>
    public TimeSpan Begin
    {
      get { return (TimeSpan)GetValue(BeginProperty); }
      set { SetValue(BeginProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Begin property.
    /// </summary>
    private static void OnBeginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaMarkerWrapper<T> target = (MediaMarkerWrapper<T>)d;
      TimeSpan oldBegin = (TimeSpan)e.OldValue;
      TimeSpan newBegin = target.Begin;
      target.OnBeginChanged(oldBegin, newBegin);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Begin property.
    /// </summary>
    protected virtual void OnBeginChanged(TimeSpan oldBegin, TimeSpan newBegin)
    {
      marker.Begin = newBegin;
    }

    #endregion

    #region End

    /// <summary>
    /// End Dependency Property
    /// </summary>
    public static readonly DependencyProperty EndProperty =
        DependencyProperty.Register("End", typeof(TimeSpan), typeof(MediaMarkerWrapper<T>),
            new FrameworkPropertyMetadata(TimeSpan.Zero,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnEndChanged)));

    /// <summary>
    /// Gets or sets the End property.
    /// </summary>
    public TimeSpan End
    {
      get { return (TimeSpan)GetValue(EndProperty); }
      set { SetValue(EndProperty, value); }
    }

    /// <summary>
    /// Handles changes to the End property.
    /// </summary>
    private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaMarkerWrapper<T> target = (MediaMarkerWrapper<T>)d;
      TimeSpan oldEnd = (TimeSpan)e.OldValue;
      TimeSpan newEnd = target.End;
      target.OnEndChanged(oldEnd, newEnd);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the End property.
    /// </summary>
    protected virtual void OnEndChanged(TimeSpan oldEnd, TimeSpan newEnd)
    {
      marker.End = newEnd;
    }

    #endregion

    #region Duration

    /// <summary>
    /// Duration Dependency Property
    /// </summary>
    public static readonly DependencyProperty DurationProperty =
        DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(MediaMarkerWrapper<T>),
            new FrameworkPropertyMetadata(TimeSpan.Zero,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnDurationChanged)));

    /// <summary>
    /// Gets or sets the Duration property.
    /// </summary>
    public TimeSpan Duration
    {
      get { return (TimeSpan)GetValue(DurationProperty); }
      set { SetValue(DurationProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Duration property.
    /// </summary>
    private static void OnDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaMarkerWrapper<T> target = (MediaMarkerWrapper<T>)d;
      TimeSpan oldDuration = (TimeSpan)e.OldValue;
      TimeSpan newDuration = target.Duration;
      target.OnDurationChanged(oldDuration, newDuration);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Duration property.
    /// </summary>
    protected virtual void OnDurationChanged(TimeSpan oldDuration, TimeSpan newDuration)
    {
      marker.End = marker.Begin + newDuration;
    }

    #endregion

    #region Content

    /// <summary>
    /// Content Dependency Property
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register("Content", typeof(object), typeof(MediaMarkerWrapper<T>),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnContentChanged)));

    /// <summary>
    /// Gets or sets the Content property.
    /// </summary>
    public object Content
    {
      get { return (object)GetValue(ContentProperty); }
      set { SetValue(ContentProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Content property.
    /// </summary>
    private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaMarkerWrapper<T> target = (MediaMarkerWrapper<T>)d;
      object oldContent = (object)e.OldValue;
      object newContent = target.Content;
      target.OnContentChanged(oldContent, newContent);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Content property.
    /// </summary>
    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      marker.Content = newContent;
    }

    #endregion

    #endregion

    #region --- Methods ---

    public MediaMarkerWrapper(T theMediaMarker)
    {
      Marker = theMediaMarker; //set the property here (not the field) so that it attaches property change event handler
    }

    #endregion

    #region --- Events ---

    protected void Marker_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == null)
      {
        Begin = marker.Begin;
        End = marker.End;
        Duration = marker.Duration;
        Content = marker.Content;
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case "Begin":
            Begin = marker.Begin;
            break;
          case "End":
            End = marker.End;
            break;
          case "Duration":
            Duration = marker.Duration;
            break;
          case "Content":
            Content = marker.Content;
            break;
        }
    }
    
    #endregion

  }
}
