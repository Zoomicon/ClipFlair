//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: StringExtensions.cs
//Version: 20121030


using System;

namespace ClipFlair.Utils.Extensions
{
    public static class StringExtensions
    {

        public static Uri ToUri(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            else
                return new Uri(input, UriKind.Absolute);
        }

    }
}
