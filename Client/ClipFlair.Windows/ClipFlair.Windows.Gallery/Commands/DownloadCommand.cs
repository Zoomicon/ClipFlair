//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: DownloadCommand.cs
//Version: 20130829

using ClipFlair.UI.Dialogs;
using Utils.Extensions;

using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls.Pivot;

namespace ClipFlair.Windows.Gallery.Commands
{

  public class DownloadCommand : BaseCommand
  {

    public DownloadCommand(PivotViewerItem item)
      : base()
    {
      DisplayName = "";
      Icon = new System.Uri("/ClipFlair.Windows.Gallery;component/Images/Download.png", UriKind.Relative); //must specify that this is a relative Uri

      bool isVideo = (((string)item["Href"][0]).Contains("?video="));
      string filename = (string)item["Filename"][0];

      string url = "http://gallery.clipflair.net/" + ((isVideo)?"video/"+filename+"/" : "activity/") + filename + ((isVideo)?".mp4":"");
      ToolTip = "Download " + url;
      IsExecutable = !string.IsNullOrWhiteSpace(url) && !isVideo;

      if (isVideo)
        throw new NotImplementedException(); //TODO: need to add .mp4 files for each smooth stream
    }

    public override void Execute(object parameter)
    {
      Uri uri = new Uri("http://gallery.clipflair.net/activity/" + (string)((PivotViewerItem)parameter)["Filename"][0]);
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
