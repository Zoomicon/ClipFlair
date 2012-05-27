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
using WPFCompatibility;

namespace ZoomAndPan
{
    /// <summary>
    /// This is an extension to the ZoomAndPanControl class that implements
    /// the IScrollInfo interface "MakeVisible" method for Silverlight.
    ///     
    /// </summary>   
    public partial class ZoomAndPanControl
    {
                
        #region Methods

        /// <summary>
        /// Bring the specified rectangle to view.
        /// </summary>
        public Rect MakeVisible(UIElement visual, Rect rectangle) //Silverlight
        {
            return MakeVisible(new Visual(visual), rectangle);
        }

        #endregion
        
    }

}

