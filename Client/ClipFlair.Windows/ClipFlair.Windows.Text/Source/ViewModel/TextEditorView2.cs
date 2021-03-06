﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorView2.cs
//Version: 20140615

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")] //Using the correct contract name (compared to the older TextEditorView that had a typo ["Views" instead of "View"])
  public class TextEditorView2 : BaseView, ITextEditor2
  {
    public TextEditorView2()
    {
    }

    public TextEditorView2(ITextEditor view) : base(view)
    {
      if (view == null) return;

      Source = view.Source;
      ToolbarVisible = view.ToolbarVisible;
      Editable = view.Editable;

      //Dirty flag
      Dirty = view.Dirty; //must be last - any overriden versions should also set this
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
