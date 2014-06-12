//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: SliderExt.cs
//Version: 20140612

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SliderExtLib
{
  public class SliderExt : Slider
  {

    public SliderExt()
    {
      //peek into mouse button events (even handled ones)
      AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
      AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp), true);
    }

    #region --- Fields ---

    protected bool dragging;

    #endregion

    #region --- Properties ---
    
    public 
      #if !SILVERLIGHT
      new
      #endif
      bool IsMoveToPointEnabled //WPF has such property (hiding it on purpose), but doesn't implement move to point if you click and drag on the track instead of the thumb
    {
      get { return (bool)GetValue(IsMoveToPointEnabledProperty); }
      set { SetValue(IsMoveToPointEnabledProperty, value); }
    }

    public static readonly 
      #if !SILVERLIGHT
      new
      #endif
      DependencyProperty IsMoveToPointEnabledProperty = 
                                        DependencyProperty.RegisterAttached("IsMoveToPointEnabled",
                                          typeof(bool), typeof(SliderExt), new PropertyMetadata(false));
    
    #endregion

    #region --- Methods ---

    private void MoveThumbToPoint(Point p)
    {
      if (Orientation == Orientation.Horizontal)
        Value = p.X / ActualWidth * Maximum;
      else //if (Orientation == Orientation.Vertical)
        Value = p.Y / ActualHeight * Maximum;
    }

    #endregion

    #region --- Events ---

    private void OnMouseLeftButtonDown(object source, MouseButtonEventArgs e)
    {
      if (IsMoveToPointEnabled)
      {
        MoveThumbToPoint(e.GetPosition(this));
        CaptureMouse(); //must do before setting dragging=true, since WPF seems to be always calling OnLostMouseCapture on all controls, even if they didn't have the mouse capture
        dragging = true; //always set, we might not make it to capture the mouse
       }
    }

    private void OnMouseLeftButtonUp(object source, MouseButtonEventArgs e)
    {
      ReleaseMouseCapture();
      dragging = false; //always clear, in case we never had the mouse capture
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      if (dragging && IsMoveToPointEnabled)
        MoveThumbToPoint(e.GetPosition(this));
    }

    protected override void OnLostMouseCapture(MouseEventArgs e)
    {
      base.OnLostMouseCapture(e);

      dragging = false; //set dragging to false whatever the value of IsMoveToPointEnabled (may have changed while dragging)
    }
    
    #endregion

  }

}
