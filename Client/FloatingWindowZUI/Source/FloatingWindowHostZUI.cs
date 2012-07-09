﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

using SilverFlow.Controls;
using ZoomAndPan;
using WPFCompatibility;

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
    #region Constants

    protected const string PART_ZoomHost = "PART_ZoomHost";

    #endregion

    #region Properties

    public ZoomAndPanControl ZoomHost { get; private set; }

    #endregion

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

    public FloatingWindowHostZUI()
    {
      DefaultStyleKey = typeof(FloatingWindowHostZUI);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      ZoomHost = base.GetTemplateChild(PART_ZoomHost) as ZoomAndPanControl;
      ZoomHost.ContentScale = ContentScale; //?
    }

  }

}
