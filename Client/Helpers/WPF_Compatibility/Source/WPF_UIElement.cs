//Filename: WPF_UIElement
//Version: 20131224
//Author: George Birbilis <birbilis@kagi.com>

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WPFCompatibility
{

    public static class WPF_UIElement
    {

#if SILVERLIGHT

        public static bool IsAncestorOf(this UIElement target, Visual visual)
        {
            return IsAncestorOf(target, visual.Element);
        }

        /// <summary>
        /// Check if current UIElement is ancestor of the given UIElement
        /// </summary>
        /// <param name="self">The current UIElement.</param>
        /// <param name="rect">The UIElement to check.</param>
        /// <returns><c>true</c> if the current UIElement is ancestor of the specified
        ///          UIElement; otherwise, <c>false</c>.</returns>
        public static bool IsAncestorOf(this UIElement target, UIElement element)
        //based on: http://blogs.telerik.com/blogs/posts/08-07-24/adding-mouse-wheel-support-in-silverlight-the-easy-way.aspx (if it doesn't work try modifying code from http://www.hardcodet.net/2008/02/find-wpf-parent)
        {
            if (target == element) return true;

            UIElement parent = VisualTreeHelper.GetParent(element) as UIElement;

            while (parent != null)
            {
                if (parent == target) return true;
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }

            return false;
        }

    /// <summary>
    /// Retrieves a set of objects that are located within a specified point of an object's coordinate space.
    /// </summary>
    /// <param name="subtree">The object to search within.</param>
    /// <param name="intersectingPoint">The point to use as the determination point.</param>
    /// <returns>
    /// An enumerable set of System.Windows.UIElement objects that are determined
    /// to be located in the visual tree composition at the specified point and within
    /// the specified subtee.
    /// </returns>
    public static IEnumerable<UIElement> FindElementsInCoordinates(this UIElement subtree, Point intersectingPoint) //copied from SilverFlow (FloatingWindow) "ControlExtensions.cs" file
    {
      return VisualTreeHelper.FindElementsInHostCoordinates(intersectingPoint, subtree);
    }


#endif

    }

}
