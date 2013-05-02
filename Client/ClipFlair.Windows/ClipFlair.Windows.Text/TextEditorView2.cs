//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorView2.cs
//Version: 20130501

using System;
using System.ComponentModel;
using System.Windows.Browser;
using System.Runtime.Serialization;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")] //Using the correct contract name (compared to the older TextEditorView that had a typo ["Views" instead of "View"])
  public class TextEditorView2 : BaseView, ITextEditor
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
      RTL = view.RTL;      
    }

    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private TimeSpan time;
    private bool toolbarVisible;
    private bool editable;
    private bool rtl;

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

    [DataMember(Order = 0)] //Order=0 means this gets deserialized after other fields (that don't have order set)
    //[DefaultValue(TextEditorDefaults.DefaultTime)] //can't use static fields here (and we're forced to use one for TimeSpan unfortunately, doesn't work with const)
    public virtual TimeSpan Time
    {
      get { return time; }
      set
      {
        if (value != time)
        {
          time = value;
          RaisePropertyChanged(ITextEditorProperties.PropertyTime);
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

    [DataMember]
    [DefaultValue(TextEditorDefaults.DefaultRTL)]
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
    {
      TextEditorDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
