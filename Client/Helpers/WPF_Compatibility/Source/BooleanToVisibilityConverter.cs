//Version: 20120712

#if SILVERLIGHT

using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace System.Windows.Controls
{

  [ValueConversion(typeof(bool), typeof(Visibility))]
  public class BooleanToVisibilityConverter : IValueConverter
  {
    /// <summary>
    /// Converts a boolean value to the display state of an element.
    /// </summary>
    /// <param name="value">The source data being passed to the target.</param>
    /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
    /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
    /// <param name="culture">The culture of the conversion.</param>
    /// <returns>
    /// The value to be passed to the target dependency property.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      return (bool)value ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      return (Visibility)value == Visibility.Visible ? true : false;
    }

  }

}

#endif