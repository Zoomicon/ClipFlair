//Version: 20120712

#if SILVERLIGHT

using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace Converters
{

  public class ReverseConverter : IValueConverter
  {
    IValueConverter converter;

    public ReverseConverter(IValueConverter converter)
    {
      this.converter = converter;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return converter.ConvertBack(value, targetType, parameter, culture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return converter.Convert(value, targetType, parameter, culture);
    }

  }

}

#endif