//Version: 20130309
//based on sample from: http://loekvandenouweland.com/index.php/2011/02/silverlight-templated-image-button-with-two-images/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ImageButton
{
    public class ImageButton : Button
    {

        public ImageSource ImageNormal
        {
            get { return (ImageSource)GetValue(ImageNormalProperty); }
            set { SetValue(ImageNormalProperty, value); }
        }

        public static readonly DependencyProperty ImageNormalProperty =
            DependencyProperty.Register("ImageNormal",
                                        typeof(ImageSource),
                                        typeof(ImageButton),
                                        new PropertyMetadata(null));


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


        public ImageButton()
        {
            this.DefaultStyleKey = typeof(ImageButton);
        }
    }
}
