//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BrowserView.cs
//Version: 

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class BrowserView: BaseView, IBrowser
  {
    public BrowserView()
    {
    }

    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(BrowserDefaults.DefaultSource)]
    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(IBrowserProperties.PropertySource);
          Dirty = true;
        }
      }
    }

    #endregion

    #region Methods
  
    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      BrowserDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
