//using System;
//namespace MarsXApps.Module.Validation
//{
//	public class Helper
//	{
//        public static DateTime GetDateTimeByGMT(int value)
//        {
//            DateTimeOffset currentTimeUtc = DateTimeOffset.UtcNow;

//            int gmtOffset = value;

//            TimeSpan offset = TimeSpan.FromHours(gmtOffset);

//            DateTimeOffset currentTimeGmt = currentTimeUtc.ToOffset(offset);

//            return currentTimeGmt.DateTime;
//        }
//    }
//}

