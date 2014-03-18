//Project: WebCapture
//Filename: MainForm.cs
//Author: George Birbilis (http://zoomicon.com)
//Version: 20131202

using System;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WebCapture
{
  public partial class MainForm : Form, IWebCapture
  {

    #region --- Constants ---

    public static readonly Size SIZE_640x480 = new Size(640, 480);
    public static readonly Size SIZE_800x600 = new Size(800, 600);
    public static readonly Size SIZE_1024x768 = new Size(1024, 768);
    public static readonly Size SIZE_1280x1024 = new Size(1280, 1024);
    public static readonly Size SIZE_1920x1010 = new Size(1920, 1010);

    public const string MSG_ERROR_WRONG_URL = "Wrong URL";
    public const string MSG_ERROR_WRONG_NUMBER = "Wrong number";
    public const string MSG_ERROR_COMMAND_LINE = "Error parsing command-line";

    #endregion

    #region --- Initialization ---

    public MainForm()
    {
      InitializeComponent();
      ShowVersion();

      webBrowser.CanGoBackChanged += new EventHandler(webBrowser_CanGoBackChanged);
      webBrowser.CanGoForwardChanged += new EventHandler(webBrowser_CanGoForwardChanged);
    }

    public MainForm(string[] args) : this()
    {
      CommandLineArguments = args;
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

    #region --- Properties ---

    public string[] CommandLineArguments { get; set; }

    public Uri Address
    {
      get { return webBrowser.Url; }
      set { webBrowser.Url = value; }
    }

    public Size BrowserSize
    {
      get { return webBrowser.Size; }
      set
      {
        Size diff = value - webBrowser.Size; //calculate difference between wanted webBrowser size and its current one
        Size = Size + diff; //resize form by that difference, assuming webBrowser will get similar size difference since it's docked to the form center
        WindowState = FormWindowState.Normal; //if at maximized mode, change to normal window mode to display at the new size
      }
    }

    #endregion

    #region --- Methods ---

    public void ParseCommandLine(string[] args)
    {
      if (args != null)
        if (args.Length == 0)
          return;
        else if (args.Length != 3)
          MessageBox.Show("Syntax: WebCapture url imageFilename delay");
        else
          try
          {
            CaptureScreenshot(new Uri(args[0]), args[1], int.Parse(args[2]), true);
          }
          catch (UriFormatException e)
          {
            Error(e.Message, MSG_ERROR_WRONG_URL);
          }
          catch (FormatException e)
          {
            Error(e.Message, MSG_ERROR_WRONG_NUMBER);
          }
          catch (Exception e)
          {
            Error(e.Message, MSG_ERROR_COMMAND_LINE);
          }
    }
    
    private void Error(string message, string caption)
    {
      MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void GoBack()
    {
      webBrowser.GoBack();
    }

    public void GoForward()
    {
      webBrowser.GoForward();
    }

    public void Go(Uri address = null)
    {
      if (address == null)
        try
        {
          string url = comboURL.Text;

          if (!url.Contains("://"))
            url = "http://" + url; //prepend HTTP protocol if none was given

          address = new Uri(url);
        }
        catch(Exception e)
        {
          Error(e.Message, MSG_ERROR_WRONG_URL);
          return;
        }

      webBrowser.Url = address;
    }

    private WebBrowserDocumentCompletedEventHandler handler; //need to make it a class field for the handler below (anonymous delegates seem to capture state at point of definition, so they can't capture their own reference)
    private string imageFilename;
    private bool exit;

    public void CaptureScreenshot(Uri address = null, string imageFilename = null, int msecDelay = 0, bool exit = false)
    {
      handler = (s, e) =>
       {
         webBrowser.DocumentCompleted -= handler; //must do first

         this.imageFilename = imageFilename;
         this.exit = exit;

         timerScreenshot.Interval = (msecDelay > 0)? msecDelay : 1;
         timerScreenshot.Enabled = true;
       };

      webBrowser.DocumentCompleted += handler;
      Go(address); //if address == null, will use URL from UI
    }

    #endregion

    #region --- Events ---

    private void MainForm_Shown(object sender, EventArgs e)
    {
      ParseCommandLine(CommandLineArguments);
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      GoBack();
    }

    private void btnForward_Click(object sender, EventArgs e)
    {
      GoForward();
    }
    
    private void comboURL_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        Go();
    }

    private void btnGo_Click(object sender, EventArgs e)
    {
      Go();
    }

    private void btnCapture_Click(object sender, EventArgs e)
    {
      CaptureScreenshot();
    }

    void webBrowser_CanGoBackChanged(object sender, EventArgs e)
    {
      btnBack.Enabled = webBrowser.CanGoBack;
    }

    void webBrowser_CanGoForwardChanged(object sender, EventArgs e)
    {
      btnForward.Enabled = webBrowser.CanGoForward;
    }

    private void webBrowser_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
    {
      //TODO: optionally here can disable creating external windows (unfortunately can't find a way to get their URL and show them inside the webBrowser control)
    }

    private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
    {
      comboURL.Text = webBrowser.Url.ToString();
    }

    private void webBrowser_SizeChanged(object sender, EventArgs e)
    {
      btnWebBrowserSize.Text = webBrowser.Size.ToString();
    }

    private void mnuSize640x480_Click(object sender, EventArgs e)
    {
      BrowserSize = SIZE_640x480;
    }

    private void mnuSize800x600_Click(object sender, EventArgs e)
    {
      BrowserSize = SIZE_800x600;
    }

    private void mnuSize1024x768_Click(object sender, EventArgs e)
    {
      BrowserSize = SIZE_1024x768;
    }

    private void mnuSize1280x1024_Click(object sender, EventArgs e)
    {
      BrowserSize = SIZE_1280x1024;
    }

    private void mnuSize1920x1010_Click(object sender, EventArgs e)
    {
      BrowserSize = SIZE_1920x1010;
    }
        
    private void timerScreenshot_Tick(object sender, EventArgs e)
    {
      timerScreenshot.Enabled = false; //must do first

      BeginInvoke((Action)(() => //Invoke at UI thread
      { //run in UI thread

        BringToFront();
        Bitmap bitmap = webBrowser.GetScreenshot();

        if (imageFilename == null)
          imageFilename = bitmap.ShowSaveFileDialog();

        if (imageFilename != null)
        {
          Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(imageFilename))); //create any parent directories needed
          bitmap.Save(imageFilename);
        }

        bitmap.Dispose(); //release bitmap resources

        if (exit)
          Close(); //this should close the app, since this is the main form

      }), null);
    }

    #endregion

  }

}
