//Filename: Label.cs
//Version: 20150205

using System.Windows;

namespace WPF_Compatibility
{
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
