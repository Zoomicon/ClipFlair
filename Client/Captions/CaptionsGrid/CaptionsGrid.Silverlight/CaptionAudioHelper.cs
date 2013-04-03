//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt.cs
//Version: 20130403

using Utils.Extensions;

using AudioLib;

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System;
using System.IO;
using System.Windows.Browser;
using System.Collections.Generic;

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// Audio helper class
  /// </summary>
  public static class CaptionAudioHelper
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

    public static void SaveAudio(CaptionRegion captions, Stream output)
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
