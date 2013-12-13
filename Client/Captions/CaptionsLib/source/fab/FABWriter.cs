//Filename: FABWriter.cs
//Version: 20131114

using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.CaptionsLib.FAB
{

  public class FABWriter : BaseCaptionWriter
  {

    #region --- Methods ---

    public override void WriteCaption(CaptionElement caption, TextWriter writer)
    {
      writer.WriteLine(FABUtils.SecondsToFABtime(caption.Begin.TotalSeconds) + "  " + FABUtils.SecondsToFABtime(caption.End.TotalSeconds));  //separator is double space
      writer.WriteLine(caption.Content); //TODO: assuming Caption alredy contains CRLF between rows (not at ending row) - may should first convert LFs to CRLFs, then also trip CRLF's at end
      writer.WriteLine();  //FAB format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
    }

    #endregion

  }

}