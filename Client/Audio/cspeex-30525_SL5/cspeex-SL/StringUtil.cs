namespace cspeex
{
    public static class StringUtil
    {
        public static string getStringForBytes(byte[] bytes, int index, int count)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.Append((char)bytes[index + i]);
            }

            return sb.ToString();
        }

        public static byte[] getBytesForString(string str)
        {
            byte[] temp = new byte[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                temp[i] = (byte)str[i];
            }

            return temp;
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
