using System;
namespace MarsXApps.Models.Customs
{
	public class SysTermsAndConditionsTransactionModel
	{
		public SysTermsAndConditionsTransactionModel()
		{
		}

        public int Id { get; set; }

        public int SysTermsAndConditionsId { get; set; }

        public Guid SysCustomersId { get; set; }

        public bool Consideration { get; set; }

        public DateTime? ConsiderationDate { get; set; }

        public string? DeviceId { get; set; }

        public string? DeviceName { get; set; }
    }
}

