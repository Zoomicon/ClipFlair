//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ShareCommand.cs
//Version: 20130828

using ClipFlair.UI.Dialogs;
using Utils.Extensions;

using System;
using System.Collections;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls.Pivot;

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
