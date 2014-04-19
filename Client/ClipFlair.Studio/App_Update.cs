//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App_Update.cs
//Version: 20140420

using System.Windows;

using ClipFlair.UI.Dialogs;
using System.Net.NetworkInformation;

namespace ClipFlair
{

  public partial class App: Application
  {

    #if SILVERLIGHT

    private void UpdateOOB()
    {
      if (!IsRunningOutOfBrowser) return;

      if (!NetworkInterface.GetIsNetworkAvailable()) return; //must check this, else CheckAndDownloadUploadAsync will return error at its callback when offline

      CheckAndDownloadUpdateCompleted += OnCheckAndDownloadUpdateCompleted; //attach event handler
      try
      {
        CheckAndDownloadUpdateAsync();
      }
      catch
      {
        //Ignore any exceptions
      }
    }
    
    #endif

    private void OnCheckAndDownloadUpdateCompleted(object sender, CheckAndDownloadUpdateCompletedEventArgs e)
    {
      CheckAndDownloadUpdateCompleted -= OnCheckAndDownloadUpdateCompleted; //detach event handler

      if (e.UpdateAvailable) //update was found and downloaded
        MessageDialog.Show("", "Update downloaded, will use at next launch");
      else if (e.Error != null) //error during update process
        ErrorDialog.Show("Update failed, will try again at next launch", e.Error);
    }

  }

}
