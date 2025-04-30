using System;
namespace MarsXApps.Models.Customs.Return.SasOrder
{
    public class SasOrderDetialReturnModel
    {
        public SasOrderDetialReturnModel()
        {
            sasOrderTrans = new List<SasOrderTranReturnModel>();
        }

        public int Id { get; set; } 

        public string Type { get; set; } = null!;

        public string StatusValue { get; set; }

        public string StatusNameTh { get; set; } = null!;

        public string StatusNameEn { get; set; } = null!;

        public string OrderNo { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string ContactTel { get; set; } = null!;

        public string AddressTh { get; set; } = null!;

        public string AddressEn { get; set; } = null!;

        public string MachineModel { get; set; } = null!;

        public string MachineSerial { get; set; } = null!;

        public string MachineChassic { get; set; } = null!;

        public List<SasOrderTranReturnModel> sasOrderTrans { get; set; }

    }
}

