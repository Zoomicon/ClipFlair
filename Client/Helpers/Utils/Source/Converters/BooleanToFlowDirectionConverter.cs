//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BooleanToFlowDirectionConverter.cs
//Version: 20130606

using System;
using System.Windows;
using System.Windows.Data;


namespace Utils.Converters
{

    [ValueConversion(typeof(bool), typeof(FlowDirection))]
    public class BooleanToFlowDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           if (value is bool)
             return ((bool)value)? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
           else
             return FlowDirection.LeftToRight;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
          if (value is FlowDirection)
            return ((FlowDirection)value == FlowDirection.RightToLeft);
          else
            return false;
        }

    }

}
