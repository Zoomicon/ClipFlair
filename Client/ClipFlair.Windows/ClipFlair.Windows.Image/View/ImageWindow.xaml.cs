//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20140613

using ClipFlair.UI.Dialogs;
using ClipFlair.Windows.Views;
using Utils.Extensions;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ClipFlair.Windows.Image;

namespace ClipFlair.Windows
{

  public partial class ImageWindow : BaseWindow
  {
    public ImageWindow()
    {
      View = new ImageView(); //must set the view first
      InitializeComponent();

      if (options != null)
        options.ImageWindow = this;

      imgContent.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(imgContent_MouseLeftButtonDown), true); //must pass "true" to handle events marked as already "handled"
      UpdateZoomControlsVisible();
    }

    #region View

    public IImageViewer ImageView
    {
      get { return (IImageViewer)View; }
      set { View = value; }
    }

    public override IView View
    {
      get
      {
        return base.View;
      }
      set
      {
        base.View = value;
        if (options != null)
          options.ImageWindow = this;
      }
    }
    
    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

      if (imgContent == null)
        return;

      switch (e.PropertyName)
      {
        case null: //BaseWindow's "View" property's setter calls View_PropertyChanged with PropertyName=null to signify multiple properties have changed (initialized with default values that is)
          UpdateZoomControlsVisible();
          imgContent.ContentZoomToFit = ImageView.ContentZoomToFit;
          break;
        case IImageViewerProperties.PropertyActionTime:
        case IImageViewerProperties.PropertyActionURL:          
          UpdateZoomControlsVisible();
          break;
        case IImageViewerProperties.PropertyContentZoomToFit:
          imgContent.ContentZoomToFit = ImageView.ContentZoomToFit;
          break;
      }
    }

    #endregion

    #region --- Methods ---

    public void ZoomToFit()
    {
      imgContent.ZoomToFit();
    }

    private void UpdateZoomControlsVisible()
    {
      if (imgContent != null)
        imgContent.ZoomControlsAvailable = (ImageView.ActionTime == null && ImageView.ActionURL == null); //TODO: add a ZoomControlsAvailable property to the view too and AND its value here
    }

    public bool OpenLocalFile()
    {
      bool done = imgContent.OpenLocalFile();
      if (done) Flipped = false; //flip back to front
      return done;
    }

    public override string LoadFilter
    {
      get
      {
        return base.LoadFilter + "|" + ImageWindowFactory.LOAD_FILTER;
      }
    }

    public override void LoadOptions(FileInfo f)
    {
      if (!f.Name.EndsWith(new string[] { CLIPFLAIR_EXTENSION, CLIPFLAIR_ZIP_EXTENSION }))
        imgContent.Open(f);
      else
        base.LoadOptions(f);
    }

    public override void LoadContent(Stream stream, string title = "") //doesn't close stream
    {
      imgContent.Open(stream); //image filetype is autodetected from stream data
    }

    #endregion

    #region --- Events ---

    private void imgContent_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (ImageView.ActionURL != null)
      {
        BrowserDialog.Show(ImageView.ActionURL);

        e.Handled = true; //event should already be handled by inner content, but marking it as handled anyway in case inner content hasn't
      }

      if (ImageView.ActionTime != null)
      {
        ImageView.Time = (TimeSpan)ImageView.ActionTime;
        e.Handled = true; //event should already be handled by inner content, but marking it as handled anyway in case inner content hasn't
      }
    }

    #endregion

  }
}
