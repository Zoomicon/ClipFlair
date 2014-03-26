//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ArrayToStringConverter.cs
//Version: 20140326

using System;
using System.Globalization;
using System.Windows.Data;

using Utils.Extensions;

namespace Utils.Converters
{

    [ValueConversion(typeof(Array), typeof(string))]
    public class ArrayToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          return (value as Array).Concatenate(); //calling an extension method which checks for null (should be safe without a null check here, since an extension method is like a static method with an implied 1st parameter for the object reference)
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
          throw new NotImplementedException();
        }

    }

}
