﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: NewsView.cs
//Version: 20141017

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Browser;

namespace ClipFlair.Windows.Views
{

  [ScriptableType]
  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class NewsView: BaseView, INewsReader
  {
    public NewsView()
    {
    }

    #region Fields

    //fields are initialized via respective properties at "SetDefaults" method
    private Uri source;
    private TimeSpan refreshInterval;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(NewsReaderDefaults.DefaultSource)]
    public Uri Source
    {
      get { return source; }
      set
      {
        if (value != source)
        {
          source = value;
          RaisePropertyChanged(INewsReaderProperties.PropertySource);
          Dirty = true;
        }
      }
    }

    [DataMember]
    //[DefaultValue(NewsReaderDefaults.DefaultRefreshInterval)]
    public TimeSpan RefreshInterval
    {
      get { return refreshInterval; }
      set
      {
        if (value != refreshInterval)
        {
          refreshInterval = value;
          RaisePropertyChanged(INewsReaderProperties.PropertyRefreshInterval);
          Dirty = true;
        }
      }
    }
    
    #endregion

    #region Methods
  
    public override void SetDefaults() //do not call at constructor, BaseView does it already
    {
      NewsReaderDefaults.SetDefaults(this); //this makes sure we set public properties (invoking "set" accessors), not fields //It also calls ViewDefaults.SetDefaults
    }

    #endregion

  }

}
