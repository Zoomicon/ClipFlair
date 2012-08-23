//Filename: TextEditorView.cs
//Version: 20120823

using ClipFlair.Models.Views;

using System;

namespace ClipFlair.Views
{
  public class TextEditorView: BaseView, ITextEditor
  {
    public TextEditorView()
    {
      Source = ITextEditorDefaults.DefaultSource;
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
