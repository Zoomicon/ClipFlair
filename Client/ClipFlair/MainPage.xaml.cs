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

namespace ClipFlair
{
  public partial class MainPage : UserControl
  {
    public MainPage()
    {
      InitializeComponent();
    }

   
    private void Page_MouseWheel(object sender, MouseWheelEventArgs e)
    {
      double scalingFactor = (double)e.Delta / 110;
      if (scalingFactor > 0)
      {
        viewbox.Width *= scalingFactor;
        viewbox.Height *= scalingFactor;
      }
      else
      {
        viewbox.Width /= -scalingFactor;
        viewbox.Height /= -scalingFactor;
      }
    }
  }
}
