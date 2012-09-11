namespace cspeex
{
    public static class StringUtil
    {
        public static string getStringForBytes(byte[] bytes, int index, int count)
        {
            return System.Text.Encoding.ASCII.GetString(bytes, index, count);
        }

        public static byte[] getBytesForString(string str)
        {
            return System.Text.Encoding.ASCII.GetBytes(str);
        }

        public static string substring(string str, int startIndex, int length)
        {
            return str.Substring(startIndex, length);
        }

        public static bool equalsIgnoreCase(string str1, string str2)
        {
            return string.Equals(str1, str2, System.StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
