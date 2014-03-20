namespace java.io
{
    public class ObjectStreamException : System.IO.IOException
    {
        public ObjectStreamException(string message)
            : base(message)
        {
        }
    }
}
