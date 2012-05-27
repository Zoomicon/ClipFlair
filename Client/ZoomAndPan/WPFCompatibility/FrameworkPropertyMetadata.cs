using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPFCompatibility
{
    public class FrameworkPropertyMetadata : PropertyMetadata
    {

        public FrameworkPropertyMetadata(object defaultValue) : base(defaultValue)
        {
        }

        public FrameworkPropertyMetadata(PropertyChangedCallback propertyChangedCallback) : base(propertyChangedCallback)
        {
        }

        public FrameworkPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback) : base(defaultValue,propertyChangedCallback)
        {
        }

    }
}
