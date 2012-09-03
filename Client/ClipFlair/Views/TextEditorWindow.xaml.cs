//Filename: TextEditorWindow.xaml.cs
//Version: 20120902

using ClipFlair.Models.Views;

using System;
using System.Windows;
using System.ComponentModel;

namespace ClipFlair.Views
{
  public partial class TextEditorWindow : FlipWindow
  {
    public TextEditorWindow()
    {
      View = new TextEditorView(); //must set the view first
      InitializeComponent();
    }

    #region View

    public TextEditorView View
    {
      get { return (TextEditorView)DataContext; }
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          ((INotifyPropertyChanged)DataContext).PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged);
        //add property changed handler to new view
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
        //set the new view (must do last)
        DataContext = value;
      }
    }

    protected void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
      {
        Source = View.Source;
        ToolbarVisible = View.ToolbarVisible;
        //...
      }
      else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
        {
          case ITextEditorProperties.PropertySource:
            Source = View.Source;
            break;
          case ITextEditorProperties.PropertyToolbarVisible:
            ToolbarVisible = View.ToolbarVisible;
            break;
          //...
        }
    }

    #endregion

    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(ITextEditorProperties.PropertySource, typeof(Uri), typeof(TextEditorWindow),
            new FrameworkPropertyMetadata((Uri)ITextEditorDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

    /// <summary>
    /// Gets or sets the Source property.
    /// </summary>
    public Uri Source
    {
      get { return (Uri)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }

    /// <summary>
    /// Handles changes to the Source property.
    /// </summary>
    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      TextEditorWindow target = (TextEditorWindow)d;
      target.OnSourceChanged((Uri)e.OldValue, target.Source);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
    {
      View.Source = newSource;
    }

    #endregion

    #region ToolbarVisible

    /// <summary>
    /// ToolbarVisible Dependency Property
    /// </summary>
    public static readonly DependencyProperty ToolbarVisibleProperty =
        DependencyProperty.Register(ITextEditorProperties.PropertyToolbarVisible, typeof(bool), typeof(TextEditorWindow),
            new FrameworkPropertyMetadata((bool)ITextEditorDefaults.DefaultToolbarVisible, new PropertyChangedCallback(OnToolbarVisibleChanged)));

    /// <summary>
    /// Gets or sets the ToolbarVisible property.
    /// </summary>
    public bool ToolbarVisible
    {
      get { return (bool)GetValue(ToolbarVisibleProperty); }
      set { SetValue(ToolbarVisibleProperty, value); }
    }

    /// <summary>
    /// Handles changes to the ToolbarVisible property.
    /// </summary>
    private static void OnToolbarVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      TextEditorWindow target = (TextEditorWindow)d;
      target.OnToolbarVisibleChanged((bool)e.OldValue, target.ToolbarVisible);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
    /// </summary>
    protected virtual void OnToolbarVisibleChanged(bool oldToolbarVisible, bool newToolbarVisible)
    {
      View.ToolbarVisible = newToolbarVisible;
    }

    #endregion

  }
}
