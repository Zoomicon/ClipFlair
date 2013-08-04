//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App.xaml.cs
//Version: 20130726

using ClipFlair.UI.Dialogs;
using ClipFlair.Windows;

using SilverFlow.Controls;
using Utils.Extensions;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Interop;
using System.Windows.Threading;

namespace ClipFlair
{

  public partial class App : Application
  {
    
    #region Constants

    public const string CLIPFLAIR_GALLERY_ACTIVITY = "http://gallery.clipflair.net/activity/";
    public const string CLIPFLAIR_GALLERY_VIDEO = "http://gallery.clipflair.net/video/";
    public const string CLIPFLAIR_GALLERY_AUDIO = "http://gallery.clipflair.net/audio/";
    public const string CLIPFLAIR_GALLERY_TEXT = "http://gallery.clipflair.net/text/";
    public const string CLIPFLAIR_GALLERY_IMAGE = "http://gallery.clipflair.net/image/";
    public const string CLIPFLAIR_GALLERY_MAP = "http://gallery.clipflair.net/map/";
    public const string CLIPFLAIR_GALLERY_COLLECTION = "http://gallery.clipflair.net/collection/";

    private const string SMOOTH_STREAM_EXTENSION_SHORT = ".ism";
    private const string SMOOTH_STREAM_EXTENSION = ".ism/Manifest";
    private const string GALLERY_EXTENSION = ".cxml";

    public const string PARAMETER_NOTOOLBAR = "notoolbar";
    public const string PARAMETER_ACTIVITY = "activity";
    public const string PARAMETER_COMPONENT = "component";
    public const string PARAMETER_MEDIA = "media";
    public const string PARAMETER_VIDEO = "video";
    public const string PARAMETER_AUDIO = "audio";
    public const string PARAMETER_TEXT = "text";
    public const string PARAMETER_IMAGE = "image";
    public const string PARAMETER_MAP = "map";
    public const string PARAMETER_GALLERY = "gallery";
    public const string PARAMETER_COLLECTION = "collection";

    #endregion

    #region --- Fields ---

    SilverlightHost silverlightHost; //The Host object, which represents the host Silverlight plug-in.
    Settings settings; //The Settings object, which represents Web browser settings
    Content content; // The Content object, which represents the plug-in display area.
    IDictionary<string, string> queryString;
 
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

      silverlightHost = Application.Current.Host;
      settings = silverlightHost.Settings;
      content = silverlightHost.Content;

      if (!IsRunningOutOfBrowser)
        queryString = HtmlPage.Document.QueryString;

      //// Read/write properties of the Settings object.
      //settings.EnableFrameRateCounter = true;
      //settings.EnableRedrawRegions = true;
      //settings.MaxFrameRate = 60;

      //// Read-only properties of the Settings object.
      //bool windowless = settings.Windowless;
      //bool htmlAccessEnabled = settings.EnableHTMLAccess;

      //// The read/write IsFullScreen property of the Content object.
      //// See also the Content.FullScreenChanged event.
      //bool isFullScreen = content.IsFullScreen;

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
        }; //TODO: add Closing (with cancel) event handler in webpage script too for when running in-browser


      host.Rendered += (s, ev) =>
      //content.Resized += (s, ev) => //if we use this, should use as a method so that we can remove it after 1st call
      {
        host.IsBottomBarVisible = false; //hide outer container's bottom bar, only want to show the one of the ActivityContainer that the ActivityWindow hosts
        activityWindow.Width = host.ActualWidth;
        activityWindow.Height = host.ActualHeight;

        if (!IsRunningOutOfBrowser)
        {
          HtmlPage.RegisterScriptableObject("activityWindow", activityWindow); //NOTE: must do this only after setting RootVisual (obviously for Rendered to be called this will have occured)

          if (queryString.ContainsKey(PARAMETER_NOTOOLBAR))
            activityWindow.ActivityView.ToolbarVisible = false;
        }

        if (!ParseUrlParameters(activityWindow)) //ParseUrlParameters returns false if IsRunningOutOfBrowser is true
        {
          GalleryWindow w = activityWindow.Container.AddGallery();
          w.Width = activityWindow.Width;
          w.Height = activityWindow.Height - (activityWindow.ActivityView.ToolbarVisible?80:0); //TODO: should change this if/when activity toolbar is made vertical (have option to get ActualWidth/ActualHeight of activity toolbar)
          activityWindow.ShowLoadURLDialog();
        }

      };

