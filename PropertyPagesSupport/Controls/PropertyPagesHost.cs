using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PropertyPagesSupport.Controls
{
  public class PropertyPagesHost : Control, IPropertyPagesHost
  {    
    public PropertyPagesHost()
    {
      this.DefaultStyleKey = typeof(PropertyPagesHost);
    }

    public IPropertyPages PropertyPages { get; set; }
  }
}
