//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: TextEditorWindow.xaml.cs
//Version: 20121004

using ClipFlair.Models.Views;
using ClipFlair.Windows.Views;

using System;
using System.Windows;
using System.ComponentModel;

namespace ClipFlair.Windows
{
  public partial class TextEditorWindow : BaseWindow
  {
    public TextEditorWindow()
    {
      View = new TextEditorView(); //must set the view first
      InitializeComponent();
    }

    #region View

    public new ITextEditor View //hiding parent property
    {
      get { return (ITextEditor)base.View; } //delegating to parent property
      set { base.View = value; }
    }

    protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.View_PropertyChanged(sender, e);

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
            new FrameworkPropertyMetadata(ITextEditorDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

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
            new FrameworkPropertyMetadata(ITextEditorDefaults.DefaultToolbarVisible, new PropertyChangedCallback(OnToolbarVisibleChanged)));

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
