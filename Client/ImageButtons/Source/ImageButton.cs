//Version: 20130314
//based on sample from: http://loekvandenouweland.com/index.php/2011/02/silverlight-templated-image-button-with-two-images/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ImageButtons
{
  public class ImageButton : Button
  {

    public ImageButton()
    {
      this.DefaultStyleKey = typeof(ImageButton);
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
                                    typeof(ImageButton),
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
                                    typeof(ImageButton),
                                    new PropertyMetadata(null));

    #endregion

  }

}
