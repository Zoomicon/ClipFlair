//Filename: AudioRecorderControl.xaml.cs
//Version: 20120911

using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AudioRecorder
{
    public partial class AudioRecorderControl : UserControl
    {
        AudioRecorderViewModel ViewModel;

        public AudioRecorderControl()
        {
            InitializeComponent();

            ViewModel = new AudioRecorderViewModel();

            ViewModel.PlayPauseCommand.CanExecuteChanged += new EventHandler(PlayPauseCommand_CanExecuteChanged);

            DataContext = ViewModel;
        }

        void PlayPauseCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            //tapeMeter.MeterMode = ((ICommand)sender).CanExecute(null) ? Codeplex.Dashboarding.Odometer.Mode.AutoIncrement : Codeplex.Dashboarding.Odometer.Mode.Static;
        }

    }
}
