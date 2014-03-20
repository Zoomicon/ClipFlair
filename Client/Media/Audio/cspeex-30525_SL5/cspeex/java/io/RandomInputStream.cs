namespace java.io
{
    public class RandomInputStream : InputStream
    {
        private System.IO.Stream stream;

        public RandomInputStream(System.IO.Stream stream)
        {
            this.stream = stream;
        }

        public System.IO.Stream InnerStream { get { return stream; } }

        public override int read()
        {
            return stream.ReadByte();
        }

        public override void close()
        {
            stream.Close();
        }
    }
}
