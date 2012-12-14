//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ExecUtils.cs
//Version: 20121213

//source: http://social.msdn.microsoft.com/Forums/en-US/lightswitchgeneral/thread/5871c39a-a5fe-4d0c-a157-151442f6ee8d

using System;
using Microsoft.CSharp;
using System.Runtime.InteropServices.Automation;

namespace ClipFlair.Utils
{
  public class ExecUtils
  {

    public static void OpenHyperlink(string navigateUri)
    {
        if (AutomationFactory.IsAvailable)
        {
          dynamic shell = AutomationFactory.CreateObject("Shell.Application");
          shell.ShellExecute(navigateUri.ToString());
        }
        else if (!System.Windows.Application.Current.IsRunningOutOfBrowser)
        {
          System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(navigateUri), "_blank");
        }
        else
        {
          throw new InvalidOperationException();
        }
    }

  }
}
