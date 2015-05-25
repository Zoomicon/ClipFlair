//Filename: SSAWriter.cs
//Version: 20150525

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.CaptionsLib.SSA
{

  public class SSAWriter : BaseCaptionWriter
  {

    #region --- Methods ---

    public override void WriteHeader(TextWriter writer)
    {
      writer.WriteLine("[Script Info]");
      writer.WriteLine("; This is a Sub Station Alpha v4 script.");
      writer.WriteLine("Title: "); //TODO: see how a title could be passed here
      writer.WriteLine("ScriptType: v4.00");
      writer.WriteLine("Collisions: Normal");
      writer.WriteLine("PlayDepth: 0");
      writer.WriteLine();
      writer.WriteLine("[V4 Styles]");
      writer.WriteLine("Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, TertiaryColour, BackColour, Bold, Italic, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, AlphaLevel, Encoding");
      writer.WriteLine("Style: Default,Arial,20,16777215,65535,65535,-2147483640,-1,0,1,2,1,2,10,10,10,0,1");
      writer.WriteLine();
      writer.WriteLine("[Events]");
      writer.WriteLine("Format: Marked, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text");
    }

    public override void WriteCaption(CaptionElement caption, TextWriter writer)
    {
      writer.WriteLine(SSAUtils.CaptionToSSAString(caption));
    }

    #endregion

  }

}