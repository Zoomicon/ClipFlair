//Version: 20121219

using System.Windows;
using SilverFlow.Controls;
using ZoomAndPan;

namespace FloatingWindowZUI
{
  public class IconBarZUI : IconBar
  {

    protected override void Icon_Click(object sender, RoutedEventArgs e)
    {
      base.Icon_Click(sender, e);

      WindowIcon icon = sender as WindowIcon;
      if (icon == null) return;

      FloatingWindow w = icon.Window;
      if (w == null) return;

      FrameworkElement host = w.HostPanel;
      if (host == null) return;

      ZoomAndPanControl zoomHost = host.Parent as ZoomAndPanControl;
      if (zoomHost == null) return;

      zoomHost.ScrollToCenter(w.BoundingRectangle); //zoomHost.ZoomTo(w.BoundingRectangle);
    }

  }

}
