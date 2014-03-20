//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: SliderExt.cs
//Version: 20140311

//based on: http://timheuer.com/blog/archive/2008/09/08/customizing-slider-enable-move-mouse-to-point.aspx

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CustomSlider_CS
{
  public class SliderExt : Slider
  {

    #region --- Initialization ---

    public SliderExt() { }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      _HorizontalThumb = GetTemplateChild("HorizontalThumb") as Thumb;

      left = GetTemplateChild("LeftTrack") as FrameworkElement;
      right = GetTemplateChild("RightTrack") as FrameworkElement;

      if (left != null) left.MouseLeftButtonDown += new MouseButtonEventHandler(OnMoveThumbToMouse);
      if (right != null) right.MouseLeftButtonDown += new MouseButtonEventHandler(OnMoveThumbToMouse);
    }

    #endregion

    #region --- Fields ---

    private Thumb _HorizontalThumb;
    private FrameworkElement left;
    private FrameworkElement right;

    #endregion

    #region --- Properties ---



    #endregion

    #region --- Events ---

    private void OnMoveThumbToMouse(object sender, MouseButtonEventArgs e)
    {
      Point p = e.GetPosition(this);

      Value = (p.X - (_HorizontalThumb.ActualWidth / 2)) / (ActualWidth - _HorizontalThumb.ActualWidth) * Maximum;
    }

    #endregion

  }

}
