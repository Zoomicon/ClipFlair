//Filename: IViewer.cs
//Version: 20141130
//Author: George Birbilis (http://zoomicon.com)

namespace ZoomImage.Models
{
  public interface IViewer //TODO: NOT USED YET
  {
    void ZoomToFit();
    void ZoomIn(double zoomStep);
    void ZoomOut(double zoomStep);
    void Zoom(double zoomStep, double viewerFocusPointX, double viewerFocusPointY);
  }
}
