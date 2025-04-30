using System;
namespace MarsXApps.Models.Customs.Return.SasOrder
{
	public class SasOrderDetialPaymentReturnModel
	{
		public SasOrderDetialPaymentReturnModel()
		{
            paymentChannels = new List<PaymentChannel>();

            caecItemMasters = new List<CaecItemMaster>();
        }

		public int Id { get; set; }

        public string Type { get; set; } = null!;

        public string StatusValue { get; set; } = null!;

        public string StatusNameTh { get; set; } = null!;

        public string StatusNameEn { get; set; } = null!;

        public string? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

		public string OrderNo { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string ContactTel { get; set; } = null!;

        public string MachineModel { get; set; } = null!;

        public DateTime? ServiceDate { get; set; } = null!;

        public string ServicePeriodTime { get; set; } = null!;

        public string MachineSerial { get; set; } = null!;

        public string MachineChassic { get; set; } = null!;

        public string InsuranceType { get; set; } = null!;

        public string AddressTh { get; set; } = null!;

        public string AddressEn { get; set; } = null!;

        public List<PaymentChannel> paymentChannels { get; set; }

        public List<CaecItemMaster> caecItemMasters { get; set; }

        public string SumPrice { get; set; } = null!;

        public string CaecJobNo { get; set; } = null!;

    }

    public class PaymentChannel
    {
        public string Code { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string? NameEn { get; set; }

        public string? Image { get; set; }

        public string? BankNameTh { get; set; }

        public string? BankNameEn { get; set; }

        public string? BankIcon { get; set; }

        public string? BankNo { get; set; }
    }

    public class CaecItemMaster
    {
        public CaecItemMaster()
        {
            ItemName = string.Empty;
            Quantity = string.Empty;
            Price = string.Empty;
        }

        public string ItemName { get; set; }

        public string Quantity { get; set; }

        public string Price { get; set; }
    }
}

