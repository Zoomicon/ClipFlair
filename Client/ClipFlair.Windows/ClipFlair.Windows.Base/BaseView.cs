//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseView.cs
//Version: 20121206

using ClipFlair.Windows.Views;

using System.ComponentModel;
using System;
using System.Runtime.Serialization;
using System.Windows;

namespace ClipFlair.Windows.Views
{

  [DataContract(Namespace = "http://clipflair.net/Contracts/View")]
  public class BaseView: IView
  {

    public BaseView()
    {
      SetDefaults(); //ancestors don't need to call "SetDefaults" at their constructors since this constructor is always called
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged(string PropertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
    }

    #endregion

    #region Fields

    private Uri optionsSource;
    private string title;
    private Point position;
    private double width;
    private double height;
    private double zoom;
    private double opacity;
    private bool moveable;
    private bool resizable;
    private bool zoomable;

    #endregion

    #region Properties

    [DataMember]
    [DefaultValue(IViewDefaults.DefaultOptionsSource)]
    public Uri OptionsSource
    {
      get { return optionsSource; }
      set
      {
        if (value != optionsSource)
        {
          optionsSource = value;
          RaisePropertyChanged(IViewProperties.PropertyOptionsSource);
        }
      }
    }

    [DataMember]
    [DefaultValue(IViewDefaults.DefaultTitle)]
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
    //[DefaultValue(IViewDefaults.DefaultPosition)]
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
    [DefaultValue(IViewDefaults.DefaultWidth)]
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
    [DefaultValue(IViewDefaults.DefaultHeight)]
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
    [DefaultValue(IViewDefaults.DefaultZoom)]
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
    [DefaultValue(IViewDefaults.DefaultOpacity)]
    public double Opacity
    {
      get { return opacity; }
      set
      {
        if (value != opacity)
        {
          opacity = value;
          RaisePropertyChanged(IViewProperties.PropertyOpacity);
        }
      }
    }

    [DataMember]
    [DefaultValue(IViewDefaults.DefaultMoveable)]
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
    [DefaultValue(IViewDefaults.DefaultResizable)]
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
    [DefaultValue(IViewDefaults.DefaultZoomable)]
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

    #region Methods

    public virtual void SetDefaults()
    { //Must set property values, not fields

      //BaseView defaults
      OptionsSource = IViewDefaults.DefaultOptionsSource;
      Title = IViewDefaults.DefaultTitle;
      Position = IViewDefaults.DefaultPosition;
      Width = IViewDefaults.DefaultWidth;
      Height = IViewDefaults.DefaultHeight;
      Zoom = IViewDefaults.DefaultZoom;
      Opacity = IViewDefaults.DefaultOpacity;
      Moveable = IViewDefaults.DefaultMoveable;
      Resizable = IViewDefaults.DefaultResizable;
      Zoomable = IViewDefaults.DefaultZoomable;
    }

    [OnDeserializing()] //this is called before deserialization occurs to set defaults for any properties that may be missing at the serialized data (e.g. from older serialized state)
    public void OnDeserializing(StreamingContext context) //Note that this cannot be a virtual method
    {
      SetDefaults(); //this one is a virtual method overriden by descendents (they should call "base.SetDefaults()" first there, then override any BaseView default values and then set defaults for their own properties)
    }

    #endregion

  }

}
