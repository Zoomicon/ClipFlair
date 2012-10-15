//Filename: MemoryAudioSink.cs
//Version: 20120912

using System;
using System.Windows.Media;
using System.IO;

namespace ClipFlair.AudioLib
{

  public class MemoryAudioSink : AudioSink
  {
    // Memory sink to record into.
    private MemoryStream _stream;
    // Current format the sinks records audio in.
    private AudioFormat _format;

    public Stream BackingStream
    {
      get { return _stream; }
    }

    public AudioFormat CurrentFormat
    {
      get { return _format; }
    }

    protected override void OnCaptureStarted()
    {
      _stream = new MemoryStream(1024);
    }

    protected override void OnCaptureStopped()
    {
    }

    protected override void OnFormatChange(AudioFormat audioFormat)
    {
      if (audioFormat.WaveFormat != WaveFormatType.Pcm)
        throw new InvalidOperationException("MemoryAudioSink supports only PCM audio format.");

      _format = audioFormat;
    }

    protected override void OnSamples(long sampleTime, long sampleDuration, byte[] sampleData)
    {
      // New audio data arrived, write them to the stream.
      _stream.Write(sampleData, 0, sampleData.Length);
    }
  }

}