//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseWindow.xaml.cs
//Version: 20121112

using ClipFlair.Utils.Bindings;
using ClipFlair.Windows.Views;

using SilverFlow.Controls;
using Ionic.Zip;

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace ClipFlair.Windows
{

  [ContentProperty("FrontContent")]
  public partial class BaseWindow : FloatingWindow
  {

    private bool isTopLevel;
  
    public BaseWindow()
    {
      if (Application.Current.Host.Settings.EnableGPUAcceleration) //GPU acceleration can been turned on at HTML/ASPX page or at OOB settings for OOB apps
        CacheMode = new BitmapCache(); //must do this before setting the "Scale" property, since that will set "RenderAtScale" property of the BitmapCache

      ShowMaximizeButton = false; //!!! (till we fix it to resize to current visible view area and to allow moving the window in that case only [when it's not same size as parent])

      InitializeComponent(); //can change the "ShowMaximize" button, or set the "Scale" and other properties from XAML, so must do after the commands above

      //Bind to ancestor properties
      BindingUtils.RegisterForNotification("Title", this, (d, e) => { if (View != null) { View.Title = (string)e.NewValue; } });
      BindingUtils.RegisterForNotification("Position", this, (d, e) => { if (View != null) { View.Position = (Point)e.NewValue; } });
      BindingUtils.RegisterForNotification("Width", this, (d, e) => { if (View != null) { View.Width = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("Height", this, (d, e) => { if (View != null) { View.Height = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("Scale", this, (d, e) => { if (View != null) { View.Zoom = (double)e.NewValue; } });
      BindingUtils.RegisterForNotification("MoveEnabled", this, (d, e) => { if (View != null) { View.Moveable = (bool)e.NewValue; } });
      BindingUtils.RegisterForNotification("ResizeEnabled", this, (d, e) => { if (View != null) { View.Resizable = (bool)e.NewValue; } });
      BindingUtils.RegisterForNotification("Scalable", this, (d, e) => { if (View != null) { View.Zoomable = (bool)e.NewValue; } });

      HelpRequested += (s, e) =>
      {
        MessageBox.Show("Help not available yet - see http://ClipFlair.net for contact info");
      };

      //Closing += (s,e) => { e.Cancel = (MessageBox.Show("Are you sure you want to close the window?", "Confirmation", MessageBoxButton.OKCancel) != MessageBoxResult.OK); };
      //TODO: add separate event for closing by end-user, we don't want to get such events if app is closing down (or detect the app is closing down and ignore event or remove event handler early) 

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

        FlipPanel.IsFlipped = !FlipPanel.IsFlipped; //turn window arround to show/hide its options
      };

      //Load-Save (TODO: check: can't set them in the XAML for some reason) //must do after InitializeComponent
      btnLoad.Click += new RoutedEventHandler(btnLoad_Click);
      btnSave.Click += new RoutedEventHandler(btnSave_Click);
    }
 
    ~BaseWindow()
    {
      View = null; //unregister PropertyChangedEventHandler
    }
    
    #region Properties

    protected virtual void InitializeView() //must call from ancestor after setting the View and calling InitializeComponent
    {
      View.Title = (string)Title;
      View.Position = Position;
      View.Width = Width;
      View.Height = Height;
      View.Zoom = Scale;
      View.Moveable = MoveEnabled;
      View.Resizable = ResizeEnabled;
      View.Zoomable = ScaleEnabled;
    }

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

        //set the new view (must do after setting property change event handler)
        DataContext = value;
        UpdatePropertiesFromView();
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

    public bool IsTopLevel
    {
      get { return isTopLevel; }
      set
      {
        isTopLevel = value;

        Visibility visibility = value ? Visibility.Collapsed : Visibility.Visible; //hide backpanel properties not relevant when not being a child window
        propPosition.Visibility = visibility;
        propWidth.Visibility = visibility;
        propHeight.Visibility = visibility;
        propZoom.Visibility = visibility;
        propMoveable.Visibility = visibility;
        propResizable.Visibility = visibility;
        propZoomable.Visibility = visibility;

        if (value) MoveEnabled = false; else MoveEnabled = IViewDefaults.DefaultMoveable;
        if (value) ResizeEnabled = false; else ResizeEnabled = IViewDefaults.DefaultResizable;
        if (value) ScaleEnabled = false; else ScaleEnabled = IViewDefaults.DefaultZoomable;      
      }
    }

    #endregion

    #region Events

    protected void UpdatePropertiesFromView()
    {
      View_PropertyChanged(null, new PropertyChangedEventArgs(null));
    }

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
        ScaleEnabled = View.Zoomable;
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
            ScaleEnabled = View.Zoomable;
            break;
          //...
        }
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "ClipFlair options archive|*.clipflair.zip";
        dlg.FilterIndex = 1; //note: this index is 1-based, not 0-based
    
        if (dlg.ShowDialog() == true) //TODO: find the parent window
          using (Stream stream = dlg.File.OpenRead()) //will close the stream when done
            LoadOptions(stream);
      }
      catch (Exception ex)
      {
        MessageBox.Show("ClipFlair options load failed: " + ex.Message); //TODO: find the parent window
      }
    }
  
    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      if (View == null) return;

      try
      {
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.Filter = "ClipFlair options archive|*.clipflair.zip";
        //dlg.FilterIndex = 1; //note: this index is 1-based, not 0-based //do not set if DefaultExt is supplied
        //dlg.DefaultFileName = View.Title + ".clipflair.zip"; //Silverlight will prompt "Do you want to save X?" (where X is the DefaultFileName value). If we set this, but the prompt can go under the main window, so avoid it
        dlg.DefaultExt = ".clipflair.zip"; //this doesn't seem to be used if FilterIndex is set

        if (dlg.ShowDialog() == true) //TODO: find the parent window
          using (Stream stream = dlg.OpenFile()) //will close the stream when done
            SaveOptions(stream);
      }
      catch (Exception ex)
      {
        MessageBox.Show("CliFlair options save failed: " + ex.Message); //TODO: find the parent window
      }
    }

    #endregion

    #region Load / Save Options

    public void LoadOptions(Stream stream, string zipFolder = "") //doesn't close stream
    {
      using (ZipFile zip = ZipFile.Read(stream))
        LoadOptions(zip, zipFolder); //reading from root folder
    }

    public virtual void LoadOptions(ZipFile zip, string zipFolder = "")
    {
      DataContractSerializer serializer = new DataContractSerializer(View.GetType()); //assuming current View isn't null and has been set by descendent class with wanted BaseView descendent //TODO: maybe use some property to return appropriate View type
      using (Stream stream = zip[zipFolder + "/" + View.GetType().FullName + ".options.xml"].OpenReader())
        View = (IView)serializer.ReadObject(stream);
    }

    public static BaseWindow LoadWindow(Stream stream, string zipFolder = "") //doesn't close stream
    {
      using (ZipFile zip = ZipFile.Read(stream))
        return LoadWindow(zip, zipFolder); //reading from root folder
    }

    public static BaseWindow LoadWindow(ZipFile zip, string zipFolder = "") //doesn't close stream
    {
      foreach (ZipEntry options in zip.SelectEntries("*.options.xml", zipFolder))
      {
        int offset = zipFolder.Length;
        if (offset != 0) offset += 1; //add +1 for the "/" path separator, only if a folder path has been given
        string typeName = options.FileName.Substring(offset, options.FileName.Length - offset - ".options.xml".Length);

        switch (typeName)
        {
          case "ClipFlair.Windows.Views.ActivityView":
            typeName = "ClipFlair.Windows.ActivityContainerWindow";
            break;
          case "ClipFlair.Windows.Views.MediaPlayerView":
            typeName = "ClipFlair.Windows.MediaPlayerWindow";
            break;
          case "ClipFlair.Windows.Views.CaptionsGridView":
            typeName = "ClipFlair.Windows.CaptionsGridWindow";
            break;
          case "ClipFlair.Windows.Views.TextEditorView":
            typeName = "ClipFlair.Windows.TextEditorWindow";
            break;
          case "ClipFlair.Windows.Views.ImageView":
            typeName = "ClipFlair.Windows.ImageWindow";
            break;
          default:
            throw new Exception("Unknown view type");
        }

        BaseWindow window = (BaseWindow)Activator.CreateInstance(Type.GetType(typeName));
        window.LoadOptions(zip, zipFolder);
        return window;
      }
      return null;
    }

    public void SaveOptions(Stream stream, string zipFolder = "") //doesn't close stream
    {
      using (ZipFile zip = new ZipFile(Encoding.UTF8))
      {
        zip.Comment = "ClipFlair Options Archive";
        SaveOptions(zip, zipFolder);
        zip.Save(stream);
        stream.Flush(); //flush all buffers
      }
    }

    public virtual void SaveOptions(ZipFile zip, string zipFolder = "")
    { //TODO: not optimal implementation, should try to pipe streams without first saving into memory
      MemoryStream stream = new MemoryStream(); //don't close this (e.g. don't write "using" here), DotNetZip should close that stream and has been set by descendent class with wanted BaseView descendent //TODO: maybe use some property to return appropriate View type
      DataContractSerializer serializer = new DataContractSerializer(View.GetType()); //assuming current View isn't null
      serializer.WriteObject(stream, View);
      stream.Position = 0;
      ZipEntry optionsXML = zip.AddEntry(zipFolder + "/" + View.GetType().FullName + ".options.xml", stream);
    }
 
    #endregion

  }

}
