//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt.cs
//Version: 20130401

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

    public static void SaveAudio(CaptionRegion captions, Stream output)
    {
      List<Stream> wavStreams = new List<Stream>();
      foreach (CaptionElement c in captions.Children)
      {
        Stream audio = c.GetAudio();
        if (audio != null)
          wavStreams.Add(audio); //TODO: add silence in between (and as padding at each audio entry to make correct duration), add info on where to stop processing streams (time limit)
      }

      WavManager.ConcatenateWavs(wavStreams.ToArray(), output); //TODO: test, need to replace the call to this one with similar code (see comment at that method implementation)
    }

  }
}
