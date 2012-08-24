//Filename: TextEditorView.cs
//Version: 20120824

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class TextEditorView: BaseView, ITextEditor
  {
    public TextEditorView()
    {
      //can set fields directly here since we don't yet have any PropertyChanged listeners
      source = ITextEditorDefaults.DefaultSource;
      toolbarVisible = ITextEditorDefaults.DefaultToolbarVisible;
    }
        
    #region ITextEditor

    private Uri source;
    private bool toolbarVisible;

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
  }

}