      RootVisual = host;
     
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
      if (IsRunningOutOfBrowser)
        return false;

      bool foundParam = false;

      if (queryString.ContainsKey(PARAMETER_ACTIVITY))
      {
        activityWindow.LoadOptions(makeActivityUri(queryString[PARAMETER_ACTIVITY]));
        foundParam = true;
      }

      if (queryString.ContainsKey(PARAMETER_MEDIA))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        MediaPlayerWindow w = new MediaPlayerWindow();
        w.Width = activityWindow.Width;
        w.Height = activityWindow.Height - (activityWindow.ActivityView.ToolbarVisible ? 80 : 0); //TODO: should change this if/when activity toolbar is made vertical (have option to get ActualWidth/ActualHeight of activity toolbar)
        activityWindow.Container.AddWindowInViewCenter(w);
        w.MediaPlayerView.Source = makeClipUri(CLIPFLAIR_GALLERY_VIDEO, queryString[PARAMETER_MEDIA]);
        foundParam = true;
      }

      if (queryString.ContainsKey(PARAMETER_VIDEO))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        MediaPlayerWindow w = new MediaPlayerWindow();
        w.Width = activityWindow.Width;
        w.Height = activityWindow.Height - (activityWindow.ActivityView.ToolbarVisible ? 80 : 0); //TODO: should change this if/when activity toolbar is made vertical (have option to get ActualWidth/ActualHeight of activity toolbar)
        activityWindow.Container.AddWindowInViewCenter(w);
        w.MediaPlayerView.Source = makeClipUri(CLIPFLAIR_GALLERY_VIDEO, queryString[PARAMETER_VIDEO]);
        foundParam = true;
      }

      if (queryString.ContainsKey(PARAMETER_AUDIO))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        MediaPlayerWindow w = new MediaPlayerWindow();
        //w.Width = 800;
        //w.Height = 600;
        activityWindow.Container.AddWindowInViewCenter(w);
        w.MediaPlayerView.VideoVisible = false;
        w.MediaPlayerView.Source = makeClipUri(CLIPFLAIR_GALLERY_AUDIO, queryString[PARAMETER_AUDIO]);
        foundParam = true;
      }

      if (queryString.ContainsKey(PARAMETER_IMAGE))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        ImageWindow w = new ImageWindow();
        w.Width = activityWindow.Width;
        w.Height = activityWindow.Height - (activityWindow.ActivityView.ToolbarVisible ? 80 : 0); //TODO: should change this if/when activity toolbar is made vertical (have option to get ActualWidth/ActualHeight of activity toolbar)
        activityWindow.Container.AddWindowInViewCenter(w);
        w.ImageView.Source = new Uri(new Uri(CLIPFLAIR_GALLERY_IMAGE), queryString[PARAMETER_IMAGE]);
        foundParam = true;
      }

      if (queryString.ContainsKey(PARAMETER_GALLERY))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        GalleryWindow w = new GalleryWindow();
        w.Width = activityWindow.Width;
        w.Height = activityWindow.Height - (activityWindow.ActivityView.ToolbarVisible ? 80 : 0); //TODO: should change this if/when activity toolbar is made vertical (have option to get ActualWidth/ActualHeight of activity toolbar)
        activityWindow.Container.AddWindowInViewCenter(w);
        w.GalleryView.Source = makeGalleryUri(queryString[PARAMETER_GALLERY]);
        foundParam = true;
      }

      if (queryString.ContainsKey(PARAMETER_COLLECTION))
      {
        WaitTillNotBusy(activityWindow); //TODO: doesn't work (should wait for any activity to load first)
        GalleryWindow w = new GalleryWindow();
        w.Width = activityWindow.Width;
        w.Height = activityWindow.Height - (activityWindow.ActivityView.ToolbarVisible ? 80 : 0); //TODO: should change this if/when activity toolbar is made vertical (have option to get ActualWidth/ActualHeight of activity toolbar)
        activityWindow.Container.AddWindowInViewCenter(w);
        w.GalleryView.Source = makeGalleryUri(queryString[PARAMETER_COLLECTION]);
        foundParam = true;
      }

      //TODO: add ...PARAMETER_CAPTIONS, PARAMETER_COMPONENT, TEXT, MAP etc.
      
      return foundParam;
    } //TODO: add CAPTIONS parameter to load .SRT/.TTS and show it

    public static Uri makeActivityUri(string param) //TODO: reuse this code at load-activity-from-url dialog and activity component
    {
      Uri result = new Uri(new Uri(CLIPFLAIR_GALLERY_ACTIVITY), param); //make a full URL if it's a partial one

      string s = result.ToString();
      if (s.StartsWith(CLIPFLAIR_GALLERY_ACTIVITY, StringComparison.OrdinalIgnoreCase) &&
          !s.EndsWith(BaseWindow.CLIPFLAIR_EXTENSION, StringComparison.OrdinalIgnoreCase))
      
        if (s.EndsWith(BaseWindow.CLIPFLAIR_ZIP_EXTENSION, StringComparison.OrdinalIgnoreCase)) //all .clipflair.zip files have been renamed to .clipflair in the gallery
          result = new Uri(s.ReplaceSuffix(BaseWindow.CLIPFLAIR_ZIP_EXTENSION, BaseWindow.CLIPFLAIR_EXTENSION, StringComparison.OrdinalIgnoreCase));
        else
          result = new Uri(s + BaseWindow.CLIPFLAIR_EXTENSION);

      return result;
    }

    public static Uri makeClipUri(string galleryBaseUri, string param) //TODO: reuse this code at Clip component
    {
      if (!param.Contains("/"))
        param = param + "/" + param; //all smooth streams are in a subfolder

      Uri result = new Uri(new Uri(galleryBaseUri), param); //make a full URL if it's a partial one

      string s = result.ToString();
      if (s.StartsWith(galleryBaseUri, StringComparison.OrdinalIgnoreCase) && 
          !s.EndsWith(SMOOTH_STREAM_EXTENSION_SHORT, StringComparison.OrdinalIgnoreCase) &&
          !s.EndsWith(SMOOTH_STREAM_EXTENSION, StringComparison.OrdinalIgnoreCase) )
        result = new Uri(s + SMOOTH_STREAM_EXTENSION);

      return result;
    }
    
    public static Uri makeGalleryUri(string param) //TODO: reuse this code at Gallery component
    {
      Uri result = new Uri(new Uri(CLIPFLAIR_GALLERY_COLLECTION), param); //make a full URL if it's a partial one

      string s = result.ToString();
      if (s.StartsWith(CLIPFLAIR_GALLERY_COLLECTION, StringComparison.OrdinalIgnoreCase) &&
          !s.EndsWith(GALLERY_EXTENSION, StringComparison.OrdinalIgnoreCase))
        result = new Uri(s + GALLERY_EXTENSION);

      return result;
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
        MessageDialog.Show("", "Update downloaded, will use at next launch");
      else if (e.Error!=null) //error during update process
        ErrorDialog.Show("Update failed", e.Error);
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
            () => ErrorDialog.Show("Unexpected error", e.ExceptionObject)
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
