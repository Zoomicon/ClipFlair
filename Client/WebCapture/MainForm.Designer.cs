namespace WebCapture
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.webBrowser = new System.Windows.Forms.WebBrowser();
      this.btnCapture = new System.Windows.Forms.Button();
      this.txtURL = new System.Windows.Forms.TextBox();
      this.btnGo = new System.Windows.Forms.Button();
      this.lblURL = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // webBrowser
      // 
      this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.webBrowser.Location = new System.Drawing.Point(0, 30);
      this.webBrowser.Margin = new System.Windows.Forms.Padding(0);
      this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser.Name = "webBrowser";
      this.webBrowser.ScriptErrorsSuppressed = true;
      this.webBrowser.ScrollBarsEnabled = false;
      this.webBrowser.Size = new System.Drawing.Size(657, 243);
      this.webBrowser.TabIndex = 0;
      this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
      this.webBrowser.LocationChanged += new System.EventHandler(this.webBrowser_LocationChanged);
      // 
      // btnCapture
      // 
      this.btnCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCapture.Location = new System.Drawing.Point(580, 2);
      this.btnCapture.Name = "btnCapture";
      this.btnCapture.Size = new System.Drawing.Size(75, 23);
      this.btnCapture.TabIndex = 1;
      this.btnCapture.Text = "Capture";
      this.btnCapture.UseVisualStyleBackColor = true;
      this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
      // 
      // txtURL
      // 
      this.txtURL.AllowDrop = true;
      this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtURL.Location = new System.Drawing.Point(28, 4);
      this.txtURL.Name = "txtURL";
      this.txtURL.Size = new System.Drawing.Size(501, 20);
      this.txtURL.TabIndex = 2;
      this.txtURL.Text = "http://studio.clipflair.net/?activity=Tutorial";
      this.txtURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtURL_KeyDown);
      // 
      // btnGo
      // 
      this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnGo.Location = new System.Drawing.Point(535, 2);
      this.btnGo.Name = "btnGo";
      this.btnGo.Size = new System.Drawing.Size(39, 23);
      this.btnGo.TabIndex = 3;
      this.btnGo.Text = "Go";
      this.btnGo.UseVisualStyleBackColor = true;
      this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
      // 
      // lblURL
      // 
      this.lblURL.AutoSize = true;
      this.lblURL.Location = new System.Drawing.Point(-3, 7);
      this.lblURL.Name = "lblURL";
      this.lblURL.Size = new System.Drawing.Size(32, 13);
      this.lblURL.TabIndex = 4;
      this.lblURL.Text = "URL:";
      // 
      // MainForm
      // 
      this.AcceptButton = this.btnGo;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(657, 273);
      this.Controls.Add(this.lblURL);
      this.Controls.Add(this.btnGo);
      this.Controls.Add(this.txtURL);
      this.Controls.Add(this.btnCapture);
      this.Controls.Add(this.webBrowser);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
      this.Text = "WebCapture";
      this.TopMost = true;
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.WebBrowser webBrowser;
    private System.Windows.Forms.Button btnCapture;
    private System.Windows.Forms.TextBox txtURL;
    private System.Windows.Forms.Button btnGo;
    private System.Windows.Forms.Label lblURL;
  }
}

