//Filename: EncoreUtils.cs
//Version: 20131105

using System;
using ClipFlair.CaptionsLib.Utils;

namespace ClipFlair.CaptionsLib.Encore
{

  public sealed class EncoreUtils
  {
    //for Adobe Encore

    public const string EncoreTimeFormat = "HH:mm:ss:ff";

    public const int SignificantDigits = 2;

    public static DateTime BaseTime = DateTimeUtils.DATETIMEZERO;
    public static string SecondsToEncoreTime(double seconds)
    {
      return DateTimeUtils.SecondsToDateTimeStr(seconds, BaseTime, EncoreTimeFormat, SignificantDigits);
    }

    public static double EncoreTimeToSeconds(string EncoreTime)
    {
      return DateTimeUtils.TimeStrToSeconds(EncoreTime, BaseTime, EncoreTimeFormat, SignificantDigits);
    }

  }

}