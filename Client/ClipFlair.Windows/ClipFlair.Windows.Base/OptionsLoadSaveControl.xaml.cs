//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: OptionsLoadSaveControl.xaml.cs
//Version: 20121221

using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.Windows.Base
{

  public partial class OptionsLoadSaveControl : UserControl
  {

    public OptionsLoadSaveControl()
    {
      InitializeComponent();
    }

    public event RoutedEventHandler LoadURLClick;
    public event RoutedEventHandler LoadClick;
    public event RoutedEventHandler SaveClick;

    private void btnLoadURL_Click(object sender, RoutedEventArgs e)
    {
      if (LoadURLClick != null)
        LoadURLClick(this, e);
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
      if (LoadClick != null)
        LoadClick(this, e);
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      if (SaveClick != null)
        SaveClick(this, e);
    }

  }

}
