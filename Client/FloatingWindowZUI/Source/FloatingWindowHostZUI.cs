﻿//Filename: FloatingWindowHostZUI.cs
//Version: 20130131

using SilverFlow.Controls;
using SilverFlow.Controls.Extensions;
using ZoomAndPan;
using WPFCompatibility;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using System.Collections.Specialized;
using System.Windows.Markup;

namespace FloatingWindowZUI
{

  /// <summary>
  /// A zoom & pan content control containing floating windows.
  /// </summary>
  [TemplatePart(Name = PART_ZoomHost, Type = typeof(ZoomAndPanControl))] //the ZoomAndPan control
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

    #region Part names

    protected const string PART_ZoomHost = "PART_ZoomHost";

    #endregion

    protected ZoomAndPanControl _zoomHost;

    public ZoomAndPanControl ZoomHost {
      get
      {
        if (_zoomHost == null) ApplyTemplate();
        return _zoomHost;
      }
      private set
      {
        _zoomHost = value;
      }
    }

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

      ZoomHost = base.GetTemplateChild(PART_ZoomHost) as ZoomAndPanControl;
      ZoomHost.ContentScale = ContentScale; //TODO: also need event handler for the property to apply content scale to ZoomHost

      ZoomHost.IsDefaultMouseHandling = true; //use default mouse handling

      SubscribeToFloatingWindowEvents();
    }

    #region ContentScalable

    /// <summary>
    /// ContentScalable Dependency Property
    /// </summary>
    public static readonly DependencyProperty ContentScalableProperty =
        DependencyProperty.Register("ContentScalable", typeof(bool), typeof(FloatingWindowHostZUI), new FrameworkPropertyMetadata(true));

    /// <summary>
    /// Gets or sets the ContentScalable property.
    /// </summary>
    public bool ContentScalable
    {
      get { return (bool)GetValue(ContentScalableProperty); }
      set { SetValue(ContentScalableProperty, value); }
    }

    #endregion

    #region ContentScale

    /// <summary>
    /// ContentScale Dependency Property
    /// </summary>
    public static readonly DependencyProperty ContentScaleProperty =
      WPF_DependencyProperty.Register("ContentScale", typeof(double), typeof(FloatingWindowHostZUI), new FrameworkPropertyMetadata(1.0)); //no need to do value Coercion, we bind to ZoomHost which will do it

    /// <summary>
    /// Gets or sets the ContentScale property.
    /// </summary>
    public double ContentScale
    {
      get { return (double)GetValue(ContentScaleProperty); }
      set { SetValue(ContentScaleProperty, value); }
    }

    #endregion

    #region MinContentScale

    /// <summary>
    /// MinContentScale Dependency Property
    /// </summary>
    public static readonly DependencyProperty MinContentScaleProperty =
      WPF_DependencyProperty.Register("MinContentScale", typeof(double), typeof(FloatingWindowHostZUI), new FrameworkPropertyMetadata(0.01)); //no need to do value Coercion, we bind to ZoomHost which will do it

    /// <summary>
    /// Gets or sets the MinContentScale property.
    /// </summary>
    public double MinContentScale
    {
      get { return (double)GetValue(MinContentScaleProperty); }
      set { SetValue(MinContentScaleProperty, value); }
    }

    #endregion

    #region MaxContentScale

    /// <summary>
    /// MaxContentScale Dependency Property
    /// </summary>
    public static readonly DependencyProperty MaxContentScaleProperty =
      WPF_DependencyProperty.Register("MaxContentScale", typeof(double), typeof(FloatingWindowHostZUI), new FrameworkPropertyMetadata(10.0)); //no need to do value Coercion, we bind to ZoomHost which will do it

    /// <summary>
    /// Gets or sets the MaxContentScale property.
    /// </summary>
    public double MaxContentScale
    {
      get { return (double)GetValue(MaxContentScaleProperty); }
      set { SetValue(MaxContentScaleProperty, value); }
    }

    #endregion
 
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
      FloatingWindow window = (FloatingWindow)sender;
      if (window.ScaleEnabled && (Keyboard.Modifiers & ModifierKeys.Control) != 0)
      {
        if (changedButton == MouseButton.Left)
          window.Scale += 0.05; //zoom in
        else if (changedButton == MouseButton.Right)
          window.Scale -= 0.05; //zoom out

        e.Handled = true;
      }
      else
        e.Handled = false;
    }

    private void FloatingWindow_MouseWheel(object sender, MouseWheelEventArgs e)
    {
      if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
      {
        FloatingWindow window = (FloatingWindow)sender; //delta should be either >0 or <0
        //Point mousePosition = args.GetPosition(HostPanel); //could use mousePosition here to center the window to the mouse point or something, but better have the logic at FloatingWindow.Scale itself, to recenter arround its previous center point after scaling
        if (e.Delta > 0)
          window.Scale += 0.05; //zoom in
        else if (e.Delta < 0)
          window.Scale -= 0.05; //zoom out

        e.Handled = true;
      }
      else
        e.Handled = false;
    }

    #endregion

    //---------------------------------------------------------------------//

    public override Rect MaximizedWindowBounds
    {
      get
      {
        ScrollViewer scroller = (ZoomHost != null) ? (ZoomHost.Parent as ScrollViewer) : null; //will return null if parent is not a ScrollViewer            
        if (scroller != null) //if the parent is a ScrollViewer maximize to fit the current viewport (needed in ZUI interfaces where the FloatingWindowHost size may be very big)
        {
          double scale = ZoomHost.ContentScale;
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
      ZoomHost.ZoomAboutPoint(ZoomHost.ContentScale - 0.2, contentZoomCenter);
    }

    /// <summary>
    /// The 'ZoomIn' command (bound to the plus key) was executed.
    /// </summary>
    private void ZoomIn(Point contentZoomCenter)
    {
      ZoomHost.ZoomAboutPoint(ZoomHost.ContentScale + 0.2, contentZoomCenter);
    }

  }

}
