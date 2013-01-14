//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IImageViewerDefaults.cs
//Version: 20130114

using System;
using System.Windows;
using System.Windows.Media;

namespace ClipFlair.Windows.Views
{

  public static class IImageViewerDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Image";
    public const double DefaultWidth = 400;
    public const double DefaultHeight = 400;
        
    #endregion

    public const Uri DefaultSource = null;
    public const Stretch DefaultStretch = Stretch.Uniform;
  }

}