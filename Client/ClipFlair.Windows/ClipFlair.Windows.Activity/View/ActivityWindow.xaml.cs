﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityWindow.xaml.cs
//Version: 20140921

using ClipFlair.UI.Dialogs;
using ClipFlair.Windows;
using ClipFlair.Windows.Activity;
using ClipFlair.Windows.Captions;
using ClipFlair.Windows.Image;
using ClipFlair.Windows.Media;
using ClipFlair.Windows.Text;
using ClipFlair.Windows.Views;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using System.Windows.Media;
using Utils.Extensions;

[assembly: TypeForwardedTo(typeof(MediaPlayerView))]
[assembly: TypeForwardedTo(typeof(CaptionsGridView))]
[assembly: TypeForwardedTo(typeof(TextEditorView))]
[assembly: TypeForwardedTo(typeof(TextEditorView2))]
[assembly: TypeForwardedTo(typeof(ImageView))]
[assembly: TypeForwardedTo(typeof(MapView))]
[assembly: TypeForwardedTo(typeof(NewsView))]
[assembly: TypeForwardedTo(typeof(GalleryView))]
//[assembly: TypeForwardedTo(typeof(ActivityView))] //can't forward to a type from this assembly (don't need to anyway)

namespace ClipFlair.Windows
{

  [ScriptableType]
  public partial class ActivityWindow : BaseWindow, IClipFlairStartActions
  {

    #region --- Constants ---

    public const string URL_PROJECT_HOME = "http://ClipFlair.net";
    public const string URL_HELP_TUTORIAL_ACTIVITY = "http://gallery.clipflair.net/activity/Tutorial.clipflair"; //TODO: use in HelpTutorialActivity method
    public const string URL_HELP_TUTORIAL_VIDEOS = "http://social.clipflair.net/Pages/Tutorials.aspx";
    public const string URL_HELP_MANUAL = "http://social.clipflair.net/help/manual.aspx";
    public const string URL_HELP_FAQ = "http://social.clipflair.net/help/faq.aspx";
    public const string URL_HELP_CONTACT = "http://social.clipflair.net/MonoX/Pages/Contact.aspx";
    public const string URL_SOCIAL = "http://social.clipflair.net";
    public const string URL_NEWS = "http://social.clipflair.net/Blog.aspx?MonoXRssFeed=ClipFlair-All-blog-posts";

    public const string URL_DEFAULT_ACTIVITY = ""; //TODO: use at param passed to Open Activity from URL Dialog
    public const string URL_DEFAULT_VIDEO = "http://video3.smoothhd.com.edgesuite.net/ondemand/Big%20Buck%20Bunny%20Adaptive.ism/Manifest"; //MPEG-DASH sample: http://wams.edgesuite.net/media/MPTExpressionData02/BigBuckBunny_1080p24_IYUV_2ch.ism/manifest(format=mpd-time-csf)
    public const string URL_DEFAULT_IMAGE = "http://gallery.clipflair.net/image/clipflair-logo.jpg";
    public const string URL_GALLERY_PREFIX = "http://gallery.clipflair.net/collection/";

    private const string DEFAULT_ACTIVITY = URL_HELP_TUTORIAL_ACTIVITY; //TODO: change this with a list of entries loaded from the web (and have a cached one in app config for offline scenaria or fetched/cached during oob install) //MAYBE COULD HAVE A DEFAULT SMALL ONE IN THE XAP
    //private const string CLIPFLAIR_FEEDBACK = "http://social.clipflair.net/MonoX/Pages/SocialNetworking/Discussion/dboard/O6PslGovYUqUraEHARCOVw/"; //http://bit.ly/YGBPbD

    #endregion

    #region --- Initialization ---

    public ActivityWindow()
    {
      InitializeComponent();

      activity.ActivityWindow = this;
      mefContainer = activity.mefContainer;

      OptionsLoadSave.LoadURLTooltip = "Load activity from URL"; //TODO: localize
      OptionsLoadSave.LoadTooltip = "Load activity from file";
      OptionsLoadSave.SaveTooltip = "Save activity to file";
      
      View = activity.View; //set window's View to be the same as the nested activity's View

      defaultLoadURL = DEFAULT_ACTIVITY;
      IsTopLevel = false; //must call this property setter
      Container.zuiContainer.CloseWindowsOnApplicationExit = false; //do not allow this, only using for parent FloatingWindowHost of the toplevel ActivityWindow (setting at App.xaml.cs)
    }

