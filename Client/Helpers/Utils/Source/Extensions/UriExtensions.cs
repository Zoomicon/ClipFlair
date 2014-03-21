//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StringExtensions.cs
//Version: 20140321

using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#if SILVERLIGHT
using System.Runtime.InteropServices.Automation;
#endif

namespace Utils.Extensions
{
    public static class UriExtensions
    {

        /* //doesn't seem to work in Silverlight 5 
        public static void NavigateTo(this Uri link)
        {
          new ProgrammableHyperlink(link).DoClick(); //this works both in-browser and in OOB mode
        }
        */

        /**/
        public static void NavigateTo(this Uri link)
        { //source: http://social.msdn.microsoft.com/Forums/en-US/lightswitchgeneral/thread/5871c39a-a5fe-4d0c-a157-151442f6ee8d
          #if SILVERLIGHT && !WINDOWS_PHONE
                      if (AutomationFactory.IsAvailable) //needs elevated rights
                      {
                        dynamic shell = AutomationFactory.CreateObject("Shell.Application");
                        shell.ShellExecute(link.ToString());
                      }
                      else if (!System.Windows.Application.Current.IsRunningOutOfBrowser) //works only when running in browser (not as OOB app)
                      {
                        System.Windows.Browser.HtmlPage.Window.Navigate(link, "_blank");
                      }
                      else
                      {
                        throw new InvalidOperationException();
                      }
          #else
          //TODO: implement for WPF (see http://social.msdn.microsoft.com/Forums/vstudio/en-US/61fd208c-140e-4cf0-8876-a34731da5d20/converting-silverlight-4-automation-factory-to-net40-wpf)
          #endif
        } //USE PROGRAMMABLEHYPERLINK CLASS?
       /**/

        public static Image CreateImage(this Uri uri, double? width = null, double? height = null)
        {
          Image img = new Image(); //image control
          img.Stretch = Stretch.Uniform;
          if (width != null) img.Width = (double)width;
          if (height != null) img.Height = (double)height;
          BitmapImage bi = new BitmapImage(uri); //image (object) source
          img.Source = bi;
          img.Tag = bi.UriSource.ToString();
          return img;
        }

    }
}
