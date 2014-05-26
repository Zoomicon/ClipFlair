//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: OptionsLoadSaveControl.xaml.cs
//Version: 20130315

using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.UI.Widgets
{

  public partial class OptionsLoadSaveControl : UserControl
  {

    public OptionsLoadSaveControl()
    {
      InitializeComponent();
    }

    #region LoadURLTooltip

    public string LoadURLTooltip
    {
      get { return (string)btnLoadURL.GetValue(ToolTipService.ToolTipProperty); }
      set { btnLoadURL.SetValue(ToolTipService.ToolTipProperty, value); }
    }

    #endregion
    
    #region LoadTooltip

    public string LoadTooltip
    {
      get { return (string)btnLoad.GetValue(ToolTipService.ToolTipProperty); }
      set { btnLoad.SetValue(ToolTipService.ToolTipProperty, value); }
    }

    #endregion

    #region SaveTooltip

    public string SaveTooltip
    {
      get { return (string)btnSave.GetValue(ToolTipService.ToolTipProperty); }
      set { btnSave.SetValue(ToolTipService.ToolTipProperty, value); }
    }

    #endregion

    #region Events

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

    #endregion
  
  }

}
