﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseWindow.xaml.cs
//Version: 20140418

//TODO: unbind control at close

#define PROPERTY_CHANGE_SUPPORT
#define WRITE_FORMATTED_XML

using ClipFlair.UI.Dialogs;
using ClipFlair.UI.Widgets;
using ClipFlair.Windows.Views;
using Ionic.Zip;
using SilverFlow.Controls;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using Utils.Extensions;
using System.ComponentModel.Composition.Primitives;

namespace ClipFlair.Windows
{

  [ScriptableType]
  [ContentProperty("FrontContent")]
  public partial class BaseWindow : FloatingWindow
  {

    #region --- Constants ---

    public const string CLIPFLAIR_EXTENSION = ".clipflair";
    public const string CLIPFLAIR_ZIP_EXTENSION = ".clipflair.zip";
    public const string CLIPFLAIR_LOAD_FILTER = "ClipFlair archive (*.clipflair, *.clipflair.zip)|*.clipflair;*.clipflair.zip";
    public const string CLIPFLAIR_SAVE_FILTER = "ClipFlair archive (*.clipflair)|*.clipflair|ClipFlair archive (*.clipflair.zip)|*.clipflair.zip";
    public const string CLIPFLAIR_TUTORIALS = "http://social.clipflair.net/Pages/Tutorials.aspx";

    #if WRITE_FORMATTED_XML
    private static XmlWriterSettings XML_WRITER_SETTINGS = new XmlWriterSettings() { Indent=true, IndentChars="  "};
    #endif

    #endregion

    #region --- MEF ---

    protected static CompositionContainer mefContainer; //assigned by ActivityWindow descendent

    /*
    public static IWindowFactory MediaPlayerWindowFactory { get; protected set; }
    public static IWindowFactory CaptionsGridWindowFactory { get; protected set; }
    public static IWindowFactory TextEditorWindowFactory { get; protected set; }
    public static IWindowFactory ImageWindowFactory { get; protected set; }
    //public static IWindowFactory CameraWindowFactory { get; protected set; }
    public static IWindowFactory MapWindowFactory { get; protected set; }
    public static IWindowFactory NewsWindowFactory { get; protected set; }
    public static IWindowFactory GalleryWindowFactory { get; protected set; }
    public static IWindowFactory ActivityWindowFactory { get; protected set; }
    */

    #endregion

    #region --- Fields ---

    protected bool isTopLevel;
    protected OptionsLoadSaveControl OptionsLoadSave;
    protected string defaultLoadURL = "";
    protected ModifierKeys loadModifiers = ModifierKeys.None;

    #endregion

    #region --- Constructor ---

    public BaseWindow()
    {
      //if (Application.Current.Host.Settings.EnableGPUAcceleration) //GPU acceleration can been turned on at HTML/ASPX page or at OOB settings for OOB apps
      //  CacheMode = new BitmapCache(); //must do this before setting the "Scale" property, since that will set "RenderAtScale" property of the BitmapCache
      //NOTE: causes issue with video acceleration

      InitializeComponent(); //can change properties from XAML, so must do after the commands above
      OptionsLoadSave = ctrlOptionsLoadSave;
     
      //BindingUtils.RegisterForNotification("Title", this, (d, e) => { if (View != null) { View.Title = (bool)e.NewValue; } }); //not used, using data binding in XAML instead

      HelpRequested += (s, e) => ShowHelp();
      OptionsRequested += (s, e) => ShowOptions();

      //Load-Save (TODO: check: can't set them in the XAML (probably some issue with UserControl inheritance), says "Failed to assign to property 'System.Windows.Controls.Primitives.ButtonBase.Click'") //must do after InitializeComponent
      ctrlOptionsLoadSave.LoadURLClick += new RoutedEventHandler(btnLoadURL_Click);
      ctrlOptionsLoadSave.LoadClick += new RoutedEventHandler(btnLoad_Click);
      ctrlOptionsLoadSave.SaveClick += new RoutedEventHandler(btnSave_Click);
    }

    #endregion

    #region --- Properties ---

    [ScriptableMember]
    public BaseView GetView() //NOTE: need to return BaseView and not IView since the HTMLBridge doesn't seem to support interface types
    {
      return (BaseView)View;
    }

