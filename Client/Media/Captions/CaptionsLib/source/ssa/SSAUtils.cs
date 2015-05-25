//Filenam: SSAUtils.cs
//Version: 20150525

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System;

namespace ClipFlair.CaptionsLib.SSA
{

  public sealed class SSAUtils
  {

    public const string SSAtimeFormat = "HH:mm:ss.ff";
    public const int SignificantDigits = 2; //Must use 2 instead of 3 as done at all the ClipFlair.CaptionsLib controls

    public const string SSA_EVENT_START = "Dialogue:";
    public const string SSA_LINE_SEPARATOR = "\\N";

    public static DateTime BaseTime = DateTimeUtils.DATETIMEZERO;

    public static string SecondsToSSAtime(double seconds)
    {
      return DateTimeUtils.SecondsToDateTimeStr(seconds, BaseTime, SSAtimeFormat, SignificantDigits);
    }

    public static double SSAtimeToSeconds(string ssaTime)
    {
      return DateTimeUtils.TimeStrToSeconds(ssaTime, BaseTime, SSAtimeFormat, SignificantDigits);
    }

    public static void SSAStringToCaption(string ssaString, CaptionElement caption)
    {
      try
      {
        if (string.IsNullOrEmpty(ssaString)) return;

        string[] items = StringUtils.Split(ssaString, ",");
        
        string beginTime = items[1];
        string endTime = items[2];
        
        string role = items[4]; //TODO: see how we can give this to CaptionElement (cast to CaptionElementExt???)
        
        //note: reconstruct caption string if it has been split too due to "," chars contained in it
        string captionText = items[9];
        for (int i = 10; i < items.Length; i++)
          captionText += "," + items[i];
        
        caption.Begin = TimeSpan.FromSeconds(SSAtimeToSeconds(beginTime));
        caption.End = TimeSpan.FromSeconds(SSAtimeToSeconds(endTime));
        caption.Content = captionText.Replace(SSA_LINE_SEPARATOR, StringUtils.vbCrLf);
      }
      catch
      {
        throw new FormatException("Invalid SSA");
      }
    }

    public static string CaptionToSSAString(CaptionElement caption)
    {
      return
        SSA_EVENT_START +
        " Marked=0," +
        SecondsToSSAtime(caption.Begin.TotalSeconds) + "," + 
        SecondsToSSAtime(caption.End.TotalSeconds) + "," +
        "Default," +
        "" + "," +
        "0000,0000,0000,!Effect," + 
        ((string)caption.Content ?? "").CrToCrLf().Replace(StringUtils.vbCrLf, SSA_LINE_SEPARATOR);
    }

  }

}