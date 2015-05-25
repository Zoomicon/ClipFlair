//Filename: EncoreWriter.cs
//Version: 20150525

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.CaptionsLib.Encore
{

  public class EncoreWriter : BaseCaptionWriter
  {

    #region --- Methods ---

    public override void WriteCaption(CaptionElement caption, TextWriter writer)
    {
      writer.WriteLine(
        EncoreUtils.SecondsToEncoreTime(caption.Begin.TotalSeconds) + " " + 
        EncoreUtils.SecondsToEncoreTime(caption.End.TotalSeconds) + " " + 
        ((string)caption.Content ?? "").CrToCrLf());
    }

    #endregion

  }

}