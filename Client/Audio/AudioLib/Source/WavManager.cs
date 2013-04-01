//Filename: WavManager.cs
//Version: 20130401
//Editor: George Birbilis (http://zoomicon.com)

using System;
using System.Windows.Media;
using System.IO;

namespace AudioLib
{

  public class WavManager
  {

    public static void WriteWavHeader(Stream output, AudioFormatEx audioFormat, uint rawDataLength)
    {
      if (audioFormat == null)
        throw new ArgumentException("No audio available."); //this may occur if there is no audio hardware

      if (audioFormat.WaveFormat != WaveFormatType.Pcm)
        throw new ArgumentException("Only PCM coding is supported.");

      BinaryWriter bwOutput = new BinaryWriter(output);

      // Write down the WAV header.
      // Refer to http://technology.niagarac.on.ca/courses/ctec1631/WavFileFormat.html (broken link: see https://ccrma.stanford.edu/courses/422/projects/WaveFormat/ instead)
      // for details on the format.

      // Note that we use ToCharArray() when writing fixed strings
      // to force using the char[] overload because
      // Write(string) writes the string prefixed by its length.

      // -- RIFF chunk

      bwOutput.Write("RIFF".ToCharArray());

      // Total Length Of Package To Follow
      // Computed as data length plus the header length without the data
      // we have written so far and this data (44 - 4 ("RIFF") - 4 (this data))
      bwOutput.Write(rawDataLength + 36);

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

      // Calculate Bytes/Sample and Bytes/Second
      ushort bytesPerSample = (ushort)(audioFormat.BitsPerSample * audioFormat.Channels / 8); // 1=8 bit Mono, 2=8 bit Stereo or 16 bit Mono, 4=16 bit Stereo
      uint bytesPerSecond = (uint)(bytesPerSample * audioFormat.SamplesPerSecond);

      // Bytes Per Second
      bwOutput.Write(bytesPerSecond);

      // Bytes Per Sample
      bwOutput.Write(bytesPerSample);

      // Bits Per Sample
      bwOutput.Write((ushort)audioFormat.BitsPerSample);

      // -- DATA chunk

      bwOutput.Write("data".ToCharArray());

      // Length Of Data To Follow
      bwOutput.Write(rawDataLength);
    }

    public static void WriteRawData(Stream rawData, Stream output, AudioFormatEx audioFormat)
    {
      BinaryWriter bwOutput = new BinaryWriter(output);

      // Reset position in rawData and remember its origin position
      long originalRawDataStreamPosition = rawData.Position;
      rawData.Seek(0, SeekOrigin.Begin);

      // Append all data from rawData stream into output stream.
      byte[] buffer = new byte[4096];
      int read;       // number of bytes read in one iteration
      while ((read = rawData.Read(buffer, 0, 4096)) > 0)
        bwOutput.Write(buffer, 0, read);

      // Restore origin position
      rawData.Seek(originalRawDataStreamPosition, SeekOrigin.Begin);
    }

    public static void WriteSilence(Stream outputStream, AudioFormatEx audioFormat, double seconds)
    {
      // Calculate Bytes/Sample and Bytes/Second
      ushort bytesPerSample = (ushort)(audioFormat.BitsPerSample * audioFormat.Channels / 8); // 1=8 bit Mono, 2=8 bit Stereo or 16 bit Mono, 4=16 bit Stereo
      uint bytesPerSecond = (uint)(bytesPerSample * audioFormat.SamplesPerSecond);
      uint bytes = (uint)Math.Round(bytesPerSecond * seconds);

      BinaryWriter b = new BinaryWriter(outputStream);
      for (uint i = 0; i < bytes; i++)
        b.Write((byte)0);
    }

    public static void SavePcmToWav(Stream rawData, Stream output, AudioFormatEx audioFormat) //TODO: add optional maxSeconds parameter
    {
      WriteWavHeader(output, audioFormat, (uint)rawData.Length);
      WriteRawData(rawData, output, audioFormat); //raw PCM data
    }

    public static void SavePcmToWav(Stream[] rawData, Stream output, AudioFormatEx audioFormat)
    {
      // Get total raw data stream length
      uint len = 0;
      foreach (Stream s in rawData)
        len += (uint)s.Length;

      // Write WAV header
      WriteWavHeader(output, audioFormat, len);

      // Write raw PCM data streams in sequence
      foreach (Stream s in rawData)
        WriteRawData(s, output, audioFormat);
    }

    public static void ConcatenateWavs(Stream[] wavs, Stream output)
    {
      WavParser[] parsedWavs = new WavParser[wavs.Length];

      // Get total raw data stream length
      uint len = 0;
      for (int i=0; i<wavs.Length; i++)
      {
        WavParser wavParser = new WavParser(wavs[i]);
        parsedWavs[i] = wavParser;
        len += wavParser.RawWaveSize;
      }

      // Write WAV header
      WriteWavHeader(output, parsedWavs[0].AudioFormat, len);

      // Write raw PCM data streams in sequence
      for (int i=0; i<wavs.Length; i++)
        WriteRawData(wavs[i], output, parsedWavs[i].AudioFormat); //TODO: need to specify start offset and length (get offset from parsedWav [make sure we save the chunk data pos] - get length as max of the parsedWav.rawWaveSize)
    } //TODO: need to copy and adapt this code in the method that makes wav from CaptionRegion - there it will need to add silence to fit times and also crop wavs depending on duration)

  }

}