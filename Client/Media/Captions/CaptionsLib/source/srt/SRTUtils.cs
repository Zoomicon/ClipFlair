//Filenam: SRTUtils.cs
//Version: 20150525

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System;

namespace ClipFlair.CaptionsLib.SRT
{

  public sealed class SRTUtils
  {

    public const string SRTtimeFormat = "HH:mm:ss,fff";
    public const int SignificantDigits = 2; //Must use 2 instead of 3 as done at all the ClipFlair.CaptionsLib controls

    public const string SRT_TIME_SEPARATOR = " --> ";

    public static DateTime BaseTime = DateTimeUtils.DATETIMEZERO;

    public static string SecondsToSRTtime(double seconds)
    {
      return DateTimeUtils.SecondsToDateTimeStr(seconds, BaseTime, SRTtimeFormat, SignificantDigits).Replace('.', ':'); //fix: issue has been reported where resulting SRTs contained . instead of :
    }

    public static double SRTtimeToSeconds(string srtTime)
    {
      return DateTimeUtils.TimeStrToSeconds(srtTime.Replace('.', ':'), BaseTime, SRTtimeFormat, SignificantDigits); //fix: issue has been reported where resulting SRTs contained . instead of :
    }

    public static void SRTStringToCaption(string srtString, CaptionElement caption)
    {
      try
      {
        if (srtString == null) return;

        string[] TimesAndCaptions = StringUtils.Split(srtString, StringUtils.vbCrLf);

        string[] TimesOnly = StringUtils.Split(TimesAndCaptions[1], SRT_TIME_SEPARATOR);
        caption.Begin = TimeSpan.FromSeconds(SRTtimeToSeconds(TimesOnly[0]));
        caption.End = TimeSpan.FromSeconds(SRTtimeToSeconds(TimesOnly[1]));

        caption.Content = "";
        for (int i = 2; i <= TimesAndCaptions.Length - 1; i++)
        {
          if (!string.IsNullOrEmpty((string)caption.Content))
            caption.Content += StringUtils.vbCrLf;

          caption.Content += TimesAndCaptions[i];
        }

      }
      catch
      {
        throw new FormatException("Invalid SRT");
      }
    }

  }

}