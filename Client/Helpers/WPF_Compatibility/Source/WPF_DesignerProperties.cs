//Filename: WPF_DesignerProperties
//Version: 20130708
//Author: George Birbilis <birbilis@kagi.com>

using System.ComponentModel;

namespace WPFCompatibility
{

  using System.Windows.Controls;
  public static class WPF_DesignerProperties
  {

    public static bool IsInDesignTool
    {
      get
      {
        bool result = DesignerProperties.GetIsInDesignMode(new Button());
        #if SILVERLIGHT
        return result || DesignerProperties.IsInDesignTool;
        #else
        return result;
        #endif
      }
    }

  }

}
