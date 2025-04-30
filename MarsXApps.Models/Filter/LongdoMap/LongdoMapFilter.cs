using System;
namespace MarsXApps.Models.Filter.LongdoMap
{
	public class LongdoMapFilter
	{
		public LongdoMapFilter()
		{
		}

        public string? key { get; set; }
        public string? locale { get; set; }
        public double? lat { get; set; }
        public double? lon { get; set; }
    }
}

