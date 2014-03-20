//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: MultiScaleImageExtensions.cs
//Version: 20140223

using System.Windows;
using System.Windows.Controls;

namespace Utils.Extensions
{
  public static class MultiScaleImageExtensions
  {

    //
    // Summary:
    //     Enables a user to zoom in on a point of the System.Windows.Controls.MultiScaleImage.
    //
    // Parameters:
    //   zoomIncrementFactor:
    //     Specifies the zoom. This number is greater than 0. A value of 1 specifies
    //     that the image fit the allotted page size exactly. A number greater than
    //     1 specifies to zoom in. If a value of 0 or less is used, failure is returned
    //     and no zoom changes are applied.
    //
    //   zoomCenterLogical:
    //     the point on the System.Windows.Controls.MultiScaleImage
    //     that is zoomed in on. This is a logical point (its coordinates are between 0 and 1).
    public static void ZoomAboutLogicalPoint(this MultiScaleImage img, double zoomIncrementFactor, Point zoomCenterLogical)
    {
      img.ZoomAboutLogicalPoint(zoomIncrementFactor, zoomCenterLogical.X, zoomCenterLogical.Y);
    }
 

  }

}
