//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ITextEditorDefaults.cs
//Version: 20121106

using System;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  public static class ITextEditorDefaults
  {
    #region IView defaults - overrides
    
    public static string DefaultTitle = "Text";
    
    #endregion

    public static Uri DefaultSource = null;
    public static bool DefaultToolbarVisible = true;
    public static bool DefaultEditable = true;
    public static bool DefaultRTL = false;
  }

}