//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainer.xaml.cs
//Version: 20121204

//TODO: add ContentPartsZoomable property
//TODO: move zoom slider UI to FloatingWindowHostZUI's XAML template
//TODO: add to FloatingWindowHostZUI a rezoom to fit button
//TODO: must clear bindings when child window closes (now seem to stay as zombies hearing revoicing entries play at given time)

//#define PART_NESTED_ACTIVITY
//#define PART_MEDIA
#define PART_CAPTIONS
#define PART_TEXT
#define PART_IMAGE
#define PART_MAP

//TODO: maybe use MEF Deployment Catalog and put components in separate XAPs (each one a Silverlight app, set to reuse the same Web PRoject) and imported here with CopyLocal=False

using ClipFlair.Windows.Views;
using ClipFlair.Utils.Bindings;

using ZoomAndPan;
using SilverFlow.Controls;

using System;
using System.Windows;
using System.Windows.Markup;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace ClipFlair.Windows
{

  [ContentProperty("Windows")]
  public partial class ActivityContainer : UserControl
  {

    public ActivityContainer()
    {
      View = new ActivityView(); //must set the view first

      InitializeComponent();
      InitializeView();

      BindingUtils.RegisterForNotification("ContentOffsetX", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewPosition = new Point(zuiContainer.ZoomHost.ContentOffsetX, zuiContainer.ZoomHost.ContentOffsetY); } });
      BindingUtils.RegisterForNotification("ContentOffsetY", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewPosition = new Point(zuiContainer.ZoomHost.ContentOffsetX, zuiContainer.ZoomHost.ContentOffsetY); } });
      BindingUtils.RegisterForNotification("ContentViewportWidth", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewWidth = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("ContentViewportHeight", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewHeight = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("ContentScale", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ContentZoom = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("ContentScalable", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ContentZoomable = (bool)e.NewValue; } });

      LoadParts();
    }

    private void LoadParts()
    {
      AggregateCatalog partsCatalog = new AggregateCatalog();
      //don't put the following in conditional compilation block, all are needed for loading of saved options
      partsCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly())); //typeof(ActivityWindow).Assembly
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MediaPlayerWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CaptionsGridWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(TextEditorWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ImageWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MapWindow).Assembly));

#if PART_NESTED_ACTIVITY
      btnAddNestedActivity.Visibility = Visibility.Visible;
      btnAddNestedActivity.Click += new RoutedEventHandler(btnAddNestedActivity_Click);
#endif

#if PART_MEDIA      
      btnAddMedia.Visibility = Visibility.Visible;
      btnAddMedia.Click += new RoutedEventHandler(btnAddMedia_Click);
#endif

#if PART_CAPTIONS
      btnAddCaptions.Visibility = Visibility.Visible;
      btnAddCaptions.Click += new RoutedEventHandler(btnAddCaptions_Click);
#endif

#if PART_TEXT
      btnAddText.Visibility = Visibility.Visible;
      btnAddText.Click += new RoutedEventHandler(btnAddText_Click);
#endif

#if PART_IMAGE
      btnAddImage.Visibility = Visibility.Visible;
      btnAddImage.Click += new RoutedEventHandler(btnAddImage_Click);
#endif

#if PART_MAP
      btnAddMap.Visibility = Visibility.Visible;
      btnAddMap.Click += new RoutedEventHandler(btnAddMap_Click);
