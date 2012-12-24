//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: AudioRecorderControl.xaml.cs
//Version: 20121224

using WPFCompatibility;

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.AudioRecorder
{
  public partial class AudioRecorderControl : UserControl
  {
 
    public AudioRecorderControl()
    {
      View = new AudioRecorderView(); //must do first
      InitializeComponent();

      //Unloaded += (s, e) => View = null; //release property change event handlers
    }

    #region Properties

    #region View

    public AudioRecorderView View
    {
      get { return (AudioRecorderView)DataContext; }
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          View.PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged); //IView inherits from INotifyPropertyChanged
        
        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);

        //set the new view (must do after setting property change event handler)
        DataContext = value;

        View_PropertyChanged(null, new PropertyChangedEventArgs(null)); //notify property change listeners that all properties of the view changed
      }
    }

    protected virtual void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Audio = View.Audio;
        //...
      }
      else switch (e.PropertyName)
        {
          case AudioRecorderView.PROPERTY_AUDIO:
            Audio = View.Audio;
            break; 
          default:
            //NOP
            break;
          //...
        }
    }

    #endregion

     #region Audio

    /// <summary>
    /// Audio Dependency Property
    /// </summary>
    public static readonly DependencyProperty AudioProperty =
        DependencyProperty.Register("Audio", typeof(Stream), typeof(AudioRecorderControl),
            new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnAudioChanged)));

    /// <summary>
    /// Gets or sets the Audio property.
    /// </summary>
    public Stream Audio
    {
      get { return (Stream)GetValue(AudioProperty); }
      set { SetValue(AudioProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Audio property.
    /// </summary>
    private static void OnAudioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      AudioRecorderControl target = (AudioRecorderControl)d;
      Stream oldAudio = (Stream)e.OldValue;
      Stream newAudio = target.Audio;
      target.OnAudioChanged(oldAudio, newAudio);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Audio property.
    /// </summary>
    protected virtual void OnAudioChanged(Stream oldAudio, Stream newAudio)
    {
      View.Audio = newAudio;
    }

    #endregion

    #endregion

  }

}
