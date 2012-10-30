//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainer.xaml.cs
//Version: 20121030

using ClipFlair.Models.Views;
using ClipFlair.Windows.Views;

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

      //(assuming Using SilverFlow.Controls)
      //zuiContainer.Add((FloatingWindow)XamlReader.Load("<clipflair:MediaPlayerWindow Width=\"Auto\" Height=\"Auto\" MinWidth=\"100\" MinHeight=\"100\" Title=\"4 Media Player\" IconText=\"1Media Player\" Tag=\"1MediaPlayer\" xmlns:clipflair=\"clr-namespace:ClipFlair;assembly=ClipFlair\"/>"));
      //zuiContainer.Add(new MediaPlayerWindow()).Show();
      //zuiContainer.FloatingWindows.First<FloatingWindow>().Show();

      BindWindows(); //Bind MediaPlayerWindows's Time to CaptionGridWindows's Time programmatically (since using x:Name on the controls doesn't seem to work [components are resolved to null when searched by name - must have to do with the implementation of the Windows property of ZUI container])
      //above command could be avoided if binding to activity view's Time property worked
    }

    private void BindWindows()
    {
      //Two-Way bind MediaPlayerWindow and CaptionsGridWindow over Time property
      Binding timeBinding = new Binding("Time");
      timeBinding.Source = FindWindow("MediaPlayer");
      timeBinding.Mode = BindingMode.TwoWay;
      BindingOperations.SetBinding(FindWindow("Captions"), CaptionsGridWindow.TimeProperty, timeBinding);

      //Two-Way bind MediaPlayerWindow and CaptionsGridWindow over Captions property      
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

    #region Add Windows

    private void btnAddMediaPlayer_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(new MediaPlayerWindow());
    }

    private void btnAddCaptionsGrid_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(new CaptionsGridWindow());
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
      AddWindow(new ActivityContainerWindow());
    }

    private void AddWindow(BaseWindow window)
    {
      window.Scale = 1d / zuiContainer.ZoomHost.ContentScale; //TODO: !!! don't use host.ContentScale, has bug and is always 1
      ZoomAndPanControl host = zuiContainer.ZoomHost;
      Point startPoint = new Point((host.ContentOffsetX + host.ViewportWidth / 2) * host.ContentScale, (zuiContainer.ContentOffsetY + host.ViewportHeight / 2) * zuiContainer.ContentScale); //Center at current view
      zuiContainer.Add(window).Show(startPoint);
    }

    #endregion

  }
  
}
