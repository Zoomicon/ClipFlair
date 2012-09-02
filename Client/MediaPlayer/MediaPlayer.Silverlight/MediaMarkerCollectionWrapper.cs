//Filename: MediaMarkerWrapper.cs
//Version: 20120902

using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.Specialized;

using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities;

namespace Zoomicon.MediaPlayer
{
 
  public class MediaMarkerCollectionWrapper<T> : ScriptableObservableCollection<MediaMarkerWrapper<T>> where T : MediaMarker
  {

    #region --- Fields ---

    private MediaMarkerCollection<T> markers;

    #endregion

    #region --- Properties ---

    #region Markers

    public MediaMarkerCollection<T> Markers
    {
      get { return null; }
      set
      {
        //remove all media marker wrappers from this collection
        Clear();

        //remove collection changed handler from old media markers collection
        if (markers != null)
          markers.CollectionChanged -= new NotifyCollectionChangedEventHandler(Markers_CollectionChanged);

        //set the new markers
        markers = value;
       
        //add collection changed handler to new media markers collection
        if (markers != null)
          markers.CollectionChanged += new NotifyCollectionChangedEventHandler(Markers_CollectionChanged);

        //create and add media marker wrappers to this collection
        foreach (T marker in value)
          Add(new MediaMarkerWrapper<T>(marker));
      }
    }

    #endregion

    #endregion

    #region --- Methods

    public MediaMarkerCollectionWrapper(MediaMarkerCollection<T> theMarkers)
    {
      Markers = theMarkers; //must set the property, not the field in order to generate wrappers and add listeners etc.
      //TODO: we could also listen for the wrapper collection changes (or override the ancestor's protected methbods) and sync them with the markers collection (but make sure to not do spurious loop)
    }

    #endregion

    #region --- Events ---

    private void Markers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
    {
      switch (args.Action)
      {
        case NotifyCollectionChangedAction.Add:
          foreach (T item in args.NewItems)
            Add(new MediaMarkerWrapper<T>(item)); //TODO shouldn't we Insert at the correct position? (see args.NewStartingIndex)
          break;

        case NotifyCollectionChangedAction.Remove:
          foreach (T item in args.OldItems)
            foreach (MediaMarkerWrapper<T> wrapper in Items) //TODO: since the wrapper and the wrapped collections have a 1-1 relationship, could use args.OldStartingIndex instead and args.OldItems.Count to remove needed elements
              if (wrapper.Marker == item)
                Remove(wrapper);
          break;
      }
    }

    #endregion

  }

}