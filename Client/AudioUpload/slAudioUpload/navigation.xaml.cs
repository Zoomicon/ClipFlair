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

namespace slAudioUpload
{
    public partial class navigation : UserControl
    {
        public navigation()
        {
            InitializeComponent();

            this.Height = App.Current.Host.Content.ActualHeight;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
               // Theme themeContainer = (Theme)((FrameworkElement)Application.Current.RootVisual).FindName("ThemeContainer");
                //themeContainer.ThemeUri = new Uri("/System.Windows.Controls.Theming." + ((ComboBoxItem)cbTheme.SelectedItem).Content.ToString() + ";component/Theme.xaml", UriKind.RelativeOrAbsolute);
            }
            catch { }
        }
    }
}
