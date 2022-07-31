using System;

namespace SynologyDotNet.Core.Extensions
{
    public static class DateTimeExtensions
    {
        #region Unix seconds

        public static DateTime FromUnixSecondsToDateTimeUtc(this long unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
        }

        public static long FromDateTimeUtcToUnixSeconds(this DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }

        public static DateTime? FromUnixSecondsToDateTimeUtc(this long? unixTime)
        {
            return unixTime?.FromUnixSecondsToDateTimeUtc();
        }

        public static long? FromDateTimeUtcToUnix(this DateTime? dateTime)
        {
            return dateTime?.FromDateTimeUtcToUnixSeconds();
        }

        #endregion

        #region Milliseconds

        public static DateTime FromUnixMillisecondsToDateTimeUtc(this long unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixTime);
        }

        public static long FromDateTimeUtcToUnixMilliseconds(this DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
        }

        public static DateTime? FromUnixMillisecondsToDateTimeUtc(this long? unixTime)
        {
            return unixTime?.FromUnixMillisecondsToDateTimeUtc();
        }

        public static long? FromDateTimeUtcToUnixMilliseconds(this DateTime? dateTime)
        {
            return dateTime?.FromDateTimeUtcToUnixMilliseconds();
        }

        #endregion

    }
}
