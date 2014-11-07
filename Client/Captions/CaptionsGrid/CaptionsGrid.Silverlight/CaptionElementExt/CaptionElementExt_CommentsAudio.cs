//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: CaptionElementExt_CommentsAudio.cs
//Version: 20141107

using ClipFlair.AudioRecorder;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

using System.IO;

namespace ClipFlair.CaptionsGrid
{

  /// <summary>
  /// CommentsAudio extensions class for CaptionElementExt
  /// </summary>
  public static class CaptionElementExt_CommentsAudio
  {

    public static bool HasCommentsAudio(this CaptionElement caption)
    {
      CaptionElementExt c = caption as CaptionElementExt;
      return (c != null) ? (c.CommentsAudio != null) : false;
    }

    public static Stream GetCommentsAudio(this CaptionElement caption)
    {
      CaptionElementExt c = caption as CaptionElementExt;
      return (c != null) ? c.CommentsAudio : null;
    }

    public static void LoadCommentsAudio(this CaptionElement caption, Stream stream, int sizeHint = 0) //does not close stream
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      MemoryStream buffer = new MemoryStream(sizeHint); //using "sizeHint" only to set MemoryStream's initial capacity

      AudioRecorderView.LoadAudio(stream, buffer); //keep load logic encapsulated so that we can add decoding/decompression there
      captionExt.CommentsAudio = buffer;
    }

    public static void SaveCommentsAudio(this CaptionElement caption, Stream stream) //does not close stream
    {
      CaptionElementExt captionExt = caption as CaptionElementExt;
      if (captionExt == null) return;

      AudioRecorderView.SaveAudio(stream, captionExt.CommentsAudio); //keep save logic encapsulated so that we can add encoding/compression there
    }

  }
}
