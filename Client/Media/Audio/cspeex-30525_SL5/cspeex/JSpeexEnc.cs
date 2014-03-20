namespace cspeex
{
    /// <summary>Java Speex Command Line Encoder.</summary>
    /// <remarks>
    /// Java Speex Command Line Encoder.
    /// Currently this code has been updated to be compatible with release 1.0.3.
    /// </remarks>
    /// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
    /// <version>$Revision: 155 $</version>
    public class JSpeexEnc
    {
        /// <summary>Version of the Speex Encoder</summary>
        public static readonly string VERSION = "Java Speex Command Line Encoder v0.9.7 ($Revision: 155 $)";

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

        /// <summary>Defines the encoder mode (0=NB, 1=WB and 2=UWB).</summary>
        /// <remarks>Defines the encoder mode (0=NB, 1=WB and 2=UWB).</remarks>
        protected int mode = -1;

        /// <summary>Defines the encoder quality setting (integer from 0 to 10).</summary>
        /// <remarks>Defines the encoder quality setting (integer from 0 to 10).</remarks>
        protected int quality = 8;

        /// <summary>Defines the encoders algorithmic complexity.</summary>
        /// <remarks>Defines the encoders algorithmic complexity.</remarks>
        protected int complexity = 3;

        /// <summary>Defines the number of frames per speex packet.</summary>
        /// <remarks>Defines the number of frames per speex packet.</remarks>
        protected int nframes = 1;

        /// <summary>Defines the desired bitrate for the encoded audio.</summary>
        /// <remarks>Defines the desired bitrate for the encoded audio.</remarks>
        protected int bitrate = -1;

        /// <summary>Defines the sampling rate of the audio input.</summary>
        /// <remarks>Defines the sampling rate of the audio input.</remarks>
        protected int sampleRate = -1;

        /// <summary>Defines the number of channels of the audio input (1=mono, 2=stereo).</summary>
        /// <remarks>Defines the number of channels of the audio input (1=mono, 2=stereo).</remarks>
        protected int channels = 1;

        /// <summary>Defines the encoder VBR quality setting (float from 0 to 10).</summary>
        /// <remarks>Defines the encoder VBR quality setting (float from 0 to 10).</remarks>
        protected float vbr_quality = -1;

        /// <summary>Defines whether or not to use VBR (Variable Bit Rate).</summary>
        /// <remarks>Defines whether or not to use VBR (Variable Bit Rate).</remarks>
        protected bool vbr = false;

        /// <summary>Defines whether or not to use VAD (Voice Activity Detection).</summary>
        /// <remarks>Defines whether or not to use VAD (Voice Activity Detection).</remarks>
        protected bool vad = false;

        /// <summary>Defines whether or not to use DTX (Discontinuous Transmission).</summary>
        /// <remarks>Defines whether or not to use DTX (Discontinuous Transmission).</remarks>
        protected bool dtx = false;

        /// <summary>The audio input file</summary>
        protected string srcFile;

        /// <summary>The audio output file</summary>
        protected string destFile;

        /// <summary>Builds a plain JSpeex Encoder with default values.</summary>
        /// <remarks>Builds a plain JSpeex Encoder with default values.</remarks>
        public JSpeexEnc()
        {
        }

        /// <summary>
        /// Command line entrance:
        /// <pre>
        /// Usage: JSpeexEnc [options] input_file output_file
        /// </pre>
        /// </summary>
        /// <param name="args">Command line parameters.</param>
        /// <exception>IOException</exception>
        /// <exception cref="System.IO.IOException"></exception>
        public static void DoMain(string[] args)
        {
            JSpeexEnc encoder = new JSpeexEnc();
            if (encoder.parseArgs(args))
            {
                encoder.encode();
            }
        }

        /// <summary>Parse the command line arguments.</summary>
        /// <remarks>Parse the command line arguments.</remarks>
        /// <param name="args">Command line parameters.</param>
        /// <returns>true if the parsed arguments are sufficient to run the encoder.</returns>
        public virtual bool parseArgs(string[] args)
        {
            // make sure we have command args
            if (args.Length < 2)
            {
                if (args.Length == 1 && (cspeex.StringUtil.equalsIgnoreCase(args[0], "-v") || cspeex.StringUtil.equalsIgnoreCase
                    (args[0], "--version")))
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
            if (srcFile.ToLower().EndsWith(".wav"))
            {
                srcFormat = FILE_FORMAT_WAVE;
            }
            else
            {
                srcFormat = FILE_FORMAT_RAW;
            }
            if (destFile.ToLower().EndsWith(".spx"))
            {
                destFormat = FILE_FORMAT_OGG;
            }
            else
            {
                if (destFile.ToLower().EndsWith(".wav"))
                {
                    destFormat = FILE_FORMAT_WAVE;
                }
                else
                {
                    destFormat = FILE_FORMAT_RAW;
                }
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
                                                if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--complexity"))
                                                {
                                                    try
                                                    {
                                                        complexity = int.Parse(args[++i]);
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
                                                            if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--vad"))
                                                            {
                                                                vad = true;
                                                            }
                                                            else
                                                            {
                                                                if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--dtx"))
                                                                {
                                                                    dtx = true;
                                                                }
                                                                else
                                                                {
                                                                    if (cspeex.StringUtil.equalsIgnoreCase(args[i], "--rate"))
                                                                    {
                                                                        try
                                                                        {
                                                                            sampleRate = int.Parse(args[++i]);
                                                                        }
                                                                        catch (System.FormatException)
                                                                        {
                                                                            usage();
                                                                            return false;
                                                                        }
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
            }
            return true;
        }

        /// <summary>Prints the usage guidelines.</summary>
        /// <remarks>Prints the usage guidelines.</remarks>
        public static void usage()
        {
            version();
            System.Console.WriteLine(string.Empty);
            System.Console.WriteLine("Usage: JSpeexEnc [options] input_file output_file");
            System.Console.WriteLine("Where:");
            System.Console.WriteLine("  input_file can be:");
            System.Console.WriteLine("    filename.wav  a PCM wav file");
            System.Console.WriteLine("    filename.*    a raw PCM file (any extension other than .wav)"
                );
            System.Console.WriteLine("  output_file can be:");
            System.Console.WriteLine("    filename.spx  an Ogg Speex file");
            System.Console.WriteLine("    filename.wav  a Wave Speex file (beta!!!)");
            System.Console.WriteLine("    filename.*    a raw Speex file");
            System.Console.WriteLine("Options: -h, --help     This help");
            System.Console.WriteLine("         -v, --version  Version information");
            System.Console.WriteLine("         --verbose      Print detailed information"
                );
            System.Console.WriteLine("         --quiet        Print minimal information");
            System.Console.WriteLine("         -n, -nb        Consider input as Narrowband (8kHz)"
                );
            System.Console.WriteLine("         -w, -wb        Consider input as Wideband (16kHz)"
                );
            System.Console.WriteLine("         -u, -uwb       Consider input as Ultra-Wideband (32kHz)"
                );
            System.Console.WriteLine("         --quality n    Encoding quality (0-10) default 8"
                );
            System.Console.WriteLine("         --complexity n Encoding complexity (0-10) default 3"
                );
            System.Console.WriteLine("         --nframes n    Number of frames per Ogg packet, default 1"
                );
            System.Console.WriteLine("         --vbr          Enable varible bit-rate (VBR)"
                );
            System.Console.WriteLine("         --vad          Enable voice activity detection (VAD)"
                );
            System.Console.WriteLine("         --dtx          Enable file based discontinuous transmission (DTX)"
                );
            System.Console.WriteLine("         if the input file is raw PCM (not a Wave file)"
                );
            System.Console.WriteLine("         --rate n       Sampling rate for raw input"
                );
            System.Console.WriteLine("         --stereo       Consider input as stereo");
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
            System.Console.WriteLine("using " + org.xiph.speex.SpeexEncoder.VERSION);
            System.Console.WriteLine(COPYRIGHT);
        }

        /// <summary>Encodes a PCM file to Speex.</summary>
        /// <remarks>Encodes a PCM file to Speex.</remarks>
        /// <exception>IOException</exception>
        /// <exception cref="System.IO.IOException"></exception>
        public virtual void encode()
        {
            encode(new java.io.File(srcFile), new java.io.File(destFile));
        }

        /// <summary>Encodes a PCM file to Speex.</summary>
        /// <remarks>Encodes a PCM file to Speex.</remarks>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <exception>IOException</exception>
        /// <exception cref="System.IO.IOException"></exception>
        public virtual void encode(java.io.File srcPath, java.io.File destPath)
        {
            byte[] temp = new byte[2560];
            // stereo UWB requires one to read 2560b
            int HEADERSIZE = 8;
            string RIFF = "RIFF";
            string WAVE = "WAVE";
            string FORMAT = "fmt ";
            string DATA = "data";
            int WAVE_FORMAT_PCM = unchecked((int)(0x0001));
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
            // Open the input stream
            java.io.DataInputStream dis = new java.io.DataInputStream(new java.io.FileInputStream
                (srcPath));
            // Prepare input stream
            if (srcFormat == FILE_FORMAT_WAVE)
            {
                // read the WAVE header
                dis.readFully(temp, 0, HEADERSIZE + 4);
                // make sure its a WAVE header
                if (!RIFF.Equals(cspeex.StringUtil.getStringForBytes(temp, 0, 4)) && !WAVE.Equals(cspeex.StringUtil.getStringForBytes
                    (temp, 8, 4)))
                {
                    System.Console.Error.WriteLine("Not a WAVE file");
                    return;
                }
                // Read other header chunks
                dis.readFully(temp, 0, HEADERSIZE);
                string chunk = cspeex.StringUtil.getStringForBytes(temp, 0, 4);
                int size = readInt(temp, 4);
                while (!chunk.Equals(DATA))
                {
                    dis.readFully(temp, 0, size);
                    if (chunk.Equals(FORMAT))
                    {
                        if (readShort(temp, 0) != WAVE_FORMAT_PCM)
                        {
                            System.Console.Error.WriteLine("Not a PCM file");
                            return;
                        }
                        channels = readShort(temp, 2);
                        sampleRate = readInt(temp, 4);
                        if (readShort(temp, 14) != 16)
                        {
                            System.Console.Error.WriteLine("Not a 16 bit file " + readShort(temp, 18));
                            return;
                        }
                        // Display audio info
                        if (printlevel <= DEBUG)
                        {
                            System.Console.WriteLine("File Format: PCM wave");
                            System.Console.WriteLine("Sample Rate: " + sampleRate);
                            System.Console.WriteLine("Channels: " + channels);
                        }
                    }
                    dis.readFully(temp, 0, HEADERSIZE);
                    chunk = cspeex.StringUtil.getStringForBytes(temp, 0, 4);
                    size = readInt(temp, 4);
                }
                if (printlevel <= DEBUG)
                {
                    System.Console.WriteLine("Data size: " + size);
                }
            }
            else
            {
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
                                break;
                            }
                    }
                }
                // Display audio info
                if (printlevel <= DEBUG)
                {
                    System.Console.WriteLine("File format: Raw audio");
                    System.Console.WriteLine("Sample rate: " + sampleRate);
                    System.Console.WriteLine("Channels: " + channels);
                    System.Console.WriteLine("Data size: " + srcPath.length());
                }
            }
            // Set the mode if it has not yet been determined
            if (mode < 0)
            {
                if (sampleRate < 100)
                {
                    // Sample Rate has probably been given in kHz
                    sampleRate *= 1000;
                }
                if (sampleRate < 12000)
                {
                    mode = 0;
                }
                else
                {
                    // Narrowband
                    if (sampleRate < 24000)
                    {
                        mode = 1;
                    }
                    else
                    {
                        // Wideband
                        mode = 2;
                    }
                }
            }
            // Ultra-wideband
            // Construct a new encoder
            org.xiph.speex.SpeexEncoder speexEncoder = new org.xiph.speex.SpeexEncoder();
            speexEncoder.init(mode, quality, sampleRate, channels);
            if (complexity > 0)
            {
                speexEncoder.getEncoder().setComplexity(complexity);
            }
            if (bitrate > 0)
            {
                speexEncoder.getEncoder().setBitRate(bitrate);
            }
            if (vbr)
            {
                speexEncoder.getEncoder().setVbr(vbr);
                if (vbr_quality > 0)
                {
                    speexEncoder.getEncoder().setVbrQuality(vbr_quality);
                }
            }
            if (vad)
            {
                speexEncoder.getEncoder().setVad(vad);
            }
            if (dtx)
            {
                speexEncoder.getEncoder().setDtx(dtx);
            }
            // Display info
            if (printlevel <= DEBUG)
            {
                System.Console.WriteLine(string.Empty);
                System.Console.WriteLine("Output File: " + destPath);
                System.Console.WriteLine("File format: Ogg Speex");
                System.Console.WriteLine("Encoder mode: " + (mode == 0 ? "Narrowband" : (mode
                     == 1 ? "Wideband" : "UltraWideband")));
                System.Console.WriteLine("Quality: " + (vbr ? vbr_quality : quality));
                System.Console.WriteLine("Complexity: " + complexity);
                System.Console.WriteLine("Frames per packet: " + nframes);
                System.Console.WriteLine("Varible bitrate: " + vbr);
                System.Console.WriteLine("Voice activity detection: " + vad);
                System.Console.WriteLine("Discontinouous Transmission: " + dtx);
            }
            // Open the file writer
            org.xiph.speex.AudioFileWriter writer;
            if (destFormat == FILE_FORMAT_OGG)
            {
                writer = new org.xiph.speex.OggSpeexWriter(mode, sampleRate, channels, nframes, vbr
                    );
            }
            else
            {
                if (destFormat == FILE_FORMAT_WAVE)
                {
                    nframes = org.xiph.speex.PcmWaveWriter.WAVE_FRAME_SIZES[mode - 1][channels - 1][quality
                        ];
                    writer = new org.xiph.speex.PcmWaveWriter(mode, quality, sampleRate, channels, nframes
                        , vbr);
                }
                else
                {
                    writer = new org.xiph.speex.RawWriter();
                }
            }
            writer.open(destPath);
            writer.writeHeader("Encoded with: " + VERSION);
            int pcmPacketSize = 2 * channels * speexEncoder.getFrameSize();
            try
            {
                // read until we get to EOF
                while (true)
                {
                    dis.readFully(temp, 0, nframes * pcmPacketSize);
                    for (int i = 0; i < nframes; i++)
                    {
                        speexEncoder.processData(temp, i * pcmPacketSize, pcmPacketSize);
                    }
                    int encsize = speexEncoder.getProcessedData(temp, 0);
                    if (encsize > 0)
                    {
                        writer.writePacket(temp, 0, encsize);
                    }
                }
            }
            catch (java.io.EOFException)
            {
            }
            writer.close();
            dis.close();
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