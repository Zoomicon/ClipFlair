//Filename: Expander.cs
//Version: 20150205

using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPF_Compatibility
{
 
  // Summary:
  //     Represents the control that displays a header and has a collapsible content window
  #if SILVERLIGHT
  [TemplatePart(Name = "ExpanderButton", Type = typeof(ToggleButton))]
  [TemplateVisualState(Name = "Collapsed", GroupName = "ExpansionStates")]
  [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "ExpandDown", GroupName = "ExpandDirectionStates")]
  [TemplateVisualState(Name = "Expanded", GroupName = "ExpansionStates")]
  [TemplateVisualState(Name = "ExpandLeft", GroupName = "ExpandDirectionStates")]
  [TemplateVisualState(Name = "ExpandRight", GroupName = "ExpandDirectionStates")]
  [TemplateVisualState(Name = "ExpandUp", GroupName = "ExpandDirectionStates")]
  [TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
  [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Pressed", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
  #else
  [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
  #endif
  public class Expander : System.Windows.Controls.Expander
  {

    #region --- Initialization ---

    #if !SILVERLIGHT
    static Expander()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Expander), new FrameworkPropertyMetadata(typeof(System.Windows.Controls.Expander)));
    }
    #endif

    public Expander()
    {
      ApplyStyle();
    }

    public virtual void ApplyStyle()
    {
      //note: don't call base.ApplyStyle() at descendent
      DefaultStyleKey = typeof(System.Windows.Controls.Expander);
    }

    #endregion

  }

}
