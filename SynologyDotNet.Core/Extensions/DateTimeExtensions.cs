using System;

namespace SynologyDotNet.Core.Extensions
{
    /// <summary>
    /// Extension methods to convert from and to Unix timestamps. Be aware that DSM sometimes returns Unix timestamps in seconds, and sometimes in milliseconds
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Unix seconds

        /// <summary>
        /// Converts Unix time in seconds to a DateTime
        /// </summary>
        /// <param name="unixTime">The unix time.</param>
        /// <returns></returns>
        public static DateTime FromUnixSecondsToDateTimeUtc(this long unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
        }

        /// <summary>
        /// Converts from DateTime to a Unix time in seconds
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static long FromDateTimeUtcToUnixSeconds(this DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Converts Unix time in seconds to a DateTime
        /// </summary>
        /// <param name="unixTime">The unix time.</param>
        /// <returns></returns>
        public static DateTime? FromUnixSecondsToDateTimeUtc(this long? unixTime)
        {
            return unixTime?.FromUnixSecondsToDateTimeUtc();
        }

        /// <summary>
        /// Converts from DateTime to a Unix time in seconds
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static long? FromDateTimeUtcToUnix(this DateTime? dateTime)
        {
            return dateTime?.FromDateTimeUtcToUnixSeconds();
        }

        #endregion

        #region Milliseconds

        /// <summary>
        /// Converts Unix time in milliseconds to a DateTime
        /// </summary>
        /// <param name="unixTime">The unix time.</param>
        /// <returns></returns>
        public static DateTime FromUnixMillisecondsToDateTimeUtc(this long unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixTime);
        }

        /// <summary>
        /// Converts from DateTime to a Unix time in milliseconds
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static long FromDateTimeUtcToUnixMilliseconds(this DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Converts Unix time in milliseconds to a DateTime
        /// </summary>
        /// <param name="unixTime">The unix time.</param>
        /// <returns></returns>
        public static DateTime? FromUnixMillisecondsToDateTimeUtc(this long? unixTime)
        {
            return unixTime?.FromUnixMillisecondsToDateTimeUtc();
        }

        /// <summary>
        /// Converts from DateTime to a Unix time in milliseconds
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static long? FromDateTimeUtcToUnixMilliseconds(this DateTime? dateTime)
        {
            return dateTime?.FromDateTimeUtcToUnixMilliseconds();
        }

        #endregion

    }
}
