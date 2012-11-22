//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainerWindow.xaml.cs
//Version: 20121122

using ClipFlair.Windows.Views;

using Ionic.Zip;
using SilverFlow.Controls;

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace ClipFlair.Windows
{
  public partial class ActivityContainerWindow : BaseWindow
  {
   
    public ActivityContainerWindow()
    {
      InitializeComponent();
      View = activity.View; //set window's View to be the same as the nested activity's View
      InitializeView();
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

      activity.Windows.RemoveAll(); //TODO: do not use "Clear", doesn't work
      foreach (ZipEntry childZip in zip.SelectEntries("*.clipflair.zip", zipFolder))
        using (Stream stream = childZip.OpenReader())
          using (MemoryStream memStream = new MemoryStream()) //TODO: research this - can't use activity.Windows.Add(LoadWindow(stream), seems DotNetZip fails to open a .zip from a stream inside another .zip
          {
            stream.CopyTo(memStream);
            stream.Flush();
            memStream.Position = 0;
            BaseWindow w = LoadWindow(memStream);
            activity.Windows.Add(w);
            w.Show();

            //TODO: temp, should load/save Tags (maybe at view), not use hardcoded ones
            if (w is ActivityContainerWindow) w.Tag = "Activity";
            else if (w is MediaPlayerWindow) w.Tag = "Media";
            else if (w is CaptionsGridWindow) w.Tag = "Captions";
            else if (w is TextEditorWindow) w.Tag = "Text";
            else if (w is ImageWindow) w.Tag = "Image";
          }

      activity.BindWindows(); //TODO: temp, should load/save bindings, not use hardcoded ones

      if (IsTopLevel)
      {
        MoveEnabled = false;
        ResizeEnabled = false;
        ScaleEnabled = false;

        FrameworkElement host = (FrameworkElement)VisualTreeHelper.GetRoot(this);
        Width = host.ActualWidth;
        Height = host.ActualHeight;
      } //TODO: most probably needed cause Width/Height View settings of ActivityContainer when top window aren't set correctly (App.xaml has event that resizes window to get container size, but may occur without view finding out?)
    }

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);
      foreach (BaseWindow window in activity.Windows)
        SaveWindow(zip, zipFolder, window);
    }

    private static void SaveWindow(ZipFile zip, string zipFolder, BaseWindow window)
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