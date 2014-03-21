//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: HtmlEncodedToDecodedStringConverter.cs
//Version: 20140321

using System;
using System.Windows.Data;

#if SILVERLIGHT && !WINDOWS_PHONE
using System.Windows.Browser;
#else
using System.Net;
#endif

namespace Utils.Converters
{

    [ValueConversion(typeof(string), typeof(string))]
    public class HtmlEncodedToDecodedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          if (value is string)
            #if SILVERLIGHT
            return HttpUtility.HtmlDecode((string)value);
            #else
            return WebUtility.HtmlDecode((string)value); //at .NET 4 Client Profile you have to use System.Net.WebUtility.HtmlDecode, System.Web.HttpUtility is only at full .NET 4
            #endif
           else
             return"";
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          if (value is string)
            #if SILVERLIGHT
            return HttpUtility.HtmlEncode((string)value);
            #else
            return WebUtility.HtmlEncode((string)value); //at .NET 4 Client Profile you have to use System.Net.WebUtility.HtmlEncode, System.Web.HttpUtility is only at full .NET 4
            #endif
          else
            return "";
        }

    }

}
