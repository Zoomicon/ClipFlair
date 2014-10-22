//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App_Errors.cs
//Version: 20140403

using System.Windows;
using System.Windows.Threading;

using ClipFlair.UI.Dialogs;
using ClipFlair.Resources;

namespace ClipFlair
{

  public partial class App: Application
  {

    private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
    {
      // If the app is running outside of the debugger then report the exception using
      // the browser's exception mechanism. On IE this will display it a yellow alert 
      // icon in the status bar and Firefox will display a script error.
      //if (System.Diagnostics.Debugger.IsAttached) return; //THIS DOESN'T SEEM TO WORK, FREEZES

      // NOTE: This will allow the application to continue running after an exception has been thrown
      // but not handled. 
      // For production applications this error handling should be replaced with something that will 
      // report the error to the website and stop the application.

      e.Handled = true;

      //try to show message on UI thread
      Dispatcher dispatcher = Deployment.Current.Dispatcher;
      if (dispatcher != null)
        dispatcher.BeginInvoke(
          () => ErrorDialog.Show(ClipFlairStudioStrings.MsgUnexpectedError, e.ExceptionObject)
          //ReportErrorToDOM(e) //don't use this, uses Browser's error facility (wouldn't work in OOB)
        );
    }

    /*
    private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
    {
      try
      {
        string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
        errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

        System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
      }
      catch (Exception)
      {
      }
    }
   */

  }

}
