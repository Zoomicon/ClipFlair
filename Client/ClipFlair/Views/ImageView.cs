//Filename: ImageView.cs
//Version: 20120814

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class ImageView: BaseView, IImageViewer
  {
    public ImageView()
    {
      Source = IImageViewerDefaults.DefaultSource;
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
