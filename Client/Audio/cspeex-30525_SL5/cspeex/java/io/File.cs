namespace java.io
{
    public class File
    {
        public File(string filename)
        {
            this.Filename = filename;
        }

        public void delete()
        {
            System.IO.File.Delete(Filename);
        }

        public long length()
        {
            return new System.IO.FileInfo(Filename).Length;
        }

        public string Filename { get; private set; }
    }
}
