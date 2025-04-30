using System;
namespace MarsXApps.Models.Customs.LongdoMap
{
	public class Geocoding
	{
        public Geocoding()
        {
            geocode = string.Empty;
            country = string.Empty;
            province = string.Empty;
            district = string.Empty;
            subdistrict = string.Empty;
            postcode = string.Empty;
            aoi = string.Empty;
            elevation = 0;
            road = string.Empty;
            road_lon = 0;
            road_lat = 0;
            road_char = 0;
        }

        public string? geocode { get; set; }
        public string? country { get; set; }
        public string? province { get; set; }
        public string? district { get; set; }
        public string? subdistrict { get; set; }
        public string? postcode { get; set; }
        public string? aoi { get; set; }
        public int? elevation { get; set; }
        public string? road { get; set; }
        public double? road_lon { get; set; }
        public double? road_lat { get; set; }
        public int? road_char { get; set; }
    }
}

