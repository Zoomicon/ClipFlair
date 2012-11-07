//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseWindow.xaml.cs
//Version: 20121106

using ClipFlair.Utils.Bindings;
using ClipFlair.Windows.Views;

using SilverFlow.Controls;

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace ClipFlair.Windows
{
  
  [ContentProperty("FrontContent")]
  public partial class BaseWindow : FloatingWindow
  {

    public BaseWindow()
    {
      if (Application.Current.Host.Settings.EnableGPUAcceleration) //GPU acceleration can been turned on at HTML/ASPX page or at OOB settings for OOB apps
        CacheMode = new BitmapCache(); //must do this before setting the "Scale" property, since it will set "RenderAtScale" property of the BitmapCache

      ShowMaximizeButton = false; //!!! (till we fix it to resize to current visible view area and to allow moving the window in that case only [when it's not same size as parent])

      InitializeComponent(); //this can override the "ShowMaximize" button, or set the "Scale" property, so must do last

      HelpRequested += (s,e) => {
        MessageBox.Show("Help not available yet - see http://ClipFlair.net for contact info");
      };

      OptionsRequested += (s, e) =>
      {
        //try to set focus to front content so that changes to property editboxes at the back content are applied
        if (!Focus())
        {
          // If the Focus() fails it means there is no focusable element in the front content. In this case we set IsTabStop to true to enable keyboard functionality for the container.
          IsTabStop = true;
          Focus();
          //keeping tab stop functionality for future back to front flips
        }

        FlipPanel.IsFlipped = !FlipPanel.IsFlipped;
      };

      //Bind to ancestor properties
      BindingUtils.RegisterForNotification("Title", this, (d, e) => { if (View != null) { View.Title = (string)e.NewValue; } });
      BindingUtils.RegisterForNotification("Position", this, (d, e) => { if (View != null) { View.Position = (Point)e.NewValue; } });
      BindingUtils.RegisterForNotification("Width", this, (d, e) => { if (View != null) { View.Width = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("Height", this, (d, e) => { if (View != null) { View.Height = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("Scale", this, (d, e) => { if (View != null) { View.Zoom = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("MoveEnabled", this, (d, e) => { if (View != null) { View.Moveable = (bool)e.NewValue; } });
      BindingUtils.RegisterForNotification("ResizeEnabled", this, (d, e) => { if (View != null) { View.Resizable = (bool)e.NewValue; } });
      BindingUtils.RegisterForNotification("Scalable", this, (d, e) => { if (View != null) { View.Zoomable = (bool)e.NewValue; } });
    }
 
    ~BaseWindow()
    {
      View = null; //unregister PropertyChangedEventHandler
    }
    
    #region Properties

    public IView View
    {
      get { return (IView)DataContext; }
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          ((IView)DataContext).PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged); //IView inherits from INotifyPropertyChanged
        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
        //set the new view (must do last)
        DataContext = value;
      }
    }

    public object FrontContent
    {
      get { return FlipPanel.FrontContent; }
      set { FlipPanel.FrontContent = value; }
    }

    public object BackContent //if one wants to replace the default backcontent that hosts the PropertiesPanel etc.
    {
      get { return FlipPanel.BackContent; }
      set { FlipPanel.BackContent = value; }
    }

    public UIElementCollection PropertyItems
    {
      get { return PropertiesPanel.Children; }
      set
      {
        //PropertiesPanel.Children.Clear(); //don't remove any children the ancestor had added
        foreach (UIElement item in value) { PropertiesPanel.Children.Add(item); }
      }
    }

    #endregion

    #region Events

    protected virtual void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Title = View.Title;
        IconText = View.Title; //IconText should match the Title
        Position = View.Position;
        Width = View.Width;
        Height = View.Height;
        Scale = View.Zoom;
        MoveEnabled = View.Moveable;
        ResizeEnabled = View.Resizable;
        Scalable = View.Zoomable;
        //...
      }
      else switch (e.PropertyName)
        {
          case IViewProperties.PropertyTitle:
            Title = View.Title;
            IconText = View.Title; //IconText should match the Title
            break;
          case IViewProperties.PropertyPosition:
            Position = View.Position;
            break;
          case IViewProperties.PropertyWidth:
            Width = View.Width;
            break;
          case IViewProperties.PropertyHeight:
            Height = View.Height;
            break;
          case IViewProperties.PropertyZoom:
            Scale = View.Zoom;
            break;
          case IViewProperties.PropertyMoveable:
            MoveEnabled = View.Moveable;
            break;
          case IViewProperties.PropertyResizable:
            ResizeEnabled = View.Resizable;
            break;
          case IViewProperties.PropertyZoomable:
            Scalable = View.Zoomable;
            break;
          //...
        }
    }

    #endregion

  }

}
