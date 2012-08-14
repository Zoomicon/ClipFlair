//Filename: FloatingWindowHostZUI.cs
//Version: 20120812

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Collections.Specialized;

using SilverFlow.Controls;
using SilverFlow.Controls.Extensions;
using ZoomAndPan;
using WPFCompatibility;

namespace FloatingWindowZUI
{

  /// <summary>
  /// A zoom & pan content control containing floating windows.
  /// </summary>
  [TemplatePart(Name = PART_ZoomHost, Type = typeof(ZoomAndPanControl))]
  public class FloatingWindowHostZUI : FloatingWindowHost
  {

    #region Part names

    protected const string PART_ZoomHost = "PART_ZoomHost";

    #endregion

    #region Fields

    protected ZoomAndPanControl zoomHost;

    #endregion

    public FloatingWindowHostZUI()
    {
      ApplyStyle();
    }

    public override void ApplyStyle()
    {
      //don't call base.ApplyStyle() here
      DefaultStyleKey = typeof(FloatingWindowHostZUI);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate(); //must call this

      zoomHost = base.GetTemplateChild(PART_ZoomHost) as ZoomAndPanControl;
      zoomHost.ContentScale = ContentScale; //TODO: also need event handler for the property to apply content scale to zoomHost

      zoomHost.IsDefaultMouseHandling = true; //use default mouse handling

      SubscribeToFloatingWindowEvents();
    }

    #region FloatingWindowEvents

    private void SubscribeToFloatingWindowEvents()
    {
      //subscribing to current windows
      foreach (FloatingWindow w in Windows)
        SubscribeToFloatingWindowEvents(w);

      //subscribing to added windows and unsubscribing from removed windows
      Windows.CollectionChanged += (s, e) =>
      {
        switch (e.Action)
        {
          case NotifyCollectionChangedAction.Add:
            foreach (FloatingWindow w in e.NewItems)
              SubscribeToFloatingWindowEvents(w);
            break;
          case NotifyCollectionChangedAction.Remove:
            foreach (FloatingWindow w in e.OldItems)
              UnsubscribeFromFloatingWidnowEvents(w);
            break;
        }
      };
    }

    private void SubscribeToFloatingWindowEvents(FloatingWindow w)
    {
      //w.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(FloatingWindow_MouseLeftButtonDown), true); //passing true to get handled events too
      //w.AddHandler(MouseRightButtonDownEvent, new MouseButtonEventHandler(FloatingWindow_MouseRightButtonDown), true);
      w.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(FloatingWindow_MouseLeftButtonUp), true);
      w.AddHandler(MouseRightButtonUpEvent, new MouseButtonEventHandler(FloatingWindow_MouseRightButtonUp), true);
      //w.MouseMove += new MouseEventHandler(FloatingWindow_MouseMove);
      w.MouseWheel += new MouseWheelEventHandler(FloatingWindow_MouseWheel);
    }

