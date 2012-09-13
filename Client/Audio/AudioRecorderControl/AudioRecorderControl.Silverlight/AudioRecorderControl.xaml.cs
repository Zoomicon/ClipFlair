//Filename: AudioRecorderControl.xaml.cs
//Version: 20120912

using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zoomicon.AudioRecorder
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
      StopPlayback(); //stop any current playback
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
