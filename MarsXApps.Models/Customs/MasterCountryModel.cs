using System;
namespace MarsXApps.Models.Customs
{
	public class MasterCountryModel
	{
		public MasterCountryModel()
		{
		}

        public int Id { get; set; }

        public string? NameTh { get; set; }

        public string? NameEn { get; set; }

        public string? Code { get; set; }

        public string? CurrencyCode { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? Image { get; set; }
    }
}

