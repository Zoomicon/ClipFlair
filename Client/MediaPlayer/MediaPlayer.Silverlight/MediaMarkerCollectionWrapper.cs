//Filename: MediaMarkerWrapper.cs
//Version: 20120902

using System;
using System.Windows;
using System.ComponentModel;

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

    public MediaMarkerCollection<T> Markers
    {
      get { return null; }
      set
      { //TODO: listen for collection changed events (see ObservableCollection doc) on the wrapped list and on our list and sync them (using MediaMarkerWrapper objects for each item)
        //remove property changed handler from old media marker
        if (markers != null)
          ;// ((INotifyPropertyChanged)markers).PropertyChanged -= new PropertyChangedEventHandler(Markers_PropertyChanged);
        //set the new media marker
        markers = value;
        //add property changed handler to new media marker
        if (markers != null)
          ;// ((INotifyPropertyChanged)markers).PropertyChanged += new PropertyChangedEventHandler(Markers_PropertyChanged);
      }
    }
  
    #endregion

  }

}