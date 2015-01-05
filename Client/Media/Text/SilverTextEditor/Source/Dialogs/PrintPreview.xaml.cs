//Filename: PrintPreview.xaml.cs
//Version: 20130811

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SilverTextEditor
{
    public partial class PrintPreview : ChildWindowExt
    {
        public PrintPreview()
        {
            InitializeComponent();
        }

        public void ShowPreview(RichTextBox rtb)
        {   
#if SILVERLIGHT
            WriteableBitmap image = new WriteableBitmap(rtb, null);
            previewImage.Source = image;            
#else
          //TODO (see FloatingWindow control extensions on how to get image of control on WPF)
#endif
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

