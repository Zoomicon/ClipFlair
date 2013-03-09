//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StringExtensions.cs
//Version: 20130122

using System;

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

  }
}
