using System;
using System.Windows;

namespace CustomSlider_CS
{
	public partial class App : Application 
	{

		public App() 
		{
			this.Startup += this.OnStartup;
			this.Exit += this.OnExit;
			this.UnhandledException += this.Application_UnhandledException;

			InitializeComponent();
		}

		private void OnStartup(object sender, StartupEventArgs e) 
		{
			// Load the main control here
			this.RootVisual = new Page();
		}

		private void OnExit(object sender, EventArgs e) 
		{

		}

		private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			if (!System.Diagnostics.Debugger.IsAttached)
			{
				e.Handled = true;

				try
				{
					string errorMsg = e.ExceptionObject.Message + @"\n" + e.ExceptionObject.StackTrace;
					errorMsg = errorMsg.Replace("\"", "\\\"").Replace("\r\n", @"\n");

					System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight 2 Application: " + errorMsg + "\");");
				}
				catch (Exception)
				{
				}
			}
		}
	}
}