using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    /// <summary>
    /// DateTime Helper
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Ics the bit to date time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static DateTime ICBitToDateTime(this string time)
        {
            var splits = time.Split(' ');
            splits[0] = Regex.Replace(splits[0], @"[a-z]", "", RegexOptions.IgnoreCase).PadLeft(2, '0');

            var cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            return DateTime.ParseExact(string.Join(" ", splits), "dd MMM yyyy HH:mm:ss", cultureInfo);
        }

        /// <summary>
        /// To the time stamp.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime dateTime)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(dateTime - startTime).TotalMilliseconds;
        }
    }
}