    public virtual IView View
    {
      get { return (IView)DataContext; }
      set
      {
#if PROPERTY_CHANGE_SUPPORT
        //remove property changed handler from old view
        if (DataContext != null)
          View.PropertyChanged -= View_PropertyChanged; //IView inherits from INotifyPropertyChanged

        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += View_PropertyChanged; //don't use "new PropertyChangedEventHandler(View_PropertyChanged)", else it won't call overriden event handler at descendent classes
#endif

        //set the new view (must do after setting property change event handler)
        DataContext = value;

#if PROPERTY_CHANGE_SUPPORT
        if (value != null)
          View_PropertyChanged(View, new PropertyChangedEventArgs(null)); //notify property change listeners that all properties of the view changed
#endif

        OnViewChanged(); //make sure ViewChangedEventHandler is fired
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
      get { return isTopLevel;  }
      set
      {
        isTopLevel = value;

        Visibility visibility = value ? Visibility.Collapsed : Visibility.Visible; //hide backpanel properties not relevant when not being a child window
        //propPosition.Visibility = visiblity;
        propX.Visibility = visibility;
        propY.Visibility = visibility;
        propWidth.Visibility = visibility;
        propHeight.Visibility = visibility;
        propZoom.Visibility = visibility;
        propMoveable.Visibility = visibility;
        propResizable.Visibility = visibility;
        propZoomable.Visibility = visibility;

        if (value) MoveEnabled = false; else MoveEnabled = ViewDefaults.DefaultMoveable;
        if (value) ResizeEnabled = false; else ResizeEnabled = ViewDefaults.DefaultResizable;
        if (value) ScaleEnabled = false; else ScaleEnabled = ViewDefaults.DefaultZoomable;
      }
    }

    #region Flipped

    /// <summary>
    /// Flipped Dependency Property
    /// </summary>
    public static readonly DependencyProperty FlippedProperty =
        DependencyProperty.Register("Flipped", typeof(bool), typeof(BaseWindow),
            new FrameworkPropertyMetadata((bool)false,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnFlippedChanged)));

