//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt_Audio.cs
//Version: 20140706

using AudioLib;

using ClipFlair.AudioRecorder;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.IO;

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Audio extensions class for CaptionElementExt
  /// </summary>
  public static class CaptionElementExt_Audio
  {

    public static bool HasAudio(this CaptionElement caption)
    {
      CaptionElementExt c = caption as CaptionElementExt;
      return (c != null) ? (c.Audio != null) : false;
    }

    public static Stream GetAudio(this CaptionElement caption)
    {
      CaptionElementExt c = caption as CaptionElementExt;
      return (c != null) ? c.Audio : null;
    }

    public static CaptionElement GetLastCaptionWithAudio(this CaptionRegion captions)
    {
      CaptionElement result = null;
      foreach (CaptionElement caption in captions.Children)
        if (caption.HasAudio())
          result = caption;
      return result;
    }

    public static void LoadAudio(this CaptionElement caption, Stream stream) //does not close stream
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      MemoryStream buffer = new MemoryStream();
      AudioRecorderView.LoadAudio(stream, buffer); //keep load logic encapsulated so that we can add decoding/decompression there
      captionExt.Audio = buffer;
    }

    public static void SaveAudio(this CaptionElement caption, Stream stream) //does not close stream
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      AudioRecorderView.SaveAudio(stream, captionExt.Audio); //keep save logic encapsulated so that we can add encoding/compression there
   }

    public static void SaveMergedAudio(this CaptionRegion captions, Stream output)
    {
      CaptionElement lastCaptionWithAudio = captions.GetLastCaptionWithAudio();
      if (lastCaptionWithAudio == null)
        throw new Exception("No audio exists");
      
      double totalDuration = lastCaptionWithAudio.End.TotalMilliseconds;

      bool firstAudio = true;
      double lastEnd = 0;

      foreach (CaptionElement caption in captions.Children)
      {
        Stream audio = caption.GetAudio();
        if (audio == null) continue; //skip entries that have no audio

        double currentBegin = caption.Begin.TotalMilliseconds;
        double delta = currentBegin - lastEnd; //this will be a negative number if there is overlap of current caption with audio and last caption that had audio
        double silenceDuration = Math.Max(0, delta); //this will give a non-zero (positive) result, only if delta>0
        double overlap = - Math.Min(0, delta); //this will give a non-zero (positive) result only if delta<0
        lastEnd = caption.End.TotalMilliseconds;
        double audioDuration = lastEnd - currentBegin;

        WavParser parsedAudio = new WavParser(audio);
        WAVEFORMATEX waveFormatEx = parsedAudio.WaveFormatEx;

        if (firstAudio)
        {
          firstAudio = false; //mark that we added the WAV header
          WavManager.WriteWavHeader(output,
                                    parsedAudio.AudioFormat,
                                    (uint)waveFormatEx.BufferSizeFromAudioDuration((long)(totalDuration * 10000))); //expects time in 100-nanosecond units [hns]
        }

        WavManager.WriteSilence(output, parsedAudio.WaveFormatEx, silenceDuration);

        uint overlapSize = (uint)waveFormatEx.BufferSizeFromAudioDuration((long)(overlap * 10000)); //expects time in 100-nanosecond units [hns]
        uint audioSize = (uint)waveFormatEx.BufferSizeFromAudioDuration((long)(audioDuration * 10000)); //expects time in 100-nanosecond units [hns]
        WavManager.WriteRawData(audio, output, WavManager.WAV_HEADER_SIZE + overlapSize, audioSize - overlapSize);
      }

    }

  }
}
