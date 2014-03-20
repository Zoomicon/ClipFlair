namespace java.io
{
    public class FileInputStream : InputStream
    {
        private File file;
        private System.IO.FileStream stream;

        public FileInputStream(File file)
        {
            this.file = file;
            stream = new System.IO.FileStream(file.Filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
        }

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
