using System;
namespace MarsXApps.Models.Filter.LongdoMap
{
    public class ReverseGeocodingFilter
    {
        public ReverseGeocodingFilter()
        {
        }

        public string? Locale { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
    }
}

