//Filename: FloatingWindowCollection.cs
//Version: 20121103

using System.Collections.ObjectModel;

namespace SilverFlow.Controls
{

  public class FloatingWindowCollection : ObservableCollection<FloatingWindow>
  {

    public bool ShowOptionsButton
    {
      set
      {
        foreach (FloatingWindow w in this)
          w.ShowOptionsButton = value;
      }
    }
    
  }

}
