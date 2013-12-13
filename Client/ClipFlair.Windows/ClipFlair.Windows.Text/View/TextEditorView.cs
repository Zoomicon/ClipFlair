﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorView.cs
//Version: 20131213

//NOTE: Do not add any more properties to this view, kept for backwards compatibility. Use TextEditorView2 instead.

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")] //"Views" is a typo here in the contract (kept as is for backwards compatibility), replaced with "View" at TextEditorView2
  public class TextEditorView : BaseView, ITextEditor
  {
    public TextEditorView()
    {
    }

    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private bool toolbarVisible;
    private bool editable;
  
    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(TextEditorDefaults.DefaultSource)]
    public virtual Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(ITextEditorProperties.PropertySource);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(TextEditorDefaults.DefaultToolbarVisible)]
    public virtual bool ToolbarVisible
    {
      get { return toolbarVisible; }
      set
      {
        if (value != toolbarVisible)
        {
          toolbarVisible = value;
          RaisePropertyChanged(ITextEditorProperties.PropertyToolbarVisible);
          Dirty = true;
        }
      }
    }

    [DataMember]
    [DefaultValue(TextEditorDefaults.DefaultEditable)]
    public bool Editable
    {
      get { return editable; }
      set
      {
        if (value != editable)
        {
          editable = value;
          RaisePropertyChanged(ITextEditorProperties.PropertyEditable);
          Dirty = true;
        }
      }
    }

    #endregion

    #region Methods

    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      TextEditorDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
