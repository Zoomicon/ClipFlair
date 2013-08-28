//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: OpenCommand.cs
//Version: 20130828

using ClipFlair.UI.Dialogs;
using Utils.Extensions;

using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class OpenCommand : BaseCommand
  {

    public OpenCommand(PivotViewerItem item) : base()
    {
      DisplayName = ">";
      Icon = null; //new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Open.png";
      string url = (string)item["Href"][0];
      ToolTip = "Open " + url;
      IsExecutable = !string.IsNullOrWhiteSpace(url);
    }

    public override void Execute(object parameter)
    {
      Uri uri = new Uri((string)((PivotViewerItem)parameter)["Href"][0]);
      try
      {
        uri.NavigateTo();
      }
      catch
      {
        MessageDialog.Show("Navigation", "Please visit " + uri); //TODO: use URLDialog here with clickable URL on it
      }
    }

  }

}
