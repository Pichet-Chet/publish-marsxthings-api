using System;
namespace MarsXApps.Models.Customs.CAEC
{
	public class WorksheetsPmDetailModel
	{
        public string jobNo { get; set; }
        public int seqNo { get; set; }
        public string itemsDetail { get; set; }
        public int noOfItems { get; set; }
        public decimal unitPrice { get; set; }
        public string uom { get; set; }
        public int? pmHour { get; set; }
        public string? partNumber { get; set; }
        public string? itemType { get; set; }
    }
}

