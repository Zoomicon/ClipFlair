//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: InfoCommand.cs
//Version: 20130828

using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class InfoCommand : IPivotViewerUICommand
  {
    PivotViewerItem item;

    public InfoCommand(PivotViewerItem theItem)
    {
      item = theItem;
    }

    public string DisplayName
    {
      get { return "(i)"; }
    }

    public System.Uri Icon
    {
      get { return null; /* new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Info.png"); */}
    }

    public object ToolTip
    {
      get { return item["Name"][0] + " - " + item["Description"][0]; }
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public event System.EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      //TODO
    }

  }

}
