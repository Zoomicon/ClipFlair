namespace java.io
{
    public class StreamCorruptedException : ObjectStreamException
    {
        public StreamCorruptedException(string message)
            : base(message)
        {
        }
    }
}
