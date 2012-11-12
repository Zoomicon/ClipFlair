//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainerWindow.xaml.cs
//Version: 20121112

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
      View = activity.View;
      InitializeView();
    }

    #region --- Properties ---

    #region View

    public new IActivity View //hiding parent property
    {
      get { return (IActivity)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        //Time = View.Time;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case IActivityProperties.PropertyTime:
            //Time = View.Time;
            break;
          //...
        }
    }

    #endregion

    #region Time

    /// <summary>
    /// Time Dependency Property
    /// </summary>
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register(IActivityProperties.PropertyTime, typeof(TimeSpan), typeof(MediaPlayerWindow),
            new FrameworkPropertyMetadata(IActivityDefaults.DefaultTime, new PropertyChangedCallback(OnTimeChanged)));

    /// <summary>
    /// Gets or sets the Time property.
    /// </summary>
    public TimeSpan Time
    {
      get { return (TimeSpan)GetValue(TimeProperty); }
      set { SetValue(TimeProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Time property.
    /// </summary>
    private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ActivityContainerWindow target = (ActivityContainerWindow)d;
      TimeSpan oldTime = (TimeSpan)e.OldValue;
      TimeSpan newTime = target.Time;
      target.OnTimeChanged(oldTime, newTime);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Time property.
    /// </summary>
    protected virtual void OnTimeChanged(TimeSpan oldTime, TimeSpan newTime)
    {
      View.Time = newTime;
    }

    #endregion
          
    #endregion

    #region Load / Save Options

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);

      activity.Windows.RemoveAll(); //TODO: do not use "Clear", doesn't work
      foreach (ZipEntry childZip in zip.SelectEntries("*.clipflair.zip", zipFolder))
        using (Stream stream = childZip.OpenReader())
          using (MemoryStream memStream = new MemoryStream()) //can't use activity.Windows.Add(LoadWindow(stream), seems DotNetZip fails to open a .zip from a stream inside another .zip
          {
            stream.CopyTo(memStream);
            stream.Flush();
            memStream.Position = 0;
            BaseWindow w = LoadWindow(memStream);
            activity.Windows.Add(w);
            w.Show();
          }

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
      MemoryStream stream = new MemoryStream(); //TODO: not optimal implementation, should try to pipe streams without first saving into memory
      window.SaveOptions(stream); //save ZIP file for child window
      stream.Position = 0;
      string title = ((string)window.Title).TrimStart();
      if (title == "") title = window.GetType().Name;
      zip.AddEntry(zipFolder + "/" + title + " - " + Guid.NewGuid() + ".clipflair.zip", stream); //using TrimStart() to not have filenames start with space chars in case it's an issue with ZIP spec
    }

    #endregion
        
  }

}