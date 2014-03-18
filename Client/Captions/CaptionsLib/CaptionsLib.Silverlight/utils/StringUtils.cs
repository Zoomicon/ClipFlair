//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StringUtils.cs
//Version: 20131113

using System;

namespace ClipFlair.CaptionsLib.Utils
{

  public static class StringUtils
  {

    public const string vbCr = "\r";
    public const string vbLf = "\n";
    public const string vbCrLf = "\r\n";

    public static string[] Split(string s, string separator, StringSplitOptions options = StringSplitOptions.None)
    {
      if (s == null)
        return null;

      return s.Split(new string[] {separator}, options);
    }

  }

}