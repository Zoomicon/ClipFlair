//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainer.xaml.cs
//Version: 20121030

using ClipFlair.Models.Views;
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
      //Two-way bind MediaPlayerWindow.Time to  CaptionsGridWindow.Time
      //BindProperties(FindWindow("MediaPlayer"), "Time", FindWindow("Captions"), CaptionsGridWindow.TimeProperty, BindingMode.TwoWay); //not using, binding to container's Time instead for now

      //Two-way bind MediaPlayerWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(this, "Time", FindWindow("MediaPlayer"), MediaPlayerWindow.TimeProperty, BindingMode.TwoWay);
      //Two-way bind CaptionsGridWindow.Time to ActivityContainer.Time
      BindingUtils.BindProperties(this, "Time", FindWindow("Captions"), CaptionsGridWindow.TimeProperty, BindingMode.TwoWay);

      //Two-Way bind MediaPlayerWindow.Captions and CaptionsGridWindow.Captions
      Binding captionsBinding = new Binding("Captions"); //"Captions" is the name of the property here
      captionsBinding.Source = FindWindow("MediaPlayer");
      captionsBinding.Mode = BindingMode.TwoWay;
      BindingOperations.SetBinding(FindWindow("Captions"), CaptionsGridWindow.CaptionsProperty, captionsBinding); //"Captions" is the tag of the window here
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
        Offset = View.Offset;
        Scale = View.Scale;
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
          case IActivityProperties.PropertyOffset:
            Offset = View.Offset;
            break;
          case IActivityProperties.PropertyScale:
            Scale = View.Scale;
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

    #region Offset

    /// <summary>
    /// Offset Dependency Property
    /// </summary>
    public static readonly DependencyProperty OffsetProperty =
        DependencyProperty.Register("Offset", typeof(Point), typeof(ActivityContainer),
            new FrameworkPropertyMetadata(IActivityDefaults.DefaultOffset, new PropertyChangedCallback(OnOffsetChanged)));

    /// <summary>
    /// Gets or sets the Offset property.
    /// </summary>
    public Point Offset
    {
      get { return (Point)GetValue(OffsetProperty); }
      set { SetValue(OffsetProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Offset property.
    /// </summary>
    private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ActivityContainer target = (ActivityContainer)d;
      Point oldOffset = (Point)e.OldValue;
      Point newOffset = target.Offset;
      target.OnOffsetChanged(oldOffset, newOffset);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Offset property.
    /// </summary>
    protected virtual void OnOffsetChanged(Point oldOffset, Point newOffset)
    {
      View.Offset = newOffset;
      //TODO: fix properties of zuiContainer so that we don't need to use .zoomHost. syntax
      zuiContainer.ZoomHost.ContentOffsetX = newOffset.X;
      zuiContainer.ZoomHost.ContentOffsetY = newOffset.Y;
    }

    #endregion

    #region Scale

    /// <summary>
    /// Scale Dependency Property
    /// </summary>
    public static readonly DependencyProperty ScaleProperty =
        DependencyProperty.Register("Scale", typeof(double), typeof(ActivityContainer),
            new FrameworkPropertyMetadata(IActivityDefaults.DefaultScale,
                new PropertyChangedCallback(OnScaleChanged)));

    /// <summary>
    /// Gets or sets the Scale property.
    /// </summary>
    public double Scale
    {
      get { return (double)GetValue(ScaleProperty); }
      set { SetValue(ScaleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Scale property.
    /// </summary>
    private static void OnScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ActivityContainer target = (ActivityContainer)d;
      double oldScale = (double)e.OldValue;
      double newScale = target.Scale;
      target.OnScaleChanged(oldScale, newScale);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Scale property.
    /// </summary>
    protected virtual void OnScaleChanged(double oldScale, double newScale)
    {
      View.Scale = newScale;
      zuiContainer.ZoomHost.ContentScale = newScale; //TODO: fix properties of zuiContainer so that we don't need to use .zoomHost. syntax
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
      window.Scale = 1.0d / zuiContainer.ZoomHost.ContentScale; //TODO: !!! don't use host.ContentScale, has bug and is always 1
      ZoomAndPanControl host = zuiContainer.ZoomHost;
      Point startPoint = new Point((host.ContentOffsetX + host.ViewportWidth / 2) * host.ContentScale, (zuiContainer.ContentOffsetY + host.ViewportHeight / 2) * zuiContainer.ContentScale); //Center at current view
      zuiContainer.Add(window).Show(startPoint);
      return window;
    }

    #endregion

  }

}
