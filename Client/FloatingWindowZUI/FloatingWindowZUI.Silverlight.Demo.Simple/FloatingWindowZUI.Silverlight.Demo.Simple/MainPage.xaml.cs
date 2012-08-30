//Filename MainPage.xaml.cs
//Version: 20120823

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
      ShowNewWindow(false);
    }

    private void ShowNewWindow(bool withIcon)
    {
      FloatingWindow window = (withIcon)? new WindowWithIcon() : new FloatingWindow();

      if (Application.Current.Host.Settings.EnableGPUAcceleration) //GPU acceleration can been turned on at HTML/ASPX page or at OOB settings for OOB apps
        window.CacheMode = new BitmapCache(); //must do this before setting the "Scale" property, since it will set "RenderAtScale" property of the BitmapCache
      
      window.Scale = 1d / host.ZoomHost.ContentScale; //TODO: !!! don't use host.ContentScale, has bug and is always 1
          
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

      window.HelpRequested += (s, a) =>
      {
        Debug.WriteLine("Help button pressed");
        MessageBox.Show("Help...");
      };

      window.OptionsRequested += (s, a) =>
      {
        Debug.WriteLine("Options button pressed");
        MessageBox.Show("Options...");
      };

      window.Show(startPoint);
      startPoint = startPoint.Add(20, 20);
    }

    private void ShowNewWindow_Click(object sender, RoutedEventArgs e)
    {
      ShowNewWindow(false);
    }

    private void ShowWindowWithIcon_Click(object sender, RoutedEventArgs e)
    {
      ShowNewWindow(true);
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
      if (host != null)
        //host.ContentScale = ((Slider)sender).Value; //why is sldZoom null here??? //TODO: fix with PropertyChangeEvent that is missing
        host.ZoomHost.ContentScale = ((Slider)sender).Value; //why is sldZoom null here???
    }
  }
}
