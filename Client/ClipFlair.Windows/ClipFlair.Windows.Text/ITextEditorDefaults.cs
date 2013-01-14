//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ITextEditorDefaults.cs
//Version: 20130114

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class ITextEditorDefaults
  {
    #region IView defaults - overrides
    
    public const string DefaultTitle = "Text";
    public const double DefaultWidth = 650;
    public const double DefaultHeight = 630;
    
    #endregion

    public const Uri DefaultSource = null;
    public const bool DefaultToolbarVisible = true;
    public const bool DefaultEditable = true;
    public const bool DefaultRTL = false;
  }

}