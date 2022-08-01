
using System;

namespace DiasShared.Operations.DateOperations
{
    public static class DateFunctions
    {
        /// <summary>
        /// Use on Hmac operations
        /// </summary>
        /// <returns></returns>
        public static TimeSpan CalculateTimeSpanForHmacOperations()
        {
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan timeSpan = DateTime.UtcNow - epochStart;

            return timeSpan;
        }
    }
}
