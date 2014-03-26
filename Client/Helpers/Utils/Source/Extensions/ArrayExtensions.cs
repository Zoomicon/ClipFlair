//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ArrayExtensions.cs
//Version: 20140326

using System;
using System.Text.RegularExpressions;

namespace Utils.Extensions
{
  public static class ArrayExtensions
  {

    public static string Concatenate(this Array input, string separator = "\r\n")
    {
      string result = "";

      if (input != null)
        for (int i = 0; i < input.Length; i++)
          result += ((i != 0) ? separator : "") + input.GetValue(i).ToString();

      return result;
    }

  }

}
