//Filename: WavManager.cs
//Version: 20120912

using System;
using System.Windows.Media;
using System.IO;

namespace Zoomicon.AudioLib
{

    public class WavManager
    {
        public static byte[] SavePcmToWav(Stream rawData, Stream output, AudioFormat audioFormat)
        {
            if (audioFormat == null)
            {
              throw new ArgumentException("No audio available."); //this may occur if there is no audio hardware
            }

            if (audioFormat.WaveFormat != WaveFormatType.Pcm)
                throw new ArgumentException("Only PCM coding is supported.");

            BinaryWriter bwOutput = new BinaryWriter(output);

            // Write down the WAV header.
            // Refer to http://technology.niagarac.on.ca/courses/ctec1631/WavFileFormat.html
            // for details on the format.

            // Note that we use ToCharArray() when writing fixed strings
            // to force using the char[] overload because
            // Write(string) writes the string prefixed by its length.

            // -- RIFF chunk

            bwOutput.Write("RIFF".ToCharArray());

            // Total Length Of Package To Follow
            // Computed as data length plus the header length without the data
            // we have written so far and this data (44 - 4 ("RIFF") - 4 (this data))
            bwOutput.Write((uint)(rawData.Length + 36));

            bwOutput.Write("WAVE".ToCharArray());

            // -- FORMAT chunk

            bwOutput.Write("fmt ".ToCharArray());

            // Length Of FORMAT Chunk (Binary, always 0x10)
            bwOutput.Write((uint)0x10);

            // Always 0x01
            bwOutput.Write((ushort)0x01);

            // Channel Numbers (Always 0x01=Mono, 0x02=Stereo)
            bwOutput.Write((ushort)audioFormat.Channels);

            // Sample Rate (Binary, in Hz)
            bwOutput.Write((uint)audioFormat.SamplesPerSecond);

            // Bytes Per Second
            bwOutput.Write((uint)(audioFormat.BitsPerSample * audioFormat.SamplesPerSecond * audioFormat.Channels / 8));

            // Bytes Per Sample: 1=8 bit Mono, 2=8 bit Stereo or 16 bit Mono, 4=16 bit Stereo
            bwOutput.Write((ushort)(audioFormat.BitsPerSample * audioFormat.Channels / 8));

            // Bits Per Sample
            bwOutput.Write((ushort)audioFormat.BitsPerSample);

            // -- DATA chunk

            bwOutput.Write("data".ToCharArray());

            // Length Of Data To Follow
            bwOutput.Write((uint)rawData.Length);

            // Raw PCM data follows...

            // Reset position in rawData and remember its origin position
            // to restore at the end.
            long originalRawDataStreamPosition = rawData.Position;
            rawData.Seek(0, SeekOrigin.Begin);

            // Append all data from rawData stream into output stream.
            byte[] buffer = new byte[4096];
            int read;       // number of bytes read in one iteration
            while ((read = rawData.Read(buffer, 0, 4096)) > 0)
            {
                bwOutput.Write(buffer, 0, read);
            }

            rawData.Seek(originalRawDataStreamPosition, SeekOrigin.Begin);
            return buffer;
        }
    }

}