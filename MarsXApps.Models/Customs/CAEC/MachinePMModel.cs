using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsXApps.Models.Customs.CAEC
{
    public class MachinePMModel
    {
        public string? jobNo { get; set; }
        public string? uiKeyValue { get; set; }
        public int? id { get; set; }
        public string? mcModelGobal { get; set; }
        public int? pmHour { get; set; }
        public int? seqNo { get; set; }
        public string? itemsDetail { get; set; }
        public int? noOfItems { get; set; }
        public decimal? unitPrice { get; set; }
        public string? uom { get; set; }
        public string? remark { get; set; }
        public DateTime? createDate { get; set; }
        public string? createUser { get; set; }
        public DateTime? updateDate { get; set; }
        public string? updateUser { get; set; }
        public string? partNumber { get; set; }
        public string? sumPrice { get; set; }
        public string? sumPriceWordTh { get; set; }
        public string? itemType { get; set; }
    }
}
