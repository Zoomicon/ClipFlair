//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorView.cs
//Version: 20121206

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")]
  public class TextEditorView : BaseView, ITextEditor
  {
    public TextEditorView()
    {
    }

    #region Fields

    //fields are initialized at "SetDefaults" method
    private Uri source;
    private bool toolbarVisible;
    private bool editable;
    private bool rtl;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(ITextEditorDefaults.DefaultSource)]
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
    [DefaultValue(ITextEditorDefaults.DefaultToolbarVisible)]
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
    [DefaultValue(ITextEditorDefaults.DefaultEditable)]
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
    [DefaultValue(ITextEditorDefaults.DefaultRTL)]
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

    #region Methods

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    { //Must set property values, not fields

      //BaseView defaults and overrides
      base.SetDefaults();
      Title = ITextEditorDefaults.DefaultTitle;

      //TextEditorView defaults
      Source = ITextEditorDefaults.DefaultSource;
      ToolbarVisible = ITextEditorDefaults.DefaultToolbarVisible;
      Editable = ITextEditorDefaults.DefaultEditable;
      RTL = ITextEditorDefaults.DefaultRTL;
    }

    #endregion

  }

}
