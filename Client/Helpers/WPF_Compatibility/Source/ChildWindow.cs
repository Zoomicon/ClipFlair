//Filename: ChildWindowExt.cs
//Version: 20150205

#if SILVERLIGHT
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
#else
using System.Security.Permissions;
using System.Windows;
#endif

namespace WPF_Compatibility
{

  #if SILVERLIGHT
  [TemplatePartAttribute(Name = "Overlay", Type = typeof(Panel))]
  [TemplatePartAttribute(Name = "ContentRoot", Type = typeof(FrameworkElement))]
  [TemplatePartAttribute(Name = "CloseButton", Type = typeof(ButtonBase))]
  [TemplatePartAttribute(Name = "ContentPresenter", Type = typeof(FrameworkElement))]
  [TemplatePartAttribute(Name = "Chrome", Type = typeof(FrameworkElement))]
  [TemplateVisualStateAttribute(Name = "Closed", GroupName = "WindowStates")]
  [TemplatePartAttribute(Name = "Root", Type = typeof(FrameworkElement))]
  [TemplateVisualStateAttribute(Name = "Open", GroupName = "WindowStates")]
  #else
  [LocalizabilityAttribute(LocalizationCategory.Ignore)]
  [UIPermissionAttribute(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
  #endif
  public class ChildWindowExt : 
    #if SILVERLIGHT
    System.Windows.Controls.ChildWindow
    #else
    Window
    #endif
  {

    #region --- Constructor ---

    #if !SILVERLIGHT
    static ChildWindowExt()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ChildWindowExt), new FrameworkPropertyMetadata(typeof(Window)));
    }
    #endif

    public ChildWindowExt()
    {
      ApplyStyle();
    }

    public virtual void ApplyStyle()
    {
      //note: don't call base.ApplyStyle() at descendent
      DefaultStyleKey = typeof(
        #if SILVERLIGHT
        System.Windows.Controls.ChildWindow
        #else
        Window
        #endif
        );
    }

    #endregion

    public void ShowModal()
    {
      #if SILVERLIGHT
      Show();
      #else
      ShowModal();
      #endif
    }
  
  }
  
}
