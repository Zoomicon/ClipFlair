//Filename: FileUtils.cs
//Version: 20131113

using System;

namespace ClipFlair.CaptionsLib.Utils
{

	public static class FileUtils
	{

		#region --- Methods ---

		public static string CheckExtension(string filename, string extension)
		{
			if ((filename != null) && filename.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase))
				return extension;
			else
				return null;
		}

		public static string CheckExtension(string filename, string[] extensions)
		{
			if (filename != null) {
				foreach (string s in extensions) {
					if (filename.EndsWith(s, StringComparison.InvariantCultureIgnoreCase))
						return s;
				}
			}
			return null;
		}

		#endregion

	}

}