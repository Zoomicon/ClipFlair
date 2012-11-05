//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TestWindow1.xaml.cs
//Version: 20121004

namespace ClipFlair.Windows
{
  
  public partial class TestWindow1 : BaseWindow
  {

    public TestWindow1()
    {
      InitializeComponent();
    }

    private void btnTest_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      btnTest.Content += "1";
    }

  }

}
