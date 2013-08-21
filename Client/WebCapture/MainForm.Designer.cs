﻿namespace WebCapture
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.webBrowser = new System.Windows.Forms.WebBrowser();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.btnWebBrowserSize = new System.Windows.Forms.ToolStripDropDownButton();
      this.mnuSize640x480 = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSize800x600 = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSize1024x768 = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSize1280x1024 = new System.Windows.Forms.ToolStripMenuItem();
      this.panelToolbar = new System.Windows.Forms.Panel();
      this.btnGo = new System.Windows.Forms.Button();
      this.lblURL = new System.Windows.Forms.Label();
      this.txtURL = new System.Windows.Forms.TextBox();
      this.btnCapture = new System.Windows.Forms.Button();
      this.statusStrip.SuspendLayout();
      this.panelToolbar.SuspendLayout();
      this.SuspendLayout();
      // 
      // webBrowser
      // 
      this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.webBrowser.Location = new System.Drawing.Point(0, 29);
      this.webBrowser.Margin = new System.Windows.Forms.Padding(0);
      this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser.Name = "webBrowser";
      this.webBrowser.ScriptErrorsSuppressed = true;
      this.webBrowser.ScrollBarsEnabled = false;
      this.webBrowser.Size = new System.Drawing.Size(827, 361);
      this.webBrowser.TabIndex = 0;
      this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
      this.webBrowser.SizeChanged += new System.EventHandler(this.webBrowser_SizeChanged);
      // 
      // statusStrip
      // 
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnWebBrowserSize});
      this.statusStrip.Location = new System.Drawing.Point(0, 390);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new System.Drawing.Size(827, 22);
      this.statusStrip.TabIndex = 5;
      this.statusStrip.Text = "statusStrip1";
      // 
      // btnWebBrowserSize
      // 
      this.btnWebBrowserSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnWebBrowserSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSize640x480,
            this.mnuSize800x600,
            this.mnuSize1024x768,
            this.mnuSize1280x1024});
      this.btnWebBrowserSize.Image = ((System.Drawing.Image)(resources.GetObject("btnWebBrowserSize.Image")));
      this.btnWebBrowserSize.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnWebBrowserSize.Name = "btnWebBrowserSize";
      this.btnWebBrowserSize.Size = new System.Drawing.Size(100, 20);
      this.btnWebBrowserSize.Text = "webBrowser size";
      // 
      // mnuSize640x480
      // 
      this.mnuSize640x480.Name = "mnuSize640x480";
      this.mnuSize640x480.Size = new System.Drawing.Size(128, 22);
      this.mnuSize640x480.Text = "640x480";
      this.mnuSize640x480.Click += new System.EventHandler(this.mnuSize640x480_Click);
      // 
      // mnuSize800x600
      // 
      this.mnuSize800x600.Name = "mnuSize800x600";
      this.mnuSize800x600.Size = new System.Drawing.Size(128, 22);
      this.mnuSize800x600.Text = "800x600";
      this.mnuSize800x600.Click += new System.EventHandler(this.mnuSize800x600_Click);
      // 
      // mnuSize1024x768
      // 
      this.mnuSize1024x768.Name = "mnuSize1024x768";
      this.mnuSize1024x768.Size = new System.Drawing.Size(128, 22);
      this.mnuSize1024x768.Text = "1024x768";
      this.mnuSize1024x768.Click += new System.EventHandler(this.mnuSize1024x768_Click);
      // 
      // mnuSize1280x1024
      // 
      this.mnuSize1280x1024.Name = "mnuSize1280x1024";
      this.mnuSize1280x1024.Size = new System.Drawing.Size(128, 22);
      this.mnuSize1280x1024.Text = "1280x1024";
      this.mnuSize1280x1024.Click += new System.EventHandler(this.mnuSize1280x1024_Click);
      // 
      // panelToolbar
      // 
      this.panelToolbar.Controls.Add(this.btnGo);
      this.panelToolbar.Controls.Add(this.lblURL);
      this.panelToolbar.Controls.Add(this.txtURL);
      this.panelToolbar.Controls.Add(this.btnCapture);
      this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelToolbar.Location = new System.Drawing.Point(0, 0);
      this.panelToolbar.Name = "panelToolbar";
      this.panelToolbar.Size = new System.Drawing.Size(827, 26);
      this.panelToolbar.TabIndex = 6;
      // 
      // btnGo
      // 
      this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnGo.Location = new System.Drawing.Point(704, 1);
      this.btnGo.Name = "btnGo";
      this.btnGo.Size = new System.Drawing.Size(39, 23);
      this.btnGo.TabIndex = 9;
      this.btnGo.Text = "Go";
      this.btnGo.UseVisualStyleBackColor = true;
      this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
      // 
      // lblURL
      // 
      this.lblURL.AutoSize = true;
      this.lblURL.Location = new System.Drawing.Point(3, 6);
      this.lblURL.Name = "lblURL";
      this.lblURL.Size = new System.Drawing.Size(29, 13);
      this.lblURL.TabIndex = 8;
      this.lblURL.Text = "URL";
      // 
      // txtURL
      // 
      this.txtURL.AllowDrop = true;
      this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtURL.Location = new System.Drawing.Point(38, 3);
      this.txtURL.Name = "txtURL";
      this.txtURL.Size = new System.Drawing.Size(660, 20);
      this.txtURL.TabIndex = 6;
      this.txtURL.Text = "http://studio.clipflair.net/?activity=Tutorial";
      this.txtURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtURL_KeyDown);
      // 
      // btnCapture
      // 
      this.btnCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCapture.Location = new System.Drawing.Point(749, 1);
      this.btnCapture.Name = "btnCapture";
      this.btnCapture.Size = new System.Drawing.Size(75, 23);
      this.btnCapture.TabIndex = 5;
      this.btnCapture.Text = "Capture";
      this.btnCapture.UseVisualStyleBackColor = true;
      this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(827, 412);
      this.Controls.Add(this.webBrowser);
      this.Controls.Add(this.panelToolbar);
      this.Controls.Add(this.statusStrip);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
      this.Text = "WebCapture";
      this.TopMost = true;
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.panelToolbar.ResumeLayout(false);
      this.panelToolbar.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.WebBrowser webBrowser;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.Panel panelToolbar;
    private System.Windows.Forms.Button btnGo;
    private System.Windows.Forms.Label lblURL;
    private System.Windows.Forms.TextBox txtURL;
    private System.Windows.Forms.Button btnCapture;
    private System.Windows.Forms.ToolStripDropDownButton btnWebBrowserSize;
    private System.Windows.Forms.ToolStripMenuItem mnuSize640x480;
    private System.Windows.Forms.ToolStripMenuItem mnuSize800x600;
    private System.Windows.Forms.ToolStripMenuItem mnuSize1024x768;
    private System.Windows.Forms.ToolStripMenuItem mnuSize1280x1024;
  }
}

