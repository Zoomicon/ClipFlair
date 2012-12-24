//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StringExtensions.cs
//Version: 20121224

using ClipFlair.Utils;

using System;
using System.Runtime.InteropServices.Automation;

namespace ClipFlair.Utils.Extensions
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
        }
       /**/

    }
}
