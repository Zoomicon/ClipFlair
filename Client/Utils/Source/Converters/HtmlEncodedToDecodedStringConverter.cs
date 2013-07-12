//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: HtmlEncodedToDecodedStringConverter.cs
//Version: 20130704

using System;
using System.Windows.Data;

#if SILVERLIGHT
using System.Windows.Browser;
#else
using System.Web;
#endif

namespace Utils.Converters
{

    [ValueConversion(typeof(string), typeof(string))]
    public class HtmlEncodedToDecodedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           if (value is string)
             return HttpUtility.HtmlDecode((string)value);
           else
             return"";
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          if (value is string)
            return HttpUtility.HtmlEncode((string)value);
          else
            return "";
        }

    }

}
