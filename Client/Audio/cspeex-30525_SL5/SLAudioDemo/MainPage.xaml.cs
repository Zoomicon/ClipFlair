//Filename: MainPage.xaml.cs
//Version: 20120609
//Editor: George Birbilis <birbilis@kagi.com>

//Modified cspeex-30525 source from http://cspeex.codeplex.com to work with Silverlight 4 RC and higher (was for Silverlight 4 Beta)

using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using cspeex;
using java.io;

namespace SLAudioDemo
{
    public partial class MainPage : UserControl
    {
        private CaptureSource _captureSource;
        private ObservableCollection<WriteableBitmap> _images = new ObservableCollection<WriteableBitmap>();
        private StreamAudioSink streamAudioSink;

        public MainPage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // get list of audio sources
            AudioSources.ItemsSource = CaptureDeviceConfiguration.GetAvailableAudioCaptureDevices();

            // get list of the video sources
            VideoSources.ItemsSource = CaptureDeviceConfiguration.GetAvailableVideoCaptureDevices();

            // creating a new capture source
            _captureSource = new CaptureSource();

            // set event handler for image capture
            _captureSource.CaptureImageCompleted += (source, eventargs) =>
            {
                _images.Add(eventargs.Result);
            }; //Birbilis: using Silverlight 4 RC and higher API, not Silverlight Beta ("ImageCaptureAsync" method has been replaced by "CaptureImageAsync")

            streamAudioSink = new StreamAudioSink();

            streamAudioSink.CaptureSource = _captureSource;

            // bind snapshot images
            Snapshots.ItemsSource = _images;
        }

        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            if (_captureSource != null)
            {
                _captureSource.Stop(); // stop whatever device may be capturing

                // set the devices for the capture source
                _captureSource.VideoCaptureDevice = (VideoCaptureDevice)VideoSources.SelectedItem;
                _captureSource.AudioCaptureDevice = (AudioCaptureDevice)AudioSources.SelectedItem;

                // create the brush
                VideoBrush vidBrush = new VideoBrush();
                vidBrush.SetSource(_captureSource);
                WebcamCapture.Fill = vidBrush; // paint the brush on the rectangle

                // request user permission and display the capture
                if (CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess())
                {
                    _captureSource.Start();
                }
            }
        }

        private void StopCapture_Click(object sender, RoutedEventArgs e)
        {
            if (_captureSource != null)
            {
                _captureSource.Stop();
            }
        }

        private void TakeSnapshot_Click(object sender, RoutedEventArgs e)
        {
            if (_captureSource != null)
            {
                try
                {
                    // capture the current frame and add it to our observable collection
                    _captureSource.CaptureImageAsync();
                }
                catch (System.InvalidOperationException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void SaveAudio_Click(object sender, RoutedEventArgs e)
        {
            if (_captureSource.State != CaptureState.Stopped)
            {
                MessageBox.Show("Stop capture!");
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Audio files (*.wav)|*.wav",
                    FilterIndex = 1
                };

                if (sfd.ShowDialog() == true)
                {
                    // User selected item. Only property we can get to is.
                    using (Stream stream = sfd.OpenFile())
                    {
                        JSpeexDec decoder = new JSpeexDec();
                        decoder.setDestFormat(JSpeexDec.FILE_FORMAT_WAVE);
                        decoder.setStereo(true);

                        Stream memStream = streamAudioSink.MemFile.InnerStream;
                        memStream.Position = 0;

                        decoder.decode(new RandomInputStream(memStream), new RandomOutputStream(stream));

                        stream.Close();
                    }
                }
            }
        }
    }
}
