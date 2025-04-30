using System;
namespace MarsXApps.Models.Customs.Return.SasOrder
{
    public class SasOrderHeaderReturnModel
    {
        public SasOrderHeaderReturnModel()
        {
        }

        public int Id { get; set; }

        public string OrderNo { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string SysCustomerId { get; set; } = null!;

        public string StatusValue { get; set; } = null!;

        public string StatusNameTh { get; set; } = null!;

        public string StatusNameEn { get; set; } = null!;

        public string ContactName { get; set; }

        public string Description { get; set; } = null!;

        public string? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string MachineModel { get; set; } = null!;

        public string ServiceDate { get; set; } = null!;

        public string ServicePeriodTime { get; set; }

        public string AddressTh { get; set; } = null!;

        public string AddressEn { get; set; } = null!;

        public string? CaecJobNo { get; set; }

        public string SumPrice { get; set; } = null!;

        public string? MachineSerialNo { get; set; }

        public string? MachineChassic { get; set; }

        public string? BranchName { get; set; }


    }
}

