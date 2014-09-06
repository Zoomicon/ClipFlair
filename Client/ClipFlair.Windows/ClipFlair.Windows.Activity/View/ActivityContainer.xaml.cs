//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityContainer.xaml.cs
//Version: 20140906

//TODO: add ContentPartsCloseable property
//TODO: add ContentPartsZoomable property
//TODO: must clear bindings when child window closes (now seem to stay as zombies hearing revoicing entries play at given time)

//TODO: maybe use MEF Deployment Catalog and put components in separate XAPs (each one a Silverlight app, set to reuse the same Web Project) and imported here with CopyLocal=False

using ClipFlair.UI.Dialogs;
using ClipFlair.Windows.Views;
using SilverFlow.Controls;
using FloatingWindowZUI;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Utils.Bindings;
using ZoomAndPan;
using Utils.Extensions;

namespace ClipFlair.Windows
{

  [ContentProperty("Windows")]
  public partial class ActivityContainer : UserControl
  {

    #region --- Initialization ---

    public ActivityContainer()
    {
      View = new ActivityView(); //must set the view first

      InitializeComponent();
      InitializeView();

      BindingUtils.RegisterForNotification("ContentOffsetX", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewPosition = new Point(zuiContainer.ZoomHost.ContentOffsetX, zuiContainer.ZoomHost.ContentOffsetY); } });
      BindingUtils.RegisterForNotification("ContentOffsetY", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewPosition = new Point(zuiContainer.ZoomHost.ContentOffsetX, zuiContainer.ZoomHost.ContentOffsetY); } });
      BindingUtils.RegisterForNotification("ContentViewportWidth", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewWidth = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("ContentViewportHeight", zuiContainer.ZoomHost, (d, e) => { if (View != null) { View.ViewHeight = (double)e.NewValue; } });
 
      LoadParts();
    }

    #endregion

    #region --- View ---

    protected virtual void InitializeView()
    {
      View.ViewPosition = new Point(zuiContainer.ZoomHost.ContentOffsetX, zuiContainer.ZoomHost.ContentOffsetY);
      View.ViewWidth = zuiContainer.ZoomHost.ContentViewportWidth;
      View.ViewHeight = zuiContainer.ZoomHost.ContentViewportHeight;
      View.ContentPartsConfigurable = zuiContainer.ShowOptionsButton; //TODO: add similar choice for Screenshot and Help buttons
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

    //TODO: try to replace these with bindings, as long as they're unbound/rebound when view changes
    protected void View_PropertyChanged(object sender, PropertyChangedEventArgs e) //note: for View.ContentPartsConfigurable using data binding in XAML (binds to zuiContainer.WindowsConfigurable)
    {
      switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
      {
        case null: //multiple (not specified) properties have changed, consider all as changed
          zuiContainer.ZoomHost.ContentOffsetX = View.ViewPosition.X;
          zuiContainer.ZoomHost.ContentOffsetY = View.ViewPosition.Y;
          zuiContainer.ZoomHost.ContentViewportWidth = View.ViewWidth;
          zuiContainer.ZoomHost.ContentViewportHeight = View.ViewHeight;
          CheckZoomToFit();
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
        case IActivityProperties.PropertyContentZoomToFit:
          CheckZoomToFit();
          break;
      }
    }

    #endregion

    #region --- Windows ---

    public ActivityWindow ActivityWindow { get; internal set; }

    public BaseWindow FindWindow(string tag) //need this since floating windows are not added in the XAML visual tree by the FloatingWindowHostZUI.Windows property (maybe should have FloatingWindowHostZUI inherit 
    {
      foreach (BaseWindow w in zuiContainer.Windows)
        if (tag == (string)w.Tag) return w; //must cast to string to compare (else we compare object references, since Tag property is of type object, not string)
      return null;
    }
    
    public void ZoomToFit()
    {
      Dispatcher.BeginInvoke(() => { zuiContainer.ZoomToFit(); }); //make sure we invoke this on the UI thread, else it won't always work correctly
    }

    public void CheckZoomToFit()
    {
      if (View.ContentZoomToFit)
        ZoomToFit();
    }

    public FloatingWindowCollection Windows //TODO: in the future listen for change events to it (remove/add) to unbind windows or do any default bindings with the container
    {
      get { return zuiContainer.Windows; }
    }

    public void RemoveWindows(bool ignoreChildrenWarnOnClosing = false)
    {
      if (ignoreChildrenWarnOnClosing)
        DisableChildrenWarnOnClosing();

      //TODO: should unbind windows here
      //Windows.RemoveAll(); //TODO: do not use "Clear", doesn't work
      zuiContainer.CloseAllWindows(); //do not use remove, not sure if that closes the window or just removes it from the list (which might keep it alive)
    }

    public void DisableChildrenWarnOnClosing()
    {
      foreach (BaseWindow window in Windows)
        window.View.WarnOnClosing = false;
    }

    public BaseWindow AddWindow(IWindowFactory windowFactory, bool newInstance = false)
    {
      try
      {
        BaseWindow w = windowFactory.CreateWindow();

        if (!newInstance)
          AddWindow(w);
        else
        { //TODO: must change this scheme to remove hardcoded logic here, by using reverse binding order (source=container, target=component) when adding a new component manually than when loading saved state

          //for new child window instances (that the user adds), must reset their properties to match their containers so that they don't cause other components bound to the container to lose their Time/Captions when these get bound as sources to the container (which AddWindow does by calling BindWindow)
          if (w is MediaPlayerWindow)
          {
            IMediaPlayer v = ((MediaPlayerWindow)w).MediaPlayerView;
            v.Time = View.Time;
            v.Captions = View.Captions;
          }
          else if (w is CaptionsWindow)
          {
            ICaptionsGrid v = ((CaptionsWindow)w).CaptionsGridView;
            v.Time = View.Time;
            v.Captions = View.Captions;
          }
          else if (w is TextEditorWindow)
          {
            ITextEditor2 v = ((TextEditorWindow)w).TextEditorView2;
            v.Time = View.Time;
            //v.Captions = View.Captions;
          }
          else if (w is ImageWindow)
          {
            IImageViewer v = ((ImageWindow)w).ImageView;
            v.Time = View.Time;
          }
          else if (w is MapWindow)
          {
            IMapViewer v = ((MapWindow)w).MapView;
            v.Time = View.Time;
            //v.Captions = View.Captions;
          } 
          AddWindowInViewCenter(w);
        };

        return w;
      }
      catch (Exception e)
      {
        ErrorDialog.Show("Failed to create component", e);
        return null;
      }
    }

    public BaseWindow AddWindowInViewCenter(BaseWindow window, bool autozoom = false) //TODO: have param to add some randomness (within given x,y offsets)
    {
      ZoomAndPanControl host = zuiContainer.ZoomHost;

      if (autozoom) //TODO: see why when using autozoom at zoomed out container on small screens, new windows can get out of screen bounds on the top
      {
        double zoom = host.ContentScale;
        window.Scale = 1.0d / zoom;
      }
      
      Point startPoint = new Point(host.ContentOffsetX + host.ContentViewportWidth / 2,
                                   host.ContentOffsetY + host.ContentViewportHeight / 2); //Center at current view //must use ContentViewport methods, not Viewport ones (they give width/height in content coordinates)
      zuiContainer.Add(window).Show(startPoint, true);
      BindWindow(window);
      return window;
    }

    public BaseWindow AddWindow(BaseWindow window, bool bringToFront = true)
    {
      zuiContainer.Add(window).Show(bringToFront);
      BindWindow(window);

      //GalleryWindow gw = window as GalleryWindow; //TODO: try to remove this patch (needed to be able to load activities with saved gallery that has a filter set to it)
      //if (gw != null) gw.RefreshFilter();

      return window;
    }

    #region Add actions

    public MediaPlayerWindow AddClip()
    {
      MediaPlayerWindow w = (MediaPlayerWindow)AddWindow(MediaPlayerWindowFactory, newInstance: true);
      w.MediaPlayerView.AutoPlay = false;
      w.MediaPlayerView.Source = new Uri(ActivityWindow.URL_DEFAULT_VIDEO, UriKind.Absolute);
      return w;
    }

    public CaptionsWindow AddCaptions()
    {
      CaptionsWindow w = (CaptionsWindow)AddWindow(CaptionsWindowFactory, newInstance: true);
      return w;
    }

    public CaptionsWindow AddRevoicing()
    {
      CaptionsWindow w = (CaptionsWindow)AddWindow(CaptionsWindowFactory, newInstance: true);
      w.View.Title = "Revoicing";
      w.CaptionsGridView.CaptionVisible = false;
      w.CaptionsGridView.AudioVisible = true;
      w.View.Width = CaptionsGridDefaults.DefaultWidth_Revoicing;
      return w;
    }

    public TextEditorWindow AddText()
    {
      TextEditorWindow w = (TextEditorWindow)AddWindow(TextEditorWindowFactory, newInstance: true);
      return w;
    }

    public ImageWindow AddImage()
    {
      ImageWindow w = (ImageWindow)AddWindow(ImageWindowFactory, newInstance: true);
      w.ImageView.Source = new Uri(ActivityWindow.URL_DEFAULT_IMAGE, UriKind.Absolute);
      return w;
    }

    public MapWindow AddMap()
    {
      MapWindow w = (MapWindow)AddWindow(MapWindowFactory, newInstance: true);
      return w;
    }

    public NewsWindow AddNews()
    {
      NewsWindow w = (NewsWindow)AddWindow(NewsWindowFactory, newInstance: true);
      w.NewsView.Source = new Uri(ActivityWindow.URL_NEWS, UriKind.Absolute);
      return w;
    }

    public BrowserWindow AddBrowser()
    {
      BrowserWindow w = (BrowserWindow)AddWindow(BrowserWindowFactory, newInstance: true);
      w.BrowserView.Source = new Uri(ActivityWindow.URL_PROJECT_HOME, UriKind.Absolute);
      return w;
    }

    public GalleryWindow AddGallery(string source = "activities", string title = null)
    {
      GalleryWindow w = (GalleryWindow)AddWindow(GalleryWindowFactory, newInstance: true);
      w.GalleryView.Source = new Uri(ActivityWindow.URL_GALLERY_PREFIX + source + ".cxml"); //TODO: move logic from Studio's App.xaml.cs into GalleryWindow's Source proprty to translate partial URIs into ClipFlair gallery URIs
      if (title != null)
        w.Title = title;
      return w;
    }

    public void AddNestedActivity()
    {
      AddWindow(ActivityWindowFactory, newInstance: true);
    }

    #endregion

    #endregion

    #region --- Binding ---

    private void BindWindow(BaseWindow window)
    {
      window.ViewChanged += (d, e) => { BindWindow(window); }; //rebind the window if its view changes (e.g. after it loads new state)
      //TODO: remove this when no hard-coded bindings are needed any more
      if (window is MediaPlayerWindow) BindMediaPlayerWindow((MediaPlayerWindow)window);
      else if (window is CaptionsWindow) BindCaptionsGridWindow((CaptionsWindow)window);
      else if (window is TextEditorWindow) BindTextEditorWindow((TextEditorWindow)window);
      else if (window is ImageWindow) BindImageWindow((ImageWindow)window);
      else if (window is MapWindow) BindMapWindow((MapWindow)window);
      else if (window is NewsWindow) BindNewsWindow((NewsWindow)window);
      else if (window is BrowserWindow) BindBrowserWindow((BrowserWindow)window);
      else if (window is GalleryWindow) BindGalleryWindow((GalleryWindow)window);
    }

    private void BindMediaPlayerWindow(MediaPlayerWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, IViewProperties.PropertyTime,
                                      View, IViewProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)

          BindingUtils.BindProperties(window.View, IMediaPlayerProperties.PropertyCaptions,
                                      View, IActivityProperties.PropertyCaptions); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Failed to bind Clip component", ex);
        }
    } //TODO: check why it won't sync smoothly (see what was doing in LvS, maybe ignore time events that are very close to current time) //most probably need to ignore small time differences at sync

    private void BindCaptionsGridWindow(CaptionsWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, IViewProperties.PropertyTime,
                                      View, IViewProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)

          BindingUtils.BindProperties(window.View, ICaptionsGridProperties.PropertyCaptions,
                                      View, IActivityProperties.PropertyCaptions); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Failed to bind Captions component", ex);
        }
    }

    private void BindTextEditorWindow(TextEditorWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, IViewProperties.PropertyTime,
                                      View, IViewProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Failed to bind Text component", ex);
        }
    }

    private void BindImageWindow(ImageWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, IViewProperties.PropertyTime,
                                      View, IViewProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Failed to bind Image component", ex);
        }
    }

    private void BindMapWindow(MapWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, IViewProperties.PropertyTime,
                                      View, IViewProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Failed to bind Map component", ex);
        }
    }

    private void BindNewsWindow(NewsWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          //BindingUtils.BindProperties(window.View, IViewProperties.PropertyTime,
          //                            View, IViewProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Failed to bind News component", ex);
        }
    }

    private void BindBrowserWindow(BrowserWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          //BindingUtils.BindProperties(window.View, IViewProperties.PropertyTime,
          //                            View, IViewProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Failed to bind Browser component", ex);
        }
    }

    private void BindGalleryWindow(GalleryWindow window)
    {
      if (window.View != null && View != null)
        try
        {
          BindingUtils.BindProperties(window.View, IViewProperties.PropertyTime,
                                      View, IViewProperties.PropertyTime); //TODO: should rebind after changing view if the window is inside a BaseWindow (get its View and bind to it)
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Failed to bind Gallery component", ex);
        }
    }

    #endregion

    private void btnProjectHome_Click(object sender, RoutedEventArgs e)
    {
      ActivityWindow.ShowStartDialog();
    }

    #region Add Button events

    private void btnAddClip_Click(object sender, RoutedEventArgs e)
    {
      AddClip();
    }

    private void btnAddCaptions_Click(object sender, RoutedEventArgs e)
    {
      AddCaptions();
    }

    private void btnAddRevoicing_Click(object sender, RoutedEventArgs e)
    {
      AddRevoicing();
    }

    private void btnAddText_Click(object sender, RoutedEventArgs e)
    {
      AddText();
    }

    private void btnAddImage_Click(object sender, RoutedEventArgs e)
    {
      AddImage();
    }

    private void btnAddMap_Click(object sender, RoutedEventArgs e)
    {
      AddMap();
    }

    private void btnAddNews_Click(object sender, RoutedEventArgs e)
    {
      AddNews();
    }

    private void btnAddBrowser_Click(object sender, RoutedEventArgs e)
    {
      AddBrowser();
    }

    private void btnAddGallery_Click(object sender, RoutedEventArgs e)
    {
      AddGallery();
    }

    private void btnAddNestedActivity_Click(object sender, RoutedEventArgs e)
    {
      AddNestedActivity();
    }

    #endregion

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

    #region --- MEF ---

    public CompositionContainer mefContainer;

    [Import("ClipFlair.Windows.Views.MediaPlayerView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory MediaPlayerWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.CaptionsGridView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory CaptionsWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.TextEditorView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory TextEditorWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.ImageView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory ImageWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.MapView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory MapWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.NewsView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory NewsWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.BrowserView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory BrowserWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.GalleryView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory GalleryWindowFactory { get; set; }

    [Import("ClipFlair.Windows.Views.ActivityView", typeof(IWindowFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
    public IWindowFactory ActivityWindowFactory { get; set; }

    private void LoadParts()
    {
      AggregateCatalog partsCatalog = new AggregateCatalog();
      //don't put the following in conditional compilation block, all are needed for loading of saved options
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MediaPlayerWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CaptionsWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(TextEditorWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ImageWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MapWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(NewsWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(BrowserWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(typeof(GalleryWindow).Assembly));
      partsCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly())); //typeof(ActivityWindow).Assembly

      InitPartButtons();

      mefContainer = new CompositionContainer(partsCatalog);
      mefContainer.SatisfyImportsOnce(this);
      //CompositionInitializer.SatisfyImports(this);
    }

    private void InitPartButtons()
    {
      btnAddClip.Click += btnAddClip_Click;
      btnAddCaptions.Click += btnAddCaptions_Click;
      btnAddRevoicing.Click += btnAddRevoicing_Click;
      btnAddText.Click += btnAddText_Click;
      btnAddImage.Click += btnAddImage_Click;
      btnAddMap.Click += btnAddMap_Click;
      btnAddNews.Click += btnAddNews_Click;
      btnAddBrowser.Click += btnAddBrowser_Click;
      btnAddGallery.Click += btnAddGallery_Click;
      btnAddNestedActivity.Click += btnAddNestedActivity_Click;

      #if SILVERLIGHT
      #if !WINDOWS_PHONE
      if (!Application.Current.IsRunningOutOfBrowser)
      #endif
        btnAddBrowser.Visibility = Visibility.Collapsed; //WebBrowser class is available in OOB mode only - even if we had some C# version of a full WebBrowser, still would have issue with remote sites not using ClientAccessPolicy.xml
      #endif
    }

    #endregion

  }

}
