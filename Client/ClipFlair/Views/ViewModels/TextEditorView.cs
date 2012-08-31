//Filename: TextEditorView.cs
//Version: 20120831

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class TextEditorView : BaseView, ITextEditor
  {
    public TextEditorView()
    {
    }

    #region ITextEditor

    #region Fields

    //can set fields directly here or at the constructor
    private Uri source = ITextEditorDefaults.DefaultSource;
    private bool toolbarVisible = ITextEditorDefaults.DefaultToolbarVisible;

    #endregion

    #region Properties

    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(ITextEditorProperties.PropertySource);
        }
      }
    }

    public bool ToolbarVisible
    {
      get { return toolbarVisible; }
      set
      {
        if (value != toolbarVisible)
        {
          toolbarVisible = value;
          RaisePropertyChanged(ITextEditorProperties.PropertyToolbarVisible);
        }
      }
    }

    #endregion

    #endregion
  }

}