    private void UnsubscribeFromFloatingWidnowEvents(FloatingWindow w)
    {
      //w.RemoveHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(FloatingWindow_MouseLeftButtonDown));
      //w.RemoveHandler(MouseRightButtonDownEvent, new MouseButtonEventHandler(FloatingWindow_MouseRightButtonDown));
      w.RemoveHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(FloatingWindow_MouseLeftButtonUp));
      w.RemoveHandler(MouseRightButtonUpEvent, new MouseButtonEventHandler(FloatingWindow_MouseRightButtonUp));
      //w.MouseMove -= new MouseEventHandler(FloatingWindow_MouseMove);
      w.MouseWheel -= new MouseWheelEventHandler(FloatingWindow_MouseWheel);
    }

    private void FloatingWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      FloatingWindow_MouseUp(sender, e, MouseButton.Left);
    }

    private void FloatingWindow_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
      FloatingWindow_MouseUp(sender, e, MouseButton.Right);
    }

    private void FloatingWindow_MouseUp(object sender, MouseButtonEventArgs e, MouseButton changedButton)
    {
      if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
      {
        if (changedButton == MouseButton.Left)
          ((FloatingWindow)sender).Scale += 0.2; //zoom in
        else if (changedButton == MouseButton.Right)
          ((FloatingWindow)sender).Scale -= 0.2; //zoom out

        e.Handled = true;
      }
    }

    private void FloatingWindow_MouseWheel(object sender, MouseWheelEventArgs e)
    {
      if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
      {
        FloatingWindow window = (FloatingWindow)sender; //delta should be either >0 or <0
        //Point mousePosition = args.GetPosition(HostPanel); //could use mousePosition here to center the window to the mouse point or something, but better have the logic at FloatingWindow.Scale itself, to recenter arround its previous center point after scaling
        if (e.Delta > 0)
          window.Scale += 0.2; //zoom in
        else if (e.Delta < 0)
          window.Scale -= 0.2; //zoom out

        e.Handled = true;
      }
    }

    #endregion

    //---------------------------------------------------------------------//

    public override Rect MaximizedWindowBounds
    {
      get
      {
        ScrollViewer scroller = (zoomHost != null) ? (zoomHost.Parent as ScrollViewer) : null; //will return null if parent is not a ScrollViewer            
        if (scroller != null) //if the parent is a ScrollViewer maximize to fit the current viewport (needed in ZUI interfaces where the FloatingWindowHost size may be very big)
        {
          double scale = zoomHost.ContentScale;
          return new Rect(scroller.HorizontalOffset / scale, scroller.VerticalOffset / scale, scroller.ViewportWidth / scale, scroller.ViewportHeight / scale); //TODO: see why this doesn't work correctly (try at different scrollbar offsets and zoom levels)
        }
        else return base.MaximizedWindowBounds;
      }
    }

    /// <summary>
    /// The 'ZoomOut' command (bound to the minus key) was executed.
    /// </summary>
    private void ZoomOut(Point contentZoomCenter)
    {
      zoomHost.ZoomAboutPoint(zoomHost.ContentScale - 0.2, contentZoomCenter);
    }

    /// <summary>
    /// The 'ZoomIn' command (bound to the plus key) was executed.
    /// </summary>
    private void ZoomIn(Point contentZoomCenter)
    {
      zoomHost.ZoomAboutPoint(zoomHost.ContentScale + 0.2, contentZoomCenter);
    }

    #region ZoomAndPan properties

    /// <summary>
    /// Get/set the X offset (in content coordinates) of the view on the content.
    /// </summary>
    public double ContentOffsetX
    {
      get
      {
        return (double)zoomHost.GetValue(ZoomAndPanControl.ContentOffsetXProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.ContentOffsetXProperty, value);
      }
    }

    /// <summary>
    /// Get/set the Y offset (in content coordinates) of the view on the content.
    /// </summary>
    public double ContentOffsetY
    {
      get
      {
        return (double)zoomHost.GetValue(ZoomAndPanControl.ContentOffsetYProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.ContentOffsetYProperty, value);
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
        return (double)zoomHost.GetValue(ZoomAndPanControl.ContentZoomFocusXProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.ContentZoomFocusXProperty, value);
      }
    }

    /// <summary>
    /// The Y coordinate of the content focus, this is the point that we are focusing on when zooming.
    /// </summary>
    public double ContentZoomFocusY
    {
      get
      {
        return (double)zoomHost.GetValue(ZoomAndPanControl.ContentZoomFocusYProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.ContentZoomFocusYProperty, value);
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
        return (double)zoomHost.GetValue(ZoomAndPanControl.ViewportZoomFocusXProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.ViewportZoomFocusXProperty, value);
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
        return (double)zoomHost.GetValue(ZoomAndPanControl.ViewportZoomFocusYProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.ViewportZoomFocusYProperty, value);
      }
    }

    /// <summary>
    /// The duration of the animations (in seconds) started by calling AnimatedZoomTo and the other animation methods.
    /// </summary>
    public double AnimationDuration
    {
      get
      {
        return (double)zoomHost.GetValue(ZoomAndPanControl.AnimationDurationProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.AnimationDurationProperty, value);
      }
    }

    /// <summary>
    /// Get the viewport width, in content coordinates.
    /// </summary>
    public double ContentViewportWidth
    {
      get
      {
        return (double)zoomHost.GetValue(ZoomAndPanControl.ContentViewportWidthProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.ContentViewportWidthProperty, value);
      }
    }

    /// <summary>
    /// Get the viewport height, in content coordinates.
    /// </summary>
    public double ContentViewportHeight
    {
      get
      {
        return (double)zoomHost.GetValue(ZoomAndPanControl.ContentViewportHeightProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.ContentViewportHeightProperty, value);
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
        return (bool)zoomHost.GetValue(ZoomAndPanControl.IsMouseWheelScrollingEnabledProperty);
      }
      set
      {
        zoomHost.SetValue(ZoomAndPanControl.IsMouseWheelScrollingEnabledProperty, value);
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
      ZoomAndPanControl.ContentScale_PropertyChanged(((FloatingWindowHostZUI)o).zoomHost, e);
    }

    /// <summary>
    /// Method called to clamp the 'ContentScale' value to its valid range.
    /// </summary>
    private static object ContentScale_Coerce(DependencyObject d, object baseValue)
    {
      return ZoomAndPanControl.ContentScale_Coerce(((FloatingWindowHostZUI)d).zoomHost, baseValue);
    }

    /// <summary>
    /// Event raised 'MinContentScale' or 'MaxContentScale' has changed.
    /// </summary>
    public static void MinOrMaxContentScale_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      ZoomAndPanControl.MinOrMaxContentScale_PropertyChanged(((FloatingWindowHostZUI)o).zoomHost, e);
    }

    #endregion

  }

}
