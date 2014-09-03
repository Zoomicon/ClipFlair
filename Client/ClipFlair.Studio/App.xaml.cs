//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App.xaml.cs
//Version: 20140903

//#define GALLERY_IN_BACKGROUND

using ClipFlair.Windows;
using SilverFlow.Controls;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Interop;
using Utils.Extensions;

namespace ClipFlair
{

  public partial class App : Application
  {
    
    #region --- Constants ---

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

    public const string PARAMETER_ICONBAR = "iconbar";
    //public const string PARAMETER_LOCKICONBAR = "lockiconbar";
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
      host.CloseWindowsOnApplicationExit = true; //only using for the parent of the toplevel ActivityWindow, else multiple closing warning dialogs can show up

      ActivityWindow activityWindow = CreateActivityWindow(host);

      if (IsRunningOutOfBrowser) //Must not set this for in-browser apps, else warning that this will be ignored will be shown at runtime

        App.Current.MainWindow.Closing += (s, ev) => //due to a bug in Silverlight, this has to be attached BEFORE setting the App's RootVisual
        {
          if (activityWindow.View.WarnOnClosing)
            if (MessageBox.Show("Do you want to exit " + ProductName + "?", "Confirmation", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
              ev.Cancel = true;
              return;
            }
            else //proceed with app closing
              activityWindow.View.WarnOnClosing = false; //disable warning before proceeding with close (not really needed, since the ActivityWindow checks if it's TopLevel or not)
          
          activityWindow.Container.DisableChildrenWarnOnClosing();
          activityWindow.Close();
        };


      host.Rendered += (s, ev) =>
      //content.Resized += (s, ev) => //if we use this, should use as a method so that we can remove it after 1st call
      {
        host.IsBottomBarVisible = false; //hide outer container's bottom bar, only want to show the one of the ActivityContainer that the ActivityWindow hosts
        activityWindow.Width = host.ActualWidth;
        activityWindow.Height = host.ActualHeight;

        if (!IsRunningOutOfBrowser)
        {
          HtmlPage.RegisterScriptableObject("activityWindow", activityWindow); //NOTE: must do this only after setting RootVisual (obviously for Rendered to be called this will have occured)

          if (queryString.ContainsKey(PARAMETER_ICONBAR))
            activityWindow.ActivityView.IconbarVisible = true;
          //if (queryString.ContainsKey(PARAMETER_LOCKICONBAR))
          //  activityWindow.ActivityView.IconbarAutoHide = false;         
          if (queryString.ContainsKey(PARAMETER_NOTOOLBAR))
            activityWindow.ActivityView.ToolbarVisible = false;
        }

        if (!ParseUrlParameters(activityWindow)) //ParseUrlParameters returns false if IsRunningOutOfBrowser is true
        {
          #if GALLERY_IN_BACKGROUND
          GalleryWindow w = activityWindow.Container.AddGallery();
          w.Width = activityWindow.Width;
          w.Height = activityWindow.Height - (activityWindow.ActivityView.ToolbarVisible?80:0); //TODO: should change this if/when activity toolbar is made vertical (have option to get ActualWidth/ActualHeight of activity toolbar)
          #endif

          activityWindow.ShowStartDialog();
        }

      };

      RootVisual = host;
     
      //MessageBox.Show("ClipFlair loaded"); //uncomment this to test the loading indicator
    }

    private void Application_Exit(object sender, EventArgs e)
    {
      //TODO: maybe should store current state at isolated storage and offer to restore next time (unless an activity file/url is given as param to load)
      //not sure if browsers give enough time to the app to save (might save partial/corrupted state to isolated storage), or if all browsers inform the app on closing
    }

    #endregion

    #region URL

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
        w.Width = activityWindow.Width;
        w.Height = 69; //Using small height since there's only audio //NOTE: if SMF skin changes this may not look ok
        activityWindow.Container.AddWindowInViewCenter(w);
        w.MediaPlayerView.VideoVisible = false;
        w.MediaPlayerView.Source = new Uri(new Uri(CLIPFLAIR_GALLERY_AUDIO), queryString[PARAMETER_AUDIO]);
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
          !s.EndsWith(SMOOTH_STREAM_EXTENSION, StringComparison.OrdinalIgnoreCase))
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

    #endregion

    #region Helpers

    private ActivityWindow CreateActivityWindow(FloatingWindowHost host)
    {
      ActivityWindow w = new ActivityWindow();
      w.IsTopLevel = true; //hide backpanel properties not relevant when not being a child window
      host.Add(w);

      w.Position = new Point(0, 0);
      w.MaximizeWindow(); //TODO: seems MaximizeAction is broken, need to check original FloatingWindow control (Silverlight version)

      w.ShowMaximizeRestoreButton = false;
      w.ShowMinimizeButton = false;
      w.ShowCloseButton = false;

      return w;
    }

    private void WaitTillNotBusy(ActivityWindow w)
    {
      while (w.View.Busy)
        Thread.Sleep(100); //wait for 1/10 sec
    }

    #endregion

  }

}
