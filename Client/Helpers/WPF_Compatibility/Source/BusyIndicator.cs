//Filename: BusyIndicator.cs
//Version: 20150204

using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPF_Compatibility
{
  //WPFToolkit and Silverlight seem to share the same visual states and parts for ChildWindow

  internal static partial class VisualStates 
  {
    /// <summary>
    /// Busyness group name.
    /// </summary>
    public const string GroupBusyStatus = "BusyStatusStates";

    /// <summary>
    /// Busy state for BusyIndicator.
    /// </summary>
    public const string StateBusy = "Busy";

    /// <summary>
    /// Idle state for BusyIndicator.
    /// </summary>
    public const string StateIdle = "Idle";

    /// <summary>
    /// BusyDisplay group.
    /// </summary>
    public const string GroupVisibility = "VisibilityStates";

    /// <summary>
    /// Visible state name for BusyIndicator.
    /// </summary>
    public const string StateVisible = "Visible";

    /// <summary>
    /// Hidden state name for BusyIndicator.
    /// </summary>
    public const string StateHidden = "Hidden";
  }

  [TemplateVisualState(Name = VisualStates.StateIdle, GroupName = VisualStates.GroupBusyStatus)]
  [TemplateVisualState(Name = VisualStates.StateBusy, GroupName = VisualStates.GroupBusyStatus)]
  [TemplateVisualState(Name = VisualStates.StateVisible, GroupName = VisualStates.GroupVisibility)]
  [TemplateVisualState(Name = VisualStates.StateHidden, GroupName = VisualStates.GroupVisibility)]
  [StyleTypedProperty(Property = "OverlayStyle", StyleTargetType = typeof(Rectangle))]
  [StyleTypedProperty(Property = "ProgressBarStyle", StyleTargetType = typeof(ProgressBar))]
  public class BusyIndicator : 
    #if SILVERLIGHT
    System.Windows.Controls.BusyIndicator
    #else
    Xceed.Wpf.Toolkit.BusyIndicator
    #endif
  {

    #region --- Constructor ---

    #if !SILVERLIGHT
    static BusyIndicator()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(
    #if SILVERLIGHT
    System.Windows.Controls.BusyIndicator
    #else
    Xceed.Wpf.Toolkit.BusyIndicator
    #endif
    )));
    }
    #endif

    public BusyIndicator()
    {
      ApplyStyle();
    }

    public virtual void ApplyStyle()
    {
      //note: don't call base.ApplyStyle() at descendent
      DefaultStyleKey = typeof(
#if SILVERLIGHT
    System.Windows.Controls.BusyIndicator
#else
Xceed.Wpf.Toolkit.BusyIndicator
#endif        
        );
    }

    #endregion

  }

}
