//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityWindow.xaml.cs
//Version: 20130613

using Utils.Extensions;
using Utils.Bindings;
using ClipFlair.Windows.Views;

using Ionic.Zip;

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Media;

namespace ClipFlair.Windows
{

  [ScriptableType]
  public partial class ActivityWindow : BaseWindow
  {

    #region Constants

    private const string DEFAULT_ACTIVITY = "http://gallery.clipflair.net/activity/Tutorial.clipflair"; //TODO: change this with a list of entries loaded from the web (and have a cached one in app config for offline scenaria or fetched/cached during oob install) //MAYBE COULD HAVE A DEFAULT SMALL ONE IN THE XAP
    private const string CLIPFLAIR_FEEDBACK = "http://bit.ly/YGBPbD"; //http://social.clipflair.net/MonoX/Pages/SocialNetworking/Discussion/dboard/O6PslGovYUqUraEHARCOVw/

    #endregion

    public ActivityWindow()
    {
      InitializeComponent();

      OptionsLoadSave.LoadURLTooltip = "Load activity from URL";
      OptionsLoadSave.LoadTooltip = "Load activity from file";
      OptionsLoadSave.SaveTooltip = "Save activity to file";
      
      View = activity.View; //set window's View to be the same as the nested activity's View

      //copy Window Factory objects (initialized in activity using MEF) to static fields so that code in BaseWindow can use them
      MediaPlayerWindowFactory = activity.MediaPlayerWindowFactory;
      CaptionsGridWindowFactory = activity.CaptionsGridWindowFactory;
      TextEditorWindowFactory = activity.TextEditorWindowFactory;
      ImageWindowFactory = activity.ImageWindowFactory;
      MapWindowFactory = activity.MapWindowFactory;
      GalleryWindowFactory = activity.GalleryWindowFactory;
      ActivityWindowFactory = activity.ActivityWindowFactory;

      defaultLoadURL = DEFAULT_ACTIVITY;

      BindingUtils.RegisterForNotification("Width", this, (d, e) => { CheckZoomToFit(); });
      BindingUtils.RegisterForNotification("Height", this, (d, e) => { CheckZoomToFit(); });
    }

    #region View

    public override IView View
    {
      get { return (IActivity)base.View; } //delegating to parent property
      set
      {
        base.View = value;
        activity.View = (IActivity)value; //set the view of the activity control too
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

    public void CheckZoomToFit()
    {
      activity.CheckZoomToFit();
    }

    #region Load / Save Options

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder); //this will set the View of the ActivityWindow, which will set the view of the ActivityContainer control too
      View.Busy = true; //set busy flag again since the above command will set it to false after loading

      activity.DisableChildrenWarnOnClosing();
      activity.RemoveWindows(); //don't call Windows.Clear(), won't work //TODO (remove this note when fixed): don't call Windows.RemoveAll(), won't do bindings currently

      try
      {
        foreach (ZipEntry childZip in zip.SelectEntries("*" + BaseWindow.CLIPFLAIR_ZIP_EXTENSION, zipFolder))
          LoadWindow(childZip);

        foreach (ZipEntry childZip in zip.SelectEntries("*" + BaseWindow.CLIPFLAIR_EXTENSION, zipFolder)) //in case somebody has placed .clipflair files inside a ClipFlair archive (when saving those contain .clipflair.zip files for each component)
          LoadWindow(childZip);
      }
      finally
      {
        View.Busy = false;
      }

      if (IsTopLevel) //if this activity window is the outer container
      {
        MoveEnabled = false;
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

      CheckZoomToFit();
    }

    public void LoadWindow(ZipEntry childZip)
    {
      using (Stream stream = childZip.OpenReader())
      using (MemoryStream memStream = new MemoryStream()) //TODO: see why this is needed - can't use activity.Windows.Add(LoadWindow(stream), seems DotNetZip fails to open a .zip from a stream inside another .zip
      {
        stream.CopyTo(memStream);
        memStream.Position = 0;
        BaseWindow w = LoadWindow(memStream);
        if (w == null) return;
        activity.AddWindow(w); //TODO (remove this note when fixed): don't call Windows.Add, won't do bindings currently
      }
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);
      foreach (BaseWindow window in activity.Windows)
        SaveWindow(window, zip, zipFolder);
    }

    private static void SaveWindow(BaseWindow window, ZipFile zip, string zipFolder = "")
    {
      string title = ((string)window.Title).TrimStart(); //using TrimStart() to not have filenames start with space chars in case it's an issue with ZIP spec
      if (title == "") title = window.GetType().Name;
      zip.AddEntry(
        zipFolder + "/" + title + " - " + Guid.NewGuid() + BaseWindow.CLIPFLAIR_ZIP_EXTENSION, //using .clipflair.zip extension for nested components' state to ease examining with ZIP archivers
        new WriteDelegate((entryName, stream) => { window.SaveOptions(stream); })); //save ZIP file for child window 
    }

    #endregion

    #region Methods

    public override void ShowLoadURLDialog(string loadItemTitle = "ClipFlair Activity")
    {
      base.ShowLoadURLDialog(loadItemTitle);
    }

    #endregion

    #region Events

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      if (!e.Cancel) //disable warning at each child window before proceeding to close the activity
        activity.DisableChildrenWarnOnClosing();
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e); //this will fire "Closed" event handler

      View = null; //clearing the view to release property change event handler //must not do this at class destructor, else may get cross-thread-access exceptions //this also clears the View for nested ActivityContainer
    }

    private void activity_LoadURLClick(object sender, RoutedEventArgs e)
    {
      ShowLoadURLDialog("ClipFlair Activity"); //there is a bug with overriden methods using the parent method initializer for default parameters, so have to explicitly pass the parameter here
    }

    private void activity_LoadClick(object sender, RoutedEventArgs e)
    {
      ShowLoadDialog();
    }

    private void activity_SaveClick(object sender, RoutedEventArgs e)
    {
      ShowSaveDialog();
    }

    #endregion

    private void lblBeta_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      GetFeedback();
    }

    public void GetFeedback()
    {
      Dispatcher.BeginInvoke(delegate
      {
        try
        {
          new Uri(CLIPFLAIR_FEEDBACK).NavigateTo();
        }
        catch
        {
          MessageBox.Show("For feedback visit " + CLIPFLAIR_FEEDBACK);
        }
      });
    }

  }

}