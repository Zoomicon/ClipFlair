//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewerDefaults.cs
//Version: 20121203

using System;
using System.Windows;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class IImageViewerDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Image";
    
    #endregion

    public const Uri DefaultSource = null;
    public const Stretch DefaultStretch = Stretch.Uniform;
  }

}