//Filename: WPF_DependencyObject
//Version: 20120606
//Author: George Birbilis <birbilis@kagi.com>

using System.Windows;

namespace WPF_Compatibility
{

    public static class WPF_DependencyObject
    {

#if SILVERLIGHT

        public static void CoerceValue(this DependencyObject target, DependencyProperty dp)
        {
            FrameworkPropertyMetadata.DoExplicitCoercion(target, dp);
        }

#endif

    }

}
