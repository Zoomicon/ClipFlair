//Filename: Label.cs
//Version: 20150205

using System.Windows;

namespace WPF_Compatibility
{
  #if SILVERLIGHT
  [TemplateVisualStateAttribute(Name = "Invalid", GroupName = "ValidationStates")]
  [TemplateVisualStateAttribute(Name = "Disabled", GroupName = "CommonStates")]
  [TemplateVisualStateAttribute(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualStateAttribute(Name = "Valid", GroupName = "ValidationStates")]
  [TemplateVisualStateAttribute(Name = "NotRequired", GroupName = "RequiredStates")]
  [TemplateVisualStateAttribute(Name = "Required", GroupName = "RequiredStates")]
  #else
  [LocalizabilityAttribute(LocalizationCategory.Label)]
  #endif
  public class Label : System.Windows.Controls.Label
  {

    #region --- Constructor ---

    #if !SILVERLIGHT
    static Label()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Label), new FrameworkPropertyMetadata(typeof(System.Windows.Controls.Label)));
    }
    #endif

    public Label()
    {
      ApplyStyle();
    }

    public virtual void ApplyStyle()
    {
      //note: don't call base.ApplyStyle() at descendent
      DefaultStyleKey = typeof(System.Windows.Controls.Label);
    }

    #endregion

  }

}
