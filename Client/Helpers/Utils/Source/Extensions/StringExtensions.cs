//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StringExtensions.cs
//Version: 20140404

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Utils.Extensions
{
  public static class StringExtensions
  {

    public static bool IsEmpty(this string s)
    {
      return (s.Trim().Length == 0);
    }

    public static Uri ToUri(this string s)
    {
      if (string.IsNullOrEmpty(s))
        return null;
      else
        return new Uri(s, UriKind.Absolute);
    }

    public static int WordCount(this string s)
    {
      int last = s.Length-1;

      int count = 0;
      for (int i = 0; i <= last; i++)
      {
        if ( char.IsLetterOrDigit(s[i]) &&
             ((i==last) || char.IsWhiteSpace(s[i+1]) || char.IsPunctuation(s[i+1])) )
          count++;
      }
      return count;
    }

    public static bool StartsWith(this string s, string[] prefixes, StringComparison comparisonType = StringComparison.CurrentCulture)
    {
      foreach (string prefix in prefixes)
        if (s.StartsWith(prefix, comparisonType))
          return true;
      return false;
    }

    public static bool EndsWith(this string s, string[] suffixes, StringComparison comparisonType = StringComparison.CurrentCulture)
    {
      foreach (string suffix in suffixes)
        if (s.EndsWith(suffix, comparisonType)) 
          return true;
      return false;
    }

    public static string ReplacePrefix(this string s, string fromPrefix, string toPrefix, StringComparison comparisonType = StringComparison.CurrentCulture)
    {
      return (s.StartsWith(fromPrefix, comparisonType)) ? toPrefix + s.Substring(fromPrefix.Length) : s;
    }

    public static string ReplacePrefix(this string s, string[] fromPrefix, string toPrefix, StringComparison comparisonType = StringComparison.CurrentCulture)
    {
      foreach (string prefix in fromPrefix)
        if (s.StartsWith(prefix, comparisonType))
          return toPrefix + s.Substring(prefix.Length);
      return s;
    }

    public static string ReplaceSuffix(this string s, string fromSuffix, string toSuffix, StringComparison comparisonType = StringComparison.CurrentCulture)
    {
      return (s.EndsWith(fromSuffix, comparisonType)) ? s.Substring(0, s.Length - fromSuffix.Length) + toSuffix : s;
    }

    public static string ReplaceSuffix(this string s, string[] fromSuffix, string toSuffix, StringComparison comparisonType = StringComparison.CurrentCulture)
    {
      foreach (string suffix in fromSuffix)
        if (s.EndsWith(suffix, comparisonType))
          return s.Substring(0, s.Length - suffix.Length) + toSuffix;
      return s;
    }

    public static string ReplaceInvalidFileNameChars(this string s, string replacement = "")
    {
      return Regex.Replace(s,
        "[" + Regex.Escape(
          Path.VolumeSeparatorChar + //slash ("/") on UNIX, and a backslash ("\") on the Windows and Macintosh operating systems
          Path.DirectorySeparatorChar + //slash ("/") on UNIX, and a backslash ("\") on the Windows and Macintosh operating systems
          Path.AltDirectorySeparatorChar + //backslash ('\') on UNIX, and a slash ('/') on Windows and Macintosh operating systems
          ":" + //added to cover Windows & Mac in case code is run on UNIX
          "\\" + //added for future platforms that use totally different separator chars (those would still have problem if we used such chars when running on Win/Mac/Unix)
          "/" + //same as previous one
          "<" +
          ">" +
          "|" +
          "\b" +
          "\0" +
          "\t" + //based on characters not allowed on Windows mentioned at http://msdn.microsoft.com/en-us/library/system.io.path.getinvalidpathchars(v=vs.110).aspx
          new string(Path.GetInvalidPathChars()) + //this seems to miss *, ? and " in Silverlight 5.1 (The array returned from this method is not guaranteed to contain the complete set of characters that are invalid in file and directory names)
          "*" +
          "?" +
          "\""
          ) + "]",
        replacement, //can even use a replacement string of any length
        RegexOptions.IgnoreCase);
        //not using System.IO.Path.InvalidPathChars (deprecated insecure API)
    }

  }

}
