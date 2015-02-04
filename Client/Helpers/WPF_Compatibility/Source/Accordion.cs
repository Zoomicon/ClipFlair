//Filename: Accordion.cs
//Version: 20150204

using System.Windows;

namespace WPF_Compatibility
{
  public class Accordion : System.Windows.Controls.Accordion
  {

    #region --- Initialization ---

    #if !SILVERLIGHT
    static Accordion()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Accordion), new FrameworkPropertyMetadata(typeof(System.Windows.Controls.Accordion)));
    }
    #endif

    public Accordion()
    {
      ApplyStyle();
    }

    public virtual void ApplyStyle()
    {
      //note: don't call base.ApplyStyle() at descendent
      DefaultStyleKey = typeof(System.Windows.Controls.Accordion);
    }

    #endregion

  }

}
