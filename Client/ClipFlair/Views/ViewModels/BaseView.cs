//Filename: BaseView.cs
//Version: 20120730

using ClipFlair.Models.Views;

using System.ComponentModel;

namespace ClipFlair.Views
{
  public class BaseView: IView
  {

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged(string PropertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
    }

    #endregion

  }

}
