//Version: 20120702

using System;

namespace ClipFlair.Extensions
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
