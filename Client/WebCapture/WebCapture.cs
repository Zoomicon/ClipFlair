//Project: WebCapture
//Filename: WebCapture.cs
//Author: George Birbilis (http://zoomicon.com)
//Version: 20130821

using System;
using System.Windows.Forms;

namespace WebCapture
{

  static class WebCapture
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }

  }

}
