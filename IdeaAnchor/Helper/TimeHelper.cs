using System;
using System.Globalization;

namespace IdeaAnchor.Helper
{
	public static class TimeHelper
	{
		public static string GetTimeStamp()
		{
			//gives an ISO 8601 date time string
			return DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
        }

		public static string GetReadableTime(DateTime time)
		{
			return time.ToShortDateString();
		}

		public static DateTime ToDateTime(this string timestamp)
		{
            return DateTime.Parse(timestamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
	}
}

