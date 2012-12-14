//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseWindow.xaml.cs
//Version: 20121214

//TODO: unbind control at close
//TODO: do not allow to set too low opacity values that could make windows disappear

#define PROPERTY_CHANGE_SUPPORT

using ClipFlair.Windows.Views;
using ClipFlair.Utils;

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

    public static IWindowFactory ActivityWindowFactory { get; protected set; }
    public static IWindowFactory MediaPlayerWindowFactory { get; protected set; }
    public static IWindowFactory CaptionsGridWindowFactory { get; protected set; }
    public static IWindowFactory TextEditorWindowFactory { get; protected set; }
    public static IWindowFactory ImageWindowFactory { get; protected set; }
    public static IWindowFactory MapWindowFactory { get; protected set; }

    private bool isTopLevel;
  
    public BaseWindow()
    {
      if (Application.Current.Host.Settings.EnableGPUAcceleration) //GPU acceleration can been turned on at HTML/ASPX page or at OOB settings for OOB apps
        CacheMode = new BitmapCache(); //must do this before setting the "Scale" property, since that will set "RenderAtScale" property of the BitmapCache

      ShowMaximizeButton = false; //!!! (till we fix it to resize to current visible view area and to allow moving the window in that case only [when it's not same size as parent])

      InitializeComponent(); //can change the "ShowMaximize" button, or set the "Scale" and other properties from XAML, so must do after the commands above

      //BindingUtils.RegisterForNotification("Title", this, (d, e) => { if (View != null) { View.Title = (bool)e.NewValue; } }); //not used, using data binding in XAML instead

      HelpRequested += (s, e) =>
      {
        Dispatcher.BeginInvoke(delegate
        {
          try
          {
            ExecUtils.OpenHyperlink("http://clipflairsrv.cti.gr/Social/Pages/Tutorials.aspx"); //TODO: change to play.clipflair.net URL
          }
          catch
          {
            MessageBox.Show("For help see http://clipflairsrv.cti.gr/Social/Pages/Tutorials.aspx");  //TODO: change to play.clipflair.net URL //TODO: fix to show help in OOB using WebBrowser control (just make sure help pages are coming from same domain as the one that deploys the app)
          }
        });
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

      //Load-Save (TODO: check: can't set them in the XAML (probably some issue with UserControl inheritance), says "Failed to assign to property 'System.Windows.Controls.Primitives.ButtonBase.Click'") //must do after InitializeComponent
      btnLoad.Click += new RoutedEventHandler(btnLoad_Click);
      btnSave.Click += new RoutedEventHandler(btnSave_Click);
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
        #if PROPERTY_CHANGE_SUPPORT
        //remove property changed handler from old view
        if (DataContext != null)
          View.PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged); //IView inherits from INotifyPropertyChanged
        
        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
        #endif

        //set the new view (must do after setting property change event handler)
        DataContext = value;

        #if PROPERTY_CHANGE_SUPPORT
        if (value!=null) View_PropertyChanged(View, new PropertyChangedEventArgs(null)); //notify property change listeners that all properties of the view changed
        #endif
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


    #if PROPERTY_CHANGE_SUPPORT
    protected virtual void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    /*
      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        //Title = View.Title; //not used, using data binding in XAML instead
        //...
      }
      else switch (e.PropertyName)
        {
          //case IViewProperties.PropertyTitle: //not used, using data binding in XAML instead
          //  Title = View.Title;
          //  IconText = View.Title; //IconText should match the Title
          //  break; 
          default:
            //NOP
            break;
          //...
        }
    */
    }
    #endif

    private void btnLoadURL_Click(object sender, RoutedEventArgs e)
    {
      //TODO
      
      //LoadOptions(stream);
      FlipPanel.IsFlipped = !FlipPanel.IsFlipped; //turn window arround again after succesful options loading
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "ClipFlair options archive|*.clipflair.zip";
        dlg.FilterIndex = 1; //note: this index is 1-based, not 0-based
        //dlg.DefaultExt = ".clipflair.zip"; //OpenFileDialog doesn't seem to have a DefaultExt like SaveFileDialog

        if (dlg.ShowDialog() == true) //TODO: find the parent window
          using (Stream stream = dlg.File.OpenRead()) //will close the stream when done
          {
            LoadOptions(stream);
            FlipPanel.IsFlipped = !FlipPanel.IsFlipped; //turn window arround again after succesful options loading
          }
      }
      catch (NullReferenceException)
      {
        MessageBox.Show("ClipFlair options load failed - These saved options may be for other window"); //TODO: find the parent window
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

        IWindowFactory windowFactory;
        switch (typeName)
        {
          case "ClipFlair.Windows.Views.ActivityView":
            windowFactory = ActivityWindowFactory;
            break;
          case "ClipFlair.Windows.Views.MediaPlayerView":
            windowFactory = MediaPlayerWindowFactory;
            break;
          case "ClipFlair.Windows.Views.CaptionsGridView":
            windowFactory = CaptionsGridWindowFactory;
            break;
          case "ClipFlair.Windows.Views.TextEditorView":
            windowFactory = TextEditorWindowFactory;
            break;
          case "ClipFlair.Windows.Views.ImageView":
            windowFactory = ImageWindowFactory;
            break;
          case "ClipFlair.Windows.Views.MapView":
            windowFactory = MapWindowFactory;
            break;
          default:
            throw new Exception("Unknown view type");
        }

        BaseWindow window = windowFactory.CreateWindow();
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
