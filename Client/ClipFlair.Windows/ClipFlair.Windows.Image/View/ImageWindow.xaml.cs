//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20140615

using ClipFlair.UI.Dialogs;
using ClipFlair.Windows.Views;
using Utils.Extensions;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ClipFlair.Windows.Image;
using Ionic.Zip;

namespace ClipFlair.Windows
{

  public partial class ImageWindow : BaseWindow
  {

    #region ---- Initialization ---

    public ImageWindow()
    {
      View = new ImageView(); //must set the view first
      InitializeComponent();

      if (options != null)
        options.ImageWindow = this;

      imgContent.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(imgContent_MouseLeftButtonDown), true); //must pass "true" to handle events marked as already "handled"
      UpdateZoomControlsVisible();
    }

    #endregion

    #region --- View ---

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

    #region --- Zoom ---

    public void ZoomToFit()
    {
      imgContent.ZoomToFit();
    }

    private void UpdateZoomControlsVisible()
    {
      if (imgContent != null)
        imgContent.ZoomControlsAvailable = (ImageView.ActionTime == null && ImageView.ActionURL == null); //TODO: add a ZoomControlsAvailable property to the view too and AND its value here
    }

    #endregion

    #region --- Open local file dialog ---

    public bool OpenLocalFile()
    {
      bool done = imgContent.OpenLocalFile();
      if (done) Flipped = false; //flip back to front
      return done;
    }

    #endregion

    #region --- LoadOptions dialog ---

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

    #endregion

    #region --- Load image from stream ----

    public override void LoadContent(Stream stream, string title = "") //doesn't close stream
    {
      imgContent.Open(stream, title); //image filetype is autodetected from stream data
    }

    #endregion

    #region --- Load saved state ---

    public override void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      base.LoadOptions(zip, zipFolder);

      foreach (ZipEntry img in zip.SelectEntries("*.JPG", zipFolder))
      {
        LoadContent(img.OpenReader(), img.FileName);
        break; //just load the first one
      }
    }

    #endregion

    #region --- Save state ---

    public override void SaveOptions(ZipFile zip, string zipFolder = "")
    {
      base.SaveOptions(zip, zipFolder);

      //save image
      if (imgContent.ImageData != null)
        zip.AddEntry(zipFolder + "/" + imgContent.Filename, SaveImage); //SaveImage is a callback method
    }

    public void SaveImage(string entryName, Stream stream) //callback
    {
      Stream s = imgContent.ImageData;
      if (s != null)
      {
        s.Position = 0;
        s.CopyTo(stream);
      }
    }
        
    #endregion

    #region --- Click Action (goto ActiveURL and/or ActiveTime) ---

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
