//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ReverseConverter.cs
//Version: 20121030


#if SILVERLIGHT

using System;
using System.Globalization;
using System.Windows.Data;

namespace Utils.Converters
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