//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainer.xaml.cs
//Version: 20121111

//TODO: move zoom slider UI to FloatingWindowHostZUI's XAML template

using ClipFlair.Windows.Views;
using ClipFlair.Utils.Bindings;

using ZoomAndPan;
using SilverFlow.Controls;

using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace ClipFlair.Windows
{
  public partial class ActivityContainer : UserControl
  {
    public ActivityContainer()
    {
      View = new ActivityView(); //must set the view first
      InitializeComponent();

      //(assuming: using SilverFlow.Controls)
      //zuiContainer.Add((FloatingWindow)XamlReader.Load("<clipflair:MediaPlayerWindow Width=\"Auto\" Height=\"Auto\" MinWidth=\"100\" MinHeight=\"100\" Title=\"4 Media Player\" IconText=\"1Media Player\" Tag=\"1MediaPlayer\" xmlns:clipflair=\"clr-namespace:ClipFlair;assembly=ClipFlair\"/>"));
      //zuiContainer.Add(new MediaPlayerWindow()).Show();
      //zuiContainer.FloatingWindows.First<FloatingWindow>().Show();

      BindWindows(); //Bind MediaPlayerWindows's Time to CaptionGridWindows's Time programmatically (since using x:Name on the controls doesn't seem to work [components are resolved to null when searched by name - must have to do with the implementation of the Windows property of ZUI container])
      //above command could be avoided if binding to activity view's Time property worked

      BindingUtils.RegisterForNotification("ContentOffsetX", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewPosition = new Point(zuiContainer.ZoomHost.ContentOffsetX, zuiContainer.ZoomHost.ContentOffsetY); } });
      BindingUtils.RegisterForNotification("ContentOffsetX", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewPosition = new Point(zuiContainer.ZoomHost.ContentOffsetX, zuiContainer.ZoomHost.ContentOffsetY); } });
      BindingUtils.RegisterForNotification("ContentViewportWidth", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewWidth = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("ContentViewportHeight", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewHeight = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("ContentScale", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ContentZoom = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("ContentScalable", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ContentZoomable = (bool)e.NewValue; } });
      BindingUtils.RegisterForNotification("WindowsConfigurable", zuiContainer, (d, e) => { if (View != null) { View.ContentPartsConfigurable = (bool)e.NewValue; } });
    }

    private void BindWindows()
    {
      //Two-way bind MediaPlayerWindow.Time to CaptionsGridWindow.Time
      //BindProperties(FindWindow("MediaPlayer"), "Time", FindWindow("Captions"), CaptionsGridWindow.TimeProperty, BindingMode.TwoWay); //not using, binding to container's Time instead for now

      //Two-way bind MediaPlayerWindow.Time to ActivityContainer.View.Time via inherited DataContext
      BindingUtils.BindProperties(View, "Time", FindWindow("MediaPlayer"), MediaPlayerWindow.TimeProperty, BindingMode.TwoWay);
      //Two-way bind CaptionsGridWindow.Time to ActivityContainer.View.Time via inherited DataContext
      BindingUtils.BindProperties(View, "Time", FindWindow("Captions"), CaptionsGridWindow.TimeProperty, BindingMode.TwoWay);

      //Two-Way bind MediaPlayerWindow.Captions to CaptionsGridWindow.Captions
      BindingUtils.BindProperties(FindWindow("Captions"), "Captions", FindWindow("MediaPlayer"), MediaPlayerWindow.CaptionsProperty, BindingMode.TwoWay);
    }

    public BaseWindow FindWindow(string tag) //need this since floating windows are not added in the XAML visual tree by the FloatingWindowHostZUI.Windows property (maybe should have FloatingWindowHostZUI inherit 
    {
      foreach (BaseWindow w in zuiContainer.Windows)
        if (tag == (string)w.Tag) return w; //must cast to string to compare (else we compare object references, since Tag property is of type object, not string)
      return null;
    }

    public FloatingWindowCollection Windows
    {
      get { return zuiContainer.Windows; }
    }

    #region View

    public ActivityView View
    {
      get { return (ActivityView)DataContext; }
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          ((INotifyPropertyChanged)DataContext).PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged);
        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
        //set the new view (must do last)
        DataContext = value;
      }
    }

    protected void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Source = View.Source;
        Time = View.Time;
        zuiContainer.ZoomHost.ContentOffsetX = View.ViewPosition.X;
        zuiContainer.ZoomHost.ContentOffsetY = View.ViewPosition.Y;
        zuiContainer.ZoomHost.ContentViewportWidth = View.ViewWidth;
        zuiContainer.ZoomHost.ContentViewportHeight = View.ViewHeight;
        zuiContainer.ZoomHost.ContentScale = View.ContentZoom;
        zuiContainer.ZoomHost.ContentScalable = View.ContentZoomable;
        zuiContainer.WindowsConfigurable = View.ContentPartsConfigurable;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case IActivityProperties.PropertySource:
            Source = View.Source;
            break;
          case IActivityProperties.PropertyTime:
            Time = View.Time;
            break;
          case IActivityProperties.PropertyViewPosition:
            zuiContainer.ZoomHost.ContentOffsetX = View.ViewPosition.X;
            zuiContainer.ZoomHost.ContentOffsetY = View.ViewPosition.Y;
            break;
          case IActivityProperties.PropertyViewWidth:
            zuiContainer.ZoomHost.ContentViewportWidth = View.ViewWidth;
            break;
          case IActivityProperties.PropertyViewHeight:
            zuiContainer.ZoomHost.ContentViewportHeight = View.ViewHeight;
            break;
          case IActivityProperties.PropertyContentZoom:
            zuiContainer.ZoomHost.ContentScale = View.ContentZoom;
            break;
          case IActivityProperties.PropertyContentZoomable:
            zuiContainer.ZoomHost.ContentScalable = View.ContentZoomable;
            break;
          case IActivityProperties.PropertyContentPartsConfigurable:
            zuiContainer.WindowsConfigurable = View.ContentPartsConfigurable;
            break;
            //...
        }
    }

    #endregion

    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(IActivityProperties.PropertySource, typeof(Uri), typeof(ActivityContainer),
            new FrameworkPropertyMetadata(IActivityDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

    /// <summary>
    /// Gets or sets the Source property.
    /// </summary>
    public Uri Source
    {
      get { return (Uri)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Source property.
    /// </summary>
    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ActivityContainer target = (ActivityContainer)d;
      target.OnSourceChanged((Uri)e.OldValue, target.Source);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Source property.
    /// </summary>
    protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
    {
      View.Source = newSource;
      //...
    }

    #endregion

    #region Time

    /// <summary>
    /// Time Dependency Property
    /// </summary>
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register(IActivityProperties.PropertyTime, typeof(TimeSpan), typeof(ActivityContainer),
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
      ActivityContainer target = (ActivityContainer)d;
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

    #region Add Windows

    private void btnAddMediaPlayer_Click(object sender, RoutedEventArgs e)
    {
      MediaPlayerWindow w = new MediaPlayerWindow();
      AddWindow(w);
      //Two-way bind MediaPlayerWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(View, "Time", w, MediaPlayerWindow.TimeProperty, BindingMode.TwoWay);
      //BindingUtils.BindProperties(w.View, "Time", this, ActivityContainer.TimeProperty, BindingMode.TwoWay);
    } //TODO: check why it won't sync smoothly

    private void btnAddCaptionsGrid_Click(object sender, RoutedEventArgs e)
    {
      //Two-way bind CaptionsGridWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(View, "Time", AddWindow(new CaptionsGridWindow()), CaptionsGridWindow.TimeProperty, BindingMode.TwoWay);
    }

    private void btnAddTextEditor_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(new TextEditorWindow());
    }

    private void btnAddImage_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(new ImageWindow());
    }

    private void btnAddActivityContainer_Click(object sender, RoutedEventArgs e)
    {
      ActivityContainerWindow w = new ActivityContainerWindow(); //TODO: check why this won't work
      AddWindow(w);
      //Two-way bind ActivityContainerWindow.Time to ActivityContainer.Time
      //BindingUtils.BindProperties(this, "Time", AddWindow(new ActivityContainerWindow()), ActivityContainerWindow.TimeProperty, BindingMode.TwoWay);
      BindingUtils.BindProperties(w.View, "Time", this, ActivityContainer.TimeProperty, BindingMode.TwoWay);
    }

    private BaseWindow AddWindow(BaseWindow window)
    {
      window.Scale = 1.0d / zuiContainer.ZoomHost.ContentScale; //TODO: !!! don't use host.Scale, has bug and is always 1
      ZoomAndPanControl host = zuiContainer.ZoomHost;
      Point startPoint = new Point((host.ContentOffsetX + host.ViewportWidth / 2) * host.ContentScale, (zuiContainer.ContentOffsetY + host.ViewportHeight / 2) * zuiContainer.ContentScale); //Center at current view
      zuiContainer.Add(window).Show(startPoint);
      return window;
    }

    #endregion

  }

}