#endif

      CompositionContainer container = new CompositionContainer(partsCatalog);
      container.SatisfyImportsOnce(this);
      //CompositionInitializer.SatisfyImports(this);
    }

    ~ActivityContainer()
    {
      View = null; //unregister PropertyChangedEventHandler
    }

    public BaseWindow FindWindow(string tag) //need this since floating windows are not added in the XAML visual tree by the FloatingWindowHostZUI.Windows property (maybe should have FloatingWindowHostZUI inherit 
    {
      foreach (BaseWindow w in zuiContainer.Windows)
        if (tag == (string)w.Tag) return w; //must cast to string to compare (else we compare object references, since Tag property is of type object, not string)
      return null;
    }

    #region View

    protected virtual void InitializeView()
    {
      View.ViewPosition = new Point(zuiContainer.ZoomHost.ContentOffsetX, zuiContainer.ZoomHost.ContentOffsetY);
      View.ViewWidth = zuiContainer.ZoomHost.ContentViewportWidth;
      View.ViewHeight = zuiContainer.ZoomHost.ContentViewportHeight;
      View.ContentZoom = zuiContainer.ZoomHost.ContentScale;
      View.ContentZoomable = zuiContainer.ZoomHost.ContentScalable;
      View.ContentPartsConfigurable = zuiContainer.WindowsConfigurable;
    }

    public IActivity View
    {
      get { return (ActivityView)DataContext; }
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          View.PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged);
        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
        //set the new view (must do last)
        DataContext = value;
      }
    }

    protected void View_PropertyChanged(object sender, PropertyChangedEventArgs e) //note: for View.ContentPartsConfigurable using data binding in XAML (binds to zuiContainer.WindowsConfigurable)
    {
      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Time = View.Time;
        zuiContainer.ZoomHost.ContentOffsetX = View.ViewPosition.X;
        zuiContainer.ZoomHost.ContentOffsetY = View.ViewPosition.Y;
        zuiContainer.ZoomHost.ContentViewportWidth = View.ViewWidth;
        zuiContainer.ZoomHost.ContentViewportHeight = View.ViewHeight;
        zuiContainer.ZoomHost.ContentScale = View.ContentZoom;
        zuiContainer.ZoomHost.ContentScalable = View.ContentZoomable;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
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
            //...
        }
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

    #region Windows

    public FloatingWindowCollection Windows //TODO: in the future listen for change events to it (remove/add) to unbind windows or do any default bindings with the container
    {
      get { return zuiContainer.Windows; }
    }

    public void RemoveWindows()
    {
      //TODO: should unbind windows here
      Windows.RemoveAll(); //TODO: do not use "Clear", doesn't work
    }

    [Import("ClipFlair.Windows.Views.ActivityView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory ActivityWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.MediaPlayerView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory MediaPlayerWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.CaptionsGridView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory CaptionsGridWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.TextEditorView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory TextEditorWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.ImageView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory ImageWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.MapView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory MapWindowFactory { get; set; }

    public BaseWindow AddWindow(IWindowFactory windowFactory)
    {
      try
      {
        BaseWindow w = windowFactory.CreateWindow();
        AddWindow(w);
        return w;
      }
      catch (Exception e)
      {
        MessageBox.Show("Failed to create component: " + e.Message); //TODO: find parent window
        return null;
      }
    }

    public BaseWindow AddWindow(BaseWindow window)
    {
      window.Scale = 1.0d / zuiContainer.ZoomHost.ContentScale; //TODO: !!! don't use host.Scale, has bug and is always 1
      ZoomAndPanControl host = zuiContainer.ZoomHost;
      Point startPoint = new Point((host.ContentOffsetX + host.ViewportWidth / 2) * host.ContentScale, (zuiContainer.ContentOffsetY + host.ViewportHeight / 2) * zuiContainer.ContentScale); //Center at current view
      zuiContainer.Add(window).Show(startPoint);

      //TODO: remove this when no hard-coded bindings are needed any more
      //if (window is MediaPlayerWindow) BindMediaPlayerWindow((MediaPlayerWindow)window);
      //else if (window is CaptionsGridWindow) BindCaptionsGridWindow((CaptionsGridWindow)window);

      return window;
    }

    private void BindMediaPlayerWindow(MediaPlayerWindow w)
    {
      try
      {
        //Two-way bind MediaPlayerWindow.Time to ActivityContainer.View.Time via inherited DataContext (make sure we don't bind to window view since it changes after state reloading)
        BindingUtils.BindProperties(View, "Time", w, BindingUtils.GetDependencyProperty(w, "Time" + "Property"), BindingMode.TwoWay);
        BindingUtils.BindProperties(View, "Captions", w, BindingUtils.GetDependencyProperty(w, "Captions" + "Property"), BindingMode.TwoWay);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Failed to bind component: " + ex.Message); //TODO: find parent window
      }
    } //TODO: check why it won't sync smoothly (see what was doing in LvS, maybe ignore time events that are very close to current time)

    private void BindCaptionsGridWindow(CaptionsGridWindow w)
    {
      //Two-way bind CaptionsGridWindow.Time to ActivityContainer.Time
      try
      {
        //Two-way bind CaptionsGridWindow.Time to ActivityContainer.View.Time via inherited DataContext (make sure we don't bind to window view since it changes after state reloading)
        BindingUtils.BindProperties(View, "Time", w, BindingUtils.GetDependencyProperty(w, "Time" + "Property"), BindingMode.TwoWay);
        View.Captions = w.Captions; //must do this else CaptionGrid's loaded captions will be lost
        BindingUtils.BindProperties(View, "Captions", w, BindingUtils.GetDependencyProperty(w, "Captions" + "Property"), BindingMode.TwoWay);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Failed to bind component: " + ex.Message); //TODO: find parent window
      }
    }

    #if PART_NESTED_ACTIVITY

    private void btnAddNestedActivity_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(ActivityWindowFactory.CreateWindow());
    }

    #endif

    #if PART_MEDIA

    private void btnAddMedia_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(MediaPlayerWindowFactory);
    }

    #endif

    #if PART_CAPTIONS

    private void btnAddCaptions_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(CaptionsGridWindowFactory);
    }

    #endif

    #if PART_TEXT

    private void btnAddText_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(TextEditorWindowFactory);
    }

    #endif

    #if PART_IMAGE

    private void btnAddImage_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(ImageWindowFactory);
    }

    #endif

    #if PART_MAP

    private void btnAddMap_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(MapWindowFactory);
    }

    #endif

    #endregion

  }

}
