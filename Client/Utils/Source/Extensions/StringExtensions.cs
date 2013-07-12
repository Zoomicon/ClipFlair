//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StringExtensions.cs
//Version: 20130712

using System;
using System.Text.RegularExpressions;

namespace Utils.Extensions
{
  public static class StringExtensions
  {

    public static Uri ToUri(this string s)
    {
      if (String.IsNullOrEmpty(s))
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

    public static bool StartsWith(this string s, string[] suffixes, StringComparison comparisonType = StringComparison.CurrentCulture)
    {
      foreach (string suffix in suffixes)
        if (s.StartsWith(suffix, comparisonType))
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
        "[" + Regex.Escape(new String(System.IO.Path.GetInvalidPathChars())) + "]",
        replacement, //can even use a replacement string of any length
        RegexOptions.IgnoreCase);
        //not using System.IO.Path.InvalidPathChars (deprecated insecure API)
    }

  }

}
