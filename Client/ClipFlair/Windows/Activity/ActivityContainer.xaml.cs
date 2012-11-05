//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainer.xaml.cs
//Version: 20121104

using ClipFlair.Windows.Views;
using ClipFlair.Utils.Bindings;

using ZoomAndPan;

using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

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
    }

    private void BindWindows()
    {
      //Two-way bind MediaPlayerWindow.Time to CaptionsGridWindow.Time
      //BindProperties(FindWindow("MediaPlayer"), "Time", FindWindow("Captions"), CaptionsGridWindow.TimeProperty, BindingMode.TwoWay); //not using, binding to container's Time instead for now

      //Two-way bind MediaPlayerWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(this, "Time", FindWindow("MediaPlayer"), MediaPlayerWindow.TimeProperty, BindingMode.TwoWay);
      //Two-way bind CaptionsGridWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(this, "Time", FindWindow("Captions"), CaptionsGridWindow.TimeProperty, BindingMode.TwoWay);

      //Two-Way bind MediaPlayerWindow.Captions to CaptionsGridWindow.Captions
      BindingUtils.BindProperties(FindWindow("Captions"), "Captions", FindWindow("MediaPlayer"), MediaPlayerWindow.CaptionsProperty, BindingMode.TwoWay);
    }

    public BaseWindow FindWindow(string tag) //need this since floating windows are not added in the XAML visual tree by the FloatingWindowHostZUI.Windows property (maybe should have FloatingWindowHostZUI inherit 
    {
      foreach (BaseWindow w in zuiContainer.Windows)
        if (tag == (string)w.Tag) return w; //must cast to string to compare (else we compare object references, since Tag property is of type object, not string)
      return null;
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
        ViewPosition = View.ViewPosition;
        ViewHeight = View.ViewHeight;
        ViewHeight = View.ViewHeight;
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
            ViewPosition = View.ViewPosition;
            break;
          case IActivityProperties.PropertyViewWidth:
            ViewHeight = View.ViewWidth;
            break;
          case IActivityProperties.PropertyViewHeight:
            ViewHeight = View.ViewHeight;
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
      //...
    }

    #endregion

    #region ViewPosition

    /// <summary>
    /// ViewPosition Dependency Property
    /// </summary>
    public static readonly DependencyProperty ViewPositionProperty =
        DependencyProperty.Register(IActivityProperties.PropertyViewPosition, typeof(Point), typeof(ActivityContainer),
            new FrameworkPropertyMetadata(IActivityDefaults.DefaultViewPosition, new PropertyChangedCallback(OnViewPositionChanged)));

    /// <summary>
    /// Gets or sets the ViewPosition property.
    /// </summary>
    public Point ViewPosition
    {
      get { return (Point)GetValue(ViewPositionProperty); }
      set { SetValue(ViewPositionProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ViewPosition property.
    /// </summary>
    private static void OnViewPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ActivityContainer target = (ActivityContainer)d;
      Point oldViewPosition = (Point)e.OldValue;
      Point newViewPosition = target.ViewPosition;
      target.OnViewPositionChanged(oldViewPosition, newViewPosition);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ViewPosition property.
    /// </summary>
    protected virtual void OnViewPositionChanged(Point oldViewPosition, Point newViewPosition)
    {
      View.ViewPosition = newViewPosition;
      //TODO: fix properties of zuiContainer so that we don't need to use .zoomHost. syntax
      zuiContainer.ZoomHost.ContentOffsetX = newViewPosition.X;
      zuiContainer.ZoomHost.ContentOffsetY = newViewPosition.Y;
    }

    #endregion

    #region ViewWidth

    /// <summary>
    /// ViewWidth Dependency Property
    /// </summary>
    public static readonly DependencyProperty ViewWidthProperty =
        DependencyProperty.Register(IActivityProperties.PropertyViewWidth, typeof(double), typeof(ActivityContainer),
            new FrameworkPropertyMetadata(IActivityDefaults.DefaultViewWidth, new PropertyChangedCallback(OnViewWidthChanged)));

    /// <summary>
    /// Gets or sets the ViewWidth property.
    /// </summary>
    public double ViewWidth
    {
      get { return (double)GetValue(ViewWidthProperty); }
      set { SetValue(ViewWidthProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ViewWidth property.
    /// </summary>
    private static void OnViewWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ActivityContainer target = (ActivityContainer)d;
      double oldViewWidth = (double)e.OldValue;
      double newViewWidth = target.ViewWidth;
      target.OnViewWidthChanged(oldViewWidth, newViewWidth);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ViewWidth property.
    /// </summary>
    protected virtual void OnViewWidthChanged(double oldViewWidth, double newViewWidth)
    {
      View.ViewWidth = newViewWidth;
      zuiContainer.ZoomHost.ContentViewportWidth = newViewWidth; //TODO: fix properties of zuiContainer so that we don't need to use .zoomHost. syntax
    }

    #endregion

    #region ViewHeight

    /// <summary>
    /// ViewHeight Dependency Property
    /// </summary>
    public static readonly DependencyProperty ViewHeightProperty =
        DependencyProperty.Register(IActivityProperties.PropertyViewHeight, typeof(double), typeof(ActivityContainer),
            new FrameworkPropertyMetadata(IActivityDefaults.DefaultViewHeight, new PropertyChangedCallback(OnViewHeightChanged)));

    /// <summary>
    /// Gets or sets the ViewHeight property.
    /// </summary>
    public double ViewHeight
    {
      get { return (double)GetValue(ViewHeightProperty); }
      set { SetValue(ViewHeightProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ViewHeight property.
    /// </summary>
    private static void OnViewHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ActivityContainer target = (ActivityContainer)d;
      double oldViewHeight = (double)e.OldValue;
      double newViewHeight = target.ViewHeight;
      target.OnViewHeightChanged(oldViewHeight, newViewHeight);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ViewHeight property.
    /// </summary>
    protected virtual void OnViewHeightChanged(double oldViewHeight, double newViewHeight)
    {
      View.ViewHeight = newViewHeight;
      zuiContainer.ZoomHost.ContentViewportHeight = newViewHeight; //TODO: fix properties of zuiContainer so that we don't need to use .zoomHost. syntax
    }

    #endregion

    #region ContentZoom

    /// <summary>
    /// ContentZoom Dependency Property
    /// </summary>
    public static readonly DependencyProperty ContentZoomProperty =
        DependencyProperty.Register(IActivityProperties.PropertyContentZoom, typeof(double), typeof(ActivityContainer),
            new FrameworkPropertyMetadata(IActivityDefaults.DefaultContentZoom, new PropertyChangedCallback(OnContentZoomChanged)));

    /// <summary>
    /// Gets or sets the ContentZoom property.
    /// </summary>
    public double ContentZoom
    {
      get { return (double)GetValue(ContentZoomProperty); }
      set { SetValue(ContentZoomProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ContentZoom property.
    /// </summary>
    private static void OnContentZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ActivityContainer target = (ActivityContainer)d;
      double oldContentZoom = (double)e.OldValue;
      double newContentZoom = target.ContentZoom;
      target.OnContentZoomChanged(oldContentZoom, newContentZoom);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ContentZoom property.
    /// </summary>
    protected virtual void OnContentZoomChanged(double oldContentZoom, double newContentZoom)
    {
      View.ContentZoom = newContentZoom;
      zuiContainer.ZoomHost.ContentScale = newContentZoom; //TODO: fix properties of zuiContainer so that we don't need to use .zoomHost. syntax
    }

    #endregion

    #region Add Windows

    private void btnAddMediaPlayer_Click(object sender, RoutedEventArgs e)
    {
      //Two-way bind MediaPlayerWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(this, "Time", AddWindow(new MediaPlayerWindow()), MediaPlayerWindow.TimeProperty, BindingMode.TwoWay);
    }

    private void btnAddCaptionsGrid_Click(object sender, RoutedEventArgs e)
    {
      //Two-way bind CaptionsGridWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(this, "Time", AddWindow(new CaptionsGridWindow()), CaptionsGridWindow.TimeProperty, BindingMode.TwoWay);
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
      //Two-way bind ActivityContainerWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(this, "Time", AddWindow(new ActivityContainerWindow()), ActivityContainerWindow.TimeProperty, BindingMode.TwoWay);
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
