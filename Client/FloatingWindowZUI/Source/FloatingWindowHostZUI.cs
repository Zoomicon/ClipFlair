//Version: 20120712

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Input;
using SilverFlow.Controls;
using ZoomAndPan;
using WPFCompatibility;

using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace FloatingWindowZUI
{

  [TemplatePart(Name = PART_Root, Type = typeof(Grid))]
  [TemplatePart(Name = PART_ContentRoot, Type = typeof(FrameworkElement))]
  [TemplatePart(Name = PART_HostCanvas, Type = typeof(Canvas))]
  [TemplatePart(Name = PART_ModalCanvas, Type = typeof(Canvas))]
  [TemplatePart(Name = PART_IconBarContainer, Type = typeof(FrameworkElement))]
  [TemplatePart(Name = PART_Overlay, Type = typeof(Grid))]
  [TemplatePart(Name = PART_IconBar, Type = typeof(IconBar))]
  [TemplatePart(Name = PART_BottomBar, Type = typeof(FrameworkElement))]
  [TemplatePart(Name = PART_BootstrapButton, Type = typeof(BootstrapButton))]
  [TemplatePart(Name = PART_BarContent, Type = typeof(ContentControl))]
  [TemplateVisualState(Name = VSMSTATE_VisibleOverlay, GroupName = VSMGROUP_Overlay)]
  [TemplateVisualState(Name = VSMSTATE_HiddenOverlay, GroupName = VSMGROUP_Overlay)]
  [StyleTypedProperty(Property = PROPERTY_BottomBarStyle, StyleTargetType = typeof(Border))]
  [StyleTypedProperty(Property = PROPERTY_BootstrapButtonStyle, StyleTargetType = typeof(BootstrapButton))]
  [StyleTypedProperty(Property = PROPERTY_WindowIconStyle, StyleTargetType = typeof(WindowIcon))]
  [ContentProperty("Windows")]
  public class FloatingWindowHostZUI : FloatingWindowHost
  {

    protected const string PART_ZoomHost = "PART_ZoomHost";

    public ZoomAndPanControl ZoomHost { get; private set; }

    public FloatingWindowHostZUI()
    {
      DefaultStyleKey = typeof(FloatingWindowHostZUI);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      ZoomHost = base.GetTemplateChild(PART_ZoomHost) as ZoomAndPanControl;
      ZoomHost.ContentScale = ContentScale; //?

      _timer = new DispatcherTimer();
      _timer.Interval = new TimeSpan(0, 0, 0, 0, INTERVAL);
      _timer.Tick += new EventHandler(_timer_Tick);

      ZoomHost.MouseMove += new MouseEventHandler(zoomAndPanControl_MouseMove);
      ZoomHost.MouseWheel += new MouseWheelEventHandler(zoomAndPanControl_MouseWheel);
      
      ZoomHost.MouseLeftButtonDown += new MouseButtonEventHandler(zoomAndPanControl_MouseLeftButtonDown);
      ZoomHost.MouseLeftButtonUp += new MouseButtonEventHandler(zoomAndPanControl_MouseLeftButtonUp);
      //ZoomHost.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(zoomAndPanControl_MouseLeftButtonDown), true);
      //ZoomHost.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(zoomAndPanControl_MouseLeftButtonUp), true);

      ZoomHost.IsMouseWheelScrollingEnabled = true; //??? if this worked we could pan with mouse wheel when not over a control
    }

    //---------------------------------------------------------------------//

    /// <summary>
    /// The 'ZoomOut' command (bound to the minus key) was executed.
    /// </summary>
    private void ZoomOut(Point contentZoomCenter)
    {
      ZoomHost.ZoomAboutPoint(ZoomHost.ContentScale - 0.2, contentZoomCenter);
    }

    /// <summary>
    /// The 'ZoomIn' command (bound to the plus key) was executed.
    /// </summary>
    private void ZoomIn(Point contentZoomCenter)
    {
      ZoomHost.ZoomAboutPoint(ZoomHost.ContentScale + 0.2, contentZoomCenter);
    }
   

    #region ZoomAndPan properties

    /// <summary>
    /// Get/set the X offset (in content coordinates) of the view on the content.
    /// </summary>
    public double ContentOffsetX
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.ContentOffsetXProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.ContentOffsetXProperty, value);
      }
    }

    /// <summary>
    /// Get/set the Y offset (in content coordinates) of the view on the content.
    /// </summary>
    public double ContentOffsetY
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.ContentOffsetYProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.ContentOffsetYProperty, value);
      }
    }

    /// <summary>
    /// Get/set the current scale (or zoom factor) of the content.
    /// </summary>
    public double ContentScale
    {
      get
      {
        return (double)GetValue(ContentScaleProperty);
      }
      set
      {
        SetValue(ContentScaleProperty, value);
      }
    }

    /// <summary>
    /// Get/set the minimum value for 'ContentScale'.
    /// </summary>
    public double MinContentScale
    {
      get
      {
        return (double)GetValue(MinContentScaleProperty);
      }
      set
      {
        SetValue(MinContentScaleProperty, value);
      }
    }

    /// <summary>
    /// Get/set the maximum value for 'ContentScale'.
    /// </summary>
    public double MaxContentScale
    {
      get
      {
        return (double)GetValue(MaxContentScaleProperty);
      }
      set
      {
        SetValue(MaxContentScaleProperty, value);
      }
    }

    /*
    /// <summary>
    /// The X coordinate of the content focus, this is the point that we are focusing on when zooming.
    /// </summary>
    public double ContentZoomFocusX
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.ContentZoomFocusXProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.ContentZoomFocusXProperty, value);
      }
    }

    /// <summary>
    /// The Y coordinate of the content focus, this is the point that we are focusing on when zooming.
    /// </summary>
    public double ContentZoomFocusY
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.ContentZoomFocusYProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.ContentZoomFocusYProperty, value);
      }
    }

    /// <summary>
    /// The X coordinate of the viewport focus, this is the point in the viewport (in viewport coordinates) 
    /// that the content focus point is locked to while zooming in.
    /// </summary>
    public double ViewportZoomFocusX
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.ViewportZoomFocusXProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.ViewportZoomFocusXProperty, value);
      }
    }

    /// <summary>
    /// The Y coordinate of the viewport focus, this is the point in the viewport (in viewport coordinates) 
    /// that the content focus point is locked to while zooming in.
    /// </summary>
    public double ViewportZoomFocusY
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.ViewportZoomFocusYProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.ViewportZoomFocusYProperty, value);
      }
    }

    /// <summary>
    /// The duration of the animations (in seconds) started by calling AnimatedZoomTo and the other animation methods.
    /// </summary>
    public double AnimationDuration
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.AnimationDurationProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.AnimationDurationProperty, value);
      }
    }

    /// <summary>
    /// Get the viewport width, in content coordinates.
    /// </summary>
    public double ContentViewportWidth
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.ContentViewportWidthProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.ContentViewportWidthProperty, value);
      }
    }

    /// <summary>
    /// Get the viewport height, in content coordinates.
    /// </summary>
    public double ContentViewportHeight
    {
      get
      {
        return (double)ZoomHost.GetValue(ZoomAndPanControl.ContentViewportHeightProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.ContentViewportHeightProperty, value);
      }
    }

    /// <summary>
    /// Set to 'true' to enable the mouse wheel to scroll the zoom and pan control.
    /// This is set to 'false' by default.
    /// </summary>
    public bool IsMouseWheelScrollingEnabled
    {
      get
      {
        return (bool)ZoomHost.GetValue(ZoomAndPanControl.IsMouseWheelScrollingEnabledProperty);
      }
      set
      {
        ZoomHost.SetValue(ZoomAndPanControl.IsMouseWheelScrollingEnabledProperty, value);
      }
    }
    */

    #endregion

    #region ZoomAndPan DependencyProperties

    public static readonly DependencyProperty ContentScaleProperty =
            WPF_DependencyProperty.Register("ContentScale", typeof(double), typeof(FloatingWindowHostZUI), new FrameworkPropertyMetadata(1.0, ContentScale_PropertyChanged, ContentScale_Coerce));


    public static readonly DependencyProperty MinContentScaleProperty =
            WPF_DependencyProperty.Register("MinContentScale", typeof(double), typeof(FloatingWindowHostZUI), new FrameworkPropertyMetadata(0.01, MinOrMaxContentScale_PropertyChanged));

    public static readonly DependencyProperty MaxContentScaleProperty =
            WPF_DependencyProperty.Register("MaxContentScale", typeof(double), typeof(FloatingWindowHostZUI), new FrameworkPropertyMetadata(10.0, MinOrMaxContentScale_PropertyChanged));

    /// <summary>
    /// Event raised when the 'ContentScale' property has changed value.
    /// </summary>
    public static void ContentScale_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      ZoomAndPanControl.ContentScale_PropertyChanged(((FloatingWindowHostZUI)o).ZoomHost, e);
    }

    /// <summary>
    /// Method called to clamp the 'ContentScale' value to its valid range.
    /// </summary>
    private static object ContentScale_Coerce(DependencyObject d, object baseValue)
    {
      return ZoomAndPanControl.ContentScale_Coerce(((FloatingWindowHostZUI)d).ZoomHost, baseValue);
    }

    /// <summary>
    /// Event raised 'MinContentScale' or 'MaxContentScale' has changed.
    /// </summary>
    public static void MinOrMaxContentScale_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      ZoomAndPanControl.MinOrMaxContentScale_PropertyChanged(((FloatingWindowHostZUI)o).ZoomHost, e);
    }

    #endregion

    #region ZoomAndPan events related variables

    /// <summary>
    /// Specifies the current state of the mouse handling logic.
    /// </summary>
    private MouseHandlingMode mouseHandlingMode = MouseHandlingMode.None;

    /// <summary>
    /// The point that was clicked relative to the ZoomAndPanControl.
    /// </summary>
    private Point origZoomAndPanControlMouseDownPoint;

    /// <summary>
    /// The point that was clicked relative to the content that is contained within the ZoomAndPanControl.
    /// </summary>
    private Point origContentMouseDownPoint;

    /// <summary>
    /// To Detect Double Click
    /// </summary>
    DispatcherTimer _timer;

    /// <summary>
    /// Double Click Detect Interval
    /// </summary>
    private static int INTERVAL = 200;
    
    /// <summary>
    /// Aminate content image while zooming.
    /// </summary>
    private Storyboard sb;
    #endregion


    #region ZoomAndPan events

    /// <summary>
    /// Event raised on mouse move in the ZoomAndPanControl.
    /// </summary>
    private void zoomAndPanControl_MouseMove(object sender, MouseEventArgs e)
    {
      if (mouseHandlingMode == MouseHandlingMode.Panning)
      {
        Point curContentMousePoint = e.GetPosition(HostPanel);
        ZoomHost.ContentOffsetX -= curContentMousePoint.X - origContentMouseDownPoint.X;
        ZoomHost.ContentOffsetY -= curContentMousePoint.Y - origContentMouseDownPoint.Y;
      }
    }
    
    /// <summary>
    /// Event raised by rotating the mouse wheel
    /// </summary>
    private void zoomAndPanControl_MouseWheel(object sender, MouseWheelEventArgs e)
    {
      e.Handled = true;

      if (e.Delta > 0)
      {
        Point curContentMousePoint = e.GetPosition(HostPanel);
        ZoomIn(curContentMousePoint);
      }
      else if (e.Delta < 0)
      {
        Point curContentMousePoint = e.GetPosition(HostPanel);
        ZoomOut(curContentMousePoint);
      }
    }

    void sb_Completed(object sender, EventArgs e)
    {
      sb.Stop();
    }

    void _timer_Tick(object sender, EventArgs e)
    {
      _timer.Stop();
    }

    /// <summary>
    /// Event raised on mouse down in the ZoomAndPanControl.
    /// </summary>
    private void zoomAndPanControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (_timer.IsEnabled)
      {
        if (ZoomHost.ContentScale <= ZoomHost.MaxContentScale)
        {
          _timer.Stop();
          sb = new Storyboard();
          DoubleAnimation db = new DoubleAnimation();
          sb.Children.Add(db);
          sb.Completed += new EventHandler(sb_Completed);
          db.To = ZoomHost.ContentScale + 0.2;
          db.Duration = new Duration(TimeSpan.FromSeconds(0.3));
          Storyboard.SetTarget(db, ZoomHost);
          Storyboard.SetTargetProperty(db, new PropertyPath("ContentScale"));
          sb.Begin();

          /*Point curContentMousePoint = e.GetPosition(content);
          //ZoomHost.ContentOffsetX = curContentMousePoint.X ; //dragOffset.X;
          //ZoomHost.ContentOffsetY = curContentMousePoint.Y ; //dragOffset.Y;               
          ZoomHost.ZoomAboutPoint(ZoomHost.ContentScale + 0.2, curContentMousePoint);*/
        }

      }
      else
      {
        _timer.Start();
        if (ZoomHost.ContentScale >= MinContentScale)
        {

          origZoomAndPanControlMouseDownPoint = e.GetPosition(ZoomHost);
          origContentMouseDownPoint = e.GetPosition(HostPanel);
          mouseHandlingMode = MouseHandlingMode.Panning;
          if (mouseHandlingMode != MouseHandlingMode.None)
          {
            ZoomHost.CaptureMouse();
            e.Handled = true;
          }
        }

      }

    }

    /// <summary>
    /// Event raised on mouse up in the ZoomAndPanControl.
    /// </summary>
    private void zoomAndPanControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      if (ZoomHost.ContentScale >= MinContentScale)
      {
        ZoomHost.ReleaseMouseCapture();
        mouseHandlingMode = MouseHandlingMode.None;
        e.Handled = true;
      }
    }

    #endregion

  }

}
