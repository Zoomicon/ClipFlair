//Filename: TextEditorView.cs
//Version: 20120910

using ClipFlair.Models.Views;

using System;
using System.Runtime.Serialization;

namespace ClipFlair.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")]
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

    [DataMember]
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

    [DataMember]
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
