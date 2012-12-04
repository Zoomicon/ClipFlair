//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityWindow.xaml.cs
//Version: 20121203

using ClipFlair.Windows.Views;
using ClipFlair.Utils.Bindings;

using Ionic.Zip;
using SilverFlow.Controls;

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ClipFlair.Windows
{

  [Export("ClipFlair.Windows.Views.ActivityView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class ActivityWindowFactory : IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new ActivityWindow();
    }
  }

  public partial class ActivityWindow : BaseWindow
  {
   
    public ActivityWindow()
    {
      InitializeComponent();
      View = activity.View; //set window's View to be the same as the nested activity's View
      InitializeView();

      //copy Window Factory objects (initialized in activity using MEF) to static fields so that code in BaseWindow can use them
      ActivityWindowFactory = activity.ActivityWindowFactory;
      MediaPlayerWindowFactory = activity.MediaPlayerWindowFactory;
      CaptionsGridWindowFactory = activity.CaptionsGridWindowFactory;
      TextEditorWindowFactory = activity.TextEditorWindowFactory;
      ImageWindowFactory = activity.ImageWindowFactory;
      MapWindowFactory = activity.MapWindowFactory;
    }

    #region --- Properties ---

    #region View

    public new IActivity View //hiding parent property
    {
      get { return (IActivity)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    #endregion
          
    #endregion

    #region Load / Save Options

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);
      View = activity.View; //set window's View to be the same as the nested activity's View

      activity.RemoveWindows(); //don't call Windows.Clear(), won't work //TODO (remove this note when fixed): don't call Windows.RemoveAll(), won't do bindings currently
      foreach (ZipEntry childZip in zip.SelectEntries("*.clipflair.zip", zipFolder))
        LoadWindow(childZip);

      if (IsTopLevel) //if this activity window is the outer container
      {
        MoveEnabled = false;
        ResizeEnabled = false;
        ScaleEnabled = false;

        FrameworkElement host = (FrameworkElement)VisualTreeHelper.GetRoot(this);
        Width = host.ActualWidth;
        Height = host.ActualHeight;
      } //TODO: most probably needed cause Width/Height View settings of ActivityContainer when top window aren't set correctly (App.xaml has event that resizes window to get container size, but may occur without view finding out?)
    }

    public void LoadWindow(ZipEntry childZip)
    {
      using (Stream stream = childZip.OpenReader())
      using (MemoryStream memStream = new MemoryStream()) //TODO: research this - can't use activity.Windows.Add(LoadWindow(stream), seems DotNetZip fails to open a .zip from a stream inside another .zip
      {
        stream.CopyTo(memStream);
        stream.Flush();
        memStream.Position = 0;
        BaseWindow w = LoadWindow(memStream);
        if (w == null) return;

        //TODO: temp, should load/save Tags (maybe at view), not use hardcoded ones (for now needed for rebinding to container) [MUST DO BEFORE AddWindow]
        if (w is ActivityWindow) w.Tag = "Activity";
        else if (w.SafeIsAssignableTo("ClipFlair.Windows.MediaPlayerWindow")) w.Tag = "Media";
        else if (w.SafeIsAssignableTo("ClipFlair.Windows.CaptionsGridWindow")) w.Tag = "Captions";
        else if (w.SafeIsAssignableTo("ClipFlair.Windows.TextEditorWindow")) w.Tag = "Text";
        else if (w.SafeIsAssignableTo("ClipFlair.Windows.ImageWindow")) w.Tag = "Image";
        else if (w.SafeIsAssignableTo("ClipFlair.Windows.MapWindow")) w.Tag = "Map";

        activity.AddWindow(w); //TODO (remove this note when fixed): don't call Windows.Add, won't do bindings currently
        w.Show();
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
        zipFolder + "/" + title + " - " + Guid.NewGuid() + ".clipflair.zip",
        new WriteDelegate((entryName, stream) => { window.SaveOptions(stream); }) ); //save ZIP file for child window 
    }

    #endregion
        
  }

}