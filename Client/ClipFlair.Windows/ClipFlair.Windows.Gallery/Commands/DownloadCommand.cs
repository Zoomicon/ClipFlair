//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: DownloadCommand.cs
//Version: 20140616

using ClipFlair.UI.Dialogs;
using System;
using System.Windows.Controls.Pivot;
using Utils.Extensions;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class DownloadCommand : BaseCommand
  {

    public DownloadCommand(PivotViewerItem item)
      : base()
    {
      DisplayName = "";
      Icon = new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Download.png", UriKind.Relative); //must specify that this is a relative Uri

      string filename = (string)item["Filename"][0];
      string href = (string)item["Href"][0];
      bool isVideo = href.Contains("?video=");
      bool isImage = href.Contains("?image=");

      string url = "http://gallery.clipflair.net/" + ((isImage)? "image" : "activity") + "/" + filename;
      ToolTip = "Download " + url;
      IsExecutable = !string.IsNullOrWhiteSpace(url) && !isVideo;

      if (isVideo)
        throw new NotImplementedException(); //TODO: need to add .mp4 files for each smooth stream
    }

    public override void Execute(object parameter)
    {
      PivotViewerItem item = (PivotViewerItem)parameter;

      string filename = (string)item["Filename"][0]; 
      string href = (string)item["Href"][0];
      bool isVideo = href.Contains("?video=");
      bool isImage = href.Contains("?image=");

      Uri uri = new Uri("http://gallery.clipflair.net/" + ((isImage)? "image" : "activity") + "/" + filename);

      BrowserDialog.Show(uri);
    }

  }

}
