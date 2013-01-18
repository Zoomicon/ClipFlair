//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainer.xaml.cs
//Version: 20130118

//TODO: add ContentPartsZoomable property
//TODO: move zoom slider UI to FloatingWindowHostZUI's XAML template
//TODO: add to FloatingWindowHostZUI a rezoom to fit button
//TODO: must clear bindings when child window closes (now seem to stay as zombies hearing revoicing entries play at given time)

//#define PART_NESTED_ACTIVITY
#define PART_MEDIA
#define PART_CAPTIONS
#define PART_REVOICING
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

      #if PART_REVOICING
      btnAddRevoicing.Visibility = Visibility.Visible;
      btnAddRevoicing.Click += new RoutedEventHandler(btnAddRevoicing_Click);
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

    #region Windows

    public FloatingWindowCollection Windows //TODO: in the future listen for change events to it (remove/add) to unbind windows or do any default bindings with the container
    {
      get { return zuiContainer.Windows; }
    }

    public void RemoveWindows()
    {
      //TODO: should unbind windows here
      //Windows.RemoveAll(); //TODO: do not use "Clear", doesn't work
      zuiContainer.CloseAllWindows(); //do not use remove, not sure if that closes the window or just removes it from the list (which might keep it alive)
    }

    public void DisableChildrenWarnOnClosing()
    {
      foreach (BaseWindow window in Windows)
        window.View.WarnOnClosing = false;
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

    public BaseWindow AddWindow(IWindowFactory windowFactory, bool newInstance = false)
    {
      try
      {
        BaseWindow w = windowFactory.CreateWindow();

        if (!newInstance)
          AddWindow(w);
        else
        { //for new child window instances the user adds must reset their properties to match their containers so that they don't cause other components bound to the container to lose their Time/Captions when these get bound as sources to the container (which AddWindow does by calling BindWindow)
          if (w is MediaPlayerWindow)
          {
            IMediaPlayer v = ((MediaPlayerWindow)w).View;
            v.Time = View.Time;
            v.Captions = View.Captions;
          }
          else if (w is CaptionsGridWindow)
          {
            ICaptionsGrid v = ((CaptionsGridWindow)w).View;
            v.Time = View.Time;
            v.Captions = View.Captions;
          }
          AddWindowInViewCenter(w);
        };

        return w;
      }
      catch (Exception e)
      {
        MessageBox.Show("Failed to create component: " + e.Message); //TODO: find parent window
        return null;
      }
    }

    public BaseWindow AddWindowInViewCenter(BaseWindow window)
    {
      window.Scale = 1.0d / zuiContainer.ZoomHost.ContentScale; //TODO: !!! don't use host.Scale, has bug and is always 1
      ZoomAndPanControl host = zuiContainer.ZoomHost;
      Point startPoint = new Point((host.ContentOffsetX + host.ViewportWidth / 2) * host.ContentScale, (zuiContainer.ContentOffsetY + host.ViewportHeight / 2) * zuiContainer.ContentScale); //Center at current view
      zuiContainer.Add(window).Show(startPoint);
      BindWindow(window);
      return window;
    }

    public BaseWindow AddWindow(BaseWindow window)
    {
      zuiContainer.Add(window).Show();
      BindWindow(window);
      return window;
    }

    #region binding

    private void BindWindow(BaseWindow window)
    {
      window.ViewChanged += (d, e) => { BindWindow(window); }; //rebind the window if its view changes (e.g. after it loads new state)
      //TODO: remove this when no hard-coded bindings are needed any more
      if (window is MediaPlayerWindow) BindMediaPlayerWindow((MediaPlayerWindow)window);
      else if (window is CaptionsGridWindow) BindCaptionsGridWindow((CaptionsGridWindow)window);
      else if (window is TextEditorWindow) BindTextEditorWindow((TextEditorWindow)window);
    }

    private void BindMediaPlayerWindow(MediaPlayerWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, IMediaPlayerProperties.PropertyTime,
                                      View, IActivityProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)

          BindingUtils.BindProperties(window.View, IMediaPlayerProperties.PropertyCaptions,
                                      View, IActivityProperties.PropertyCaptions); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          MessageBox.Show("Failed to bind component: " + ex.Message); //TODO: find parent window
        }
    } //TODO: check why it won't sync smoothly (see what was doing in LvS, maybe ignore time events that are very close to current time) //most probably need to ignore small time differences at sync

    private void BindCaptionsGridWindow(CaptionsGridWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, ICaptionsGridProperties.PropertyTime,
                                      View, IActivityProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)

          BindingUtils.BindProperties(window.View, ICaptionsGridProperties.PropertyCaptions,
                                      View, IActivityProperties.PropertyCaptions); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          MessageBox.Show("Failed to bind component: " + ex.Message); //TODO: find parent window
        }
    }

    private void BindTextEditorWindow(TextEditorWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, ICaptionsGridProperties.PropertyTime,
                                      View, IActivityProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          MessageBox.Show("Failed to bind component: " + ex.Message); //TODO: find parent window
        }
    }
    
    #endregion

    #if PART_NESTED_ACTIVITY

    private void btnAddNestedActivity_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(ActivityWindowFactory.CreateWindow(), true);
    }

    #endif

    #if PART_MEDIA

    private void btnAddMedia_Click(object sender, RoutedEventArgs e)
    {
      MediaPlayerWindow w = (MediaPlayerWindow)AddWindow(MediaPlayerWindowFactory, true);
      w.View.Source = new Uri("http://video3.smoothhd.com.edgesuite.net/ondemand/Big%20Buck%20Bunny%20Adaptive.ism/Manifest", UriKind.Absolute);
    }

    #endif

    #if PART_CAPTIONS

    private void btnAddCaptions_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(CaptionsGridWindowFactory, true);
    }

    #endif

    #if PART_REVOICING

    private void btnAddRevoicing_Click(object sender, RoutedEventArgs e)
    {
      CaptionsGridWindow w = (CaptionsGridWindow)AddWindow(CaptionsGridWindowFactory, true);
      w.View.Title = "Revoicing";
      w.View.CaptionVisible = false;
      w.View.AudioVisible = true;
      w.View.Width = CaptionsGridDefaults.DefaultWidth_Revoicing;
    }

    #endif

    #if PART_TEXT

    private void btnAddText_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(TextEditorWindowFactory, true);
    }

    #endif

    #if PART_IMAGE

    private void btnAddImage_Click(object sender, RoutedEventArgs e)
    {
      ImageWindow w = (ImageWindow)AddWindow(ImageWindowFactory, true);
      w.View.Source = new Uri("http://clipflair.net/wp-content/themes/clipflair-theme/images/clipflair-logo.jpg", UriKind.Absolute);
    }

    #endif

    #if PART_MAP

    private void btnAddMap_Click(object sender, RoutedEventArgs e)
    {
      AddWindow(MapWindowFactory, true);
    }

    #endif

    #region Load-Save

    public event RoutedEventHandler LoadURLClick;
    public event RoutedEventHandler LoadClick;
    public event RoutedEventHandler SaveClick;

    private void OptionsLoadSaveControl_LoadURLClick(object sender, RoutedEventArgs e)
    {
      if (LoadURLClick != null)
        LoadURLClick(this, e);
    }

    private void OptionsLoadSaveControl_LoadClick(object sender, RoutedEventArgs e)
    {
      if (LoadClick != null)
        LoadClick(this, e);
    }

    private void OptionsLoadSaveControl_SaveClick(object sender, RoutedEventArgs e)
    {
      if (SaveClick != null)
        SaveClick(this, e);
    }

    #endregion

    #endregion

  }

}
