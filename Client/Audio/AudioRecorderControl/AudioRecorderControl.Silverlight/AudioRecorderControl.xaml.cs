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

            ViewModel = new AudioRecorderViewModel(btnRecord);

            //ViewModel.StopCommand.CanExecuteChanged += new EventHandler(StopCommand_CanExecuteChanged);

            DataContext = ViewModel;
        }

      /*
        void StopCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            //tapeMeter.MeterMode = ((ICommand)sender).CanExecute(null) ? Codeplex.Dashboarding.Odometer.Mode.AutoIncrement : Codeplex.Dashboarding.Odometer.Mode.Static;
        }
       */

    public void Play()
    {
      ViewModel.Play();
    }

  }

}
