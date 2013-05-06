//Filename: ZoomImage.xaml.cs
//Version: 20130507
//Author: George Birbilis (http://zoomicon.com)
//Based on http://samples.msdn.microsoft.com/Silverlight/SampleBrowser DeepZoom samples

//TODO: add ZoomButtonsAlwaysVisible or maybe Zoomable property
//TODO: add way to select the mode programmatically since some URIs may not provide a file extension (or if it has no extension try first to open as image and if it fails its the XML content for DeepZoom which shouldn't take long to reload into MultiScaleImage used as fallback)

using Utils.Bindings;
using Utils.Extensions;

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZoomImage
{
  public partial class ZoomImage : UserControl
  {

    #region Constants

    public const double DEFAULT_ZOOM_STEP = 0.2;

    #endregion

    public ZoomImage()
    {
      InitializeComponent();

      BindingUtils.RegisterForNotification("Width", this, (d, e) => { CheckZoomToFit(); });
      BindingUtils.RegisterForNotification("Height", this, (d, e) => { CheckZoomToFit(); });

      imgPlainZoom.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(control_MouseLeftButtonDown), true); //seems to be needed to grab that specific event
      imgPlainZoom.AddHandler(MouseWheelEvent, new MouseWheelEventHandler(control_MouseWheel), true); //seems to be needed to grab that specific event
    }

    #region Properties
    
    private bool PlainZoomMode
    {
      get { return (scrollPlainZoom.Visibility == Visibility.Visible); }
    }

    private bool DeepZoomMode
    {
      get { return (imgDeepZoom.Visibility == Visibility.Visible); }
    }
    
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
            new FrameworkPropertyMetadata(true,
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

      Uri uri = PreprocessUri(newSource);
      if (uri.ToString().EndsWith(new String[]{".dzi",".dzc",".xml"}, StringComparison.OrdinalIgnoreCase)) //.DZI or .XML for DeepZoom Image and .DZC or .XML for DeepZoom Image Collection (there's also .DZIZ for zipped package with assets, but should be only for authoring/editing tools use)
      {
        scrollPlainZoom.Visibility = Visibility.Collapsed; //hide the ScrollViewer parent of ZoomAndPan control that hosts the classic Image control
        imgPlain.Source = null;

        imgDeepZoom.Visibility = Visibility.Visible;
        imgDeepZoom.Source = new DeepZoomImageTileSource(uri);
      }
      else
      {
        imgDeepZoom.Visibility = Visibility.Collapsed;
        imgDeepZoom.Source = null;

        scrollPlainZoom.Visibility = Visibility.Visible; //show the ScrollViewer parent of ZoomAndPan control that hosts the classic Image control
        imgPlain.Source = new BitmapImage(uri);

      }

      CheckZoomToFit();
    }

    #endregion

    #endregion

    #region Methods

    public void CheckZoomToFit()
    {
      if (ContentZoomToFit)
        try
        {
          ZoomToFit(); //TODO: see why this throws exception when adding an Image component to ClipFlair Studio
        }
        catch { }
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
      
      public void ZoomToFit()
    {
      if (PlainZoomMode) 
        imgPlainZoom.ScaleToFit();
      else if (DeepZoomMode)
      {
        imgDeepZoom.ViewportOrigin = new Point(0, 0);
        imgDeepZoom.ViewportWidth = 1;
      }
    }

    private void Zoom(double zoomStep, Point elementFocusPoint)
    {
      if (PlainZoomMode) 
        imgPlainZoom.ZoomAboutPoint(imgPlainZoom.ContentScale + zoomStep, imgPlainZoom.ElementToLogicalPoint(elementFocusPoint));
      else if (DeepZoomMode)
      {
        Point logicalPoint = imgDeepZoom.ElementToLogicalPoint(elementFocusPoint);
        imgDeepZoom.ZoomAboutLogicalPoint(1 + zoomStep, logicalPoint.X, logicalPoint.Y); //could add an extension method for MultiScaleImage to take a Point instead of X & Y
      }
    }

    #region Uri filters
    
    private Uri PreprocessUri(Uri uri)
    {
      uri = ZoomItUriToDeepZoomImageUri(uri);
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

    #endregion

    #endregion

    #region Events

    private void UserControl_MouseEnter(object sender, MouseEventArgs e)
    {
      zoomControls.Visibility = Visibility.Visible;
    }

    private void UserControl_MouseLeave(object sender, MouseEventArgs e)
    {
      zoomControls.Visibility = Visibility.Collapsed;
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
        control_MouseLeftDoubleClick(sender, e);
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
          if (!DeepZoomMode) return;
          lastMouseLogicalPos = e.GetPosition(imgDeepZoom);
          lastMouseViewPort = imgDeepZoom.ViewportOrigin;
          duringDrag = true;
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
      if (DeepZoomMode) imgDeepZoom.UseSprings = true;
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

    private void control_ImageOpenSucceeded(object sender, RoutedEventArgs e)
    {
      CheckZoomToFit();
    }

    #endregion
   
  }
}
