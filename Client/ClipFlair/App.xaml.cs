//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App.xaml.cs
//Version: 20121206

using ClipFlair.Windows;
using SilverFlow.Controls;

using System;
using System.Windows;

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
      UpdateOOB(); //TODO: run this from background thread, seems to take some time //CALLING THIS FIRST, SINCE THE REST OF THE CODE COULD THROW AN EXCEPTION WHICH WOULD BLOCK UPDATES (AND ALSO TO MAKE USE OF THE TIME TO SET UP THE APP, SINCE UPDATING OCCURS IN THE BACKGROUND)
      
      FloatingWindowHost host = new FloatingWindowHost(); //don't use FloatingWindowHostZUI here
      
      ActivityWindow activityWindow = new ActivityWindow();
      activityWindow.IsTopLevel = true; //hide backpanel properties not relevant when not being a child window
      host.Add(activityWindow);

      activityWindow.Position = new Point(0, 0);
      activityWindow.MaximizeWindow(); //TODO: seems MaximizeAction is broken, need to check original FloatingWindow control (Silverlight version)
      
      activityWindow.ShowMaximizeButton = false;
      activityWindow.ShowMinimizeButton = false;
      activityWindow.ShowCloseButton = false;
      
      this.RootVisual = host; //new ActivityContainer();
      host.Rendered += (s, ev) => {
        host.IsBottomBarVisible = false;
        activityWindow.Width = host.ActualWidth;
        activityWindow.Height = host.ActualHeight;
      };
  
      //MessageBox.Show("ClipFlair loaded"); //uncomment this to test the loading indicator
    }

    private void Application_Exit(object sender, EventArgs e)
    {
      //TODO: maybe should store current state at isolated storage and offer to restore next time (unless an activity file/url is given as param to load)
    }

    #endregion

    #region Update

    private void UpdateOOB()
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

      if (e.UpdateAvailable) //update was found and downloaded
        MessageBox.Show("Update has been downloaded, will be used at next application launch"); //TODO: should try to show this on UI thread?
      else if (e.Error!=null) //error during update process
        MessageBox.Show("Couldn't download application update: " + e.Error.Message);
    }

    #endregion

    #region Errors

    private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
    {
      // If the app is running outside of the debugger then report the exception using
      // the browser's exception mechanism. On IE this will display it a yellow alert 
      // icon in the status bar and Firefox will display a script error.
      //if (!System.Diagnostics.Debugger.IsAttached) //THIS DOESN'T SEEM TO WORK, FREEZES
      {
        // NOTE: This will allow the application to continue running after an exception has been thrown
        // but not handled. 
        // For production applications this error handling should be replaced with something that will 
        // report the error to the website and stop the application.

        MessageBox.Show("Unexpected error: " + e.ExceptionObject.Message + "\n" + e.ExceptionObject.StackTrace); //TODO: find parent window?
        e.Handled = true;

        //Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); }); //don't use this, non-useful browser message
      }
    }

 /*
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
*/
    #endregion

  }
}
