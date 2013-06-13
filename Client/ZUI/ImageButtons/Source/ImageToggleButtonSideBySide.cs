//Version: 20130314

using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ImageButtons
{
  public class ImageToggleButtonSideBySide: ToggleButton
  {

    public ImageToggleButtonSideBySide()
    {
      this.DefaultStyleKey = typeof(ImageToggleButtonSideBySide);
    }

    #region Image

    public ImageSource Image
    {
      get { return (ImageSource)GetValue(ImageProperty); }
      set { SetValue(ImageProperty, value); }
    }

    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register("Image",
                                    typeof(ImageSource),
                                    typeof(ImageToggleButtonSideBySide),
                                    new PropertyMetadata(null));

    #endregion

    #region ImageHover

    public ImageSource ImageHover
    {
      get { return (ImageSource)GetValue(ImageHoverProperty); }
      set { SetValue(ImageHoverProperty, value); }
    }

    public static readonly DependencyProperty ImageHoverProperty =
        DependencyProperty.Register("ImageHover",
                                    typeof(ImageSource),
                                    typeof(ImageToggleButtonSideBySide),
                                    new PropertyMetadata(null));

    #endregion

    #region ImageChecked

    public ImageSource ImageChecked
    {
      get { return (ImageSource)GetValue(ImageCheckedProperty); }
      set { SetValue(ImageCheckedProperty, value); }
    }

    public static readonly DependencyProperty ImageCheckedProperty =
        DependencyProperty.Register("ImageChecked",
                                    typeof(ImageSource),
                                    typeof(ImageToggleButtonSideBySide),
                                    new PropertyMetadata(null));

    #endregion

    #region ImageCheckedHover

    public ImageSource ImageCheckedHover
    {
      get { return (ImageSource)GetValue(ImageCheckedHoverProperty); }
      set { SetValue(ImageCheckedHoverProperty, value); }
    }

    public static readonly DependencyProperty ImageCheckedHoverProperty =
        DependencyProperty.Register("ImageCheckedHover",
                                    typeof(ImageSource),
                                    typeof(ImageToggleButtonSideBySide),
                                    new PropertyMetadata(null));

    #endregion

    #region ImageSelector

    public ImageSource ImageSelector
    {
      get { return (ImageSource)GetValue(ImageSelectorProperty); }
      set { SetValue(ImageSelectorProperty, value); }
    }

    public static readonly DependencyProperty ImageSelectorProperty =
        DependencyProperty.Register("ImageSelector",
                                    typeof(ImageSource),
                                    typeof(ImageToggleButtonSideBySide),
                                    new PropertyMetadata(null));

    #endregion

  }

}
