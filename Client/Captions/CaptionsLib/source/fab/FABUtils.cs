//Filename: FABUtils.cs
//Version: 20131105

using System;
using ClipFlair.CaptionsLib.Utils;

namespace ClipFlair.CaptionsLib.FAB
{

	public sealed class FABUtils
	{

		public const string FABtimeFormat = "HH:mm:ss:ff";

		public const int SignificantDigits = 2;

		public static DateTime BaseTime = DateTimeUtils.DATETIMEZERO;
		public static string SecondsToFABtime(double seconds)
		{
      return DateTimeUtils.SecondsToDateTimeStr(seconds, BaseTime, FABtimeFormat, SignificantDigits);
		}

		public static double FABtimeToSeconds(string FABTime)
		{
      return DateTimeUtils.TimeStrToSeconds(FABTime, BaseTime, FABtimeFormat, SignificantDigits);
		}

	}

}