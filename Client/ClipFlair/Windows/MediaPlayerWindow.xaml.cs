//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MediaPlayerWindow.xaml.cs
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
  public partial class MediaPlayerWindow : BaseWindow
  {
    public MediaPlayerWindow()
    {
      View = new MediaPlayerView(); //must set the view first
      InitializeComponent();
    }

    #region --- Properties ---

    #region View

    public new IMediaPlayer View //hiding parent property
    {
      get {return (IMediaPlayer)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Source = View.Source;
        Time = View.Time;
        ControllerVisible = View.ControllerVisible;
        CaptionsVisible = View.CaptionsVisible;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case IMediaPlayerProperties.PropertySource:
            Source = View.Source;
            break;
          case IMediaPlayerProperties.PropertyTime:
            Time = View.Time;
            break;
          case IMediaPlayerProperties.PropertyControllerVisible:
            ControllerVisible = View.ControllerVisible;
            break;
          case IMediaPlayerProperties.PropertyCaptionsVisible:
            CaptionsVisible = View.CaptionsVisible;
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
      //player.Source = newSource; //not used, using data binding instead in the XAML
    }

    #endregion

    #region Time

    /// <summary>
    /// Time Dependency Property
    /// </summary>
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register(IMediaPlayerProperties.PropertyTime, typeof(TimeSpan), typeof(MediaPlayerWindow),
            new FrameworkPropertyMetadata(IMediaPlayerDefaults.DefaultTime,
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
      MediaPlayerWindow target = (MediaPlayerWindow)d;
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
      if (player.Time != newTime) //check this for speedup and to avoid loops
        player.Time = newTime;
    }

    #endregion

    #region Captions

    /// <summary>
    /// Captions Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionsProperty =
        DependencyProperty.Register("Captions", typeof(CaptionRegion), typeof(MediaPlayerWindow),
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
      MediaPlayerWindow target = (MediaPlayerWindow)d;
      CaptionRegion oldCaptions = (CaptionRegion)e.OldValue;
      CaptionRegion newCaptions = target.Captions;
      target.OnCaptionsChanged(oldCaptions, newCaptions);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Captions property.
    /// </summary>
    protected virtual void OnCaptionsChanged(CaptionRegion oldCaptions, CaptionRegion newCaptions)
    {
      //NOP (using two-way binding at the XAML)
      //player.Captions1 = newCaptions;
    }

    #endregion

    #region ControllerVisible

    /// <summary>
    /// ControllerVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty ControllerVisibleProperty =
        DependencyProperty.Register(IMediaPlayerProperties.PropertyControllerVisible, typeof(bool), typeof(MediaPlayerWindow),
            new FrameworkPropertyMetadata(IMediaPlayerDefaults.DefaultControllerVisible,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnControllerVisibleChanged)));

    /// <summary>
    /// Gets or sets the ControllerVisible property. This dependency property 
    /// indicates ....
    /// </summary>
    public bool ControllerVisible
    {
      get { return (bool)GetValue(ControllerVisibleProperty); }
      set { SetValue(ControllerVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ControllerVisible property.
    /// </summary>
    private static void OnControllerVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayerWindow target = (MediaPlayerWindow)d;
      bool oldControllerVisible = (bool)e.OldValue;
      bool newControllerVisible = target.ControllerVisible;
      target.OnControllerVisibleChanged(oldControllerVisible, newControllerVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ControllerVisible property.
    /// </summary>
    protected virtual void OnControllerVisibleChanged(bool oldControllerVisible, bool newControllerVisible)
    {
      View.ControllerVisible = newControllerVisible;
      player.IsControlStripVisible = newControllerVisible;
    }

    #endregion

    #region CaptionsVisible

    /// <summary>
    /// CaptionsVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty CaptionsVisibleProperty =
        DependencyProperty.Register(IMediaPlayerProperties.PropertyCaptionsVisible, typeof(bool), typeof(MediaPlayerWindow),
            new FrameworkPropertyMetadata(IMediaPlayerDefaults.DefaultCaptionsVisible,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnCaptionsVisibleChanged)));

    /// <summary>
    /// Gets or sets the CaptionsVisible property. This dependency property 
    /// indicates ....
    /// </summary>
    public bool CaptionsVisible
    {
      get { return (bool)GetValue(CaptionsVisibleProperty); }
      set { SetValue(CaptionsVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CaptionsVisible property.
    /// </summary>
    private static void OnCaptionsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      MediaPlayerWindow target = (MediaPlayerWindow)d;
      bool oldCaptionsVisible = (bool)e.OldValue;
      bool newCaptionsVisible = target.CaptionsVisible;
      target.OnCaptionsVisibleChanged(oldCaptionsVisible, newCaptionsVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CaptionsVisible property.
    /// </summary>
    protected virtual void OnCaptionsVisibleChanged(bool oldCaptionsVisible, bool newCaptionsVisible)
    {
      View.CaptionsVisible = newCaptionsVisible;
      player.CaptionsVisible = newCaptionsVisible;
    }

    #endregion

    private void btnSaveOffline_TextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {

    }

    #endregion

    private void btnSaveOffline_Click(object sender, RoutedEventArgs e) //TODO: doesn't seem to work, maybe needs offline cache plugin or respective SMF assemblies
    {
      player.StorePlaylistContentOffline("ClipFlairPlaylist"); //TODO: allow to define offline filename and maybe allow to delete old ones? (or show offline size and allow clear)
    }

    private void btnLoadOffline_Click(object sender, RoutedEventArgs e) //TODO: doesn't seem to work, maybe needs offline cache plugin or respective SMF assemblies
    {
      player.OpenOfflinePlaylist("ClipFlairPlaylist"); //TODO: allow to define offline filename and maybe allow to delete old ones? (or show offline size and allow clear)
    }

  }

}