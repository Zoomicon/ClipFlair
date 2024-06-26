﻿//Filename: PivotViewerExt.cs
//Version: 20140902

//using Microsoft.Internal.Pivot.Views;
using System;
using System.Linq;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Controls.Pivot;
using Compatibility;

namespace PivotViewerExt
{

  public class PivotViewerExt : PivotViewer //MetroPivotViewer //!!! (search for !!! below)
  {

    public PivotViewerExt()
    {
      //DefaultStyleKey = typeof(PivotViewerExt);

      FilterChanged += (s, e) =>
      {
        filter = Filter ?? ""; //if Filter is null, keep as "" instead
      };

      //SetValue(Clipper.ClipToBoundsProperty, true); //TODO: DOESN'T SEEM TO WORK, CAN'T CLIP PIVOTVIEWER'S CONTENT WHICH DRAWS ALL OVER THE SCREEN AT CHART MODE OR WHEN USING A SMALL SIZE WITH THE GRID MODE
    }

    #region Fields

    private CxmlCollectionSource cxml = null;
    private string filter = "";

    #endregion

    #region Properties

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
      Clear();

      if (newSource != null)
      {
        //IsLoading = true; //!!!

        cxml = new CxmlCollectionSource();
        cxml.StateChanged += OnCollectionStateChanged; //must first set this, then "UriSource" just in case of a collection that is cached and comes up very fast
        cxml.UriSource = newSource;
      }
    }

    #endregion

    #endregion

    #region Methods

    public void Clear()
    {
      if (cxml != null)
      {
        cxml.StateChanged -= OnCollectionStateChanged;
        cxml = null;
      }

      //IsLoading = false; //!!!

      filter = ""; //must clear this separately
      Filter = "";
      PivotProperties = null;
      ItemTemplates = null;
      ItemsSource = null;
    }

    public void RefreshFilter()
    {
      try
      {
        string f = Filter;
        Filter = "";
        Filter = f;
      }
      catch
      {
        //NOP
      }
    }

    #endregion

    #region Events

    /*
    //CollectionViewerView cvv;

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      
      //Grid partContainer = (Grid)this.GetTemplateChild("PART_Container");
      //cvv = (CollectionViewerView)partContainer.Children[2];
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
          //TODO: search for property names in WrapProperties list (need to add that) and set them to wrap
          ItemTemplates = cxml.ItemTemplates;
          ItemsSource = cxml.Items;

          Filter = filter; //re-apply filter
          //SortPivotProperty = (PivotViewerProperty)PivotProperties.GetEnumerator().Current; //doesn't work

          //View = GraphView; //need to refresh the view to show the images from the filter (could try to set the same view [GridView] or switch to other for a moment maybe). //TODO: add some event that CXML was loaded or make this overridable (?)
          //base.SetViewerState(base.SerializeViewerState()); //REFRESH

          InvalidateMeasure();
          InvalidateArrange();
          UpdateLayout();

          break;
      }
    }

    #endregion

    //////////////////////////////////////////////////////////////////////////////////////////////

  }

}
