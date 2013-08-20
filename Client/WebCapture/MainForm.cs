//Project: WebCapture
//Filename: MainForm.cs
//Author: George Birbilis (http://zoomicon.com)
//Version: 20130820

using System;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Reflection;

namespace WebCapture
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
      Version version = Assembly.GetExecutingAssembly().GetName().Version;
      Text = Text + " " + version.Major + "." + version.Minor + " (build " + version.Build + ")"; //change form title
    }

    #region --- Actions ---

    public void PerformGo()
    {
      webBrowser.Url = new Uri(txtURL.Text);
    }

    public void PerformCapture()
    {
      BringToFront();
      Bitmap bitmap = webBrowser.GetScreenshot();
      bitmap.ShowSaveFileDialog();
      bitmap.Dispose(); //release bitmap resources
    }

    #endregion

    #region --- Events ---

    private void txtURL_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        PerformGo();
    }

    private void btnGo_Click(object sender, EventArgs e)
    {
      PerformGo();
    }

    private void btnCapture_Click(object sender, EventArgs e)
    {
      PerformCapture();
    }

    private void webBrowser_LocationChanged(object sender, EventArgs e)
    {
      txtURL.Text = webBrowser.Location.ToString();
    }

    #endregion

  }

}
