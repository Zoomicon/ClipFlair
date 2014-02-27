//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: UrlEncodedToDecodedStringConverter.cs
//Version: 20140227

using System;
using System.Windows.Data;

namespace Utils.Converters
{

    [ValueConversion(typeof(string), typeof(string))]
    public class UrlEncodedToDecodedStringConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        if (value is string)
          return UrlDecode((string)value);
        else
          return "";
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        if (value is string)
          return UrlEncode((string)value);
        else
          return "";
      }

      //--- copied from http://weblog.west-wind.com/posts/2009/Feb/05/Html-and-Uri-String-Encoding-without-SystemWeb

            /// <summary>
      /// UrlEncodes a string without the requirement for System.Web
      /// </summary>
      /// <param name="String"></param>
      /// <returns></returns>
      // [Obsolete("Use System.Uri.EscapeDataString instead")]
      public static string UrlEncode(string text)
      {
          // Sytem.Uri provides reliable parsing
          return System.Uri.EscapeDataString(text);
      }

      /// <summary>
      /// UrlDecodes a string without requiring System.Web
      /// </summary>
      /// <param name="text">String to decode.</param>
      /// <returns>decoded string</returns>
      public static string UrlDecode(string text)
      {
          // pre-process for + sign space formatting since System.Uri doesn't handle it
          // plus literals are encoded as %2b normally so this should be safe
          text = text.Replace("+", " ");
          return System.Uri.UnescapeDataString(text);
      }

    }

}