    /// <summary>
    /// Gets or sets the Flipped property.
    /// </summary>
    public bool Flipped
    {
      get { return (bool)GetValue(FlippedProperty); }
      set { SetValue(FlippedProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Flipped property.
    /// </summary>
    private static void OnFlippedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BaseWindow target = (BaseWindow)d;
      bool oldFlipped = (bool)e.OldValue;
      bool newFlipped = target.Flipped;
      target.OnFlippedChanged(oldFlipped, newFlipped);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Flipped property.
    /// </summary>
    protected virtual void OnFlippedChanged(bool oldFlipped, bool newFlipped)
    {
      FlipPanel.IsFlipped = newFlipped; //Note: must make sure we don't set FlipPanel.IsFlipped directly elsewhere to keep its state in-sync with the Flipped dependency property
    }

    #endregion

    #region IsAnimated

    /// <summary>
    /// IsAnimated Dependency Property
    /// </summary>
    public static readonly DependencyProperty IsAnimatedProperty =
        DependencyProperty.Register("IsAnimated", typeof(bool), typeof(BaseWindow),
            new FrameworkPropertyMetadata(true,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnIsAnimatedChanged)));

    /// <summary>
    /// Gets or sets the IsAnimated property.
    /// </summary>
    public bool IsAnimated
    {
      get { return (bool)GetValue(IsAnimatedProperty); }
      set { SetValue(IsAnimatedProperty, value); }
    }

    /// <summary>
    /// Handles changes to the IsAnimated property.
    /// </summary>
    private static void OnIsAnimatedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BaseWindow target = (BaseWindow)d;
      bool oldIsAnimated = (bool)e.OldValue;
      bool newIsAnimated = target.IsAnimated;
      target.OnIsAnimatedChanged(oldIsAnimated, newIsAnimated);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAnimated property.
    /// </summary>
    protected virtual void OnIsAnimatedChanged(bool oldIsAnimated, bool newIsAnimated)
    {
      FlipPanel.IsAnimated = newIsAnimated;
    }

    #endregion

    #endregion

    #region --- Methods ---

    public virtual void ShowLoadURLDialog(string loadItemTitle = "ClipFlair Component Template")
    {
      try
      {
        InputDialog.Show("Load " + loadItemTitle, "URL:", defaultLoadURL,
        (s, ex) =>
        {
          string input = ((InputDialog)s).Input;
          if (input != null && input.Trim() != "")
          { //ignoring empty URLs
            loadModifiers = Keyboard.Modifiers;
            LoadOptions(new Uri(input, UriKind.Absolute)); //since that is an asynchronous operation we expect from it to flip the view back to front after succesful loading
          }
        });
      }
      catch (NullReferenceException ex)
      {
        ErrorDialog.Show("Loading failed - Saved options may be for other window", ex);
      }
      catch (Exception ex)
      {
        ErrorDialog.Show("Loading failed", ex);
      }
    }

    public virtual string LoadFilter
    {
      get { return CLIPFLAIR_LOAD_FILTER;  }
    }

    public void ShowLoadDialog() //this has to be called by user-initiated event handler
    {
      try
      {
        OpenFileDialog dlg = new OpenFileDialog()
        {
          Filter = LoadFilter,
          FilterIndex = 1, //note: this index is 1-based, not 0-based
          //DefaultExt = CLIPFLAIR_EXTENSION //OpenFileDialog doesn't seem to have a DefaultExt like SaveFileDialog
        };

        if (dlg.ShowDialog() == true) //TODO: find the parent window
        {
          loadModifiers = Keyboard.Modifiers;
          LoadOptions(dlg.File);
          Flipped = false; //flip the view back to front after succesful options loading
        }
      }
      catch (NullReferenceException ex)
      {
        ErrorDialog.Show("Loading failed - Saved options may be for other window", ex);
      }
      catch (Exception ex)
      {
        ErrorDialog.Show("Loading failed", ex);
      }
    }

    public void ShowSaveDialog() //this has to be called by user-initiated event handler
    {
      if (View == null) return;

      try
      {
        SaveFileDialog dlg = new SaveFileDialog()
        {
          Filter = CLIPFLAIR_SAVE_FILTER,
          FilterIndex = 1, //note: this index is 1-based, not 0-based //do not set if DefaultExt is supplied
          //DefaultFileName = View.Title + CLIPFLAIR_EXTENSION, //Silverlight will prompt "Do you want to save X?" (where X is the DefaultFileName value). If we set this, but the prompt can go under the main window, so avoid it
          //DefaultExt = CLIPFLAIR_EXTENSION //this doesn't seem to be used, it uses the selected index of the filter anyway (even if you don't set FilterIndex)
        };

        if (dlg.ShowDialog() == true) //TODO: find the parent window
          using (Stream stream = dlg.OpenFile()) //will close the stream when done
            SaveOptions(stream);
      }
      catch (Exception ex)
      {
        ErrorDialog.Show("Saving failed", ex);
      }
    }

    public void ShowHelp()
    {
      BrowserDialog.Show(new Uri(CLIPFLAIR_TUTORIALS));
    }

    public virtual void ShowOptions()
    {
      //try to set focus to front content so that changes to property editboxes at the back content are applied
      if (!Focus())
      {
        // If the Focus() fails it means there is no focusable element in the front content. In this case we set IsTabStop to true to enable keyboard functionality for the container.
        IsTabStop = true;
        Focus();
        //keeping tab stop functionality for future back to front flips
      }

      Flipped = !Flipped; //flip the view to show/hide window options
    }

    #region ---------------- Load ----------------

    protected virtual Type ResolveType(string typeName)
    {
      return GetType().Assembly.GetType(typeName, true); //don't use Type.GetType(), want GetType to execute at the context of the assembly of the descendent class
    }

    public virtual void LoadOptions(ZipFile zip, string zipFolder = "") //THIS IS THE CORE LOADING LOGIC
    {
      if (LoadingOptions != null) LoadingOptions(this, null); //notify any listeners

      View.Busy = true;
      try
      {
        foreach (ZipEntry options in zip.SelectEntries("*.options.xml", zipFolder))
        {
          //Note: we try to load the 1st file that ends in ".options.xml" (so that a component can support various versions
          //of saved state, e.g. both ClipFlair.Windows.Views.TextEditorView [that one had a data contract namespace typo] and
          //ClipFlair.Windows.Views.TextEditorView2 [replacing the older view] implement ITextEditor interface and can be set as TextEditorView to TextEditorWindow)

          DataContractSerializer serializer = new DataContractSerializer(ResolveType(options.FileName.ReplaceSuffix(".options.xml", ""))); //assuming the view exists in the same assembly as the component

          using (Stream stream = options.OpenReader())
            View = (IView)serializer.ReadObject(stream); //this will set a new View that defaults to Busy=false

          if (LoadedOptions != null) LoadedOptions(this, null); //notify any listeners

          break; //expecting only one .options.xml file
        }
      }
      finally
      {
        View.Busy = false; //in any case (error or not) clear the Busy flag
      }
    }

    public void LoadOptions(Uri uri)
    {
      if (uri == null) return;

      WebClient webClient = new WebClient();

      //set up OpenReadCompleted event handler
      webClient.OpenReadCompleted += (s, e) =>
      {
        try
        {
          using (e.Result)
          {
            MemoryStream memStream = new MemoryStream(); //TODO: see why this is (Ionic.Zip fails to load directly from the InternalMemoryStream because of some call to Flush which is not supported)
            using (memStream)
            {
              e.Result.CopyTo(memStream);
              memStream.Position = 0;
              LoadOptions(memStream);
            }
            Flipped = false; //flip the view back to front after succesful options loading //since this is an asynchronous operation we have to flip here
          }
        }
        finally
        {
          View.Busy = false;
        }
      };

      try
      {
        View.Busy = true; //the busy flag will be reset back to false either at OpenReadComplete event handler (on success) or at the exception handler below (on failure)
        webClient.OpenReadAsync(uri); //open the stream asynchronously
      }
      finally
      {
        View.Busy = false;
      }
    }

    public virtual void LoadOptions(FileInfo f)
    {
      using (Stream stream = f.OpenRead()) //will close the stream when done
        LoadOptions(stream);
    }

    public void LoadOptions(Stream stream, string zipFolder = "") //doesn't close stream
    {
      using (ZipFile zip = ZipFile.Read(stream))
        LoadOptions(zip, zipFolder); //reading from root folder
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

        BaseWindow window = GetWindowFactory(typeName).CreateWindow();
        window.LoadOptions(zip, zipFolder);
        return window;
      }
      return null;
    }

    public static BaseWindow LoadWindow(ZipEntry childZip) //load window from nested archive
    {
      using (Stream stream = childZip.OpenReader())
      using (MemoryStream memStream = new MemoryStream()) //TODO: see why this is needed - can't use activity.Windows.Add(LoadWindow(stream), seems DotNetZip fails to open a .zip from a stream inside another .zip
      {
        stream.CopyTo(memStream);
        memStream.Position = 0;
        return LoadWindow(memStream);
      }
    }
    protected static IWindowFactory GetWindowFactory(string contract)
    {
      Lazy<IWindowFactory> win = mefContainer.GetExports<IWindowFactory>(contract).FirstOrDefault();
      if (win == null)
        throw new Exception("Unknown view type: " + contract);
      else
        return win.Value;
    }

    protected static IFileWindowFactory GetFileWindowFactory(string contract)
    {
      Lazy<IFileWindowFactory> win = mefContainer.GetExports<IFileWindowFactory>(contract).FirstOrDefault();
      if (win != null)
        return win.Value;
      else
        return null;
    }
    
    #endregion

    #region  ---------------- Save ----------------

    public virtual void SaveOptions(ZipFile zip, string zipFolder = "") //THIS IS THE CORE SAVING LOGIC
    {
      if (SavingOptions != null) SavingOptions(this, null); //notify any listeners

      View.Busy = true;
      try
      {
        ZipEntry optionsXML = zip.AddEntry(zipFolder + "/" + View.GetType().FullName + ".options.xml",
          new WriteDelegate((entryName, stream) =>
          {
            DataContractSerializer serializer = new DataContractSerializer(View.GetType()); //assuming current View isn't null
            #if WRITE_FORMATTED_XML
            using (XmlWriter writer = XmlWriter.Create(stream, XML_WRITER_SETTINGS))
              serializer.WriteObject(writer, View);
            #else
            serializer.WriteObject(stream, View);
            #endif
          }));
      }
      catch (Exception e)
      {
        ErrorDialog.Show("Saving failed", e);
      }
      finally
      {
        View.Busy = false; //in any case (error or not) clear the Busy flag
      }

      if (SavedOptions != null) SavedOptions(this, null); //notify any listeners
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

    public static void SaveWindow(BaseWindow window, ZipFile zip, string zipFolder = "") //save window to nested archive
    {
      string title = ((string)window.Title).TrimStart(); //using TrimStart() to not have filenames start with space chars in case it's an issue with ZIP spec
      if (title == "") title = window.GetType().Name;
      zip.AddEntry(
        zipFolder + "/" + title.ReplaceInvalidFileNameChars() + " - " + window.View.ID + BaseWindow.CLIPFLAIR_ZIP_EXTENSION, //using .clipflair.zip extension for nested components' state to ease examining with ZIP archivers
        new WriteDelegate((entryName, stream) => { window.SaveOptions(stream); })); //save ZIP file for child window 
    }

    #endregion

    #endregion

    #region --- Events ---

    public event EventHandler LoadingOptions;
    public event EventHandler LoadedOptions;
    public event EventHandler SavingOptions;
    public event EventHandler SavedOptions;

    public delegate void ViewChangedEventHandler(object sender, IView newView);
    public event ViewChangedEventHandler ViewChanged;

    #region Drag & Drop

    protected override void OnDragEnter(DragEventArgs e)
    {
      base.OnDragEnter(e);
      e.Handled = true; //must do this
      VisualStateManager.GoToState(this, "DragOver", true);
    }

    protected override void OnDragOver(DragEventArgs e)
    {
      base.OnDragOver(e);
      e.Handled = true; //must do this
      //NOP
    }

    protected override void OnDragLeave(DragEventArgs e)
    {
      base.OnDragLeave(e);
      e.Handled = true; //must do this
      VisualStateManager.GoToState(this, "Normal", true);
    }

    protected override void OnDrop(DragEventArgs e)
    {
      base.OnDrop(e);
      e.Handled = true; //must do this

      VisualStateManager.GoToState(this, "Normal", true);

      //we receive an array of FileInfo objects for the list of files that were selected and drag-dropped onto this control.
      if (e.Data == null)
        return;

      IDataObject f = e.Data as IDataObject;
      if (f == null) //checks if the dropped objects are files
        return;

      object data = f.GetData(DataFormats.FileDrop); //Silverlight 5 only supports FileDrop - GetData returns null if format is not supported
      FileInfo[] files = data as FileInfo[];

      if (files != null && files.Length > 0) //Use only 1st item from array of FileInfo objects
      {
        loadModifiers = Keyboard.Modifiers;

        try
        {
          LoadOptions(files[0]); //TODO: make LoadOptions of activity (override) clever to handle various file extensions apart from .clipflair/.clipflair.zip (no extension will be assumed to be .clipflair) and add to LoadOptions file dialog more file types, like videos, images etc. and create respective components to load and wrap that content (need an array property to be able to define the extra extensions allowed per component). Do similar at each component's LoadOptions for the extra data types they know how to handle (override BaseWindow.LoadOptions and do the check before calling base.LoadOptions)
        }
        catch (NullReferenceException ex)
        {
          ErrorDialog.Show("Loading failed - Saved options may be for other window", ex);
        }
        catch (Exception ex)
        {
          ErrorDialog.Show("Loading failed", ex);
        }
      }
    }

    #endregion

    #region Closing

    protected override void OnClosing(CancelEventArgs e)
    {
      if (!IsTopLevel //for top level window showing closing warning (with option to cancel closing) via webpage JavaScript event handler or via App class event handler at OOB mode
          && View.WarnOnClosing) //Containers should set WarnOnClosing=false to each of their children if they warn user themselves and users select to proceed with closing
        e.Cancel = (MessageBox.Show("Are you sure you want to close this window?", "Confirmation", MessageBoxButton.OKCancel) != MessageBoxResult.OK);

      if (!e.Cancel)
      {
        //TODO: 
        base.OnClosing(e); //this will fire "Closing" event handler
      }
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e); //this will fire "Closed" event handler

      View = null; //clearing the view to release property change event handler //must not do this at class destructor, else may get cross-thread-access exceptions
    }

    #endregion

    protected virtual void OnViewChanged()
    {
      if (ViewChanged != null)
        ViewChanged(this, View); //fire ViewChanged event handler
    }

    #if PROPERTY_CHANGE_SUPPORT
    protected virtual void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      /*
      switch (e.PropertyName)
      {
        case null: //multiple (not specified) properties have changed, consider all as changed
          //Title = View.Title; //not used, using data binding in XAML instead
          //...
          break;
        //case IViewProperties.PropertyTitle: //not used, using data binding in XAML instead
        //  Title = View.Title;
        //  IconText = View.Title; //IconText should match the Title
        //  break; 
        //...
        default:
          //NOP
          break;
      }
      */
    }

    #endif

    private void btnLoadURL_Click(object sender, RoutedEventArgs e)
    {
      ShowLoadURLDialog();
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
      ShowLoadDialog();
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      ShowSaveDialog();
    }

    #endregion

  }

}
