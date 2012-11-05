//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TestWindow2.xaml.cs
//Version: 20121004

using SilverFlow.Controls;

namespace ClipFlair.Windows
{
  
  public partial class TestWindow2 : FloatingWindow
  {

    public TestWindow2()
    {
      InitializeComponent();
    }

    private void btnTest_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      btnTest.Content += "2";
    }

  }

}
