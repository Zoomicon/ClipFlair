//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapWindow.xaml.cs
//Version: 20121221

using ClipFlair.Windows.Views;

namespace ClipFlair.Windows
{

  public partial class MapWindow : BaseWindow
    {
        public MapWindow()
        {
          View = new MapView(); //must set the view first
          InitializeComponent();
        }

        #region View

        public new IMapViewer View //hiding parent property
        {
          get { return (IMapViewer)base.View; } //delegating to parent property
          set { base.View = value; }
        }

        #endregion
     
    }
}
