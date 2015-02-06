//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseWindow.xaml.cs
//Version: 20150204

//TODO: unbind control at close

#define PROPERTY_CHANGE_SUPPORT
#define WRITE_FORMATTED_XML

using ClipFlair.UI.Dialogs;
using ClipFlair.UI.Widgets;
using ClipFlair.Windows.Base.Resources;
using ClipFlair.Windows.Views;
using Ionic.Zip;
using SilverFlow.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using Utils.Extensions;

#if !SILVERLIGHT
using Microsoft.Win32;
#endif

namespace ClipFlair.Windows
{

  [ScriptableType]
  [ContentProperty("FrontContent")]
  public partial class BaseWindow : FloatingWindow, INotifyPropertyChanged
  {

    #region --- Constants ---

    public const string CLIPFLAIR_EXTENSION = ".clipflair";
    public const string CLIPFLAIR_ZIP_EXTENSION = ".clipflair.zip";
    public string CLIPFLAIR_LOAD_FILTER = BaseWindowStrings.msgLoadFilter;
    public string CLIPFLAIR_SAVE_FILTER = BaseWindowStrings.msgSaveFilter;
    public const string CLIPFLAIR_TUTORIALS = "http://social.clipflair.net/Pages/Tutorials.aspx";

    #if WRITE_FORMATTED_XML
    private static XmlWriterSettings XML_WRITER_SETTINGS = new XmlWriterSettings() { Indent=true, IndentChars="  "};
    #endif

    #endregion

    #region --- MEF ---

    protected static CompositionContainer mefContainer; //assigned by ActivityWindow descendent