    #endregion

    #region --- View ---

    public override IView View
    {
      get { return (IActivity)base.View; } //delegating to parent property
      set
      {
        activity.View = (ActivityView)value; //try to cast to ActivityView before setting base.View - do not allow setting other IView objects so that we can detect and insert as children saved states of other components
        base.View = value;
      }
    }

    public IActivity ActivityView
    {
      get { return (IActivity)View; }
      set { View = value; }
    }

    public ActivityContainer Container
    {
      get { return activity; }
    }

    #endregion

    #region --- OOB install ---

    public bool IsShowingInstall
    {
      get
      {
        Application app = Application.Current;
        return IsTopLevel && !app.IsRunningOutOfBrowser && (app.InstallState != InstallState.Installed);
      }
    }

    private void lblInstall_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      Application app = Application.Current;
      if ((app.InstallState != InstallState.Installed) &&
          (app.InstallState != InstallState.Installing))
        try
        {
          app.Install();
        }
        catch (InvalidOperationException)
        {
          MessageDialog.Show("Error", "ClipFlair Studio is already installed for Out-Of-Browser (OOB) use");
        }
    }

    #endregion

    #region --- Load / Save ---

    public override void ShowLoadURLDialog(string loadItemTitle = "ClipFlair Activity")
    {
      base.ShowLoadURLDialog(loadItemTitle);
    }

    public override string LoadFilter
    {
      get
      { 
        return base.LoadFilter + 
               "|" + MediaPlayerWindowFactory.LOAD_FILTER + 
               "|" + CaptionsWindowFactory.LOAD_FILTER +
               "|" + ImageWindowFactory.LOAD_FILTER +
               "|" + TextEditorWindowFactory.LOAD_FILTER //placing this last, since it has an "All Files (*.*)" at its end
               ;
      }
    }

    public override void LoadContent(Stream stream, string filename)
    {
      LoadOptions(stream); //ActivityWindow's "content" is a .clipflair/.clipflair.zip file
    }

    public override void LoadOptions(Stream stream, string zipFolder = "") //doesn't close stream
    {
      //bool oldIconbarAutohide = ActivityView.IconbarAutohide;
      bool oldIconbarVisible = ActivityView.IconbarVisible;
      bool oldToolbarVisible = ActivityView.ToolbarVisible;
      try
      {
        base.LoadOptions(stream, zipFolder);
      }
      finally
      {
        //ActivityView.IcobarAutohide &= oldIconbarAutohide; //set the iconbar to autohide only if it was already autohiding and the loaded state specifies it as autohiding
        ActivityView.IconbarVisible |= oldIconbarVisible; //show the iconbar if it was already visible or the loaded state specifies it as visible
        ActivityView.ToolbarVisible &= oldToolbarVisible; //show the activity toolbar only if it was already visible and the loaded state specifies it as visible too
      }
    }

    public override void LoadOptions(IEnumerable<FileInfo> files) //descendents that can handle multiple files should override this
    {
      if (files != null)
      {
        if (files.Count() > 1) //if trying to load more than one files, always add as children (do not replace current activity state) 
          loadModifiers = loadModifiers | ModifierKeys.Control; //turn on the CTRL flag

        foreach (FileInfo f in files)
          LoadOptions(f);
      }
    }

    public override void LoadOptions(FileInfo f){ 
      if (!f.Name.EndsWith(new string[]{ CLIPFLAIR_EXTENSION, CLIPFLAIR_ZIP_EXTENSION }))
      {
        IFileWindowFactory windowFactory = GetFileWindowFactory(f.Extension.ToUpper()) ?? new TextEditorWindowFactory();

        BaseWindow window = windowFactory.CreateWindow();
        activity.AddWindow(window); //some components may require to be added to a parent container first, then add content to them
        window.LoadContent(f.OpenRead(), f.Name); //not closing the stream (components like MediaPlayerWindow require it open) //TODO: make sure those components close the streams when not using them anymore
      }
      else
        base.LoadOptions(f);
    }

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      bool insert = ((loadModifiers & ModifierKeys.Control) > 0); //if CTRL was pressed at start of loading action, then insert content instead without removing current state & content
      bool childrenOnly = ((loadModifiers & ModifierKeys.Shift) > 0); //if SHIFT was pressed, insert children of each component only (if components don't have such it won't load anything)

