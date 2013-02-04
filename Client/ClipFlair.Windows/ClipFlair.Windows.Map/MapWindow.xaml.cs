//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MapWindow.xaml.cs
//Version: 20130204

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
      map.Mode = MapView.ModeValue; //this Map property doesn't support data-binding
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

      if (map == null) return;
 
      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        map.Mode = MapView.ModeValue;
        //...
      }
      else switch (e.PropertyName)
      {
        case IMapViewerProperties.PropertyMode:
        case IMapViewerProperties.PropertyLabelsVisible:
        case IMapViewerProperties.PropertyLabelsFading:
          map.Mode = MapView.ModeValue;
          break;
        //...
        default:
         //NOP
         break;
      }
    }

    #endregion

    #region Events

    private void map_ModeChanged(object sender, MapEventArgs e)
    {
      if (View != null)
        MapView.ModeValue = map.Mode;
    }

    #endregion

  }
}
