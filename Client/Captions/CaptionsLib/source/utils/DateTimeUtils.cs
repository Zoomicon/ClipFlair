//Filename: DateTimeUtils.cs
//Version: 20131113

using System;

namespace ClipFlair.CaptionsLib.Utils
{
  
  public static class DateTimeUtils
  {

    #region --- Constants ---

    public static readonly DateTime DATETIMEZERO = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Local); //01/01/0001, 0:0:0.00 (dates must be from year 01 and above) '??? trying bigger year to avoid accuracy errors but it causes hichkups at the timebar

    #endregion

    #region --- Methods ---

    public static DateTime FixDateTime(DateTime datetime)
    {
      return new DateTime(DATETIMEZERO.Year, DATETIMEZERO.Month, DATETIMEZERO.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
    }

    public static double DateTimeToSeconds(DateTime theDateTime, DateTime baseDateTime, int digits)
    {
      double result = (theDateTime - baseDateTime).TotalSeconds;
      if (digits != -1)
        return Math.Round(result, digits);
      return result;
    }

    public static DateTime SecondsToDateTime(double seconds, DateTime baseDateTime, int digits)
    {
      try {
        if (digits != -1)
          seconds = Math.Round(seconds, digits);
        return baseDateTime.AddSeconds(seconds);
      } catch {
        return baseDateTime;
        //silently handle exceptions, returning the baseDateTime in that case
      }
    }

    public static DateTime TimeStrToDateTime(string datetimeStr, DateTime baseDateTime, string datetimeFormat)
    {
      DateTime result;
      try {
        result = DateTime.ParseExact(datetimeStr, datetimeFormat, null);
      //if exact datetime parsing fails, try with the plain parse method
      } catch {
        result = DateTime.Parse(datetimeStr);
      }
      result = FixDateTime(baseDateTime + (result - baseDateTime));
      return result;
    }

    public static double TimeStrToSeconds(string datetimeStr, DateTime baseDateTime, string datetimeFormat, int digits)
    {
      return DateTimeToSeconds(TimeStrToDateTime(datetimeStr, baseDateTime, datetimeFormat), baseDateTime, digits);
    }

    public static string SecondsToDateTimeStr(double seconds, DateTime baseDateTime, string datetimeFormat, int digits)
    {
      return SecondsToDateTime(seconds, baseDateTime, digits).ToString(datetimeFormat);
    }

    #endregion

  }

}