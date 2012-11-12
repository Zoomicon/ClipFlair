//Filename: FloatingWindowCollection.cs
//Version: 20121112

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

    public void RemoveAll() //TODO: Windows.Clear() doesn't seem to work
    {
      FloatingWindow[] c = new FloatingWindow[Count];
      CopyTo(c, 0);
      foreach (FloatingWindow w in c) Remove(w);
    }
    
  }

}
