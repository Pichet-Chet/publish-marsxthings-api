using System;
namespace MarsXApps.Models.Customs
{
	public class MasterPaymentChannelModel
	{
		public MasterPaymentChannelModel()
		{
		}

        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string? NameEn { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public int? Seq { get; set; }

        public string? BankNameTh { get; set; }

        public string? BankNameEn { get; set; }

        public string? BankIcon { get; set; }

        public string? BankNo { get; set; }
    }
}

