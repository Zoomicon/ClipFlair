//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainerWindow.xaml.cs
//Version: 20121102

using ClipFlair.Windows.Views;

using System;
using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Windows
{
  public partial class ActivityContainerWindow : BaseWindow
  {
    public ActivityContainerWindow()
    {
      InitializeComponent();

      //TODO: see why this breaks time-sync
      //View = activity.View; //must initialize the component first
      //Title = View.Title;
      //IconText = View.Title; //IconText should match the Title
      
      //Position = View.Position;
      //Width = View.Width;
      //Height = View.Height;
      //Scale = View.Zoom;
      //MoveEnabled = View.Moveable;
      //ResizeEnabled = View.Resizable;
      //Scalable = View.Zoomable;

      //Source = View.Source;
      //Time = View.Time;
    }

    #region --- Properties ---

    #region View

    public new IActivity View //hiding parent property
    {
      get {return (IActivity)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

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
        DependencyProperty.Register(IActivityProperties.PropertySource, typeof(Uri), typeof(MediaPlayerWindow),
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
      ActivityContainerWindow target = (ActivityContainerWindow)d;
      target.OnSourceChanged((Uri)e.OldValue, target.Source);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
    {
      View.Source = newSource;
      //player.Source = newSource; //not used, using data binding instead in the XAML
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
      if (activity.Time != newTime) //check this for speedup and to avoid loops
        activity.Time = newTime;
    }

    #endregion
          
    #endregion
        
  }

}