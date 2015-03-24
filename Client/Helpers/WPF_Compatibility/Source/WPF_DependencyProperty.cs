//Filename: WPF_DependencyProperty
//Version: 20130131
//Author: George Birbilis <zoomicon.com>

using System;
using System.Windows;

namespace WPF_Compatibility
{

    public static class WPF_DependencyProperty
    {

        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, FrameworkPropertyMetadata typeMetadata)
        {
            DependencyProperty dp = DependencyProperty.Register(name, propertyType, ownerType, typeMetadata);
#if SILVERLIGHT
            if (typeMetadata != null && typeMetadata.CoerceValueCallback != null) 
              FrameworkPropertyMetadata.AssociatePropertyWithCoercionMethod(dp, typeMetadata.CoerceValueCallback);
#endif
            return dp;
        }

    }

}