      if (!insert && !childrenOnly) //if inserting or loading only children of activity, don't load its options
        try
        {
          base.LoadOptions(zip, zipFolder); //this will set the View of the ActivityWindow, which will set the view of the ActivityContainer control too
        }
        catch //couldn't load saved options (*.options.xml) as ActivityView...
        {
          insert = true; //...so assuming this is saved state of other control and insert as child
        }

      View.Busy = true; //set busy flag again since base.LoadOptions call will set it to false after loading state
      try
      {
        if (!insert) //remove current windows only if not an insert action
          activity.RemoveWindows(ignoreChildrenWarnOnClosing:true); //don't call Windows.Clear(), won't work //TODO: remove this note when fixed: don't call Windows.RemoveAll(), won't do bindings currently

        if (insert && !childrenOnly)
          
          //load component as child window
          activity.AddWindow(LoadWindow(zip, zipFolder)); //...loading as a child window saved state instead //TODO: remove THIS NOTE when fixed: don't call Windows.Add, won't do bindings currently
        
        else //occurs when (insert==false || childrenOnly==true)
          
          //load inner archives as child windows 
          foreach (string ext in ActivityWindowFactory.SUPPORTED_FILE_EXTENSIONS)
           foreach (ZipEntry zipEntry in zip.SelectEntries("*" + ext, zipFolder))       
             activity.AddWindow(LoadWindow(zipEntry), bringToFront:false); //TODO: remove THIS NOTE when fixed: don't call Windows.Add, won't do bindings currently
      }
      finally
      {
        View.Busy = false;
      }

      if (IsTopLevel) //if this activity window is the outer container
      {
        MoveEnabled = false; //reapply TopLevel settings after load since default values will have been set to them
        ResizeEnabled = false;
        ScaleEnabled = false;

        FrameworkElement host = (FrameworkElement)VisualTreeHelper.GetRoot(this);
        Width = host.ActualWidth;
        Height = host.ActualHeight;

        /*
        Position = new Point(0, 0);
        Scale = 1.0;
        Opacity = 1.0;
        ActivityView.ViewPosition = new Point(0, 0);
        ActivityView.ViewWidth = Width;
        ActivityView.ViewHeight = Height;
        */ 
      } //TODO: most probably needed cause Width/Height View settings of ActivityContainer (when top window) aren't set correctly (App.xaml has event that resizes window to get container size, but may occur without view finding out?)
      else
      {
        MoveEnabled = true; //reapply activity default settings after load since saved values will have been set to them (we usually save toplevel activities so they will have those values set to false)
        ResizeEnabled = true;
        ScaleEnabled = true;
      }

