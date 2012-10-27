//Filename: AudioRecorderControl.xaml.cs
//Version: 20120912

using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClipFlair.AudioRecorder
{
    public partial class AudioRecorderControl : UserControl
    {
        AudioRecorderViewModel ViewModel;

        public AudioRecorderControl()
        {
            InitializeComponent();

            ViewModel = new AudioRecorderViewModel(btnRecord, btnPlay);

            DataContext = ViewModel;
        }

    public void Play()
    {
      //StopPlayback(); //stop any current playback //don't call this, since when clicking on Play button at other grid row we have the button checked then unchecked by this
      ViewModel.PlayCommand.IsChecked = true; //don't talk to ToggleButton directly
    }

    public void StopPlayback()
    {
      ViewModel.PlayCommand.IsChecked = false; //don't talk to ToggleButton directly
    }

    public double Volume
    {
      get { return ViewModel.Volume; }
      set { ViewModel.Volume = value;  }
    }

  }

}
