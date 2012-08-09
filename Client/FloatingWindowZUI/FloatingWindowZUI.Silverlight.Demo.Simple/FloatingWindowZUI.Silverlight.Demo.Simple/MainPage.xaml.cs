//Filename MainPage.xaml.cs
//Version: 20120809

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using SilverFlow.Controls;
using SilverFlow.Controls.Extensions;

namespace FloatingWindowZUI.Demo
{
    public partial class MainPage : UserControl
    {

        private int nWindows = 1;
        private Point startPoint = new Point(50, 50);

        public MainPage()
        {
            InitializeComponent();

            FloatingWindow window = new FloatingWindow();
            window.Title = "Centered Window";
            window.IconText = "Centered Window";
            host.Add(window);
            window.Show(startPoint); //since we use a large zoomable canvas area, show near the start instead of centered
            startPoint = startPoint.Add(20, 20);
        }

        private void ShowNewWindow_Click(object sender, RoutedEventArgs e)
        {
            FloatingWindow window = new FloatingWindow();
            window.Scale = 1d/host.ZoomHost.ContentScale; //TODO: !!! don't use host.ContentScale, has bug and is always 1
            host.Add(window);

            string title = "Window " + nWindows++;
            window.Title = title;
            window.IconText = title;

            window.Activated += (s, a) =>
            {
                Debug.WriteLine("Activated: {0}", window.IconText);
            };

            window.Deactivated += (s, a) =>
            {
                Debug.WriteLine("Deactivated: {0}", window.IconText);
            };

            window.Show(startPoint);
            startPoint = startPoint.Add(20, 20);
        }

        private void ShowWindowWithIcon_Click(object sender, RoutedEventArgs e)
        {
            WindowWithIcon window = new WindowWithIcon();
            window.Scale = 1d / host.ZoomHost.ContentScale; //TODO: !!! don't use host.ContentScale, has bug and is always 1
            host.Add(window);

            window.Activated += (s, a) =>
            {
                Debug.WriteLine("Activated: {0}", window.IconText);
            };

            window.Deactivated += (s, a) =>
            {
                Debug.WriteLine("Deactivated: {0}", window.IconText);
            };

            window.Show(startPoint);
            startPoint = startPoint.Add(20, 20);
        }

        private void ShowIconbar_Click(object sender, RoutedEventArgs e)
        {
            host.ShowIconBar();
        }

        private void HideIconbar_Click(object sender, RoutedEventArgs e)
        {
            host.HideIconBar();
        }

        private void CloseWindows_Click(object sender, RoutedEventArgs e)
        {
            host.CloseAllWindows();
        }

        private void sldZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
          if(host!=null) 
            //host.ContentScale = ((Slider)sender).Value; //why is sldZoom null here???
            host.ZoomHost.ContentScale = ((Slider)sender).Value; //why is sldZoom null here???
        }
    }
}
