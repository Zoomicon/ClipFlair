﻿//Filenam: SRTUtils.cs
//Version: 20131114

using ClipFlair.CaptionsLib.Utils;

using System;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace ClipFlair.CaptionsLib.SRT
{

  public sealed class SRTUtils
  {

    public const string SRTtimeFormat = "HH:mm:ss,fff";
      //Must use 2 instead of 3 as done at all the ClipFlair.CaptionsLib controls
    public const int SignificantDigits = 2;

    public const string SRT_TIME_SEPARATOR = " --> ";

    public static DateTime BaseTime = DateTimeUtils.DATETIMEZERO;
    public static string SecondsToSRTtime(double seconds)
    {
      return DateTimeUtils.SecondsToDateTimeStr(seconds, BaseTime, SRTtimeFormat, SignificantDigits);
    }

    public static double SRTtimeToSeconds(string srtTime)
    {
      return DateTimeUtils.TimeStrToSeconds(srtTime, BaseTime, SRTtimeFormat, SignificantDigits);
    }

    public static void SRTStringToCaption(string srtString, CaptionElement caption)
    {
      try
      {
        if (srtString != null)
        {
          string[] TimesAndCaptions = StringUtils.Split(srtString, StringUtils.vbCrLf);

          string[] TimesOnly = StringUtils.Split(TimesAndCaptions[1], SRT_TIME_SEPARATOR);
          caption.Begin = TimeSpan.FromSeconds(SRTtimeToSeconds(TimesOnly[0]));
          caption.End = TimeSpan.FromSeconds(SRTtimeToSeconds(TimesOnly[1]));

          caption.Content = "";
          for (int i = 2; i <= TimesAndCaptions.Length - 1; i++)
          {
            if ((caption.Content != ""))
              caption.Content += StringUtils.vbCrLf;
            caption.Content += TimesAndCaptions[i];
          }
        }
      }
      catch
      {
        throw new Exception("Invalid SRT");
        //TODO: localize
      }
    }

  }

}