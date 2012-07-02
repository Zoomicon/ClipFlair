using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

using SilverFlow.Controls;
using ZoomAndPan;

namespace FloatingWindowZUI
{

    [TemplatePart(Name = PART_Root, Type = typeof(Grid))]
    [TemplatePart(Name = PART_ContentRoot, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_HostCanvas, Type = typeof(Canvas))]
    [TemplatePart(Name = PART_ModalCanvas, Type = typeof(Canvas))]
    [TemplatePart(Name = PART_IconBarContainer, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_Overlay, Type = typeof(Grid))]
    [TemplatePart(Name = PART_IconBar, Type = typeof(IconBar))]
    [TemplatePart(Name = PART_BottomBar, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_BootstrapButton, Type = typeof(BootstrapButton))]
    [TemplatePart(Name = PART_BarContent, Type = typeof(ContentControl))]
    [TemplateVisualState(Name = VSMSTATE_VisibleOverlay, GroupName = VSMGROUP_Overlay)]
    [TemplateVisualState(Name = VSMSTATE_HiddenOverlay, GroupName = VSMGROUP_Overlay)]
    [StyleTypedProperty(Property = PROPERTY_BottomBarStyle, StyleTargetType = typeof(Border))]
    [StyleTypedProperty(Property = PROPERTY_BootstrapButtonStyle, StyleTargetType = typeof(BootstrapButton))]
    [StyleTypedProperty(Property = PROPERTY_WindowIconStyle, StyleTargetType = typeof(WindowIcon))]
    [ContentProperty("Windows")]
    public class FloatingWindowHostZUI : FloatingWindowHost
    {
        #region "Constants"

        protected const string PART_ZUIHost = "PART_ZUIHost";

        #endregion

        #region "Member Fields"

        private ZoomAndPanControl zuiHost;

        #endregion

        public FloatingWindowHostZUI()
        {
            DefaultStyleKey = typeof(FloatingWindowHostZUI);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            zuiHost = base.GetTemplateChild(PART_ZUIHost) as ZoomAndPanControl;
        }

    }

}
