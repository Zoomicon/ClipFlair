//Filename: Visual
//Version: 20120606
//Author: George Birbilis <birbilis@kagi.com>

#if SILVERLIGHT

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace WPFCompatibility
{

    public class Visual //: FrameworkElement
    {

        #region Fields
        
        private UIElement _element;

        #endregion
                
        #region Properties

        public UIElement Element {
            get { return _element; }
        }

        public Visual(UIElement element)
        {
            _element = element;
        }

        public bool AllowDrop {
            get { return _element.AllowDrop; }
            set { _element.AllowDrop = value; }
        }

        public CacheMode CacheMode {
            get { return _element.CacheMode; }
            set { _element.CacheMode = value; }
        }

        public Geometry Clip {
            get { return _element.Clip; }
            set { _element.Clip = value; }
        }

        public Size DesiredSize {
            get { return _element.DesiredSize; }
        }

        public Effect Effect {
            get { return _element.Effect; }
            set { _element.Effect = value; }
        }

        public bool IsHitTestVisible {
            get { return _element.IsHitTestVisible; }
            set { _element.IsHitTestVisible = value; }
        }

        public double Opacity {
            get { return _element.Opacity; }
            set { _element.Opacity = value; }
        }

        public Brush OpacityMask {
            get { return _element.OpacityMask; }
            set { _element.OpacityMask = value; }
        }

        public Projection Projection {
            get { return _element.Projection; }
            set { _element.Projection = value; }
        }

        public Size RenderSize {
            get { return _element.RenderSize; }
        }

        public Transform RenderTransform {
            get { return _element.RenderTransform; }
            set { _element.RenderTransform = value; }
        }

        public Point RenderTransformOrigin {
            get { return _element.RenderTransformOrigin; }
            set { _element.RenderTransformOrigin = value; }
        }

        public bool UseLayoutRounding {
            get { return _element.UseLayoutRounding; }
            set { _element.UseLayoutRounding = value; }
        }
        
        public Visibility Visibility {
            get { return _element.Visibility; }
            set { _element.Visibility = value; }
        }

        #endregion

        #region Methods

        public GeneralTransform TransformToAncestor(UIElement visual)
        {
            return _element.TransformToVisual(visual);
        }

        public GeneralTransform TransformToAncestor(Visual visual)
        //Silverlight doesn't have TransformToAncestor (neight TransformToDescendant), so using TransformToVisual which seems to be more general
        {
            return _element.TransformToVisual(visual.Element);
        }

        //TODO: add the rest of UIElement methods here

        #endregion

    }
    
}

#endif