      CheckZoomToFit();
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);
      foreach (BaseWindow window in activity.Windows)
        SaveWindow(window, zip, zipFolder);
    }

    private void activity_LoadURLClick(object sender, RoutedEventArgs e) //assinged to ActivityContainer object's respective event in the XAML
    {
      ShowLoadURLDialog("ClipFlair Activity"); //there is an issue with overriden methods using the parent method initializer for default parameters, so have to explicitly pass the parameter here
    }

    private void activity_LoadClick(object sender, RoutedEventArgs e) //assinged to ActivityContainer object's respective event in the XAML
    {
      ShowLoadDialog();
    }

    private void activity_SaveClick(object sender, RoutedEventArgs e) //assinged to ActivityContainer object's respective event in the XAML
    {
      ShowSaveDialog();
    }

    #endregion

    #region --- ZoomToFit ---

    public void CheckZoomToFit()
    {
      activity.CheckZoomToFit();
    }

    private void BaseWindow_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      CheckZoomToFit();
    }

    #endregion
    
    #region --- Closing ---

    protected override void OnClosing(CancelEventArgs e)
    {
      if (!IsTopLevel //for top level window showing closing warning (with option to cancel closing) via webpage JavaScript event handler or via App class event handler at OOB mode
           && View.WarnOnClosing) //Containers should set WarnOnClosing=false to each of their children if they warn user themselves and users select to proceed with closing
        e.Cancel = (MessageBox.Show("Are you sure you want to close this window?", "Confirmation", MessageBoxButton.OKCancel) != MessageBoxResult.OK);

      if (!e.Cancel)
      {
        activity.DisableChildrenWarnOnClosing(); //disable warning at each child window before proceeding to close the activity
        BaseOnClosing(e); //this will fire "Closing" event handler of FloatingWindow (2nd level ancestor)
      }
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e); //this will fire "Closed" event handler

      View = null; //clearing the view to release property change event handler //must not do this at class destructor, else may get cross-thread-access exceptions //this also clears the View for nested ActivityContainer
    }

    #endregion
    
    /* //not used
    public void SendFeedback()
    {
      BrowserDialog.Show(new Uri(CLIPFLAIR_FEEDBACK));
    }
    */

    #region --- Start Dialog ---

    public void ShowStartDialog()
    {
      StartDialog.Show(this);
    }

    #region --- IClipFlairStartActions ---

    //Project Home//

    public bool ProjectHome()
    {
      return NavigateTo(URL_PROJECT_HOME);
    }

    //NewActivity//

    public bool NewActivity()
    {
      Container.RemoveWindows(ignoreChildrenWarnOnClosing: true);
      Container.View = new ActivityView(); //must set Container.View, not this.View, else the activity doesn't show in full window size
      return true;
    }

    //OpenActivity//

    public bool OpenActivityFile()
    {
      return ShowLoadDialog();
    }

    public bool OpenActivityURL()
    {
      ShowLoadURLDialog();
      return true; //TODO: return false if user cancelled (need to make ShowLoadURLDialog wait for OnClosing before returning and check DialogResult, then return false if InputDialog was cancelled)
    }

    public bool OpenActivityGallery()
    {
      GalleryWindow w = Container.AddGallery("activities", "Activity Gallery"); //TODO: use PivotDialog instead, then load activity
      w.ResizeToView();
      return true; //TODO: return false if user cancelled PivotDialog
    }

    //OpenVideo//

    public bool OpenVideoFile()
    {
      MediaPlayerWindow win = Container.AddClip();
      return win.OpenLocalFile();
    }

    public bool OpenVideoURL()
    {
      MediaPlayerWindow win = Container.AddClip();
      win.Flipped = true; //flip for user to fill-in Media URL field
      return true;
    }

    public bool OpenVideoGallery()
    {
      GalleryWindow w = Container.AddGallery("video", "Video Gallery"); //TODO: use PivotDialog instead, invoked by talking to newly added video component
      w.ResizeToView();
      return true; //TODO: return false if user cancelled PivotDialog
    }

    //OpenImage//

    public bool OpenImageFile()
    {
      ImageWindow win = Container.AddImage();
      return win.OpenLocalFile();
    }

    public bool OpenImageURL()
    {
      ImageWindow win = Container.AddImage();
      win.Flipped = true; //flip for user to fill-in Image URL field
      return true;
    }

    public bool OpenImageGallery()
    {
      GalleryWindow w = Container.AddGallery("images", "Image Gallery"); //TODO: use PivotDialog instead to get URL, invoked by talking to newly added image component
      w.ResizeToView();
      return true; //TODO: return false if user cancelled PivotDialog
    }

    //Help//

    public bool HelpTutorialActivity()
    {
      LoadOptions(new Uri(URL_HELP_TUTORIAL_ACTIVITY, UriKind.Absolute));
      return true;
    }

    public bool HelpTutorialVideos()
    {
      return NavigateTo(URL_HELP_TUTORIAL_VIDEOS);
    }

    public bool HelpManual()
    {
      return NavigateTo(URL_HELP_MANUAL);
    }

    public bool HelpFAQ()
    {
      return NavigateTo(URL_HELP_FAQ);
    }

    public bool HelpContact()
    {
      return NavigateTo(URL_HELP_CONTACT);
    }

    //Social//

    public bool Social()
    {
      return NavigateTo(URL_SOCIAL);
    }

    private bool NavigateTo(string uri)
    {
      try
      {
        BrowserDialog.Show(new Uri(uri)); //TODO: add WebBrowserWindow and WebBrowserDialog for OOP version and use that to show URLs
        return true;
      }
      catch
      {
        return false;
      }
    }

    #endregion

    #endregion

  }

}