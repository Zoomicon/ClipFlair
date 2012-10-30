//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StringToUriConverter.cs
//Version: 20121030

using System;
using System.Windows.Data;

using ClipFlair.Utils.Extensions;

namespace ClipFlair.Utils.Converters
{

    [ValueConversion(typeof(string), typeof(Uri))]
    public class StringToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            return (input!=null)? input.ToUri() : null;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Uri input = value as Uri;
            if (input == null) return String.Empty;
            else return input.ToString();
        }

    }

}
