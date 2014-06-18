//Filename: ZoomImage.xaml.cs
//Version: 20140618
//Author: George Birbilis (http://zoomicon.com)

//Based on http://samples.msdn.microsoft.com/Silverlight/SampleBrowser DeepZoom samples

//TODO: add ContentZoomable property
//TODO: add way to select the mode programmatically since some URIs may not provide a file extension (or if it has no extension try first to open as image and if it fails its the XML content for DeepZoom which shouldn't take long to reload into MultiScaleImage used as fallback)

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Extensions;

namespace ZoomImage
{
  public partial class ZoomImage : UserControl
  {

    #region --- Constants ---

    public const string IMAGE_LOAD_FILTER = "Image files (*.png, *.jpg)|*.png;*.jpg";

    public const bool DEFAULT_CONTENT_ZOOM_TO_FIT = true;
    public const bool DEFAULT_ZOOM_CONTROLS_AVAILABLE = true;
    public const double DEFAULT_ZOOM_STEP = 0.2;

    #endregion

    #region --- Initialization ---

    public ZoomImage()
    {
      InitializeComponent();

      imgPlainZoom.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(control_MouseLeftButtonDown), true); //seems to be needed to grab that specific event
      imgPlainZoom.AddHandler(MouseWheelEvent, new MouseWheelEventHandler(control_MouseWheel), true); //seems to be needed to grab that specific event
    }

    #endregion

    #region --- Properties ---

    public string Filename { get; set; }
    public Stream ImageData { get; set; } 
    
    private bool PlainZoomMode
    {
      get { return (scrollPlainZoom.Visibility == Visibility.Visible); }
    }

    private bool DeepZoomMode
    {
      get { return (imgDeepZoom.Visibility == Visibility.Visible); }
    }

    #region ZoomControlsAvailable

    /// <summary>
    /// ZoomControlsAvailable Dependency Property
    /// </summary>
    public static readonly DependencyProperty ZoomControlsAvailableProperty =
        DependencyProperty.Register("ZoomControlsAvailable", typeof(bool), typeof(ZoomImage),
            new FrameworkPropertyMetadata(DEFAULT_ZOOM_CONTROLS_AVAILABLE));

    /// <summary>
    /// Gets or sets the ZoomControlsAvailable property
    /// </summary>
    public bool ZoomControlsAvailable
    {
      get { return (bool)GetValue(ZoomControlsAvailableProperty); }
      set { SetValue(ZoomControlsAvailableProperty, value); }
    }

    #endregion

    #region ZoomStep

    /// <summary>
    /// ZoomStep Dependency Property
    /// </summary>
    public static readonly DependencyProperty ZoomStepProperty =
        DependencyProperty.Register("ZoomStep", typeof(double), typeof(ZoomImage),
            new FrameworkPropertyMetadata(DEFAULT_ZOOM_STEP,
                FrameworkPropertyMetadataOptions.None));

    /// <summary>
    /// Gets or sets the ZoomStep property
    /// </summary>
    public double ZoomStep
    {
      get { return (double)GetValue(ZoomStepProperty); }
      set { SetValue(ZoomStepProperty, value); }
    }

    #endregion

    #region ContentZoomToFit

