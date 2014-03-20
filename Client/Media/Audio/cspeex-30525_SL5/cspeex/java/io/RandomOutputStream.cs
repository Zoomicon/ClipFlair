namespace java.io
{
    public class RandomOutputStream : OutputStream
    {
        private System.IO.Stream stream;

        public RandomOutputStream(System.IO.Stream stream)
        {
            this.stream = stream;
        }

        public System.IO.Stream InnerStream { get { return stream; } }

        public override void write(int b)
        {
            stream.WriteByte((byte)b);
        }

        public override void write(byte[] b)
        {
            stream.Write(b, 0, b.Length);
        }

        public override void write(byte[] b, int off, int len)
        {
            stream.Write(b, off, len);
        }

        public void writeBoolean(bool v)
        {
            byte[] temp = System.BitConverter.GetBytes(v);
            stream.Write(temp, 0, temp.Length);
        }

        public void writeByte(int v)
        {
            stream.WriteByte((byte)v);
        }

        public void writeShort(int v)
        {
            byte[] temp = System.BitConverter.GetBytes((short)v);
            stream.Write(temp, 0, temp.Length);
        }

        public void writeChar(int v)
        {
            stream.WriteByte((byte)v);
        }

        public void writeInt(int v)
        {
            byte[] temp = System.BitConverter.GetBytes(v);
            stream.Write(temp, 0, temp.Length);
        }

        public void writeLong(long v)
        {
            byte[] temp = System.BitConverter.GetBytes(v);
            stream.Write(temp, 0, temp.Length);
        }

        public void writeFloat(float v)
        {
            byte[] temp = System.BitConverter.GetBytes(v);
            stream.Write(temp, 0, temp.Length);
        }

        public void writeDouble(double v)
        {
            byte[] temp = System.BitConverter.GetBytes(v);
            stream.Write(temp, 0, temp.Length);
        }

        public void writeBytes(string s)
        {
            byte[] buff = cspeex.StringUtil.getBytesForString(s);

            stream.Write(buff, 0, buff.Length);
        }

        public void writeChars(string s)
        {
            byte[] buff = cspeex.StringUtil.getBytesForString(s);

            stream.Write(buff, 0, buff.Length);
        }

        public void writeUTF(string s)
        {
            byte[] buff = System.Text.Encoding.UTF8.GetBytes(s);

            stream.Write(buff, 0, buff.Length);
        }

        public void seek(int p)
        {
            stream.Position = p;
        }

        public int position()
        {
            return (int)stream.Position;
        }

        public int length()
        {
            return (int)stream.Length;
        }

        public override void close()
        {
            stream.Close();
        }
    }
}
