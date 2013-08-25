//Project: WebCapture
//Filename: IWebCapture.cs
//Author: George Birbilis (http://zoomicon.com)
//Version: 20130825

using System;
using System.Drawing;

namespace WebCapture
{
  interface IWebCapture : IWebBrowser
  {

    #region --- Methods ---

    void CaptureScreenshot(Uri address = null, string imageFilename = null, int timeout = 0, bool exit = false);

    #endregion
  }
}
