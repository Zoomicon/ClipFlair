//Filename: ImageView.cs
//Version: 20120824

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class ImageView: BaseView, IImageViewer
  {
    public ImageView()
    {
      //can set fields directly here since we don't yet have any PropertyChanged listeners
      source = IImageViewerDefaults.DefaultSource;
    }
        
    #region IImageViewer

    private Uri source;

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
