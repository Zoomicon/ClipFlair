//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseView.cs
//Version: 20121030

using ClipFlair.Models.Views;

using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/Views")]
  public class BaseView: IView
  {

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged(string PropertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
    }

    #endregion

    #region Fields

    private string title = IViewDefaults.DefaultTitle;
    private Point position = IViewDefaults.DefaultPosition;
    private double scale = IViewDefaults.DefaultScale;
    private double width = IViewDefaults.DefaultWidth;
    private double height = IViewDefaults.DefaultHeight;

    #endregion

    #region Properties

    [DataMember]
    public string Title
    {
      get { return title; }
      set
      {
        if (value != title)
        {
          title = value;
          RaisePropertyChanged(IViewProperties.PropertyTitle);
        }
      }
    }    

    [DataMember]
    public Point Position
    {
      get { return position; }
      set
      {
        if (value != position)
        {
          position = value;
          RaisePropertyChanged(IViewProperties.PropertyPosition);
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
          RaisePropertyChanged(IViewProperties.PropertyScale);
        }
      }
    }

    [DataMember]
    public double Width
    {
      get { return width; }
      set
      {
        if (value != width)
        {
          width = value;
          RaisePropertyChanged(IViewProperties.PropertyWidth);
        }
      }
    }

    [DataMember]
    public double Height
    {
      get { return height; }
      set
      {
        if (value != height)
        {
          height = value;
          RaisePropertyChanged(IViewProperties.PropertyHeight);
        }
      }
    }

    #endregion

  }

}
