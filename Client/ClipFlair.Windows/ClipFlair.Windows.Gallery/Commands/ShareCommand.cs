//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ShareCommand.cs
//Version: 20140421

using ClipFlair.UI.Dialogs;
using System;
using System.Collections;
using System.Windows.Controls.Pivot;
using Utils.Extensions;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class ShareCommand : BaseCommand
  {

    public ShareCommand(PivotViewerItem item) : base()
    {
      DisplayName = "";
      Icon = new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Share.png", UriKind.Relative); //must specify that this is a relative Uri
      string url = (string)item["Href"][0];
      ToolTip = "Share " + url;
      IsExecutable = !string.IsNullOrWhiteSpace(url);
    }

    public override void Execute(object parameter)
    {
      PivotViewerItem item = (PivotViewerItem)parameter;

      IList nameData = item["Name"];
      string name = (nameData != null && nameData.Count > 0)? (string)nameData[0] : "";
      if (name == null)
        name = ""; //just to be safe

      Uri uri = new Uri("http://api.addthis.com/oexchange/0.8/offer?url=" 
                + (string)item["Href"][0]
                + "&title=" + name
                );

      BrowserDialog.Show(uri);
    }

  }

}
