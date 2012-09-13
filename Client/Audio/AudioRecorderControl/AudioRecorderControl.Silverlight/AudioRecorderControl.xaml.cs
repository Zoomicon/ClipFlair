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
      btnPlay.IsChecked = true; //play
    }

    public void StopPlayback()
    {
      btnPlay.IsChecked = false;
    }

    public double Volume
    {
      get { return ViewModel.Volume; }
      set { ViewModel.Volume = value;  }
    }

  }

}
