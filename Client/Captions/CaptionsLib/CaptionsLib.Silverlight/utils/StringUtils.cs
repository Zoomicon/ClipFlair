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

    public static string CrToCrLf(this string s)
    {
      return s.Replace(vbCrLf, vbCr).Replace(vbCr, vbCrLf); //doing CrLf->Cr, Cr->CrLf to make sure that mixed Cr, CrLF strings ae converted to CrLf ok
    }

    public static string PrefixEmptyLines(this string s, string prefix)
    {
      return s.Replace(vbCrLf + vbCrLf, vbCrLf + prefix + vbCrLf);
    }

  }

}