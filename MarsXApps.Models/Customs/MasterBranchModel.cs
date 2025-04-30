using System;
namespace MarsXApps.Models.Customs
{
	public class MasterBranchModel
	{
        public int Id { get; set; }

        public string? Code { get; set; } = null!;

        public string? NameTh { get; set; } = null!;

        public string? NameEn { get; set; } = null!;

        public string? AddressTh { get; set; } = null!;

        public string? AddressEn { get; set; } = null!;

        public string? Telephone { get; set; } = null!;

        public decimal? Lat { get; set; }

        public decimal? Lon { get; set; }

        public string? Image { get; set; }

        public int? Seq { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? UrlGoogleMap { get; set; }

    }
}

