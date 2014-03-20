using cspeex;

namespace CSpeexTool
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] {"--rate", "44100", "--stereo", "test.pcm", "test.spx"};
            JSpeexEnc.DoMain(args);
            args = new string[] { "--stereo", "test.spx", "test.wav" };
            JSpeexDec.DoMain(args);
        }
    }
}
