//Project: WebCapture
//Filename: IWebBrowser.cs
//Author: George Birbilis (http://zoomicon.com)
//Version: 20130823

using System;
using System.Drawing;

namespace WebCapture
{
  interface IWebBrowser
  {

    #region --- Properties ---

    Uri Address { get; set; }
    Size BrowserSize { get; set; }

    #endregion
    
    #region --- Methods ---

    void Go(Uri address = null);
    void GoBack();
    void GoForward();

    #endregion

  }
}
