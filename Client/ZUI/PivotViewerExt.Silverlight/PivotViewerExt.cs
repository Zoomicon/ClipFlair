//Filename: PivotViewerExt.cs
//Version: 20130327

using WPFCompatibility;

using Microsoft.Internal.Pivot.Views;

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Pivot;
using System.Collections.Generic;

namespace PivotViewerExt
{

  public class PivotViewerExt : PivotViewer
  {

    #region Fields

    private CxmlCollectionSource cxml = null;

    #endregion

    #region Source

    /// <summary>
    /// Source Dependency Property
    /// </summary>
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(Uri), typeof(PivotViewerExt),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnSourceChanged)));

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
      PivotViewerExt target = (PivotViewerExt)d;
      Uri oldSource = (Uri)e.OldValue;
      Uri newSource = target.Source;
      target.OnSourceChanged(oldSource, newSource);
    }

    /// <summary>
    /// Provides derived classes an opportunity to handle changes to the Source property.
    /// </summary>
    protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
    {
      if (newSource == null)
      {
        Clear();
        if (cxml != null)
        {
          cxml.StateChanged -= OnCollectionStateChanged;
          cxml = null;
        }
      }
      else
      {
        cxml = new CxmlCollectionSource();
        cxml.StateChanged += OnCollectionStateChanged; //must first set this, then "UriSource" just in case of a collection that is cached and comes up very fast
        cxml.UriSource = newSource;
      }
    }

    #endregion

    #region Methods

    public void Clear()
    {
      PivotProperties = null;
      ItemTemplates = null;
      ItemsSource = null;
    }

    #endregion

  
    #region Events

/*
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      Grid partContainer = (Grid)this.GetTemplateChild("PART_Container");
      CollectionViewerView cvv = (CollectionViewerView)partContainer.Children[2];
    }
*/

    private void OnCollectionStateChanged(object sender, CxmlCollectionStateChangedEventArgs e)
    {
      switch (e.NewState)
      {
        case CxmlCollectionState.Failed:
          Clear();
          break;
        case CxmlCollectionState.Loaded:
          PivotProperties = cxml.ItemProperties.ToList();
          ItemTemplates = cxml.ItemTemplates;
          ItemsSource = cxml.Items;
          break;
      }
    }

    #endregion

  }

}
