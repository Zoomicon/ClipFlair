//Version: 20120710

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SilverFlow.Controls.Extensions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;
using WPFCompatibility;

namespace SilverFlow.Controls.Helpers
{
    /// <summary>
    /// Bitmap helper.
    /// </summary>
    public class BitmapHelper : FrameworkElement, IValueConverter, IBitmapHelper
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
         // try
          {
            return RenderVisual((FrameworkElement)value, Width, Height);
          }
         /* catch //e.g. if value is not a FrameworkElement
          {
            return new BitmapImage();
          }*/
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
          throw new NotImplementedException();
        }

        /// <summary>
        /// Renders the visual element and returns a bitmap, containing bitmap image of the element.
        /// </summary>
        /// <param name="element">The visual element.</param>
        /// <param name="imageWidth">Image width.</param>
        /// <param name="imageHeight">Image height.</param>
        /// <returns>Bitmap image of the element.</returns>
        public ImageSource RenderVisual(FrameworkElement element, double imageWidth, double imageHeight)
        {
            int width = element.Width.IsNotSet() ? (int)element.ActualWidth : (int)element.Width;
            int height = element.Height.IsNotSet() ? (int)element.ActualHeight : (int)element.Height;

#if SILVERLIGHT

            ScaleTransform transform = null;

            // If the element is an image - do not scale it
            if (!(element is Image))
            {

#endif
                // Scale down the element to fit it into the window's thumbnail
                double scaleX = imageWidth / width;
                double scaleY = imageHeight / height;
                double minScale = Math.Min(scaleX, scaleY);

                if (minScale < 1)
                {

#if SILVERLIGHT                  
                    transform = new ScaleTransform { ScaleX = minScale, ScaleY = minScale };
#endif
                    width = (int)(width * minScale);
                    height = (int)(height * minScale);
                }

#if SILVERLIGHT
            }

            WriteableBitmap bitmap = new WriteableBitmap(width, height);
            bitmap.Render(element, transform);
            bitmap.Invalidate();

#else

            // Get current dpi
            PresentationSource presentationSource = PresentationSource.FromVisual(Application.Current.MainWindow);
            Matrix m = presentationSource.CompositionTarget.TransformToDevice;
            double dpiX = m.M11 * 96;
            double dpiY = m.M22 * 96;

            RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, dpiX, dpiY, PixelFormats.Default);

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(element);
                drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Size(width, height)));
            }

            // Draw the element
            bitmap.Render(drawingVisual);

#endif

            return bitmap;
        }
    }
}
