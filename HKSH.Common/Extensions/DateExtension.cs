using HKSH.Common.Constants;
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
        public static string GetStringTypeTimeStamp(this DateTime time) => time.ToUniversalTime().Subtract(DateTime.UnixEpoch).TotalMilliseconds.ToString("F0");

        /// <summary>
        /// Gets the long type time stamp.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static long GetLongTypeTimeStamp(this DateTime time) => long.Parse(time.ToUniversalTime().Subtract(DateTime.UnixEpoch).TotalMilliseconds.ToString("F0"));

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
        public static DateTime? GetDateRangeMin(this string dateString) => DateTime.TryParse(dateString, new CultureInfo(CultureInfoNameConstant.HK, true), DateTimeStyles.None, out DateTime outDate) ? new DateTime(outDate.Year, outDate.Month, outDate.Day, 0, 0, 0, DateTimeKind.Local) : null;

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
    }
}