    /// <summary>
    /// ContentZoomToFit Dependency Property
    /// </summary>
    public static readonly DependencyProperty ContentZoomToFitProperty =
        DependencyProperty.Register("ContentZoomToFit", typeof(bool), typeof(ZoomImage),
            new FrameworkPropertyMetadata(DEFAULT_CONTENT_ZOOM_TO_FIT,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnContentZoomToFitChanged)));

    /// <summary>
    /// Gets or sets the ContentZoomToFit property
    /// </summary>
    public bool ContentZoomToFit
    {
      get { return (bool)GetValue(ContentZoomToFitProperty); }
      set { SetValue(ContentZoomToFitProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ContentZoomToFit property
    /// </summary>
    private static void OnContentZoomToFitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ZoomImage target = (ZoomImage)d;
      bool oldContentZoomToFit = (bool)e.OldValue;
      bool newContentZoomToFit = target.ContentZoomToFit;
      target.OnContentZoomToFitChanged(oldContentZoomToFit, newContentZoomToFit);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the ContentZoomToFit property
    /// </summary>
    protected virtual void OnContentZoomToFitChanged(bool oldContentZoomToFit, bool newContentZoomToFit)
    {
      CheckZoomToFit();
    }

    #endregion
    
    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(Uri), typeof(ZoomImage),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnSourceChanged)));

    /// <summary>
    /// Gets or sets the Source property
    /// </summary>
    public Uri Source
    {
      get { return (Uri)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Source property
    /// </summary>
    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ZoomImage target = (ZoomImage)d;
      Uri oldSource = (Uri)e.OldValue;
      Uri newSource = target.Source;
      target.OnSourceChanged(oldSource, newSource);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Source property
    /// </summary>
    protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
    {
      if (newSource == null) {
        imgPlain.Source = null;
        imgDeepZoom.Source = null;
        return;
      }

      Filename = null;
      ImageData = null;

      Uri uri = PreprocessUri(newSource);
      if (uri.ToString().EndsWith(new string[]{".dzi",".dzc",".xml"}, StringComparison.OrdinalIgnoreCase)) //.DZI or .XML for DeepZoom Image and .DZC or .XML for DeepZoom Image Collection (there's also .DZIZ for zipped package with assets, but should be only for authoring/editing tools use)
      {
        scrollPlainZoom.Visibility = Visibility.Collapsed; //hide the ScrollViewer parent of ZoomAndPan control that hosts the classic Image control
        imgPlain.Source = null;

        imgDeepZoom.Visibility = Visibility.Visible;
        imgDeepZoom.Source = new DeepZoomImageTileSource(uri);
      }
      else //Plain image (no DeepZoom one)
      {
        imgDeepZoom.Visibility = Visibility.Collapsed;
        imgDeepZoom.Source = null;

        scrollPlainZoom.Visibility = Visibility.Visible; //show the ScrollViewer parent of ZoomAndPan control that hosts the classic Image control
        imgPlain.Source = new BitmapImage(uri);
      }

      //CheckZoomToFit(); //not calling this here, since it will be called by "control_ImageOpenSucceeded" when image has opened (which will be called for local images too)
    }

    #endregion

    #endregion

    #region --- Methods ---

    public void CheckZoomToFit()
    {
      if (ContentZoomToFit)
        try
        {
          ZoomToFit(); //TODO: see why this throws exception when adding an Image component to ClipFlair Studio
        }
        catch { }
    }

    public void ZoomToFit()
    {
      if (PlainZoomMode)
        imgPlainZoom.ZoomToFit();
      else if (DeepZoomMode)
      {
        imgDeepZoom.ViewportOrigin = new Point(0, 0);
        imgDeepZoom.ViewportWidth = 1;
      }
    }

    public void ZoomIn(double zoomStep = DEFAULT_ZOOM_STEP)
    {
      if (PlainZoomMode) imgPlainZoom.ZoomIn(zoomStep);
      else if (DeepZoomMode) imgDeepZoom.ZoomAboutLogicalPoint(1 + zoomStep, 0.5, 0.5); //using same ZoomFactorStep as ZoomAndPan control (imgPlainZoom) uses
    }

    public void ZoomOut(double zoomStep = DEFAULT_ZOOM_STEP)
    {
      if (PlainZoomMode) imgPlainZoom.ZoomOut(zoomStep);
      else if (DeepZoomMode) imgDeepZoom.ZoomAboutLogicalPoint(1 - zoomStep, 0.5, 0.5); //using same ZoomFactorStep as ZoomAndPan control (imgPlainZoom) uses
    }
      
    private void Zoom(double zoomStep, Point elementFocusPoint)
    {
      if (PlainZoomMode) 
        imgPlainZoom.ZoomAboutPoint(imgPlainZoom.ContentScale + zoomStep, imgPlainZoom.ElementToLogicalPoint(elementFocusPoint)); //NOTE: this expects to get the new zoom, not a delta
      else if (DeepZoomMode)
        imgDeepZoom.ZoomAboutLogicalPoint(1 + zoomStep, imgDeepZoom.ElementToLogicalPoint(elementFocusPoint)); //NOTE: this expects a delta?
    }

    #region Uri filters
    
    private Uri PreprocessUri(Uri uri)
    {
      uri = ZoomItUriToDeepZoomImageUri(uri);
      //uri = ClipFlairGalleryToDeepZoomImageUri(uri); //DO NOT USE, HAS LABELS BURNED ON THE IMAGE
      //note: can add other filter calls too as separate "uri = Filter(uri);" rows or can call them as one chain in a single command like "uri = Filter1(Filter2(Filter3(uri)));"
      return uri;
    }

    public static Uri ZoomItUriToDeepZoomImageUri(Uri uri)
    {
      string uriStr = uri.ToString();
      if (uriStr.StartsWith("http://zoom.it/"))
        uri = new Uri(uriStr.ReplacePrefix("http://zoom.it/", "http://cache.zoom.it/content/", StringComparison.OrdinalIgnoreCase) + ".dzi");
      return uri;
    }

    public static Uri ClipFlairGalleryToDeepZoomImageUri(Uri uri) //UNFORTUNATELY THESE DEEPZOOM IMAGES HAVE LABELS BURNED ON THEM
    {
      string uriStr = uri.ToString();
      if (uriStr.StartsWith("http://gallery.clipflair.net/image/"))
        uri = new Uri(uriStr.ReplacePrefix("http://gallery.clipflair.net/image/", "http://gallery.clipflair.net/collection/images_deepzoom/", StringComparison.OrdinalIgnoreCase) + ".dzi");
      return uri;
    }


    #endregion

    public bool OpenLocalFile() //Note: this has to be initiated by user action (Silverlight security)
    {
      try
      {
        OpenFileDialog dlg = new OpenFileDialog()
        {
          Filter = IMAGE_LOAD_FILTER,
          FilterIndex = 1 //note: this index is 1-based, not 0-based //OpenFileDialog doesn't seem to have a DefaultExt like SaveFileDialog
        };

        if (dlg.ShowDialog() == true) //TODO: find the parent window
        { 
          Open(dlg.File);
          return true;
        }
      }
      catch (Exception e)
      {
        MessageBox.Show("Loading failed: " + e.Message); //TODO: find the parent window
      }
      return false;
    }

    public void Open(FileInfo file)
    {
      using (Stream fileStream = file.OpenRead()) //closing stream after loading image
        Open(fileStream, file.Name);
    }

    public void Open(Stream stream, string filename)
    {
      Filename = filename;
      ImageData = stream;

      Source = null; //clear source URL since we're loading directly from a Stream

      imgDeepZoom.Visibility = Visibility.Collapsed;
      imgDeepZoom.Source = null;

      BitmapImage bitmap = new BitmapImage();
      bitmap.SetSource(stream);
      imgPlain.Source = bitmap;

      scrollPlainZoom.Visibility = Visibility.Visible; //show the ScrollViewer parent of ZoomAndPan control that hosts the classic Image control
    } 
   
    #endregion

    #region --- Events ---

    private void UserControl_MouseEnter(object sender, MouseEventArgs e)
    {
      zoomControls.Visibility = (ZoomControlsAvailable)? Visibility.Visible : Visibility.Collapsed; //show zoom controls at mouse enter if ZoomControlsAvailable is true
    }

    private void UserControl_MouseLeave(object sender, MouseEventArgs e)
    {
      zoomControls.Visibility = Visibility.Collapsed; //hide zoom controls at mouse leave
    }

    private void btnZoomIn_Click(object sender, RoutedEventArgs e)
    {
      ZoomIn();
    }

    private void btnZoomOut_Click(object sender, RoutedEventArgs e)
    {
      ZoomOut();
    }

    private void btnZoomToFit_Click(object sender, RoutedEventArgs e)
    {
      ZoomToFit();
    }

    private void control_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
      e.Handled = true;
      Zoom(ZoomStep * Math.Sign(e.Delta), e.GetPosition((UIElement)sender));
    }
  
    Point lastMouseLogicalPos = new Point();
    Point lastMouseViewPort = new Point();
    bool duringDrag = false;
  
    private void control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      #if SILVERLIGHT
      if (e.ClickCount == 2)
      {
        control_MouseLeftDoubleClick(sender, e); //called function will handle the event
        return;
      }
      #endif

      if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
        {
          e.Handled = true;
          Zoom(ZoomStep, e.GetPosition((UIElement)sender));
        }
        else
        {
          duringDrag = true;

          if (!DeepZoomMode) return;
          
          e.Handled = true;
          lastMouseLogicalPos = e.GetPosition(imgDeepZoom);
          lastMouseViewPort = imgDeepZoom.ViewportOrigin;
        }
    }

    private void control_MouseLeftDoubleClick(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
      Zoom(ZoomStep, e.GetPosition((UIElement)sender));
    }

    private void control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
        e.Handled = true;

      duringDrag = false;

      if (DeepZoomMode)
      {
        e.Handled = true;
        imgDeepZoom.UseSprings = true; //???
      }
    }

    private void control_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
      if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
      {
        e.Handled = true;
        Zoom(-ZoomStep, e.GetPosition((UIElement)sender));
      }
    }

    private void control_MouseMove(object sender, MouseEventArgs e)
    {
      if (!DeepZoomMode) return;

      if (duringDrag)
      {
        Point newPoint = lastMouseViewPort;
        Point thisMouseLogicalPos = e.GetPosition((UIElement)sender);
        newPoint.X += (lastMouseLogicalPos.X - thisMouseLogicalPos.X) / imgDeepZoom.ActualWidth * imgDeepZoom.ViewportWidth;
        newPoint.Y += (lastMouseLogicalPos.Y - thisMouseLogicalPos.Y) / imgDeepZoom.ActualWidth * imgDeepZoom.ViewportWidth;
        imgDeepZoom.ViewportOrigin = newPoint;
      }
    }

    private void control_ImageOpenSucceeded(object sender, RoutedEventArgs e) //using same event handler for "imgDeepZoom" and "imgPlain"
    {
      CheckZoomToFit();
    }

    private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      CheckZoomToFit();
    }

    #region Drag & Drop

    protected override void OnDragEnter(DragEventArgs e)
    {
      base.OnDragEnter(e);
      e.Handled = true; //must do this
      VisualStateManager.GoToState(this, "DragOver", true);
    }

    protected override void OnDragOver(DragEventArgs e)
    {
      base.OnDragOver(e);
      e.Handled = true; //must do this
      //NOP
    }

    protected override void OnDragLeave(DragEventArgs e)
    {
      base.OnDragLeave(e);
      e.Handled = true; //must do this
      VisualStateManager.GoToState(this, "Normal", true);
    }

    protected override void OnDrop(DragEventArgs e)
    {
      base.OnDrop(e);

      VisualStateManager.GoToState(this, "Normal", true);

      //we receive an array of FileInfo objects for the list of files that were selected and drag-dropped onto this control.
      if (e.Data == null)
        return;

      IDataObject f = e.Data as IDataObject;
      if (f == null) //checks if the dropped objects are files
        return;

      object data = f.GetData(DataFormats.FileDrop); //Silverlight 5 only supports FileDrop - GetData returns null if format is not supported
      FileInfo[] files = data as FileInfo[];

      if (files != null && files.Length > 0) //Use only 1st item from array of FileInfo objects
      {
        //TODO: instead of hardcoding which file extensions to ignore, should have this as property of the control (a ; separated string or an array)
        if (files[0].Name.EndsWith(new string[] { ".clipflair", ".clipflair.zip" }, StringComparison.OrdinalIgnoreCase))
          return;

        e.Handled = true; //must do this

        try
        {
          Open(files[0]); //open the first file dropped //TODO: add slideshow support using SlideShow.net component
        }
        catch (Exception ex)
        {
          MessageBox.Show("Loading failed: " + ex.Message); //TODO: find the parent window
          //TODO: maybe should wrap the original exception as inner exception and throw a new one
        }
      }
    }

    #endregion
    
    #endregion

  }
}
