//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorView.cs
//Version: 20121106

using System;
using System.Runtime.Serialization;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")]
  public class TextEditorView : BaseView, ITextEditor
  {
    public TextEditorView()
    {
      //BaseView defaults - overrides
      Title = ITextEditorDefaults.DefaultTitle;
    }

    #region Fields

    //can set fields directly here or at the constructor
    private Uri source = ITextEditorDefaults.DefaultSource;
    private bool toolbarVisible = ITextEditorDefaults.DefaultToolbarVisible;
    private bool editable = ITextEditorDefaults.DefaultEditable;
    private bool rtl = ITextEditorDefaults.DefaultRTL;

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

    [DataMember]
    public bool Editable
    {
      get { return editable; }
      set
      {
        if (value != editable)
        {
          editable = value;
          RaisePropertyChanged(ITextEditorProperties.PropertyEditable);
        }
      }
    }

    [DataMember]
    public bool RTL
    {
      get { return rtl; }
      set
      {
        if (value != rtl)
        {
          rtl = value;
          RaisePropertyChanged(ITextEditorProperties.PropertyRTL);
        }
      }
    }

    #endregion

  }

}
