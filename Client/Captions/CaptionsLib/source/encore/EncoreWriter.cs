//Filename: EncoreWriter.cs
//Version: 20131113

using System.IO;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.CaptionsLib.Encore
{

  public class EncoreWriter : BaseCaptionWriter
  {

    #region --- Methods ---

    public override void WriteCaption(CaptionElement caption, TextWriter writer)
    {
      writer.WriteLine(EncoreUtils.SecondsToEncoreTime(caption.Begin.TotalSeconds) + " " + EncoreUtils.SecondsToEncoreTime(caption.End.TotalSeconds) + " " + caption.Content); //TODO: assuming Caption alredy contains CRLF between rows (not at ending row) - may should first convert LFs to CRLFs, then also trip CRLF's at end
    }

    #endregion

  }

}