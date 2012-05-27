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

namespace ZoomAndPan.WPFCompatibility
{
    public static class WPF_ScaleTransform
    {

      public static ScaleTransform newScaleTransform(double scaleX, double scaleY) //unfortunately there are is no extension method mechanism for contructors in C# yet
        {
                    ScaleTransform result = new ScaleTransform();
                    result.ScaleX = scaleX;
                    result.ScaleY = scaleY;
                    return result;
        }

    }

}
