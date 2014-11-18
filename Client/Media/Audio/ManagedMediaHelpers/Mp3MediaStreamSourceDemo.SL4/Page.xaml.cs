//-----------------------------------------------------------------------
// <copyright file="Page.xaml.cs" company="Larry Olson">
// (c) Copyright Larry Olson.
// This source is subject to the Microsoft Public License (Ms-PL)
// See http://code.msdn.microsoft.com/ManagedMediaHelpers/Project/License.aspx
// All other rights reserved.
// </copyright>
//
// Edited by George Birbilis (http://zoomicon.com)
//-----------------------------------------------------------------------

//#define AUTOPLAY
//#define PRELOAD

namespace Mp3MediaStreamSourceDemo
{
  using Media;
  using System;
  using System.IO;
  using System.Windows;
  using System.Windows.Controls;

    /// <summary>
    /// A Page of a Silvelight Application.
    /// </summary>
    public partial class Page : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the Page class.
        /// </summary>
        public Page()
        {
            InitializeComponent();

            me.Volume = 1.0; //set max volume
            
            #if !AUTOPLAY
            me.AutoPlay = false;
            #endif
        }

        /// <summary>
        /// Event handler for the Button on the Page.
        /// </summary>
        /// <param name="sender">
        /// The button which was clicked.
        /// </param>
        /// <param name="e">
        /// The state when this event was generated.
        /// </param>
        private void OpenMedia(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            Stream data = ofd.File.OpenRead();

            #if PRELOAD
            Stream m = new MemoryStream((int)ofd.File.Length);
            data.CopyTo(m);
            m.Position = 0;
            data = m;
            #endif

            Mp3MediaStreamSource mediaSource = new Mp3MediaStreamSource(data);      
            me.SetSource(mediaSource);

            #if !AUTOPLAY
            me.MediaOpened += (s, ev) =>
            {
                //me.Position = TimeSpan.Zero;
                me.Stop(); //this stops current playback (if any) and rewinds back to start
                //me.PlaybackRate = 1.0;
                try
                {
                    me.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            #endif
        }
    }
}
