//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: NullToVisibilityConverter.cs
//Version: 20140130

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Utils.Converters
{

  [ValueConversion(typeof(Visibility), typeof(object))]
  public class NullToVisibilityConverter : IValueConverter
  {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value == null ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (Visibility)value == Visibility.Collapsed ? null : (object)Visibility.Visible;
    }

  }

}
