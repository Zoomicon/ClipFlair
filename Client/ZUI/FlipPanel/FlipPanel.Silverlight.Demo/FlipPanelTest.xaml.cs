//Filename: FlipPanelText.xaml.cs
//Version: 20121204

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

namespace FlipPanelTest
{
  public partial class FlipPanelTest : UserControl
  {
    public FlipPanelTest()
    {
      InitializeComponent();
    }

    private void cmdFlip0_Click(object sender, RoutedEventArgs e)
    {
      panel0.IsFlipped = !panel0.IsFlipped;
    }

    private void cmdFlipVertical_Click(object sender, RoutedEventArgs e)
    {
      panelVertical.IsFlipped = !panelVertical.IsFlipped;
    }

    private void cmdFlip1_Click(object sender, RoutedEventArgs e)
    {
      panel1.IsFlipped = !panel1.IsFlipped;
    }

    private void cmdFlip2_Click(object sender, RoutedEventArgs e)
    {
      panel2.IsFlipped = !panel2.IsFlipped;
    }

    private void cmdFlip3_Click(object sender, RoutedEventArgs e)
    {
      panel3.IsFlipped = !panel3.IsFlipped;
    }

    private void cmdFlip4_Click(object sender, RoutedEventArgs e)
    {
      panel4.IsFlipped = !panel4.IsFlipped;
    }

  }
}
