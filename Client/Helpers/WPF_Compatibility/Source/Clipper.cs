//Filename: Clip.cs
//Version: 20140902

//based on: http://www.scottlogic.com/blog/2009/05/12/silverlight-clipClipToBounds-can-i-clip-it-yes-you-can.html

using System.Windows;
using System.Windows.Media;

namespace WPF_Compatibility
{
  public static class Clipper
  {

    public const string PROPERTY_CLIPTOBOUNDS = "ClipToBounds";

    /// <summary>
    /// Identifies the ClipToBounds Dependency Property.
    /// <summary>
    public static readonly DependencyProperty ClipToBoundsProperty =
      DependencyProperty.RegisterAttached(PROPERTY_CLIPTOBOUNDS, typeof(bool),
        typeof(Clipper), new PropertyMetadata(false, OnClipToBoundsPropertyChanged));

    private static void OnClipToBoundsPropertyChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement fe = d as FrameworkElement;
      if (fe == null) return;

      UpdateClip(fe);

      if ((bool)e.NewValue)
      {
        // whenever the element which this property is attached to is loaded
        // or re-sizes, we need to update its clipping geometry
        fe.Loaded += fe_Loaded;
        fe.SizeChanged += fe_SizeChanged;
      }
      else
      {
        fe.Loaded -= fe_Loaded;
        fe.SizeChanged -= fe_SizeChanged;
      }
    }

    #region Helper methods

    public static bool GetClipToBounds(DependencyObject depObj)
    {
      return (bool)depObj.GetValue(ClipToBoundsProperty);
    }

    public static void SetClipToBounds(DependencyObject depObj, bool clipClipToBounds)
    {
      depObj.SetValue(ClipToBoundsProperty, clipClipToBounds);
    }

    #endregion

    #region Clipping logic

    /// <summary>
    /// Creates a rectangular clipping geometry which matches the geometry of the passed element, or removes clip, based on ClipToBounds attached property
    /// </summary>
    private static void UpdateClip(FrameworkElement fe)
    {
      if (GetClipToBounds(fe))
        fe.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, fe.Width, fe.Height) }; //don't use ActualWidth/ActualHeight since they're affected by Scale
      else
        fe.Clip = null;
    }

    static void fe_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      UpdateClip(sender as FrameworkElement);
    }

    static void fe_Loaded(object sender, RoutedEventArgs e)
    {
      UpdateClip(sender as FrameworkElement);
    }


    #endregion

  }
}
