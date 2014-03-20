using System;
using HSS.Interlink.Web;

namespace AudioUpload.Web
{
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Gets the folder path where uploaded/downloaded files are stored; FileStore
		/// </summary>
		public static string StoredFiles = @"Uploads";

		protected void Application_Start(object sender, EventArgs e)
		{
			// Uncomment the following 2 lines to turn off purging of temp files.
			//DownloadTempCache.Current.AutoPurge = false;
			//UploadTempCache.Current.AutoPurge = false;

			DownloadTempCache.Current.PurgeInterval = new TimeSpan(0, 10, 0);
			UploadTempCache.Current.PurgeInterval = new TimeSpan(0, 10, 0);
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}