//Filename: TTSUtils.cs
//Version: 20140322

using ClipFlair.CaptionsLib.Utils;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System;

namespace ClipFlair.CaptionsLib.TTS
{

  public static class TTSUtils
  {

    public const string TTStimeFormat = "H:mm:ss.ff"; //do not use HH since TTS doesn't require two digits for the hour
    public const int SignificantDigits = 2;

    public const string TTS_TIME_END = ",NTP ";

    public static DateTime BaseTime = DateTimeUtils.DATETIMEZERO;
    public static string SecondsToTTStime(double seconds)
    {
      return DateTimeUtils.SecondsToDateTimeStr(seconds, BaseTime, TTStimeFormat, SignificantDigits);
    }

    public static double TTStimeToSeconds(string ttsTime)
    {
      return DateTimeUtils.TimeStrToSeconds(ttsTime, BaseTime, TTStimeFormat, SignificantDigits);
    }

    public static string CaptionToTTSString(CaptionElement caption)
    {
      return SecondsToTTStime(caption.Begin.TotalSeconds) + "," + SecondsToTTStime(caption.End.TotalSeconds) + TTS_TIME_END + ((string)caption.Content).CrToCrLf().Replace(StringUtils.vbCrLf, "|");
    }

    public static void TTSStringToCaption(string ttsString, CaptionElement caption)
    {
      try
      {
        string[] TimesAndCaptions = StringUtils.Split(ttsString, TTS_TIME_END);

        string[] TimesOnly = StringUtils.Split(TimesAndCaptions[0], ",");
        caption.Begin = TimeSpan.FromSeconds(TTStimeToSeconds(TimesOnly[0]));
        caption.End = TimeSpan.FromSeconds(TTStimeToSeconds(TimesOnly[1]));

        caption.Content = TimesAndCaptions[1].Replace("|",  StringUtils.vbCrLf);
      }
      catch
      {
        throw new Exception("Invalid TTS");
        //TODO: localize
      }
    }

  }

}