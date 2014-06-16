//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityWindow.xaml.cs
//Version: 20140615

using ClipFlair.UI.Dialogs;
using ClipFlair.Windows;
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
  public partial class ActivityWindow : BaseWindow
  {

    #region --- Constants ---

    private const string DEFAULT_ACTIVITY = "http://gallery.clipflair.net/activity/Tutorial.clipflair"; //TODO: change this with a list of entries loaded from the web (and have a cached one in app config for offline scenaria or fetched/cached during oob install) //MAYBE COULD HAVE A DEFAULT SMALL ONE IN THE XAP
    private const string CLIPFLAIR_FEEDBACK = "http://bit.ly/YGBPbD"; //http://social.clipflair.net/MonoX/Pages/SocialNetworking/Discussion/dboard/O6PslGovYUqUraEHARCOVw/

    #endregion

    #region --- Constructor ---

    public ActivityWindow()
    {
      InitializeComponent();

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

    #region --- StartDialog ---

    public void ShowStartDialog()
    {
      Container.ShowStartDialog();
    }

    #endregion

    #region --- Load / Save Options ---

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
               "|" + CaptionsGridWindowFactory.LOAD_FILTER +
               "|" + ImageWindowFactory.LOAD_FILTER +
               "|" + TextEditorWindowFactory.LOAD_FILTER //placing this last, since it has an "All Files (*.*)" at its end
               ;
      }
    }

    public override void LoadContent(Stream stream, string filename)
    {
      LoadOptions(stream); //ActivityWindow's "content" is a .clipflair/.clipflair.zip file
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

        if (insert && !childrenOnly) //load component as child window
          activity.AddWindow(LoadWindow(zip, zipFolder)); //...loading as a child window saved state instead //TODO: remove THIS NOTE when fixed: don't call Windows.Add, won't do bindings currently
        
        else //load inner archives as child windows //this occurs when insert=false or when childrenOnly=true
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

    #endregion

    #region --- Methods ---

    public void CheckZoomToFit()
    {
      activity.CheckZoomToFit();
    }

    public void SendFeedback()
    {
      BrowserDialog.Show(new Uri(CLIPFLAIR_FEEDBACK));
    }

    #endregion

    #region --- Events ---

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

    private void BaseWindow_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      CheckZoomToFit();
    }
    
    #endregion

  }

}