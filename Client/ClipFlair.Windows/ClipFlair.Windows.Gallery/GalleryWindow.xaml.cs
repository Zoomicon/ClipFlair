﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: GalleryWindow.xaml.cs
//Version: 20130828

using ClipFlair.Windows.Views;
using ClipFlair.Windows.Gallery.Commands;

using System.Windows;
using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows
{

  public partial class GalleryWindow : BaseWindow
  {
    public GalleryWindow()
    {
      View = new GalleryView(); //must set the view first
      InitializeComponent();
      
      //IsAnimated = false; //workarround: Can't turn over Pivot viewer when using 3D-style rotation, thowing transformation class cast errors
    }

    #region View

    public IGallery GalleryView
    {
      get { return (IGallery)View; }
      set { View = value; }
    }

    #endregion

    #region Events

    /*
    public override void ShowOptions() //workarround: Can't turn over Pivot viewer when using 3D-style rotation, thowing transformation class cast errors (the IsAnimated=false isn't enough, need this too, else at resize etc. when flipped will throw exceptions)
    {
      if (!Flipped)
        pivot.Visibility = Visibility.Collapsed;

      base.ShowOptions();

      if (!Flipped)
        pivot.Visibility = Visibility.Visible;
    }
*/

    private void GalleryItemAdorner_CommandsRequested(object sender, PivotViewerCommandsRequestedEventArgs e)
    {
      //if (e.IsItemSelected)
      {
        e.Commands.Add(new InfoCommand((PivotViewerItem)e.Item));
        //e.Commands.Add(new ShareCommand());
        //e.Commands.Add(new DownloadCommand());
        //e.Commands.Add(new OpenCommand());
      }
    }

    #endregion

  }
}
