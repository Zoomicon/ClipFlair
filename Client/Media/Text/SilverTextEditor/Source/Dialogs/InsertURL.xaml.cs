﻿//Filename: InsertURL.xaml.cs
//Version: 20150206

using System;
using System.Windows;
using Compatibility;

namespace SilverTextEditor
{
    public partial class InsertURL : ChildWindowExt
    {
        public InsertURL(string selectedText)
        {
            InitializeComponent();

            txtURLDesc.Text = selectedText;

            //hooking up a handler to the ChildWindow closing event
            //handler here validates that the URL is a well-formed http, https or ftp URL
            this.Closing += (s, e) =>
            {
                if (this.DialogResult.Value)
                {
                    if (txtURL.Text.Length == 0)
                        e.Cancel = true;
                    else
                    {
                        //if URL does not specify http, https or ftp default to http
                        if ((txtURL.Text.IndexOf("http://") == -1) &&
                            (txtURL.Text.IndexOf("https://") == -1) &&
                            (txtURL.Text.IndexOf("ftp://") == -1)
                        )
                            txtURL.Text = "http://" + txtURL.Text;

                        if (!Uri.IsWellFormedUriString(txtURL.Text,UriKind.Absolute))
                            e.Cancel = true;
                    }
                    if (e.Cancel)
                        MessageBox.Show("URL is empty or not valid.\nPlease try again");
                }
            };
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

