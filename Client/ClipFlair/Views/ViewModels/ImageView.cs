//Filename: ImageView.cs
//Version: 20120831

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class ImageView: BaseView, IImageViewer
  {
    public ImageView()
    {
    }
        
    #region IImageViewer

    //can set fields directly here or at the constructor
    private Uri source = IImageViewerDefaults.DefaultSource;

    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(IImageViewerProperties.PropertySource);
        }
      }
    }

    #endregion
  }

}
