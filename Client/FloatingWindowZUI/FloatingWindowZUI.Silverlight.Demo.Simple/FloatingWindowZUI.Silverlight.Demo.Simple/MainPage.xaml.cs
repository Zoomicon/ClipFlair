﻿using System.Diagnostics;
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
            window.Show();
        }

        private void ShowNewWindow_Click(object sender, RoutedEventArgs e)
        {
            FloatingWindow window = new FloatingWindow();
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
            host.Add(window);

            window.Activated += (s, a) =>
            {
                Debug.WriteLine("Activated: {0}", window.IconText);
            };

            window.Deactivated += (s, a) =>
            {
                Debug.WriteLine("Deactivated: {0}", window.IconText);
            };

            window.Show(200, 100);
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
