//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapWindow.xaml.cs
//Version: 20121205

using ClipFlair.Windows.Views;

using System.ComponentModel.Composition;

namespace ClipFlair.Windows
{

  [Export("ClipFlair.Windows.Views.MapView", typeof(IWindowFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class MapWindowFactory: IWindowFactory
  {
    public BaseWindow CreateWindow()
    {
      return new MapWindow();
    }
  }

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
