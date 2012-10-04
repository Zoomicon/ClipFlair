﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageView.cs
//Version: 20121004

using ClipFlair.Models.Views;

using System;
using System.Runtime.Serialization;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")]
  public class ImageView: BaseView, IImageViewer
  {
    public ImageView()
    {
    }
        
    #region IImageViewer

    //can set fields directly here or at the constructor
    private Uri source = IImageViewerDefaults.DefaultSource;

    [DataMember]
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
