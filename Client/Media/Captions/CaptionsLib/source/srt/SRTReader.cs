//Description: SRTReader class
//Version: 20140327

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.CaptionsLib.SRT
{

  public class SRTReader : BaseCaptionReader
  {

    #region --- Fields ---
    
    protected int fLineNumber; //=0
    protected string fLine; //=null
    
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
      fLine = "";
      //assuming we're reading a "file" from start, so resetting counter
    }

    public override void ReadCaption(CaptionElement caption, TextReader reader)
    {
      fLineNumber += 1;

      if (string.IsNullOrEmpty(fLine)) //do not use IsNullOrWhitespace here since we use a single space char for empty caption rows
        fLine = reader.ReadLine();

      string c = "";
      while (!string.IsNullOrEmpty(fLine)) //do not use IsNullOrWhitespace here since we use a single space char for empty caption rows
      {
        if (c != "")
          c += StringUtils.vbCrLf;
        c += (fLine != " ")? fLine : ""; //treating saved single space lines as empty lines
        fLine = reader.ReadLine();
      }

      while ((fLine != null) && (fLine == "")) //skip any empty lines between captions
        fLine = reader.ReadLine();

      if (c != "")
        SRTUtils.SRTStringToCaption(c, caption);
    }

    public override void ReadFooter(System.IO.TextReader reader)
    {
      fLine = null;
    }

    #endregion

  }

}