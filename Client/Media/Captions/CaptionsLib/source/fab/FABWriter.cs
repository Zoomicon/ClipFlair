﻿//Filename: FABWriter.cs
//Version: 20150525

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.IO;

namespace ClipFlair.CaptionsLib.FAB
{

  public class FABWriter : BaseCaptionWriter
  {

    #region --- Methods ---

    public override void WriteCaption(CaptionElement caption, TextWriter writer)
    {
      writer.WriteLine(
        FABUtils.SecondsToFABtime(caption.Begin.TotalSeconds) + 
        FABUtils.FAB_TIME_SEPARATOR + 
        FABUtils.SecondsToFABtime(caption.End.TotalSeconds));

      writer.WriteLine(((string)caption.Content ?? "").CrToCrLf().PrefixEmptyLines(" "));
      
      writer.WriteLine();  //FAB format has an empty line AFTER each Caption, resuling to the file ending up at two empty lines (maybe done so that more Captions can be easily appended later on to the file)
    }

    #endregion

  }

}