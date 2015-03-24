//Filename: MouseButton.cs
//Version: 20120728
//Author: George Birbilis <zoomicon.com>

#if SILVERLIGHT

namespace System.Windows.Input
{
    /// <summary>
    /// Silverlight does not have System.Windows.Input.MouseButton, has separate event handlers for left and right mouse click
    /// </summary>
    public enum MouseButton
    {

      /// <summary>
      /// The left mouse button.
      /// </summary>
      Left,

      /// <summary>
      /// The middle mouse button.
      /// </summary>
      Middle,

      /// <summary>
      /// The right mouse button.
      /// </summary>
      Right,

      /// <summary>
      /// The fourth mouse button.
      /// </summary>
      XButton1,

      /// <summary>
      /// The fifth mouse button.
      /// </summary>
      XButton2

    }

}

#endif