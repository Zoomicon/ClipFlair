//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ActivityView.cs
//Version: 20121030

using ClipFlair.Models.Views;

using System;
using System.Runtime.Serialization;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")]
  public class ActivityView: BaseView, IActivity
  {
    public ActivityView()
    {
    }
        
    #region IActivity

    #region Fields

    //can set fields directly here or at constructor
    private Uri source = IActivityDefaults.DefaultSource;
    private TimeSpan time = IActivityDefaults.DefaultTime;

    private Point offset = IActivityDefaults.DefaultOffset;
    private double scale = IActivityDefaults.DefaultScale;

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
          RaisePropertyChanged(IActivityProperties.PropertySource);
        }
      }
    }

    [DataMember]
    public TimeSpan Time
    {
      get { return time; }
      set
      {
        if (value != time)
        {
          time = value;
          RaisePropertyChanged(IActivityProperties.PropertyTime);
        }
      }
    }

    [DataMember]
    public Point Offset
    {
      get { return offset; }
      set
      {
        if (value != offset)
        {
          offset = value;
          RaisePropertyChanged(IActivityProperties.PropertyOffset);
        }
      }
    }

    [DataMember]
    public double Scale
    {
      get { return scale; }
      set
      {
        if (value != scale)
        {
          scale = value;
          RaisePropertyChanged(IActivityProperties.PropertyScale);
        }
      }
    }

    #endregion

    #endregion
  }

}
