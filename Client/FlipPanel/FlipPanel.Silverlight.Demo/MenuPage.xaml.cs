using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Reflection;

namespace FlipPanelTest
{
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void lstPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            // Get the selected item.
            string newPageName = ((ListBoxItem)e.AddedItems[0]).Content.ToString();
            
            // Create an instance of the page named
            // by the current button.
            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            UserControl newPage = (UserControl)assembly.CreateInstance(
                type.Namespace + "." + newPageName);

            newPage.Width = 400;
            newPage.Height = 400;

            // Show the page.
            pagePlaceholder.Child = newPage;
        }        
    }
}
