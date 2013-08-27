//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: DownloadCommand.cs
//Version: 20130828

using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class DownloadCommand : IPivotViewerUICommand
  {

    public string DisplayName
    {
      get { return ""; }
    }

    public System.Uri Icon
    {
      get { return new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Download.png"); }
    }

    public object ToolTip
    {
      get { return "Download"; }
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
