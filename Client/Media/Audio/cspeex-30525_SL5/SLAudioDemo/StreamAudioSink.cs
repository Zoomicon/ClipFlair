//Filename: StreamAudioSink.cs
//Version: 20120609
//Editor: George Birbilis <birbilis@kagi.com>

//Modified cspeex-30525 source from http://cspeex.codeplex.com to work with Silverlight 4 RC and higher (was for Silverlight 4 Beta)

using System;
using System.IO;
using System.Windows.Media;
using org.xiph.speex;
using java.io;

namespace SLAudioDemo
{
    public class StreamAudioSink : AudioSink
    {
        private SpeexEncoder speexEncoder;
        private int pcmPacketSize;
        private byte[] temp;
        private int tempOffset;
        private RandomOutputStream memFile;
        private AudioFileWriter writer;

        public StreamAudioSink()
        {
        }

        public RandomOutputStream MemFile { get { return memFile; } }

        protected override void OnCaptureStarted()
        {
            speexEncoder = new org.xiph.speex.SpeexEncoder();
        }

        protected override void OnCaptureStopped()
        {
        }

        protected override void OnFormatChange(AudioFormat audioFormat)
        {
            if (audioFormat.WaveFormat == WaveFormatType.Pcm) //Birbilis: "PCM" has been renamed to "Pcm" in Silverlight 4 RC and higher (was using Silverlight 4 Beta here)
            {
                speexEncoder.init(2, 8, audioFormat.SamplesPerSecond, audioFormat.Channels);
                pcmPacketSize = 2 * speexEncoder.getChannels() * speexEncoder.getFrameSize();
                temp = new byte[pcmPacketSize];
                tempOffset = 0;

                if (writer != null)
                    writer.close();

                writer = new org.xiph.speex.OggSpeexWriter(2, audioFormat.SamplesPerSecond, audioFormat.Channels, 1, false);

                memFile = new RandomOutputStream(new MemoryStream(2 * 1024 * pcmPacketSize));

                writer.open(memFile);
                writer.writeHeader("Encoded with Speex");
            }
            else
            {
                throw new Exception("Codec not supported");
            }
        }

        protected override void OnSamples(long sampleTime, long sampleDuration, byte[] sampleData)
        {
            for (int i = 0; i < sampleData.Length; )
            {
                int len = Math.Min(sampleData.Length - i, temp.Length - tempOffset);

                Buffer.BlockCopy(sampleData, i, temp, tempOffset, len);

                if (len < temp.Length - tempOffset)
                {
                    tempOffset += len;
                }
                else
                {
                    tempOffset = 0;

                    speexEncoder.processData(temp, 0, temp.Length);

                    int encsize = speexEncoder.getProcessedData(temp, 0);

                    if (encsize > 0 && (memFile.InnerStream.Position + encsize < ((MemoryStream)memFile.InnerStream).Capacity))
                    {
                        writer.writePacket(temp, 0, encsize);
                    }
                }

                i += len;
            }
        }
    }
}
