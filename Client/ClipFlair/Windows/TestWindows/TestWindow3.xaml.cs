//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TestWindow3.xaml.cs
//Version: 20121004

namespace ClipFlair.Windows
{
  
  public partial class TestWindow3 : TestWindow2
  {

    public TestWindow3()
    {
      InitializeComponent();
    }

    private void btnTest_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      btnTest.Content += "3";
    }

    private void btnA_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      btnA.Content += "3";
    }

  }

}
