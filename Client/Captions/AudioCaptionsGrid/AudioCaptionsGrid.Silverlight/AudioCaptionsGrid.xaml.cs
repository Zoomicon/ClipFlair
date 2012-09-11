//Filename: AudioCaptionsGrid.xaml.cs
//Version: 20120911

using Zoomicon.CaptionsGrid;
using WPFCompatibility;

using System;
using System.Windows;
using System.ComponentModel;

namespace Zoomicon.AudioCaptionsGrid
{
  public partial class AudioCaptionsGrid : CaptionsGrid.CaptionsGrid
  {
    public AudioCaptionsGrid()
    {
    }

    #region --- Properties ---

    #region IsCaptionAudioVisible

    /// <summary>
    /// IsCaptionAudioVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsCaptionAudioVisibleProperty =
        DependencyProperty.Register("IsCaptionAudioVisible", typeof(bool), typeof(CaptionsGrid),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnIsCaptionAudioVisibleChanged)));

    /// <summary>
    /// Gets or sets the IsCaptionAudioVisible property. 
    /// </summary>
    public bool IsCaptionAudioVisible
    {
      get { return (bool)GetValue(IsCaptionAudioVisibleProperty); }
      set { SetValue(IsCaptionAudioVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsCaptionAudioVisible property.
    /// </summary>
    private static void OnIsCaptionAudioVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CaptionsGrid target = (CaptionsGrid)d;
      target.OnIsCaptionAudioVisibleChanged((bool)e.OldValue, target.IsCaptionAudioVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsCaptionAudioVisible property.
    /// </summary>
    protected virtual void OnIsCaptionAudioVisibleChanged(bool oldValue, bool newValue)
    {
      ColumnAudio.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #endregion

  }

}