namespace java.io
{
    public class EOFException : System.IO.IOException
    {
        public EOFException()
        {
        }

        public EOFException(string message)
            : base(message)
        {
        }
    }
}
