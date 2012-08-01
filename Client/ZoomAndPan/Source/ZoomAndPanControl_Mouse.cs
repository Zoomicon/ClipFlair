//Filename: ZoomAndPanControl_Mouse.cs
//Version: 20120728

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace ZoomAndPan
{
    /// <summary>
    /// This is an extension to the ZoomAndPanControl class that implements
    /// default mouse handling properties and functions.
    /// 
    /// </summary>   
    public partial class ZoomAndPanControl
    {

      #region Mouse-related Fields

      private bool isDefaultMouseHandling = false;
      
      /// <summary>
      /// Parts needed for drag zoom rectangle, initialized at "OnApplyTemplate" method
      /// </summary>
      private Canvas dragZoomCanvas;
      private Border dragZoomBorder;

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
      /// Records which mouse button clicked during mouse dragging.
      /// </summary>
      private MouseButton mouseButtonDown; //update WPF Compatibility with System.Windows.Input.MouseButton for SL and System.Windows.Browser.MouseButtons for WPF

      /// <summary>
      /// Saves the previous zoom rectangle, pressing the backspace key jumps back to this zoom rectangle.
      /// </summary>
      private Rect prevZoomRect;

      /// <summary>
      /// Save the previous content scale, pressing the backspace key jumps back to this scale.
      /// </summary>
      private double prevZoomScale;

      /// <summary>
      /// Set to 'true' when the previous zoom rect is saved.
      /// </summary>
      private bool prevZoomRectSet = false; //TODO: check where that should be used

      #endregion

      #region Mouse-related Properties

      public bool IsDefaultMouseHandling
      {
        get {
          return isDefaultMouseHandling;
        }

        set {
          if (isDefaultMouseHandling == value) return; //!!! C# compiler bug? lets you use = instead of == here
          isDefaultMouseHandling = value;
/*
          if (value)
          {
#if SILVERLIGHT
            AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(zoomAndPanControl_MouseDown), false); //this handles the double-click to
            MouseRightButtonDown += new MouseButtonEventHandler(zoomAndPanControl_MouseDown);
            MouseLeftButtonUp += new MouseButtonEventHandler(zoomAndPanControl_MouseUp);
            MouseRightButtonUp += new MouseButtonEventHandler(zoomAndPanControl_MouseUp);
#else
            MouseDoubleClick += new MouseButtonEventHandler(zoomAndPanControl_MouseDoubleClick);
            MouseDown += new MouseButtonEventHandler(zoomAndPanControl_MouseDown);
            MouseUp += new MouseButtonEventHandler(zoomAndPanControl_MouseUp);
#endif
            MouseMove += new MouseEventHandler(zoomAndPanControl_MouseMove);
            MouseWheel += new MouseWheelEventHandler(zoomAndPanControl_MouseWheel);
          } 
          else
          {
#if SILVERLIGHT
            MouseLeftButtonDown -= new MouseButtonEventHandler(zoomAndPanControl_MouseDown);
            MouseRightButtonDown -= new MouseButtonEventHandler(zoomAndPanControl_MouseDown);
            MouseLeftButtonUp -= new MouseButtonEventHandler(zoomAndPanControl_MouseUp);
            MouseRightButtonUp -= new MouseButtonEventHandler(zoomAndPanControl_MouseUp);
#else
            MouseDown -= new MouseButtonEventHandler(zoomAndPanControl_MouseDown);
            MouseUp -= new MouseButtonEventHandler(zoomAndPanControl_MouseUp);
#endif
            MouseMove -= new MouseEventHandler(zoomAndPanControl_MouseMove);
            MouseWheel -= new MouseWheelEventHandler(zoomAndPanControl_MouseWheel);  
          }
 */
        }

      }

      #endregion

      #region Mouse-related Event Handlers
/*
      /// <summary>
      /// Event raised when the user has double clicked in the zoom and pan control. (WPF only)
      /// </summary>
      private void zoomAndPanControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
        OnMouseDoubleClick(e);
      }

      /// <summary>
      /// Event raised on mouse down in the ZoomAndPanControl.
      /// </summary>
      private void zoomAndPanControl_MouseDown(object sender, MouseButtonEventArgs e)
      {
        OnMouseDown(e, false);
      }

      /// <summary>
      /// Event raised on mouse up in the ZoomAndPanControl.
      /// </summary>
      private void zoomAndPanControl_MouseUp(object sender, MouseButtonEventArgs e)
      {
#if SILVERLIGHT
        if (e.ClickCount == 2) { OnMouseDoubleClick(e); return; }
#endif
        OnMouseUp(e, false);
      }

      /// <summary>
      /// Event raised on mouse move in the ZoomAndPanControl.
      /// </summary>
      private void zoomAndPanControl_MouseMove(object sender, MouseEventArgs e)
      {
        OnMouseMove(e);
      }

      private void zoomAndPanControl_MouseDownRight(object sender, MouseButtonEventArgs e)
      {
#if SILVERLIGHT
        if (e.ClickCount == 2) { OnMouseDoubleClick(e); return; }
#endif
        OnMouseDown(e, true);
      }

      /// <summary>
      /// Event raised by rotating the mouse wheel
      /// </summary>
      private void zoomAndPanControl_MouseWheel(object sender, MouseWheelEventArgs e)
      {
        OnMouseWheel(e);
      }
*/
      #endregion

      #region Mouse-related Methods

      protected 
#if SILVERLIGHT
        virtual
#else 
        override
#endif
      void OnMouseDoubleClick(MouseButtonEventArgs e)
      {
#if !SILVERLIGHT
        base.OnMouseDoubleClick(e);
#endif
        if (e.Handled) return;

        if ((Keyboard.Modifiers & ModifierKeys.Shift) == 0)
        {
          Point doubleClickPoint = e.GetPosition(content);
          AnimatedSnapTo(doubleClickPoint);
        }
        e.Handled = true;
      }
      
      protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
      {
        base.OnMouseLeftButtonDown(e);
        if(!e.Handled) OnMouseDown(e, MouseButton.Left);
        e.Handled = true;
      }

      protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
      {
        base.OnMouseRightButtonDown(e);
        if (!e.Handled) OnMouseDown(e, MouseButton.Right);
        e.Handled = true;
      }
      
      protected virtual void OnMouseDown(MouseButtonEventArgs e, MouseButton changedButton)
      {
        Focus(); //had content.Focus
#if !SILVERLIGHT
        Keyboard.Focus(content);
#endif

        mouseButtonDown = changedButton; //at WPF one could also use "e.ChangedButton"

        origZoomAndPanControlMouseDownPoint = e.GetPosition(this);
        origContentMouseDownPoint = e.GetPosition(content);

        if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0 
            &&
            (mouseButtonDown == MouseButton.Left ||
             mouseButtonDown == MouseButton.Right))
        {
          // Shift + left- or right-down initiates zooming mode.
          mouseHandlingMode = MouseHandlingMode.Zooming;
        }
        else if (mouseButtonDown == MouseButton.Left)
        {
          // Just a plain old left-down initiates panning mode.
          mouseHandlingMode = MouseHandlingMode.Panning;
        }

        if (mouseHandlingMode != MouseHandlingMode.None)
        {
          // Capture the mouse so that we eventually receive the mouse up event.
          this.CaptureMouse();
          //e.Handled = true; //always handling the events at the caller anyway
        }
      }

      protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
      {
        base.OnMouseLeftButtonUp(e);
        if (e.Handled) return;

#if SILVERLIGHT
        if (e.ClickCount == 2) { OnMouseDoubleClick(e); return; }
#endif
        OnMouseUp(e, MouseButton.Left);
        e.Handled = true;
      }

      protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
      {
        base.OnMouseRightButtonUp(e);
        if (e.Handled) return;

#if SILVERLIGHT
        if (e.ClickCount == 2) { OnMouseDoubleClick(e); return; } //this will also do "e.Handled=true"
#endif
        OnMouseUp(e, MouseButton.Right);
        e.Handled = true;
      }

      protected virtual void OnMouseUp(MouseButtonEventArgs e, MouseButton changedButton)
      {

        if (mouseHandlingMode != MouseHandlingMode.None)
        {
          if (mouseHandlingMode == MouseHandlingMode.Zooming)
          {
            if (mouseButtonDown == MouseButton.Left && changedButton == MouseButton.Left) //at WPF one could also use "e.ChangedButton"
            {
              // Shift + left-click zooms in on the content.
              ZoomIn(origContentMouseDownPoint);
            }
            else if (mouseButtonDown == MouseButton.Right && changedButton == MouseButton.Right) //at WPF one could also use "e.ChangedButton"
            {
              // Shift + left-click zooms out from the content.
              ZoomOut(origContentMouseDownPoint);
            }
          }
          else if (mouseHandlingMode == MouseHandlingMode.DragZooming)
          {
            // When drag-zooming has finished we zoom in on the rectangle that was highlighted by the user.
            ApplyDragZoomRect();
          }

          ReleaseMouseCapture();
          mouseHandlingMode = MouseHandlingMode.None;
          //e.Handled = true; //always handling the events at the caller anyway
        }
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        base.OnMouseMove(e);

        if (mouseHandlingMode == MouseHandlingMode.Panning)
        {
          //
          // The user is left-dragging the mouse.
          // Pan the viewport by the appropriate amount.
          //
          Point curContentMousePoint = e.GetPosition(content);
          //Vector dragOffset = curContentMousePoint - origContentMouseDownPoint; //TODO: add Mono's System.Windows.Vector to WPF compatibility and add WPF_Point class with extension method for add and subtract to return Vector (get implementation from Mono)

          ContentOffsetX -= curContentMousePoint.X - origContentMouseDownPoint.X; //dragOffset.X
          ContentOffsetY -= curContentMousePoint.Y - origContentMouseDownPoint.Y; //dragOffset.Y

          #if !SILVERLIGHT
          e.Handled = true;
          #endif
        }
        else if (mouseHandlingMode == MouseHandlingMode.Zooming)
        {
          Point curZoomAndPanControlMousePoint = e.GetPosition(this);
          //Vector dragOffset = curZoomAndPanControlMousePoint - origZoomAndPanControlMouseDownPoint;
          double dragThreshold = 10;
          if (mouseButtonDown == MouseButton.Left &&
              (Math.Abs(curZoomAndPanControlMousePoint.X - origZoomAndPanControlMouseDownPoint.X /*dragOffset.X*/) > dragThreshold ||
               Math.Abs(curZoomAndPanControlMousePoint.Y - origZoomAndPanControlMouseDownPoint.Y /*dragOffset.Y*/) > dragThreshold))
          {
            //
            // When Shift + left-down zooming mode and the user drags beyond the drag threshold,
            // initiate drag zooming mode where the user can drag out a rectangle to select the area
            // to zoom in on.
            //
            mouseHandlingMode = MouseHandlingMode.DragZooming;
            Point curContentMousePoint = e.GetPosition(content);
            InitDragZoomRect(origContentMouseDownPoint, curContentMousePoint);
          }

          #if !SILVERLIGHT
          e.Handled = true;
          #endif
        }
        else if (mouseHandlingMode == MouseHandlingMode.DragZooming)
        {
          //
          // When in drag zooming mode continously update the position of the rectangle
          // that the user is dragging out.
          //
          Point curContentMousePoint = e.GetPosition(content);
          SetDragZoomRect(origContentMouseDownPoint, curContentMousePoint);

          #if !SILVERLIGHT
          e.Handled = true;
          #endif
        }
      }

      protected override void OnMouseWheel(MouseWheelEventArgs e)
      {
        base.OnMouseWheel(e);
        if (e.Handled) return;
        
        if (e.Delta > 0)
        {
          Point curContentMousePoint = e.GetPosition(content);
          ZoomIn(curContentMousePoint);
        }
        else if (e.Delta < 0)
        {
          Point curContentMousePoint = e.GetPosition(content);
          ZoomOut(curContentMousePoint);
        }

        e.Handled = true;
      }

      #endregion

      #region Mouse Helper methods

      /// <summary>
      /// Zoom out
      /// </summary>
      private void ZoomOut(Point contentZoomCenter)
      {
        ZoomAboutPoint(ContentScale - 0.2, contentZoomCenter);
      }

      /// <summary>
      /// Zoom in
      /// </summary>
      private void ZoomIn(Point contentZoomCenter)
      {
        ZoomAboutPoint(ContentScale + 0.2, contentZoomCenter);
      }

      /// <summary>
      /// Initialise the rectangle that the use is dragging out.
      /// </summary>
      private void InitDragZoomRect(Point pt1, Point pt2)
      {
        SetDragZoomRect(pt1, pt2);

        dragZoomCanvas.Visibility = Visibility.Visible;
        dragZoomBorder.Opacity = 0.5;
      }

      /// <summary>
      /// Update the position and size of the rectangle that user is dragging out.
      /// </summary>
      private void SetDragZoomRect(Point pt1, Point pt2)
      {
        double x, y, width, height;

        //
        // Deterine x,y,width and height of the rect inverting the points if necessary.
        // 

        if (pt2.X < pt1.X)
        {
          x = pt2.X;
          width = pt1.X - pt2.X;
        }
        else
        {
          x = pt1.X;
          width = pt2.X - pt1.X;
        }

        if (pt2.Y < pt1.Y)
        {
          y = pt2.Y;
          height = pt1.Y - pt2.Y;
        }
        else
        {
          y = pt1.Y;
          height = pt2.Y - pt1.Y;
        }

        //
        // Update the coordinates of the rectangle that is being dragged out by the user.
        // The we offset and rescale to convert from content coordinates.
        //
        Canvas.SetLeft(dragZoomBorder, x); //assuming dragZoomBorder is inside a Canvas
        Canvas.SetTop(dragZoomBorder, y);
        dragZoomBorder.Width = width;
        dragZoomBorder.Height = height;
      }

      /// <summary>
      /// When the user has finished dragging out the rectangle the zoom operation is applied.
      /// </summary>
      private void ApplyDragZoomRect()
      {
        //
        // Record the previous zoom level, so that we can jump back to it when the backspace key is pressed.
        //
        SavePrevZoomRect();

        //
        // Retreive the rectangle that the user draggged out and zoom in on it.
        //
        double contentX = Canvas.GetLeft(dragZoomBorder);
        double contentY = Canvas.GetTop(dragZoomBorder);
        double contentWidth = dragZoomBorder.Width;
        double contentHeight = dragZoomBorder.Height;
        AnimatedZoomTo(new Rect(contentX, contentY, contentWidth, contentHeight));

        FadeOutDragZoomRect();
      }

      //
      // Fade out the drag zoom rectangle.
      //
      private void FadeOutDragZoomRect()
      {
        AnimationHelper.StartAnimation(dragZoomBorder, Border.OpacityProperty, 0.0, 0.1,
            delegate(object sender, EventArgs e)
            {
              dragZoomCanvas.Visibility = Visibility.Collapsed;
            });
      }

      //
      // Record the previous zoom level, so that we can jump back to it when the backspace key is pressed.
      //
      private void SavePrevZoomRect()
      {
        prevZoomRect = new Rect(ContentOffsetX, ContentOffsetY, ContentViewportWidth, ContentViewportHeight);
        prevZoomScale = ContentScale;
        prevZoomRectSet = true;
      }

      /// <summary>
      /// Clear the memory of the previous zoom level.
      /// </summary>
      private void ClearPrevZoomRect()
      {
        prevZoomRectSet = false;
      }

      #endregion

    }
}

