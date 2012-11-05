//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseView.cs
//Version: 20121102

using ClipFlair.Windows.Views;

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
    private double width = IViewDefaults.DefaultWidth;
    private double height = IViewDefaults.DefaultHeight;
    private double zoom = IViewDefaults.DefaultZoom;
    private bool moveable = IViewDefaults.DefaultMoveable;
    private bool resizable = IViewDefaults.DefaultResizable;
    private bool zoomable = IViewDefaults.DefaultZoomable;

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

    [DataMember]
    public double Zoom
    {
      get { return zoom; }
      set
      {
        if (value != zoom)
        {
          zoom = value;
          RaisePropertyChanged(IViewProperties.PropertyZoom);
        }
      }
    }

    [DataMember]
    public bool Moveable
    {
      get { return moveable; }
      set
      {
        if (value != moveable)
        {
          moveable = value;
          RaisePropertyChanged(IViewProperties.PropertyMoveable);
        }
      }
    }
    
    [DataMember]
    public bool Resizable
    {
      get { return resizable; }
      set
      {
        if (value != resizable)
        {
          resizable = value;
          RaisePropertyChanged(IViewProperties.PropertyResizable);
        }
      }
    }

    [DataMember]
    public bool Zoomable
    {
      get { return zoomable; }
      set
      {
        if (value != zoomable)
        {
          zoomable = value;
          RaisePropertyChanged(IViewProperties.PropertyZoomable);
        }
      }
    }   
    
    #endregion

  }

}