    /*
    public static IWindowFactory MediaPlayerWindowFactory { get; protected set; }
    public static IWindowFactory CaptionsWindowFactory { get; protected set; }
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
      get { return options.IsTopLevel; }
      set {
        if (IsTopLevel != value)
        {
          options.IsTopLevel = value;
          RaisePropertyChanged("IsTopLevel");
        } //TODO: see if the following could get inside the "if" block without breaking any related functionality

        if (value) MoveEnabled = false; else MoveEnabled = ViewDefaults.DefaultMoveable;
        if (value) ResizeEnabled = false; else ResizeEnabled = ViewDefaults.DefaultResizable;
        if (value) ScaleEnabled = false; else ScaleEnabled = ViewDefaults.DefaultZoomable;

        ShowHelpButton = value; //showing help button only for top level window
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
                OnFlippedChanged));

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
                OnIsAnimatedChanged));

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

    public virtual void ShowLoadURLDialog(string loadItemTitle = "#msgLoadURLDialogTitle#")
    {
      try
      {
        if (loadItemTitle.Equals("#msgLoadURLDialogTitle#"))
          loadItemTitle = BaseWindowStrings.msgLoadURLDialogTitle;

        InputDialog.Show(BaseWindowStrings.msgLoad + " " + loadItemTitle, BaseWindowStrings.msgURL, defaultLoadURL,
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
        ErrorDialog.Show(BaseWindowStrings.msgLoadingFailedOtherWindow, ex);
      }
      catch (Exception ex)
      {
        ErrorDialog.Show(BaseWindowStrings.msgLoadingFailed, ex);
      }
    }

    public virtual string LoadFilter
    {
      get { return CLIPFLAIR_LOAD_FILTER;  }
    }

    public bool ShowLoadDialog() //this has to be called by user-initiated event handler
    {
      try
      {
        OpenFileDialog dlg = new OpenFileDialog()
        {
          Filter = LoadFilter,
          FilterIndex = 1, //note: this index is 1-based, not 0-based
          Multiselect = true, //depends on the component whether they'll load the first file selected or try to handle more
          //DefaultExt = CLIPFLAIR_EXTENSION //OpenFileDialog doesn't seem to have a DefaultExt like SaveFileDialog
        };

        if (dlg.ShowDialog() == true) //TODO: find the parent window
        {
          loadModifiers = Keyboard.Modifiers;
          #if SILVERLIGHT
          LoadOptions(dlg.Files); 
          #else
          LoadOptions(dlg.FileNames.Select<string,FileInfo>((s)=>{ return new FileInfo(s);}));
          #endif
          Flipped = false; //flip the view back to front after succesful options loading
          return true;
        }
      }
      catch (NullReferenceException ex)
      {
        ErrorDialog.Show(BaseWindowStrings.msgLoadingFailedOtherWindow, ex);
      }
      catch (Exception ex)
      {
        ErrorDialog.Show(BaseWindowStrings.msgLoadingFailed, ex);
      }
      return false;
    }

    public bool ShowSaveDialog() //this has to be called by user-initiated event handler
    {
      if (View == null) return false;

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
        {
          using (Stream stream = dlg.OpenFile()) //will close the stream when done
            SaveOptions(stream);
          return true;
        }
      }
      catch (Exception ex)
      {
        ErrorDialog.Show(BaseWindowStrings.msgSavingFailed, ex);
      }
      return false;
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

    public virtual void LoadContent(Stream stream, string filename) //filename can be used to check filetype from file extension, or to show as title etc.
    {
      throw new NotImplementedException(); //descendents that support loading content from a stream should override and implement this method
    }

    protected virtual Type ResolveType(string typeName)
    {
      return GetType().Assembly.GetType(typeName, true); //don't use Type.GetType(), want GetType to execute at the context of the assembly of the descendent class
    }

    public virtual void /*bool*/ LoadOptions(ZipFile zip, string zipFolder = "") //THIS IS THE CORE LOADING LOGIC
    {
      if (LoadingOptions != null) LoadingOptions(this, null); //notify any listeners

      View.Busy = true;
      try
      {
        foreach (ZipEntry options in zip.SelectEntries("*.options.xml", zipFolder))
        {
          //Note: we try to load the 1st file that ends in ".options.xml" (so that a component can support various versions
          //of saved state (not coexisting of course), e.g. both ClipFlair.Windows.Views.TextEditorView [that one had a data contract namespace typo] and
          //ClipFlair.Windows.Views.TextEditorView2 [replacing the older view] implement ITextEditor interface and can be set as TextEditorView to TextEditorWindow)

          DataContractSerializer serializer = new DataContractSerializer(ResolveType(options.FileName.ReplaceSuffix(".options.xml", ""))); //assuming the view exists in the same assembly as the component

          using (Stream stream = options.OpenReader())
            View = (IView)serializer.ReadObject(stream); //this will set a new View that defaults to Busy=false

          if (LoadedOptions != null) LoadedOptions(this, null); //notify any listeners

          break; //return true; //expecting only one .options.xml file
        }

        //return false; //didn't find saved options file

      } //note: we aren't catching exceptions
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
          Stream f = e.Result;
          using (f)
          {
            MemoryStream memStream = new MemoryStream((int)f.Length); //TODO: see why this is needed (Ionic.Zip fails to load directly from the InternalMemoryStream because of some call to Flush which is not supported [maybe we could wrap InternalMemoryStream with other stream that ignores the Flush])
            using (memStream)
            {
              f.CopyTo(memStream);
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

    public virtual void LoadOptions(IEnumerable<FileInfo> files) //descendents that can handle multiple files should override this
    {
      if (files != null && files.Count() > 0)
        LoadOptions(files.First());
    }

    public virtual void LoadOptions(FileInfo f) //used for the Load button at the backpanel (descendents can override to open more filetypes)
    {
      using (Stream stream = f.OpenRead()) //will close the stream when done
        LoadOptions(stream);
    }

    public virtual void LoadOptions(Stream stream, string zipFolder = "") //doesn't close stream
    {
      //save position, size and ZIndex (to restore after loading saved state) //used in drag-drop and when loading state from backpanel to avoid having the component move arround or go to the background
      Point oldPos = Position;
      double oldWidth = Width;
      double oldHeight = Height; //do not use (ActualWidth, ActualHeight) here, assigning to (Width, Height) below
      int oldZIndex = View.ZIndex;
      try
      {
        using (ZipFile zip = ZipFile.Read(stream))
          LoadOptions(zip, zipFolder); //reading from root folder
      }
      finally
      {
        //restore position and size - needed so that components and especially nested activity ones don't change position/size after loading saved state of theirs from file [dragdrop included] or URL
        Position = oldPos;
        Width = oldWidth;
        Height = oldHeight;
        View.ZIndex = oldZIndex; //Canvas.ZIndex attached dependency property which is two-way bound to View.ZIndex in the XAML
      }
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
      using (Stream zipStream = childZip.OpenReader())
        using (MemoryStream memStream = new MemoryStream((int)childZip.UncompressedSize)) //TODO: see why this is needed - can't use activity.Windows.Add(LoadWindow(stream), seems DotNetZip fails to open a .zip from a stream inside another .zip
        {
          zipStream.CopyTo(memStream);
          memStream.Position = 0;
          return LoadWindow(memStream);
        }
    }

    protected static IWindowFactory GetWindowFactory(string contract)
    {
      Lazy<IWindowFactory> win = mefContainer.GetExports<IWindowFactory>(contract).FirstOrDefault();
      if (win == null)
        throw new Exception(BaseWindowStrings.msgUnknownViewType + contract);
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
        ErrorDialog.Show(BaseWindowStrings.msgSavingFailed, e);
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

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged(string PropertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
    }

    #endregion

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

      if (files != null && files.Length > 0)
      {
        loadModifiers = Keyboard.Modifiers;

        try
        {
          LoadOptions(files);
        }
        catch (NullReferenceException ex)
        {
          ErrorDialog.Show(BaseWindowStrings.msgLoadingFailedOtherWindow, ex);
        }
        catch (Exception ex)
        {
          ErrorDialog.Show(BaseWindowStrings.msgLoadingFailed, ex);
        }
      }
    }

    #endregion

    #region Closing

    protected void BaseOnClosing(CancelEventArgs e) //needed to be able to call directly by ancestors
    {
      base.OnClosing(e);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      if (!IsTopLevel //for top level window showing closing warning (with option to cancel closing) via webpage JavaScript event handler or via App class event handler at OOB mode
          && View.WarnOnClosing) 
        e.Cancel = (MessageBox.Show(BaseWindowStrings.msgCloseConfirmation, BaseWindowStrings.msgConfirmation, MessageBoxButton.OKCancel) != MessageBoxResult.OK);

      if (!e.Cancel)
      {
        //Containers should reimplement this method and set WarnOnClosing=false here to each of their children if they warn user themselves and users select to proceed with closing
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
