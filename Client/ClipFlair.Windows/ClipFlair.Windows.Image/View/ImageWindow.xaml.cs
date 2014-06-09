//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20140609

using ClipFlair.UI.Dialogs;
using ClipFlair.Windows.Views;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

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

    public void ZoomToFit()
    {
      imgContent.ZoomToFit();
    }

    private void UpdateZoomControlsVisible()
    {
      if (imgContent != null)
        imgContent.ZoomControlsAvailable = (ImageView.ActionTime == null && ImageView.ActionURL == null); //TODO: add a ZoomControlsAvailable property to the view too and AND its value here
    }

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

  }
}
