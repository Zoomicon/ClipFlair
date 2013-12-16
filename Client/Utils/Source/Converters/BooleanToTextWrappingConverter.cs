//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BooleanToTextWrappingConverter.cs
//Version: 20131216

using System;
using System.Windows;
using System.Windows.Data;


namespace Utils.Converters
{

    [ValueConversion(typeof(bool), typeof(TextWrapping))]
    public class BooleanToTextWrappingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           if (value is bool)
             return ((bool)value)? TextWrapping.Wrap : TextWrapping.NoWrap;
           else
             return TextWrapping.NoWrap;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          if (value is TextWrapping)
            return ((TextWrapping)value == TextWrapping.Wrap);
          else
            return false;
        }

    }

}
