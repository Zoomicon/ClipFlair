//Filename: WavManager.cs
//Version: 20130403
//Editor: George Birbilis (http://zoomicon.com)

using System;
using System.Windows.Media;
using System.IO;

namespace AudioLib
{

  public class WavManager
  {

    public const int WAV_HEADER_SIZE = 44;

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
      // we have written so far and this data (44 - 4 ("RIFF") - 4 (this number))
      bwOutput.Write(rawDataLength + WAV_HEADER_SIZE - 4 - 4);

      bwOutput.Write("WAVE".ToCharArray()); //RIFF data type

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

    public static void WriteRawData(Stream rawData, Stream output, uint offset = 0, long maxCount = -1)
    {
      BinaryWriter bwOutput = new BinaryWriter(output);

      // Reset position in rawData and remember its origin position
      long originalRawDataStreamPosition = rawData.Position;
      rawData.Seek(offset, SeekOrigin.Begin); //0-indexed, skipping 44 bytes (the WAV header)

     // Append data from rawData stream into output stream.
      byte[] buffer = new byte[4096];
      int read;       // number of bytes read in one iteration
      while ( (maxCount != 0) && (read = rawData.Read(buffer, 0, (maxCount < 0 || maxCount >= 4096)? 4096 : (int)maxCount)) > 0 )
      {
        bwOutput.Write(buffer, 0, read);
         if (maxCount >= 0) maxCount-= read; //when maxCount was initially >=0, then after final read this will do maxCount-maxCount=0 and loop will stop when it checks for maxCount!=0
       }

      // Restore origin position
      rawData.Seek(originalRawDataStreamPosition, SeekOrigin.Begin);
    }

    public static void WriteSilence(Stream outputStream, WAVEFORMATEX audioFormat, double msec)
    {
      if (msec <= 0) return;

      long bytes = audioFormat.BufferSizeFromAudioDuration((long)(msec * 10000)); //expects duration in 100-nanosecond units (hns)
      
      BinaryWriter b = new BinaryWriter(outputStream);
      for (uint i = 0; i < bytes; i++)
        b.Write((byte)0);
    }

    public static void SavePcmToWav(Stream rawData, Stream output, AudioFormatEx audioFormat, uint offset = 0, long maxLength = -1) //TODO: add optional maxSeconds parameter
    {
      WriteWavHeader(output, audioFormat, (uint)rawData.Length);
      WriteRawData(rawData, output, offset, maxLength); //raw PCM data
    }

    public static void SavePcmToWav(Stream[] rawData, Stream output, AudioFormatEx audioFormat, uint offset = 0, long maxLength = -1) //Saves WAV concatenating multiple raw data streams (each optionally starting at offset and cropped at maxLength)
    {
      // Get total raw data stream length
      uint len = 0;
      foreach (Stream s in rawData)
      {
        uint lengthAfterOffset = (uint)s.Length - offset;
        len += (lengthAfterOffset <= maxLength) ? lengthAfterOffset : (uint)maxLength;
      }

      // Write WAV header
      WriteWavHeader(output, audioFormat, len);

      // Write raw PCM data streams in sequence
      foreach (Stream s in rawData)
        WriteRawData(s, output, offset, maxLength);
    }

    public static void ConcatenateWavs(Stream[] wavs, Stream output)
    {
      WavParser[] parsedWavs = new WavParser[wavs.Length];

      // Get total raw data stream length
      uint len = 0;
      for (int i = 0; i < wavs.Length; i++)
      {
        WavParser wavParser = new WavParser(wavs[i]);
        parsedWavs[i] = wavParser;
        len += wavParser.RawWaveSize;
      }

      // Write WAV header
      WriteWavHeader(output, parsedWavs[0].AudioFormat, len);

      // Write raw PCM data streams in sequence
      for (int i=0; i<wavs.Length; i++)
        WriteRawData(wavs[i], output, WAV_HEADER_SIZE, parsedWavs[i].RawWaveSize); //trim at the end to ignore any extra data chunks (like the ones in BWF [Broadcast Wave Format])
    }

  }

}