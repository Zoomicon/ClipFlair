//Description: SRTReader class
//Version: 20131113

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.CaptionsLib.SRT
{

  public class SRTReader : BaseCaptionReader
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

    public override void ReadHeader(System.IO.TextReader reader)
    {
      fLineNumber = 0;
      //assuming we're reading a "file" from start, so resetting counter
    }

    public override void ReadCaption(CaptionElement Caption, TextReader reader)
    {
      fLineNumber += 1;

      string line = reader.ReadLine();
      string c = "";
      //TODO: must change this to detect a blank line (treated as separator) before the end of the file or just before a line with the next number
      while ((line != null) && (line != "")) {
        if ((c != ""))
          c +=  StringUtils.vbCrLf;
        c += line;
        line = reader.ReadLine();
      }
      if ((c != ""))
        SRTUtils.SRTStringToCaption(c, Caption);
    }

    #endregion

  }

}