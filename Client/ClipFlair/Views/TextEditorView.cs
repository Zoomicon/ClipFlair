//Filename: TextEditorView.cs
//Version: 20120814

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

    #endregion
  }

}
