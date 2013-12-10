//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityWindow.xaml.cs
//Version: 20131206

using Utils.Extensions;
using Utils.Bindings;
using ClipFlair.Windows.Views;

using Ionic.Zip;

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Media;

[assembly: TypeForwardedToAttribute(typeof(MediaPlayerView))]
[assembly: TypeForwardedToAttribute(typeof(CaptionsGridView))]
[assembly: TypeForwardedToAttribute(typeof(TextEditorView))]
[assembly: TypeForwardedToAttribute(typeof(TextEditorView2))]
[assembly: TypeForwardedToAttribute(typeof(ImageView))]
[assembly: TypeForwardedToAttribute(typeof(MapView))]
[assembly: TypeForwardedToAttribute(typeof(GalleryView))]
//[assembly: TypeForwardedToAttribute(typeof(ActivityView))] //can't forward to a type from this assembly (don't need to anyway)

namespace ClipFlair.Windows
{

  [ScriptableType]
  public partial class ActivityWindow : BaseWindow
  {

    #region Constants

    private const string DEFAULT_ACTIVITY = "http://gallery.clipflair.net/activity/Tutorial.clipflair"; //TODO: change this with a list of entries loaded from the web (and have a cached one in app config for offline scenaria or fetched/cached during oob install) //MAYBE COULD HAVE A DEFAULT SMALL ONE IN THE XAP
    private const string CLIPFLAIR_FEEDBACK = "http://bit.ly/YGBPbD"; //http://social.clipflair.net/MonoX/Pages/SocialNetworking/Discussion/dboard/O6PslGovYUqUraEHARCOVw/

    #endregion

    #region Constructor

    public ActivityWindow()
    {
      InitializeComponent();

      OptionsLoadSave.LoadURLTooltip = "Load activity from URL"; //TODO: localize
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

    #endregion

    #region View

    protected bool loadingChildWindow = false; //loading a child window instead of an activity saved state

    public override IView View
    {
      get { return (IActivity)base.View; } //delegating to parent property
      set
      {
        IActivity activityView = value as IActivity;

        loadingChildWindow = (activityView == null);
        if (loadingChildWindow) //special handling to allow activity to load a single component's state, by clearing the activity and then adding it as a child (see LoadOptions too)
          activityView = new ActivityView();
 
        base.View = activityView;
        activity.View = activityView; //set the view of the activity control too
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

    #region Load / Save Options

    public override void ShowLoadURLDialog(string loadItemTitle = "ClipFlair Activity")
    {
      base.ShowLoadURLDialog(loadItemTitle);
    }
    
    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder); //this will set the View of the ActivityWindow, which will set the view of the ActivityContainer control too

      View.Busy = true; //set busy flag again since base.LoadOptions call will set it to false after loading state
      try
      {
        activity.RemoveWindows(ignoreChildrenWarnOnClosing:true); //don't call Windows.Clear(), won't work //TODO: remove this note when fixed: don't call Windows.RemoveAll(), won't do bindings currently

        if (loadingChildWindow) //check if this is not a saved activity state (as marked by View property's set accessor)...
        {
          loadingChildWindow = false; //restore to default value
          activity.AddWindow(LoadWindow(zip, zipFolder)); //...loading as a child window saved state instead //TODO: remove THIS NOTE when fixed: don't call Windows.Add, won't do bindings currently
        }
        else
        { //TODO: maybe can use ";" to pass multiple search items instead of using two "foreach" loops
          foreach (ZipEntry childZip in zip.SelectEntries("*" + BaseWindow.CLIPFLAIR_ZIP_EXTENSION, zipFolder))
            activity.AddWindow(LoadWindow(childZip), bringToFront:false); //TODO: remove THIS NOTE when fixed: don't call Windows.Add, won't do bindings currently

          foreach (ZipEntry childZip in zip.SelectEntries("*" + BaseWindow.CLIPFLAIR_EXTENSION, zipFolder)) //in case somebody has placed .clipflair files inside a ClipFlair archive (when saving those contain .clipflair.zip files for each component)
            activity.AddWindow(LoadWindow(childZip), bringToFront: false); //TODO: remove THIS NOTE when fixed: don't call Windows.Add, won't do bindings currently
        }
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

      CheckZoomToFit();
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);
      foreach (BaseWindow window in activity.Windows)
        SaveWindow(window, zip, zipFolder);
    }

    #endregion

    #region Methods

    public void CheckZoomToFit()
    {
      activity.CheckZoomToFit();
    }

    public void SendFeedback()
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

    #endregion

    #region Events

    private void lblBeta_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      SendFeedback();
    }

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

  }

}