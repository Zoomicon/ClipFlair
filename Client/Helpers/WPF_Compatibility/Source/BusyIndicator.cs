//Filename: BusyIndicator.cs
//Version: 20150204

using System.Windows;
namespace WPF_Compatibility
{
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
