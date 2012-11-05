//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TestWindow3.xaml.cs
//Version: 20121103

namespace ClipFlair.Windows
{
  
  public partial class TestWindow3 : TestWindow2
  {

    public TestWindow3()
    {
      InitializeComponent();
    }

    private void btnA_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      btnA.Content += "3";
    }

    private void btnB_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      btnB.Content += "3";
    }

  }

}
