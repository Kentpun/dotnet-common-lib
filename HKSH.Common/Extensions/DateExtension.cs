using MongoDB.Driver.Linq;
using System.Globalization;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// Date Extension
    /// </summary>
    public static class DateExtension
    {
        /// <summary>
        /// Gets the string type time stamp.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static string GetStringTypeTimeStamp(this DateTime time) => time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("F0");

        /// <summary>
        /// Gets the long type time stamp.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static long GetLongTypeTimeStamp(this DateTime time) => long.Parse(time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("F0"));

        /// <summary>
        /// Gets the local date time by time stamp.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <returns></returns>
        public static DateTime GetLocalDateTimeByTimeStamp(this long timeStamp) => DateTimeOffset.FromUnixTimeMilliseconds(timeStamp).LocalDateTime;

        /// <summary>
        /// Gets the local date time by time stamp.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <returns></returns>
        public static DateTime GetLocalDateTimeByTimeStamp(this string timeStamp) => GetLocalDateTimeByTimeStamp(long.Parse(timeStamp));

        /// <summary>
        /// Converts to readable.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <returns></returns>
        public static string ToReadable(this TimeSpan span)
        {
            if (span.Days > 0)
            {
                return $"{span.Days}天" + (span.Hours > 0 ? $"{span.Hours}小時" : "");
            }
            else
            {
                return "＜1天";
            }
        }

        /// <summary>
        /// Converts to readable.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <returns></returns>
        public static string? ToReadable(this TimeSpan? span)
        {
            if (!span.HasValue)
            {
                return null;
            }
            return ToReadable(span.Value);
        }

        /// <summary>
        /// Gets the date range minimum.
        /// </summary>
        /// <param name="dateString">The date string.</param>
        /// <returns></returns>
        public static DateTime? GetDateRangeMin(this string dateString) => DateTime.TryParse(dateString, out DateTime outDate) ? new DateTime(outDate.Year, outDate.Month, outDate.Day) : null;

        /// <summary>
        /// Gets the date range maximum.
        /// </summary>
        /// <param name="dateString">The date string.</param>
        /// <returns></returns>
        public static DateTime? GetDateRangeMax(this string dateString)
        {
            DateTime? date = dateString.GetDateRangeMin();
            return date.HasValue ? date.Value.AddDays(1) : null;
        }

        /// <summary>
        /// Converts to 8601string.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string To8601String(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.ToString("yyyy-MM-dd'T'HH:mm:ssZ", CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }
    }
}