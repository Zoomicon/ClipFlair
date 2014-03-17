using ColorPickerLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker_Demo
{
  public partial class MainPage : UserControl
  {
    public MainPage()
    {
      InitializeComponent();
      OnColorChanged();
    }

    private void colorPicker1_ColorChanged(object sender, RoutedEventArgs e)
    {
      if (colorPicker1 != null)
      {
        MessageBox.Show(colorPicker1.Color.ToString());
        OnColorChanged();
      }
    }

    public void OnColorChanged()
    {
      if (ellipse00 != null)
        ellipse00.Fill = ColorHelper.ChangedColor((SolidColorBrush)ellipse00.Fill, colorPicker1.Color); //NOTE: original code was trying to change brush color directly which isn't allowed in WPF (brush object is frozen)
    }

  }
}
