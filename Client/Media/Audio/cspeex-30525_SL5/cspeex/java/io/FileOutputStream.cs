namespace java.io
{
    public class FileOutputStream : OutputStream
    {
        private File file;
        private System.IO.FileStream stream;

        public FileOutputStream(File file)
        {
            this.file = file;
            this.stream = System.IO.File.OpenWrite(file.Filename);
        }

        public override void write(int b)
        {
            stream.WriteByte((byte)b);
        }

        public override void close()
        {
            stream.Close();
        }
    }
}
