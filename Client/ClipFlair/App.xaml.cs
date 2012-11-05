//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App.xaml.cs
//Version: 20121104

using ClipFlair.Windows;

using SilverFlow.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ClipFlair
{
  public partial class App : Application
  {

    public App()
    {
      this.Startup += this.Application_Startup;
      this.Exit += this.Application_Exit;
      this.UnhandledException += this.Application_UnhandledException;

      InitializeComponent();
    }

    #region Lifetime

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      //this.RootVisual = new SplashScreen(); //TODO: find some way to show SplashScreen (in case instantiating/initializing ActivityContainer takes some time)
      //Update();
      //System.Threading.Thread.Sleep(3000); //3 sec delay (for testing only)
      FrameworkElement c=new ActivityContainer(); //ActivityContainerWindow
      FloatingWindow w = (c as FloatingWindow);
      if (w != null)
      {
        w.ResizeEnabled = false;
        w.Visibility = Visibility.Visible;
      }
      this.RootVisual = c;
      Update();
      //MessageBox.Show("ClipFlair loaded"); //uncomment this to try splash screen
    }

    private void Application_Exit(object sender, EventArgs e)
    {

    }

    #endregion

    #region Update

    private void Update()
    {
      if (!IsRunningOutOfBrowser) return;

      CheckAndDownloadUpdateCompleted += new CheckAndDownloadUpdateCompletedEventHandler(OnCheckAndDownloadUpdateCompleted); //attach event handler
      try
      {
        CheckAndDownloadUpdateAsync();
      }
      catch
      {
        //Ignore any exceptions (e.g. when offline)
      }
    }

    private void OnCheckAndDownloadUpdateCompleted(object sender, CheckAndDownloadUpdateCompletedEventArgs e)
    {
      CheckAndDownloadUpdateCompleted -= new CheckAndDownloadUpdateCompletedEventHandler(OnCheckAndDownloadUpdateCompleted); //detach event handler

      if (e.UpdateAvailable)
      {
        MessageBox.Show("Update has been downloaded, will be used at next application launch");
      }
      else
      {
        MessageBox.Show("Couldn't download application update: " + e.Error.Message);
      }
    }

    #endregion

    #region Errors

    private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
    {
      // If the app is running outside of the debugger then report the exception using
      // the browser's exception mechanism. On IE this will display it a yellow alert 
      // icon in the status bar and Firefox will display a script error.
      if (!System.Diagnostics.Debugger.IsAttached)
      {

        // NOTE: This will allow the application to continue running after an exception has been thrown
        // but not handled. 
        // For production applications this error handling should be replaced with something that will 
        // report the error to the website and stop the application.
        e.Handled = true;
        Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
      }
    }

    private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
    {
      try
      {
        string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
        errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

        System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
      }
      catch (Exception)
      {
      }
    }

    #endregion

  }
}
