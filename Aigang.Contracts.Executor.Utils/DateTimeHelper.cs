using System;
using System.Numerics;

namespace Aigang.Contracts.Executor.Utils
{
    public static class DateTimeHelper
    {
        public static long UnixNow()
        {
            TimeSpan span = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0,DateTimeKind.Utc);
            var unixTime = (long)span.TotalSeconds;
            return unixTime;
        }
        
        public static long ToUnix(DateTime time)
        {
            TimeSpan span = time - new DateTime(1970, 1, 1, 0, 0, 0, 0,DateTimeKind.Utc);
            var unixTime = (long)span.TotalSeconds;
            return unixTime;
        }
        
        public static DateTime UnixTimeStampToDateTime(BigInteger unixTimeStamp)
        {
            var numericValue = (long) unixTimeStamp;
            
            // Unix timestamp is seconds past epoch
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(numericValue);
            
            
            return dateTimeOffset.UtcDateTime;
        }

       
    }
}