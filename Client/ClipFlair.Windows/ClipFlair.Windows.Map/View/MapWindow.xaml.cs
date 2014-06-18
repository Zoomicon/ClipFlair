//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapWindow.xaml.cs
//Version: 20140618

using ClipFlair.Windows.Views;

using Microsoft.Maps.MapControl;

using System.ComponentModel;

namespace ClipFlair.Windows
{

  public partial class MapWindow : BaseWindow
  {
    public MapWindow()
    {
      View = new MapView(); //must initialize the view first
      InitializeComponent();
      updateMapMode(); //this Map property doesn't support data-binding
    }

    #region View

    public IMapViewer MapView
    {
      get { return (IMapViewer)View; }
      set { View = value; }
    }

    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

      if (map == null)
        return;
 
      switch (e.PropertyName)
      {
        case null: //BaseWindow's "View" property's setter calls View_PropertyChanged with PropertyName=null to signify multiple properties have changed (initialized with default values that is)
        case IMapViewerProperties.PropertyMode:
        case IMapViewerProperties.PropertyLabelsVisible:
        case IMapViewerProperties.PropertyLabelsFading:
          updateMapMode();
          break;
      }
    }

    #endregion

    private void updateMapMode()
    {
      if (map != null)
        map.Mode = MapView.ModeValue;
    }

    #region Events

    private void map_ModeChanged(object sender, MapEventArgs e)
    {
      if (View != null)
        MapView.ModeValue = map.Mode;
    }

    private void map_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      e.Handled = true; //consume the mouse down event (used to pan arround) that Bing Maps doesn't seem to consume itself, so that it isn't grabbed by BaseWindow to drag the component arround
    }
    
    #endregion

  }
}
