//Filename: CaptionsGridWindow.xaml.cs
//Version: 20120814

using ClipFlair.Views;

using System;
using System.Windows;

namespace ClipFlair.Components
{
  public partial class CaptionsGridWindow : FlipWindow
  {

    public CaptionsGridWindow()
    {
      View = new CaptionsGridView(); //must set the view first
      InitializeComponent();
    }

    #region View

    public CaptionsGridView View
    {
      get { return (CaptionsGridView)DataContext; }
      set { DataContext = value; }
    }

    #endregion
    
    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(Uri), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata((Uri)null, new PropertyChangedCallback(OnSourceChanged)));

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
      CaptionsGridWindow target = (CaptionsGridWindow)d;
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

  }

}