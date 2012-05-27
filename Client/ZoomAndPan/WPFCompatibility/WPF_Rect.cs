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

    public static class WPF_Rect
    {

        /// <summary>
        /// Indicates whether the current rectangle contains the specified rectangle,
        /// by checking for containment of the specified rectangle's top-left and
        /// bottom-right points.
        /// </summary>
        /// <param name="self">The current rectangle.</param>
        /// <param name="rect">The rectangle to check.</param>
        /// <returns><c>true</c> if the current rectangle contains the specified
        ///          rectangle; otherwise, <c>false</c>.</returns>
        public static bool Contains(this Rect self, Rect rect)
        {
            return self.Contains(new Point(rect.Left, rect.Top)) && self.Contains(new Point(rect.Right, rect.Bottom));
        }

    }
}
