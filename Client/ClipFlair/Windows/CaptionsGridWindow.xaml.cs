//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionsGridWindow.xaml.cs
//Version: 20121004

using ClipFlair.Models.Views;
using ClipFlair.Windows.Views;

using System;
using System.ComponentModel;
using System.Windows;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.Windows
{
  public partial class CaptionsGridWindow : BaseWindow
  {

    public CaptionsGridWindow()
    {
      View = new CaptionsGridView(); //must set the view first
      InitializeComponent();
    }

    #region --- Properties ---

    #region View

    public new ICaptionsGrid View //hiding parent property
    {
      get { return (ICaptionsGrid)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

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

    #region Captions

    /// <summary>
    /// Captions Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionsProperty =
        DependencyProperty.Register("Captions", typeof(CaptionRegion), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.None,
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
      CaptionsGridWindow target = (CaptionsGridWindow)d;
      CaptionRegion oldCaptions = (CaptionRegion)e.OldValue;
      CaptionRegion newCaptions = target.Captions;
      target.OnCaptionsChanged(oldCaptions, newCaptions);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Captions property.
    /// </summary>
    protected virtual void OnCaptionsChanged(CaptionRegion oldCaptions, CaptionRegion newCaptions)
    {
      //NOP (using two-way data binding)
      //gridCaptions.Captions = newCaptions;
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