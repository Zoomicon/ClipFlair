//Filename: SRTWriter.cs
//Version: 20131120

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.CaptionsLib.SRT
{

  public class SRTWriter : BaseCaptionWriter
  {

    #region --- Fields ---
    
    protected int fLineNumber;
    
    #endregion

    #region --- Properties ---

    public int LineNumber {
      get { return fLineNumber; }
    }

    #endregion

    #region --- Methods ---

    public override void WriteHeader(TextWriter writer)
    {
      fLineNumber = 0; //assuming we're writing a new "file", so resetting counter
    }

    public override void WriteCaption(CaptionElement caption, TextWriter writer)
    {
      fLineNumber += 1;
      writer.WriteLine(LineNumber); //assuming NewLine property of writer has been set to  StringUtils.vbCrLf
      writer.WriteLine(SRTUtils.SecondsToSRTtime(caption.Begin.TotalSeconds) + SRTUtils.SRT_TIME_SEPARATOR + SRTUtils.SecondsToSRTtime(caption.End.TotalSeconds));
      if (!string.IsNullOrEmpty((string)caption.Content))
        writer.WriteLine(((string)caption.Content).Replace(StringUtils.vbCrLf + StringUtils.vbCrLf, StringUtils.vbCrLf + " " + StringUtils.vbCrLf)); //never write an empty line (since the parser can treat it as a caption end)
      writer.WriteLine();  //SRT format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
    }

    #endregion

  }

}