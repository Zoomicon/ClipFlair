//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App.xaml.cs
//Version: 20130204

using ClipFlair.Windows;

using SilverFlow.Controls;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Threading;

namespace ClipFlair
{

  public partial class App : Application
  {
    
    #region Constants

    private const string CLIPFLAIR_GALLERY_ACTIVITY = "http://gallery.clipflair.net/activity/";
    private const string CLIPFLAIR_GALLERY_VIDEO = "http://gallery.clipflair.net/video/";
    private const string CLIPFLAIR_GALLERY_AUDIO = "http://gallery.clipflair.net/audio/";
    private const string CLIPFLAIR_GALLERY_TEXT = "http://gallery.clipflair.net/text/";
    private const string CLIPFLAIR_GALLERY_IMAGE = "http://gallery.clipflair.net/image/";
    private const string CLIPFLAIR_GALLERY_MAP = "http://gallery.clipflair.net/map/";

    private const string PARAMETER_ACTIVITY = "activity";
    private const string PARAMETER_COMPONENT = "component";
    private const string PARAMETER_MEDIA = "media";
    private const string PARAMETER_VIDEO = "video";
    private const string PARAMETER_AUDIO = "audio";
    private const string PARAMETER_TEXT = "text";
    private const string PARAMETER_IMAGE = "image";
    private const string PARAMETER_MAP = "map";

    #endregion

    public App()
    {
      Startup += Application_Startup;
      Exit += Application_Exit;
      UnhandledException += Application_UnhandledException;

      InitializeComponent();
    }

    #region Lifetime

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      UpdateOOB(); //TODO: run this from background thread, seems to take some time //CALLING THIS FIRST, SINCE THE REST OF THE CODE COULD THROW AN EXCEPTION WHICH WOULD BLOCK UPDATES (AND ALSO TO MAKE USE OF THE TIME TO SET UP THE APP, SINCE UPDATING OCCURS IN THE BACKGROUND)
    
      FloatingWindowHost host = new FloatingWindowHost(); //don't use FloatingWindowHostZUI here
      ActivityWindow activityWindow = CreateActivityWindow(host);

      if (IsRunningOutOfBrowser) //Must not set this for in-browser apps, else warning that this will be ignored will be shown at runtime

        App.Current.MainWindow.Closing += (s, ev) => //due to a bug in Silverlight, this has to be attached BEFORE setting the App's RootVisual
        {
          if (activityWindow.View.WarnOnClosing)
            if (MessageBox.Show("Do you want to exit " + ProductName + "?", "Confirmation", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
              ev.Cancel = true;
            else //proceed with app closing
              activityWindow.View.WarnOnClosing = false; //disable warning before proceeding with close (not really needed, since the ActivityWindow checks if it's TopLevel or not)
        }; //TODO: add Closing (with cancel) event handler) in webpage script too for when running in-browser


      host.Rendered += (s, ev) =>
      {
        host.IsBottomBarVisible = false; //hide outer container's bottom bar, only want to show the one of the ActivityContainer that the ActivityWindow hosts
        activityWindow.Width = host.ActualWidth;
        activityWindow.Height = host.ActualHeight;
 
        if (IsRunningOutOfBrowser)
          activityWindow.ShowLoadURLDialog();
        else
          if (!ParseUrlParameters(activityWindow)) 
            activityWindow.ShowLoadURLDialog();
      };

      RootVisual = host;

      if (!IsRunningOutOfBrowser)
        HtmlPage.RegisterScriptableObject("activity", activityWindow); //NOTE: must do this only after setting RootVisual
      
      //MessageBox.Show("ClipFlair loaded"); //uncomment this to test the loading indicator
    }

    private ActivityWindow CreateActivityWindow(FloatingWindowHost host)
    {
      ActivityWindow w = new ActivityWindow();
      w.IsTopLevel = true; //hide backpanel properties not relevant when not being a child window
      host.Add(w);

      w.Position = new Point(0, 0);
      w.MaximizeWindow(); //TODO: seems MaximizeAction is broken, need to check original FloatingWindow control (Silverlight version)

      w.ShowMaximizeButton = false;
      w.ShowMinimizeButton = false;
      w.ShowCloseButton = false;

      return w;
    }

    private bool ParseUrlParameters(ActivityWindow activityWindow)
    {
      IDictionary<string, string> queryString = HtmlPage.Document.QueryString;
      bool foundParam = false;
      if (queryString.ContainsKey(PARAMETER_ACTIVITY))
      {
        activityWindow.LoadOptions(new Uri(new Uri(CLIPFLAIR_GALLERY_ACTIVITY), queryString[PARAMETER_ACTIVITY]));
        foundParam = true;
      }
      if (queryString.ContainsKey(PARAMETER_MEDIA))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        MediaPlayerWindow w = new MediaPlayerWindow();
        activityWindow.activityContainer.AddWindowInViewCenter(w);
        w.MediaPlayerView.Source = new Uri(new Uri(CLIPFLAIR_GALLERY_VIDEO), queryString[PARAMETER_MEDIA]);
        foundParam = true;
      }
      if (queryString.ContainsKey(PARAMETER_VIDEO))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        MediaPlayerWindow w = new MediaPlayerWindow();
        activityWindow.activityContainer.AddWindowInViewCenter(w);
        w.MediaPlayerView.Source = new Uri(new Uri(CLIPFLAIR_GALLERY_VIDEO), queryString[PARAMETER_VIDEO]);
        foundParam = true;
      }
      if (queryString.ContainsKey(PARAMETER_AUDIO))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        MediaPlayerWindow w = new MediaPlayerWindow();
        activityWindow.activityContainer.AddWindowInViewCenter(w);
        w.MediaPlayerView.VideoVisible = false;
        w.MediaPlayerView.Source = new Uri(new Uri(CLIPFLAIR_GALLERY_AUDIO), queryString[PARAMETER_AUDIO]);
        foundParam = true;
      }
      if (queryString.ContainsKey(PARAMETER_IMAGE))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        ImageWindow w = new ImageWindow();
        activityWindow.activityContainer.AddWindowInViewCenter(w);
        w.ImageView.Source = new Uri(new Uri(CLIPFLAIR_GALLERY_IMAGE), queryString[PARAMETER_IMAGE]);
        foundParam = true;
      }

      //TODO: add ...PARAMETER_CAPTIONS, PARAMETER_COMPONENT, TEXT, MAP etc.
      
      return foundParam;
    }

    private void WaitTillNotBusy(ActivityWindow w)
    {
      while (w.View.Busy) 
        Thread.Sleep(100); //wait for 1/10 sec
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
      //if (System.Diagnostics.Debugger.IsAttached) return; //THIS DOESN'T SEEM TO WORK, FREEZES
      
        // NOTE: This will allow the application to continue running after an exception has been thrown
        // but not handled. 
        // For production applications this error handling should be replaced with something that will 
        // report the error to the website and stop the application.

        e.Handled = true;

        //try to show message on UI thread
        Dispatcher dispatcher = Deployment.Current.Dispatcher;
        if (dispatcher != null) 
          dispatcher.BeginInvoke(
            () => 
                MessageBox.Show("Unexpected error: " + e.ExceptionObject.Message + "\n" + e.ExceptionObject.StackTrace) //TODO: find parent window?
              //ReportErrorToDOM(e) //don't use this, uses Browser's error facility (wouldn't work in OOB)
          );
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
