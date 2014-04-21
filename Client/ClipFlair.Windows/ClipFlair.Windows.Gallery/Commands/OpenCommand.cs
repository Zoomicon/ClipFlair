//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: OpenCommand.cs
//Version: 20140421

using ClipFlair.UI.Dialogs;
using System;
using System.Windows.Controls.Pivot;
using Utils.Extensions;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class OpenCommand : BaseCommand
  {

    public OpenCommand(PivotViewerItem item) : base()
    {
      DisplayName = "";
      Icon = new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Open.png", UriKind.Relative); //must specify that this is a relative Uri
      string url = (string)item["Href"][0];
      ToolTip = "Open " + url;
      IsExecutable = !string.IsNullOrWhiteSpace(url);
    }

    public override void Execute(object parameter)
    {
      Uri uri = new Uri((string)((PivotViewerItem)parameter)["Href"][0]);
      
      BrowserDialog.Show(uri);
    }

  }

}
