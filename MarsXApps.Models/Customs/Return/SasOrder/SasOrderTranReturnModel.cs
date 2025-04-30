using System;
namespace MarsXApps.Models.Customs.Return.SasOrder
{
    public class SasOrderTranReturnModel
    {
        public SasOrderTranReturnModel()
        {
        }

        public DateTime tranDate { get; set; }

        public string StatusValue { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public string SubTitleNameTh { get; set; } = null!;

        public string SubTitleNameEn { get; set; } = null!;

        public string Remark { get; set; } = null!;

        public string? ImageUrl{ get; set; }

    }
}

