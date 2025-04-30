using System;
namespace MarsXApps.Models.Extensions
{
	public static class DateTimeExtensions
	{
        private static DateTime? _customDateTime;

        // Extension method to get the current date and time
        public static DateTime MarsX(this DateTime dateTime)
        {
            return _customDateTime ?? DateTime.Now;
        }

        // Method to set a custom date and time
        public static void SetCustomDateTime(DateTime customDateTime)
        {
            _customDateTime = customDateTime;
        }

        // Method to reset to the system date and time
        public static void ResetToSystemTime()
        {
            _customDateTime = null;
        }
    }
}

