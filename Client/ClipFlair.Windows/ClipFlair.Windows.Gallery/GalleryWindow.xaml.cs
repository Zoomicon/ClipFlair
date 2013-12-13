//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryWindow.xaml.cs
//Version: 20131002

using ClipFlair.Windows.Gallery.Commands;
using ClipFlair.Windows.Views;
using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows
{

  public partial class GalleryWindow : BaseWindow
  {
    public GalleryWindow()
    {
      View = new GalleryView(); //must set the view first
      InitializeComponent();
      
      //IsAnimated = false; //workarround: Can't turn over Pivot viewer when using 3D-style rotation, thowing transformation class cast errors
    }

    #region View

    public IGallery GalleryView
    {
      get { return (IGallery)View; }
      set { View = value; }
    }

    #endregion

    #region Methods

    public void RefreshFilter()
    {
      string f = pivot.Filter;
      pivot.Filter = "";
      pivot.Filter = f;

      InvalidateMeasure();
      InvalidateArrange();
      UpdateLayout();
    }

    #endregion

    #region Events

    /*
    public override void ShowOptions() //workarround: Can't turn over Pivot viewer when using 3D-style rotation, thowing transformation class cast errors (the IsAnimated=false isn't enough, need this too, else at resize etc. when flipped will throw exceptions)
    {
      if (!Flipped)
        pivot.Visibility = Visibility.Collapsed;

      base.ShowOptions();

      if (!Flipped)
        pivot.Visibility = Visibility.Visible;
    }
*/

    private void GalleryItemAdorner_CommandsRequested(object sender, PivotViewerCommandsRequestedEventArgs e)
    {
      //if (e.IsItemSelected)
      {
        PivotViewerItem item = (PivotViewerItem)e.Item;
        try { e.Commands.Add(new InfoCommand(item)); } catch { }
        try { e.Commands.Add(new ShareCommand(item)); } catch { }
        try { e.Commands.Add(new DownloadCommand(item)); } catch { }
        try { e.Commands.Add(new OpenCommand(item)); } catch { }
      }
    }

    #endregion

  }
}
