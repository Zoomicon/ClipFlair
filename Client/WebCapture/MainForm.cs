//Project: WebCapture
//Filename: MainForm.cs
//Author: George Birbilis (http://zoomicon.com)
//Version: 20130821

using System;
using System.Deployment.Application;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Reflection;

namespace WebCapture
{
  public partial class MainForm : Form
  {

    #region --- Initialization ---

    public MainForm()
    {
      InitializeComponent();
      ShowVersion();
    }

    private void ShowVersion()
    {
      Version version = (ApplicationDeployment.IsNetworkDeployed)?
        ApplicationDeployment.CurrentDeployment.CurrentVersion :
        Assembly.GetExecutingAssembly().GetName().Version;
        //if network deployed show published version (as the web install page does)

      Text = Text + " " + version.Major + "." + version.Minor + "."
                        + version.Build + "." + version.Revision; //change form title
    }

    #endregion

    #region --- Actions ---

    public static readonly Size SIZE_640x480 = new Size(640, 480); 
    public static readonly Size SIZE_800x600 = new Size(800, 600);
    public static readonly Size SIZE_1024x768 = new Size(1024, 768);
    public static readonly Size SIZE_1280x1024 = new Size(1280, 1024);

    public void ResizeWebBrowser(Size newSize)
    {
      Size diff = newSize - webBrowser.Size; //calculate difference between wanted webBrowser size and its current one
      Size = Size + diff; //resize form by that difference, assuming webBrowser will get similar size difference since it's docked to the form center
      WindowState = FormWindowState.Normal; //if at maximized mode, change to normal window mode to display at the new size
    }

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

    private void webBrowser_SizeChanged(object sender, EventArgs e)
    {
      btnWebBrowserSize.Text = webBrowser.Size.ToString();
    }

    private void mnuSize640x480_Click(object sender, EventArgs e)
    {
      ResizeWebBrowser(SIZE_640x480);
    }

    private void mnuSize800x600_Click(object sender, EventArgs e)
    {
      ResizeWebBrowser(SIZE_800x600);
    }

    private void mnuSize1024x768_Click(object sender, EventArgs e)
    {
      ResizeWebBrowser(SIZE_1024x768);
    }

    private void mnuSize1280x1024_Click(object sender, EventArgs e)
    {
      ResizeWebBrowser(SIZE_1280x1024);
    }

    #endregion

  }

}
