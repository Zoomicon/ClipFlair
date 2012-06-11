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
//using FlipPanel;

namespace FlipPanelTest
{
    public partial class FlipPanelTest : UserControl
    {
        public FlipPanelTest()
        {
            InitializeComponent();
        }

        private void cmdFlip1_Click(object sender, RoutedEventArgs e)
        {            
            panel1.IsFlipped = !panel1.IsFlipped;
        }

        private void cmdFlip2_Click(object sender, RoutedEventArgs e)
        {
          panel2.IsFlipped = !panel2.IsFlipped;
        }

    }
}
