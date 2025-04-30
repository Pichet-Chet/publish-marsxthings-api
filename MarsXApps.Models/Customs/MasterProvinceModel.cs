using System;

namespace MarsXApps.Models.Customs
{
	public class MasterProvinceModel
    {
		public MasterProvinceModel()
		{
		}

        public int Id { get; set; }

        public string? NameTh { get; set; }

        public string? NameEn { get; set; }

        public int? MasterGeographiesId { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public virtual MasterGeographyModel MasterGeographyModel { get; set; }
    }
}

