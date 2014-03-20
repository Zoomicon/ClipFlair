//Version: 20130804

/*
NOTE: Please add 
  
#if SILVERLIGHT
using ExitEventArgs = System.EventArgs;
#endif  

at your source code file and then use 

Application.Current.Exit = Application_Exit;

private void Application_Exit(object sender, ExitEventArgs args)
{
  //...
}

instead of 

Application.Current.Exit = new ExitEventHandler(Application_Exit);

in both WPF and Silverlight in your source code
(Silverlight doesn't have ExitEventHandler and ExitEventargs as WPF does)

*/