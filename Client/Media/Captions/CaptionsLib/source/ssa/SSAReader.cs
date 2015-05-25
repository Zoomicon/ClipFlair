//Description: SSAReader class
//Version: 20150525

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.CaptionsLib.SSA
{

  public class SSAReader : BaseCaptionReader
  {

    #region --- Methods ---

    public override void ReadHeader(System.IO.TextReader reader)
    {
      string s = reader.ReadLine();
      while (s != null)
      {
        if (s == "[Events]")
        {
          reader.ReadLine(); //skip the "Format:" row //TODO: check SSA reference PDF if the format can vary (or order of its elements)
          break;
        }
        s = reader.ReadLine();
      }
    }

    public override void ReadCaption(CaptionElement caption, TextReader reader)
    {
      SSAUtils.SSAStringToCaption(reader.ReadLine(), caption);
    }

    public override void ReadFooter(System.IO.TextReader reader)
    {
      //sometimes more sections can follow the [Events] one, just ignore them
    }

    #endregion

  }

}