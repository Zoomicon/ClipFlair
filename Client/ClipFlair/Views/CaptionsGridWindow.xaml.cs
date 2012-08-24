﻿//Filename: CaptionsGridWindow.xaml.cs
//Version: 20120824

using ClipFlair.Models.Views;

using System;
using System.ComponentModel;
using System.Windows;

namespace ClipFlair.Views
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
      set
      {
        //remove property changed handler from old view
        if (DataContext != null)
          ((INotifyPropertyChanged)DataContext).PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged);
        //add property changed handler to new view
        value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
        //set the new view
        DataContext = value;
      }
    }

    protected void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == null)
      {
        Source = View.Source;
        //...
      }
      else if (e.PropertyName.Equals(ICaptionsGridProperties.PropertySource))
      {
        Source = View.Source;
      }
    }

    #endregion
    
    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(ICaptionsGridProperties.PropertySource, typeof(Uri), typeof(CaptionsGridWindow),
            new FrameworkPropertyMetadata((Uri)ICaptionsGridDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

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
      //TODO: load captions
    }

    #endregion

  }

}