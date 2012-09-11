namespace cspeex
{
    /// <summary>Java Speex Command Line Decoder.</summary>
    /// <remarks>
    /// Java Speex Command Line Decoder.
    /// Decodes SPX files created by Speex's speexenc utility to WAV entirely in pure java.
    /// Currently this code has been updated to be compatible with release 1.0.3.
    /// NOTE!!! A number of advanced options are NOT supported.
    /// --  DTX implemented but untested.
    /// --  Packet loss support implemented but untested.
    /// --  SPX files with more than one comment.
    /// --  Can't force decoder to run at another rate, mode, or channel count.
    /// </remarks>
    /// <author>Jim Lawrence, helloNetwork.com</author>
    /// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
    /// <version>$Revision: 155 $</version>
    public class JSpeexDec
    {
        /// <summary>Version of the Speex Encoder</summary>
        public static readonly string VERSION = "Java Speex Command Line Decoder v0.9.7 ($Revision: 155 $)";

        /// <summary>Copyright display String</summary>
        public static readonly string COPYRIGHT = "Copyright (C) 2002-2004 Wimba S.A.";

        /// <summary>Print level for messages : Print debug information</summary>
        public const int DEBUG = 0;

        /// <summary>Print level for messages : Print basic information</summary>
        public const int INFO = 1;

        /// <summary>Print level for messages : Print only warnings and errors</summary>
        public const int WARN = 2;

        /// <summary>Print level for messages : Print only errors</summary>
        public const int ERROR = 3;

        /// <summary>Print level for messages</summary>
        protected int printlevel = INFO;

        /// <summary>File format for input or output audio file: Raw</summary>
        public const int FILE_FORMAT_RAW = 0;

        /// <summary>File format for input or output audio file: Ogg</summary>
        public const int FILE_FORMAT_OGG = 1;

        /// <summary>File format for input or output audio file: Wave</summary>
        public const int FILE_FORMAT_WAVE = 2;

        /// <summary>Defines File format for input audio file (Raw, Ogg or Wave).</summary>
        /// <remarks>Defines File format for input audio file (Raw, Ogg or Wave).</remarks>
        protected int srcFormat = FILE_FORMAT_OGG;

        /// <summary>Defines File format for output audio file (Raw or Wave).</summary>
        /// <remarks>Defines File format for output audio file (Raw or Wave).</remarks>
        protected int destFormat = FILE_FORMAT_WAVE;

        /// <summary>Random number generator for packet loss simulation.</summary>
        /// <remarks>Random number generator for packet loss simulation.</remarks>
        protected static java.util.Random random = new java.util.Random();

        /// <summary>Speex Decoder</summary>
        protected org.xiph.speex.SpeexDecoder speexDecoder;

        /// <summary>Defines whether or not the perceptual enhancement is used.</summary>
        /// <remarks>Defines whether or not the perceptual enhancement is used.</remarks>
        protected bool enhanced = true;

        /// <summary>If input is raw, defines the decoder mode (0=NB, 1=WB and 2-UWB).</summary>
        /// <remarks>If input is raw, defines the decoder mode (0=NB, 1=WB and 2-UWB).</remarks>
        private int mode = 0;

        /// <summary>If input is raw, defines the quality setting used by the encoder.</summary>
        /// <remarks>If input is raw, defines the quality setting used by the encoder.</remarks>
        private int quality = 8;

        /// <summary>If input is raw, defines the number of frmaes per packet.</summary>
        /// <remarks>If input is raw, defines the number of frmaes per packet.</remarks>
        private int nframes = 1;

        /// <summary>If input is raw, defines the sample rate of the audio.</summary>
        /// <remarks>If input is raw, defines the sample rate of the audio.</remarks>
        private int sampleRate = -1;

        private float vbr_quality = -1;

        private bool vbr = false;

        /// <summary>If input is raw, defines th number of channels (1=mono, 2=stereo).</summary>
        /// <remarks>If input is raw, defines th number of channels (1=mono, 2=stereo).</remarks>
        private int channels = 1;

        /// <summary>The percentage of packets to lose in the packet loss simulation.</summary>
        /// <remarks>The percentage of packets to lose in the packet loss simulation.</remarks>
        private int loss = 0;

        /// <summary>The audio input file</summary>
        protected string srcFile;

        /// <summary>The audio output file</summary>
        protected string destFile;

        /// <summary>Builds a plain JSpeex Decoder with default values.</summary>
        /// <remarks>Builds a plain JSpeex Decoder with default values.</remarks>
        public JSpeexDec()
        {
        }

        /// <summary>
        /// Command line entrance:
        /// <pre>
        /// Usage: JSpeexDec [options] input_file output_file
        /// </pre>
        /// </summary>
        /// <param name="args">Command line parameters.</param>
        /// <exception>IOException</exception>
        /// <exception cref="System.IO.IOException"></exception>
        public static void DoMain(string[] args)
        {
            JSpeexDec decoder = new JSpeexDec();
            if (decoder.parseArgs(args))
            {
                decoder.decode();
            }
        }

        public void setDestFormat(int format)
        {
            this.destFormat = format;
        }

        public void setStereo(bool stereo)
        {
            this.channels = stereo ? 2 : 1;
        }

        /// <summary>Parse the command line arguments.</summary>
        /// <remarks>Parse the command line arguments.</remarks>
        /// <param name="args">Command line parameters.</param>
        /// <returns>true if the parsed arguments are sufficient to run the decoder.</returns>
        public virtual bool parseArgs(string[] args)
        {
            // make sure we have command args
            if (args.Length < 2)
            {
                if (args.Length == 1 && (args[0].Equals("-v") || args[0].Equals("--version")))
                {
                    version();
                    return false;
                }
                usage();
                return false;
            }
            // Determine input, output and file formats
            srcFile = args[args.Length - 2];
            destFile = args[args.Length - 1];
            if (srcFile.ToLower().EndsWith(".spx"))
            {
                srcFormat = FILE_FORMAT_OGG;
            }
            else
            {
                if (srcFile.ToLower().EndsWith(".wav"))
                {
                    srcFormat = FILE_FORMAT_WAVE;
                }
                else
                {
                    srcFormat = FILE_FORMAT_RAW;
                }
            }
            if (destFile.ToLower().EndsWith(".wav"))
            {
                destFormat = FILE_FORMAT_WAVE;
            }
            else
            {
                destFormat = FILE_FORMAT_RAW;
            }
            // Determine encoder options
            for (int i = 0; i < args.Length - 2; i++)
            {
                if (cspeex.StringUtil.equalsIgnoreCase(args[i], "-h") || cspeex.StringUtil.equalsIgnoreCase
                    (args[i], "--help"))
                {
                    usage();
                    return false;
                }
                else
                {
                    if (cspeex.StringUtil.equalsIgnoreCase(args[i], "-v") || cspeex.StringUtil.equalsIgnoreCase
                        (args[i], "--version"))
                    {
                        version();
                        return false;
                    }
                    else
                    {
                        if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--verbose"))
                        {
                            printlevel = DEBUG;
                        }
                        else
                        {
                            if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--quiet"))
                            {
                                printlevel = WARN;
                            }
                            else
                            {
                                if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--enh"))
                                {
                                    enhanced = true;
                                }
                                else
                                {
                                    if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--no-enh"))
                                    {
                                        enhanced = false;
                                    }
                                    else
                                    {
                                        if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--packet-loss"))
                                        {
                                            try
                                            {
                                                loss = int.Parse(args[++i]);
                                            }
                                            catch (System.FormatException)
                                            {
                                                usage();
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (cspeex.StringUtil.equalsIgnoreCase(args[i], "-n") || cspeex.StringUtil.equalsIgnoreCase
                                                (args[i], "-nb") || cspeex.StringUtil.equalsIgnoreCase(args[i], "--narrowband"))
                                            {
                                                mode = 0;
                                            }
                                            else
                                            {
                                                if (cspeex.StringUtil.equalsIgnoreCase(args[i], "-w") || cspeex.StringUtil.equalsIgnoreCase
                                                    (args[i], "-wb") || cspeex.StringUtil.equalsIgnoreCase(args[i], "--wideband"))
                                                {
                                                    mode = 1;
                                                }
                                                else
                                                {
                                                    if (cspeex.StringUtil.equalsIgnoreCase(args[i], "-u") || cspeex.StringUtil.equalsIgnoreCase
                                                        (args[i], "-uwb") || cspeex.StringUtil.equalsIgnoreCase(args[i], "--ultra-wideband"
                                                        ))
                                                    {
                                                        mode = 2;
                                                    }
                                                    else
                                                    {
                                                        if (cspeex.StringUtil.equalsIgnoreCase(args[i], "-q") || cspeex.StringUtil.equalsIgnoreCase
                                                            (args[i], "--quality"))
                                                        {
                                                            try
                                                            {
                                                                vbr_quality = float.Parse(args[++i]);
                                                                quality = (int)vbr_quality;
                                                            }
                                                            catch (System.FormatException)
                                                            {
                                                                usage();
                                                                return false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--nframes"))
                                                            {
                                                                try
                                                                {
                                                                    nframes = int.Parse(args[++i]);
                                                                }
                                                                catch (System.FormatException)
                                                                {
                                                                    usage();
                                                                    return false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--vbr"))
                                                                {
                                                                    vbr = true;
                                                                }
                                                                else
                                                                {
                                                                    if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--stereo"))
                                                                    {
                                                                        channels = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        usage();
                                                                        return false;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (sampleRate < 0)
            {
                switch (mode)
                {
                    case 0:
                        {
                            sampleRate = 8000;
                            break;
                        }

                    case 1:
                        {
                            sampleRate = 16000;
                            break;
                        }

                    case 2:
                        {
                            sampleRate = 32000;
                            break;
                        }

                    default:
                        {
                            sampleRate = 8000;
                            break;
                        }
                }
            }
            return true;
        }

        /// <summary>Prints the usage guidelines.</summary>
        /// <remarks>Prints the usage guidelines.</remarks>
        public static void usage()
        {
            version();
            System.Console.WriteLine("Usage: JSpeexDec [options] input_file output_file");
            System.Console.WriteLine("Where:");
            System.Console.WriteLine("  input_file can be:");
            System.Console.WriteLine("    filename.spx  an Ogg Speex file");
            System.Console.WriteLine("    filename.wav  a Wave Speex file (beta!!!)");
            System.Console.WriteLine("    filename.*    a raw Speex file");
            System.Console.WriteLine("  output_file can be:");
            System.Console.WriteLine("    filename.wav  a PCM wav file");
            System.Console.WriteLine("    filename.*    a raw PCM file (any extension other than .wav)"
                );
            System.Console.WriteLine("Options: -h, --help     This help");
            System.Console.WriteLine("         -v, --version    Version information");
            System.Console.WriteLine("         --verbose        Print detailed information"
                );
            System.Console.WriteLine("         --quiet          Print minimal information"
                );
            System.Console.WriteLine("         --enh            Enable perceptual enhancement (default)"
                );
            System.Console.WriteLine("         --no-enh         Disable perceptual enhancement"
                );
            System.Console.WriteLine("         --packet-loss n  Simulate n % random packet loss"
                );
            System.Console.WriteLine("         if the input file is raw Speex (not Ogg Speex)"
                );
            System.Console.WriteLine("         -n, -nb          Narrowband (8kHz)");
            System.Console.WriteLine("         -w, -wb          Wideband (16kHz)");
            System.Console.WriteLine("         -u, -uwb         Ultra-Wideband (32kHz)");
            System.Console.WriteLine("         --quality n      Encoding quality (0-10) default 8"
                );
            System.Console.WriteLine("         --nframes n      Number of frames per Ogg packet, default 1"
                );
            System.Console.WriteLine("         --vbr            Enable varible bit-rate (VBR)"
                );
            System.Console.WriteLine("         --stereo         Consider input as stereo"
                );
            System.Console.WriteLine("More information is available from: http://jspeex.sourceforge.net/"
                );
            System.Console.WriteLine("This code is a Java port of the Speex codec: http://www.speex.org/"
                );
        }

        /// <summary>Prints the version.</summary>
        /// <remarks>Prints the version.</remarks>
        public static void version()
        {
            System.Console.WriteLine(VERSION);
            System.Console.WriteLine("using " + org.xiph.speex.SpeexDecoder.VERSION);
            System.Console.WriteLine(COPYRIGHT);
        }

        /// <summary>Decodes a Speex file to PCM.</summary>
        /// <remarks>Decodes a Speex file to PCM.</remarks>
        /// <exception>IOException</exception>
        /// <exception cref="System.IO.IOException"></exception>
        public virtual void decode()
        {
            decode(new java.io.File(srcFile), new java.io.File(destFile));
        }

        /// <summary>Decodes a Speex file to PCM.</summary>
        /// <remarks>Decodes a Speex file to PCM.</remarks>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <exception>IOException</exception>
        /// <exception cref="System.IO.IOException"></exception>
        public virtual void decode(java.io.File srcPath, java.io.File destPath)
        {
            byte[] header = new byte[2048];
            byte[] payload = new byte[65536];
            byte[] decdat = new byte[44100 * 2 * 2];
            int WAV_HEADERSIZE = 8;
            short WAVE_FORMAT_SPEEX = (short)unchecked((short)(0xa109));
            string RIFF = "RIFF";
            string WAVE = "WAVE";
            string FORMAT = "fmt ";
            string DATA = "data";
            int OGG_HEADERSIZE = 27;
            int OGG_SEGOFFSET = 26;
            string OGGID = "OggS";
            int segments = 0;
            int curseg = 0;
            int bodybytes = 0;
            int decsize = 0;
            int packetNo = 0;
            // Display info
            if (printlevel <= INFO)
            {
                version();
            }
            if (printlevel <= DEBUG)
            {
                System.Console.WriteLine(string.Empty);
            }
            if (printlevel <= DEBUG)
            {
                System.Console.WriteLine("Input File: " + srcPath);
            }
            // construct a new decoder
            speexDecoder = new org.xiph.speex.SpeexDecoder();
            // open the input stream
            java.io.DataInputStream dis = new java.io.DataInputStream(new java.io.FileInputStream
                (srcPath));
            org.xiph.speex.AudioFileWriter writer = null;
            int origchksum;
            int chksum;
            try
            {
                // read until we get to EOF
                while (true)
                {
                    if (srcFormat == FILE_FORMAT_OGG)
                    {
                        // read the OGG header
                        dis.readFully(header, 0, OGG_HEADERSIZE);
                        origchksum = readInt(header, 22);
                        header[22] = 0;
                        header[23] = 0;
                        header[24] = 0;
                        header[25] = 0;
                        chksum = org.xiph.speex.OggCrc.checksum(0, header, 0, OGG_HEADERSIZE);
                        // make sure its a OGG header
                        if (!OGGID.Equals(cspeex.StringUtil.getStringForBytes(header, 0, 4)))
                        {
                            System.Console.Error.WriteLine("missing ogg id!");
                            return;
                        }
                        segments = header[OGG_SEGOFFSET] & unchecked((int)(0xFF));
                        dis.readFully(header, OGG_HEADERSIZE, segments);
                        chksum = org.xiph.speex.OggCrc.checksum(chksum, header, OGG_HEADERSIZE, segments);
                        for (curseg = 0; curseg < segments; curseg++)
                        {
                            bodybytes = header[OGG_HEADERSIZE + curseg] & unchecked((int)(0xFF));
                            if (bodybytes == 255)
                            {
                                System.Console.Error.WriteLine("sorry, don't handle 255 sizes!");
                                return;
                            }
                            dis.readFully(payload, 0, bodybytes);
                            chksum = org.xiph.speex.OggCrc.checksum(chksum, payload, 0, bodybytes);
                            if (packetNo == 0)
                            {
                                if (readSpeexHeader(payload, 0, bodybytes))
                                {
                                    if (printlevel <= DEBUG)
                                    {
                                        System.Console.WriteLine("File Format: Ogg Speex");
                                        System.Console.WriteLine("Sample Rate: " + sampleRate);
                                        System.Console.WriteLine("Channels: " + channels);
                                        System.Console.WriteLine("Encoder mode: " + (mode == 0 ? "Narrowband" : (mode
                                             == 1 ? "Wideband" : "UltraWideband")));
                                        System.Console.WriteLine("Frames per packet: " + nframes);
                                    }
                                    if (destFormat == FILE_FORMAT_WAVE)
                                    {
                                        writer = new org.xiph.speex.PcmWaveWriter(speexDecoder.getSampleRate(), speexDecoder
                                            .getChannels());
                                        if (printlevel <= DEBUG)
                                        {
                                            System.Console.WriteLine(string.Empty);
                                            System.Console.WriteLine("Output File: " + destPath);
                                            System.Console.WriteLine("File Format: PCM Wave");
                                            System.Console.WriteLine("Perceptual Enhancement: " + enhanced);
                                        }
                                    }
                                    else
                                    {
                                        writer = new org.xiph.speex.RawWriter();
                                        if (printlevel <= DEBUG)
                                        {
                                            System.Console.WriteLine(string.Empty);
                                            System.Console.WriteLine("Output File: " + destPath);
                                            System.Console.WriteLine("File Format: Raw Audio");
                                            System.Console.WriteLine("Perceptual Enhancement: " + enhanced);
                                        }
                                    }
                                    writer.open(destPath);
                                    writer.writeHeader(null);
                                    packetNo++;
                                }
                                else
                                {
                                    packetNo = 0;
                                }
                            }
                            else
                            {
                                if (packetNo == 1)
                                {
                                    // Ogg Comment packet
                                    packetNo++;
                                }
                                else
                                {
                                    if (loss > 0 && random.nextInt(100) < loss)
                                    {
                                        speexDecoder.processData(null, 0, bodybytes);
                                        for (int i = 1; i < nframes; i++)
                                        {
                                            speexDecoder.processData(true);
                                        }
                                    }
                                    else
                                    {
                                        speexDecoder.processData(payload, 0, bodybytes);
                                        for (int i = 1; i < nframes; i++)
                                        {
                                            speexDecoder.processData(false);
                                        }
                                    }
                                    if ((decsize = speexDecoder.getProcessedData(decdat, 0)) > 0)
                                    {
                                        writer.writePacket(decdat, 0, decsize);
                                    }
                                    packetNo++;
                                }
                            }
                        }
                        if (chksum != origchksum)
                        {
                            throw new System.IO.IOException("Ogg CheckSums do not match");
                        }
                    }
                    else
                    {
                        // Wave or Raw Speex
                        if (packetNo == 0)
                        {
                            if (srcFormat == FILE_FORMAT_WAVE)
                            {
                                // read the WAVE header
                                dis.readFully(header, 0, WAV_HEADERSIZE + 4);
                                // make sure its a WAVE header
                                if (!RIFF.Equals(cspeex.StringUtil.getStringForBytes(header, 0, 4)) && !WAVE.Equals
                                    (cspeex.StringUtil.getStringForBytes(header, 8, 4)))
                                {
                                    System.Console.Error.WriteLine("Not a WAVE file");
                                    return;
                                }
                                // Read other header chunks
                                dis.readFully(header, 0, WAV_HEADERSIZE);
                                string chunk = cspeex.StringUtil.getStringForBytes(header, 0, 4);
                                int size = readInt(header, 4);
                                while (!chunk.Equals(DATA))
                                {
                                    dis.readFully(header, 0, size);
                                    if (chunk.Equals(FORMAT))
                                    {
                                        if (readShort(header, 0) != WAVE_FORMAT_SPEEX)
                                        {
                                            System.Console.Error.WriteLine("Not a Wave Speex file");
                                            return;
                                        }
                                        channels = readShort(header, 2);
                                        sampleRate = readInt(header, 4);
                                        bodybytes = readShort(header, 12);
                                        if (readShort(header, 16) < 82)
                                        {
                                            System.Console.Error.WriteLine("Possibly corrupt Speex Wave file.");
                                            return;
                                        }
                                        readSpeexHeader(header, 20, 80);
                                        // Display audio info
                                        if (printlevel <= DEBUG)
                                        {
                                            System.Console.WriteLine("File Format: Wave Speex");
                                            System.Console.WriteLine("Sample Rate: " + sampleRate);
                                            System.Console.WriteLine("Channels: " + channels);
                                            System.Console.WriteLine("Encoder mode: " + (mode == 0 ? "Narrowband" : (mode
                                                 == 1 ? "Wideband" : "UltraWideband")));
                                            System.Console.WriteLine("Frames per packet: " + nframes);
                                        }
                                    }
                                    dis.readFully(header, 0, WAV_HEADERSIZE);
                                    chunk = cspeex.StringUtil.getStringForBytes(header, 0, 4);
                                    size = readInt(header, 4);
                                }
                                if (printlevel <= DEBUG)
                                {
                                    System.Console.WriteLine("Data size: " + size);
                                }
                            }
                            else
                            {
                                if (printlevel <= DEBUG)
                                {
                                    System.Console.WriteLine("File Format: Raw Speex");
                                    System.Console.WriteLine("Sample Rate: " + sampleRate);
                                    System.Console.WriteLine("Channels: " + channels);
                                    System.Console.WriteLine("Encoder mode: " + (mode == 0 ? "Narrowband" : (mode
                                         == 1 ? "Wideband" : "UltraWideband")));
                                    System.Console.WriteLine("Frames per packet: " + nframes);
                                }
                                speexDecoder.init(mode, sampleRate, channels, enhanced);
                                if (!vbr)
                                {
                                    switch (mode)
                                    {
                                        case 0:
                                            {
                                                bodybytes = org.xiph.speex.NbEncoder.NB_FRAME_SIZE[org.xiph.speex.NbEncoder.NB_QUALITY_MAP
                                                    [quality]];
                                                break;
                                            }

                                        case 1:
                                            {
                                                //Wideband
                                                bodybytes = org.xiph.speex.SbEncoder.NB_FRAME_SIZE[org.xiph.speex.SbEncoder.NB_QUALITY_MAP
                                                    [quality]];
                                                bodybytes += org.xiph.speex.SbEncoder.SB_FRAME_SIZE[org.xiph.speex.SbEncoder.WB_QUALITY_MAP
                                                    [quality]];
                                                break;
                                            }

                                        case 2:
                                            {
                                                bodybytes = org.xiph.speex.SbEncoder.NB_FRAME_SIZE[org.xiph.speex.SbEncoder.NB_QUALITY_MAP
                                                    [quality]];
                                                bodybytes += org.xiph.speex.SbEncoder.SB_FRAME_SIZE[org.xiph.speex.SbEncoder.WB_QUALITY_MAP
                                                    [quality]];
                                                bodybytes += org.xiph.speex.SbEncoder.SB_FRAME_SIZE[org.xiph.speex.SbEncoder.UWB_QUALITY_MAP
                                                    [quality]];
                                                break;
                                            }

                                        default:
                                            {
                                                //*/
                                                throw new System.IO.IOException("Illegal mode encoundered.");
                                                break;
                                            }
                                    }
                                    bodybytes = (bodybytes + 7) >> 3;
                                }
                                else
                                {
                                    // We have read the stream to find out more
                                    bodybytes = 0;
                                }
                            }
                            if (destFormat == FILE_FORMAT_WAVE)
                            {
                                writer = new org.xiph.speex.PcmWaveWriter(sampleRate, channels);
                                if (printlevel <= DEBUG)
                                {
                                    System.Console.WriteLine(string.Empty);
                                    System.Console.WriteLine("Output File: " + destPath);
                                    System.Console.WriteLine("File Format: PCM Wave");
                                    System.Console.WriteLine("Perceptual Enhancement: " + enhanced);
                                }
                            }
                            else
                            {
                                writer = new org.xiph.speex.RawWriter();
                                if (printlevel <= DEBUG)
                                {
                                    System.Console.WriteLine(string.Empty);
                                    System.Console.WriteLine("Output File: " + destPath);
                                    System.Console.WriteLine("File Format: Raw Audio");
                                    System.Console.WriteLine("Perceptual Enhancement: " + enhanced);
                                }
                            }
                            writer.open(destPath);
                            writer.writeHeader(null);
                            packetNo++;
                        }
                        else
                        {
                            dis.readFully(payload, 0, bodybytes);
                            if (loss > 0 && random.nextInt(100) < loss)
                            {
                                speexDecoder.processData(null, 0, bodybytes);
                                for (int i = 1; i < nframes; i++)
                                {
                                    speexDecoder.processData(true);
                                }
                            }
                            else
                            {
                                speexDecoder.processData(payload, 0, bodybytes);
                                for (int i = 1; i < nframes; i++)
                                {
                                    speexDecoder.processData(false);
                                }
                            }
                            if ((decsize = speexDecoder.getProcessedData(decdat, 0)) > 0)
                            {
                                writer.writePacket(decdat, 0, decsize);
                            }
                            packetNo++;
                        }
                    }
                }
            }
            catch (java.io.EOFException)
            {
            }
            writer.close();
        }

        /// <summary>Decodes a Speex file to PCM.</summary>
        /// <remarks>Decodes a Speex file to PCM.</remarks>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <exception>IOException</exception>
        /// <exception cref="System.IO.IOException"></exception>
        public virtual void decode(java.io.RandomInputStream srcPath, java.io.RandomOutputStream destPath)
        {
            byte[] header = new byte[2048];
            byte[] payload = new byte[65536];
            byte[] decdat = new byte[44100 * 2 * 2];
            int WAV_HEADERSIZE = 8;
            short WAVE_FORMAT_SPEEX = (short)unchecked((short)(0xa109));
            string RIFF = "RIFF";
            string WAVE = "WAVE";
            string FORMAT = "fmt ";
            string DATA = "data";
            int OGG_HEADERSIZE = 27;
            int OGG_SEGOFFSET = 26;
            string OGGID = "OggS";
            int segments = 0;
            int curseg = 0;
            int bodybytes = 0;
            int decsize = 0;
            int packetNo = 0;
            // Display info
            if (printlevel <= INFO)
            {
                version();
            }
            if (printlevel <= DEBUG)
            {
                System.Console.WriteLine(string.Empty);
            }
            if (printlevel <= DEBUG)
            {
                System.Console.WriteLine("Input File: " + srcPath);
            }
            // construct a new decoder
            speexDecoder = new org.xiph.speex.SpeexDecoder();
            // open the input stream
            java.io.DataInputStream dis = new java.io.DataInputStream((srcPath));
            org.xiph.speex.AudioFileWriter writer = null;
            int origchksum;
            int chksum;
            try
            {
                // read until we get to EOF
                while (true)
                {
                    if (srcFormat == FILE_FORMAT_OGG)
                    {
                        // read the OGG header
                        dis.readFully(header, 0, OGG_HEADERSIZE);
                        origchksum = readInt(header, 22);
                        header[22] = 0;
                        header[23] = 0;
                        header[24] = 0;
                        header[25] = 0;
                        chksum = org.xiph.speex.OggCrc.checksum(0, header, 0, OGG_HEADERSIZE);
                        // make sure its a OGG header
                        if (!OGGID.Equals(cspeex.StringUtil.getStringForBytes(header, 0, 4)))
                        {
                            System.Console.Error.WriteLine("missing ogg id!");
                            return;
                        }
                        segments = header[OGG_SEGOFFSET] & unchecked((int)(0xFF));
                        dis.readFully(header, OGG_HEADERSIZE, segments);
                        chksum = org.xiph.speex.OggCrc.checksum(chksum, header, OGG_HEADERSIZE, segments);
                        for (curseg = 0; curseg < segments; curseg++)
                        {
                            bodybytes = header[OGG_HEADERSIZE + curseg] & unchecked((int)(0xFF));
                            if (bodybytes == 255)
                            {
                                System.Console.Error.WriteLine("sorry, don't handle 255 sizes!");
                                return;
                            }
                            dis.readFully(payload, 0, bodybytes);
                            chksum = org.xiph.speex.OggCrc.checksum(chksum, payload, 0, bodybytes);
                            if (packetNo == 0)
                            {
                                if (readSpeexHeader(payload, 0, bodybytes))
                                {
                                    if (printlevel <= DEBUG)
                                    {
                                        System.Console.WriteLine("File Format: Ogg Speex");
                                        System.Console.WriteLine("Sample Rate: " + sampleRate);
                                        System.Console.WriteLine("Channels: " + channels);
                                        System.Console.WriteLine("Encoder mode: " + (mode == 0 ? "Narrowband" : (mode
                                             == 1 ? "Wideband" : "UltraWideband")));
                                        System.Console.WriteLine("Frames per packet: " + nframes);
                                    }
                                    if (destFormat == FILE_FORMAT_WAVE)
                                    {
                                        writer = new org.xiph.speex.PcmWaveWriter(speexDecoder.getSampleRate(), speexDecoder
                                            .getChannels());
                                        if (printlevel <= DEBUG)
                                        {
                                            System.Console.WriteLine(string.Empty);
                                            System.Console.WriteLine("Output File: " + destPath);
                                            System.Console.WriteLine("File Format: PCM Wave");
                                            System.Console.WriteLine("Perceptual Enhancement: " + enhanced);
                                        }
                                    }
                                    else
                                    {
                                        writer = new org.xiph.speex.RawWriter();
                                        if (printlevel <= DEBUG)
                                        {
                                            System.Console.WriteLine(string.Empty);
                                            System.Console.WriteLine("Output File: " + destPath);
                                            System.Console.WriteLine("File Format: Raw Audio");
                                            System.Console.WriteLine("Perceptual Enhancement: " + enhanced);
                                        }
                                    }
                                    writer.open(destPath);
                                    writer.writeHeader(null);
                                    packetNo++;
                                }
                                else
                                {
                                    packetNo = 0;
                                }
                            }
                            else
                            {
                                if (packetNo == 1)
                                {
                                    // Ogg Comment packet
                                    packetNo++;
                                }
                                else
                                {
                                    if (loss > 0 && random.nextInt(100) < loss)
                                    {
                                        speexDecoder.processData(null, 0, bodybytes);
                                        for (int i = 1; i < nframes; i++)
                                        {
                                            speexDecoder.processData(true);
                                        }
                                    }
                                    else
                                    {
                                        speexDecoder.processData(payload, 0, bodybytes);
                                        for (int i = 1; i < nframes; i++)
                                        {
                                            speexDecoder.processData(false);
                                        }
                                    }
                                    if ((decsize = speexDecoder.getProcessedData(decdat, 0)) > 0)
                                    {
                                        writer.writePacket(decdat, 0, decsize);
                                    }
                                    packetNo++;
                                }
                            }
                        }
                        if (chksum != origchksum)
                        {
                            throw new System.IO.IOException("Ogg CheckSums do not match");
                        }
                    }
                    else
                    {
                        // Wave or Raw Speex
                        if (packetNo == 0)
                        {
                            if (srcFormat == FILE_FORMAT_WAVE)
                            {
                                // read the WAVE header
                                dis.readFully(header, 0, WAV_HEADERSIZE + 4);
                                // make sure its a WAVE header
                                if (!RIFF.Equals(cspeex.StringUtil.getStringForBytes(header, 0, 4)) && !WAVE.Equals
                                    (cspeex.StringUtil.getStringForBytes(header, 8, 4)))
                                {
                                    System.Console.Error.WriteLine("Not a WAVE file");
                                    return;
                                }
                                // Read other header chunks
                                dis.readFully(header, 0, WAV_HEADERSIZE);
                                string chunk = cspeex.StringUtil.getStringForBytes(header, 0, 4);
                                int size = readInt(header, 4);
                                while (!chunk.Equals(DATA))
                                {
                                    dis.readFully(header, 0, size);
                                    if (chunk.Equals(FORMAT))
                                    {
                                        if (readShort(header, 0) != WAVE_FORMAT_SPEEX)
                                        {
                                            System.Console.Error.WriteLine("Not a Wave Speex file");
                                            return;
                                        }
                                        channels = readShort(header, 2);
                                        sampleRate = readInt(header, 4);
                                        bodybytes = readShort(header, 12);
                                        if (readShort(header, 16) < 82)
                                        {
                                            System.Console.Error.WriteLine("Possibly corrupt Speex Wave file.");
                                            return;
                                        }
                                        readSpeexHeader(header, 20, 80);
                                        // Display audio info
                                        if (printlevel <= DEBUG)
                                        {
                                            System.Console.WriteLine("File Format: Wave Speex");
                                            System.Console.WriteLine("Sample Rate: " + sampleRate);
                                            System.Console.WriteLine("Channels: " + channels);
                                            System.Console.WriteLine("Encoder mode: " + (mode == 0 ? "Narrowband" : (mode
                                                 == 1 ? "Wideband" : "UltraWideband")));
                                            System.Console.WriteLine("Frames per packet: " + nframes);
                                        }
                                    }
                                    dis.readFully(header, 0, WAV_HEADERSIZE);
                                    chunk = cspeex.StringUtil.getStringForBytes(header, 0, 4);
                                    size = readInt(header, 4);
                                }
                                if (printlevel <= DEBUG)
                                {
                                    System.Console.WriteLine("Data size: " + size);
                                }
                            }
                            else
                            {
                                if (printlevel <= DEBUG)
                                {
                                    System.Console.WriteLine("File Format: Raw Speex");
                                    System.Console.WriteLine("Sample Rate: " + sampleRate);
                                    System.Console.WriteLine("Channels: " + channels);
                                    System.Console.WriteLine("Encoder mode: " + (mode == 0 ? "Narrowband" : (mode
                                         == 1 ? "Wideband" : "UltraWideband")));
                                    System.Console.WriteLine("Frames per packet: " + nframes);
                                }
                                speexDecoder.init(mode, sampleRate, channels, enhanced);
                                if (!vbr)
                                {
                                    switch (mode)
                                    {
                                        case 0:
                                            {
                                                bodybytes = org.xiph.speex.NbEncoder.NB_FRAME_SIZE[org.xiph.speex.NbEncoder.NB_QUALITY_MAP
                                                    [quality]];
                                                break;
                                            }

                                        case 1:
                                            {
                                                //Wideband
                                                bodybytes = org.xiph.speex.SbEncoder.NB_FRAME_SIZE[org.xiph.speex.SbEncoder.NB_QUALITY_MAP
                                                    [quality]];
                                                bodybytes += org.xiph.speex.SbEncoder.SB_FRAME_SIZE[org.xiph.speex.SbEncoder.WB_QUALITY_MAP
                                                    [quality]];
                                                break;
                                            }

                                        case 2:
                                            {
                                                bodybytes = org.xiph.speex.SbEncoder.NB_FRAME_SIZE[org.xiph.speex.SbEncoder.NB_QUALITY_MAP
                                                    [quality]];
                                                bodybytes += org.xiph.speex.SbEncoder.SB_FRAME_SIZE[org.xiph.speex.SbEncoder.WB_QUALITY_MAP
                                                    [quality]];
                                                bodybytes += org.xiph.speex.SbEncoder.SB_FRAME_SIZE[org.xiph.speex.SbEncoder.UWB_QUALITY_MAP
                                                    [quality]];
                                                break;
                                            }

                                        default:
                                            {
                                                //*/
                                                throw new System.IO.IOException("Illegal mode encoundered.");
                                                break;
                                            }
                                    }
                                    bodybytes = (bodybytes + 7) >> 3;
                                }
                                else
                                {
                                    // We have read the stream to find out more
                                    bodybytes = 0;
                                }
                            }
                            if (destFormat == FILE_FORMAT_WAVE)
                            {
                                writer = new org.xiph.speex.PcmWaveWriter(sampleRate, channels);
                                if (printlevel <= DEBUG)
                                {
                                    System.Console.WriteLine(string.Empty);
                                    System.Console.WriteLine("Output File: " + destPath);
                                    System.Console.WriteLine("File Format: PCM Wave");
                                    System.Console.WriteLine("Perceptual Enhancement: " + enhanced);
                                }
                            }
                            else
                            {
                                writer = new org.xiph.speex.RawWriter();
                                if (printlevel <= DEBUG)
                                {
                                    System.Console.WriteLine(string.Empty);
                                    System.Console.WriteLine("Output File: " + destPath);
                                    System.Console.WriteLine("File Format: Raw Audio");
                                    System.Console.WriteLine("Perceptual Enhancement: " + enhanced);
                                }
                            }
                            writer.open(destPath);
                            writer.writeHeader(null);
                            packetNo++;
                        }
                        else
                        {
                            dis.readFully(payload, 0, bodybytes);
                            if (loss > 0 && random.nextInt(100) < loss)
                            {
                                speexDecoder.processData(null, 0, bodybytes);
                                for (int i = 1; i < nframes; i++)
                                {
                                    speexDecoder.processData(true);
                                }
                            }
                            else
                            {
                                speexDecoder.processData(payload, 0, bodybytes);
                                for (int i = 1; i < nframes; i++)
                                {
                                    speexDecoder.processData(false);
                                }
                            }
                            if ((decsize = speexDecoder.getProcessedData(decdat, 0)) > 0)
                            {
                                writer.writePacket(decdat, 0, decsize);
                            }
                            packetNo++;
                        }
                    }
                }
            }
            catch (java.io.EOFException)
            {
            }
            writer.close();
        }

        /// <summary>Reads the header packet.</summary>
        /// <remarks>
        /// Reads the header packet.
        /// <pre>
        /// 0 -  7: speex_string: "Speex   "
        /// 8 - 27: speex_version: "speex-1.0"
        /// 28 - 31: speex_version_id: 1
        /// 32 - 35: header_size: 80
        /// 36 - 39: rate
        /// 40 - 43: mode: 0=narrowband, 1=wb, 2=uwb
        /// 44 - 47: mode_bitstream_version: 4
        /// 48 - 51: nb_channels
        /// 52 - 55: bitrate: -1
        /// 56 - 59: frame_size: 160
        /// 60 - 63: vbr
        /// 64 - 67: frames_per_packet
        /// 68 - 71: extra_headers: 0
        /// 72 - 75: reserved1
        /// 76 - 79: reserved2
        /// </pre>
        /// </remarks>
        /// <param name="packet"></param>
        /// <param name="offset"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private bool readSpeexHeader(byte[] packet, int offset, int bytes)
        {
            if (bytes != 80)
            {
                System.Console.WriteLine("Oooops");
                return false;
            }
            if (!"Speex   ".Equals(cspeex.StringUtil.getStringForBytes(packet, offset, 8)))
            {
                return false;
            }
            mode = packet[40 + offset] & unchecked((int)(0xFF));
            sampleRate = readInt(packet, offset + 36);
            channels = readInt(packet, offset + 48);
            nframes = readInt(packet, offset + 64);
            return speexDecoder.init(mode, sampleRate, channels, enhanced);
        }

        /// <summary>Converts Little Endian (Windows) bytes to an int (Java uses Big Endian).
        /// 	</summary>
        /// <remarks>Converts Little Endian (Windows) bytes to an int (Java uses Big Endian).
        /// 	</remarks>
        /// <param name="data">the data to read.</param>
        /// <param name="offset">the offset from which to start reading.</param>
        /// <returns>the integer value of the reassembled bytes.</returns>
        protected static int readInt(byte[] data, int offset)
        {
            return (data[offset] & unchecked((int)(0xff))) | ((data[offset + 1] & unchecked((
                int)(0xff))) << 8) | ((data[offset + 2] & unchecked((int)(0xff))) << 16) | (data
                [offset + 3] << 24);
        }

        // no 0xff on the last one to keep the sign
        /// <summary>Converts Little Endian (Windows) bytes to an short (Java uses Big Endian).
        /// 	</summary>
        /// <remarks>Converts Little Endian (Windows) bytes to an short (Java uses Big Endian).
        /// 	</remarks>
        /// <param name="data">the data to read.</param>
        /// <param name="offset">the offset from which to start reading.</param>
        /// <returns>the integer value of the reassembled bytes.</returns>
        protected static int readShort(byte[] data, int offset)
        {
            return (data[offset] & unchecked((int)(0xff))) | (data[offset + 1] << 8);
        }
        // no 0xff on the last one to keep the sign
    }
}