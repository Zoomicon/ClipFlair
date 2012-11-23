//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: AudioRecorderControl.xaml.cs
//Version: 20121123

using WPFCompatibility;

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.AudioRecorder
{
  public partial class AudioRecorderControl : UserControl
  {
 
    public AudioRecorderControl()
    {
      InitializeComponent();
      View = new AudioRecorderView();
    }

    public AudioRecorderView View
    {
      get { return (AudioRecorderView)DataContext; }
      set {
        value.SetToggleButtons(btnRecord, btnPlay);
        DataContext = value; 
      }
    }

    public void Play()
    {
      //StopPlayback(); //stop any current playback //don't call this, since when clicking on Play button at other grid row we have the button checked then unchecked by this
      View.PlayCommand.IsChecked = true; //don't talk to ToggleButton directly
    }

    public void StopPlayback()
    {
      View.PlayCommand.IsChecked = false; //don't talk to ToggleButton directly
    }

  }

}
