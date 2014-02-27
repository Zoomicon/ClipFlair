//Filename: MetroPivotViewer.cs
//Version: 20140225

//article: http://stevenhollidge.blogspot.gr/2012/12/pivotviewer-itemsloaded-event.html
//demo:    http://stevenhollidge.com/blog-source-code/pivotviewerwithitemsloadedevent
//source:  https://github.com/stevenh77/PivotViewerWithItemsLoadedEvent

using System;
using System.ComponentModel;
using System.Windows.Controls.Pivot;
using System.Windows.Threading;

namespace PivotViewerExt
{
  public class MetroPivotViewer: PivotViewer, INotifyPropertyChanged
  {

    #region --- Fields ---
 
    private string title;
    private bool isLoading; //=false
    private readonly UiHelper layoutUpdatedFinished;

    #endregion

    #region --- Properties ---

    /*
 
    public string Title  
    {
      get { return title; }
      set
      {
        if (title == value) return;
        title = value;
        NotifyPropertyChanged("Title");
      }
    }
    */

    public bool IsLoading
    {
      get { return isLoading; }
      set
      {
        if (isLoading != value)
        {
          isLoading = value;
          NotifyPropertyChanged("IsLoading");
        }
      }
    }

    #endregion

    public MetroPivotViewer()
    {
      layoutUpdatedFinished = new UiHelper();
      //IsLoading = true; //REMOVED, MUST SET EXPLICITLY BEFORE STARTING TO LOAD NEW COLLECTION

      this.StateSynchronizationFinished += MetroPivotViewer_StateSynchronizationFinished;
      this.LayoutUpdated += MetroPivotViewer_LayoutUpdated;
    }

    #region --- Events ---

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
    }

    void MetroPivotViewer_LayoutUpdated(object sender, EventArgs e)
    {
      layoutUpdatedFinished.Reset();
    }

    void MetroPivotViewer_StateSynchronizationFinished(object sender, EventArgs e)
    {
      layoutUpdatedFinished.SetTimeout(300, () =>
      {
        IsLoading = false;
        this.View = this.GraphView;
        NotifyItemsLoaded();
      });
    }

    public event EventHandler ItemsLoaded;
    public void NotifyItemsLoaded()
    {
      if (ItemsLoaded != null)
        ItemsLoaded(this, null);
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    public void NotifyPropertyChanged(string propName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propName));
    }

    #endregion

    #endregion

  }

  #region --- Helpers ---

  public class UiHelper
  {
    private readonly ActionableDispatcherTimer timer;

    public UiHelper()
    {
      timer = new ActionableDispatcherTimer();
    }

    public void SetTimeout(int milliseconds, Action action)
    {
      timer.Interval = new TimeSpan(0, 0, 0, 0, milliseconds);
      timer.Action = action;
      timer.Tick += OnTimeout;
      timer.Start();
    }

    public void Reset()
    {
      timer.Stop();
      timer.Start();
    }

    private void OnTimeout(object sender, EventArgs arg)
    {
      timer.Stop();
      timer.Tick -= OnTimeout;
      timer.Action();
    }
  }

  public class ActionableDispatcherTimer : DispatcherTimer
  {
    public Action Action { get; set; }
  }

  #endregion

}
