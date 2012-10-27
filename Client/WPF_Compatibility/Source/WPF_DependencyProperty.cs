//Filename: WPF_DependencyProperty
//Version: 20120606
//Author: George Birbilis <birbilis@kagi.com>

using System;
using System.Windows;

namespace WPFCompatibility
{

    public static class WPF_DependencyProperty
    {

        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, FrameworkPropertyMetadata typeMetadata)
        {
            DependencyProperty dp = DependencyProperty.Register(name, propertyType, ownerType, typeMetadata);
#if SILVERLIGHT
            FrameworkPropertyMetadata.AssociatePropertyWithCoercionMethod(dp, typeMetadata.CoerceValueCallback);
#endif
            return dp;
        }

    }

}
