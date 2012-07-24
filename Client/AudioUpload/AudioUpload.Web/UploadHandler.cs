// TestClient.Web.UploadHandler.cs
// ----------------------------------------------------------------------------
// Licensed under the MIT license (http://www.opensource.org/licenses/mit-license.html)
// ----------------------------------------------------------------------------
// HighSpeed-Solutions, LLC
// Copyright (c) 2001-2012
// ----------------------------------------------------------------------------
// File:       UploadHandler.cs
// Author:     HSS\gbanta
// Created:    07/16/2011
// Modified:   07/16/2011
// ----------------------------------------------------------------------------
namespace AudioUpload.Web
{
	#region Using Directives
	using System;
	using System.IO;
	using HSS.Interlink.Web;
	#endregion

	#region UploadHandler
	/// <summary>
	/// The File Handler for Uploads.
	/// </summary>
	public class UploadHandler : BaseUploadHandler
	{
		/// <summary>
		/// Gets the absolute path of the requested file.
		/// </summary>
		private string FilePath
		{
			get
			{
				if (string.IsNullOrEmpty(_filePath))
					_filePath = Path.Combine(this.GetFolder(Global.StoredFiles), this.FileName);
				return _filePath;
			}
		} string _filePath;

		#region BaseUploadHandler Members
		public override bool IsAuthorized()
		{
			#region Validate Authorization
			//if (this.Context.User.Identity.IsAuthenticated)
			//{
			//    if (this.Context.User.IsInRole("requiredRole"))
			//        return true;
			//}
			//return false;
			#endregion

			return true;
		}
		public override bool CheckFileExists()
		{
			return File.Exists(FilePath);
		}
		public override void CreateNewFile()
		{
			if (File.Exists(FilePath))
				File.Delete(FilePath);
			File.Create(FilePath).Close();
		}
		public override Response AppendToFile(byte[] buffer)
		{
			#region Test Retry

			// If you're unable to append the bytes
			// you can request the client retry

			// Sample test for retry
			//if (sometest == false)
			//    return Responses.AppendFileRetry;

			#endregion

			using (var fs = File.Open(FilePath, FileMode.Append))
				fs.Write(buffer, 0, buffer.Length);

			return Response.Success;
		}
		public override void CancelUpload()
		{
			System.Diagnostics.Debug.WriteLine(this.FileName + " CANCELED");
			if (File.Exists(FilePath))
				File.Delete(FilePath);
		}
		public override string UploadComplete()
		{
			System.Diagnostics.Debug.WriteLine(this.Metadata);
			return this.FilePath; // Optional string to return to caller.
		}
		public override void OnError(ref Exception ex)
		{
			// Log Exception

			if (File.Exists(FilePath))
				File.Delete(FilePath);
		}
		#endregion
	}
	#endregion
}