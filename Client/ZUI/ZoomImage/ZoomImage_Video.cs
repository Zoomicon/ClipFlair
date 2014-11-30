//Filename: ZoomImage_Video.cs
//Version: 20141130
//Author: George Birbilis (http://zoomicon.com)

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZoomImage
{
  public partial class ZoomImage : UserControl
  {

    #region --- Fields ---

    CaptureSource videoCaptureSource; //=null
    VideoBrush videoBrush; //=null

    #endregion

    #region --- Properties ---

    #region CameraSourceUsed

    /// <summary>
    /// CameraSourceUsed Dependency Property
    /// </summary>
    public static readonly DependencyProperty CameraSourceUsedProperty =
        DependencyProperty.Register("CameraSourceUsed", typeof(bool), typeof(ZoomImage),
            new FrameworkPropertyMetadata(false, //this has to be false so that we receive the change event to open the camera source
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnCameraSourceUsedChanged)));

    /// <summary>
    /// Gets or sets the CameraSourceUsed property
    /// </summary>
    public bool CameraSourceUsed
    {
      get { return (bool)GetValue(CameraSourceUsedProperty); }
      set { SetValue(CameraSourceUsedProperty, value); }
    }

    /// <summary>
    /// Handles changes to the CameraSourceUsed property
    /// </summary>
    private static void OnCameraSourceUsedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ZoomImage target = (ZoomImage)d;
      bool oldCameraSourceUsed = (bool)e.OldValue;
      bool newCameraSourceUsed = target.CameraSourceUsed;
      target.OnCameraSourceUsedChanged(oldCameraSourceUsed, newCameraSourceUsed);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the CameraSourceUsed property
    /// </summary>
    protected virtual void OnCameraSourceUsedChanged(bool oldCameraSourceUsed, bool newCameraSourceUsed)
    {
      if (newCameraSourceUsed)
        StartVideoCapture();
      else
        StopVideoCapture();
    }

    #endregion

    #endregion

    #region --- Methods ---

    private void StartVideoCapture()
    {
      try
      {
        if (CaptureDeviceConfiguration.RequestDeviceAccess())
        {
          //use default video capture device (can select other from Silverlight settings)
          if (videoCaptureSource == null)
            videoCaptureSource = new CaptureSource()
            {
              VideoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice()
            };

          if (videoBrush == null)
          {
            videoBrush = new VideoBrush()
            {
              Stretch = Stretch.UniformToFill //don't deform the image
            };
            videoBrush.SetSource(videoCaptureSource);

            videoCaptureSource.Start();
          }

          imgPlain.Visibility = Visibility.Collapsed;
          imgPlainZoom.Background = videoBrush;
        }
      }
      catch
      {
        //NOP
      }
      ShowPlainImage();
    }

    private void StopVideoCapture()
    {
      try
      {
        videoCaptureSource.Stop();
      }
      catch
      {
        //NOP
      }
      imgPlain.Visibility = Visibility.Visible;
      ApplySource(Source); //reapply existing Source uri
    }

    #endregion

  }
}
