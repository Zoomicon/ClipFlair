//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App_Update.cs
//Version: 20140403

using System.Windows;

using ClipFlair.UI.Dialogs;

namespace ClipFlair
{

  public partial class App: Application
  {

    #if SILVERLIGHT

    private void UpdateOOB()
    {
      if (!IsRunningOutOfBrowser) return;

      CheckAndDownloadUpdateCompleted += new CheckAndDownloadUpdateCompletedEventHandler(OnCheckAndDownloadUpdateCompleted); //attach event handler
      try
      {
        CheckAndDownloadUpdateAsync();
      }
      catch
      {
        //Ignore any exceptions (e.g. when offline)
      }
    }
    
    #endif

    private void OnCheckAndDownloadUpdateCompleted(object sender, CheckAndDownloadUpdateCompletedEventArgs e)
    {
      CheckAndDownloadUpdateCompleted -= new CheckAndDownloadUpdateCompletedEventHandler(OnCheckAndDownloadUpdateCompleted); //detach event handler

      if (e.UpdateAvailable) //update was found and downloaded
        MessageDialog.Show("", "Update downloaded, will use at next launch");
      else if (e.Error != null) //error during update process
        ErrorDialog.Show("Update failed", e.Error);
    }

  }

}
