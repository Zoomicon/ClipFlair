//Filename: WPF_DesignerProperties
//Version: 20150209
//Author: George Birbilis <zoomicon.com>

using System.ComponentModel;
using System.Windows.Controls;

namespace WPF_Compatibility
{

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
